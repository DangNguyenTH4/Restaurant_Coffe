using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Food
    {
        private int iD;
        private string name;
        private int categoryID;
        private float price;

        public Food(int iD,string name,int categoryID,float price)
        {

        }

        public Food(DataRow row)
        {
            this.iD = (int)row["id"];
            this.name = row["name"].ToString();
            this.categoryID = (int)row["idcategory"];
            this.price = (float)(Convert.ToDouble(row["price"]));
        }
        

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public float Price { get => price; set => price = value; }
    }
}
