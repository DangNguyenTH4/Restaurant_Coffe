﻿using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DAO
{
    public class CategoryDAO
    {
        private CategoryDAO() { }
        private static CategoryDAO instance;
        
        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null) instance = new CategoryDAO();
                return instance;
            }
        }

        public List<Category> GetListCategory()
        {
            List<Category> listCategory = new List<Category>();

            string query = "select * from FoodCategory";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                Category cate = new Category(item);
                listCategory.Add(cate);

            }

            return listCategory;
        }
    }
}
