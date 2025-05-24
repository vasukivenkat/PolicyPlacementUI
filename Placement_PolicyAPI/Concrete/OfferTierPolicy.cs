using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class OfferTierPolicy : IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.OfferCategory.Enabled)
                return PolicyEvaluationResultDTO.Success();

            string tier = GetOfferTier(student.CurrentSalary, policies.OfferCategory);

            if (tier == "L1")
            {
                return PolicyEvaluationResultDTO.Failure(
                    $"L1 students (salary ≥ ₹{policies.OfferCategory.L1Threshold:N0}) cannot apply to other companies"
                );
            }

            if (tier == "L2")
            {
                decimal requiredSalary = student.CurrentSalary * (decimal)(1 + policies.OfferCategory.RequiredHikePercentageForL2 / 100);

                if (company.SalaryOffered < requiredSalary)
                {
                    return PolicyEvaluationResultDTO.Failure(
                        $"L2 student requires {policies.OfferCategory.RequiredHikePercentageForL2}% hike (₹{requiredSalary:N0}), company offers ₹{company.SalaryOffered:N0}"
                    );
                }

                return PolicyEvaluationResultDTO.Success(
                    $"Company salary ₹{company.SalaryOffered:N0} meets L2 hike requirement"
                );
            }

            return PolicyEvaluationResultDTO.Success("L3 student can apply based on other policies");
        }
        private string GetOfferTier(decimal currentSalary, OfferCategoryPolicyDTO config)
        {
            if (currentSalary >= config.L1Threshold)
                return "L1";
            if (currentSalary >= config.L2Threshold)
                return "L2";
            return "L3";
        }
    }
    
}
