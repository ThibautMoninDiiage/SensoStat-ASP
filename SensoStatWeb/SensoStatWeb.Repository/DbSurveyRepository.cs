using Microsoft.EntityFrameworkCore;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
    public class DbSurveyRepository : ISurveyRepository
    {
        private readonly SensoStatDbContext _context;
        public DbSurveyRepository(SensoStatDbContext context)
        {
            _context = context;
        }

        public async Task<Survey> CreateSurvey(Survey survey)
        {
            try
            {
                _context.Surveys.Add(survey);
                _context.SaveChanges();
                return survey;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> DeleteSurvey(int id)
        {
            try
            {
                // GET THE SURVEY
                var deleteSurvey = _context.Surveys?.Where(s => s.Id == id).FirstOrDefault();


                // DELETE THE SURVEY
                _context.Surveys?.Remove(deleteSurvey);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return true;
            }


        }

        public async Task<bool> DeploySurvey(int surveyId)
        {
            Survey survey = _context.Surveys.Where(s => s.Id == surveyId).FirstOrDefault();
            survey.StateId = 2;
            _context.Surveys.Update(survey);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UndeploySurvey(int surveyId)
        {
            Survey survey = _context.Surveys.Where(s => s.Id == surveyId).FirstOrDefault();
            survey.StateId = 1;
            _context.Surveys.Update(survey);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Survey>> GetAllSurveys()
        {
            return _context.Surveys.ToList();
        }

        //public async Task<Survey> GetSurvey(int id)
        //{
        //    var survey = _context.Surveys
        //        .Where(survey => survey.Id == id)
        //        .Select(s => new Survey
        //        {
        //            Id = s.Id,
        //            Name = s.Name,
        //            CreatorId = s.CreatorId,
        //            Administrator = s.Administrator,
        //            StateId = s.StateId,
        //            CreationDate = s.CreationDate,
        //            UserProducts = s.UserProducts,
        //            Products = s.Products,
        //            SurveyState = s.SurveyState,
        //            Instructions = s.Instructions,
        //            Questions = s.Questions,
        //        }).FirstOrDefault();

        //    return survey;
        //}

        public async Task<Survey> GetSurveyForAdministrator(int id)
        {
            var survey = _context.Surveys.Select(s => new Survey()
            {
                Id = s.Id,
                Name = s.Name,
                StateId = s.StateId,
                Instructions = s.Instructions,
                Questions = s.Questions,
                SurveyState = s.SurveyState
            }).FirstOrDefault(s => s.Id == id);
            return survey;
        }

        public async Task<Survey> GetSurveyByUserId(string userSurveyId)
        {
            var surveyId = int.Parse(userSurveyId.Remove(0, 4));

            var survey = _context.Surveys
                .Where(survey => survey.Id == surveyId)
                .Select(s => new Survey
                {
                    Id = s.Id,
                    Name = s.Name,
                    StateId = s.StateId,
                    UserProducts = s.UserProducts.Where(up => up.UserId == userSurveyId).ToList(),
                    Products = s.Products,
                    Instructions = s.Instructions,
                    Questions = s.Questions,
                }).FirstOrDefault();

            return survey;
        }

        public async Task<Survey> UpdateSurvey(Survey survey)
        {
            try
            {
                var surveyDb = _context.Surveys?.Include(s => s.Questions).Include(s => s.Instructions).FirstOrDefault(s => s.Id == survey.Id);

                // Check if the survey exist in database
                if (surveyDb != null)
                {
                    // If the questions of the survey are modified
                    if (!surveyDb.Questions.SequenceEqual(survey.Questions))
                        surveyDb.Questions = survey.Questions;

                    if (!surveyDb.Instructions.SequenceEqual(survey.Instructions))
                        surveyDb.Instructions = survey.Instructions;


                    _context.Surveys.Update(surveyDb);

                    _context.SaveChanges();
                    return surveyDb;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }


    }
}