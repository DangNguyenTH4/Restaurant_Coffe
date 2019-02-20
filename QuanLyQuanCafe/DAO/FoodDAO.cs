using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class FoodDAO
    {
        private FoodDAO() { }
        private static FoodDAO instance;
        public static FoodDAO Instance
        {
            get
            {
                if (instance == null) instance = new FoodDAO();
                return instance;
            }
        }
        public List<Food> GetFoodsByCategoryID(int idCategory)
        {
            List<Food> listFood = new List<Food>();
            string query = "select * from dbo.Food where idCategory = " + idCategory;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }

            return listFood;
        }
        public List<Food> GetListFood()
        {
            List<Food> listFood = new List<Food>();
            string query = "select * from dbo.Food";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }

            return listFood;
        }
        public List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = new List<Food>();
            string query = string.Format("select * from dbo.Food where dbo.fuConvertToUnsignl(name) like N'%' + dbo.fuConvertToUnsignl(N'{0}')+'%'", name);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                listFood.Add(food);
            }
            return listFood;
        }
        public bool InsertFood(string name, int idCategory, float price)
        {
            string query = string.Format("insert dbo.Food (name, idCategory, price) values(N'{0}',{1},{2})", name, idCategory, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateFood(int idFood, string name, int idCategory, float price)
        {
            string query = string.Format("update dbo.Food set name = N'{0}', idCategory = {1}, price = {2} where id = {3}", name, idCategory, price, idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = string.Format("delete dbo.Food where id = " + idFood);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
