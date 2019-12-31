using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using Doctors_Window.GatewayDB;
using Doctors_Window.DBConnect;
using Doctors_Window.Data_Access;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;


namespace Doctors_Window
{
    public partial class Doctors_Window : Form
    {
        public Doctors_Window()
        {
            InitializeComponent();
            newPatientPatientIdTextBox.Text = gatewayObj.getNewPatientID().ToString();
            newPrescriptionPatientIdTextBox.Text = gatewayObj.getNewPatientID().ToString();
            port = null;
            receiveNow = new AutoResetEvent(false);
            loadPorts();
            this.suggestion(newTest1TextBox);
            this.suggestion(newTest2TextBox);
            this.suggestion(newTest3TextBox);
            this.suggestion(newTest4TextBox);
            this.suggestion(newTest5TextBox);
            this.suggestion(newTest6TextBox);
            this.suggestion(newTest7TextBox);
            this.drugSuggestion(drug1TextBox);
            this.drugSuggestion(drug2TextBox);
            this.drugSuggestion(drug3TextBox);
            this.drugSuggestion(drug4TextBox);
            this.drugSuggestion(drug5TextBox);
            this.drugSuggestion(drug6TextBox);
            this.drugSuggestion(drug7TextBox);
            this.drugSuggestion(drug8TextBox);
            this.drugSuggestion(drug9TextBox);
            this.drugSuggestion(drug10TextBox);
            
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
        
        }
        private AutoResetEvent receiveNow;
        private SerialPort port;
        ShortMessageCollection messages;
        Gateway gatewayObj = new Gateway();

        private string portName;

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void showInfo()
        {

            
            Data dataObj = new Data();

            dataObj = gatewayObj.getDoctorsInfo();
            doctorsProfileGroupBox.Show();

            nameLlabel.Text = dataObj.DoctorsName;
            qualificationLabel.Text = dataObj.Qualification;
            specialityLabel.Text = dataObj.Speciality;
            designationLabel.Text = dataObj.Designation;
            institutionLabel.Text = dataObj.Institution;
            addressLabel.Text = dataObj.Address;
            mobLabel.Text = dataObj.MobileNo;
            phoneLlabel.Text = dataObj.PhoneNo;
            visitingTimeLabel.Text = dataObj.VisitingTime;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel5.Hide(); 
            doctorsProfilePanel.Show();
            doctorsProfileGroupBox.Show();
            patientPanel.Hide();            
            accountSettingsPanel.Hide();
            panel3.Hide();
            panel4.Hide();
            
            
            Data dataObj = new Data();
            dataObj = gatewayObj.getDoctorsInfo();
            if (gatewayObj.getDoctorsInfo().DoctorsName==null)
            {
                profileInfoGroupBox.Show();
                doctorsProfileGroupBox.Hide();
            }
            else {
               
                doctorsProfileGroupBox.Show();

                nameLlabel.Text = dataObj.DoctorsName;
                qualificationLabel.Text = dataObj.Qualification;
                specialityLabel.Text = dataObj.Speciality;
                designationLabel.Text = dataObj.Designation;
                institutionLabel.Text = dataObj.Institution;
                addressLabel.Text = dataObj.Address;
                mobLabel.Text = dataObj.MobileNo;
                phoneLlabel.Text = dataObj.PhoneNo;
                visitingTimeLabel.Text = dataObj.VisitingTime;
            }
            

            
        }

        private void mobileNoTextBox_TextChanged(object sender, EventArgs e)
        {
        
       
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == '.'|| e.KeyChar==(char)Keys.Space);
            saveSuccessfullLebel.Hide();
        }
       
