using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_Account
    {
        private string taiKhoan;
        private string matKhau;
        private string quyenHan;

        public string QuyenHan { get => quyenHan; set => quyenHan = value; }

        public string TaiKhoan { get => taiKhoan; set => taiKhoan = value; }

        public string MatKhau { get => matKhau; set => matKhau = value; }
    }
}
