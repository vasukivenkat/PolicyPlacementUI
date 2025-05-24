using PolicyAPI.Models;

namespace PolicyAPI.Abstract
{
    public interface IEligibilityService
    {
        EligibilityResult CheckEligibility(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage);
        List<EligibilityResult> CheckBulkEligibility(List<Student> students, List<Company> companies, PolicyConfiguration policies, double currentPlacementPercentage);
        PlacementStats GetPlacementStats(List<Student> students);
    }
}
