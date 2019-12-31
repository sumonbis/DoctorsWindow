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

namespace Doctors_Window
{
    public partial class Doctors_Window : Form
    {
        public Doctors_Window()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            doctorsProfilePanel.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Gateway gatewayObj = new Gateway();
            gatewayObj.insertDoctorsInfo(doctorsNameTextBox.Text, qualificationRichTextBox.Text, specialityTextBox.Text, designationTextBox.Text, institutionTextBox.Text, addressRichTextBox.Text, mobileNoTextBox.Text, phoneNoTextBox.Text);


        }
    }
}
