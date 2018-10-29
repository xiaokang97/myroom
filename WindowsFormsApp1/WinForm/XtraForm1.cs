using DevExpress.XtraEditors.Controls;
using System;
using System.Collections;
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
    public partial class XtraForm1 : Form
    {
        Common com = new Common();
        int PageSize = 5;
        //修改保存的对象。
        List<Users> saveUserList = new List<Users>();
        public XtraForm1()
        {
            InitializeComponent();
        }
       
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            try
            {
                //部门下拉框
                List<Depart> departList = new DepartBLL().GetALLDepart();
                lookUpEdit1.ItemIndex = 0;
               // lookUpEdit1.EditValue = "0";
                lookUpEdit1.Properties.ValueMember = "id";//绑定lookupEdit的值，获取该值时使用 lookUpEdit1.EditValue
                lookUpEdit1.Properties.DisplayMember = "name";
                lookUpEdit1.Properties.DataSource = departList;
                lookUpEdit1.Properties.DropDownRows = departList.Count;
                repositoryItemLookUpEdit1.DataSource = departList;
                repositoryItemLookUpEdit1.ValueMember = "id";
                repositoryItemLookUpEdit1.DisplayMember = "name";
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
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
            ImageComboBoxItem item3 = new ImageComboBoxItem();
            item1.Value = "男";
            item1.ImageIndex = -1;
            item1.Description = sex1.sex;
            item2.Value = "女";
            item2.Description = sex2.sex;
            item3.Value = "";
            item3.Description = "全部";
            imageComboBoxEdit1.Properties.Items.Add(item1);
            imageComboBoxEdit1.Properties.Items.Add(item2);
            imageComboBoxEdit1.Properties.Items.Add(item3);
            imageComboBoxEdit1.SelectedText = "";
            imageComboBoxEdit1.EditValue = null;

            repositoryItemImageComboBox1.Items.Add(item1);
            repositoryItemImageComboBox1.Items.Add(item2);
            // //显示所有用户
            Users users = new Users();
            UserSearch userSearch = new UserSearch();
            userSearch.page_number = 1;
            ArrayList queryList = ReturnQueryList(userSearch);
            int count =com.ToInt(queryList[0]);
            //PageCount.Text显示总页数
            if (count % PageSize == 0)
            {
                PageCount.Text = (count / PageSize).ToString();
            }
            else
            {
                PageCount.Text = ((count / PageSize) + 1).ToString();
            }
            if (count < PageSize)
            {
                CurrentPageSize.Text = count.ToString();
            }
            else
            {
                CurrentPageSize.Text = PageSize.ToString();
            }
            List<Users> userList = (List<Users>)queryList[1];
            CurrentPage.Text = "1";
            PrePage.Enabled = false;
            //GridView数据绑定
            gridControl1.DataSource = userList;


        }
        private void Select_Click(object sender, EventArgs e)
        {
        
            UserSearch userSearch = new UserSearch();
            int pn = 1;
            //页数 PageCount.Text 为总页数
            //总数
            ArrayList queryList = ReturnQueryList(userSearch);
            int count = com.ToInt(queryList[0]);
            //获取页数
            PageCount.Text = GetPage(count, PageSize).ToString();
            if (CurrentPage.Text != null && CurrentPage.Text != " ")
            {
    
                //判断当前页数和总页数的大小
                if (com.ToInt(CurrentPage.Text) < com.ToInt(PageCount.Text))

                {
                    if (com.ToInt(CurrentPage.Text) == 0)
                    {
                        CurrentPage.Text = pn.ToString();
                        CurrentPageSize.Text = PageSize.ToString();
                    }
                    else
                    { 
                    
                    CurrentPageSize.Text = PageSize.ToString();
                    }
                }
                else if (com.ToInt(CurrentPage.Text) == 1 && com.ToInt(PageCount.Text) == 1)
                {
                    pn = com.ToInt(CurrentPage.Text);
                    CurrentPage.Text = pn.ToString();
                    if(count % PageSize==0)
                    {
                        CurrentPageSize.Text = com.ToString(PageSize);
                    }
                    else
                    {
                        CurrentPageSize.Text = (count % PageSize).ToString();
                    }
                }
                else
                {
                    pn = com.ToInt(PageCount.Text);
                    CurrentPage.Text = pn.ToString();
                    CurrentPageSize.Text = (count % PageSize).ToString();
                }
            }else
            {
                pn = 1;
            }
            userSearch.page_number = pn;
            ArrayList queryList1 = ReturnQueryList(userSearch);
            List<Users> userList = (List<Users>)(queryList1[1]);
            ButtonState(pn, com.ToInt(PageCount.Text));//上一页下一页的状态
            //GridView数据绑定
            gridControl1.DataSource = userList;

        }

        private void Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // this.Hide();
            Form2 form2 = new Form2();//Form2为主窗体
            form2.Show();
        }
        private void Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int[] rownumber = this.gridView1.GetSelectedRows();//获取选中行号；
            int res = 0;

            //DataRow row = this.gridView1.GetDataRow(rownumber[0]);
            for (int i = 0; i < saveUserList.Count; i++)
            {
                Users user = new Users();
                user = saveUserList[i];
                UserBLL userBLL = new UserBLL();
               
                res = userBLL.UpdateUser(user);
            }
            if (res < 0)
            {
                MessageBox.Show("修改失败！");

            }
            this.gridView1.PostEditor();
        }
        private void Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////int[] rownumber = this.gridView1.GetSelectedRows();//获取选中行号；
            //// DataRow row = this.gridView1.GetDataRow(rownumber[1]);
            //int res = 0;
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    //DataRow row = gridView1.GetDataRow(i);
            //    ////string value = gridControl1.Select().ToString();
            //    //if (value.Equals("True"))
            //    //{
            //    //     int userid = Int32.Parse(row["user_id"].ToString());
            //    //    UserBLL userBLL = new UserBLL();
            //    //    i =userBLL.DeleteUser(userid);
            //    //   // 例如获取指定字段的值，row[i][“字段名”]即为对应字段名
            //    //}
            //}
            List<int> strList = new List<int>();

            for (int i = 0; i < this.gridView1.RowCount; i++)

            {

                if (this.gridView1.IsRowSelected(i))

                {

                    strList.Add(Convert.ToInt32(this.gridView1.GetRowCellValue(i, gridView1.Columns["user_id"])));

                }

            }
            int res = 0;

            for (int k = 0; k < strList.Count; k++)
            {
                UserBLL userBLL = new UserBLL();
                res = userBLL.DeleteUser(strList[k]);
            }
            if (res > 0)
            {
                MessageBox.Show("删除成功！");
            }
            else
            {
                MessageBox.Show("删除失败！");
            }

        }
        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
        //重置按钮
        private void Reset_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            lookUpEdit1.EditValue = 0;
            imageComboBoxEdit1.EditValue = "";

           // List<Users> userList = new UserBLL().GetAllUser(textBox1.Text, 0, imageComboBoxEdit1.EditValue.ToString(), 0, 1000);
            //GridView数据绑定
           // gridControl1.DataSource = userList;

        }

        private void imageComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

   

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorCountItem_Click(object sender, EventArgs e)
        {

        }

        private void FirstPage_Click(object sender, EventArgs e)
        {
            UserSearch userSearch = new UserSearch();
            userSearch.page_number = 1;
            //在gridcontrol显示所有用户信息
            ArrayList queryList = ReturnQueryList(userSearch);
            //总数
            int count = (int)queryList[0];
            //页数 PageCount.Text 为总页数
            //CurrentPageSize显示该页的总数
            //CurrentPage.Text 为当前页
            //获取页数
            PageCount.Text = GetPage(count, PageSize).ToString();
            if (com.ToInt(PageCount.Text) > 1)
            {
                CurrentPageSize.Text = PageSize.ToString();
            }
            else if (count < PageSize)
            {

                CurrentPageSize.Text = (count % PageSize).ToString();
            }
            else
            {
                CurrentPageSize.Text = "5";
            }
            List<Users> userList = (List<Users>)queryList[1];
            CurrentPage.Text = "1";
            int CurPage = com.ToInt(CurrentPage.Text);
            //this.CurrentPageSize.Text = PageSize.ToString();
            ButtonState(CurPage, com.ToInt(PageCount.Text));//上一页下一页的状态
            //GridView数据绑定
            gridControl1.DataSource = userList;
            // List<Users> userList = new UserBLL().GetAllUser(name, departID, sex, MinAge, MaxAge);
            //GridView数据绑定
            ////   gridControl1.DataSource = userList;

        }

        private void LastPage_Click(object sender, EventArgs e)
        {
            UserSearch userSearch = new UserSearch();
            int pageNumber = 1;
            //在gridcontrol显示所有用户信息
            ArrayList queryList = ReturnQueryList(userSearch);
            int count = (int)queryList[0];
            //总数
            //页数 PageCount.Text 为总页数
            //CurrentPageSize显示该页的总数
            //CurrentPage.Text 为当前页
            //获取页数
            PageCount.Text = GetPage(count, PageSize).ToString();
            pageNumber = com.ToInt(PageCount.Text);//得到页数
            userSearch.page_number = pageNumber;//得到页数后才能得到用户列表
            ArrayList queryList1 = ReturnQueryList(userSearch);
            if (count % PageSize == 0)
            {
               
                CurrentPageSize.Text = "5";
            }
            else
            { 
               CurrentPageSize.Text = (count % PageSize).ToString();
            }
            List<Users> userList = (List<Users>)queryList1[1];
            CurrentPage.Text = PageCount.Text.ToString();
            //this.CurrentPageSize.Text = PageSize.ToString();
            ButtonState(pageNumber, com.ToInt(PageCount.Text));//上一页下一页的状态
            //GridView数据绑定
            gridControl1.DataSource = userList;

        }

        private void PrePage_Click(object sender, EventArgs e)
        {
            //页数 PageCount.Text 为总页数
            //CurrentPageSize显示该页的总数
            //CurrentPage.Text 为当前页
            UserSearch userSearch = new UserSearch();
            int pageNumber = com.ToInt(CurrentPage.Text) - 1;
            CurrentPageSize.Text = PageSize.ToString();
            userSearch.page_number = pageNumber;//填充userSearch的页数
            //在gridcontrol显示所有用户信息
            ArrayList queryList = ReturnQueryList(userSearch);
            //总数
            int count = (int)queryList[0];
            List<Users> userList = (List<Users>)queryList[1];
                //获取页数
            PageCount.Text = GetPage(count, PageSize).ToString();
            CurrentPage.Text = pageNumber.ToString();
            //this.CurrentPageSize.Text = PageSize.ToString();
            ButtonState(pageNumber, com.ToInt(PageCount.Text));//上一页下一页的状态
            //GridView数据绑定
            gridControl1.DataSource = userList;
            // List<Users> userList = new UserBLL().GetAllUser(name, departID, sex, MinAge, MaxAge);
            //GridView数据绑定
            ////   gridControl1.DataSource = userList;
        }

        private void NextPage_Click(object sender, EventArgs e)
        {
            //在gridcontrol显示所有用户信息
            UserSearch userSearch = new UserSearch();
            //页数 PageCount.Text 为总页数
            //CurrentPageSize显示该页的总数
            //CurrentPage.Text 为当前页
           
            int pageNumber = Int32.Parse(CurrentPage.Text) + 1;
            userSearch.page_number = pageNumber;
            ArrayList queryList = ReturnQueryList(userSearch);//List集合返回的是页数和userList
            //总数
            int count = (int)queryList[0];
            if (pageNumber ==Int32.Parse(PageCount.Text))
            {
                CurrentPageSize.Text = (count % PageSize).ToString();
            }
            else
            { 
                 CurrentPageSize.Text = PageSize.ToString();
            } 
            //获取页数
            PageCount.Text = GetPage(count, PageSize).ToString();
            List<Users> userList = (List<Users>)queryList[1];
            CurrentPage.Text = pageNumber.ToString();
            //this.CurrentPageSize.Text = PageSize.ToString();
            ButtonState(pageNumber, Int32.Parse(PageCount.Text));//上一页下一页的状态
            //GridView数据绑定
            gridControl1.DataSource = userList;
        }

        private void CurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar !=8 && !Char.IsDigit(e.KeyChar))//如果不是输入数字就不让输入
            {
                e.Handled = true;

              }

        }
        //查询条件的封装
        public ArrayList ReturnQueryList(UserSearch userSearch)
        {
     
            ArrayList queryList = new ArrayList();
            string name = textBox1.Text;
            int departID;
            if (lookUpEdit1.EditValue.ToString() != "null")
            {
                departID = com.ToInt(lookUpEdit1.EditValue.ToString());
            }
            else
            {
                departID = 0;
            }
            string sex = "";
            if (imageComboBoxEdit1.EditValue != null)
            {
                sex = imageComboBoxEdit1.EditValue.ToString();
            }
            else
            {
                sex = "";
            }
            string age = this.textBox2.Text.Trim();
            string[] ages = age.Split('-');
            int [] agesInt = { 0,1000};
            if(ages[0]!=null && !ages[0].Equals(""))
            { 
            if (ages.Length==1)
            { 
            agesInt[0] = com.ToInt(ages[0]);
            agesInt[1] = com.ToInt(ages[0]);
                }
            else
            {
                    agesInt[0] = com.ToInt(ages[0]);
                    agesInt[1] = com.ToInt(ages[1]);
            }
            }
            userSearch.name = name;
            userSearch.dep_id = departID;
            userSearch.sex = sex;
            userSearch.page_size = PageSize;
            int count = new UserBLL().GetPageCount(userSearch,agesInt);//获取查询到的总条数
            List<Users> userList = new UserBLL().GetAllUser(userSearch, agesInt);
            queryList.Add(count);//0装总数
            queryList.Add(userList);//1装对象集合
   
            return queryList;

        }
        public void ButtonState(int pageNumber,int countPage)
        {
            if(countPage>1)
            { 
            if (pageNumber == 1)
            {
                    PrePage.Enabled = false;
                    NextPage.Enabled = true;
                }
                if (pageNumber == countPage)
                {
                    NextPage.Enabled = false;
                    PrePage.Enabled = true;
                }
            }
            else
            {
                NextPage.Enabled = false;
                PrePage.Enabled = false;
            }
        }
        public int GetPage(int count, int PageSize)
        {
            int i = 0;
            if (count % PageSize == 0)
            {
              i  = count / PageSize;

            }
            else
            {
              i = count / PageSize + 1;

            }
            return i;
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Users user = (Users)this.gridView1.GetRow(e.RowHandle);
            saveUserList.Add(user);
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {

        }
        //当单元格被修改时触发的事件。记录被修改过的数据


        //获取数据源
        //public List<Users> GetDataSoursce()
        //{

        //}
    }
}

