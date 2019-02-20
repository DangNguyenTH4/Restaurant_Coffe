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
        private Account loginAccount;
        public Account LoginAccount
        {
            get
            {
                return loginAccount;
            }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount.Type);
            }
        }
        public fTableManager(Account loginAccount)
        {
            InitializeComponent();
            LoginAccount = loginAccount;
            LoadTable();
            LoadCategory();
        }

        Table focusTable;
        Button focusButton;

        

        #region Method
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }
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
            if (focusTable!=null)
            {
                if (focusTable.Statuss.Equals("Trống"))
                    focusButton.BackColor = Color.Aqua;
                else focusButton.BackColor = Color.LightPink;
            }

            focusTable = (sender as Button).Tag as Table;
            focusButton = sender as Button;
            int tbId = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tbId);
            (sender as Button).BackColor = Color.Red;

        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountFile f = new fAccountFile(LoginAccount);
            f.UpdateAccountt += f_UpdateAccount;
            f.ShowDialog();

        }

        private void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin(LoginAccount);
            f.InsertFood += F_InsertFood;
            f.DeleteFood += F_DeleteFood;
            f.UpdateFood += F_UpdateFood;
            f.ShowDialog();

        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListByCategory((cbbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag as Table !=null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListByCategory((cbbCategory.SelectedItem as Category).ID);
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListByCategory((cbbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag as Table != null)
                ShowBill((lsvBill.Tag as Table).ID);
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
            if (table == null)
                return;
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
        //private void button1_click(object sender, eventargs e)
        //{
        //    txttotalpricebill.text.tostring()
        //    string[] b = txttotalpricebill.text.split(',');
        //    float a = float.parse(b[0], system.globalization.cultureinfo.getcultureinfo("de-de").numberformat);
        //    messagebox.show(a.tostring());
        //}
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
                return;

            int idBill = BillDAO.Instance.GetUnCheckBillIdByTableID(table.ID);
            int discount = (int)nmrDiscount.Value;
            float totalPrice = float.Parse(txtTotalPriceBill.Text.Split(',')[0], CultureInfo.GetCultureInfo("de-DE").NumberFormat);
            float finalTotalPrice = totalPrice * (100 - discount) / 100;
            if (idBill!=-1)
            {
               
                if(MessageBox.Show("Bàn thanh toán: " + (table.ID + 1) + "\nTổng tiền: " + txtTotalPriceBill.Text + "\nDiscount: " + nmrDiscount.Value + "%\nSố tiền thanh toán: " + finalTotalPrice.ToString("c",new CultureInfo("vi-VN")),"Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount  ,finalTotalPrice );
                    ShowBill(table.ID);
                    LoadTable();
                    if (focusButton != null)
                    {
                        focusButton = null;
                        focusTable = null;
                        
                    }
                }
            }
        }

        #endregion

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {

        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
