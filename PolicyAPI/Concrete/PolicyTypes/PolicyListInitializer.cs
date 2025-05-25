using PolicyAPI.Abstract;

namespace PolicyAPI.Concrete.PolicyTypes
{
    public static class PolicyListInitializer
    {
        public static List<IEligibilityPolicy> InitializePolicyList()
        {
            return new List<IEligibilityPolicy>
            {
                //Primary eligibility checks (must be evaluated first)
                new CgpaThresholdPolicy(),
                new DreamCompanyPolicy(),
                new DreamOfferPolicy(),
                //Secondary eligibility checks (evaluated after primary ones)
                new PlacementPercentagePolicy(),
                new MaxCompaniesPolicy(),
                new OfferTierPolicy()
            };
        }
    }
}
