using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Models.Entities.Interfaces;

namespace SensoStatWeb.WebApplication.ViewModels
{
    public class SurveyViewModel
    {
        public SurveyCreationDTODown SurveyCreationDTODown { get; set; }
        public Survey Survey { get; set; }
        public IEnumerable<IQuestionInstruction> QuestionsInstructions { get; set; }
    }
}

