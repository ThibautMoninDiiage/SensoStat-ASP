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
        public readonly Mock<SensoStatDbContext> _dbContext = new();
        public Mock<IInstructionRepository> instructionMock = new Mock<IInstructionRepository>();

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

        [Fact]
        public void GetInstructions()
        {
            #region Arrange

            InstructionController instructionController = new InstructionController(instructionMock.Object);

            #endregion

            #region Act

            var getAllInstructions = instructionController.Instructions();

            #endregion

            #region Assert

            Assert.IsType<NotFoundObjectResult>(getAllInstructions);

            #endregion
        }

        [Fact]
        public void CreateInstructionSuccess()
        {
            #region Arrange

            InstructionController instructionController = new InstructionController(instructionMock.Object);
            Instruction instruction = new Instruction();
            instruction.Libelle = "Sentez le produit";
            _dbContext.Setup(x => x.Instructions.Add(instruction));

            #endregion

            #region Act

            var createInstruction = instructionController.Instruction(instruction);

            #endregion

            #region Assert

            Assert.IsType<OkObjectResult>(createInstruction);
            _dbContext.Verify(x => x.Add(It.IsAny<Instruction>()), Times.Once);

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

            Assert.IsType<OkObjectResult>(deleteInstruction);

            #endregion
        }

    }
}
