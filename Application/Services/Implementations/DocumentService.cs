﻿using Application.Services.Evercloud.Services.Interfaces;
using Application.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Constants;
using Common.Helpers;
using Data.EntityRepositories.Interfaces;
using Data.UnitOfWorks.Interfaces;
using Domain.Entities;
using Domain.Models.Creates;
using Domain.Models.Update;
using Domain.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace Application.Services.Implementations;

public class DocumentService : BaseService, IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IDocumentStatusRepository _documentStatusRepository;
    private readonly IDocumentTypeRepository _documentTypeRepository;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IDocumentLogRepository _documentLogRepository;
    private readonly IAdditionalInformationDetailRepository _additionalInformationDetailRepository;
    private readonly IEvercloudService _evercloudService;

    public DocumentService(IUnitOfWork unitOfWork, IMapper mapper, IEvercloudService evercloudService) : base(
        unitOfWork, mapper)
    {
        _documentRepository = unitOfWork.Document;
        _documentStatusRepository = unitOfWork.DocumentStatus;
        _documentLogRepository = unitOfWork.DocumentLog;
        _additionalInformationDetailRepository = unitOfWork.AdditionalInformationDetail;
        _documentTypeRepository = unitOfWork.DocumentType;
        _organizationRepository = unitOfWork.Organization;
        _evercloudService = evercloudService;
    }

    public async Task<IActionResult> GetDocuments()
    {
        var documents = await _unitOfWork.Document.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documents);
    }

    public async Task<IActionResult> GetUserReferenceDocuments(Guid userId)
    {
        var documents = await _unitOfWork.Document.Where(x => x.ReceiverId.Equals(userId) || x.SenderId.Equals(userId)
                || x.DocumentLogs.Any(y => y.UserId.Equals(userId)))
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

    public async Task<IActionResult> GetUserReceiveDocuments(Guid id)
    {
        var drafting = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.InDrafting))
            .FirstOrDefaultAsync();
        var pendingApproval = await _documentStatusRepository
            .Where(x => x.Name.Equals(DocumentStatuses.PendingApproval))
            .FirstOrDefaultAsync();
        var documents = await _unitOfWork.Document.Where(x =>
                x.ReceiverId.Equals(id) && !x.StatusId.Equals(drafting!.Id) && x.StatusId.Equals(pendingApproval!.Id))
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documents);
    }

    public async Task<IActionResult> GetUserReturnDocuments(Guid id)
    {
        var returned = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.Returned))
            .FirstOrDefaultAsync();
        var documents = await _unitOfWork.Document.Where(x =>
                x.SenderId.Equals(id) && x.StatusId.Equals(returned!.Id))
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<DocumentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(documents);
    }

    public async Task<IActionResult> GetUserUnClassifiedDocuments(Guid id)
    {
        var received = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.Received))
            .FirstOrDefaultAsync();
        var documents = await _unitOfWork.Document
            .Where(x => x.ReceiverId.Equals(id) && x.DocumentType == null && x.StatusId.Equals(received!.Id))
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

    public async Task<IActionResult> GetUserPendingProcessingDocuments(Guid id)
    {
        var status = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.PendingProcessing))
            .FirstOrDefaultAsync();
        var documents = await _unitOfWork.Document.Where(x => x.ReceiverId.Equals(id) && x.StatusId.Equals(status!.Id))
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
        var agencyCharacter = await _organizationRepository.Where(x => x.Id.Equals(model.OrganizationId))
            .Select(x => x.Character).FirstOrDefaultAsync();
        document.Code = DocumentCodeHelper.GenerateCode("UNCLASSIFY", agencyCharacter!);
        document.CreatedById = senderId;
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

        var documentLog = new DocumentLog()
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            UserId = senderId,
            Action = DocumentLogs.CreateOutgoing
        };
        _documentLogRepository.Add(documentLog);
        _documentRepository.Add(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> CreateIncomingDocument(Guid senderId, DocumentCreateModel model)
    {
        var document = _mapper.Map<Document>(model);
        var agencyCharacter = await _organizationRepository.Where(x => x.Id.Equals(model.OrganizationId))
            .Select(x => x.Character).FirstOrDefaultAsync();
        document.Code = DocumentCodeHelper.GenerateCode("UNCLASSIFY", agencyCharacter!);
        document.SenderId = senderId;
        document.ReceiverId = (Guid)model.ReceiverId!;
        document.CreatedById = senderId;
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

        var documentLog = new DocumentLog()
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            UserId = senderId,
            Action = DocumentLogs.CreateIncoming
        };
        _documentLogRepository.Add(documentLog);
        _documentRepository.Add(document);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> CreateDraftDocument(Guid senderId, DocumentCreateModel model)
    {
        var document = _mapper.Map<Document>(model);
        var agencyCharacter = await _organizationRepository.Where(x => x.Id.Equals(model.OrganizationId))
            .Select(x => x.Character).FirstOrDefaultAsync();
        document.Code = DocumentCodeHelper.GenerateCode("UNCLASSIFY", agencyCharacter!);
        document.SenderId = senderId;
        document.CreatedById = senderId;
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

        var documentLog = new DocumentLog()
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            UserId = senderId,
            Action = DocumentLogs.CreateDraft
        };
        _documentLogRepository.Add(documentLog);

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

    public async Task<IActionResult> ReceiveDocument(Guid id)
    {
        var document = await _documentRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (document == null)
        {
            return new NotFoundResult();
        }

        var receive = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.Received))
            .FirstOrDefaultAsync();

        if (await IsReturnedDocument(document.Id))
        {
            var returnById = document.ReceiverId;
            document.StatusId = receive!.Id;
            document.ReceiverId = document.SenderId;
            document.SenderId = returnById;
            _documentRepository.Update(document);
            var documentLog = new DocumentLog()
            {
                Id = Guid.NewGuid(),
                DocumentId = document.Id,
                UserId = returnById,
                Action = DocumentLogs.Receive
            };
            _documentLogRepository.Add(documentLog);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
        }
        else
        {
            document.StatusId = receive!.Id;
            _documentRepository.Update(document);
            var documentLog = new DocumentLog()
            {
                Id = Guid.NewGuid(),
                DocumentId = document.Id,
                UserId = document.ReceiverId,
                Action = DocumentLogs.Receive
            };
            _documentLogRepository.Add(documentLog);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
        }
    }

    public async Task<IActionResult> ReturnDocument(Guid id, ReturnDocumentUpdateModel model)
    {
        var document = await _documentRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (document == null)
        {
            return new NotFoundResult();
        }

        var returned = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.Returned))
            .FirstOrDefaultAsync();
        var returnById = document.ReceiverId;
        document.StatusId = returned!.Id;
        document.ReceiverId = document.SenderId;
        document.SenderId = returnById;
        _documentRepository.Update(document);
        var documentLog = new DocumentLog()
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            UserId = returnById,
            Action = DocumentLogs.Return,
            Note = model.Message
        };
        _documentLogRepository.Add(documentLog);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> SendDocument(Guid id, Guid senderId, SendDocumentUpdateModel model)
    {
        var document = await _documentRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (document == null)
        {
            return new NotFoundResult();
        }

        var pendingApproval = await _documentStatusRepository
            .Where(x => x.Name.Equals(DocumentStatuses.PendingApproval))
            .FirstOrDefaultAsync();
        document.StatusId = pendingApproval!.Id;
        document.SenderId = senderId;
        document.ReceiverId = model.ReceiverId;
        _documentRepository.Update(document);
        var documentLog = new DocumentLog()
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            UserId = senderId,
            Action = DocumentLogs.Send,
            Note = model.Message
        };
        _documentLogRepository.Add(documentLog);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetDocument(document.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> ClassifyDocument(Guid id, ClassifyCreateModel model)
    {
        var document = await _documentRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (document == null)
        {
            return new NotFoundResult();
        }
        var documentTypeCharacter = await _documentTypeRepository.Where(x => x.Id.Equals(model.DocumentTypeId))
            .Select(x => x.Character).FirstOrDefaultAsync();
        var documentCode = DocumentCodeHelper.UpdateCode(document.Code, documentTypeCharacter!);

        var classified = await _documentStatusRepository.Where(x => x.Name.Equals(DocumentStatuses.PendingProcessing))
            .FirstOrDefaultAsync();
        document.StatusId = classified!.Id;
        document.Code = documentCode;
        document.DocumentTypeId = model.DocumentTypeId;
        document.IsArchived = model.IsArchived;
        document.IsImportant = model.IsImportant;
        document.IsInternal = model.IsInternal;

        var adis = model.AdditionalInformations
            .Select(item => new AdditionalInformationDetail()
            {
                Id = Guid.NewGuid(), 
                DocumentId = id,
                AdditionalInformationId = item.Key, 
                Value = item.Value
            }).ToList();
        
        _additionalInformationDetailRepository.AddRange(adis);
        _documentRepository.Update(document);
        var documentLog = new DocumentLog()
        {
            Id = Guid.NewGuid(),
            DocumentId = document.Id,
            UserId = document.ReceiverId,
            Action = DocumentLogs.Classify,
        };
        _documentLogRepository.Add(documentLog);
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

    private async Task<bool> IsReturnedDocument(Guid id)
    {
        var returned =
            await _documentStatusRepository.FirstOrDefaultAsync(x => x.Name.Equals(DocumentStatuses.Returned));
        return _documentRepository.Any(x => x.Id.Equals(id) && x.StatusId.Equals(returned.Id));
    }
}