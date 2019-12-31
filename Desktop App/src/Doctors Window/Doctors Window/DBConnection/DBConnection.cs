using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Doctors_Window.DBConnect
{
    class DBConnection
    {
        public string GetConnectionString()
        {

            return @"server=.\SQLEXPRESS; Integrated Security=SSPI; Database = doctorswindow;";
        }
        public SqlConnection GetConnectionObj()
        {
            SqlConnection connectionObj = new SqlConnection(GetConnectionString());
            connectionObj.Open();
            return connectionObj;
        }
        public void ExecuteSqlCommandAndCloseConnection(string insertQueryString, SqlConnection connectionObj)
        {
            SqlCommand sqlCommandObj = new SqlCommand(insertQueryString, connectionObj);
            sqlCommandObj.ExecuteNonQuery();
            connectionObj.Close();
        }
        public void ExecuteSqlCommandOnly(string insertQueryString, SqlConnection connectionObj)
        {
            SqlCommand sqlCommandObj = new SqlCommand(insertQueryString, connectionObj);
            sqlCommandObj.ExecuteNonQuery();
        }

    }
}
