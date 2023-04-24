using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Report
    {
        private int stt;
        private string name;
        private int price;
        private int count;
        private int tt;

        public DTO_Report(int stt, string name, int price, int count, int tt)
        {
            Stt = stt;
            Name = name;
            Price = price;
            Count = count;
            Tt = tt;
            
        }

        public int Stt { get => stt; set => stt = value; }

        public string Name { get => name; set => name = value; }

        public int Price { get => price; set => price = value; }

        public int Count { get => count; set => count = value; }

        public int Tt { get => tt; set => tt = value; }
    }
}
