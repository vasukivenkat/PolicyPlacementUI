using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class DreamOfferPolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResult Evaluate(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            if (!policies.DreamOffer.Enabled)
                return PolicyEvaluationResult.Success();

            if (company.SalaryOffered >= student.DreamOffer)
            {
                return PolicyEvaluationResult.Success(
                    $"Company salary ₹{company.SalaryOffered:N0} meets dream offer ₹{student.DreamOffer:N0}"
                );
            }

            return PolicyEvaluationResult.Failure(
                $"Company salary ₹{company.SalaryOffered:N0} below dream offer ₹{student.DreamOffer:N0}"
            );
        }

    }
}
