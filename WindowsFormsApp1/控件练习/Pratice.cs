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

namespace WindowsFormsApp1.WinForm
{
    public partial class Pratice : Form
    {
        public Pratice()
        {
            InitializeComponent();
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Pratice_Load(object sender, EventArgs e)
        {
            lookUpEdit1.Properties.ValueMember = "id";//实际要用的字段，相当于EditValues
            lookUpEdit1.Properties.DisplayMember = "name";
            lookUpEdit1.ItemIndex = 0;
            List<Depart> departList = new DepartBLL().GetALLDepart();
            lookUpEdit1.Properties.DataSource = departList;
            PraticeControl.DataSource = departList;
            repositoryItemLookUpEdit1.DataSource = departList;
            repositoryItemLookUpEdit1.ValueMember = "id";
            repositoryItemLookUpEdit1.DisplayMember = "name";
            //repositoryItemLookUpEdit1 =
        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //ButtomEdit 多个按钮 如何判断点击的是哪一个按钮
        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //通过kind的值进行判断,也可通过caption的值进行判断
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Close)
            {
                MessageBox.Show("关闭Close！");
            }
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.OK)
            {
                MessageBox.Show("OK！");
            }
            if (e.Button.Caption =="Plus" )
            {
                MessageBox.Show("Plus！");
            }
        }

        private void PraticeControl_Click(object sender, EventArgs e)
        {

        }
    }
}
