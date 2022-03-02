using System;
using Xunit;
using Moq;
using SensoStatWeb.Repository.Interfaces;
using SensoStatWeb.Models.Entities;
using System.Collections.Generic;
using SensoStatWeb.Api.Controllers;

namespace SensoStatWeb.xUnit
{
	public class UnitTests
	{
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

        public void ApiTests()
        {
            
        }

    }
}
