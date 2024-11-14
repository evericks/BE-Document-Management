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

public class OrganizationService : BaseService, IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _organizationRepository = unitOfWork.Organization;
    }

    public async Task<IActionResult> GetOrganizations()
    {
        var deliveryCompanies = await _unitOfWork.Organization.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(deliveryCompanies);
    }

    public async Task<IActionResult> GetOrganization(Guid id)
    {
        var organization = await _unitOfWork.Organization.Where(x => x.Id.Equals(id))
            .ProjectTo<OrganizationViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(organization);
    }

    public async Task<IActionResult> CreateOrganization(OrganizationCreateModel model)
    {
        var organization = _mapper.Map<Organization>(model);
        _organizationRepository.Add(organization);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetOrganization(organization.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateOrganization(Guid id, OrganizationUpdateModel model)
    {
        
        var organization = await _organizationRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (organization == null)
        {
            return new NotFoundResult();
        }
        _mapper.Map(model, organization);
        _organizationRepository.Update(organization);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetOrganization(organization.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteOrganization(Guid id)
    {
        var organization = await _organizationRepository
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (organization == null)
        {
            return new BadRequestResult();
        }
        _organizationRepository.Delete(organization);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new NoContentResult() : new BadRequestResult();
    }
}