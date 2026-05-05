using MySqlConnector;
using PlayerOne.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerOne.Data
{
    internal class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand(
                "SELECT * FROM users", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User
                {
                    UserId = reader.GetInt32("userid"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password")
                });
            }
            return users;
        }

        public void AddUser(string username, string password)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            var command = new MySqlCommand(
                "INSERT INTO users (username, password) VALUES (@username, @password)", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();
        }

        public User? Login(string username, string password)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            

            var command = new MySqlCommand(
                "SELECT * FROM users WHERE username = @username AND password = @password", connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new User
                {
                    UserId = reader.GetInt32("userid"),
                    Username = reader.GetString("username"),
                    Password = reader.GetString("password")
                };
            }
            return null;
        }
    }
}