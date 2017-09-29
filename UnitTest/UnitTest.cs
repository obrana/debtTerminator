using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        private Person _person;

        [TestInitialize]
        public void BeforeTest()
        {
            _person = new Person();
        }

        [TestMethod]
        public void AddPerson()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AddDebt()
        {
            Assert.Fail();
        }

        
    }
}
