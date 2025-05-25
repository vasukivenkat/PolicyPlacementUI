using PolicyAPI.Abstract;

namespace PolicyAPI.Concrete.PolicyTypes
{
    public static class PolicyListInitializer
    {
        public static List<IEligibilityPolicy> InitializePolicyList()
        {
            return new List<IEligibilityPolicy>
            {
                new CgpaThresholdPolicy(),
                new DreamCompanyPolicy(),
                new DreamOfferPolicy(),
                new PlacementPercentagePolicy(),
                new MaxCompaniesPolicy(),
                new OfferTierPolicy()
            };
        }
    }
}
