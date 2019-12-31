using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Doctors_Window.DBConnect;


namespace Doctors_Window.GatewayDB
{
    class Gateway
    {
        DBConnection dbConnectionObj = new DBConnection();
        
        public string password;

        public string getPassword(String userName)
        {
            
           

            //string connectionString = dbConnectionObj.GetConnectionString();
            string selectString = "select password  from usertable where username='" + userName + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                password = reader[0].ToString();
            }
            return password;
            
        }

        public void insertDoctorsInfo(string doctorsName,string qualification, string speciality,string designation,string institution,string address,string mobile,string phone)
        {
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string insertDoctorsInfoString = "insert into doctorsTable values('"+doctorsName+"','"+qualification+"','"+speciality+"','"+designation+"','"+institution+"','"+address+"','"+mobile+"','"+phone+"')";
            SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());


        }

      
    }
}
