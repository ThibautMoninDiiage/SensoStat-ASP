﻿using Microsoft.EntityFrameworkCore;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
    public class DbAnswerRepository : IAnswerRepository
    {
        private readonly SensoStatDbContext _context;

        public DbAnswerRepository(SensoStatDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetSurveyAnswers(int surveyId)
        {
            var answers = _context.Answers.Select(answers => new Answer { UserId = answers.UserId, UserAnswer = answers.UserAnswer, QuestionId = answers.QuestionId, User = answers.User, Question = answers.Question }).Where(a => a.User.SurveyId == surveyId).ToList();

            return answers;
        }

        public async Task<Answer>? CreateAnswer(Answer answer)
        {
            try
            {
                _context.Answers.Add(answer);
                _context.SaveChanges();
                return answer;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<float> GetPercentageAnswerOfSurvey(int surveyId)
        {
            var survey = _context.Surveys?
                .Include(s => s.Questions)
                .Include(s => s.Users)
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == surveyId);

            var numberOfQuestions = survey.Questions?.Count();
            var numberOfProducts = survey.Products?.Count();
            var numberOfUsers = survey.Users?.Count();
            var numberOfAnswers = _context.Answers.Include(a => a.Question).Where(a => a.Question.SurveyId == surveyId).Count();

            var denominator = (numberOfQuestions * numberOfProducts * numberOfAnswers * numberOfUsers);

            float percentageResponses = ((float)numberOfAnswers / (float)denominator);

            return percentageResponses;
        }
    }
}
