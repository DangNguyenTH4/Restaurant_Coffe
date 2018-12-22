using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DTO
{
    /// <summary>
    /// Field : ID, IDBill, IDFood, Count
    /// </summary>
    public class BillInfo
    {
        private int iD;
        private int idBill;
        private int idFood;
        private int count;

        public int ID { get => iD; set => iD = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }

        public BillInfo(int id,int idBill,int idFood,int count)
        {
            this.iD = id;
            this.idBill = idBill;
            this.idFood = idFood;
            this.count = count;
        }
        public BillInfo(DataRow row)
        {
            this.iD = (int)row["id"];
            this.idBill = (int)row["idBill"];
            this.idFood = (int)row["idFood"];
            this.count = (int)row["count"];
        }
    }
}
