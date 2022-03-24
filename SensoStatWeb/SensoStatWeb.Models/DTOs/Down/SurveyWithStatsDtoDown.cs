using Newtonsoft.Json;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Models.DTOs.Down
{
    public class SurveyWithStatsDtoDown
    {
        [JsonProperty("survey")]
        public Survey Survey { get; set; }

        [JsonProperty("percentageOfCompletion")]
        public string PercentageOfCompletion { get; set; }
    }
}

