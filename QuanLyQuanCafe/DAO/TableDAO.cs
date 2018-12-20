using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instace;
        private TableDAO() { }
        public static int TableWidth = 100;
        public static int TableHeight = 100;

        internal static TableDAO Instace
        {
            get
            {
                if (instace == null)
                {
                    instace = new TableDAO();
                }
                return instace;
            }

            set => instace = value;
        }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();

            string query = "exec USP_GetTableList";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Table tb = new Table(item);
                tableList.Add(tb);
            }

            return tableList;
        }
    }
}
