using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    public class Bill
    {
        private int iD;
        private DateTime dateCheckIn;
        private DateTime? dateCheckOut;
        private int idTable;
        private int status;
        private int discount;

        public int ID { get => iD; set => iD = value; }
        public DateTime DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int IdTable { get => idTable; set => idTable = value; }
        public int Discount { get => discount; set => discount = value; }

        public Bill(int id, DateTime dateCheckIn, DateTime? dateCheckOut,int idtable,int status,int discount = 0)
        {
            this.iD = id;
            this.dateCheckIn = dateCheckIn;
            this.dateCheckOut = dateCheckOut;
            this.idTable = idtable;
            this.status = status;
            this.discount = discount;
        }
        public Bill(DataRow row)
        {
            this.iD = (int)row["id"];
            this.dateCheckIn = (DateTime)row["dateCheckIn"];
            var zz = row["dateCheckOut"];
            if (zz.ToString() != "")
            {
                this.dateCheckOut = (DateTime?)row["dateCheckOut"];
            }
            this.idTable = (int)row["idTable"];
            this.status = (int)row["statuss"];
            this.discount = (int)row["discount"];

            
        }
    }
}
