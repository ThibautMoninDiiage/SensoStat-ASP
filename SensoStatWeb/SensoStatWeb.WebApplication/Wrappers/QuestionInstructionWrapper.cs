﻿using System;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.Models.Entities.Interfaces;

namespace SensoStatWeb.WebApplication.Wrappers
{
    public class QuestionInstructionWrapper
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Libelle { get; set; }

        public string Title { get; set; }



        public QuestionInstructionWrapper(Instruction instruction)
        {
            Id = instruction.Id;
            Libelle = instruction.Libelle;
            Position = instruction.Position;

            switch (instruction.Status)
            {
                case 0:
                    Title = "Introduction";
                    break;
                case 2:
                    Title = "Conclusion";
                    break;
                default:
                    Title = "Instruction";
                    break;
            }
        }

        public QuestionInstructionWrapper(Question question)
        {
            Id = question.Id;
            Libelle = question.Libelle;
            Position = question.Position;
            Title = "Question";
        }
    }

}

