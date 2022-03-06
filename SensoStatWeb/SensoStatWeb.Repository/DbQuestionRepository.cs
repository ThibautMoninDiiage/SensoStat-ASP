using System;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Repository.Interfaces;

namespace SensoStatWeb.Repository
{
	public class DbQuestionRepository : IQuestionRepository
	{
        private readonly SensoStatDbContext _context;
        public DbQuestionRepository(SensoStatDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>>? GetAllQuestions()
        {
            return _context.Questions.ToList();
        }

        public async Task<Question>? CreateQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();

            var result = _context.Questions.Where(q => q.Equals(question));
            return result.FirstOrDefault();
        }

        public async Task<bool>? DeleteQuestion(int id)
        {
            var deletedQuestion = _context.Questions.Where(q => q.Id == id).FirstOrDefault();

            _context.Questions.Remove(deletedQuestion);
            _context.SaveChanges();

            var result = _context.Questions.Where(q => q.Equals(deletedQuestion));
            return result == null ? true : false;
        }
    }
}

