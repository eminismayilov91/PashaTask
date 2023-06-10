using ApiUI.Model;
using Bussiness.Abstract;
using Entity.DAO;
using Entity.DTO;
using Entity.Filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace ApiUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Gets one Employee by its Id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>A Response Model which contains Employee model and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpGet(Name = "GetEmployeeAsync")]
        public async Task<FilteredResponse<EmployeeDAO>> GetAsync(Guid id)
        {
            var result = await _employeeService.GetAsync(id);
            if (result.Status)
            {
                return FilteredResponse<EmployeeDAO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<EmployeeDAO>.Failed(result);
        }

        /// <summary>
        /// Gets one Employee with details by its Id
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns>A Response Model which contains Employee model and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpGet(Name = "GetEmployeeDetailedInfoByIdAsync")]
        public async Task<FilteredResponse<EmployeeDTO>> GetEmployeeDetailedInfoByIdAsync(Guid id)
        {
            var result = await _employeeService.GetEmployeeDetailedInfoByIdAsync(id);
            if (result.Status)
            {
                return FilteredResponse<EmployeeDTO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<EmployeeDTO>.Failed(result);
        }

        /// <summary>
        /// Gets Employee list by the EmployeeFilter model
        /// </summary>
        /// <param name="EmployeeFilter"></param>
        /// <returns>A Response Model which contains list of Employee and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpPost(Name = "GetAllEmployeesAsync")]
        public async Task<FilteredResponse<List<EmployeeDAO>>> GetAllAsync(EmployeeFilter filter)
        {
            var result = await _employeeService.GetAllAsync(filter);
            if (result.Status)
            {
                return FilteredResponse<List<EmployeeDAO>>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<List<EmployeeDAO>>.Failed(result);
        }

        /// <summary>
        /// Adds Employee with given field
        /// </summary>
        /// <param name="EmployeeDTO"></param>
        /// <returns>A Response Model which contains an Employee and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpPost(Name = "AddEmployeeAsync")]
        public async Task<FilteredResponse<EmployeeDAO>> AddAsync(EmployeeDTO employee)
        {
            var result = await _employeeService.AddAsync(employee);
            if (result.Status)
            {
                return FilteredResponse<EmployeeDAO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<EmployeeDAO>.Failed(result);
        }

        /// <summary>
        /// Deletes Employee by its Id, only Id field is required
        /// </summary>
        /// <param name="EmployeeDTO"></param>
        /// <returns>A Response Model which contains an Employee and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>

        [HttpDelete(Name = "DeleteEmployeeAsync")]
        public async Task<FilteredResponse<EmployeeDAO>> DeleteAsync(EmployeeDTO employee)
        {
            var result = await _employeeService.DeleteAsync(employee);
            if (result.Status) 
            {
                return FilteredResponse<EmployeeDAO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<EmployeeDAO>.Failed(result);
        }

        /// <summary>
        /// Updates Employee with given field
        /// </summary>
        /// <param name="EmployeeDTO"></param>
        /// <returns>A Response Model which contains an Employee and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpPut(Name = "UpdateEmployeeAsync")]
        public async Task<FilteredResponse<EmployeeDAO>> UpdateAsync(EmployeeDTO employee)
        {
            var result = await _employeeService.UpdateAsync(employee);
            if (result.Status)
            {
                return FilteredResponse<EmployeeDAO>.Succeed(result);
            }

            _logger.LogError(result.Message);
            return FilteredResponse<EmployeeDAO>.Failed(result);
        }
    }
}
