using System;
using Microsoft.AspNetCore.Mvc;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.ViewModels;
using System.Linq;

namespace SensoStatWeb.WebApplication.Controllers
{
    public class SurveyController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Detail(string surveyName)
        {
            var model = new SurveyViewModel
            {
                Survey = new Survey { Name = surveyName}
            };

            return this.View("Detail", model);
        }

        [HttpPost]
        public IActionResult CreateSurvey(List<string>? inputQuestionInstruction, string? orderInputs)
        {
            var listPosition = orderInputs?.Substring(1)?.Split(" ").ToList();

            var questions = new List<Question>();
            var instructions = new List<Instruction>();

            for (int i = 1; i <= inputQuestionInstruction?.Count(); i++)
            {
                if (listPosition?[i - 1] == "question")
                {
                    var question = new Question()
                    {
                        Libelle = inputQuestionInstruction[i - 1],
                        Position = i
                    };

                    questions.Add(question);
                }
                else
                {
                    var instruction = new Instruction()
                    {
                        Libelle = inputQuestionInstruction[i - 1],
                        Position = i
                    };
                    instructions.Add(instruction);
                }
            }

            return RedirectToAction("index", "surveys");
        }
    }
}

