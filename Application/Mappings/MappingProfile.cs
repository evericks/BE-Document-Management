using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.Models.Authorization;
using Domain.Models.Creates;
using Domain.Models.Update;
using Domain.Models.Views;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserInformationModel>();
        CreateMap<User, UserViewModel>();
        CreateMap<UserCreateModel, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => UserStatuses.Active))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(_ => DefaultConst.UserPassword));
        CreateMap<UserUpdateModel, User>();

        CreateMap<Role, RoleViewModel>();
        CreateMap<RoleCreateModel, Role>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<RoleUpdateModel, Role>();

        CreateMap<Department, DepartmentViewModel>();
        CreateMap<DepartmentCreateModel, Department>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<DepartmentUpdateModel, Department>();

        CreateMap<Document, DocumentViewModel>();
        CreateMap<Document, DocumentDetailViewModel>()
            .ForMember(dest => dest.DocumentLogs, opt => opt.MapFrom(x => x.DocumentLogs.OrderBy(y => y.CreatedAt)));
        CreateMap<DocumentCreateModel, Document>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.Attachments, opt => opt.Ignore());
        CreateMap<DocumentUpdateModel, Document>();

        CreateMap<DocumentStatus, DocumentStatusViewModel>();
        CreateMap<DocumentStatusCreateModel, DocumentStatus>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<DocumentStatusUpdateModel, DocumentStatus>();

        CreateMap<DocumentType, DocumentTypeViewModel>()
            .ForMember(dest => dest.AdditionalInformations, opt => opt.MapFrom(src => src.AdditionalInformations.OrderBy(y => y.Name)));
        CreateMap<DocumentType, DocumentTypeDetailViewModel>();
        CreateMap<DocumentTypeCreateModel, DocumentType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<DocumentTypeUpdateModel, DocumentType>()
            .ForMember(dest => dest.AdditionalInformations, opt => opt.Ignore());

        CreateMap<Process, ProcessViewModel>()
            .ForMember(dest => dest.ProcessSteps,
                opt => opt.MapFrom(src => src.ProcessSteps.OrderBy(x => x.StepNumber)));
        CreateMap<ProcessCreateModel, Process>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<ProcessUpdateModel, Process>();

        CreateMap<ProcessStep, ProcessStepViewModel>();
        CreateMap<ProcessStepCreateModel, ProcessStep>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<ProcessStepUpdateModel, ProcessStep>();

        CreateMap<Organization, OrganizationViewModel>();
        CreateMap<OrganizationCreateModel, Organization>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<OrganizationUpdateModel, Organization>();

        CreateMap<DocumentLog, DocumentLogViewModel>();

        CreateMap<Attachment, AttachmentViewModel>();

        CreateMap<DocumentProcess, DocumentProcessDetailViewModel>();

        CreateMap<AdditionalInformation, AdditionalInformationViewModel>();

        CreateMap<AdditionalInformationDetail, AdditionalInformationDetailViewModel>();
    }
}