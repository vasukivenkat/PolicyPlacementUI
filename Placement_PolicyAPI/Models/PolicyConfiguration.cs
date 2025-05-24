namespace PolicyAPI.Models
{
    public class PolicyConfiguration
    {
        public MaxCompaniesPolicy MaxCompanies { get; set; } = new();
        public DreamOfferPolicy DreamOffer { get; set; } = new();
        public DreamCompanyPolicy DreamCompany { get; set; } = new();
        public CgpaThresholdPolicy CgpaThreshold { get; set; } = new();
        public PlacementPercentagePolicy PlacementPercentage { get; set; } = new();
        public OfferCategoryPolicy OfferCategory { get; set; } = new();
    }
}
