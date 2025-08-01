namespace Elsa.Onboarding.CustomActivities.Activities;

public class EmployeeInput
{
    public Employee Employee { get; set; } = default!;
}

public class Employee
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
