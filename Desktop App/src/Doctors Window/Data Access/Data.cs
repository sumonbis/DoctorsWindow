using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors_Window.Data_Access
{
    class Data
    
    { 
      
        private string doctorsName;
        private string qualification;
        private string speciality;
        private string designation;
        private string institution;
        private string address;
        private string mobileNo;
        private string phoneNo;

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
