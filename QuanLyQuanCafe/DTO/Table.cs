using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Table
    {
        private int iD;
        private string name;
        private readonly string statuss;
        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Statuss => statuss;
        public Table(int id,string name,string statuss)
        {
            this.iD = id;
            this.name = name;
            this.statuss = statuss;
        }
        public Table(DataRow row)
        {
            this.iD = (int)row["id"];
            this.name = row["name"].ToString();
            this.statuss = row["statuss"].ToString();
        }
    }
}
