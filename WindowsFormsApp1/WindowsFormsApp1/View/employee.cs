using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.model;

namespace WindowsFormsApp1.View
{
    public partial class employee : Form
    {
        private companyDBEntities db = new companyDBEntities();
        public employee()
        {
            InitializeComponent();
            fill_combobox();
            fill_datagrid();
            button_check();


        }
        
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            if (textBox1.Text==string.Empty)
            {
                var proj = db.Employee.Where(x => x.name == textBox1.Text);
                if (proj.Count() > 0)
                {
                    MessageBox.Show("name of employee is already exist", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty )
            {
                label8.Text = "";
                label8.Text += "please insert Employee name \n";
            }
            else
            {


                if (textBox2.Text == string.Empty ||textBox2.Text.Length>2)
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
                                        db.Employee.Add(emp);
                                        db.SaveChanges();
                                        Close();
                                        employee em = new employee();
                                        em.Show();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            if (textBox4.Text !=string.Empty)
            {
                var employee_username = db.Employee.Where(x => x.username == textBox4.Text).FirstOrDefault();
                if (employee_username != null)
                {
                    MessageBox.Show("user name must be unique", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                }

            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
           int id=int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

           int count= db.Employee.Where(x => x.manger_id == id).Count();
            if (count > 0)
            {
                MessageBox.Show("Employee you want to delete is manager to another employee","warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                string name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                if (MessageBox.Show("are you sure you want to delete " + name, "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.Entry(db.Employee.Find(id)).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    MessageBox.Show("Employee is deleted");
                    fill_datagrid();
                    fill_combobox();
                }
            }
            button_check();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            update_employee frm = new update_employee();
            frm.textBox7.Text= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            frm.textBox1.Text =dataGridView1.SelectedRows[0].Cells[1].Value.ToString() ;
            frm.textBox3.Text =dataGridView1.SelectedRows[0].Cells[2].Value.ToString() ;
            frm.textBox4.Text =dataGridView1.SelectedRows[0].Cells[4].Value.ToString() ;
            frm.textBox5.Text =dataGridView1.SelectedRows[0].Cells[7].Value.ToString() ;
            frm.dateTimePicker2.Value =Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[5].Value);
            frm.textBox2.Text =dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            frm.comboBox1.Text = get_manager(dataGridView1.SelectedRows[0].Cells[3].Value);
            frm.ShowDialog();
            fill_datagrid();


        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch == 46)
            {
                e.Handled = true;
                return;
            }
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }
        void fill_datagrid()
        {
            dataGridView1.DataSource = db.proc_employeefor_datagrid().Where(x => x.name != "No manager").ToList();
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[0].Visible = false;
        }
        void fill_combobox()
        {
            comboBox1.DataSource = (from p in db.Employee
                                     select new { p.name, p.id }).ToList();
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
        }
        string get_manager(object name)
        {
          if(  name !=null)
            {
                return name.ToString();
            }
            else
            {
                return "No manager";
            }
        }
        void button_check()
        {
            if (dataGridView1.Rows.Count == 0)
            {
                button4.Enabled = false;
                button5.Enabled = false;
            };
        }
    }
}
