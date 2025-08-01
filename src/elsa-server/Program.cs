using Elsa.EntityFrameworkCore.Extensions;
using Elsa.EntityFrameworkCore.Modules.Management;
using Elsa.EntityFrameworkCore.Modules.Runtime;
using Elsa.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddElsa(elsa =>
{
    // Configure Management layer to use EF Core.
    elsa.UseWorkflowManagement(management => management.UseEntityFrameworkCore(options => options.UseSqlite("Data Source=elsa.sqlite.db")));

    // Configure Runtime layer to use EF Core.
    elsa.UseWorkflowRuntime(runtime => runtime.UseEntityFrameworkCore(options => options.UseSqlite("Data Source=elsa.sqlite.db")));

    // Default Identity features for authentication/authorization.
    elsa.UseIdentity(identity =>
    {
        //identity.TokenOptions = options => options.SigningKey = "sufficiently-large-secret-signing-key"; // This key needs to be at least 256 bits long.
        identity.TokenOptions = options =>
        {
            options.SigningKey = "c7dc81876a782d502084763fa322429fca015941eac90ce8ca7ad95fc8752035";
            options.AccessTokenLifetime = TimeSpan.FromDays(1);
        };
        identity.UseAdminUserProvider();
    });

    // Configure ASP.NET authentication/authorization.
    elsa.UseDefaultAuthentication(auth => auth.UseAdminApiKey());

    // Expose Elsa API endpoints.
    elsa.UseWorkflowsApi();

    // Setup a SignalR hub for real-time updates from the server.
    elsa.UseRealTimeWorkflows();

    // Enable JavaScript workflow expressions.
    elsa.UseJavaScript();

    // Enable C# workflow expressions.
    elsa.UseCSharp();

    // Enable HTTP activities.
    elsa.UseHttp();

    // Use timer activities.
    elsa.UseScheduling();

    //elsa.UseEmail(email =>
    //{
    //    // Configure email options from the configuration file.
    //    email.ConfigureOptions = options =>
    //    {
    //        builder.Configuration.GetSection("Email").Bind(options);
    //    };
    //});
    elsa.UseEmail();

    // Register custom activities from the application, if any.
    elsa.AddActivitiesFrom<Program>();

    // Register custom workflows from the application, if any.
    elsa.AddWorkflowsFrom<Program>();
    
    // Enable Webhooks.
    elsa.UseWebhooks(webhooks =>
    {
        webhooks.ConfigureSinks = options =>
        {
            builder.Configuration.GetSection("Webhooks").Bind(options);
        };
    });
});

// Configure CORS to allow designer app hosted on a different origin to invoke the APIs.
builder.Services.AddCors(cors => cors
    .AddDefaultPolicy(policy => policy
        .AllowAnyOrigin() // For demo purposes only. Use a specific origin instead.
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithExposedHeaders("x-elsa-workflow-instance-id"))); // Required for Elsa Studio in order to support running workflows from the designer. Alternatively, you can use the `*` wildcard to expose all headers.

// Build the web application.
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure web application's middleware pipeline.
app.UseCors();
app.UseRouting(); // Required for SignalR.
app.UseAuthentication();
app.UseAuthorization();
app.UseWorkflowsApi(); // Use Elsa API endpoints.
app.UseWorkflows(); // Use Elsa middleware to handle HTTP requests mapped to HTTP Endpoint activities.
app.UseWorkflowsSignalRHubs(); // Optional SignalR integration. Elsa Studio uses SignalR to receive real-time updates from the server.

app.Run();