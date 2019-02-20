using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class fAdmin : Form
    {
        Account loginAccount;
        Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; }
        }
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();      

        public fAdmin(Account loginAccount)
        {
            InitializeComponent();
            LoginAccount = loginAccount;
            Load1();
        }

        #region method
        List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();
            list = FoodDAO.Instance.SearchFoodByName(name);
            return list;
        }
        void Load1()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;   

            LoadDateTimePickerBill();
            LoadListFood();
            LoadListBillByDate(dtpkFrontDate.Value, dtpkToDate.Value);
            LoaddAccount();
            LoadCategoryIntoCbb(cbbFoodCategory);
            AddFoodBinding();
            AddAccountBinding();
            
        }
        void LoaddAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }
        void AddAccountBinding()
        {
            txtUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource,"DisplayName", true, DataSourceUpdateMode.Never));
            txtAccountType.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetListBillByDate(checkIn, checkOut);
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFrontDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFrontDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        void AddFoodBinding()
        {
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name",true,DataSourceUpdateMode.Never));
            txtFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmrFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoCbb(ComboBox cbb)
        {
            cbb.DataSource = CategoryDAO.Instance.GetListCategory();
            cbb.DisplayMember = "Name";     
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm thành công.");
                LoaddAccount();
            }
            else MessageBox.Show("Thêm thất bại.");
        }
        void UpdateAccount(string userName,string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Sửa thành công.");
                LoaddAccount();

            }
            else MessageBox.Show("Sửa thất bại.");
        }
        void DeleteAccount(string userName)
        {
            if (userName == LoginAccount.UserName)
            {
                MessageBox.Show("Bạn không thể xóa chính mình.");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa thành công.");
                LoaddAccount();
            }
        }
        void ResetPassword(string userName)
        {
            if (AccountDAO.Instance.ResetPassword(userName))
            {
                MessageBox.Show("Mật khẩu mặc định : 1");
            }
            else MessageBox.Show("Lỗi khi đặt lại");
        }
        #endregion

        #region event
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFrontDate.Value, dtpkToDate.Value);
        }
        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            int categoryId = (cbbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmrFoodPrice.Value;
            if (FoodDAO.Instance.InsertFood(name, categoryId, price))
            {
                MessageBox.Show("Thêm món thành công.");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else MessageBox.Show("Có lỗi khi thêm món.");
        }
        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txtFoodID.Text);
            string name = txtFoodName.Text;
            int categoryId = (cbbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmrFoodPrice.Value;
            if (FoodDAO.Instance.UpdateFood(idFood, name, categoryId, price))
            {
                MessageBox.Show("Sửa món thành công.");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else MessageBox.Show("Có lỗi khi sửa món.");
        }
        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int idFood = Convert.ToInt32(txtFoodID.Text);
            if (FoodDAO.Instance.DeleteFood(idFood))
            {
                MessageBox.Show("Xóa món thành công.");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else MessageBox.Show("Có lỗi khi xóa món.");
        }
        private void fAdmin_Load(object sender, EventArgs e)
        {

        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txtSearchFood.Text);
        }
        private void txtFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id1 = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryId"].Value;
                    Category cate = CategoryDAO.Instance.GetCategoryByID(id1);

                    //cbbFoodCategory.SelectedItem = cate;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbbFoodCategory.Items)
                    {
                        if (item.ID == cate.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }
                    cbbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }
        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoaddAccount();
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }
        private void btnAddAcount_Click(object sender, EventArgs e)
        {

            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = Convert.ToInt32(txtAccountType.Text);
            AddAccount(userName, displayName, type);
        }
        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string displayName = txtDisplayName.Text;
            int type = Convert.ToInt32(txtAccountType.Text);
            UpdateAccount(userName, displayName, type);
           
        }
        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            DeleteAccount(userName);
        }
        private void btnReSetPassWord_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            ResetPassword(userName);
        }
        #endregion


    }
}
