using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Category
    {
        private int iD;
        private string name;

        public DTO_Category(int id, string name)
        {
            this.name = name;
            this.iD = id;
        }

        public DTO_Category() { }

        public int ID { get => iD; set => iD = value; }

        public string Name { get => name; set => name = value; }
    }
}
