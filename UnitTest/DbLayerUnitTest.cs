using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTest
{
    [TestClass]
    public class DbLayerUnitTest
    {


        [TestMethod]
        public void AddPerson()
        {
            var db = DbLayer.DbLayer.Database;
            var numberOfPersonsBefore = db.GetPersons().Count();

            var person = new Person()
            {
                Address = "Adress",
                CPR = "CPR",
                DOB = DateTime.Now,
                Email = "Email",
                Gender = "Male",
                Name = "Name",
                Phone = "77777777"
            };

            db.AddPerson(person);

            Assert.AreEqual(numberOfPersonsBefore + 1, db.GetPersons().Count());
        }
        [TestMethod]
        public void AddMorePerson()
        {
            var db = DbLayer.DbLayer.Database;
           

            var person1 = new Person()
            {
                Address = "Adress",
                CPR = "CPR",
                DOB = DateTime.Now,
                Email = "Email",
                Gender = "Male",
                Name = "Name",
                Phone = "77777777"
            };
            var person2 = new Person()
            {
                Address = "Adress",
                CPR = "CPR",
                DOB = DateTime.Now,
                Email = "Email",
                Gender = "Male",
                Name = "Name",
                Phone = "77777777"
            };

            db.AddPerson(person1);
            var numberOfPersonsBefore = db.GetPersons().Count();
            db.AddPerson(person2);

            Assert.AreEqual(numberOfPersonsBefore, db.GetPersons().Count());
        }
        [TestMethod]
        public void AddDebt()
        {
            var db = DbLayer.DbLayer.Database;

            var pers = db.GetPersons().First();

            var numberoFDebt = db.GetDebtsOfPerson(pers).Count();
            var debt = new Debt();
            debt.PersonId = pers.PersonId;
            debt.Amout = 1233434;
            debt.DateStart = DateTime.Now;
            debt.DebtStatus = false;
            debt.DueDate = DateTime.Now;
            debt.PaidAmout = 124134;

            db.AddDebtToPerson(pers, debt);

            Assert.AreEqual(numberoFDebt + 1, db.GetDebtsOfPerson(pers).Count());
        }
        [TestMethod]
        public void AddNegativeDebt()
        {
            var db = DbLayer.DbLayer.Database;

            var pers = db.GetPersons().First();

            var numberoFDebt = db.GetDebtsOfPerson(pers).Count();
            var debt = new Debt();
            debt.PersonId = pers.PersonId;
            debt.Amout = -150;
            debt.DateStart = DateTime.Now;
            debt.DebtStatus = false;
            debt.DueDate = DateTime.Now;
            debt.PaidAmout = 124134;

            db.AddDebtToPerson(pers, debt);

            Assert.AreEqual(numberoFDebt , db.GetDebtsOfPerson(pers).Count());
        }

    }
}
