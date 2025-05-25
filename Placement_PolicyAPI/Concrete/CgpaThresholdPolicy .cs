using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class CgpaThresholdPolicy: IEligibilityPolicy
    {
        public PolicyEvaluationResult Evaluate(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            if (!policies.CgpaThreshold.Enabled)
                return PolicyEvaluationResult.Success();

            if (company.SalaryOffered < policies.CgpaThreshold.HighSalaryThreshold)
                return PolicyEvaluationResult.Success();

            if (student.Cgpa < policies.CgpaThreshold.MinimumCgpa)
            {
                return PolicyEvaluationResult.Failure(
                    $"CGPA {student.Cgpa} below minimum {policies.CgpaThreshold.MinimumCgpa} for high-paying positions"
                );
            }

            return PolicyEvaluationResult.Success(
                $"CGPA {student.Cgpa} meets minimum requirement for high-paying position"
            );
        }
    }
}
