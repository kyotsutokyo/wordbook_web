using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Globalization;

namespace Wordbook
{
    public class SQLiteHelper
    {
        private readonly string _dbConnection;

        private DateTime baseTime = new DateTime(2010, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private string _fileName = "otp.db";

        /// <summary>
        /// デフォルトテーブル作成文
        /// </summary>
        private string createOtpTableQuery = @"CREATE TABLE IF NOT EXISTS [wordbook] (
                          [id]     INTEGER PRIMARY KEY,
                          [userid] INTEGER,
                          [item]   NVARCHAR(200) ,
                          [trans]  NVARCHAR(2000) ,
                          [tags]   NVARCHAR(200) ,
                          [time]   INTEGER
                          )";
        private string createUserTableQuery = @"CREATE TABLE IF NOT EXISTS [user] (
                          [userid]        INTEGER PRIMARY KEY,
                          [email]     NVARCHAR(100) NOT NULL,
                          [password]  NVARCHAR(100) ,
                          [secretKey] BLOB ,
                          [minutes]   INTEGER
                          )";

        public SQLiteHelper(string directory)
        {
            string path = null;
            if (String.IsNullOrEmpty(directory))
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _fileName);
                //path = rpath;//if this library is runs in a hosted mode(IIS,IIS Development server), Actual Directory will be some like c:\program files\common files\microsoft shared \devserver 10.0
            }
            else
            {
                path = Path.Combine(directory, _fileName);
            }
            _dbConnection = string.Format("Data Source={0}", path);

            if (!File.Exists(path))
            {
                CreateDefaultDatabase(path);
                InsertDefaultData();
            }
        }

        private void CreateDefaultDatabase(string path)
        {
            System.Data.SQLite.SQLiteConnection.CreateFile(path);
            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection(string.Format("Data Source={0}", path)))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = createOtpTableQuery;
                    com.ExecuteNonQuery();

                    com.CommandText = createUserTableQuery;
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public DataTable GetDataTable(SQLiteCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    DataTable result = new DataTable();
                    result.Load(reader);
                    return result;
                }
            }
        }

        public SQLiteCommand GetCommand(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                throw new ArgumentNullException("sql");

            return new SQLiteCommand { CommandText = sql, CommandType = CommandType.Text };
        }

        public int ExecuteNonQuery(SQLiteCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                return command.ExecuteNonQuery();
            }
        }

        public void InsertDefaultData()
        {
            string secret = StringUtils.RandomString(20);
            SQLiteCommand command = GetCommand("INSERT INTO user('email','password','secretKey', 'minutes') VALUES ('" + "zhangzequn@kyotsu.com" + "','abcd1234',@secretKey, " + 1 + ")");
            command.Parameters.Add(new SQLiteParameter("secretKey", DbType.Binary) { Value = System.Text.Encoding.UTF8.GetBytes(secret) });
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO user('email','password', 'minutes') VALUES ('" + "limingliang@kyotsu.com" + "','abcd1234', " + 1 + ")";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO wordbook('userid','item', 'trans','time') VALUES (1,'" + "データ" + "','data', " + (DateTime.Now-baseTime).Seconds + ")";
                command.ExecuteNonQuery();
            }
        }

        public int CheckUser(string email, string password)
        {
            int ret = -1;
            string secret = StringUtils.RandomString(20);
            SQLiteCommand command = GetCommand("SELECT count(*) FROM user where email='" + email + "'" + " and password='" + password + "'");
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                object obj = command.ExecuteScalar();
                int cnt =Convert.ToInt32(obj);
                if (cnt > 0)
                {
                    command.CommandText = "SELECT userid FROM user where email='" + email + "'" + " and password='" + password + "'";
                    ret = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return ret;
        }

        public bool ValidUser(string userid)
        {
            bool ret = false;
            string secret = StringUtils.RandomString(20);
            SQLiteCommand command = GetCommand("SELECT count(*) FROM user where id=" + userid + "");
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                int cnt =Convert.ToInt32(command.ExecuteScalar());
                if (cnt > 0)
                {
                    ret = true;
                }
            }
            return ret;
        }

        public byte[] GetSecretKey(string email, string password)
        {
            byte[] ret = new byte[1];
            SQLiteCommand command = GetCommand("SELECT secretKey FROM user where email='" + email + "'" + " and password='" + password + "'");
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = new byte[reader.GetBytes(0, 0, null, 0, int.MaxValue) - 1];
                        reader.GetBytes(0, 0, ret, 0, ret.Length);
                    }
                }
            }
            return ret;
        }

        public int GetMiniutes(string email, string password)
        {
            int ret = 0;
            string secret = StringUtils.RandomString(20);
            SQLiteCommand command = GetCommand("SELECT minutes FROM user where email='" + email + "'" + " and password='" + password + "'");
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ret = reader.GetInt32(0);
                    }
                }
            }

            return ret;
        }

        public int GetUserId(string email)
        {
            int userid = -1;
            string secret = StringUtils.RandomString(20);
            SQLiteCommand command = GetCommand("SELECT userid FROM user where email='" + email + "'");
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userid = reader.GetInt32(0);
                    }
                }
            }
            return userid;
        }

        public Wordbook GetWords(string user_id)
        {
            //int userid = GetUserId(user_id);
            Wordbook wordbook = new Wordbook();
            wordbook.Words = new List<Word>();
            SQLiteCommand command = GetCommand("SELECT id,item,trans,time FROM wordbook where userid=" + user_id);
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                using (var reader = command.ExecuteReader())
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        Word word = new Word();
                        word.Item = reader.GetString(1);
                        word.Trans = reader.GetString(2);
                        Int64 timeI = reader.GetInt64(3);
                        DateTime time = baseTime.AddSeconds(timeI);
                        word.Time = new TDateTime();
                        word.Time.Year = (short)time.Year;
                        word.Time.Month = (short)time.Month;
                        word.Time.Day = (short)time.Day;
                        word.Time.Hour = (short)time.Hour;
                        word.Time.Minute = (short)time.Minute;
                        word.Time.Second = (short)time.Second;
                        count++;
                        wordbook.Words.Add(word);
                    }
                    wordbook.Count = count;
                }
            }

            return wordbook;
        }

        public bool UpdateWords(List<Word> words,string userid)
        {
            bool ret = true;
            //user valid
            if (ValidUser(userid))
            {
                if (words != null && words.Count > 0)
                {
                    SQLiteCommand command = new SQLiteCommand() ;
                    using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
                    {
                        connection.Open();
                        command.Connection = connection;
                        for (int i = 0; i < words.Count; i++)
                        {
                            Word word = words[i];
                            double secs = (new DateTime(word.Time.Year, word.Time.Month, word.Time.Day, word.Time.Hour, word.Time.Minute, word.Time.Second, DateTimeKind.Utc) -
                                    baseTime).Seconds;
                            command.CommandText = "SELECT count(*) FROM user where userid=" + userid +  " and item= '" + word.Item + "' ";
                            int count = Convert.ToInt32(command.ExecuteScalar());
                            if (count > 0)
                            { // if need to be update based time
                                
                                command.CommandText = "SELECT count(*) FROM user where userid=" + userid + "" + " and time < " + secs + " and item= '" + word.Item + "' ";
                                count = Convert.ToInt32(command.ExecuteScalar());
                                if (count > 0)
                                { //update
                                    command.CommandText = "update table wordbook set time= " + secs + " and trans='" + word.Trans + "' " + "and tags='" + word.Tags + "' ";
                                    command.ExecuteNonQuery();
                                }
                                else
                                { // do nothing because the data in the database is newer than 

                                }
                            }
                            else // insert 
                            {
                                command.CommandText = "INSERT INTO wordbook('userid','item','trans', 'tags','time') VALUES (" +userid + ",'"+word.Item +"','"+word.Trans+"', '"+word.Tags+"'," + secs + ")";
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public int DeleteWords(List<string> words, string userid)
        {
            int ret = 0;
            //user valid
            if (ValidUser(userid))
            {
                if (words != null && words.Count > 0)
                {
                    SQLiteCommand command = new SQLiteCommand();
                    using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
                    {
                        connection.Open();
                        command.Connection = connection;
                        for (int i = 0; i < words.Count; i++)
                        {
                            command.CommandText = "delete from table where word= '" + words[i] + "' and userid=" + userid;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            return ret;
        }

        public int InsertWords(Word word, string userid)
        {
            int ret = 0;
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnection))
            {
                connection.Open();
                command.Connection = connection;
                if (String.IsNullOrEmpty(word.Trans))
                {
                    command.CommandText = "INSERT INTO wordbook('userid','item','time') VALUES (" + userid + ",'" + word.Item + "', " + (DateTime.Now - baseTime).Seconds + ")";
                }
                else
                {
                    command.CommandText = "INSERT INTO wordbook('userid','item', 'trans','time') VALUES (" + userid + ",'" + word.Item + "','" + word.Trans + "', " + (DateTime.Now - baseTime).Seconds + ")";
                }
                ret =command.ExecuteNonQuery();
            }
            return ret;
        }
    }
}

