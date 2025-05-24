namespace PolicyAPI.DTOs
{
    public class SingleEligibilityRequestDTO
    {
        public int StudentId { get; set; }
        public int CompanyId { get; set; }
        public List<StudentDTO> Students { get; set; } = new();
        public List<CompanyDTO> Companies { get; set; } = new();
        public PolicyConfigurationDTO Policies { get; set; } = new();
        public double CurrentPlacementPercentage { get; set; }
    }
}
