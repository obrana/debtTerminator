using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DbLayer
{
    public class DbLayer
    {
        private const string AddPersonCommand = "insert into Person(Name,CPR,Adress,DOB,Gender,Email,Phone)"
                                + " values(@Name,@CPR,@Adress,@DOB,@Gender,@Email,@Phone)";
        private const string GetPersonsCommand = "select PersonId,Name,CPR,Adress,DOB,Gender,Email,Phone"
                                                + " from Person";
        private const string AddDebtToPersonCommand = "insert into Debt(PersonId,Amount,DateStart,DueDate,DebtStatus,PaidAmount)"
                                                       + " values(@PersonId,@Amount,@DateStart,@DueDate,@DebtStatus,@PaidAmount)";

        private const string GetDebtsOfPersonCommand = "select PersonId, Amount, DateStart, DebtId, DebtStatus, DueDate, PaidAmount "
                                                        + " from Debt where PersonId=@PersonId";

        private const string PersonExistCommand = "select * from Person where CPR=@CPR";

        private static DbLayer _database;
        private SqlConnection _connection;
        private string _connectionString;

        private DbLayer()
        {
            _connectionString = "Server=tcp:lackopeter.database.windows.net,1433;Initial "
                + "Catalog=SchoolDatabase;Persist Security Info=False;User ID=peterlacko ;Password = Admin1122;"
                + "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            _connection = new SqlConnection(_connectionString);
        }

        public static DbLayer Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new DbLayer();
                }
                return _database;
            }
        }

        public bool AddPerson(Person person)
        {
            if (PersonExist(person))
            {
                return false;
            }
            var affectedRows = 0;

            try
            {
                _connection.Open();
                using (var command = new SqlCommand(AddPersonCommand, _connection))
                {
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.Parameters.AddWithValue("@CPR", person.CPR);
                    command.Parameters.AddWithValue("@Adress", person.Address);
                    command.Parameters.AddWithValue("@DOB", person.DOB);
                    command.Parameters.AddWithValue("@Gender", person.Gender);
                    command.Parameters.AddWithValue("@Email", person.Email);
                    command.Parameters.AddWithValue("@Phone", person.Phone);

                    affectedRows = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            catch (Exception ex)
            {
                
            }

            return affectedRows > 0;
        }
        private bool PersonExist(Person person)
        {
            var exist = false;

            try
            {
                _connection.Open();
                using (var command = new SqlCommand(PersonExistCommand, _connection))
                {
                    command.Parameters.AddWithValue("@CPR", person.CPR);

                    var reader = command.ExecuteReader();
                    exist= reader.HasRows;
                }
                _connection.Close();
            }
            catch (Exception ex)
            {

            }

            return exist;
        }
        public bool AddDebtToPerson(Person person, Debt debt)
        {
            if (debt.Amout < 0)
            {
                return false;
            }
            var affectedRows = 0;
            try
            {
                _connection.Open();
                using (var command = new SqlCommand(AddDebtToPersonCommand, _connection))
                {
                    command.Parameters.AddWithValue("@PersonId", person.PersonId);
                    command.Parameters.AddWithValue("@Amount", debt.Amout);
                    command.Parameters.AddWithValue("@DateStart", debt.DateStart);
                    command.Parameters.AddWithValue("@DueDate", debt.DueDate);
                    command.Parameters.AddWithValue("@DebtStatus", debt.DebtStatus);
                    command.Parameters.AddWithValue("@PaidAmount", debt.PaidAmout);

                    affectedRows = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            catch (Exception ex)
            {

            }

            return affectedRows > 0;
        }
        public IEnumerable<Person> GetPersons()
        {
            var persons = new List<Person>();

            try
            {
                _connection.Open();
                using (var command = new SqlCommand(GetPersonsCommand, _connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        persons.Add(CreatePerson(reader));
                    }
                }
                _connection.Close();
            }
            catch (Exception ex)
            {

            }

            return persons;
        }
        public IEnumerable<Debt> GetDebtsOfPerson(Person person)
        {
            var debts = new List<Debt>();

            try
            {
                _connection.Open();
                using (var command = new SqlCommand(GetDebtsOfPersonCommand, _connection))
                {
                    command.Parameters.AddWithValue("@PersonId", person.PersonId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        debts.Add(CreateDebt(reader));
                    }
                }
                _connection.Close();
            }
            catch (Exception ex)
            {

            }

            return debts;
        }

        private Debt CreateDebt(SqlDataReader reader)
        {
            return new Debt()
            {
                PersonId = reader.GetInt32(0),
                Amout = reader.GetDecimal(1),
                DateStart = reader.GetDateTime(2),
                DebtId = reader.GetInt32(3),
                DebtStatus = reader.GetBoolean(4),
                DueDate = reader.GetDateTime(5),
                PaidAmout = reader.GetDecimal(6)              
            };
        }

        private Person CreatePerson(SqlDataReader reader)
        {
            return new Person()
            {
                PersonId = reader.GetInt32(0),
                Name = reader.GetString(1),
                CPR = reader.GetString(2),
                Address = reader.GetString(3),
                DOB = reader.GetDateTime(4),
                Gender = reader.GetString(5),
                Email = reader.GetString(6),
                Phone = reader.GetString(7)
            };
        }

        public  bool CreateUser(User user)
        {
            return false;

        }


        public IEnumerable<User> GetUsers()
        {
            var persons = new List<Person>();

            try
            {
                _connection.Open();
                using (var command = new SqlCommand(GetPersonsCommand, _connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        persons.Add(CreatePerson(reader));
                    }
                }
                _connection.Close();
            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
