﻿using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface ISurveyRepository
    {
        Task<List<Survey>>? GetAllSurveys();

        Task<Survey>? GetSurvey(int id);

        Task<Survey>? GetSurveyByUserId(int userId);

        Task<Survey>? CreateSurvey(Survey survey);

        Task<Survey>? UpdateSurvey(Survey survey);

        Task<bool>? DeleteSurvey(int id);
    }
}
