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
        BindingSource foodList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            Load1();
        }

        #region method
        void Load1()
        {
            dtgvFood.DataSource = foodList;

            LoadListBillByDate(dtpkFrontDate.Value, dtpkToDate.Value);
            LoadDateTimePickerBill();
            LoadListFood();
            LoadCategoryIntoCbb(cbbFoodCategory);
            AddFoodBinding();
            
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
            txtFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name"));
            txtFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID"));
            nmrFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Price"));
        }
        void LoadCategoryIntoCbb(ComboBox cbb)
        {
            cbb.DataSource = CategoryDAO.Instance.GetListCategory();
            cbb.DisplayMember = "Name";     
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
        #endregion

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private void txtFoodID_TextChanged(object sender, EventArgs e)
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
    }
}
