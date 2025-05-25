using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class PlacementPercentagePolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResult Evaluate(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            if (!policies.PlacementPercentage.Enabled)
                return PolicyEvaluationResult.Success();

            if (currentPlacementPercentage < policies.PlacementPercentage.TargetPercentage)
            {
                return PolicyEvaluationResult.Failure(
                    $"Current placement percentage {currentPlacementPercentage:F1}% below target {policies.PlacementPercentage.TargetPercentage}%"
                );
            }

            return PolicyEvaluationResult.Success(
                $"Placement percentage {currentPlacementPercentage:F1}% meets target {policies.PlacementPercentage.TargetPercentage}%"
            );
        }
    }
}
