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
using System.Collections;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace DataBase
{
    /// <summary>
    /// מחלקה אשר מחילה תכונות המקושרות לבסיס הנתונים
    /// </summary>
    public static class SqlHelper
    {
        
        static string dbpath = ApplicationData.Current.LocalFolder.Path;//מיקום מסד הנתונים
        private static string connectionString = "Filename=" + dbpath + "\\MyDatabase.db";//מחרוזת הקישור עם מסד הנתונים

        /// <summary>
        /// פעולה אשר מבצעת שאילתה אשר היא מקבלת
        /// </summary>
        /// <param name="query">השאילתה</param>
        private static void Execute(string query)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// פעולה אשר חמזירה רשימה של משתמשים המסודרים לפי שיאם הגבוה במשחק
        /// </summary>
        /// <returns></returns>
        public static List<User> GetScores()
        {
            string query = "SELECT Username, Score, HighScore FROM Users ORDER BY HighScore DESC";
            List<User> users = new List<User>();
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
        /// <summary>
        /// פעולה אשר מחזירה את המשתמש אשר קיים במאגר הנתונים, אם המשתמש לא קיים הפעולה תחזיר עצם ריק
        /// </summary>
        /// <param name="username">שם המשתמש</param>
        /// <param name="password">סיסמתו של המשתמש</param>
        /// <param name="mail">מייל המשתמש</param>
        /// <returns></returns>
        public static User GetUser(string username, string password, string mail)
        {
            string str = dbpath;
            string query = $"SELECT * FROM Users WHERE Username='{username}' AND Password='{password}' AND Mail='{mail}'";
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
                        Score = reader.GetInt32(4),
                        HighScore = reader.GetInt32(5),
                        Skin = (Skin)reader.GetInt32(6),
                        ShieldNum = reader.GetInt32(7)
                    };
                    return user;
                }
            }
            return null; //המשתמש לא קיים


        }
        /// <summary>
        /// פעולה אשר מחזירה את הניקוד הכללי של השחקן
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static int GetScore(int Id)
        {
            string str = dbpath;
            string query = $"SELECT ShieldNum FROM Users WHERE Id='{Id}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                    return reader.GetInt32(0);
            }
            return -1;
        }
        /// <summary>
        /// מחזיר את היוזר על פי סוג העמודה(שם משתמש, סיסמה, מייל ועוד). לפי אינם
        /// </summary>
        /// <param name="type"> אינם אשר מייצג את העמודה</param>
        /// <param name="value"> הערך אותו רוצים לחפש</param>
        /// <returns>משתמש על פי הערכים</returns>
        public static User GetUserByRow(UserRowType type, string value)
        {
            string str = dbpath;
            string query = $"SELECT * FROM Users WHERE {type} = '{value}'";
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
                        Score = reader.GetInt32(4),
                        HighScore = reader.GetInt32(5),
                        Skin = (Skin)reader.GetInt32(6),
                        ShieldNum = reader.GetInt32(7)
                    };
                    return user;
                }
            }
            return null; //המשתמש לא קיים
        }
        /// <summary>
        /// פעולה אשר בודקת אם המשתמש קיים במסד הנתונים לפי העמודה
        /// </summary>
        /// <param name="type">העמודה שלפיה רוצים לחפש</param>
        /// <param name="value">הערך אותו רוצים לחפש</param>
        /// <returns>חזיר אמת אם המשתמש קיים שקר אם הוא לא</returns>
        public static bool IsExists(UserRowType type, string value)
        {
            string query = $"SELECT * from Users WHERE {type} = '{value}'";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                    return true;
            }
            return false;
        }
        /// <summary>
        /// הפעולה מחזירה את המשתמש הקיים במבנה הנתונים על פי ערכים שהוכנסו אם המשתמש לא קיים הפעולה תחזיר עצם ריק
        /// </summary>
        /// <param name="username">שם המשתמש</param>
        /// <param name="password">סיסמתו</param>
        public static User GetUser(string username, string password)
        {
            string str = dbpath;
            string query = $"SELECT * FROM Users WHERE Username='{username}' AND Password='{password}'";
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
                        Score = reader.GetInt32(4),
                        HighScore = reader.GetInt32(5),
                        Skin = (Skin)reader.GetInt32(6),
                        ShieldNum = reader.GetInt32(7),
                    };
                    return user;
                }
            }
            return null; //המשתמש לא קיים
        }
        /// <summary>
        /// פעולה אשר מחזירה רשימה של אובייקטים מטיפוס מוצר אשר המשתמש לא רכש
        /// </summary>
        /// <param name="Id">המספר המזהה של המשתמש</param>
        /// <returns></returns>
        public static List<Product> GetShop(int Id)
        {
            List<Product> products = new List<Product>();
            string query = $"SELECT * from Products WHERE ProductId NOT IN (SELECT ProductId  FROM Purchase  WHERE Id = {Id})";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Uid = Id,
                            Skin = (Skin)reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Price = reader.GetInt32(2)
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }
        /// <summary>
        /// פעולה אשר מחזירה רשימה של אובייקטים מטיפוס מוצר אשר המשתמש רכש
        /// </summary>
        /// <param name="Id">המספר המזהה של המשתמש</param>
        /// <returns></returns>
        public static List<Product> GetPerchases(int Id)
        {
            List <Product > products = new List<Product>();
            string query = $"SELECT * from Purchase WHERE Id={Id} ORDER BY ProductId";
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)//האםח יש נתונים
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            Uid = reader.GetInt32(0),
                            Skin = (Skin)reader.GetInt32(1),

                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }
        /// <summary>
        /// פעולה אשר יוצרת משתמש במסד הנתונים
        /// </summary>
        /// <param name="username">שם המשתמש</param>
        /// <param name="password">סיסמא</param>
        /// <param name="mail">המייל האלקטרוני</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static User AddUser(string username, string password, string mail)
        {
            User user = GetUser(username, password, mail); // ניסיון שליפה מהמאגר
            if (user != null) //המשתמש קיים כבר
                return null; //the user exist
            // אם אנחנו ממשיכים זאת אומרת שהמשתמש לא קיים ועלינו להוסיף למאגר
            string query = $"INSERT INTO [Users] (Username,Password, Mail) VALUES ('{username}','{password}', '{mail}')";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 
                
            }
            catch (Exception e)
            {
                throw new Exception(query, e);
                
            }
            user = GetUser(username, password, mail); //קבלת המשתמש שהתווסף כרגע
            return user;

        }
        /// <summary>
        /// פעולה אשר עדכנת את מספר הסקין של המשתמש על פי המספר המזהה שלו
        /// </summary>
        /// <param name="Id">המספר המזהה של המשתמש</param>
        /// <param name="skinId">מספר הסקין</param>
        /// <exception cref="Exception"></exception>
        public static void UpdateSkin(int Id, int skinId)
        {
            string query = $"UPDATE Users SET SkinId = {skinId} WHERE Id ={Id}";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                throw new Exception(query, e);

            }
        }
        /// <summary>
        /// פעולה אשר מעדכנת את  הניקוד הכללי והניקוד הגבוהה של השחקן
        /// </summary>
        /// <param name="Id">המספר המזהה של השחקן</param>
        /// <param name="score">הניקוד הכללי</param>
        /// <param name="highscore">השיא בניקוד של השחקן</param>
        /// <exception cref="Exception"></exception>
        public static void UpdateScore(int Id, int score, int highscore)
        {
            string query = $"UPDATE Users SET Score = {score}, HighScore = {highscore} WHERE Id = {Id}";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                throw new Exception(query, e);

            }
        }
        /// <summary>
        /// פעולה אשר יוצרת רכישה של מוצר
        /// </summary>
        /// <param name="Id">המספר המזהה של השחקו</param>
        /// <param name="productId">המספר המזהה של הסקין</param>
        /// <exception cref="Exception"></exception>
        public static void AddProduct(int Id, int productId)
        {
            string query = $"INSERT INTO [Purchase] (Id, ProductId) VALUES ('{Id}','{productId}')";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                throw new Exception(query, e);

            }
        }
        /// <summary>
        /// פעולה אשר מעדכנת את הסיסמא של המשתמש לפי המספר המזהה
        /// </summary>
        /// <param name="Id">המספר המזהה של המשתמש</param>
        /// <param name="newpass">הסיסמא החדשה</param>
        /// <exception cref="Exception"></exception>
        public static void UpdatePassword(int Id, string newpass)
        {
            string query = $"UPDATE Users SET Password = '{newpass}' WHERE Id = '{Id}'";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                throw new Exception(query, e);

            }

        }
        /// <summary>
        /// פעולה אשר מוסיפה 1 למספר המגנים של המשתמש
        /// </summary>
        /// <param name="id">המספר המזהה של המשתמש</param>
        /// <exception cref="Exception"></exception>
        public static void AddShield(int id)
        {
            string query = $"UPDATE Users SET ShieldNum = ShieldNum+1 WHERE Id = {id}";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                throw new Exception(query, e);

            }
        }
        /// <summary>
        /// פעולה אשר מחסירה 1 ממספר המגנים של המשתמש
        /// </summary>
        /// <param name="id">המספר המזהה של המשתמש</param>
        /// <exception cref="Exception"></exception>
        public static void SubtractShield(int id)
        {
            string query = $"UPDATE Users SET ShieldNum = ShieldNum-1 WHERE Id = {id}";
            try
            {
                Execute(query); //המשתמש החדש מרגע זה מתווסף למאגר המשתמשים הקיימים 

            }
            catch (Exception e)
            {
                throw new Exception(query, e);

            }
        }       
        /// <summary>
        /// אינם המייצג את עמודות הטבלה של המשתמשים
        /// </summary>
        public enum UserRowType
        {
            Id,
            Username,
            Mail
        }
    }
}
