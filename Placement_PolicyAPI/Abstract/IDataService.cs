using PolicyAPI.DTOs;

namespace PolicyAPI.Abstract
{
    public interface IDataService
    {
        List<Student> GetStudentsFromJson();
        List<Company> GetSampleCompanies();
        PolicyConfiguration GetPolicyConfiguration();
    }
}
