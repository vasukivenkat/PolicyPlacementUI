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

        [HttpGet("CheckEligibility")]
        public ActionResult<List<EligibilityResultDTO>> CheckEligibility()
        {
            try
            {
                var results = _eligibilityService.CheckBulkEligibility( _dataService.GetStudents(), 
                    _dataService.GetCompanies(), _dataService.GetPolicyConfiguration(),_dataService.GetPlacementPercent());
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetStudents")]
        public ActionResult<List<StudentDTO>> GetStudents()
        {
            try
            {
                var results = _dataService.GetStudents();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
      
        [HttpGet("GetCompanies")]
        public ActionResult<List<CompanyDTO>> GetCompanies()
        {
            try
            {
                var results = _dataService.GetCompanies();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetPolicyConfiguration")]
        public ActionResult<PolicyConfigurationDTO> GetPolicyConfiguration()
        {
            try
            {
                var results = _dataService.GetPolicyConfiguration();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetPlacementPercentage")]
        public ActionResult<double> GetPlacementPercent()
        {
            try
            {
                var results = _eligibilityService.GetPlacementStats(_dataService.GetStudents());
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
