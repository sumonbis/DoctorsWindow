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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Gateway gatewayObj = new Gateway();
            if (passwordTextBox.Text.Equals(gatewayObj.getPassword(userNameTextBox.Text)))
            {
                this.Hide();
                Doctors_Window doctorsWindowObj = new Doctors_Window();
                doctorsWindowObj.Show();

            }
            else {
                MessageBox.Show(userNameTextBox.Text);

            
            }

        }
    }
}
