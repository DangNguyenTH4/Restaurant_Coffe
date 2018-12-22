using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;
        private MenuDAO() { }
        public static MenuDAO Instance
        {
            get
            {
                if (instance == null) instance = new MenuDAO();
                return instance;
            }
        }
        public List<Menu> GetListMenuByTable(int idTable)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "select f.name,bi.count,f.price,f.price*bi.count as totalPrice " +
                "from dbo.Bill as b, dbo.BillInfo as bi, dbo.Food as f " +
                "where  b.id = bi.idBill and bi.idFood=f.id and b.statuss=0 and b.idTable= " + idTable;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }


            return listMenu;
        }
    }
}
