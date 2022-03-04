using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IInstructionServices
    {
        Task<List<Instruction>>? GetAllInstructions();

        Task<Instruction>? CreateInstruction(Instruction instruction);

        Task<bool>? DeleteInstruction(int id);
    }
}
