namespace PolicyAPI.DTOs
{
    public class EligibilityRequestDTO
    {
        public List<StudentDTO> Students { get; set; } = new();
        public List<CompanyDTO> Companies { get; set; } = new();
        public PolicyConfigurationDTO Policies { get; set; } = new();
        public double CurrentPlacementPercentage { get; set; }
    }
}
