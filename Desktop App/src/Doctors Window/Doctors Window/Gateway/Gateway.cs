using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Doctors_Window.DBConnect;
using Doctors_Window.Data_Access;


namespace Doctors_Window.GatewayDB
{
    class Gateway
    {       
        
        public string password;
        public string timePerPatient;
        public int time;
        public string getNoOfPatient(String day)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select no_of_patient from patient_calendar where day='" + day + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                password = reader[0].ToString();
            }
            return password;
        }
        public string getId(string date)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select id from patient_calendar where date='" + date + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            string id = "";
            if (reader.Read())
            {
                id = reader[0].ToString();
            }
            return id;
        }
        public string getPatient(String day)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select no_of_patient from week_setting where day='" + day + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                password = reader[0].ToString();
            }
            return password;
        }
        public void updatePatientCalendar(string day, string rem)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string updateString = "update patient_calendar set no_of_patient = (select no_of_patient from week_setting where day = '" + day + "'), remain = '" + rem + "' where day = '" + day + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(updateString, dbConnectionObj.GetConnectionObj());

        }
        public void insertIntoInbox(Data dataObj)
        {
            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string insertDoctorsInfoString = "insert into inbox values('" + dataObj.SenderNo + "','" + dataObj.ShortMessage + "','" + dataObj.SendingTime + "', 'no','" + dataObj.InboxCount.ToString() + "')";
            SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());

        }
        public void insertIntoOutbox(string no, string message, string status)
        {
            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string insertDoctorsInfoString = "insert into outbox values('" + no + "','" + message + "','" + DateTime.Now.ToString() + "','" + status + "')";
            SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());

        }
        public void updateDatabase(int id)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update patient_calendar set period_flag = 'past' where id < '" + id + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());

        }
        public void updateRemain(string remain, string date)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update patient_calendar set remain = '" + remain + "' where date = '" + date + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());

        }

        public void updateTimeForPatient(Data dataObj)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update time set time_per_patient = '" + dataObj.TimeForPatient + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());

        }
        public string getTimeSpan(int i)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select timespan from week_setting where id='" + i + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            string timeSpan = "";
            if (reader.Read())
            {
                timeSpan = reader[0].ToString();

            }

            return timeSpan;
        }

        public int getTimePerPatient()
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select time_per_patient  from time";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                timePerPatient = reader[0].ToString();

            }
            time = Convert.ToInt32(timePerPatient);
            return time;
        }
        public void updateNoOfPatient(Data dataObj, int i)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update week_setting set no_of_patient = '" + dataObj.Time[i] + "' where id = '" + i + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());
        }
        public void updateWeekTable(Data dataObj, int i)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update week_setting set start_time = '" + dataObj.StartTime[i] + "', end_time = '" + dataObj.EndTime[i] + "', timespan = '" + dataObj.Days[i] + "', no_of_patient = '" + dataObj.Time[i] + "' where id = '" + i + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());
        }
        public void updateInbox(string id)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update inbox set qued = 'yes' where id = '" + id + "'";
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());

        }

        public void insertPatientCalendar(int id, string date, string no_of_patient, string day, string remaining_slot)
        {
            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string insertDoctorsInfoString = "insert into patient_calendar(id, date, no_of_patient, day, remain) values('" + id + "','" + date + "','" + no_of_patient + "','" + day + "','" + remaining_slot + "')";
            SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());

        }

        public void insertIntoQueue(string id, string date, string time, string age, string name, string visitedFlag, string mob_no)
        {
            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string insertDoctorsInfoString = "insert into patient_queue(id, dates, time, age, name, visited_flag, mob_no) values('" + id + "','" + date + "','" + time + "','" + age + "','" + name + "','" + visitedFlag + "', '" + mob_no + "')";
            SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());

        }

        public bool isEmpty()
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "SELECT count(*) FROM patient_calendar";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);

            int count;
            count = (int)sqlCommandObj.ExecuteScalar();
            if (count > 0)
                return false;
            else
                return true;
        }

        public double countInbox()
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "SELECT count(*) FROM inbox";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);

            double count;
            count = (int)sqlCommandObj.ExecuteScalar();
            return count;

        }
        public double countQueue()
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "SELECT count(*) FROM patient_queue";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);

            double count;
            count = (int)sqlCommandObj.ExecuteScalar();
            return count;
        }
        public DateTime getMaxDate()
        {

            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "SELECT dates FROM patient_queue where id=(select max(id) from patient_queue)";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            DateTime dt = DateTime.Now;

            if (reader.Read())
            {
                dt = Convert.ToDateTime(reader[0].ToString());

            }
            return dt;
        }
        public DateTime getMinDate()
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "SELECT dates FROM patient_queue where id=(select min(id) from patient_queue)";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            DateTime dt = DateTime.Now;

            if (reader.Read())
            {
                dt = Convert.ToDateTime(reader[0].ToString());

            }
            return dt;
        }
        public Data appData()
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select date, day, remain from patient_calendar where id = (SELECT MIN(id) FROM patient_calendar where remain>0 and period_flag = 'live')";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            Data dataObj = new Data();
            if (reader.Read())
            {
                dataObj.Calendar_date = reader[0].ToString();
                dataObj.Calendar_day = reader[1].ToString();
                dataObj.Remain = reader[2].ToString();

            }
            return dataObj;
        }
        public Data weekData(Data dataObj)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string selectString = "select start_time, no_of_patient from week_setting where day = '" + dataObj.Calendar_day + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            dataObj = new Data();
            if (reader.Read())
            {
                dataObj.Start = reader[0].ToString();
                dataObj.PatientCount = reader[1].ToString();
            }
            return dataObj;
        }
        public Data getFromInbox()
        {
            Data dataObj = new Data();
            DBConnection dbConnectionObj = new DBConnection();
            String selectString = "select sender_no, message, id from inbox where qued = 'no'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            while (reader.Read())
            {
                dataObj.SenderMobNo.Add(reader[0].ToString());
                dataObj.ReceivedMessage.Add(reader[1].ToString());
                dataObj.Id.Add(reader[2].ToString());
            }
            return dataObj;
        }
        public Data getMobNo(string date)
        {
            Data dataObj = new Data();
            DBConnection dbConnectionObj = new DBConnection();
            String selectString = "select mob_no from patient_queue where dates = '" + date + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            while (reader.Read())
            {
                dataObj.MobNo.Add(reader[0].ToString());

            }
            return dataObj;
        }
        public string getPassword(String userName)
        {


            DBConnection dbConnectionObj = new DBConnection();
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

        public Data getSecurity()
        {


            Data dataObj = new Data(); 
            DBConnection dbConnectionObj = new DBConnection();
            String selectString = "select question, answer  from usertable";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                dataObj.SecurityQuestio = reader[0].ToString();
                dataObj.SecurityAnswer = reader[1].ToString();
            }
            return dataObj;

        }

        public void insertDoctorsInfo(Data dataObj)
        {
            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string insertDoctorsInfoString = "insert into doctorsTable values('" + dataObj.DoctorsName + "','" + dataObj.Qualification + "','" + dataObj.Speciality + "','" + dataObj.Designation + "','" + dataObj.Institution + "','" + dataObj.Address + "','" + dataObj.MobileNo + "','" + dataObj.PhoneNo + "','"+dataObj.VisitingTime+"')";
            SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());


        }

        public Data getDoctorsInfo()
        {

            Data dataObj = new Data();

            DBConnection dbConnectionObj = new DBConnection();

            
            string selectString = "select * from doctorsTable";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                
                dataObj.DoctorsName = reader[0].ToString();
                dataObj.Qualification = reader[1].ToString();
                dataObj.Speciality = reader[2].ToString(); ;
                dataObj.Designation = reader[3].ToString();
                dataObj.Institution = reader[4].ToString();
                dataObj.Address = reader[5].ToString();
                dataObj.MobileNo = reader[6].ToString();
                dataObj.PhoneNo = reader[7].ToString();
                dataObj.VisitingTime = reader[8].ToString();
            }
            return dataObj;
        }

        public Data getUserInfo()
        {

            Data dataObj = new Data();

            DBConnection dbConnectionObj = new DBConnection();


            string selectString = "select * from usertable";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {

                
                dataObj.UserNmae = reader[1].ToString();
                dataObj.Password = reader[2].ToString();
                dataObj.SecurityQuestio = reader[3].ToString();
                dataObj.SecurityAnswer = reader[4].ToString();
            }
            return dataObj;
        }

        public void UpdateDoctorDetails(Data dataObj)
        {
            DBConnection dbConnectionObj = new DBConnection();
            
            
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update doctorsTable set doctors_name = '" + dataObj.DoctorsName + "', qualification = '" + dataObj.Qualification + "', speciality = '" + dataObj.Speciality + "', designation = '" + dataObj.Designation + "', institution = '" + dataObj.Institution + "', address = '" + dataObj.Address + "', mobile_no = '" + dataObj.MobileNo + "', phone_no = '" + dataObj.PhoneNo + "',visiting_time='"+dataObj.VisitingTime+"';";
            
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());

         
        }

        public void UpdateUserInfo(Data dataObj)
        {
            DBConnection dbConnectionObj = new DBConnection();
            string connectionString = dbConnectionObj.GetConnectionString();
            string insertString = "update usertable set username = '" + dataObj.UserNmae + "', password = '" + dataObj.Password + "', question = '" + dataObj.SecurityQuestio + "', answer = '" + dataObj.SecurityAnswer + "'";

            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertString, dbConnectionObj.GetConnectionObj());


        }

        public void deleteDoctorsInfo()
        {

            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            string deleteString = "DELETE FROM doctorsTable";
            SqlCommand sqlCommandObj = new SqlCommand(deleteString, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(deleteString, dbConnectionObj.GetConnectionObj());

        
        }        

        public long getNewPatientID()
        {

            DBConnection dbConnectionObj = new DBConnection();

            
            long count = 0;
            string selectString = "select patientId from patientTable";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            while(reader.Read())
            {

                count++;
              
               
            }
            return count;
        }

      
    }
}
