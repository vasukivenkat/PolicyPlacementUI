namespace PolicyAPI.Models
{
    public class EligibilityRequest
    {
        public List<Student> Students { get; set; } = new();
        public List<Company> Companies { get; set; } = new();
        public PolicyConfiguration Policies { get; set; } = new();
        public double CurrentPlacementPercentage { get; set; }
    }
}
