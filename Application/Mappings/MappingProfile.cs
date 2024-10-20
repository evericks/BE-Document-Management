using AutoMapper;
using Common.Constants;
using Domain.Entities;
using Domain.Models.Authorization;
using Domain.Models.Creates;
using Domain.Models.Update;
using Domain.Models.Views;

namespace Application.Mappings;

public class MappingProfile: Profile
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
        CreateMap<DocumentCreateModel, Document>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<DocumentUpdateModel, Document>();   
        
        CreateMap<DocumentStatus, DocumentStatusViewModel>();
        CreateMap<DocumentStatusCreateModel, DocumentStatus>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<DocumentStatusUpdateModel, DocumentStatus>(); 
        
        CreateMap<DocumentType, DocumentTypeViewModel>();
        CreateMap<DocumentTypeCreateModel, DocumentType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<DocumentTypeUpdateModel, DocumentType>();
    }
}