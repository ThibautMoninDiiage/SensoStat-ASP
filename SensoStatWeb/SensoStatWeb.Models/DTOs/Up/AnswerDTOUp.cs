using System;
using Newtonsoft.Json;

namespace SensoStatWeb.Models.DTOs.Up
{
	public class AnswerDTOUp
	{
		public string UserAnswer { get; set; }
        public int QuestionId { get; set; }
        public string Token { get; set; }
        public int ProductId { get; set; }
    }
}