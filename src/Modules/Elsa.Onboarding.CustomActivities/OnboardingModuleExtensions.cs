using Elsa.Features.Services;
using Elsa.Onboarding.CustomActivities.Features;

namespace Elsa.Onboarding.CustomActivities;

public static class OnboardingModuleExtensions
{
    public static IModule UseOnboarding(this IModule module, Action<OnboardingFeature>? configure = null)
    {
        module.Configure(configure);
        return module;
    }
}
