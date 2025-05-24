using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class MaxCompaniesPolicy:IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.MaxCompanies.Enabled)
                return PolicyEvaluationResultDTO.Success();

            if (policies.MaxCompanies.MaxApplications == 0)
                return PolicyEvaluationResultDTO.Failure("Placed students cannot apply to any additional companies");

            if (student.CompaniesApplied >= policies.MaxCompanies.MaxApplications)
                return PolicyEvaluationResultDTO.Failure($"Already applied to {student.CompaniesApplied}/{policies.MaxCompanies.MaxApplications} companies");

            return PolicyEvaluationResultDTO.Success($"Applied to {student.CompaniesApplied}/{policies.MaxCompanies.MaxApplications}");
        }
    }
}
