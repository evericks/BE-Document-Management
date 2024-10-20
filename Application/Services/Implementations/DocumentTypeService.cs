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

    public DocumentTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _documentTypeRepository = unitOfWork.DocumentType;
    }

    public async Task<IActionResult> GetDocumentTypes()
    {
        var deliveryCompanies = await _unitOfWork.DocumentType.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentTypeViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(deliveryCompanies);
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