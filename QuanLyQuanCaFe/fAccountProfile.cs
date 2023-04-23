using BUS;
using DAL;
using DTO;
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
    public partial class fAccountProfile : Form
    {
        private BUS_Account account = new BUS_Account();
        private string taikhoan;
        private string matkhau;

        public fAccountProfile(string tk, string mk)
        {
            InitializeComponent();
            taikhoan = tk;
            matkhau = mk;
        }

        void Enable(bool check)
        {
            foreach (Control ctrl in grbThongTinTaiKhoan.Controls)
            {
                ctrl.Enabled = check;
            }
        }

        void LoadInfo(string maukhau)
        {
            DTO_Account acc = account.LoadAccount(taikhoan);
            txtTenDangNhap.Text = acc.TaiKhoan;
            txtMatKhau.Text = maukhau;
            txtQuyenHan.Text = acc.QuyenHan;
        }

        private void fAccountProfile_Load(object sender, EventArgs e)
        {
            Enable(false);
            LoadInfo(matkhau);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.Text == "")
            {
                MessageBox.Show("Nhập mật khẩu mới của bạn");
            }
            else if (!account.CheckLengthPassword(txtMatKhauMoi.Text))
            {
                MessageBox.Show("Mật khẩu phải ít nhất 8 kí tự");
            }
            else
            {
                if (account.CheckConfirmPassword(txtMatKhauMoi.Text, txtNhapLaiMatKhau.Text))
                {
                    account.UpdateAccount(txtTenDangNhap.Text, SecurityUtils.SaltedHash(txtMatKhauMoi.Text));
                    MessageBox.Show("Cập nhật mật khẩu thành công");
                    LoadInfo(txtMatKhauMoi.Text);
                    txtMatKhauMoi.Clear();
                    txtNhapLaiMatKhau.Clear();
                }
                else
                {
                    MessageBox.Show("Nhập lại mật khẩu không khớp");
                }
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '\0')
            {
                btnHide_Eye.BringToFront();
                txtMatKhauMoi.PasswordChar = '*';
            }
        }

        private void btnHide_Eye_Click(object sender, EventArgs e)
        {
            if (txtMatKhauMoi.PasswordChar == '*')
            {
                btnEye.BringToFront();
                txtMatKhauMoi.PasswordChar = '\0';
            }
        }

        private void btn_Eye1_Click(object sender, EventArgs e)
        {
            if (txtNhapLaiMatKhau.PasswordChar == '\0')
            {
                btnHide_Eye1.BringToFront();
                txtNhapLaiMatKhau.PasswordChar = '*';
            }
        }

        private void btnHide_Eye1_Click(object sender, EventArgs e)
        {
            if (txtNhapLaiMatKhau.PasswordChar == '*')
            {
                btn_Eye1.BringToFront();
                txtNhapLaiMatKhau.PasswordChar = '\0';
            }
        }
    }
}
