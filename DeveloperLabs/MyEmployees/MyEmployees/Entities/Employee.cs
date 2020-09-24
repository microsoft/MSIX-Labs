namespace MyEmployees.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int HourlyComp { get; set; }
        public int HrsPerWk { get; set; }
        public int AnnualComp { get; set; }
    }
}
