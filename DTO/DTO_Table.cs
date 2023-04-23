using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Table
    {
        private int id;
        private string name;
        private string status;
        public DTO_Table(int id, string name, string stt)
        { 
            this.id = id;
            this.name= name;
            this.status = stt;
        }
        public DTO_Table() { }
        public int Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }

        public string Status { get => status; set => status = value; }
    }
}
