using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class DreamCompanyPolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResult Evaluate(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            if (!policies.DreamCompany.Enabled)
                return PolicyEvaluationResult.Success();

            bool isDreamCompany = string.Equals(company.Name, student.DreamCompany, StringComparison.OrdinalIgnoreCase);

            if (isDreamCompany)
            {
                return new PolicyEvaluationResult
                {
                    Passed = true,
                    Blocking = false, // Not blocking others; this overrides them
                    Reason = $"Applying to dream company '{student.DreamCompany}' - overrides other restrictions"
                };
            }

            return PolicyEvaluationResult.Success(); // Not a dream company; nothing to block
        }

    }
}
