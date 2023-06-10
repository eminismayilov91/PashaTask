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
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDAL _departmentDAL;
        private readonly IMapper _mapper;
        private readonly IValidator<DepartmentDTO> _validator;
        public DepartmentManager(IDepartmentDAL departmentDAL, IMapper mapper, IValidator<DepartmentDTO> validator)
        {
            _departmentDAL = departmentDAL;
            _mapper = mapper;
            _validator = validator;
        }
        
        public async Task<Response<DepartmentDAO>> AddAsync(DepartmentDTO department)
        {
            try
            {
                ValidationResult result = _validator.Validate(department);
                if (result.IsValid)
                {
                    var entity = _mapper.Map<DepartmentDAO>(department);
                    return await _departmentDAL.AddAsync(entity);
                }
                return Response<DepartmentDAO>.Failed(String.Join(",", result.Errors.Select(w => w.ErrorMessage).ToArray()));
            }
            catch (Exception exception)
            {
                return Response<DepartmentDAO>.Failed("", exception);
            }
        }

        public async Task<Response<DepartmentDAO>> DeleteAsync(DepartmentDTO department)
        {
            var entity = _mapper.Map<DepartmentDAO>(department);
            return await _departmentDAL.DeleteAsync(entity);
        }

        public Task<Response<DepartmentDAO>> GetAsync(Guid id)
        {
            return _departmentDAL.GetAsync(w => w.Id == id);
        }

        public Task<Response<List<DepartmentDAO>>> GetAllAsync(DepartmentFilter filter)
        {
            var filterExprFunc = FilterGenerator.ModelToExpressionFunc<DepartmentFilter, DepartmentDAO>(filter);
            return _departmentDAL.GetAllAsync(filterExprFunc);
        }

        public async Task<Response<DepartmentDAO>> UpdateAsync(DepartmentDTO department)
        {
            try
            {
                ValidationResult result = _validator.Validate(department);
                if (result.IsValid)
                {
                    var entity = _mapper.Map<DepartmentDAO>(department);
                    return await _departmentDAL.UpdateAsync(entity);
                }
                return Response<DepartmentDAO>.Failed(String.Join(",", result.Errors.Select(w => w.ErrorMessage).ToArray()));
            }
            catch (Exception exception)
            {
                return Response<DepartmentDAO>.Failed("", exception);
            }
        }
    }
}
