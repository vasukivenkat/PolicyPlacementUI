using PolicyAPI.DTOs;

namespace PolicyAPI.Abstract
{
    public interface IEligibilityPolicy
    {
        PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage);
    }
}
