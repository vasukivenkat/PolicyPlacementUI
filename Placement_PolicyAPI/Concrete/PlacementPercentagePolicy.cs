using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class PlacementPercentagePolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.PlacementPercentage.Enabled)
                return PolicyEvaluationResultDTO.Success();

            if (currentPlacementPercentage < policies.PlacementPercentage.TargetPercentage)
            {
                return PolicyEvaluationResultDTO.Failure(
                    $"Current placement percentage {currentPlacementPercentage:F1}% below target {policies.PlacementPercentage.TargetPercentage}%",false
                );
            }

            return PolicyEvaluationResultDTO.Success(
                $"Placement percentage {currentPlacementPercentage:F1}% meets target {policies.PlacementPercentage.TargetPercentage}%"
            );
        }
    }
}
