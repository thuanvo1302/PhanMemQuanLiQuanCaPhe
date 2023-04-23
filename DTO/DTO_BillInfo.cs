using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_BillInfo
    {
        private int iD;
        private int billID;
        private int drinkID;
        private int count;

        public int ID { get => iD; set => iD = value; }

        public int BillID { get => billID; set => billID = value; }

        public int DrinkID { get => drinkID; set => drinkID = value; }

        public int Count { get => count; set => count = value; }
    }
}
