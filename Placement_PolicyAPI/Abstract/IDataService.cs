using PolicyAPI.DTOs;

namespace PolicyAPI.Abstract
{
    public interface IDataService
    {
        List<StudentDTO> GetStudentsFromJson();
        List<CompanyDTO> GetSampleCompanies();
        PolicyConfigurationDTO GetPolicyConfiguration();
    }
}
