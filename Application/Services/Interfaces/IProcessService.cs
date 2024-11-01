using Domain.Models.Creates;
using Domain.Models.Update;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces;

public interface IProcessService
{
    Task<IActionResult> GetProcesses();
    Task<IActionResult> GetProcess(Guid id);
    Task<IActionResult> GetDocumentTypeProcess(Guid id);
    Task<IActionResult> CreateProcess(ProcessCreateModel model);
    Task<IActionResult> UpdateProcess(Guid id, ProcessUpdateModel model);
    Task<IActionResult> DeleteProcess(Guid id);
    
}