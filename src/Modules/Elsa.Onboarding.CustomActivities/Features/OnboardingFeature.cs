using Elsa.Extensions;
using Elsa.Features.Abstractions;
using Elsa.Features.Services;
using Elsa.Onboarding.CustomActivities.Activities;
using Elsa.Workflows.Management;

namespace Elsa.Onboarding.CustomActivities.Features;

public class OnboardingFeature(IModule module) : FeatureBase(module)
{
    //public override void Apply()
    //{
    //    //Services.AddScoped<IEmployeeService, EmployeeService>();
    //}

    public override void Configure()
    {
        Module.UseWorkflowManagement(management =>
        {
            management.AddVariableTypeAndAlias<EmployeeInput>(category: "Onboarding");
            management.AddActivitiesFrom<OnboardingFeature>();
        });
    }
}
