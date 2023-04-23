using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaFe
{
    public partial class fRegister : Form
    {
        BUS_Account account = new BUS_Account();
        public fRegister()
        {
            InitializeComponent();
        }
        
        private void btnDangKi_Click(object sender, EventArgs e)
        {
            if(!account.CheckLengthPassword(txtMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 kí tự");
            }
            else
            {
                if (account.CheckConfirmPassword(txtMatKhau.Text,txtNhapLaiMatKhau.Text))
                {
                    if (!account.CheckExistsAccount(txtTenDangNhap.Text))
                    {
                        string taikhoan = txtTenDangNhap.Text;
                        string matkhau = txtMatKhau.Text;
                        matkhau = SecurityUtils.SaltedHash(matkhau);

                        account.InsertAccount(taikhoan, matkhau);
                        MessageBox.Show("Bạn đã đăng kí tài khoản thành công");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản đã tồn tại");
                    }
                    
                }
                else
                {
                    MessageBox.Show("Mật khẩu không khớp");
                    txtNhapLaiMatKhau.Focus();
                }
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
