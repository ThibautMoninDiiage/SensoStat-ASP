using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class InstructionServices : IInstructionServices
    {
        private readonly IInstructionRepository _instructionRepository;

        public InstructionServices(IInstructionRepository instructionRepository)
        {
            _instructionRepository = instructionRepository;
        }

        public async Task<List<Instruction>> GetAllInstructions()
        {
            List<Instruction> result = await _instructionRepository.GetAllInstructions();
            return result;
        }

        public async Task<Instruction>? CreateInstruction(Instruction instruction)
        {
            Instruction result = await _instructionRepository.CreateInstruction(instruction);
            return result;
        }

        public async Task<bool>? DeleteInstruction(int id)
        {
            bool result = await _instructionRepository.DeleteInstruction(id);
            return result;
        }
    }
}
