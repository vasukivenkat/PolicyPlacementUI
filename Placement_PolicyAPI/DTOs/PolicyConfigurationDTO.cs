namespace PolicyAPI.DTOs
{
    public class PolicyConfigurationDTO
    {
        public MaxCompaniesPolicyDTO MaxCompanies { get; set; } = new();
        public DreamOfferPolicyDTO DreamOffer { get; set; } = new();
        public DreamCompanyPolicyDTO DreamCompany { get; set; } = new();
        public CgpaThresholdPolicyDTO CgpaThreshold { get; set; } = new();
        public PlacementPercentagePolicyDTO PlacementPercentage { get; set; } = new();
        public OfferCategoryPolicyDTO OfferCategory { get; set; } = new();
    }
}
