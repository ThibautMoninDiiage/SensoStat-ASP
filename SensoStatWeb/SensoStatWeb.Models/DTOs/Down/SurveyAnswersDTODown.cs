using SensoStatWeb.Models.DTOs.Up;
using SensoStatWeb.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoStatWeb.Models.DTOs.Down
{
    public class SurveyAnswersDTODown
    {
        public SurveyAnswersDTODown()
        {

        }

        public string Question { get; set; }
        public string UserCode { get; set; }
        public string Answer { get; set; }
    }
}
