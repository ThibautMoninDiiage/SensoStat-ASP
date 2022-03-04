using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
	public interface ISurveyStateRepository
	{
		Task<SurveyState>? GetSurveyState(int id);
	}
}

