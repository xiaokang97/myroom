using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.控件练习
{
    public partial class GridCV : Form
    {
        public GridCV()
        {
            InitializeComponent();
        }

        private void GridCV_Load(object sender, EventArgs e)
        {
            List<Users> userList = new List<Users>();
            List<Depart> departList = new List<Depart>();
            userList = new UserBLL().GetAllUserNoQuery();
            ArrayList list = new ArrayList();
            list.Add(userList);
            list.Add(departList);
            gridControl1.DataSource = list[0];
             
            ////构建GridLevelNode并添加到LevelTree集合里面
            //var node = new GridLevelNode();
            //node.LevelTemplate = gridView2;
            //node.RelationName = "departList";//这里对应集合的属性名称
            //gridControl1.LevelTree.Nodes.AddRange(new GridLevelNode[]
            //{
            //    node
            //});
            //gridControl1.ViewCollection.Clear();
            //gridControl1.ViewCollection.AddRange(new GridView[] { gridView1,gridView2});
        }
    }
}
