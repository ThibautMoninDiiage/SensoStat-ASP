using SensoStatWeb.Models.DTOs.Down;
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
    }
}
