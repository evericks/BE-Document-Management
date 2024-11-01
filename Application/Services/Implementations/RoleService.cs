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

public class RoleService : BaseService, IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _roleRepository = unitOfWork.Role;
    }

    public async Task<IActionResult> GetRoles()
    {
        var roles = await _unitOfWork.Role.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(roles);
    }

    public async Task<IActionResult> GetRole(Guid id)
    {
        var role = await _unitOfWork.Role.Where(x => x.Id.Equals(id))
            .ProjectTo<RoleViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(role);
    }

    public async Task<IActionResult> CreateRole(RoleCreateModel model)
    {
        var role = _mapper.Map<Role>(model);
        _roleRepository.Add(role);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetRole(role.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateRole(Guid id, RoleUpdateModel model)
    {
        
        var role = await _roleRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (role == null)
        {
            return new NotFoundResult();
        }
        _mapper.Map(model, role);
        _roleRepository.Update(role);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetRole(role.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteRole(Guid id)
    {
        var role = await _roleRepository
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (role == null)
        {
            return new BadRequestResult();
        }
        _roleRepository.Delete(role);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new NoContentResult() : new BadRequestResult();
    }
}