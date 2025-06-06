﻿using PolicyAPI.Abstract;
using PolicyAPI.Concrete.PolicyTypes;
using PolicyAPI.DTOs;

namespace PolicyAPI.Concrete
{
    public class EligibilityService : IEligibilityService
    {
        public EligibilityResultDTO CheckEligibility(StudentDTO student, CompanyDTO company, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            var result = new EligibilityResultDTO
            {
                StudentId = student.Id,
                StudentName = student.Name,
                CompanyId = company.Id,
                CompanyName = company.Name,
                IsEligible = true,
                Reasons = new List<string>()
            };

            // If student is not placed, they can apply (unless other restrictions apply)
            if (!student.IsPlaced)
            {
                result.Reasons.Add("Student is not yet placed - eligible to apply");

                var cgpaPolicyCheck =new CgpaThresholdPolicy();
                var cgpaCheckResult = cgpaPolicyCheck.Evaluate(student, company, policies, currentPlacementPercentage);
                if (!string.IsNullOrEmpty(cgpaCheckResult.Reason))
                    result.Reasons.Add(cgpaCheckResult.Reason);
                result.IsEligible = cgpaCheckResult.Passed;
                result.Decision = result.IsEligible ? "Eligible" : "Not Eligible";
                return result;
            }

            var policylist = PolicyListInitializer.InitializePolicyList();

            for (int i = 0; i < policylist.Count; i++)
            {
                var policy = policylist[i];
                var resultPolicy = policy.Evaluate(student, company, policies, currentPlacementPercentage);
                if (!resultPolicy.Blocking)
                {
                    result.Reasons.Add(resultPolicy.Reason);
                    result.IsEligible = resultPolicy.Passed;
                    result.Decision = result.IsEligible ? "Eligible" : "Not Eligible";
                    return result;
                }
                else
                {
                    if (!string.IsNullOrEmpty(resultPolicy.Reason))
                        result.Reasons.Add(resultPolicy.Reason);
                    result.IsEligible = resultPolicy.Passed;
                    result.Decision = result.IsEligible ? "Eligible" : "Not Eligible";
                    if (i == policylist.Count - 1)
                        return result;
                }
            }
            return result;

        }

        public List<EligibilityResultDTO> CheckBulkEligibility(List<StudentDTO> students, List<CompanyDTO> companies, PolicyConfigurationDTO policies, double currentPlacementPercentage)
        {
            var results = new List<EligibilityResultDTO>();

            foreach (var student in students)
            {
                foreach (var company in companies)
                {
                    results.Add(CheckEligibility(student, company, policies, currentPlacementPercentage));
                }
            }

            return results;
        }

        public PlacementStatusDTO GetPlacementStats(List<StudentDTO> students)
        {
            var totalStudents = students.Count;
            var placedStudents = students.Count(s => s.IsPlaced);
            var placementPercentage = totalStudents > 0 ? (double)placedStudents / totalStudents * 100 : 0;

            return new PlacementStatusDTO
            {
                TotalStudents = totalStudents,
                PlacedStudents = placedStudents,
                PlacementPercentage = Math.Round(placementPercentage, 2)
            };
        }
    }
}
