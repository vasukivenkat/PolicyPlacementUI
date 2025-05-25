using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete.PolicyTypes
{
    public class DreamOfferPolicy : IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.DreamOffer.Enabled)
                return PolicyEvaluationResultDTO.Success();

            if (company.SalaryOffered >= student.DreamOffer)
            {
                return PolicyEvaluationResultDTO.Success(
                    $"Company salary ₹{company.SalaryOffered:N0} meets dream offer ₹{student.DreamOffer:N0}", false
                );
            }

            return PolicyEvaluationResultDTO.Failure(
                $"Company salary ₹{company.SalaryOffered:N0} below dream offer ₹{student.DreamOffer:N0}", true
            );
        }

    }
}
