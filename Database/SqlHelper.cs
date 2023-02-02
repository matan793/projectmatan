using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Database.Models;
using Windows.Media.Core;
using System.Collections.ObjectModel;

namespace DataBase
{
    public static class SqlHelper
    {
        static string dbpath = ApplicationData.Current.LocalFolder.Path;
        //static string dbpath = "C:\\Users\\chokeonmydick\\Desktop\\App1-backup";
        private static string connectionString = "Filename=" + dbpath + "\\MyDatabase.db";
        //private static string connectionString = "Datasource=sql7.freesqldatabase.com:3306;Database=sql7587754;User Id=sql7587754;Password=BFcrvgKbHL;";

        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<User> GetUsers()
        {
            string query = "SELECT Username, Score, HighScore FROM Users";
            ObservableCollection<User> users = new ObservableCollection<User>();
            try
            {
                using (SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    SqliteCommand cmd = new SqliteCommand(query, connection);
                    SqliteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User() { 
                            
                            Username= reader.GetString(0),
                            Score = reader.GetInt32(1),
                            HighScore = reader.GetInt32(2)
                        };
                        users.Add(user);
                        
                    }

                   
                }
                return users;
            }
            catch (Exception)
            {
                return null;

                throw;
            }
        }
        public static User GetUser(string name, string password, string mail)
        {   
            string str = dbpath;
            string query = $"SELECT * FROM Users WHERE Username='{name}' AND Password='{password}' AND Mail='{mail}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        Mail= reader.GetString(3)
                    };
                    return user;
                }
                }
            return null; //המשתמש לא קיים
        }
        public static User GetUser(string tofind, Type t)
        {
            string query = $"SELECT * FROM Users WHERE Username='{tofind}'";
            if (t == Type.Email)
                query = query.Replace("Username", "Mail");
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        Mail = reader.GetString(3)
                    };
                    return user;
                }
            }
            return null; //המשתמש לא קיים
        }
        
        public static User GetUser(string name, string password)
        {
            string str = dbpath;
            string query = $"SELECT * FROM Users WHERE Username='{name}' AND Password='{password}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                {
                    reader.Read();
                    User user = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        Mail = reader.GetString(3),
                        Score= reader.GetInt32(4),
                        HighScore= reader.GetInt32(5)
                    };
                    return user;
                }
            }
            return null; //המשתמש לא קיים
        }
        public static User AddUser(string name, string password, string mail)
        {
            User user = GetUser(name, password, mail); // ניסיון שליפה מהמאגר
            if (user != null) //המשתמש קיים כבר
                return null; //the user exist
            // אם אנחנו ממשיכים זאת אומרת שהמשתמש לא קיים ועלינו להוסיף למאגר
            string query = $"INSERT INTO [Users] (Username,Password, Mail) VALUES ('{name}','{password}', '{mail}')";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                if(e.ToString().Contains("Unique") && e.ToString().Contains("Mail"))
                {
                    throw new Exception("mail is taken");
                }
                
            }
            user = GetUser(name, password, mail); //קבלת המשתמש שהתווסף כרגע

            return user;

        }

        public static void AddScore(int Id, int score, int highscore)
        {
            string query = $"UPDATE Users SET Score = {score}, HighScore = {highscore} WHERE Id = {Id}";
            Execute(query);
        }

        public enum Type
        {
            User,
            Email
        }
    }
}
