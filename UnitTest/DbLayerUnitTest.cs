using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Models;

namespace UnitTest
{
    [TestClass]
    public class DbLayerUnitTest
    {
        [TestMethod]
        public void CreateUser()
        {
            var db = DbLayer.DbLayer.Database;
            var numberofuserbefore = db.GetUsers().Count();
            var user = new User()
            {
                Id = 0,
                Username = "Username",
                Password = "Password"

            };
            db.CreateUser(user);
            Assert.AreEqual(numberofuserbefore + 1, db.GetUsers().Count());

        }

        [TestMethod]
        public void CreateMoreUser()
        {
            var db = DbLayer.DbLayer.Database;


            var user1 = new User()
            {
                Id = 1,
                Username = "Username",
                Password = "Password"
            };
            var user2 = new User()
            {
                Id = 2,
                Username = "Username",
                Password = "Password"
            };

            db.CreateUser(user1);
            var numberOfuserbefore = db.GetUsers().Count();
            db.CreateUser(user2);

            Assert.AreEqual(numberOfuserbefore, db.GetUsers().Count());


        }

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
