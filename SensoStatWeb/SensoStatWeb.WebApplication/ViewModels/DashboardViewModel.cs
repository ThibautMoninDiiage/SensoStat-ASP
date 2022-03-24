using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.WebApplication.ViewModels
{
	public class DashboardViewModel
	{
        public int CountTotalSurveys { get; set; }
        public int CountDeployedSurveys { get; set; }
        public int CountUndeployedSurveys { get; set; }
        public string FrameworkVersion { get; set; }
        public string WebsiteVersion { get; set; }
    }
}

