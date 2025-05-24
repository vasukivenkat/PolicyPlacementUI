using PolicyAPI.Abstract;
using PolicyAPI.Models;

namespace PolicyAPI.Concrete
{
    public class EligibilityService : IEligibilityService
    {
        public EligibilityResult CheckEligibility(Student student, Company company, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            var result = new EligibilityResult
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

                // Check CGPA threshold for unplaced students applying to high-paying positions
                if (policies.CgpaThreshold.Enabled && company.SalaryOffered >= policies.CgpaThreshold.HighSalaryThreshold)
                {
                    if (student.Cgpa < policies.CgpaThreshold.MinimumCgpa)
                    {
                        result.IsEligible = false;
                        result.Reasons.Add($"CGPA {student.Cgpa} below minimum requirement {policies.CgpaThreshold.MinimumCgpa} for high-paying positions");
                    }
                    else
                    {
                        result.Reasons.Add($"CGPA {student.Cgpa} meets minimum requirement for high-paying position");
                    }
                }

                result.Decision = result.IsEligible ? "Eligible" : "Not Eligible";
                return result;
            }

            // Student is placed - check all applicable policies
            var policyChecks = new List<(bool passed, string reason, bool blocking)>();

            // 1. Check Dream Company Policy (highest priority - can override other restrictions)
            bool isDreamCompany = policies.DreamCompany.Enabled &&
                                 string.Equals(company.Name, student.DreamCompany, StringComparison.OrdinalIgnoreCase);

            if (isDreamCompany)
            {
                policyChecks.Add((true, $"Applying to dream company '{student.DreamCompany}' - overrides other restrictions", false));
                result.IsEligible = true;
                result.Reasons.AddRange(policyChecks.Select(p => p.reason));
                result.Decision = "Eligible";
                return result;
            }

            // 2. Check Maximum Companies Policy
            if (policies.MaxCompanies.Enabled)
            {
                if (policies.MaxCompanies.MaxApplications == 0)
                {
                    policyChecks.Add((false, "Placed students cannot apply to any additional companies (max companies = 0)", true));
                }
                else if (student.CompaniesApplied >= policies.MaxCompanies.MaxApplications)
                {
                    policyChecks.Add((false, $"Student has already applied to {student.CompaniesApplied} companies (max allowed: {policies.MaxCompanies.MaxApplications})", true));
                }
                else
                {
                    policyChecks.Add((true, $"Student has applied to {student.CompaniesApplied}/{policies.MaxCompanies.MaxApplications} companies", false));
                }
            }

            // 3. Check Placement Percentage Policy
            if (policies.PlacementPercentage.Enabled)
            {
                if (currentPlacementPercentage < policies.PlacementPercentage.TargetPercentage)
                {
                    policyChecks.Add((false, $"Current placement percentage {currentPlacementPercentage:F1}% below target {policies.PlacementPercentage.TargetPercentage}%", true));
                }
                else
                {
                    policyChecks.Add((true, $"Placement percentage {currentPlacementPercentage:F1}% meets target {policies.PlacementPercentage.TargetPercentage}%", false));
                }
            }

            // 4. Check Offer Category Policy
            if (policies.OfferCategory.Enabled)
            {
                var currentOfferTier = GetOfferTier(student.CurrentSalary, policies.OfferCategory);

                if (currentOfferTier == "L1")
                {
                    policyChecks.Add((false, $"L1 students (salary ≥ ₹{policies.OfferCategory.L1Threshold:N0}) cannot apply to other companies", true));
                }
                else if (currentOfferTier == "L2")
                {
                    var requiredSalary = student.CurrentSalary * (decimal)(1 + policies.OfferCategory.RequiredHikePercentageForL2 / 100);
                    if (company.SalaryOffered < requiredSalary)
                    {
                        policyChecks.Add((false, $"L2 student requires {policies.OfferCategory.RequiredHikePercentageForL2}% hike (₹{requiredSalary:N0}), company offers ₹{company.SalaryOffered:N0}", true));
                    }
                    else
                    {
                        policyChecks.Add((true, $"Company salary ₹{company.SalaryOffered:N0} meets L2 hike requirement", false));
                    }
                }
                else
                {
                    policyChecks.Add((true, $"L3 student can apply based on other policies", false));
                }
            }

            // 5. Check Dream Offer Policy
            if (policies.DreamOffer.Enabled)
            {
                if (company.SalaryOffered >= student.DreamOffer)
                {
                    policyChecks.Add((true, $"Company salary ₹{company.SalaryOffered:N0} meets dream offer ₹{student.DreamOffer:N0}", false));
                }
                else
                {
                    policyChecks.Add((false, $"Company salary ₹{company.SalaryOffered:N0} below dream offer ₹{student.DreamOffer:N0}", true));
                }
            }

            // 6. Check CGPA Threshold Policy
            if (policies.CgpaThreshold.Enabled && company.SalaryOffered >= policies.CgpaThreshold.HighSalaryThreshold)
            {
                if (student.Cgpa < policies.CgpaThreshold.MinimumCgpa)
                {
                    policyChecks.Add((false, $"CGPA {student.Cgpa} below minimum {policies.CgpaThreshold.MinimumCgpa} for high-paying positions", true));
                }
                else
                {
                    policyChecks.Add((true, $"CGPA {student.Cgpa} meets minimum requirement for high-paying position", false));
                }
            }

            // Determine final eligibility
            var blockingFailures = policyChecks.Where(p => !p.passed && p.blocking).ToList();
            result.IsEligible = !blockingFailures.Any();
            result.Reasons.AddRange(policyChecks.Select(p => p.reason));
            result.Decision = result.IsEligible ? "Eligible" : "Not Eligible";

            return result;
        }

        public List<EligibilityResult> CheckBulkEligibility(List<Student> students, List<Company> companies, PolicyConfiguration policies, double currentPlacementPercentage)
        {
            var results = new List<EligibilityResult>();

            foreach (var student in students)
            {
                foreach (var company in companies)
                {
                    results.Add(CheckEligibility(student, company, policies, currentPlacementPercentage));
                }
            }

            return results;
        }

        public PlacementStats GetPlacementStats(List<Student> students)
        {
            var totalStudents = students.Count;
            var placedStudents = students.Count(s => s.IsPlaced);
            var placementPercentage = totalStudents > 0 ? (double)placedStudents / totalStudents * 100 : 0;

            return new PlacementStats
            {
                TotalStudents = totalStudents,
                PlacedStudents = placedStudents,
                PlacementPercentage = Math.Round(placementPercentage, 2)
            };
        }

        private string GetOfferTier(decimal salary, OfferCategoryPolicy policy)
        {
            if (salary >= policy.L1Threshold) return "L1";
            if (salary >= policy.L2Threshold) return "L2";
            return "L3";
        }
    }
}
