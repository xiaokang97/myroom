using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.BLL;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.WinForm;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int username;
            bool x =Int32.TryParse(textBox1.Text.ToString(),out username);
            if (x)
            {
                string password = textBox2.Text;
                Users user = new UserBLL().ValidLogin(username, password);
                if (user != null)
                {
                    if (checkBox1.Checked)
                    {
                        textBox1.Text = username.ToString();
                        textBox2.Text = password;
                    }
                    MessageBox.Show("登录成功！");
                    this.Hide();
                    XtraForm1 xtraform1 = new XtraForm1();//Form2为主窗体
                    xtraform1.Show();

                }
                else
                {
                    MessageBox.Show("登录失败！用户名或密码错误！");
                }
            }
            else
            {
                MessageBox.Show("用户名格式不正确！");
            }
        }
    }
}
