using DevExpress.XtraEditors.Controls;
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
using WindowsFormsApp1.DAL;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.WinForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Common com = new Common();
        private void button1_Click(object sender, EventArgs e)
        {

            int validRes = 1;
            int user_id = 0; ;
            string password = null;
            string rePassword = null;
            string name = null;
            int departID = 0;
            string sex  = "";
            if (lookUpEdit1.EditValue != null)
            { 
             departID =com.ToInt(lookUpEdit1.EditValue);
            }
           
            int []age = {0} ;
            if (textBox1.Text == null || textBox1.Text.Equals(""))
            {
                MessageBox.Show("用户名不能为空！");
                validRes = 0;
            }
            else {
                user_id = com.ToInt(textBox1.Text);
            }
            if (textBox2.Text.TrimEnd() == null || textBox2.Text.Equals(""))
            {
                MessageBox.Show("密码不能为空！");
                validRes = 0;
            }
            else
            {
                password = textBox2.Text;
                
            }
            if (textBox3.Text.TrimEnd() == null|| textBox3.Text.Equals(""))
            {
                MessageBox.Show("确认密码不能为空！");
                validRes = 0;
            } else
            {
                rePassword = textBox3.Text;
               
            }
            if (textBox3.Text.TrimEnd() != textBox2.Text.TrimEnd())
            {
                validRes = 0;
                MessageBox.Show("密码和确认密码不一致！");
            }
            if (textBox4.Text.TrimEnd() == null|| textBox4.Text.Equals(""))
            {
                MessageBox.Show("姓名不能为空！");
                validRes = 0;
            } else
            {
                name = textBox4.Text;
            }
          
            if (departID==0)
            {
                MessageBox.Show("部门名称不能为空！");
                validRes = 0;
            }
            if (imageComboBoxEdit1.EditValue == null)
            {
                MessageBox.Show("性别不能为空！");
                validRes = 0;
            }
            else
            {
                sex = imageComboBoxEdit1.EditValue.ToString();
            }
            
            if (textBox5.Text.TrimEnd() == null || textBox5.Text.Equals(""))
            {
                MessageBox.Show("年龄不能为空！");
                validRes = 0;
            }
            else
            {
                age[0] = com.ToInt(textBox5.Text.Trim());
                
            }

            if (validRes == 1)
            {
                Users user = new Users();
                user.user_id = user_id;
                user.password = password;
                user.name = name;
                user.dep_id = departID;
                user.sex = sex;
                user.age = age[0];
                //控件如何获取部门ID
                int res = new UserBLL().AddUser(user);
                MessageBox.Show("添加成功！");
            }
            else
            {
                MessageBox.Show("添加失败！");
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            List<Depart> departList = new DepartBLL().GetALLDepart();
            try
            {
                //部门下拉框
                lookUpEdit1.ItemIndex = 0;
                lookUpEdit1.Properties.NullText = "请选择部门";
                // lookUpEdit1.EditValue = "id";
                lookUpEdit1.Properties.ValueMember = "id";//绑定lookupEdit的值，获取该值时使用 lookUpEdit1.EditValue
                lookUpEdit1.Properties.DisplayMember = "name";
                lookUpEdit1.Properties.DataSource = departList;
                lookUpEdit1.Properties.DropDownRows = departList.Count;
                //性别下拉框
                List<Sex> sexList = new List<Sex>();
                Sex sex1 = new Sex();
                Sex sex2 = new Sex();
                sex1.sex = "男";
                sex2.sex = "女";
                sexList.Add(sex1);
                sexList.Add(sex2);
                ImageComboBoxItem item1 = new ImageComboBoxItem();
                ImageComboBoxItem item2 = new ImageComboBoxItem();
                item1.Value = "男";
                item1.ImageIndex = -1;
                item1.Description = sex1.sex;
                item2.Value = "女";
                item2.Description = sex2.sex;
                imageComboBoxEdit1.Properties.Items.Add(item1);
                imageComboBoxEdit1.Properties.Items.Add(item2);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            // lookUpEdit1.Properties.NullText= null;
            lookUpEdit1.Properties.DataSource = departList;
            lookUpEdit1.Properties.DropDownRows = departList.Count;
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //IsNumber是判断输入的是否是数字
            //char8是退格键的键值，可语序用户退格的个数
            if (!Char.IsNumber(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;

            }
        }

        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
