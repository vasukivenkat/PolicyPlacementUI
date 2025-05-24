namespace PolicyAPI.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Cgpa { get; set; }
        public bool IsPlaced { get; set; }
        public decimal CurrentSalary { get; set; }
        public int CompaniesApplied { get; set; }
        public decimal DreamOffer { get; set; }
        public string DreamCompany { get; set; } = string.Empty;
    }
}
