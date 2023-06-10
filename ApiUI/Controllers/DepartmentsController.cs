using ApiUI.Model;
using Bussiness.Abstract;
using Entity.DAO;
using Entity.DTO;
using Entity.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiUI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(ILogger<DepartmentsController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        /// <summary>
        /// Gets one Department by its Id
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns>A Response Model which contains Department model and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpGet(Name = "GetDepartmentAsync")]
        public async Task<FilteredResponse<DepartmentDAO>> GetAsync(Guid id)
        {
            var result = await _departmentService.GetAsync(id);
            if (result.Status)
            {
                return FilteredResponse<DepartmentDAO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<DepartmentDAO>.Failed(result);
        }

        /// <summary>
        /// Gets Department list by the DepartmentFilter model
        /// </summary>
        /// <param name="DepartmentFilter"></param>
        /// <returns>A Response Model which contains list of Department and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpPost(Name = "GetAllDepartmentsAsync")]
        public async Task<FilteredResponse<List<DepartmentDAO>>> GetAllAsync(DepartmentFilter filter)
        {
            var result = await _departmentService.GetAllAsync(filter);
            if (result.Status)
            {
                return FilteredResponse<List<DepartmentDAO>>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<List<DepartmentDAO>>.Failed(result);
        }

        /// <summary>
        /// Adds Department with given field
        /// </summary>
        /// <param name="DepartmentDTO"></param>
        /// <returns>A Response Model which contains an Department and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpPost(Name = "AddDepartmentAsync")]
        public async Task<FilteredResponse<DepartmentDAO>> AddAsync(DepartmentDTO department)
        {
            var result = await _departmentService.AddAsync(department);
            if (result.Status)
            {
                return FilteredResponse<DepartmentDAO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<DepartmentDAO>.Failed(result);
        }

        /// <summary>
        /// Deletes Department by its Id, only Id field is required
        /// </summary>
        /// <param name="DepartmentDTO"></param>
        /// <returns>A Response Model which contains an Department and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>

        [HttpDelete(Name = "DeleteDepartmentAsync")]
        public async Task<FilteredResponse<DepartmentDAO>> DeleteAsync(DepartmentDTO department)
        {
            var result = await _departmentService.DeleteAsync(department);
            if (result.Status)
            {
                return FilteredResponse<DepartmentDAO>.Succeed(result);
            }
            _logger.LogError(result.Message);
            return FilteredResponse<DepartmentDAO>.Failed(result);
        }

        /// <summary>
        /// Updates Department with given field
        /// </summary>
        /// <param name="DepartmentDTO"></param>
        /// <returns>A Response Model which contains an Department and some field about request status</returns>
        /// <response code="200">Returns 200 if request is succesfull</response>
        /// <response code="400">Returns 400 something went wrong</response>
        [HttpPut(Name = "UpdateDepartmentAsync")]
        public async Task<FilteredResponse<DepartmentDAO>> UpdateAsync(DepartmentDTO department)
        {
            var result = await _departmentService.UpdateAsync(department);
            if (result.Status)
            {
                return FilteredResponse<DepartmentDAO>.Succeed(result);
            }

            _logger.LogError(result.Message);
            return FilteredResponse<DepartmentDAO>.Failed(result);
        }
    }
}
