using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.Wrappers
{
    public class SurveyWithStatWrapper
    {
        public float PercentageOfCompletion { get; set; }

        public Survey Survey { get; set; }
    }
}

