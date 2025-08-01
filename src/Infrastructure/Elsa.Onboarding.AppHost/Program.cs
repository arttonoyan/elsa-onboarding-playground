var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ElsaServer>("elsaserver");

builder.AddProject<Projects.Onboarding>("onboarding");

builder.Build().Run();
