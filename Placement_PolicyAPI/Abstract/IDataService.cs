using PolicyAPI.DTOs;

namespace PolicyAPI.Abstract
{
    public interface IDataService
    {
        List<StudentDTO> GetStudents();
        List<CompanyDTO> GetCompanies();
        PolicyConfigurationDTO GetPolicyConfiguration();
        double GePlacementPercent();
    }
}
