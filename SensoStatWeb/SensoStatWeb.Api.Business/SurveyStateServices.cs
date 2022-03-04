using SensoStatWeb.Api.Business.Interfaces;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Api.Business
{
    public class SurveyStateServices : ISurveyStateServices
    {
        private readonly ISurveyStateRepository _surveyStateRepository;

        public SurveyStateServices(ISurveyStateRepository surveyStateRepository)
        {
            _surveyStateRepository = surveyStateRepository;
        }

        public async Task<SurveyState>? GetSurveyState(int id)
        {
            SurveyState result = await _surveyStateRepository.GetSurveyState(id);
            return result;
        }
    }
}
