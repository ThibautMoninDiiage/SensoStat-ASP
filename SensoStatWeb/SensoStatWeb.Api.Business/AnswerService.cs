using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.DTOs.Down;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoStatWeb.Api.Business
{
    public class AnswerService : IAnswerService
    {

        public AnswerService()
        {

        }

        public async Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId)
        {
            throw new NotImplementedException();
        }
    }
}
