using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class MaxCompaniesPolicy:IEligibilityPolicy
    {
        public PolicyEvaluationResult Evaluate(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            if (!policies.MaxCompanies.Enabled)
                return PolicyEvaluationResult.Success();

            if (policies.MaxCompanies.MaxApplications == 0)
                return PolicyEvaluationResult.Failure("Placed students cannot apply to any additional companies");

            if (student.CompaniesApplied >= policies.MaxCompanies.MaxApplications)
                return PolicyEvaluationResult.Failure($"Already applied to {student.CompaniesApplied}/{policies.MaxCompanies.MaxApplications} companies");

            return PolicyEvaluationResult.Success($"Applied to {student.CompaniesApplied}/{policies.MaxCompanies.MaxApplications}");
        }
    }
}
