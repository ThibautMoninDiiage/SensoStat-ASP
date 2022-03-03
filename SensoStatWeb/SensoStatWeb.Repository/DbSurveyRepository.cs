using Microsoft.EntityFrameworkCore;
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

            // INSTRUCTIONS
            var deleteInstructions = _context.Instructions.Where(i => i.SurveyId == deleteSurvey.Id).ToList();
            _context.Instructions.RemoveRange(deleteInstructions);

            // ANSWERS
            var deleteAnswers = _context.Answers.Where(a => a.Question.SurveyId == deleteSurvey.Id).ToList();
            if(deleteAnswers.Count != null)
            {
                _context.Answers.RemoveRange(deleteAnswers);
            }

            // QUESTIONS
            var deleteQuestion = _context.Questions.Where(q => q.SurveyId == deleteSurvey.Id).ToList();
            _context.Questions.RemoveRange(deleteQuestion);

            // USER PRODUCTS
            var deleteUserProducts = _context.UserProducts.Where(u => u.Product.SurveyId == deleteSurvey.Id).ToList();
            _context.UserProducts.RemoveRange(deleteUserProducts);

            // PRODUCTS
            var deleteProducts = _context.Products.Where(p => p.SurveyId == deleteSurvey.Id).ToList();
            _context.Products.RemoveRange(deleteProducts);

            // USERS
            var deleteUsers = _context.Users.Where(u => u.SurveyId == deleteSurvey.Id).ToList();
            _context.Users.RemoveRange(deleteUsers);

            _context.Surveys.Remove(deleteSurvey);
            _context.SaveChanges();

            var result = _context.Surveys.Where(s => s.Equals(deleteSurvey)).SingleOrDefault();
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

        public Survey GetSurveyByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId.ToString()).Select(u => u.Survey).FirstOrDefault();
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