using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        private BillDAO() { }
        public static BillDAO Instance
        {
            get
            {
                if (instance == null) instance = new BillDAO();
                return instance;
            }
            set { instance = value; }
        }


        /// <summary>
        /// Sử dụng ExecureQuerey
        /// Thành Công : Return Billid,
        /// Thất bại : return -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUnCheckBillIdByTableID(int idTable)
        {
            string query = "select * from dbo.Bill where  idTable =" + idTable + " and statuss = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        /// <summary>
        /// Sử dụng ExecureScale trả về ô đầu tiên (Dòng đầu, cột đầu).
        /// Thành công : idBill
        /// Thất bại : -1 
        /// </summary>
        /// <param name="idTable"></param>
        /// <returns></returns>
        public int GetUnCheckBillIdByTableIDUseScala(int idTable)
        {
            string query = "select * from dbo.Bill where  idTable =" + idTable + " and status = 1;";
            object idBill = DataProvider.Instance.ExecuteScala(query);
            if (idBill != null) return (int)idBill;
            return -1;
        }
        
    }
}
