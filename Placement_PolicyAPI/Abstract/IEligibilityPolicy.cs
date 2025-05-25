using PolicyAPI.Models;

namespace PolicyAPI.Abstract
{
    public interface IEligibilityPolicy
    {
        PolicyEvaluationResult Evaluate(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage);
    }
}
