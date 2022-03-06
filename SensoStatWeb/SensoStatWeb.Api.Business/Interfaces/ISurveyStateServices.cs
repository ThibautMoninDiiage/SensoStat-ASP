using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Api.Business.Interfaces
{
    public interface ISurveyStateServices
    {
        Task<SurveyState>? GetSurveyState(int id);
    }
}
