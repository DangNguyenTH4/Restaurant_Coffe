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
    }
}
