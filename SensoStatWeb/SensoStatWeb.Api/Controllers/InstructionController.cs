using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensoStatWeb.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class InstructionController : Controller
{
    private readonly IInstructionRepository _instructionRepository;
    public InstructionController(IInstructionRepository instructionRepository)
    {
        _instructionRepository = instructionRepository;
    }

    [HttpGet]
    public IActionResult Instructions()
    {
        var result = _instructionRepository.GetAllInstructions();

        if (result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpPost]
    public IActionResult Instruction([FromBody]Instruction instruction)
    {
        var result = _instructionRepository.CreateInstruction(instruction);

        if (result == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(result);
        }
    }

    [HttpDelete]
    public IActionResult Instruction(int id)
    {
        var result = _instructionRepository.DeleteInstruction(id);

        if (result.Result != true)
        {
            return Ok(result.Result);
        }
        else
        {
            return NotFound();
        }
    }
}

