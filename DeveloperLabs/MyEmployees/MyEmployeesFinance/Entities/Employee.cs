namespace MyEmployeesFinance.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public double AnnualCompensation { get; set; }
        public double AnnualBonus { get; set; }
    }
}
