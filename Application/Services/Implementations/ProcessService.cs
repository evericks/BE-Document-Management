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

public class ProcessService : BaseService, IProcessService
{
    private readonly IProcessRepository _processRepository;

    public ProcessService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _processRepository = unitOfWork.Process;
    }

    public async Task<IActionResult> GetProcesses()
    {
        var processes = await _unitOfWork.Process.GetAll()
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<ProcessViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        return new OkObjectResult(processes);
    }

    public async Task<IActionResult> GetProcess(Guid id)
    {
        var process = await _unitOfWork.Process.Where(x => x.Id.Equals(id))
            .ProjectTo<ProcessViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(process);
    }
    
    public async Task<IActionResult> GetDocumentTypeProcess(Guid id)
    {
        var process = await _unitOfWork.Process.Where(x => x.DocumentTypeId.Equals(id))
            .ProjectTo<ProcessViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return new OkObjectResult(process);
    }

    public async Task<IActionResult> CreateProcess(ProcessCreateModel model)
    {
        var process = _mapper.Map<Process>(model);
        _processRepository.Add(process);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetProcess(process.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> UpdateProcess(Guid id, ProcessUpdateModel model)
    {
        
        var process = await _processRepository.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        if (process == null)
        {
            return new NotFoundResult();
        }
        _mapper.Map(model, process);
        _processRepository.Update(process);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? await GetProcess(process.Id) : new BadRequestResult();
    }

    public async Task<IActionResult> DeleteProcess(Guid id)
    {
        var process = await _processRepository
            .Where(b => b.Id.Equals(id))
            .FirstOrDefaultAsync();
        if (process == null)
        {
            return new BadRequestResult();
        }
        _processRepository.Delete(process);
        var result = await _unitOfWork.SaveChangesAsync();
        return result > 0 ? new NoContentResult() : new BadRequestResult();
    }
}