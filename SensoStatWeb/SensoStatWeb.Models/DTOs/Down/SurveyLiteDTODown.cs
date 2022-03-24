using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Models.DTOs.Down
{
	public class SurveyLiteDTODown
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }

        public SurveyLiteDTODown()
        {
        }

        public SurveyLiteDTODown(Survey survey)
        {
            Id = survey.Id;
            Name = survey.Name;
            StateId = survey.StateId;
        }
    }
}

