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
    }
}
