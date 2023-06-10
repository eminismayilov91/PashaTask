using AutoMapper;
using Bussiness.Abstract;
using Core.Helper;
using DataAccess.Abstract;
using Entity.DAO;
using Entity.DTO;
using Entity.Filter;
using Entity.Helper;
using FluentValidation;
using FluentValidation.Results;

namespace Bussiness.Consrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDAL _employeeDAL;
        private readonly IMapper _mapper;
        private readonly IValidator<EmployeeDTO> _validator;
        public EmployeeManager(IEmployeeDAL employeeDAL, IMapper mapper, IValidator<EmployeeDTO> validator)
        {
            _employeeDAL = employeeDAL;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Response<EmployeeDAO>> AddAsync(EmployeeDTO employee)
        {
            try
            {
                ValidationResult result = _validator.Validate(employee);
                if (result.IsValid)
                {
                    var entity = _mapper.Map<EmployeeDAO>(employee);
                    return await _employeeDAL.AddAsync(entity);
                }
                return Response<EmployeeDAO>.Failed(String.Join(",", result.Errors.Select(w => w.ErrorMessage).ToArray()));
            }
            catch (Exception exception)
            {
                return Response<EmployeeDAO>.Failed("", exception);
            }
        }

        public async Task<Response<EmployeeDAO>> DeleteAsync(EmployeeDTO employee)
        {
            var entity = _mapper.Map<EmployeeDAO>(employee);
            return await _employeeDAL.DeleteAsync(entity);
        }

        public Task<Response<EmployeeDAO>> GetAsync(Guid id)
        {
            return _employeeDAL.GetAsync(w => w.Id == id);
        }

        public Task<Response<EmployeeDTO>> GetEmployeeDetailedInfoByIdAsync(Guid id)
        {
            return _employeeDAL.GetEmployeeDetailedInfoAsync(id);
        }

        public Task<Response<List<EmployeeDAO>>> GetAllAsync(EmployeeFilter filter)
        {
            var filterExprFunc = FilterGenerator.ModelToExpressionFunc<EmployeeFilter, EmployeeDAO>(filter);
            return _employeeDAL.GetAllAsync(filterExprFunc);
        }

        public async Task<Response<EmployeeDAO>> UpdateAsync(EmployeeDTO employee)
        {
            try
            {
                ValidationResult result = _validator.Validate(employee);
                if (result.IsValid)
                {
                    var entity = _mapper.Map<EmployeeDAO>(employee);
                    return await _employeeDAL.UpdateAsync(entity);
                }
                return Response<EmployeeDAO>.Failed(String.Join(",", result.Errors.Select(w => w.ErrorMessage).ToArray()));
            }
            catch (Exception exception)
            {
                return Response<EmployeeDAO>.Failed("", exception);
            }
            
        }
    }
}
