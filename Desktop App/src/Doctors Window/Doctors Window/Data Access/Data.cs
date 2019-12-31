using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Window.Data_Access
{
    class Data
    
    {
        double inboxCount;
        string calendar_date;
        string calendar_day;
        string start;
        string remain;

        public string Remain
        {
            get { return remain; }
            set { remain = value; }
        }
        List<string> mobNo = new List<string>();

        public List<string> MobNo
        {
            get { return mobNo; }
            set { mobNo = value; }
        }

        public string Start
        {
            get { return start; }
            set { start = value; }
        }
        string patientCount;

        public string PatientCount
        {
            get { return patientCount; }
            set { patientCount = value; }
        }

        public string Calendar_day
        {
            get { return calendar_day; }
            set { calendar_day = value; }
        }

        public string Calendar_date
        {
            get { return calendar_date; }
            set { calendar_date = value; }
        }
        


        public double InboxCount
        {
            get { return inboxCount; }
            set { inboxCount = value; }
        }
      
        private string doctorsName;
        private string qualification;
        private string speciality;
        private string designation;
        private string institution;
        private string address;
        private string mobileNo;
        private string phoneNo;
        private string visitingTime;

        public string VisitingTime
        {
            get { return visitingTime; }
            set { visitingTime = value; }
        }
        List<string> id = new List<string>();

        public List<string> Id
        {
            get { return id; }
            set { id = value; }
        }

        List<string> senderMobNo = new List<string>();

        public List<string> SenderMobNo
        {
            get { return senderMobNo; }
            set { senderMobNo = value; }
        }
        List<string> receivedMessage = new List<string>();

        public List<string> ReceivedMessage
        {
            get { return receivedMessage; }
            set { receivedMessage = value; }
        }

        string senderNo;

        public string SenderNo
        {
            get { return senderNo; }
            set { senderNo = value; }
        }
        string shortMessage;

        public string ShortMessage
        {
            get { return shortMessage; }
            set { shortMessage = value; }
        }
        string sendingTime;

        public string SendingTime
        {
            get { return sendingTime; }
            set { sendingTime = value; }
        }
        TimeSpan[] days = new TimeSpan[7];
        int[] time = new int[7];

        public int[] Time
        {
            get { return time; }
            set { time = value; }
        }

        public TimeSpan[] Days
        {
            get { return days; }
            set { days = value; }
        }


        private string timeForPatient;
        string[] weekDays = new string[7];

        public string[] WeekDays
        {
            get { return weekDays; }
            set { weekDays = value; }
        }

        private string[] startTime = new string[7];

        public string[] StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }


        private string[] endTime = new string[7];

        public string[] EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }


        private TimeSpan timeSpan1;

        public TimeSpan TimeSpan1
        {
            get { return timeSpan1; }
            set { timeSpan1 = value; }
        }
        private string noOfPatient;

        public string NoOfPatient
        {
            get { return noOfPatient; }
            set { noOfPatient = value; }
        }


        public string TimeForPatient
        {
            get { return timeForPatient; }
            set { timeForPatient = value; }
        }

        private string userNmae;

        public string UserNmae
        {
            get { return userNmae; }
            set { userNmae = value; }
        }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string securityQuestio;

        public string SecurityQuestio
        {
            get { return securityQuestio; }
            set { securityQuestio = value; }
        }
        private string securityAnswer;

        public string SecurityAnswer
        {
            get { return securityAnswer; }
            set { securityAnswer = value; }
        }



public string Qualification
{
  get { return qualification; }
  set { qualification = value; }
}


public string Speciality
{
    get { return speciality; }
    set { speciality = value; }
}


public string Designation
{
    get { return designation; }
    set { designation = value; }
}


public string Institution
{
    get { return institution; }
    set { institution = value; }
}


public string Address
{
    get { return address; }
    set { address = value; }
}


public string MobileNo
{
    get { return mobileNo; }
    set { mobileNo = value; }
}


public string PhoneNo
{
    get { return phoneNo; }
    set { phoneNo = value; }
}



        private string _hour1;
        private string _hour2;
        private string _hour3;
        private string _hour4;
        private string _hour5;
        private string _hour6;
        private string _hour7;

        private string _day1;
        private string _day2;
        private string _day3;
        private string _day4;
        private string _day5;
        private string _day6;
        private string _day7;

        private string numcode;
        private string number;

        private string limit;

        private string status;

        private string notRegDate;
        private string notRegDB;

        public string[] NumberList = null;

        public int approximateTime = 0;

        public Data()
        {
            this.doctorsName = doctorsName;
            this.qualification = qualification;
            this.speciality = speciality;
            this.designation = designation;
            this.institution = institution;
            this.address = address;
            this.mobileNo = mobileNo;
            this.phoneNo = phoneNo;
            this.visitingTime = visitingTime;
        }


        public string DoctorsName
        {
            get { return doctorsName; }
            set { doctorsName = value; }
        }

                public string Hour1
        {
            get { return _hour1; }
            set { _hour1 = value; }
        }

        public string Hour2
        {
            get { return _hour2; }
            set { _hour2 = value; }
        }

        public string Hour3
        {
            get { return _hour3; }
            set { _hour3 = value; }
        }

        public string Hour4
        {
            get { return _hour4; }
            set { _hour4 = value; }
        }

        public string Hour5
        {
            get { return _hour5; }
            set { _hour5 = value; }
        }

        public string Hour6
        {
            get { return _hour6; }
            set { _hour6 = value; }
        }

        public string Hour7
        {
            get { return _hour7; }
            set { _hour7 = value; }
        }

        public string Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public string Day1
        {
            get { return _day1; }
            set { _day1 = value; }
        }

        public string Day2
        {
            get { return _day2; }
            set { _day2 = value; }
        }

        public string Day3
        {
            get { return _day3; }
            set { _day3 = value; }
        }

        public string Day4
        {
            get { return _day4; }
            set { _day4 = value; }
        }

        public string Day5
        {
            get { return _day5; }
            set { _day5 = value; }
        }

        public string Day6
        {
            get { return _day6; }
            set { _day6 = value; }
        }

        public string Day7
        {
            get { return _day7; }
            set { _day7 = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Numcode
        {
            get { return numcode; }
            set { numcode = value; }
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public string NotRegDate
        {
            get { return notRegDate; }
            set { notRegDate = value; }
        }

        public string NotRegDb
        {
            get { return notRegDB; }
            set { notRegDB = value; }
        }

       
    }
}
