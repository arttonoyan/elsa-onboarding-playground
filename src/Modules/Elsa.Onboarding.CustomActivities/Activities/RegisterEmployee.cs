using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;

namespace Elsa.Onboarding.CustomActivities.Activities;

[OnboardingActivity(
    DisplayName = "Register Employee",
    Description = "Registers a new employee in the system.")]
public class RegisterEmployee : CodeActivity<Employee>
{
    public Input<Employee> Employee { get; set; } = default!;
    public Output<Employee> RegisteredEmployee { get; set; } = default!;

    protected override ValueTask ExecuteAsync(ActivityExecutionContext context)
    {
        var employee = context.Get(Employee);
        if (employee == null)
            throw new InvalidOperationException("Employee input cannot be null.");

        employee.Name += "_updated";

        context.Set(RegisteredEmployee, employee);

        return ValueTask.CompletedTask;
        // Do something with employee.Name or employee.Email
    }
}
