using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface IAnswerService
    {
        Task<IEnumerable<SurveyAnswersDTODown>> GetSurveyAnswers(int surveyId);

        Task<Answer> CreateAnswer(AnswerDTOUp answerDTOUp);

        Task<float> GetSurveyPercentageAnswers(int surveyId);
    }
}
