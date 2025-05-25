using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class CgpaThresholdPolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResultDTO Evaluate(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            if (!policies.CgpaThreshold.Enabled)
                return PolicyEvaluationResultDTO.Success();

            if (company.SalaryOffered < policies.CgpaThreshold.HighSalaryThreshold)
                return PolicyEvaluationResultDTO.Success();

            if (student.Cgpa < policies.CgpaThreshold.MinimumCgpa)
            {
                return PolicyEvaluationResultDTO.Failure(
                    $"CGPA {student.Cgpa} is not enough - minimum {policies.CgpaThreshold.MinimumCgpa} is needed for high-paying positions",false
                );
            }

            return PolicyEvaluationResultDTO.Success(
                $"CGPA {student.Cgpa} meets minimum requirement for high-paying position",true
            );
        }
    }
}
