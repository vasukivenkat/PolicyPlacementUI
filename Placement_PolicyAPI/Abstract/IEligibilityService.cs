using PolicyAPI.DTOs;

namespace PolicyAPI.Abstract
{
    public interface IEligibilityService
    {
        EligibilityResultDTO CheckEligibility(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage);
        List<EligibilityResultDTO> CheckBulkEligibility(List<StudentDTO> students, List<CompanyDTO> companies, PolicyConfigurationDTO policies, double currentPlacementPercentage);
        PlacementStatusDTO GetPlacementStats(List<StudentDTO> students);
    }
}
