using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Category
    {
        private int iD;
        private string name;

        public Category(int iD,string name)
        {
            this.iD = iD;
            this.name = name;
        }
        public Category(DataRow row)
        {
            this.iD = (int)row["id"];
            this.name = row["name"].ToString();
        }

        public string Name { get => name; set => name = value; }
        public int ID { get => iD; set => iD = value; }
    }
}
