using System;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
    public class DbSurveyStateRepository : ISurveyStateRepository
    {
        private readonly SensoStatDbContext _context;
        public DbSurveyStateRepository(SensoStatDbContext context)
        {
            _context = context;
        }

        public SurveyState GetSurveyState(int id)
        {
            return _context.SurveyStates.FirstOrDefault(u => u.Id == id);
        }
    }
}

