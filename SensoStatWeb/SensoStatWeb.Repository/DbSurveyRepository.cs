using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoStatWeb.Repository
{
    public class DbSurveyRepository : ISurveyRepository
    {
        private readonly SensoStatDbContext _context;
        public DbSurveyRepository(SensoStatDbContext context)
        {
            _context = context;
        }

        public async Task<Survey>? CreateSurvey(Survey survey)
        {
            _context.Surveys.Add(survey);
            _context.SaveChanges();
            var result = _context.Surveys.Where(s => s.Equals(survey));
            if(result == null)
            {
                return null;
            }
            else
            {
                return result.FirstOrDefault();
            }
        }

        public async Task<bool>? DeleteSurvey(int id)
        {
            var deleteSurvey = _context.Surveys.Where(s => s.Id == id).FirstOrDefault();
            _context.Surveys.Remove(deleteSurvey);
            _context.SaveChanges();
            var result = _context.Surveys.Where(s => s.Equals(deleteSurvey));
            if (result == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Survey> GetAllSurveys()
        {
            return _context.Surveys.ToList();
        }

        public Survey GetSurvey(int id)
        {
            return _context.Surveys.Where(s => s.Id == id).FirstOrDefault();
        }

        public async Task<Survey> UpdateSurvey(Survey survey)
        {
            _context.Surveys.Update(survey);
            var result = _context.Surveys.Where(s => s.Equals(survey));
            if (result == null)
            {
                return null;
            }
            else
            {
                _context.SaveChanges();
                return result.FirstOrDefault();
            }
        }
    }
}
