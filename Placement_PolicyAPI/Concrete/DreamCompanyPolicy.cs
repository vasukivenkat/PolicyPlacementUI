using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class DreamCompanyPolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.DreamCompany.Enabled)
                return PolicyEvaluationResultDTO.Success();

            bool isDreamCompany = string.Equals(company.Name, student.DreamCompany, StringComparison.OrdinalIgnoreCase);

            if (isDreamCompany)
            {
                return new PolicyEvaluationResultDTO
                {
                    Passed = true,
                    Blocking = false, // Not blocking others; this overrides them
                    Reason = $"Applying to dream company '{student.DreamCompany}' - overrides other restrictions"
                };
            }

            return PolicyEvaluationResultDTO.Success(); // Not a dream company; nothing to block
        }

    }
}
