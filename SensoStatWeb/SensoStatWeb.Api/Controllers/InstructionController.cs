using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class InstructionController : Controller
{
    private readonly IInstructionServices _instructionServices;
    public InstructionController(IInstructionServices instructionServices)
    {
        _instructionServices = instructionServices;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Instructions()
    {
        var result =await _instructionServices.GetAllInstructions();
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Instruction([FromBody]Instruction instruction)
    {
        var result =await _instructionServices.CreateInstruction(instruction);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Instruction(int id)
    {
        var result =await _instructionServices.DeleteInstruction(id);
        return result != true ? Ok(result) : NotFound();
    }
}

