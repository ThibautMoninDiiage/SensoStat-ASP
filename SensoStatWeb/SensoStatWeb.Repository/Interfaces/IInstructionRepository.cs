using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
	public interface IInstructionRepository
	{
        Task<List<Instruction>>? GetAllInstructions();

        Task<Instruction>? CreateInstruction(Instruction instruction);

        Task<bool>? DeleteInstruction(int id);
    }
}

