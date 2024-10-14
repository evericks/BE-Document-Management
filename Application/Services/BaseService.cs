using AutoMapper;
using Data.UnitOfWorks.Interfaces;

namespace Application.Services;

public class BaseService
{
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;

    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
}