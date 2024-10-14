using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Update;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class DepartmentService : BaseService, IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _departmentRepository = unitOfWork.Department;
    }

    public async Task<IActionResult> GetDepartments()
    {
        var deliveryCompanies = await _unitOfWork.Department.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DepartmentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(deliveryCompanies);
    }

    public async Task<IActionResult> GetDepartment(Guid id)
    {
        var department = await _unitOfWork.Department.Where(x => x.Id.Equals(id))
            .ProjectTo<DepartmentViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(department);
    }

    public async Task<IActionResult> CreateDepartment(DepartmentCreateModel model)
    {
        var department = _mapper.Map<Department>(model);
        _departmentRepository.Add(department);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDepartment(department.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateDepartment(Guid id, DepartmentUpdateModel model)
    {
        
        var department = await _departmentRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (department == null)
        {
            return new NotFoundResult();
        }
        _mapper.Map(model, department);
        _departmentRepository.Update(department);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDepartment(department.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteDepartment(Guid id)
    {
        var department = await _departmentRepository
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (department == null)
        {
            return new BadRequestResult();
        }
        _departmentRepository.Delete(department);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new NoContentResult() : new BadRequestResult();
    }
}