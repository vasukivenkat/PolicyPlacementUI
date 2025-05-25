using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete.PolicyTypes
{
    public class DreamCompanyPolicy : IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.DreamCompany.Enabled)
                return PolicyEvaluationResultDTO.Success();

            bool isDreamCompany = string.Equals(company.Name, student.DreamCompany, StringComparison.OrdinalIgnoreCase);

            if (isDreamCompany)
            {
                // Not blocking others; this overrides them
                return PolicyEvaluationResultDTO.Success(
                    $"Applying to dream company '{student.DreamCompany}' - overrides other restrictions", false);
            };

            return PolicyEvaluationResultDTO.Success(); // Not a dream company; nothing to block
        }

    }
}
