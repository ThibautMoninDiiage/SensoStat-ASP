using System;
using Xunit;
using Moq;
using SensoStatWeb.Api.Business.Interfaces;
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

    }
}
