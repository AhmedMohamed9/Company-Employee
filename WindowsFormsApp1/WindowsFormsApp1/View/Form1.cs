using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using WindowsFormsApp1.model;
using WindowsFormsApp1.View;

namespace WindowsFormsApp1
{
    public  partial class Form1 : Form
    {
        companyDBEntities db = new companyDBEntities();

        public Form1()
        {
            InitializeComponent();
        }
        public void access()
        {
            this.button1.Enabled = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            foreach (var item in db.getemployee().Where(x=>x.employee!= "No manager"))
            {
                if (string.IsNullOrEmpty(item.manager))
                {
                    TreeNode parentnode = new TreeNode();
                    parentnode.Text = item.employee.ToString();
                    getchildrennode(parentnode, item.employee);
                    //recursive fun
                    
                    treeView1.Nodes.Add(parentnode);
                    
                }
            }
            
        }
       private void getchildrennode(TreeNode tree,string item)
        {
           List<string> items = getchildren(item);
            foreach (var ad in items)
            {
                TreeNode childnode = new TreeNode();
                childnode.Text = ad;
                tree.Nodes.Add(childnode);
                if (getchildren(ad).Count > 0)
                {
                    getchildrennode(childnode,ad );
                }
            }

        }
        private List<string> getchildren(string parent)
        {
            List<string> children = (from p in db.getemployee()
                                             where p.manager == parent
                                             select p.employee).ToList();
            return children;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            employee em = new employee();
            em.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}

