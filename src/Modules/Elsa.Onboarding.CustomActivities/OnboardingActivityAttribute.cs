using Elsa.Workflows.Attributes;

namespace Elsa.Onboarding.CustomActivities;

[AttributeUsage(AttributeTargets.Class)]
public class OnboardingActivityAttribute : ActivityAttribute
{
    public OnboardingActivityAttribute()
        : base(@namespace: "Playground", category: "Onboarding")
    { }
}