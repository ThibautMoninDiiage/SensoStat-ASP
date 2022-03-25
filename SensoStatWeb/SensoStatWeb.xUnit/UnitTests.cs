using Xunit;
using Moq;
using SensoStatWeb.Api.Business.Interfaces;
using System.Collections.Generic;
using SensoStatWeb.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SensoStatWeb.Models.DTOs.Down;

namespace SensoStatWeb.xUnit
{
	public class UnitTests
	{
        #region UselessTest

        [Fact]
        public void UselessTest()
        {
            #region Arrange

            var quatre = 4;
            var deux = 2;

            #endregion

            #region Act

            var total = deux + deux;

            #endregion

            #region Assert

            Assert.Equal(quatre, total);

            #endregion
        }

        #endregion

        #region AnswerTest

        #region DataSample
        private IEnumerable<SurveyAnswersDTODown> GetAnswers()
        {
            List<SurveyAnswersDTODown> answers = new()
            {
                new SurveyAnswersDTODown
                {
                    Answer = "Non",
                    Question = "Aimez-vous les tests unitaires ?",
                    UserCode = "001"
                },
                new SurveyAnswersDTODown
                {
                    Answer = "Oui",
                    Question = "Aimez-vous Javascript ?",
                    UserCode = "002"
                },
                new SurveyAnswersDTODown
                {
                    Answer = "Non",
                    Question = "Aimez-vous Java ?",
                    UserCode = "003"
                }
            };

            return answers;
        }
        #endregion

        #region Variables
        private readonly Mock<IAnswerService> _answerService = new();
        #endregion

        [Fact]
        public void GetAnswer_ListOfAnswer_AnswerExists()
        {
            #region Arrange
            _answerService.Setup(a => a.GetSurveyAnswers(18)).Returns(Task.FromResult(GetAnswers()));
            AnswerController answerController = new(_answerService.Object);
            #endregion

            #region Act
            var actionResult = answerController.GetAnswers(18);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<SurveyAnswersDTODown>;
            #endregion

            #region Assert
            Assert.IsType<OkObjectResult>(result);
            #endregion
        }

        #endregion

    }
}
