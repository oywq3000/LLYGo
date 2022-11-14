using System;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace MysqlUtility
{
    public static class MysqlTool
    {
        private static MySqlConnection _mySqlConnection;
    
        //database info for connecting
        private static string ConnectStr
            = "server=127.0.0.1;port=3306;database=test;user=root;password=123456";
        
        
        public static string GetPassword(string name)
        {
            //fixed format
            string sql = $"select password from player where name = '{name}'";
    
            OpenDatabase();
           
            MySqlCommand cmd = new MySqlCommand(sql,_mySqlConnection);
            
            var mySqlDataReader = cmd.ExecuteReader();
            if ( mySqlDataReader.Read())
            {
                var s = mySqlDataReader.GetString("password");
                CloseDataBase();
                return s;
            }
            else
            {
                CloseDataBase();
                return "";
            }
        }
    
    
        public static bool AddPlayer(string name ,string password)
        {
            //fixed format
            string sql = $"insert into player(name,password) values('{name}','{password}')";
            
            OpenDatabase();
            MySqlCommand cmd = new MySqlCommand(sql,_mySqlConnection);
            var executeNonQuery = cmd.ExecuteNonQuery();
            CloseDataBase();
            return executeNonQuery != 0;
        }
        
        
     
        private static void OpenDatabase()
        {
            try
            {
                _mySqlConnection = new MySqlConnection(ConnectStr);
                _mySqlConnection.Open();
            }
            catch (Exception e)
            {
               Debug.Log(ConnectStr);
                Console.WriteLine(e);
                throw;
            }
        }
        private static void CloseDataBase()
        {
            _mySqlConnection.Close();
            _mySqlConnection.Dispose();
        }
    }
}
