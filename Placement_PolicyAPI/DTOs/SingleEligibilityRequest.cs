namespace PolicyAPI.DTOs
{
    public class SingleEligibilityRequest
    {
        public int StudentId { get; set; }
        public int CompanyId { get; set; }
        public List<Student> Students { get; set; } = new();
        public List<Company> Companies { get; set; } = new();
        public PolicyConfiguration Policies { get; set; } = new();
        public double CurrentPlacementPercentage { get; set; }
    }
}
