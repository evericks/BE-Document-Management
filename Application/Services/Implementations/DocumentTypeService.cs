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

public class DocumentTypeService : BaseService, IDocumentTypeService
{
    private readonly IDocumentTypeRepository _documentTypeRepository;
    private readonly IAdditionalInformationRepository _additionalInformationRepository;

    public DocumentTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _documentTypeRepository = unitOfWork.DocumentType;
        _additionalInformationRepository = unitOfWork.AdditionalInformation;
    }

    public async Task<IActionResult> GetDocumentTypes()
    {
        var deliveryCompanies = await _unitOfWork.DocumentType.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentTypeViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(deliveryCompanies);
    }
    
    public async Task<IActionResult> GetAdditionalInformationByDocumentTypeId(Guid id)
    {
        var adis = await _additionalInformationRepository.Where(x => x.DocumentTypeId.Equals(id))
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<AdditionalInformationViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(adis);
    }

    public async Task<IActionResult> GetDocumentType(Guid id)
    {
        var documentType = await _unitOfWork.DocumentType.Where(x => x.Id.Equals(id))
            .ProjectTo<DocumentTypeViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(documentType);
    }

    public async Task<IActionResult> CreateDocumentType(DocumentTypeCreateModel model)
    {
        var documentType = _mapper.Map<DocumentType>(model);
        _documentTypeRepository.Add(documentType);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocumentType(documentType.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateDocumentType(Guid id, DocumentTypeUpdateModel model)
    {
        var documentType = await _documentTypeRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (documentType == null)
        {
            return new NotFoundResult();
        }

        _mapper.Map(model, documentType);
        if (model.AdditionalInformations is { Count: > 0 })
        {
            var oldAdis = await _additionalInformationRepository.Where(x => x.DocumentTypeId.Equals(id)).ToListAsync();
            _additionalInformationRepository.DeleteRange(oldAdis);
            
            var adis = model.AdditionalInformations
                .Select(item => new AdditionalInformation()
                {
                    Id = Guid.NewGuid(), 
                    Name = item.Name,
                    DocumentTypeId = id,
                    Description = item.Description
                })
                .ToList();
            _additionalInformationRepository.AddRange(adis);
            await _unitOfWork.SaveChangesAsync();
        }

        _documentTypeRepository.Update(documentType);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocumentType(documentType.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteDocumentType(Guid id)
    {
        var documentType = await _documentTypeRepository
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (documentType == null)
        {
            return new BadRequestResult();
        }

        _documentTypeRepository.Delete(documentType);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new NoContentResult() : new BadRequestResult();
    }
}