using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillInfoDAO
    {
        private BillInfoDAO() { }
        private static BillInfoDAO instance;
        public static  BillInfoDAO Instance
        {
            get
            {
                if (instance == null) instance = new BillInfoDAO();
                return instance;
            }
            private set { instance = value; }
        }
        public List<BillInfo> GetListBillInFo(int idBill)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            string query = "Select * from dbo.BillInfo where idBill = " + idBill;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood,int count)
        {
            string query = "exec USP_InsertBillInfo @idBill , @idFood , @count";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBill, idFood, count });

        }
    }
}
