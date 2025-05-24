using PolicyAPI.Models;

namespace PolicyAPI.Abstract
{
    public interface IDataService
    {
        List<Student> GetStudentsFromJson();
        List<Company> GetSampleCompanies();
        PolicyConfiguration GetPolicyConfiguration();
    }
}
