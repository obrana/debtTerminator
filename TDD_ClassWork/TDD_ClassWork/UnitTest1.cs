using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDDClassWork;

namespace TDD_ClassWork
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
       
            public void ShouldFindOneYInMysterious()
            {
                var stringToCheck = "mysterious";
                var stringToFind = "y";
                var expectedResult = 1;
                var classUnderTest = new StringUtilities();
                var actualResult = classUnderTest.CountOccurences(stringToCheck, stringToFind);
                Assert.AreEqual(expectedResult, actualResult);
            }

        [TestMethod]
        public void ShouldFindTwoSInMysterious()
        {
            var stringToCheck = "mysterious";
            var stringToFind = "s";
            var expectedResult = 2;
            var finds = new StringUtilities();
            var actualResult = finds.CountOccurences(stringToCheck, stringToFind);
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}

