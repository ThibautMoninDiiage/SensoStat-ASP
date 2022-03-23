﻿using System;
using SensoStatWeb.Models.DTOs.Down;
using SensoStatWeb.Models.Entities;
using SensoStatWeb.WebApplication.Wrappers;

namespace SensoStatWeb.WebApplication.ViewModels
{
    public class SurveysViewModel
    {
        // public IEnumerable<Survey>? Surveys { get; set; }

        public IEnumerable<SurveyWithStatsDtoDown> Surveys { get; set; }
    }
}

