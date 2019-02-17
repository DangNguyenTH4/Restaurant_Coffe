using QuanLyQuanCafe.DAO;
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
        public fAdmin()
        {
            InitializeComponent();
            Load1();
        }

        #region method
        void Load1()
        {
            LoadListBillByDate(dtpkFrontDate.Value, dtpkToDate.Value);
            LoadDateTimePickerBill();
            LoadListFood();
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
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
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


    }
}
