namespace PolicyAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal SalaryOffered { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsHighPaying { get; set; }
    }
}
