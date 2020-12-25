using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.model;

namespace WindowsFormsApp1.View
{
    public partial class update_employee : Form
    {
        private companyDBEntities db = new model.companyDBEntities();
        public update_employee()
        {
            InitializeComponent();
            comboBox1.DataSource = (from p in db.Employee
                                   select new { p.name, p.id }).ToList();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                label8.Text = "";
                label8.Text += "please insert Employee name \n";
            }
            else
            {


                if (textBox2.Text == string.Empty || textBox2.Text.Length > 2)
                {
                    label13.Text = "";
                    label13.Text += "please insert Employee Number of children \n";
                }
                else
                {

                    if (textBox3.Text == string.Empty || textBox3.Text.Length > 50)
                    {
                        label9.Text = "";
                        label9.Text += "please insert Employee Title \n";
                    }
                    else
                    {

                        if (textBox4.Text == string.Empty || textBox4.Text.Length > 20)
                        {
                            label10.Text = "";
                            label10.Text += "please insert Employee User Name \n";
                        }
                        else
                        {
                            if (textBox5.Text == string.Empty || textBox5.Text.Length > 15)
                            {
                                label11.Text = "";
                                label11.Text += "please insert Employee Password and no more 15 number \n";
                            }
                            else
                            {
                                if (textBox6.Text == string.Empty)
                                {
                                    label12.Text = "";
                                    label12.Text += "please insert Employee Re Password \n";

                                }
                                else
                                {
                                    if (textBox6.Text != textBox5.Text)
                                    {
                                        label12.Text = "";
                                        label12.Text += "please sure password and Re-Password is same \n";
                                    }
                                    else
                                    {
                                        Employee emp = new Employee()
                                            {
                                                id = int.Parse(textBox7.Text),
                                                name = textBox1.Text,
                                                title = textBox3.Text,
                                                username = textBox4.Text,
                                                password = textBox5.Text,
                                                birth_date = dateTimePicker2.Value,
                                                num_of_child = Int16.Parse(textBox2.Text),
                                            };
                                            if (comboBox1.Text.ToString() != "No manager")
                                            {
                                                emp.manger_id = int.Parse(comboBox1.SelectedValue.ToString());
                                            }
                                            var local = db.Set<Employee>()
                                                 .Local
                                                 .FirstOrDefault(f => f.id == emp.id);
                                            if (local != null)
                                            {
                                                db.Entry(local).State = EntityState.Detached;
                                            }
                                            db.Entry(emp).State = EntityState.Modified;
                                            db.SaveChanges();
                                            Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }



        

        private void label1_Click(object sender, EventArgs e)
        {

            
        }
    }
}
