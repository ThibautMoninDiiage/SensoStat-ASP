﻿using Microsoft.EntityFrameworkCore;
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
            return result.FirstOrDefault();
        }

        public async Task<bool>? DeleteSurvey(int id)
        {
            var deleteSurvey = _context.Surveys.Where(s => s.Id == id).FirstOrDefault();
            _context.Surveys.Remove(deleteSurvey);
            _context.SaveChanges();
            var result = _context.Surveys.Where(s => s.Equals(deleteSurvey));
            return result == null ? true : false;
        }

        public async Task<List<Survey>>? GetAllSurveys()
        {
            return _context.Surveys.ToList();
        }

        public async Task<Survey>? GetSurvey(int id)
        {
            var survey = _context.Surveys
                .Include(s => s.SurveyState)
                .Include(s => s.Instructions)
                .Include(s => s.Questions)
                .Where(s => s.Id == id)
                .FirstOrDefault();
            return survey;
        }

        public async Task<Survey>? GetSurveyByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId.ToString()).Select(u => u.Survey).FirstOrDefault();
        }

        public async Task<Survey>? UpdateSurvey(Survey survey)
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


                    // _context.Surveys.Update(survey);

                    _context.SaveChanges();
                    return surveyDb;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }
    }
}
