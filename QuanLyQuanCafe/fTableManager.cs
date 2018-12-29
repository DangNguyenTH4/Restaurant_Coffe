using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            LoadTable();
            LoadCategory();
        }

        #region Method
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tbList = TableDAO.Instace.LoadTableList();
            foreach(Table item in tbList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Statuss;
                btn.Click += Btn_Click;
                btn.Tag = item;

                switch (item.Statuss)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }

        }

        void ShowBill(int idTable)
        {
            lsvBill.Items.Clear();
            List<DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(idTable);

            float totalPriceBill = 0;


            foreach(DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPriceBill += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }

 //           CultureInfo culture = new CultureInfo("vn-VN");
            CultureInfo culture = new CultureInfo("vi-VN");

            //Thread.CurrentThread.CurrentCulture = culture; //Setting lại chỉ thread này thay đổi format 

            txtTotalPriceBill.Text = totalPriceBill.ToString("c",culture);
        }
        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbbCategory.DataSource = listCategory;
            cbbCategory.DisplayMember = "Name";

        }
        void LoadFoodListByCategory(int idCategory)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodsByCategoryID(idCategory);
            cbbFood.DataSource = listFood;
            cbbFood.DisplayMember = "Name";
        }

        #endregion

        #region Event

        private void Btn_Click(object sender, EventArgs e)
        {
            int tbId = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tbId);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountFile f = new fAccountFile();
            f.ShowDialog();

        }
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();

        }
        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedItem == null)  return;
            Category selected = cbb.SelectedItem as Category;
            int id = selected.ID;

            LoadFoodListByCategory(id);
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableID(table.ID);
            int foodID = (cbbFood.SelectedItem as Food).ID;
            int count = (int)nmrFoodCount.Value;

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.ID);
            LoadTable();

        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableID(table.ID);
            int discount = (int)nmrDiscount.Value;
            if(idBill!=-1)
            {
                if(MessageBox.Show("Bạn muốn thanh toán bàn " + table.ID,"Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }
        #endregion


    }
}