        private void mobileNoTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                saveSuccessfullLebel.Hide();
            }

            
        }
     
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (doctorsNameTextBox.Text == "")            
                errorProvider2.SetError(doctorsNameTextBox, "Required");           
            else            
                errorProvider2.SetError(doctorsNameTextBox, String.Empty);

            if (qualificationRichTextBox.Text == "")
                errorProvider2.SetError(qualificationRichTextBox, "Required");
            else
                errorProvider2.SetError(qualificationRichTextBox, String.Empty);

            if (specialityTextBox.Text == "")
                errorProvider2.SetError(specialityTextBox, "Required");
            else
                errorProvider2.SetError(specialityTextBox, String.Empty);

            if (designationTextBox.Text == "")
                errorProvider2.SetError(designationTextBox, "Required");
            else
                errorProvider2.SetError(designationTextBox, String.Empty);

            if (institutionTextBox.Text == "")
                errorProvider2.SetError(institutionTextBox, "Required");
            else
                errorProvider2.SetError(institutionTextBox, String.Empty);

            if (addressRichTextBox.Text == "")
                errorProvider2.SetError(addressRichTextBox, "Required");
            else
                errorProvider2.SetError(addressRichTextBox, String.Empty);


            if (mobileNoTextBox.Text == "")
                errorProvider2.SetError(mobileNoTextBox, "Required");
            else
                errorProvider2.SetError(mobileNoTextBox, String.Empty);

            if (doctorsNameTextBox.Text != "" && qualificationRichTextBox.Text != "" && specialityTextBox.Text != "" && designationTextBox.Text != "" && institutionTextBox.Text != "" && addressRichTextBox.Text != "" && mobileNoTextBox.Text != "")
            {
                Data dataObj = new Data();

                dataObj.DoctorsName = doctorsNameTextBox.Text;
                dataObj.Qualification = qualificationRichTextBox.Text;
                dataObj.Speciality = specialityTextBox.Text;
                dataObj.Designation = designationTextBox.Text;
                dataObj.Institution = institutionTextBox.Text;
                dataObj.Address = addressRichTextBox.Text;
                dataObj.MobileNo = mobileNoTextBox.Text;
                dataObj.PhoneNo = phoneNoTextBox.Text;
                dataObj.VisitingTime = visitingTimeTextBox.Text;


                gatewayObj.insertDoctorsInfo(dataObj);

                MessageBox.Show("Doctor's Profile is Saved.");

                doctorsNameTextBox.Text = "";
                qualificationRichTextBox.Text = "";
                specialityTextBox.Text = "";
                designationTextBox.Text = "";
                institutionTextBox.Text = "";
                addressRichTextBox.Text = "";
                mobileNoTextBox.Text = "";
                phoneNoTextBox.Text = "";


                profileInfoGroupBox.Hide();

                nameLlabel.Text = dataObj.DoctorsName;
                qualificationLabel.Text = dataObj.Qualification;
                specialityLabel.Text = dataObj.Speciality;
                designationLabel.Text = dataObj.Designation;
                institutionLabel.Text = dataObj.Institution;
                addressLabel.Text = dataObj.Address;
                mobLabel.Text = dataObj.MobileNo;
                phoneLlabel.Text = dataObj.PhoneNo;
                doctorsProfileGroupBox.Show();

            }  
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            doctorsProfileGroupBox.Hide();
            profileInfoGroupBox.Show();
            saveButton.Visible = false;
            updateButton.Visible = true;

             
            
            Data dataObj = new Data();

            dataObj = gatewayObj.getDoctorsInfo();
            

            doctorsNameTextBox.Text = dataObj.DoctorsName;
            qualificationRichTextBox.Text = dataObj.Qualification;
            specialityTextBox.Text = dataObj.Speciality;
            designationTextBox.Text = dataObj.Designation;
            institutionTextBox.Text = dataObj.Institution;
            addressRichTextBox.Text = dataObj.Address;
            mobileNoTextBox.Text = dataObj.MobileNo;
            phoneNoTextBox.Text = dataObj.PhoneNo;
            visitingTimeTextBox.Text = dataObj.VisitingTime;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {

           // Gateway gatewayObj = new Gateway();
            Data dataObj = new Data();

            dataObj.DoctorsName = doctorsNameTextBox.Text;
            dataObj.Qualification = qualificationRichTextBox.Text;
            dataObj.Speciality = specialityTextBox.Text;
            dataObj.Designation = designationTextBox.Text;
            dataObj.Institution = institutionTextBox.Text;
            dataObj.Address = addressRichTextBox.Text;
            dataObj.MobileNo = mobileNoTextBox.Text;
            dataObj.PhoneNo = phoneNoTextBox.Text;
            dataObj.VisitingTime = visitingTimeTextBox.Text;


            gatewayObj.UpdateDoctorDetails(dataObj);

            MessageBox.Show("Doctor's profile has been updated.");
            profileInfoGroupBox.Hide();

            showInfo();

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void designationLabel_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult Result = MessageBox.Show("Are You Sure", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);



            switch (Result)
            {

                case DialogResult.Yes:

                    gatewayObj.deleteDoctorsInfo();
                    profileInfoGroupBox.Show();
                    doctorsProfileGroupBox.Hide();
                    doctorsNameTextBox.Text="";
             qualificationRichTextBox.Text="";
            specialityTextBox.Text="";
            designationTextBox.Text="";
            institutionTextBox.Text="";
            addressRichTextBox.Text="";
            mobileNoTextBox.Text="";
             phoneNoTextBox.Text="";
             updateButton.Hide();
             saveButton.Show();
                    break;

                case DialogResult.No:

                    doctorsProfileGroupBox.Show();
                    break;
            }
        }

        private void accountSettingsButton_Click(object sender, EventArgs e)
        {
            panel5.Hide(); 
            accountSettingsPanel.Show();
            patientPanel.Hide();            
            doctorsProfilePanel.Hide();
            panel3.Hide();
            panel4.Hide();

            Data dataObj = new Data();
            dataObj = gatewayObj.getUserInfo();
            
            usernameLabel.Text = dataObj.UserNmae;
            passwordLabel.Text = dataObj.Password;
            questionLabel.Text = dataObj.SecurityQuestio;
            answerLabel.Text = dataObj.SecurityAnswer;

        }

        private void editLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            editAccountPanel.Show();
            accountSettingsPanel.Hide();


            Data dataObj = new Data();
            dataObj = gatewayObj.getUserInfo();


            textBox1.Text = dataObj.UserNmae;
            textBox2.Text = dataObj.Password;
            textBox3.Text = dataObj.SecurityQuestio;
            textBox4.Text = dataObj.SecurityAnswer;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                errorProvider2.SetError(textBox1, "Required");
            else
                errorProvider2.SetError(textBox1, String.Empty);

            if (textBox2.Text == "")
                errorProvider2.SetError(textBox2, "Required");
            else
                errorProvider2.SetError(textBox2, String.Empty);

            if (textBox3.Text == "")
                errorProvider2.SetError(textBox3, "Required");
            else
                errorProvider2.SetError(textBox3, String.Empty);

            if (textBox4.Text == "")
                errorProvider2.SetError(textBox4, "Required");
            else
                errorProvider2.SetError(textBox4, String.Empty);

            if(textBox2.Text!=textBox5.Text)
                 errorProvider2.SetError(textBox5, "Password doesn't match!");
            else
                errorProvider2.SetError(textBox5, String.Empty);

            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox2.Text == textBox5.Text)
            { 
            Gateway gatewayObj = new Gateway();
            Data dataObj = new Data();

            dataObj.UserNmae = textBox1.Text;
            dataObj.Password = textBox2.Text;
            dataObj.SecurityQuestio = textBox3.Text;
            dataObj.SecurityAnswer = textBox4.Text;



            gatewayObj.UpdateUserInfo(dataObj);
            MessageBox.Show("Success");
            editAccountPanel.Hide();
            accountSettingsPanel.Show(); 
            
            dataObj = gatewayObj.getUserInfo();

            
            usernameLabel.Text = dataObj.UserNmae;
            passwordLabel.Text = dataObj.Password;
            questionLabel.Text = dataObj.SecurityQuestio;
            answerLabel.Text = dataObj.SecurityAnswer;

            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            patientPanel.Show();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            doctorsProfilePanel.Hide();
            accountSettingsPanel.Hide();
            newPatientPatientIdTextBox.Text = gatewayObj.getNewPatientID().ToString();
            newPrescriptionPatientIdTextBox.Text = gatewayObj.getNewPatientID().ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (newPatientPatientNameTextBox.Text == "")
                errorProvider2.SetError(newPatientPatientNameTextBox, "Required");
            else
                errorProvider2.SetError(newPatientPatientNameTextBox, String.Empty);

            if (newPatientAgeTextBox.Text == "")
                errorProvider2.SetError(newPatientAgeTextBox, "Required");
            else
                errorProvider2.SetError(newPatientAgeTextBox, String.Empty);

            if (newPatientSexComboBox.Text == "")
                errorProvider2.SetError(newPatientSexComboBox, "Required");
            else
                errorProvider2.SetError(newPatientSexComboBox, String.Empty);
                        
            if (newPatientAddressRichTextBox.Text == "")
                errorProvider2.SetError(newPatientAddressRichTextBox, "Required");
            else
                errorProvider2.SetError(newPatientAddressRichTextBox, String.Empty);


            if (newPatientPatientNameTextBox.Text != "" && newPatientAgeTextBox.Text != "" && newPatientSexComboBox.Text != "" && newPatientAddressRichTextBox.Text != "")
            {


                DBConnection dbConnectionObj = new DBConnection();
                SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
                string insertDoctorsInfoString = @"insert into patientTable values('" + newPatientPatientIdTextBox.Text + "','" + newPatientPatientNameTextBox.Text + "','" + newPatientAgeTextBox.Text + "','" + newPatientSexComboBox.Text + "','" + newPatientFathersNameTextBox.Text + "','" + newPatientAddressRichTextBox.Text + "','" + newPatientMobileNoTextBox.Text + "');";
                SqlCommand sqlCommandObj = new SqlCommand(insertDoctorsInfoString, sqlConnectionObj);
                dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertDoctorsInfoString, dbConnectionObj.GetConnectionObj());
                saveSuccessfullLebel.Show();

                newPrescriptionPatientNameTextBox.Text = newPatientPatientNameTextBox.Text;
                newPrescriptionAgeTextBox.Text = newPatientAgeTextBox.Text;
                newPrescriptionSexComboBox.Text = newPatientSexComboBox.Text;
                newPrescriptionFathersNameTextBox.Text = newPatientFathersNameTextBox.Text;
                newPrescriptionAddressRichTextBox.Text = newPatientAddressRichTextBox.Text;
                newPrescriptionMobileNoTextBox.Text = newPatientMobileNoTextBox.Text;

                newPatientPatientIdTextBox.Text = gatewayObj.getNewPatientID().ToString();

                string ab = newPatientPatientIdTextBox.Text;
                Double ba = Convert.ToDouble(ab);
                ba = ba - 1;
                newPrescriptionPatientIdTextBox.Text = ba.ToString();
                newPrescriptionPrescriptionNoTextBox.Text = ba.ToString() + DateDateTimePicker.Value.Day.ToString() + DateDateTimePicker.Value.Month.ToString() + DateDateTimePicker.Value.Year.ToString() + TimeDateTimePicker.Value.Hour.ToString() + TimeDateTimePicker.Value.Minute.ToString();
                newPatientPatientNameTextBox.Text = "";
                newPatientAgeTextBox.Text = "";
                newPatientSexComboBox.SelectedIndex = -1;
                newPatientFathersNameTextBox.Text = "";
                newPatientAddressRichTextBox.Text = "";
                newPatientMobileNoTextBox.Text = "";
            }
        
        }

        private void newPatientClearAllButton_Click(object sender, EventArgs e)
        {
            
            newPatientPatientNameTextBox.Text = "";
            newPatientAgeTextBox.Text = "";
            newPatientSexComboBox.SelectedIndex = -1;
            newPatientFathersNameTextBox.Text = "";
            newPatientAddressRichTextBox.Text = "";
            newPatientMobileNoTextBox.Text = "";
            saveSuccessfullLebel.Hide();
        }
       
        private void button11_Click(object sender, EventArgs e)
        {
            DBConnection dbConnectionObj = new DBConnection();


            string selectString = "select * from patientTable where patientId='"+newPrescriptionPatientIdTextBox.Text+"'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {

                newPrescriptionPatientNameTextBox.Text = reader[1].ToString();
                newPrescriptionAgeTextBox.Text = reader[2].ToString();
                newPrescriptionSexComboBox.Text = reader[3].ToString();
                newPrescriptionFathersNameTextBox.Text = reader[4].ToString();
                newPrescriptionAddressRichTextBox.Text = reader[5].ToString();
                newPrescriptionMobileNoTextBox.Text = reader[6].ToString();

            }
            else {

                newPrescriptionPatientNameTextBox.Text = "";
                newPrescriptionAgeTextBox.Text ="";
                newPrescriptionSexComboBox.Text = "";
                newPrescriptionFathersNameTextBox.Text = "";
                newPrescriptionAddressRichTextBox.Text = "";
                newPrescriptionMobileNoTextBox.Text = "";
            }
        }

        private void textBox66_TextChanged(object sender, EventArgs e)
        {
           
            DBConnection dbConnectionObj2 = new DBConnection();


            string selectFromPrescription = "select * from prescriptionTable where prescriptionNo='" + oldPrescriptionNoTextBox.Text + "'";
            SqlConnection sqlConnectionObj1 = dbConnectionObj2.GetConnectionObj();
            SqlCommand sqlCommandObj1 = new SqlCommand(selectFromPrescription, sqlConnectionObj1);
            SqlDataReader reader1 = sqlCommandObj1.ExecuteReader();

            if (reader1.Read())
            {

                oldPatientIdTextBox.Text = reader1[0].ToString();
                oldPatientNameTextBox.Text = reader1[2].ToString();
                oldAgeTextBox.Text = reader1[3].ToString();
                oldSexTextBox.Text = reader1[4].ToString();
                oldFathersNameTextBox.Text = reader1[5].ToString();
                oldAddressRichTextBox.Text = reader1[6].ToString();
                oldMobileNoTextBox.Text = reader1[7].ToString();
                oldDateTextBox.Text = reader1[8].ToString();
                oldTimeTextBox.Text = reader1[9].ToString();
                oldSystolTextBox.Text = reader1[10].ToString();

                oldDayastolTextBox.Text = reader1[11].ToString();
                oldPulseRateTextBox.Text = reader1[12].ToString();
                oldDescriptionRichTextBox.Text = reader1[13].ToString();

                oldTest1TextBox.Text = reader1[14].ToString();
                oldTest2TextBox.Text = reader1[15].ToString();
                oldTest3TextBox.Text = reader1[16].ToString();
                oldTest4TextBox.Text = reader1[17].ToString();
                oldTest5TextBox.Text = reader1[18].ToString();
                oldTest6TextBox.Text = reader1[19].ToString();
                oldTest7TextBox.Text = reader1[20].ToString();

                oldDrug1TextBox.Text = reader1[21].ToString();
                oldDrug2TextBox.Text = reader1[22].ToString();
                oldDrug3TextBox.Text = reader1[23].ToString();
                oldDrug4TextBox.Text = reader1[24].ToString();
                oldDrug5TextBox.Text = reader1[25].ToString();
                oldDrug6TextBox.Text = reader1[26].ToString();
                oldDrug7TextBox.Text = reader1[27].ToString();
                oldDrug8TextBox.Text = reader1[28].ToString();
                oldDrug9TextBox.Text = reader1[29].ToString();
                oldDrug10TextBox.Text = reader1[30].ToString();

                oldStrength1TextBox.Text = reader1[31].ToString();
                oldStrength2TextBox.Text = reader1[32].ToString();
                oldStrength3TextBox.Text = reader1[33].ToString();
                oldStrength4TextBox.Text = reader1[34].ToString();
                oldStrength5TextBox.Text = reader1[35].ToString();
                oldStrength6TextBox.Text = reader1[36].ToString();
                oldStrength7TextBox.Text = reader1[37].ToString();
                oldStrength8TextBox.Text = reader1[38].ToString();
                oldStrength9TextBox.Text = reader1[39].ToString();
                oldStrength10TextBox.Text = reader1[40].ToString();

                oldQuantity1TextBox.Text = reader1[41].ToString();
                oldQuantity2TextBox.Text = reader1[42].ToString();
                oldQuantity3TextBox.Text = reader1[43].ToString();
                oldQuantity4TextBox.Text = reader1[44].ToString();
                oldQuantity5TextBox.Text = reader1[45].ToString();
                oldQuantity6TextBox.Text = reader1[46].ToString();
                oldQuantity7TextBox.Text = reader1[47].ToString();
                oldQuantity8TextBox.Text = reader1[48].ToString();
                oldQuantity9TextBox.Text = reader1[49].ToString();
                oldQuantity10TextBox.Text = reader1[50].ToString();

                OldFrequency1TextBox.Text = reader1[51].ToString();
                OldFrequency2TextBox.Text = reader1[52].ToString();
                Oldfrequency3TextBox.Text = reader1[53].ToString();
                Oldfrequency4TextBox.Text = reader1[54].ToString();
                Oldfrequency5TextBox.Text = reader1[55].ToString();
                Oldfrequency6TextBox.Text = reader1[56].ToString();
                Oldfrequency7TextBox.Text = reader1[57].ToString();
                Oldfrequency8TextBox.Text = reader1[58].ToString();
                Oldfrequency9TextBox.Text = reader1[59].ToString();
                Oldfrequency10TextBox.Text = reader1[60].ToString();

                oldRemarks1TextBox.Text = reader1[61].ToString();
                oldRemarks2TextBox.Text = reader1[62].ToString();
                oldRemarks3TextBox.Text = reader1[63].ToString();
                oldRemarks4TextBox.Text = reader1[64].ToString();
                oldRemarks5TextBox.Text = reader1[65].ToString();
                oldRemarks6TextBox.Text = reader1[66].ToString();
                oldRemarks7TextBox.Text = reader1[67].ToString();
                oldRemarks8TextBox.Text = reader1[68].ToString();
                oldRemarks9TextBox.Text = reader1[69].ToString();
                oldRemarks10TextBox.Text = reader1[70].ToString();
                oldAdviceRichTextBox.Text = reader1[71].ToString();
                label106.Hide();




            }
            else if(oldPrescriptionNoTextBox.Text=="")
            {label106.Hide();}
            else
            {
                label106.Show();
                oldPatientIdTextBox.Text = "";
                oldPatientNameTextBox.Text = "";
                oldAgeTextBox.Text = "";
                oldSexTextBox.Text = "";
                oldFathersNameTextBox.Text = "";
                oldAddressRichTextBox.Text = "";
                oldMobileNoTextBox.Text = "";
                oldDateTextBox.Text = "";
                oldTimeTextBox.Text = "";


                oldDayastolTextBox.Text = "";
                oldPulseRateTextBox.Text = "";
                oldDescriptionRichTextBox.Text = "";

                oldTest1TextBox.Text = "";
                oldTest2TextBox.Text = "";
                oldTest3TextBox.Text = "";
                oldTest4TextBox.Text = "";
                oldTest5TextBox.Text = "";
                oldTest6TextBox.Text = "";
                oldTest7TextBox.Text = "";

                oldDrug1TextBox.Text = "";
                oldDrug2TextBox.Text = "";
                oldDrug3TextBox.Text = "";
                oldDrug4TextBox.Text = "";
                oldDrug5TextBox.Text = "";
                oldDrug6TextBox.Text = "";
                oldDrug7TextBox.Text = "";
                oldDrug8TextBox.Text = "";
                oldDrug9TextBox.Text = "";
                oldDrug10TextBox.Text = "";

                oldStrength1TextBox.Text = "";
                oldStrength2TextBox.Text = "";
                oldStrength3TextBox.Text = "";
                oldStrength4TextBox.Text = "";
                oldStrength5TextBox.Text = "";
                oldStrength6TextBox.Text = "";
                oldStrength7TextBox.Text = "";
                oldStrength8TextBox.Text = "";
                oldStrength9TextBox.Text = "";
                oldStrength10TextBox.Text = "";

                oldQuantity1TextBox.Text = "";
                oldQuantity2TextBox.Text = "";
                oldQuantity3TextBox.Text = "";
                oldQuantity4TextBox.Text = "";
                oldQuantity5TextBox.Text = "";
                oldQuantity6TextBox.Text = "";
                oldQuantity7TextBox.Text = "";
                oldQuantity8TextBox.Text = "";
                oldQuantity9TextBox.Text = "";
                oldQuantity10TextBox.Text = "";

                OldFrequency1TextBox.Text = "";
                OldFrequency2TextBox.Text = "";
                Oldfrequency3TextBox.Text = "";
                Oldfrequency4TextBox.Text = "";
                Oldfrequency5TextBox.Text = "";
                Oldfrequency6TextBox.Text = "";
                Oldfrequency7TextBox.Text = "";
                Oldfrequency8TextBox.Text = "";
                Oldfrequency9TextBox.Text = "";
                Oldfrequency10TextBox.Text = "";

                oldRemarks1TextBox.Text = "";
                oldRemarks2TextBox.Text = "";
                oldRemarks3TextBox.Text = "";
                oldRemarks4TextBox.Text = "";
                oldRemarks5TextBox.Text = "";
                oldRemarks6TextBox.Text = "";
                oldRemarks7TextBox.Text = "";
                oldRemarks8TextBox.Text = "";
                oldRemarks9TextBox.Text = "";
                oldRemarks10TextBox.Text = "";
                adviceRichTextBox.Text = "";
            }
           
        }

        private void prescriptionSaveButton_Click(object sender, EventArgs e)
        {
            
            if (newPrescriptionPatientNameTextBox.Text == "")
            {
                MessageBox.Show("Patient details is empty.\n First you have to enter a valid Patient Id.");
            }
            else{
            if (drug1TextBox.Text == "" || strength1TextBox.Text == "" || quantity1TextBox.Text == "" || frequency1TextBox.Text == "")
            { MessageBox.Show("You have to fill  up the first row of drug."); }
            else{
            DBConnection dbConnectionObj = new DBConnection();
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();

            string insertPrescription = "insert into prescriptionTable values('" + newPrescriptionPatientIdTextBox.Text + "','" + newPrescriptionPrescriptionNoTextBox.Text + "'" +
                                             ",'" + newPrescriptionPatientNameTextBox.Text + "','" + newPrescriptionAgeTextBox.Text + "','" + newPrescriptionSexComboBox.Text + "','" + newPrescriptionFathersNameTextBox.Text + "'" +
                                             ",'" + newPrescriptionAddressRichTextBox.Text + "','" + newPrescriptionMobileNoTextBox.Text + "','" + DateDateTimePicker.Text + "','" + TimeDateTimePicker.Text + "','" + newBPSTextBox.Text + "'" +
                                             ",'" + newBPDTextBox.Text + "','" + newPulseRateTextBox.Text + "','" + newDescriptionRichTextBox.Text + "','" + newTest1TextBox.Text + "','" + newTest2TextBox.Text + "','" + newTest3TextBox.Text + "'" +
                                             ",'" + newTest4TextBox.Text + "','" + newTest5TextBox.Text + "','" + newTest6TextBox.Text + "','" + newTest7TextBox.Text + "','" + drug1TextBox.Text + "','" + drug2TextBox.Text + "','" + drug3TextBox.Text + "'" +
                                             ",'" + drug4TextBox.Text + "','" + drug5TextBox.Text + "','" + drug6TextBox.Text + "','" + drug7TextBox.Text + "','" + drug8TextBox.Text + "','" + drug9TextBox.Text + "','" + drug10TextBox.Text + "'" +
                                             ",'" + strength1TextBox.Text + "','" + strength2TextBox.Text + "','" + strength3TextBox.Text + "','" + strength4TextBox.Text + "','" + strength5TextBox.Text + "','" + strength6TextBox.Text + "'" +
                                             ",'" + strength7TextBox.Text + "','" + strength8TextBox.Text + "','" + strength9TextBox.Text + "','" + strength10TextBox.Text + "','" + quantity1TextBox.Text + "','" + quantity2TextBox.Text + "'" +
                                             ",'" + quantity3TextBox.Text + "','" + quantity4TextBox.Text + "','" + quantity5TextBox.Text + "','" + quantity6TextBox.Text + "','" + quantity7TextBox.Text + "','" + quantity8TextBox.Text + "','" + quantity9TextBox.Text + "'" +
                                             ",'" + quantity10TextBox.Text + "','" + frequency1TextBox.Text + "','" + frequency2TextBox.Text + "','" + frequency3TextBox.Text + "','" + frequency4TextBox.Text + "','" + frequency5TextBox.Text + "'" +
                                             ",'" + frequency6TextBox.Text + "','" + frequency7TextBox.Text + "','" + frequency8TextBox.Text + "','" + frequency9TextBox.Text + "','" + frequency10TextBox.Text + "','" + remarks1TextBox.Text + "','" + remarks2TextBox.Text + "'" +
                                             ",'" + remarks3TextBox.Text + "','" + remarks4TextBox.Text + "','" + remarks5TextBox.Text + "','" + remarks6TextBox.Text + "','" + remarks7TextBox.Text + "','" + remarks8TextBox.Text + "','" + remarks9TextBox.Text + "','" + remarks10TextBox.Text + "','"+adviceRichTextBox.Text+"');";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription, dbConnectionObj.GetConnectionObj());

            DBConnection dbConnectionObj61 = new DBConnection();
            SqlConnection sqlConnectionObj61 = dbConnectionObj61.GetConnectionObj();

            string insertPrescription61 = "IF EXISTS (SELECT * FROM teststable WHERE tests='"+newTest1TextBox.Text+"') UPDATE teststable SET tests='"+newTest1TextBox.Text+"' WHERE tests='"+newTest1TextBox.Text+"' ELSE INSERT INTO teststable(tests) VALUES('"+newTest1TextBox.Text+"')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription61, dbConnectionObj61.GetConnectionObj());


            DBConnection dbConnectionObj62= new DBConnection();
            SqlConnection sqlConnectionObj62 = dbConnectionObj62.GetConnectionObj();

            string insertPrescription62 = "IF EXISTS (SELECT * FROM teststable WHERE tests='" + newTest2TextBox.Text + "') UPDATE teststable SET tests='" + newTest2TextBox.Text + "' WHERE tests='" + newTest2TextBox.Text + "' ELSE INSERT INTO teststable(tests) VALUES('" + newTest2TextBox.Text + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription62, dbConnectionObj62.GetConnectionObj());
            DBConnection dbConnectionObj63 = new DBConnection();
            SqlConnection sqlConnectionObj63 = dbConnectionObj63.GetConnectionObj();

            string insertPrescription63 = "IF EXISTS (SELECT * FROM teststable WHERE tests='" + newTest3TextBox.Text + "') UPDATE teststable SET tests='" + newTest3TextBox.Text + "' WHERE tests='" + newTest3TextBox.Text + "' ELSE INSERT INTO teststable(tests) VALUES('" + newTest3TextBox.Text + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription63, dbConnectionObj63.GetConnectionObj());
            DBConnection dbConnectionObj64 = new DBConnection();
            SqlConnection sqlConnectionObj64 = dbConnectionObj64.GetConnectionObj();

            string insertPrescription64 = "IF EXISTS (SELECT * FROM teststable WHERE tests='" + newTest4TextBox.Text + "') UPDATE teststable SET tests='" + newTest4TextBox.Text + "' WHERE tests='" + newTest4TextBox.Text + "' ELSE INSERT INTO teststable(tests) VALUES('" + newTest4TextBox.Text + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription64, dbConnectionObj64.GetConnectionObj());
            DBConnection dbConnectionObj65 = new DBConnection();
            SqlConnection sqlConnectionObj65 = dbConnectionObj65.GetConnectionObj();

            string insertPrescription65 = "IF EXISTS (SELECT * FROM teststable WHERE tests='" + newTest5TextBox.Text + "') UPDATE teststable SET tests='" + newTest5TextBox.Text + "' WHERE tests='" + newTest5TextBox.Text + "' ELSE INSERT INTO teststable(tests) VALUES('" + newTest5TextBox.Text + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription65, dbConnectionObj65.GetConnectionObj());
            DBConnection dbConnectionObj66 = new DBConnection();
            SqlConnection sqlConnectionObj66 = dbConnectionObj66.GetConnectionObj();

            string insertPrescription66 = "IF EXISTS (SELECT * FROM teststable WHERE tests='" + newTest6TextBox.Text + "') UPDATE teststable SET tests='" + newTest6TextBox.Text + "' WHERE tests='" + newTest6TextBox.Text + "' ELSE INSERT INTO teststable(tests) VALUES('" + newTest6TextBox.Text + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription65, dbConnectionObj66.GetConnectionObj());

            DBConnection dbConnectionObj67 = new DBConnection();
            SqlConnection sqlConnectionObj67 = dbConnectionObj67.GetConnectionObj();

            string insertPrescription67 = "IF EXISTS (SELECT * FROM teststable WHERE tests='" + newTest7TextBox.Text + "') UPDATE teststable SET tests='" + newTest7TextBox.Text + "' WHERE tests='" + newTest7TextBox.Text + "' ELSE INSERT INTO teststable(tests) VALUES('" + newTest7TextBox.Text + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(insertPrescription67, dbConnectionObj67.GetConnectionObj());
            this.exi(drug1TextBox.Text);
            this.exi(drug2TextBox.Text);
            this.exi(drug3TextBox.Text);
            this.exi(drug4TextBox.Text);
            this.exi(drug5TextBox.Text);
            this.exi(drug6TextBox.Text);
            this.exi(drug7TextBox.Text);
            this.exi(drug8TextBox.Text);
            this.exi(drug9TextBox.Text);
            this.exi(drug10TextBox.Text);
                
                prescriptionSaveButton.Hide();
            newPresPrintButton.Show();
            prescriptionChangeButton.Show();
            }
            }   
        }

        private void exi(String hi)
        {
            DBConnection dbConnectionObj64 = new DBConnection();
            SqlConnection sqlConnectionObj64 = dbConnectionObj64.GetConnectionObj();

            string insertPrescription64 = "IF EXISTS (SELECT * FROM drugstable WHERE drugs='" + hi + "') UPDATE drugstable SET drugs='" + hi + "' WHERE drugs='" + hi + "' ELSE INSERT INTO drugstable(drugs) VALUES('" + hi + "')";
            //SqlCommand sqlCommandObj = new SqlCommand(insertPrescription, sqlConnectionObj);
            dbConnectionObj64.ExecuteSqlCommandAndCloseConnection(insertPrescription64, dbConnectionObj64.GetConnectionObj());
        
        }
        private void prescriptionChangeButton_Click(object sender, EventArgs e)
        {
            DBConnection dbConnectionObj = new DBConnection();

            string updatePrescription = "update prescriptionTable set BPSystolic='" + newBPSTextBox.Text + "',BPDiastolic='" + newBPDTextBox.Text + "',pulseRate='" + newPulseRateTextBox.Text + "',description='" + newDescriptionRichTextBox.Text + "',test1='" + newTest1TextBox.Text + "',test2='" + newTest2TextBox.Text + "',test3='" + newTest3TextBox.Text + "',test4='" + newTest4TextBox.Text + "',test5='" + newTest5TextBox.Text + "',test6='" + newTest6TextBox.Text + "',test7='" + newTest7TextBox.Text + "',drug1='" + drug1TextBox.Text + "',drug2='" + drug2TextBox.Text + "',drug3='" + drug3TextBox.Text + "',drug4='" + drug4TextBox.Text + "',drug5='" + drug5TextBox.Text + "',drug6='" + drug6TextBox.Text + "',drug7='" + drug7TextBox.Text + "',drug8='" + drug8TextBox.Text + "',drug9='" + drug9TextBox.Text + "',drug10='" + drug10TextBox.Text + "',strength1='" + strength1TextBox.Text + "',strength2='" + strength2TextBox.Text + "',strength3='" + strength3TextBox.Text + "',strength4='" + strength4TextBox.Text + "',strength5='" + strength5TextBox.Text + "',strength6='" + strength6TextBox.Text + "',strength7='" + strength7TextBox.Text + "',strength8='" + strength8TextBox.Text + "',strength9='" + strength9TextBox.Text + "',strength10='" + strength10TextBox.Text + "',quantity1='" + quantity1TextBox.Text + "',quantity2='" + quantity2TextBox.Text + "',quantity3='" + quantity3TextBox + "',quantity4='" + quantity4TextBox + "',quantity5='" + quantity5TextBox.Text + "',quantity6='" + quantity6TextBox.Text + "',quantity7='" + quantity7TextBox.Text + "',quantity8='" + quantity8TextBox.Text + "',quantity9='" + quantity9TextBox.Text + "',quantity10='" + quantity10TextBox.Text + "',frequency1='" + frequency1TextBox.Text + "',frequency2='" + frequency2TextBox.Text + "',frequency3='" + frequency3TextBox.Text + "',frequency4='" + frequency4TextBox.Text + "',frequency5='" + frequency5TextBox.Text + "',frequency6='" + frequency6TextBox.Text + "',frequency7='" + frequency7TextBox.Text + "',frequency8='" + frequency8TextBox.Text + "',frequency9='" + frequency9TextBox.Text + "',frequency10='" + frequency10TextBox.Text + "',remarks1='" + remarks1TextBox.Text + "',remarks2='" + remarks2TextBox.Text + "',remarks3='" + remarks3TextBox.Text + "',remarks4='" + remarks4TextBox.Text + "',remarks5='" + remarks5TextBox.Text + "',remarks6='" + remarks6TextBox.Text + "',remarks7='" + remarks7TextBox.Text + "',remarks8='" + remarks8TextBox.Text + "',remarks9='" + remarks9TextBox.Text + "',remarks10='" + remarks10TextBox.Text + "',advice='" + adviceRichTextBox.Text + "'   where prescriptionNo='" + newPrescriptionPrescriptionNoTextBox.Text + "'";

            dbConnectionObj.ExecuteSqlCommandAndCloseConnection(updatePrescription, dbConnectionObj.GetConnectionObj());
        }

        private void clearAllButton_Click_1(object sender, EventArgs e)
        {
            prescriptionChangeButton.Hide();
            newPresPrintButton.Hide();
            prescriptionSaveButton.Show();
            newBPSTextBox.Text = "";
            newBPDTextBox.Text = "";
            newPulseRateTextBox.Text = "";
            newDescriptionRichTextBox.Text = "";
            newTest1TextBox.Text = "";
            newTest2TextBox.Text = "";
            newTest3TextBox.Text = "";
            newTest4TextBox.Text = "";
            newTest5TextBox.Text = "";
            newTest6TextBox.Text = "";
            newTest7TextBox.Text = "";
            drug1TextBox.Text = "";
            drug2TextBox.Text = "";
            drug3TextBox.Text = "";
            drug4TextBox.Text = "";
            drug5TextBox.Text = "";
            drug6TextBox.Text = "";
            drug7TextBox.Text = "";
            drug8TextBox.Text = "";
            drug9TextBox.Text = "";
            drug10TextBox.Text = "";
            strength1TextBox.Text = "";
            strength2TextBox.Text = "";
            strength3TextBox.Text = "";
            strength4TextBox.Text = "";
            strength5TextBox.Text = "";
            strength6TextBox.Text = "";
            strength7TextBox.Text = "";
            strength8TextBox.Text = "";
            strength9TextBox.Text = "";
            strength10TextBox.Text = "";
            quantity1TextBox.Text = "";
            quantity2TextBox.Text = "";
            quantity3TextBox.Text = "";
            quantity4TextBox.Text = "";
            quantity5TextBox.Text = "";
            quantity6TextBox.Text = "";
            quantity7TextBox.Text = "";
            quantity8TextBox.Text = "";
            quantity9TextBox.Text = "";
            quantity10TextBox.Text = "";
            frequency1TextBox.Text = "";
            frequency2TextBox.Text = "";
            frequency3TextBox.Text = "";
            frequency4TextBox.Text = "";
            frequency5TextBox.Text = "";
            frequency6TextBox.Text = "";
            frequency7TextBox.Text = "";
            frequency8TextBox.Text = "";
            frequency9TextBox.Text = "";
            frequency10TextBox.Text = "";
            remarks1TextBox.Text = "";
            remarks2TextBox.Text = "";
            remarks3TextBox.Text = "";
            remarks4TextBox.Text = "";
            remarks5TextBox.Text = "";
            remarks6TextBox.Text = "";
            remarks7TextBox.Text = "";
            remarks8TextBox.Text = "";
            remarks9TextBox.Text = "";
            remarks10TextBox.Text = "";
            adviceRichTextBox.Text = "";
        }

        private void suggestion(TextBox sug) 
        {
            DBConnection dbConnectionObj90 = new DBConnection();
            SqlConnection sqlConnectionObj90 = dbConnectionObj90.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand("SELECT  tests FROM teststable", dbConnectionObj90.GetConnectionObj());
            
            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                MyCollection.Add(reader.GetString(0));
            }
            sug.AutoCompleteCustomSource = MyCollection;


        }

        private void drugSuggestion(TextBox drug)
        {
            DBConnection dbConnectionObj90 = new DBConnection();
            SqlConnection sqlConnectionObj90 = dbConnectionObj90.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand("SELECT  drugs FROM drugstable", dbConnectionObj90.GetConnectionObj());

            SqlDataReader reader = sqlCommandObj.ExecuteReader();
            AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
            while (reader.Read())
            {
                MyCollection.Add(reader.GetString(0));
            }
            drug.AutoCompleteCustomSource = MyCollection;


        }

        private void newPrescriptionPatientIdTextBox_TextChanged_1(object sender, EventArgs e)
        {
            prescriptionChangeButton.Hide();
            newPresPrintButton.Hide();
            prescriptionSaveButton.Show();
            newBPSTextBox.Text = "";
            newBPDTextBox.Text = "";
            newPulseRateTextBox.Text = "";
            newDescriptionRichTextBox.Text = "";
            newTest1TextBox.Text = "";
            newTest2TextBox.Text = "";
            newTest3TextBox.Text = "";
            newTest4TextBox.Text = "";
            newTest5TextBox.Text = "";
            newTest6TextBox.Text = "";
            newTest7TextBox.Text = "";
            drug1TextBox.Text = "";
            drug2TextBox.Text = "";
            drug3TextBox.Text = "";
            drug4TextBox.Text = "";
            drug5TextBox.Text = "";
            drug6TextBox.Text = "";
            drug7TextBox.Text = "";
            drug8TextBox.Text = "";
            drug9TextBox.Text = "";
            drug10TextBox.Text = "";
            strength1TextBox.Text = "";
            strength2TextBox.Text = "";
            strength3TextBox.Text = "";
            strength4TextBox.Text = "";
            strength5TextBox.Text = "";
            strength6TextBox.Text = "";
            strength7TextBox.Text = "";
            strength8TextBox.Text = "";
            strength9TextBox.Text = "";
            strength10TextBox.Text = "";
            quantity1TextBox.Text = "";
            quantity2TextBox.Text = "";
            quantity3TextBox.Text = "";
            quantity4TextBox.Text = "";
            quantity5TextBox.Text = "";
            quantity6TextBox.Text = "";
            quantity7TextBox.Text = "";
            quantity8TextBox.Text = "";
            quantity9TextBox.Text = "";
            quantity10TextBox.Text = "";
            frequency1TextBox.Text = "";
            frequency2TextBox.Text = "";
            frequency3TextBox.Text = "";
            frequency4TextBox.Text = "";
            frequency5TextBox.Text = "";
            frequency6TextBox.Text = "";
            frequency7TextBox.Text = "";
            frequency8TextBox.Text = "";
            frequency9TextBox.Text = "";
            frequency10TextBox.Text = "";
            remarks1TextBox.Text = "";
            remarks2TextBox.Text = "";
            remarks3TextBox.Text = "";
            remarks4TextBox.Text = "";
            remarks5TextBox.Text = "";
            remarks6TextBox.Text = "";
            remarks7TextBox.Text = "";
            remarks8TextBox.Text = "";
            remarks9TextBox.Text = "";
            remarks10TextBox.Text = "";
            adviceRichTextBox.Text = "";

            prescriptionChangeButton.Hide();
            newPresPrintButton.Hide();
            prescriptionSaveButton.Show();
            button11_Click(sender, e);
            newPrescriptionPrescriptionNoTextBox.Text = newPrescriptionPatientIdTextBox.Text + DateDateTimePicker.Value.Day + DateDateTimePicker.Value.Month + DateDateTimePicker.Value.Year + TimeDateTimePicker.Value.Hour + TimeDateTimePicker.Value.Minute;
           
            DBConnection dbConnectionObj = new DBConnection();


            string selectString = "select * from patientTable where patientId='" + newPrescriptionPatientIdTextBox.Text + "'";
            SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
            SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
            SqlDataReader reader = sqlCommandObj.ExecuteReader();

            if (reader.Read())
            {
                errorProvider2.SetError(newPrescriptionPatientIdTextBox, String.Empty);
                newPrescriptionPatientNameTextBox.Text = reader[1].ToString();
                newPrescriptionAgeTextBox.Text = reader[2].ToString();
                newPrescriptionSexComboBox.Text = reader[3].ToString();
                newPrescriptionFathersNameTextBox.Text = reader[4].ToString();
                newPrescriptionAddressRichTextBox.Text = reader[5].ToString();
                newPrescriptionMobileNoTextBox.Text = reader[6].ToString();

            }
            else
            {
                errorProvider2.SetError(newPrescriptionPatientIdTextBox, "Patient Id doesn't match");          
                    
                newPrescriptionPatientNameTextBox.Text = "";
                newPrescriptionAgeTextBox.Text = "";
                newPrescriptionSexComboBox.Text = "";
                newPrescriptionFathersNameTextBox.Text = "";
                newPrescriptionAddressRichTextBox.Text = "";
                newPrescriptionMobileNoTextBox.Text = "";

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        
        private void newPresPrintButton_Click(object sender, EventArgs e)
        {
            Prescription prescriptionObj = new Prescription();
            prescriptionObj.Show();
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Application.StartupPath + "\\CrystalReport1.rpt");

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            crParameterDiscreteValue.Value = newPrescriptionPrescriptionNoTextBox.Text;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["prescriptionNo"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



            prescriptionObj.crystalReportViewer1.ReportSource = cryRpt;
            prescriptionObj.crystalReportViewer1.Refresh();  
        }

        private void oldPresPrintButton_Click(object sender, EventArgs e)
        {
            Prescription prescriptionObj = new Prescription();
            prescriptionObj.Show();
            ReportDocument cryRpt = new ReportDocument();
            cryRpt.Load(Application.StartupPath+"\\CrystalReport1.rpt");
            prescriptionObj.crystalReportViewer1.Refresh(); 
            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();

            crParameterDiscreteValue.Value = oldPrescriptionNoTextBox.Text;
            crParameterFieldDefinitions = cryRpt.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["prescriptionNo"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



            prescriptionObj.crystalReportViewer1.ReportSource = cryRpt;
            prescriptionObj.crystalReportViewer1.Refresh(); 
        }

        private void doctorsProfileGroupBox_Enter(object sender, EventArgs e)
        {

        }

        

        private void saveTimeButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Data dataObj = new Data();
            dataObj.TimeForPatient = timeComboBox.Text;
            gatewayObj.updateTimeForPatient(dataObj);
            int patientTime = gatewayObj.getTimePerPatient();
            int i;

            for (i = 0; i < 7; i++)
            {
                int tSpan = Convert.ToInt32(TimeSpan.Parse(gatewayObj.getTimeSpan(i)).TotalMinutes);
                dataObj.Time[i] = tSpan / patientTime;
            }

            for (i = 0; i < 7; i++)
            {
                gatewayObj.updateNoOfPatient(dataObj, i);
            }

            if (gatewayObj.isEmpty() == false)
            {


                int sat = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Saturday"));
                int sun = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Sunday"));
                int mon = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Monday"));
                int tue = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Tuesday"));
                int wed = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Wednesday"));
                int thu = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Thursday"));
                int fri = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Friday"));

                gatewayObj.updatePatientCalendar("Saturday", sat.ToString());
                gatewayObj.updatePatientCalendar("Sunday", sun.ToString());
                gatewayObj.updatePatientCalendar("Monday", mon.ToString());
                gatewayObj.updatePatientCalendar("Tuesday", tue.ToString());
                gatewayObj.updatePatientCalendar("Wednesday", wed.ToString());
                gatewayObj.updatePatientCalendar("Thursday", thu.ToString());
                gatewayObj.updatePatientCalendar("Friday", fri.ToString());

            }
            successLabel.Visible = true;
            Cursor.Current = Cursors.Default;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data dataObj = new Data();

            dataObj.Days[0] = dateTimePicker2.Value.TimeOfDay - dateTimePicker1.Value.TimeOfDay;
            dataObj.Days[1] = dateTimePicker3.Value.TimeOfDay - dateTimePicker4.Value.TimeOfDay;
            dataObj.Days[2] = dateTimePicker5.Value.TimeOfDay - dateTimePicker6.Value.TimeOfDay;
            dataObj.Days[3] = dateTimePicker11.Value.TimeOfDay - dateTimePicker12.Value.TimeOfDay;
            dataObj.Days[4] = dateTimePicker9.Value.TimeOfDay - dateTimePicker10.Value.TimeOfDay;
            dataObj.Days[5] = dateTimePicker7.Value.TimeOfDay - dateTimePicker8.Value.TimeOfDay;
            dataObj.Days[6] = dateTimePicker13.Value.TimeOfDay - dateTimePicker14.Value.TimeOfDay;


            if (dataObj.Days[0] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker2, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker2, String.Empty);
            if (dataObj.Days[1] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker3, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker3, String.Empty);
            if (dataObj.Days[2] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker5, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker5, String.Empty);
            if (dataObj.Days[3] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker11, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker11, String.Empty);
            if (dataObj.Days[4] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker9, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker9, String.Empty);
            if (dataObj.Days[5] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker7, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker7, String.Empty);
            if (dataObj.Days[6] < TimeSpan.Zero)
                errorProvider1.SetError(dateTimePicker13, "End time is incorrect.");
            else
                errorProvider1.SetError(dateTimePicker13, String.Empty);

            if (dataObj.Days[0] >= TimeSpan.Zero && dataObj.Days[1] >= TimeSpan.Zero && dataObj.Days[2] >= TimeSpan.Zero && dataObj.Days[3] >= TimeSpan.Zero && dataObj.Days[4] >= TimeSpan.Zero && dataObj.Days[5] >= TimeSpan.Zero && dataObj.Days[6] >= TimeSpan.Zero)
            {

                dataObj.StartTime[0] = dateTimePicker1.Value.TimeOfDay.ToString();
                dataObj.StartTime[1] = dateTimePicker4.Value.TimeOfDay.ToString();
                dataObj.StartTime[2] = dateTimePicker6.Value.TimeOfDay.ToString();
                dataObj.StartTime[3] = dateTimePicker12.Value.TimeOfDay.ToString();
                dataObj.StartTime[4] = dateTimePicker10.Value.TimeOfDay.ToString();
                dataObj.StartTime[5] = dateTimePicker8.Value.TimeOfDay.ToString();
                dataObj.StartTime[6] = dateTimePicker14.Value.TimeOfDay.ToString();

                dataObj.EndTime[0] = dateTimePicker2.Value.TimeOfDay.ToString();
                dataObj.EndTime[1] = dateTimePicker3.Value.TimeOfDay.ToString();
                dataObj.EndTime[2] = dateTimePicker5.Value.TimeOfDay.ToString();
                dataObj.EndTime[3] = dateTimePicker11.Value.TimeOfDay.ToString();
                dataObj.EndTime[4] = dateTimePicker9.Value.TimeOfDay.ToString();
                dataObj.EndTime[5] = dateTimePicker7.Value.TimeOfDay.ToString();
                dataObj.EndTime[6] = dateTimePicker13.Value.TimeOfDay.ToString();

                int patientTime = gatewayObj.getTimePerPatient();
                int i;

                for (i = 0; i < 7; i++)
                {
                    int tSpan = Convert.ToInt32(dataObj.Days[i].TotalMinutes);
                    dataObj.Time[i] = tSpan / patientTime;
                }

                for (i = 0; i < 7; i++)
                {
                    gatewayObj.updateWeekTable(dataObj, i);
                }

                if (gatewayObj.isEmpty())
                {
                    Cursor.Current = Cursors.WaitCursor;
                    DateTime date = DateTime.Now.AddDays(-1);
                    int day;
                    string no_patient = "0";

                    for (int j = 1; j <= 30; j++)
                    {
                        date = date.AddDays(1);
                        day = (int)date.DayOfWeek;

                        DBConnection dbConnectionObj = new DBConnection();
                        string selectString = "select no_of_patient from week_setting where day='" + date.DayOfWeek.ToString() + "'";
                        SqlConnection sqlConnectionObj = dbConnectionObj.GetConnectionObj();
                        SqlCommand sqlCommandObj = new SqlCommand(selectString, sqlConnectionObj);
                        SqlDataReader reader = sqlCommandObj.ExecuteReader();

                        if (reader.Read())
                        {
                            no_patient = reader[0].ToString();
                        }

                        gatewayObj.insertPatientCalendar(j, date.ToString("dd/MM/yyyy"), no_patient, date.DayOfWeek.ToString(), no_patient);

                    }
                    label115.Visible = true;
                    Cursor.Current = Cursors.Default;
                }
                else
                {
                    int sat = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Saturday"));
                    int sun = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Sunday"));
                    int mon = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Monday"));
                    int tue = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Tuesday"));
                    int wed = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Wednesday"));
                    int thu = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Thursday"));
                    int fri = int.Parse(gatewayObj.getNoOfPatient("Saturday")) - int.Parse(gatewayObj.getNoOfPatient("Friday"));

                    gatewayObj.updatePatientCalendar("Saturday", sat.ToString());
                    gatewayObj.updatePatientCalendar("Sunday", sun.ToString());
                    gatewayObj.updatePatientCalendar("Monday", mon.ToString());
                    gatewayObj.updatePatientCalendar("Tuesday", tue.ToString());
                    gatewayObj.updatePatientCalendar("Wednesday", thu.ToString());
                    gatewayObj.updatePatientCalendar("Thursday", wed.ToString());
                    gatewayObj.updatePatientCalendar("Friday", fri.ToString());
                    label115.Visible = true;
                }
            }

        }
        private void updateDatabase()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string id = gatewayObj.getId(date);
            int i = Int32.Parse(id);
            gatewayObj.updateDatabase(i);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            //updateDatabase();
            // Get and verify port name
            portName = cboPort.Text;
            Update();
            if (portName.Length == 0)
            {
                MessageBox.Show(this, "You must enter a port name.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Set up the phone and read the messages
            try
            {
                this.port = OpenPort(portName);
                Cursor.Current = Cursors.WaitCursor;
                // Check connection
                ExecCommand("AT", 300, "No phone connected at " + portName + ".");
                // Use message format "Text mode"
                ExecCommand("AT+CMGF=1", 300, "Failed to set message format.");
                // Use character set "ISO 8859-1"
                //	ExecCommand("AT+CSCS=\"8859-1\"", 300, "Failed to set character set.");
                // Select SIM storage
                ExecCommand("AT+CPMS=\"SM\"", 300, "Failed to select message storage.");
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (port != null)
                {
                    ClosePort(this.port);
                    this.port = null;
                }
                return;
            }

            if (port != null)
            {
                MessageBox.Show(this, "The modem is connected at PORT: " + portName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connectButton.Enabled = false;
                disconnectButton.Enabled = true;


                if (backgroundWorker1.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    backgroundWorker1.RunWorkerAsync(this);
                }
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            disconnect();
        }

        private void loadPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cboPort.Items.Add(port);
            }
        }

        string input;
        private void read()
        {
            lvwMessages.Items.Clear();
            Update();

            // Set up the phone and read the messages

            try
            {
                // Read the messages
                input = ExecCommand("AT+CMGL=\"ALL\"", 5000, "Failed to read the messages.");
                messages = ParseMessages(input);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                disconnect();
                return;
            }

            if (messages != null)
                DisplayMessages(messages);
        }
        public void DeleteMsg(string p_strCommand)
        {
            //bool isDeleted = false;
            try
            {
                string recievedData;
                //recievedData = ExecCommand("AT", 300, "No phone connected");
                //recievedData = ExecCommand("AT+CMGF=1", 300, "Failed to set message format.");
                String command = p_strCommand;
                recievedData = ExecCommand(command, 300, "Failed to delete message");

                //if (recievedData.EndsWith("\r\nOK\r\n"))
                //{
                //    isDeleted = true;
                //}
                //if (recievedData.Contains("ERROR"))
                //{
                //    isDeleted = false;
                //}
                //return isDeleted;
            }
            catch (Exception ex)
            {
                cancelBackgroundWorker();
                MessageBox.Show(ex.Message + "\nabbbbb");
            }

        }

        private void DisplayMessages(ShortMessageCollection messages)
        {
            lvwMessages.BeginUpdate();
            foreach (ShortMessage msg in messages)
            {
                ListViewItem item = new ListViewItem(new string[] { msg.Sender, msg.Message, msg.Sent });
                item.Tag = msg;
                lvwMessages.Items.Add(item);

                // insert message into inbox
                Data dataObj = new Data();
                dataObj.InboxCount = gatewayObj.countInbox() + 1;

                dataObj.SenderNo = msg.Sender;
                dataObj.ShortMessage = msg.Message;
                dataObj.SendingTime = msg.Sent;
                gatewayObj.insertIntoInbox(dataObj);

            }
            lvwMessages.EndUpdate();

            // delete all message
            string strCommand = "AT+CMGD=1,4";
            DeleteMsg(strCommand);


        }

        private ShortMessageCollection ParseMessages(string input)
        {
            ShortMessageCollection messages = new ShortMessageCollection();

            //+CMGL: 1,"REC READ","+85291234567",,"07/05/01,08:00:15+32",145,37 It is easy to list SMS text messages.
            Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");
            Match m = r.Match(input);
            while (m.Success)
            {
                ShortMessage msg = new ShortMessage();
                msg.Index = int.Parse(m.Groups[1].Value);
                msg.Status = m.Groups[2].Value;
                msg.Sender = m.Groups[3].Value;
                msg.Alphabet = m.Groups[4].Value;
                msg.Sent = m.Groups[5].Value;
                msg.Message = m.Groups[6].Value;
                messages.Add(msg);

                m = m.NextMatch();
            }

            return messages;
        }


        #region Communication
        private SerialPort OpenPort(string portName)
        {
            receiveNow = new AutoResetEvent(false);
            SerialPort port = new SerialPort();
            port.PortName = portName;
            port.BaudRate = 9600;
            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.ReadTimeout = 300;
            port.WriteTimeout = 300;
            port.Encoding = Encoding.GetEncoding("iso-8859-1");
            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            port.Open();
            port.DtrEnable = true;
            port.RtsEnable = true;
            return port;
        }

        private void ClosePort(SerialPort port)
        {
            port.Close();
            port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);
        }

        void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType == SerialData.Chars)
                receiveNow.Set();
        }

        private string ReadResponse(int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        if (buffer.Length > 0)
                            throw new ApplicationException("Response received is incomplete.");
                        else
                            throw new ApplicationException("No data received from phone.");
                    }
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }
        public bool sendMsg(string PhoneNo, string Message)
        {
            bool isSend = false;

            try
            {
                string recievedData;
                //recievedData = ExecCommand("AT", 300, "No phone connected");
                //recievedData = ExecCommand("AT+CMGF=1", 300, "Failed to set message format.");
                string command = "AT+CMGS=\"" + PhoneNo + "\"";
                recievedData = ExecCommand(command, 300, "Failed to accept phoneNo");
                command = Message + char.ConvertFromUtf32(26) + "\r";
                recievedData = ExecCommand(command, 3000, "Failed to send message"); //3 seconds
                if (recievedData.EndsWith("\r\nOK\r\n"))
                {
                    isSend = true;
                }
                else if (recievedData.Contains("ERROR"))
                {
                    isSend = false;
                }
                return isSend;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string ExecCommand(string command, int responseTimeout, string errorMessage)
        {
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                string input = ReadResponse(responseTimeout);
                if ((input.Length == 0) || ((!input.EndsWith("\r\n> ")) && (!input.EndsWith("\r\nOK\r\n"))))
                    throw new ApplicationException("No success message was received.");
                return input;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }
        #endregion

        private void disconnect()
        {
            cancelBackgroundWorker();
            if (port != null)
            {
                ClosePort(this.port);
                disconnectButton.Enabled = false;
                connectButton.Enabled = true;
                this.port = null;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int delay = 10000;
            int interval = 5 * 10000;
            int elapsed = 0;
            int i = 0;
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!worker.CancellationPending)
            {
                elapsed = 0;
                while (elapsed < interval && !worker.CancellationPending)
                {
                    i++;
                    try
                    {
                        //ExecCommand("AT", 300, "No phone connected at " + portName + ".");
                        input = ExecCommand("AT+CMGL=\"ALL\"", 5000, "Failed to read the messages.");

                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //disconnect();  
                        //label109.Text = "Modem has been removed. Please connect again.";
                        cancelBackgroundWorker();
                        return;
                    }
                    worker.ReportProgress((i * 1), input);
                    Thread.Sleep(delay);
                }
            }
            e.Cancel = true;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != -1)
            {
                lvwMessages.Items.Clear();
                Update();
                try
                {
                    input = e.UserState.ToString();
                    messages = ParseMessages(input);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    disconnect();
                }

                if (this.messages != null)
                {
                    // insert into inbox
                    DisplayMessages(this.messages);
                }
                label109.Text = e.ProgressPercentage.ToString();

                // read from inbox
                Data dataObj = new Data();
                dataObj = gatewayObj.getFromInbox();

                // process inbox message
                if (dataObj.SenderMobNo.Count != 0)
                {


                    for (int i = 0; i < dataObj.SenderMobNo.Count; i++) // Loop through List with for
                    {
                        if (dataObj.SenderMobNo[i].Length == 14)
                        {
                            if (isCorrectFormat(dataObj.ReceivedMessage[i]))
                            {
                                // insert record into patient queue
                                double noOfRow = gatewayObj.countQueue() + 1;
                                Data dataObj1 = new Data();
                                Data dataObj2 = new Data();
                                dataObj1 = gatewayObj.appData();
                                dataObj2 = gatewayObj.weekData(dataObj1);
                                TimeSpan time = TimeSpan.Parse(dataObj2.Start);
                                time = time.Add(TimeSpan.FromMinutes(gatewayObj.getTimePerPatient() * (int.Parse(dataObj2.PatientCount) - int.Parse(dataObj1.Remain))));
                                string name = senderName(dataObj.ReceivedMessage[i]);
                                string age = senderAge(dataObj.ReceivedMessage[i]);
                                string a = time.ToString();
                                int remain = Convert.ToInt32(dataObj1.Remain) - 1;

                                gatewayObj.insertIntoQueue(noOfRow.ToString(), dataObj1.Calendar_date, a, age, name, "no", dataObj.SenderMobNo[i]);
                                gatewayObj.updateInbox(dataObj.Id[i]);
                                gatewayObj.updateRemain(remain.ToString(), dataObj1.Calendar_date);

                                // send confirmation message
                                string number = dataObj.SenderMobNo[i].ToString().Substring(3, 11);
                                string message = "Welcome " + name + ". Your appointment with Dr. X has been confirmed on " + dataObj1.Calendar_date + "at " + a + ". Please be present prior the time.";
                                if (sendMsg(number, message))
                                {
                                    gatewayObj.insertIntoOutbox(number, message, "sent");
                                    MessageBox.Show("Confirmation message has sent successfully");
                                }
                                else
                                {
                                    gatewayObj.insertIntoOutbox(number, message, "pending");
                                    MessageBox.Show("Confirmation message could not be sent.");
                                }

                            }
                        }
                        else
                        {
                            // send message for invalid format
                            string number = dataObj.SenderMobNo[i];
                            string message = "Invalid message format. Please try again later.";
                            if (sendMsg(number, message))
                            {
                                gatewayObj.insertIntoOutbox(number, message, "sent");
                                MessageBox.Show("Invalid message has sent successfully");
                            }
                            else
                            {
                                gatewayObj.insertIntoOutbox(number, message, "pending");
                                MessageBox.Show("Failed to send message");
                            }

                        }
                    }

                }
            }
        }
        private bool isCorrectFormat(String msg)
        {
            // First we see the input string.
            string input = msg;

            Match match = Regex.Match(input, @"app+\s+(\d{2,3})+\s+([A-Za-z\'\,\.\-\s]+)", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                // Finally, we get the Group value and display it.
                string key = match.Groups[1].Value;
                return true;
            }
            else
                return false;
        }
        private string senderAge(String msg)
        {
            // First we see the input string.
            string input = msg;
            Match match = Regex.Match(input, @"app+\s+(\d{2,3})+\s+([A-Za-z\'\,\.\-\s]+)", RegexOptions.IgnoreCase);

            string key = match.Groups[1].Value;
            return key;
        }
        private string senderName(String msg)
        {
            // First we see the input string.
            string input = msg;
            Match match = Regex.Match(input, @"app+\s+(\d{2,3})+\s+([A-Za-z\'\,\.\-\s]+)", RegexOptions.IgnoreCase);

            string key = match.Groups[2].Value;
            return key;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if ((e.Cancelled == true))
            {

                label109.Text = "Canceled!";
                MessageBox.Show(this, "The modem is disconnected from PORT: " + portName, "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //MessageBox.Show("Modem has been removed. Please connect again.");
                disconnectButton.Enabled = false;
                connectButton.Enabled = true;
                this.port = null;
            }
            else if (!(e.Error == null))
            {
                label109.Text = ("Error: " + e.Error.Message);
            }
            else
            {
                label109.Text = "Done!";
            }
        }
        private void cancelBackgroundWorker()
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
        }

        private void satDay_CheckedChanged(object sender, EventArgs e)
        {
            if (satDay.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void sunDay_CheckedChanged(object sender, EventArgs e)
        {
            if (sunDay.Checked)
            {
                dateTimePicker3.Enabled = true;
                dateTimePicker4.Enabled = true;
            }
            else
            {
                dateTimePicker3.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker4.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
            }

        }

        private void monDay_CheckedChanged(object sender, EventArgs e)
        {
            if (monDay.Checked)
            {
                dateTimePicker5.Enabled = true;
                dateTimePicker6.Enabled = true;
            }
            else
            {
                dateTimePicker5.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker6.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker5.Enabled = false;
                dateTimePicker6.Enabled = false;
            }
        }

        private void tuesDay_CheckedChanged(object sender, EventArgs e)
        {
            if (tuesDay.Checked)
            {
                dateTimePicker11.Enabled = true;
                dateTimePicker12.Enabled = true;
            }
            else
            {
                dateTimePicker11.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker12.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker11.Enabled = false;
                dateTimePicker12.Enabled = false;
            }

        }

        private void wedDay_CheckedChanged(object sender, EventArgs e)
        {
            if (wedDay.Checked)
            {
                dateTimePicker9.Enabled = true;
                dateTimePicker10.Enabled = true;
            }
            else
            {
                dateTimePicker9.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker10.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker9.Enabled = false;
                dateTimePicker10.Enabled = false;
            }
        }

        private void thusDay_CheckedChanged(object sender, EventArgs e)
        {
            if (thusDay.Checked)
            {
                dateTimePicker7.Enabled = true;
                dateTimePicker8.Enabled = true;
            }
            else
            {
                dateTimePicker7.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker8.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker7.Enabled = false;
                dateTimePicker8.Enabled = false;
            }
        }

        private void friDay_CheckedChanged(object sender, EventArgs e)
        {
            if (friDay.Checked)
            {
                dateTimePicker13.Enabled = true;
                dateTimePicker14.Enabled = true;
            }
            else
            {
                dateTimePicker13.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker14.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                dateTimePicker13.Enabled = false;
                dateTimePicker14.Enabled = false;
            }
        }
        public static void clearControls(Control crl)
        {
            foreach (Control c in crl.Controls)
            {
                if (c.GetType().Name == "GroupBox" || c.GetType().Name == "CustomGroupBox")
                {
                    clearControls(c);
                }
                else if (c.GetType().Name == "TextBox")
                {
                    ((TextBox)c).Text = "";
                }
                else if (c.GetType().Name == "CheckBox")
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c.GetType().Name == "DateTimePicker")
                {
                    ((DateTimePicker)c).Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0);
                }

                else if (c.GetType().Name == "ComboBox")
                {
                    if (((ComboBox)c).Items.Count > 0)
                    {
                        ((ComboBox)c).SelectedIndex = 0;
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            panel5.Hide(); panel4.Show();
            doctorsProfilePanel.Hide();
            accountSettingsPanel.Hide();
            panel3.Hide();
            patientPanel.Hide(); 
            clearControls(groupBox13); 
        }

        private void button6_Click(object sender, EventArgs e)
        {

            panel5.Hide(); panel3.Show();
            doctorsProfilePanel.Hide();
            accountSettingsPanel.Hide();
            patientPanel.Hide(); 
            panel4.Hide();            
        }

        private void patientPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void newTest1TextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            PatientList pl= new PatientList();
            pl.Show();
            ReportDocument cryRpt4 = new ReportDocument();
            cryRpt4.Load(Application.StartupPath+"\\patList.rpt");

            ParameterFieldDefinitions crParameterFieldDefinitions;
            ParameterFieldDefinition crParameterFieldDefinition;
            ParameterValues crParameterValues = new ParameterValues();
            ParameterDiscreteValue crParameterDiscreteValue = new ParameterDiscreteValue();
            DateTime dateTime = DateTime.Now.Date;
            

            crParameterDiscreteValue.Value = dateTime.ToString("dd/MM/yyyy");
            crParameterFieldDefinitions = cryRpt4.DataDefinition.ParameterFields;
            crParameterFieldDefinition = crParameterFieldDefinitions["dat"];
            crParameterValues = crParameterFieldDefinition.CurrentValues;

            crParameterValues.Clear();
            crParameterValues.Add(crParameterDiscreteValue);
            crParameterFieldDefinition.ApplyCurrentValues(crParameterValues);



            pl.pList.ReportSource = cryRpt4;
            pl.pList.Refresh();  
        }

        private void newPrescriptionPrescriptionNoTextBox_TextChanged(object sender, EventArgs e)
        {
            this.suggestion(newTest1TextBox);
            this.suggestion(newTest2TextBox);
            this.suggestion(newTest3TextBox);
            this.suggestion(newTest4TextBox);
            this.suggestion(newTest5TextBox);
            this.suggestion(newTest6TextBox);
            this.suggestion(newTest7TextBox);
            this.drugSuggestion(drug1TextBox);
            this.drugSuggestion(drug2TextBox);
            this.drugSuggestion(drug3TextBox);
            this.drugSuggestion(drug4TextBox);
            this.drugSuggestion(drug5TextBox);
            this.drugSuggestion(drug6TextBox);
            this.drugSuggestion(drug7TextBox);
            this.drugSuggestion(drug8TextBox);
            this.drugSuggestion(drug9TextBox);
            this.drugSuggestion(drug10TextBox);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Data dataObj = new Data();
            if (connectButton.Enabled == false)
            {
                if (radioButton1.Checked)
                {

                    dataObj = gatewayObj.getMobNo(DateTime.Now.ToString("dd/MM/yyyy"));
                    for (int i = 0; i < dataObj.MobNo.Count; i++)
                    {
                        if (sendMsg(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString()))
                        {
                            gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "sent");
                            MessageBox.Show("Message has sent successfully");
                        }
                        else
                        {
                            gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "pending");
                            MessageBox.Show("Failed to send message");
                        }

                    }
                }
                if (radioButton2.Checked)
                {

                    DateTime start = DateTime.Now;
                    DateTime last = gatewayObj.getMaxDate();
                    while (start <= last)
                    {
                        dataObj = gatewayObj.getMobNo(start.ToString("dd/MM/yyyy"));
                        for (int i = 0; i < dataObj.MobNo.Count; i++)
                        {
                            if (sendMsg(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString()))
                            {
                                gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "sent");
                                MessageBox.Show("Invalid message has sent successfully");
                            }
                            else
                            {
                                gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "pending");
                                MessageBox.Show("Failed to send message");
                            }

                        }
                        start.AddDays(1);
                    }
                }
                if (radioButton3.Checked)
                {

                    DateTime start = DateTime.Now;
                    DateTime last = gatewayObj.getMinDate();
                    while (start > last)
                    {
                        dataObj = gatewayObj.getMobNo(start.ToString("dd/MM/yyyy"));
                        for (int i = 0; i < dataObj.MobNo.Count; i++)
                        {
                            if (sendMsg(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString()))
                            {
                                gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "sent");
                                MessageBox.Show("Invalid message has sent successfully");
                            }
                            else
                            {
                                gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "pending");
                                MessageBox.Show("Failed to send message");
                            }

                        }
                        start.AddDays(-1);
                    }

                }
                if (radioButton4.Checked)
                {
                    DateTime start = gatewayObj.getMinDate();
                    DateTime last = gatewayObj.getMaxDate();
                    while (start >= last)
                    {
                        dataObj = gatewayObj.getMobNo(start.ToString("dd/MM/yyyy"));
                        for (int i = 0; i < dataObj.MobNo.Count; i++)
                        {
                            if (sendMsg(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString()))
                            {
                                gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "sent");
                                MessageBox.Show("Invalid message has sent successfully");
                            }
                            else
                            {
                                gatewayObj.insertIntoOutbox(dataObj.MobNo[i].ToString(), richTextBox2.Text.ToString(), "pending");
                                MessageBox.Show("Failed to send message");
                            }

                        }
                        start.AddDays(1);
                    }
                }

            }

            else
            {
                MessageBox.Show(this, "Please connect the modem and try again.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            //int remaining = int.Parse(txt_text_remaining.Text.Trim());
            //remaining -= 1;
            //txt_text_remaining.Text = remaining.ToString();
            int a = 160 - richTextBox2.Text.Length;
            txt_text_remaining.Text = a.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel5.Show();
            accountSettingsPanel.Hide();
            patientPanel.Hide();
            doctorsProfilePanel.Hide();
            panel3.Hide();
            panel4.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button5.Hide();
            button6.Hide();
            button8.Hide();
            panel3.Hide();
            panel4.Hide();
            panel5.Hide();
            button10.Enabled = false;
            button12.Enabled = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button5.Show();
            button6.Show();
            button8.Show();
            
            
            button12.Enabled = false;
            button10.Enabled = true;
        }

        private void Doctors_Window_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Application.StartupPath);
            MessageBox.Show(Application.CommonAppDataPath);
            MessageBox.Show(Application.CompanyName);
        }

       
    }
}
