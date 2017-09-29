#define Test
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
        private const string AddDebtToPersonCommand = "insert into Debt(PersonId,Ammount,DateStart,DueDate,DebtStatus,PaidAmmount)"
                                                       + " values(@PersonId,@Ammount,@DateStart,@DueDate,@DebtStatus,@PaidAmmount)";

        private static DbLayer _database;
        private SqlConnection _connection;
        private string _connectionString;

        private DbLayer()
        {
#if Test
            _connection = new SqlConnection("Server=tcp:lackopeter.database.windows.net,1433;Initial "
                + "Catalog=SchoolDatabase;Persist Security Info=False;User ID=peterlacko ;Password = Admin1122;"
                + "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
#endif

            //_connection = new SqlConnection(_connectionString);
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
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
            var affectedRows = 0;

            using (_connection)
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
            }

            return affectedRows > 0;
        }

    public bool AddDebtToPerson(Person person, Debt debt)
    {
        var affectedRows = 0;
        using (_connection)
        {
            _connection.Open();
            using (var command = new SqlCommand(AddDebtToPersonCommand, _connection))
            {
                command.Parameters.AddWithValue("@PersonId", person.PersonId);
                command.Parameters.AddWithValue("@Ammount", debt.Amout);
                command.Parameters.AddWithValue("@DateStart", debt.DateStart);
                command.Parameters.AddWithValue("@DueDate", debt.DueDate);
                command.Parameters.AddWithValue("@DebtStatus", debt.DebtStatus);
                command.Parameters.AddWithValue("@PaidAmmount", debt.PaidAmout);

                affectedRows = command.ExecuteNonQuery();
            }
            _connection.Close();
        }

        return affectedRows > 0;
    }
    public IEnumerable<Person> GetPersons()
    {
        var persons = new List<Person>();

        using (_connection)
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

        return persons;
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
}
}
