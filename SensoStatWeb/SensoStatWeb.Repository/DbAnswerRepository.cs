using Microsoft.EntityFrameworkCore;
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
            var answers = _context.Answers.Include(a => a.User).Include(a => a.Question).Where(a => a.User.SurveyId == surveyId).ToList();

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
    }
}
