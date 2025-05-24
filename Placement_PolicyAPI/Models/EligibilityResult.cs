namespace PolicyAPI.Models
{
    public class EligibilityResult
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool IsEligible { get; set; }
        public List<string> Reasons { get; set; } = new();
        public string Decision { get; set; } = string.Empty;
    }
}
