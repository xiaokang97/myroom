using DevExpress.XtraGrid.Views.Grid;
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
using WindowsFormsApp1.UserList;
using WindowsFormsApp1.WinForm;

namespace WindowsFormsApp1.控件练习
{
    public partial class ViewAndControl : Form
    {
        public ViewAndControl()
        {
            InitializeComponent();
        }

        private void ViewAndControl_Load(object sender, EventArgs e)
        {

            UserOperationsvc userOP = new UserOperationsvc();//实例化WCF接口
            DataTable dt = userOP.ShowUser();
          //  List<Users> userList = new List<Users>();
           // userList = new UserBLL().GetAllUserNoQuery();
            gridControl1.DataSource = dt;
           // gridView1.SetRowCellValue(0, gridView1.Columns[0], "123");
            gridView1.SelectRows(0, 2);
            List<Depart> departList = new DepartBLL().GetALLDepart();
            repositoryItemLookUpEdit1.DataSource = departList;
            this.gridView1.IndicatorWidth = 40;//设置行号的宽度
            this.gridView1.BestFitColumns();//自动调整所有字段的宽度
            //调整某列字段宽度
            //this.gridView1.Columns[n].BestFit();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Pratice pra = new Pratice();
            pra.Show();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void gridControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "dep_id")
            {
                switch(e.Value.ToString().Trim())
                {
                    case "1":
                        e.DisplayText = "市场部";
                    break;
                    case "2":
                        e.DisplayText = "技术部";
                    break;
                    default:
                        e.DisplayText = "";
                        break;

                }
            }
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //MessageBox.Show("不可修改!");
            this.gridView1.PostEditor();    
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //MessageBox.Show("请勿编辑!");
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            int selectID = e.FocusedRowHandle;//获取选中焦点的行数
           // MessageBox.Show(" " + selectID);//默认会选中第一行，会弹出0
            int selectedRow = gridView1.GetSelectedRows()[0];//获取选中行的行数，默认会被隐藏
            if (selectedRow >= 0)
            {
               // MessageBox.Show(this.gridView1.GetRowCellValue(selectedRow, "user_id").ToString());
            }
            
        }

        private void gridView1_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            string fileName = e.FocusedColumn.FieldName;//得到该列的字段名
            string a = e.FocusedColumn.ColumnType.ToString();//得到该列的数据类型
         //   MessageBox.Show(fileName);
          //  MessageBox.Show(a);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            ////this.Hide();
            //Form2 form2 = new Form2();//Form2为主窗体
            //form2.Show();

        }
        //自定义的表格行编辑
        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "dep_id") 
              {    
                    e.RepositoryItem = repositoryItemLookUpEdit1;//编辑室选择下拉框
                //通过自定义行表格编辑事件可表格的操作多样化。
            }

        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle != view.FocusedRowHandle)
            {//鼠标没有点击的地方，设置为金黄色
             //e.Appearance.BackColor = Color.PaleGoldenrod;
            }
        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            //MessageBox.Show("点击单元格");
            //你的Grid处于可编辑状态，左键点击默认为“进入编辑”。
           // 将GridView的OptionBehavior -Editable设置为false后左键可触发。
        }
        //可分别设置每个单元格的样式
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
             GridView  view  =  sender  as  GridView ;
            // int id  = Int32.Parse(view.GetRowCellDisplayText(e.RowHandle ,gridView1.Columns["user_id"]));
            if (e.RowHandle > 0)
            {
                string name = this.gridView1.Columns["name"].ToString();
                //string sex = Convert.ToString(this.gridView1.GetRowCellValue(i, gridView1.Columns["sex"]));
                if (name != null)
                {
                    e.Appearance.ForeColor = Color.Red;
                }
             
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //this.gridView1.BestFitColumns();//自动调整所有字段的宽度
            //调整某列字段宽度
            //this.gridView1.Columns[n].BestFit();
           
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;//让文字居中
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();//显示行号
            else
                e.Info.DisplayText = gridView1.RowCount.ToString();//显示总数
        }
    }
}
