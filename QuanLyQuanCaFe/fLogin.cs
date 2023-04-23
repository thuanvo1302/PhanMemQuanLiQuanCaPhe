using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using DAL;
using BUS;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace QuanLyQuanCaFe
{
    public partial class fLogin : Form
    {
        private BUS_Account account = new BUS_Account();
        public fLogin()
        {

            InitializeComponent();

        }
        
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taikhoan = txtTenDangNhap.Text;
            string matkhau = txtMatKhau.Text;

            matkhau = SecurityUtils.SaltedHash(matkhau);
            
            if (taikhoan == "" || matkhau=="")
            {
                if (taikhoan == "") 
                {
                    MessageBox.Show("Hãy nhập vào tên đăng nhập");
                    txtTenDangNhap.Focus();
                }
                else if(matkhau=="")
                {
                    MessageBox.Show("Hãy nhập vào mật khẩu");
                    txtMatKhau.Focus();
                }

            }
            else
            {
                if (account.Login(taikhoan, matkhau))
                {
                    
                    fTableManager f = new fTableManager(txtTenDangNhap.Text, txtMatKhau.Text);
 
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Bạn nhập sai tên tài khoản hoặc mật khẩu");
                }
                txtTenDangNhap.Clear();
                txtMatKhau.Clear();
                txtTenDangNhap.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnEye_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '\0')
            {
                btnHide_Eye.BringToFront();
                txtMatKhau.PasswordChar = '*';
            }
        }

        private void btnHide_Eye_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.PasswordChar == '*')
            {
                btnEye.BringToFront();
                txtMatKhau.PasswordChar = '\0';
            }
        }

        private void llblDangKi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fRegister f = new fRegister();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(100,0,0,0);
            label1.BackColor = Color.FromArgb(0,0,0,0);
            label2.BackColor = Color.FromArgb(0,0,0,0);
            llblDangKi.BackColor = Color.FromArgb(0,0,0,0);
            
        }
    }
}
