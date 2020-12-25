using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApp1.View
{
    public partial class LoginForm : Form
    {
        private model.companyDBEntities db = new model.companyDBEntities();
        Thread th;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text !=string.Empty)
            {
                var emp = db.Employee.Where(x => x.username == textBox1.Text && x.password == textBox2.Text).FirstOrDefault();
                if (emp == null)
                {
                    label3.Text = "username or password is wrong";
                }
                else
                {
                 this.Close();
                 th = new Thread(opennewform);
                 th.SetApartmentState(ApartmentState.STA);
                 th.Start();
                }
                
                
            }
            else
            {
                label3.Text = "Please enter username and password";
            }
           
        }
        private void opennewform()
        {
            Application.Run(new Form1());
        }
    }
}
