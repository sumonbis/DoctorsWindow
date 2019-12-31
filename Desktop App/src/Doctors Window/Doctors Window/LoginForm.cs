using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Doctors_Window.GatewayDB;
using Doctors_Window.Data_Access;

namespace Doctors_Window
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        Gateway gatewayObj = new Gateway();

        private void button1_Click(object sender, EventArgs e)
        {
            Gateway gatewayObj = new Gateway();
            if (passwordTextBox.Text.Equals(gatewayObj.getPassword(userNameTextBox.Text)))
            {
                Doctors_Window doctorsWindowObj = new Doctors_Window();
                doctorsWindowObj.Show();
                this.Hide();               

            }
            else {
                MessageBox.Show("Username and password doesn't match.");

            
            }

        }

        private void forgotPasswordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            groupBox1.Show();
            loginPanel.Hide();

            Data dataObj = new Data();
            dataObj = gatewayObj.getSecurity();


            label5.Text = dataObj.SecurityQuestio;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Data dataObj = new Data();
            dataObj = gatewayObj.getSecurity();
            if (dataObj.SecurityAnswer == textBox1.Text)
            {
                this.Hide();
                Doctors_Window doctorsWindowObj = new Doctors_Window();
                doctorsWindowObj.Show();

            }
            else
            {
                MessageBox.Show("Wrong Answer!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            loginPanel.Show();
            groupBox1.Hide();





        }
    }
}
