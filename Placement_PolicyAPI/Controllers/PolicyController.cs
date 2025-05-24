using Microsoft.AspNetCore.Mvc;
using PolicyAPI.Abstract;
using PolicyAPI.DTOs;

namespace PolicyAPI.Controllers
{
    public class PolicyController : Controller
    {
        private readonly IEligibilityService _eligibilityService;
        private readonly IDataService _dataService;
        public PolicyController(IEligibilityService eligibilityService, IDataService dataService)
        {
            _eligibilityService = eligibilityService;
            _dataService = dataService;
        }

        [HttpPost("CheckEligibility")]
        public ActionResult<List<EligibilityResultDTO>> CheckEligibility()
        {
            try
            {
              
                var results = _eligibilityService.CheckBulkEligibility(
                    _dataService.GetStudentsFromJson(),
                    _dataService.GetSampleCompanies(),
                    _dataService.GetPolicyConfiguration(),
                    20
                );

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

      
    }
}
