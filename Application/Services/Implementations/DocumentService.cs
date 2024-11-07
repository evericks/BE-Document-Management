using Application.Services.Evercloud.Services.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Update;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Implementations;

public class DocumentService : BaseService, IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentStatusRepository _documentStatusRepository;
    private readonly IEvercloudService _evercloudService;

    public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, IEvercloudService evercloudService) : base(
        unitOfWork, mapper)
    {
        _documentRepository = unitOfWork.Document;
        _documentStatusRepository = unitOfWork.DocumentStatus;
        _evercloudService = evercloudService;
    }

    public async Task<IActionResult> GetDocuments()
    {
        var documents = await _unitOfWork.Document.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documents);
    }

    public async Task<IActionResult> GetUserDocuments(Guid id)
    {
        var status = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.InDrafting))
            .FirstOrDefaultAsync();
        var documents = await _unitOfWork.Document.Where(x => x.ReceiverId.Equals(id) && !x.StatusId.Equals(status!.Id))
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documents);
    }
    
    public async Task<IActionResult> GetUserDraftDocuments(Guid id)
    {
        var status = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.InDrafting))
            .FirstOrDefaultAsync();
        var documents = await _unitOfWork.Document.Where(x => x.SenderId.Equals(id) && x.StatusId.Equals(status!.Id))
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documents);
    }

    public async Task<IActionResult> GetDocument(Guid id)
    {
        var document = await _unitOfWork.Document.Where(x => x.Id.Equals(id))
            .ProjectTo<DocumentDetailViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(document);
    }

    public async Task<IActionResult> CreateDocument(Guid senderId, DocumentCreateModel model)
    {
        var document = _mapper.Map<Document>(model);
        document.SenderId = senderId;
        if (model.Attachments != null)
        {
            foreach (var item in model.Attachments)
            {
                var upload = await _evercloudService.UploadAsync(item, "attachment");
                if (upload?.Url == null) continue;
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    Url = upload.Url,
                    Name = upload.FileName,
                    DocumentId = document.Id,
                    MediaType = upload.FileExtension,
                };
                document.Attachments.Add(attachment);
            }
        }
        _documentRepository.Add(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }
    
    public async Task<IActionResult> CreateOutgoingDocument(Guid senderId, DocumentCreateModel model)
    {
        var document = _mapper.Map<Document>(model);
        document.SenderId = senderId;
        var status = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.PendingApproval))
            .FirstOrDefaultAsync();
        document.StatusId = status!.Id;
        if (model.Attachments != null)
        {
            foreach (var item in model.Attachments)
            {
                var upload = await _evercloudService.UploadAsync(item, "attachment");
                if (upload?.Url == null) continue;
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    Url = upload.Url,
                    Name = upload.FileName,
                    DocumentId = document.Id,
                    MediaType = upload.FileExtension,
                };
                document.Attachments.Add(attachment);
            }
        }
        _documentRepository.Add(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }
    
    public async Task<IActionResult> CreateIncomingDocument(Guid senderId, DocumentCreateModel model)
    {
        var document = _mapper.Map<Document>(model);
        document.SenderId = senderId;
        document.ReceiverId = senderId;
        var status = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.PendingApproval))
            .FirstOrDefaultAsync();
        document.StatusId = status!.Id;
        if (model.Attachments != null)
        {
            foreach (var item in model.Attachments)
            {
                var upload = await _evercloudService.UploadAsync(item, "attachment");
                if (upload?.Url == null) continue;
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    Url = upload.Url,
                    Name = upload.FileName,
                    DocumentId = document.Id,
                    MediaType = upload.FileExtension,
                };
                document.Attachments.Add(attachment);
            }
        }
        _documentRepository.Add(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }
    
    public async Task<IActionResult> CreateDraftDocument(Guid senderId, DocumentCreateModel model)
    {
        var document = _mapper.Map<Document>(model);
        document.SenderId = senderId;
        var status = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.InDrafting))
            .FirstOrDefaultAsync();
        document.StatusId = status!.Id;
        if (model.Attachments != null)
        {
            foreach (var item in model.Attachments)
            {
                var upload = await _evercloudService.UploadAsync(item, "attachment");
                if (upload?.Url == null) continue;
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    Url = upload.Url,
                    Name = upload.FileName,
                    DocumentId = document.Id,
                    MediaType = upload.FileExtension,
                };
                document.Attachments.Add(attachment);
            }
        }
        _documentRepository.Add(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateDocument(Guid id, DocumentUpdateModel model)
    {
        var document = await _documentRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (document == null)
        {
            return new NotFoundResult();
        }

        _mapper.Map(model, document);
        _documentRepository.Update(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteDocument(Guid id)
    {
        var document = await _documentRepository
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (document == null)
        {
            return new BadRequestResult();
        }

        _documentRepository.Delete(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new NoContentResult() : new BadRequestResult();
    }
}