using Microsoft.AspNetCore.Mvc;
using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

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
        public ActionResult<EligibilityResultDTO> CheckSingleEligibility([FromBody] SingleEligibilityRequestDTO request)
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
        public ActionResult<PlacementStatusDTO> GetPlacementStats([FromBody] List<StudentDTO> students)
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
        public ActionResult<PolicyConfigurationDTO> GetSamplePolicyConfiguration()
        {
            var sampleConfig = new PolicyConfigurationDTO
            {
                MaxCompanies = new MaxCompaniesPolicyDTO { Enabled = true, MaxApplications = 3 },
                DreamOffer = new DreamOfferPolicyDTO { Enabled = true },
                DreamCompany = new DreamCompanyPolicyDTO { Enabled = true },
                CgpaThreshold = new CgpaThresholdPolicyDTO
                {
                    Enabled = true,
                    MinimumCgpa = 7.5,
                    HighSalaryThreshold = 3000000
                },
                PlacementPercentage = new PlacementPercentagePolicyDTO
                {
                    Enabled = false,
                    TargetPercentage = 85.0
                },
                OfferCategory = new OfferCategoryPolicyDTO
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
        public ActionResult<List<CompanyDTO>> GetSampleCompanies()
        {
            var sampleCompanies = new List<CompanyDTO>
            {
                new CompanyDTO { Id = 1, Name = "Google", SalaryOffered = 5500000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 2, Name = "Microsoft", SalaryOffered = 5000000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 3, Name = "Amazon", SalaryOffered = 4500000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 4, Name = "Apple", SalaryOffered = 6000000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 5, Name = "Meta", SalaryOffered = 5200000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 6, Name = "Netflix", SalaryOffered = 4800000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 7, Name = "Tesla", SalaryOffered = 4200000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 8, Name = "Uber", SalaryOffered = 3500000, Category = "Product", IsHighPaying = true },
                new CompanyDTO { Id = 9, Name = "Flipkart", SalaryOffered = 2800000, Category = "Product", IsHighPaying = false },
                new CompanyDTO { Id = 10, Name = "Paytm", SalaryOffered = 2200000, Category = "Fintech", IsHighPaying = false },
                new CompanyDTO { Id = 11, Name = "TCS", SalaryOffered = 800000, Category = "Service", IsHighPaying = false },
                new CompanyDTO { Id = 12, Name = "Infosys", SalaryOffered = 900000, Category = "Service", IsHighPaying = false }
            };

            return Ok(sampleCompanies);
        }

    }
}
