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

public class DocumentStatusService : BaseService, IDocumentStatusService
{
    private readonly IDocumentStatusRepository _documentTypeRepository;

    public DocumentStatusService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _documentTypeRepository = unitOfWork.DocumentStatus;
    }

    public async Task<IActionResult> GetDocumentStatuses()
    {
        var documentStatuses = await _unitOfWork.DocumentStatus.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentStatusViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documentStatuses);
    }

    public async Task<IActionResult> GetDocumentStatus(Guid id)
    {
        var documentType = await _unitOfWork.DocumentStatus.Where(x => x.Id.Equals(id))
            .ProjectTo<DocumentStatusViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(documentType);
    }

    public async Task<IActionResult> CreateDocumentStatus(DocumentStatusCreateModel model)
    {
        var documentType = _mapper.Map<DocumentStatus>(model);
        _documentTypeRepository.Add(documentType);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocumentStatus(documentType.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateDocumentStatus(Guid id, DocumentStatusUpdateModel model)
    {
        
        var documentType = await _documentTypeRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (documentType == null)
        {
            return new NotFoundResult();
        }
        _mapper.Map(model, documentType);
        _documentTypeRepository.Update(documentType);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocumentStatus(documentType.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteDocumentStatus(Guid id)
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