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
                    $"CGPA {student.Cgpa} below minimum {policies.CgpaThreshold.MinimumCgpa} for high-paying positions"
                );
            }

            return PolicyEvaluationResultDTO.Success(
                $"CGPA {student.Cgpa} meets minimum requirement for high-paying position"
            );
        }
    }
}
