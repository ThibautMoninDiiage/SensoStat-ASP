﻿using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.ViewModels
{
    public class SurveyViewModel
    {
        public Survey Survey { get; set; }

        public IEnumerable<string[]> PresentationPlan { get; set; }
    }
}
