using System;
using Xunit;
using Moq;
using SensoStatWeb.Repository.Interfaces;
using SensoStatWeb.Models.Entities;
using System.Collections.Generic;
using SensoStatWeb.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SensoStatWeb.xUnit
{
	public class UnitTests
	{
        #region Variables

        private readonly Mock<SensoStatDbContext> _dbContext = new();
        public Mock<IInstructionRepository> instructionMock = new Mock<IInstructionRepository>();

        #endregion

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

        #region Instructions

        [Fact]
        public void GetInstructionsFailed()
        {
            #region Arrange

            InstructionController instructionController = new(instructionMock.Object);

            #endregion

            #region Act

            var getAllInstructions = instructionController.Instructions();

            #endregion

            #region Assert

            Assert.IsType<NotFoundResult>(getAllInstructions);
            
            #endregion
        }

        [Fact]
        public void CreateInstructionSuccess()
        {
            #region Arrange

            Instruction instruction = new();
            // instructionMock.Setup(x => x.CreateInstruction(instruction)).Returns(instruction);
            InstructionController instructionController = new(instructionMock.Object);

            #endregion

            #region Act

            var result = ((OkObjectResult)instructionController.Instruction(instruction)).Value;

            #endregion

            #region Assert

            Assert.True(instruction.Equals(result));

            #endregion
        }

        [Fact]
        public void DeleteInstructionSuccess()
        {
            #region Arrange

            InstructionController instructionController = new InstructionController(instructionMock.Object);

            #endregion

            #region Act

            var deleteInstruction = instructionController.Instruction(1);

            #endregion

            #region Assert

            Assert.IsType<OkObjectResult>(deleteInstruction);

            #endregion
        }

        [Fact]
        public void DeleteInstructionFail()
        {
            #region Arrange

            InstructionController instructionController = new InstructionController(instructionMock.Object);

            #endregion

            #region Act

            var deleteInstruction = instructionController.Instruction(39037821);

            #endregion

            #region Assert

            Assert.IsType<BadRequestObjectResult>(deleteInstruction);

            #endregion
        }

        #endregion
    }
}
