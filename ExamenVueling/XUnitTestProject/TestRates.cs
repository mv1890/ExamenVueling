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
        private async Task<List<RateModel>> GetTestListRates()
        {
            return new List<RateModel>();
        }

        private async Task<List<TransactionModel>> GetTestListTrans()
        {
            return new List<TransactionModel>();
        }

        [Fact]
        public void TestListRates()
        {
            var mock = new Mock<IRepositoryRate>();
            mock.Setup(p => p.Get()).Returns(GetTestListRates());
            Assert.IsType<List<RateModel>>(mock.Object.Get().Result);
        }
        
        [Fact]
        public void TestListTrans()
        {
            var mock = new Mock<IRepositoryTrans>();
            mock.Setup(p => p.Get()).Returns(GetTestListTrans());
            Assert.IsType<List<TransactionModel>>(mock.Object.Get().Result);
        }
        
        [Fact]
        public void TestListRatesNO()
        {
            var mock = new Mock<IRepositoryRate>();
            mock.Setup(p => p.Get()).Returns(GetTestListRates());
            Assert.IsNotType<List<TransactionModel>>(mock.Object.Get().Result);
        }

        [Fact]
        public void TestListTransNO()
        {
            var mock = new Mock<IRepositoryTrans>();
            mock.Setup(p => p.Get()).Returns(GetTestListTrans());
            Assert.IsNotType<List<RateModel>>(mock.Object.Get().Result);
        }
    }
}
