using Microsoft.AspNetCore.Mvc;
using PolicyAPI.Abstract;
using PolicyAPI.Models;

namespace PolicyAPI.Controllers
{
    public class ExtraFeaturesController : Controller
    {

        private readonly IEligibilityService _eligibilityService;
        private readonly IDataService _dataService;
        public ExtraFeaturesController(IEligibilityService eligibilityService, IDataService dataService)
        {
            _eligibilityService = eligibilityService;
            _dataService = dataService;
        }

        [HttpPost("check-single-eligibility")]
        public ActionResult<EligibilityResult> CheckSingleEligibility([FromBody] SingleEligibilityRequest request)
        {
            try
            {
                var student = request.Students.FirstOrDefault(s => s.Id == request.StudentId);
                var company = request.Companies.FirstOrDefault(c => c.Id == request.CompanyId);

                if (student == null)
                {
                    return NotFound($"Student with ID {request.StudentId} not found");
                }

                if (company == null)
                {
                    return NotFound($"Company with ID {request.CompanyId} not found");
                }

                var result = _eligibilityService.CheckEligibility(
                    student,
                    company,
                    request.Policies,
                    request.CurrentPlacementPercentage
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("placement-stats")]
        public ActionResult<PlacementStats> GetPlacementStats([FromBody] List<Student> students)
        {
            try
            {
                var stats = _eligibilityService.GetPlacementStats(students);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("sample-policy-config")]
        public ActionResult<PolicyConfiguration> GetSamplePolicyConfiguration()
        {
            var sampleConfig = new PolicyConfiguration
            {
                MaxCompanies = new MaxCompaniesPolicy { Enabled = true, MaxApplications = 3 },
                DreamOffer = new DreamOfferPolicy { Enabled = true },
                DreamCompany = new DreamCompanyPolicy { Enabled = true },
                CgpaThreshold = new CgpaThresholdPolicy
                {
                    Enabled = true,
                    MinimumCgpa = 7.5,
                    HighSalaryThreshold = 3000000
                },
                PlacementPercentage = new PlacementPercentagePolicy
                {
                    Enabled = false,
                    TargetPercentage = 85.0
                },
                OfferCategory = new OfferCategoryPolicy
                {
                    Enabled = true,
                    L1Threshold = 4000000,
                    L2Threshold = 2000000,
                    RequiredHikePercentageForL2 = 30.0
                }
            };

            return Ok(sampleConfig);
        }

        [HttpGet("sample-companies")]
        public ActionResult<List<Company>> GetSampleCompanies()
        {
            var sampleCompanies = new List<Company>
            {
                new Company { Id = 1, Name = "Google", SalaryOffered = 5500000, Category = "Product", IsHighPaying = true },
                new Company { Id = 2, Name = "Microsoft", SalaryOffered = 5000000, Category = "Product", IsHighPaying = true },
                new Company { Id = 3, Name = "Amazon", SalaryOffered = 4500000, Category = "Product", IsHighPaying = true },
                new Company { Id = 4, Name = "Apple", SalaryOffered = 6000000, Category = "Product", IsHighPaying = true },
                new Company { Id = 5, Name = "Meta", SalaryOffered = 5200000, Category = "Product", IsHighPaying = true },
                new Company { Id = 6, Name = "Netflix", SalaryOffered = 4800000, Category = "Product", IsHighPaying = true },
                new Company { Id = 7, Name = "Tesla", SalaryOffered = 4200000, Category = "Product", IsHighPaying = true },
                new Company { Id = 8, Name = "Uber", SalaryOffered = 3500000, Category = "Product", IsHighPaying = true },
                new Company { Id = 9, Name = "Flipkart", SalaryOffered = 2800000, Category = "Product", IsHighPaying = false },
                new Company { Id = 10, Name = "Paytm", SalaryOffered = 2200000, Category = "Fintech", IsHighPaying = false },
                new Company { Id = 11, Name = "TCS", SalaryOffered = 800000, Category = "Service", IsHighPaying = false },
                new Company { Id = 12, Name = "Infosys", SalaryOffered = 900000, Category = "Service", IsHighPaying = false }
            };

            return Ok(sampleCompanies);
        }

    }
}
