using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Threading;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Models;
using System.IO;

namespace TheRockPaperScissors.Server.Data
{
    public class Database
    {
        private static readonly string _dbName = "Client.db";
        private readonly string _connectionString = "Data Source = " + _dbName;

        public Database()
        {
            if (!File.Exists(_dbName))
            {
                Console.WriteLine("SOZDANIE BD");
                SQLiteConnection.CreateFile(_dbName);
                CreateTable();
            }
        }

        public void AddUser(User user)
        {
            using var connection = new SQLiteConnection(_connectionString);
            var commandString = "insert into [users]([login], [password]) " +
                "values(@login, @password)";
            using var command = new SQLiteCommand(commandString, connection);
            command.Parameters.AddWithValue("@login", user.Login);
            command.Parameters.AddWithValue("@password", user.Password);
            connection.Open();
            command.ExecuteNonQueryAsync();
            connection.Close();
        }

        public User GetUser(string login)
        {
            try
            {
                var table = new DataTable();
                var adapter = new SQLiteDataAdapter($@"select * from [users] where login='{login}'", _connectionString);
                adapter.Fill(table);
                var row = table.AsEnumerable().FirstOrDefault() ?? throw new NullReferenceException();
                var user = new User()
                {
                    Login = row.Field<string>("login"),
                    Password = row.Field<string>("password")
                };
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<User> GetAllUsers()
        {
            IList<User> users = new List<User>();
            var table = new DataTable();
            var adapter = new SQLiteDataAdapter("select * from [users]", _connectionString);
            adapter.Fill(table);
            table.AsEnumerable().ToList().ForEach(row =>
                users.Add(new User()
                {
                    Login = row.Field<string>("login"),
                    Password = row.Field<string>("password")
                }));
            return users;
        }

        public void UpdateUser(User user)
        {

        }

        private void CreateTable()
        {
            using var connection = new SQLiteConnection(_connectionString);

            using var command = new SQLiteCommand(connection)
            {
                CommandText = @"create table [users](
                [id] integer primary key autoincrement not null,
                [login] char(100) not null,
                [password] char(100) not null);",
                CommandType = CommandType.Text
            };
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
