using Data.Repository.Interfaces;
using Data.Repository.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestProject
{
    public class TestRates
    {
        [Fact]
        public void Test1()
        {
            var mock = new Mock<IRepositoryRate>();
            mock.Setup(p => p.Get()).Returns(GetTestSessions());
            Assert.IsType<List<RateModel>>(mock.Object.Get().Result);
        }

        private async Task<List<RateModel>> GetTestSessions()
        {
            return new List<RateModel>();
        }
    }
}
