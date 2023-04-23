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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;


namespace QuanLyQuanCaFe
{
    public partial class fAdmin : Form
    {
        private int SelectedDrinkID;
        private int SelectedCategoryID;
        private int SelectedTableID;
        private int SelectedBillID;
        private string SelectedAccount;

        BUS_Table tables = new BUS_Table();
        BUS_Bill bill = new BUS_Bill();
        BUS_Account account = new BUS_Account();
        public fAdmin()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (tabPage2 == tabControl1.SelectedTab)
            {
                BUS_Drink drink = new BUS_Drink();
                dtgDrink.DataSource = drink.LoadAllDrink();
                LoadCategory_1();
                LoadCategory_2();
                dtgDrink.Columns["id"].Visible = false;
                dtgDrink.Columns["CategoryID"].Visible = false;
                dtgDrink.Columns["DrinkName"].HeaderText = "Tên đồ uống";
                dtgDrink.Columns["Price"].HeaderText = "Giá";

            }
            if (tabPage3 == tabControl1.SelectedTab)
            {
                BUS_Category category= new BUS_Category();
                dtgCategory.DataSource = category.LoadCategory();
                dtgCategory.Columns["id"].Visible = false;
                grbInfoCategory.Enabled = false;
                grbThemLoai.Enabled = false;
                btnLuuCategory.Enabled = false;
                dtgCategory.Columns["Name"].HeaderText = "Tên loại";

            }
            if (tabPage4==tabControl1.SelectedTab) 
            {
                
                dtgShowTable.DataSource = tables.LoadTable();
                dtgShowTable.Columns["id"].Visible = false;
                grbThongTinBan.Enabled = false;
                dtgShowTable.Columns["Name"].HeaderText = "Tên bàn";
                dtgShowTable.Columns["Status"].HeaderText = "Tình trạng bàn";

            }
            if (tabPage5==tabControl1.SelectedTab) 
            {
                
                dtgLoadAllAccount.DataSource = account.LoadAllAccount();
                dtgLoadAllAccount.Columns["QuyenHan"].HeaderText = "Loại tài khoản";
                dtgLoadAllAccount.Columns["TaiKhoan"].HeaderText = "Tên đăng nhập";
                dtgLoadAllAccount.Columns["MatKhau"].Visible = false;
                txtThemLoaiTaiKhoan.Text = "Admin";
                grbThemAccount.Enabled = false;
                grbInfoAccount.Enabled= false;
                btnLuuAccount.Enabled = false;
            }
        }

        void Enable(bool check )
        {
            grbInfoDrink.Enabled = check;
            grbInsertDrink.Enabled = check;
            btnLuuDoUong.Enabled = check;
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {
            LoadCategory_1();
            LoadCategory_2();
            Enable(false);
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        //Thong ke
        private void dtgStatistic_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgStatistic.Rows[e.RowIndex];

                SelectedBillID = (int)row.Cells[4].Value;
                
            }
            BUS_Bill b = new BUS_Bill();
            List<DTO_Statistic> namecount = b.GetDrinkNameAndCount(SelectedBillID);
            
            dtgNameCount.DataSource = namecount;
            dtgNameCount.Columns["date_checkin"].Visible = false;
            dtgNameCount.Columns["date_checkout"].Visible = false;
            dtgNameCount.Columns["discount"].Visible = false;
            dtgNameCount.Columns["total"].Visible = false;
            dtgNameCount.Columns["id"].Visible = false;
            dtgNameCount.Columns["Count"].HeaderText = "Số lượng";
            dtgNameCount.Columns["DrinkName"].HeaderText = "Tên đồ uống";
            
        }

        private void LoadDTG(string from, string to)
        {
            bill= new BUS_Bill();
            List<DTO_Statistic> statistics = bill.LoadAllBill(from, to);

            dtgStatistic.DataSource = statistics;
            dtgStatistic.Columns["id"].Visible = false;
            dtgStatistic.Columns["Date_checkin"].HeaderText = "Thời gian vào quán";
            dtgStatistic.Columns["Date_checkout"].HeaderText = "Thời gian ra";
            dtgStatistic.Columns["Discount"].HeaderText = "Giảm giá";
            dtgStatistic.Columns["Total"].HeaderText = "Tổng tiền";
            dtgStatistic.Columns["Count"].Visible = false;
            dtgStatistic.Columns["DrinkName"].Visible = false;
        }

        private void LoadResult(string from, string to)
        {
            bill = new BUS_Bill();
            int total = 0;
            foreach (DTO_Statistic statistic in bill.LoadAllBill(from, to))
            {
                total += statistic.Total - statistic.Discount;
            }
            int count = bill.GetCount(from, to);
            txtTongDoanhThu.Text = total.ToString();
            txtTongSoMon.Text = count.ToString();
        }
  
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            
            
            string From = dtpFrom.Value.ToShortDateString();
            string To = dtpTo.Value.ToShortDateString();
            
            LoadDTG(From, To);
            if(dtgStatistic.Rows.Count != 0)
            {
                LoadResult(From, To);
            }
            else
            {
                MessageBox.Show("Không có hóa đơn trong thời gian từ " + From + " đến " + To);
            }
            
            
        }

        private void btnThongKeTuan_Click(object sender, EventArgs e)
        {
            
            
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime monday = today.AddDays( - (currentDayOfWeek-1));
            DateTime sunday = monday.AddDays(6);
            
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            
            LoadDTG(monday.ToShortDateString(), sunday.ToShortDateString());
            if (dtgStatistic.Rows.Count != 0)
            {
                LoadResult(monday.ToShortDateString(), sunday.ToShortDateString());
            }
            else
            {
                MessageBox.Show("Không có hóa đơn trong thời gian từ " + monday.ToShortDateString() + " đến " + sunday.ToShortDateString());
            }
            
        }
        
        private void btnThongKeThang_Click(object sender, EventArgs e)
        {
           

            DateTime date = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            LoadDTG(firstDayOfMonth.ToShortDateString(), lastDayOfMonth.ToShortDateString());
            
            if (dtgStatistic.Rows.Count != 0)
            {
                LoadResult(firstDayOfMonth.ToShortDateString(), lastDayOfMonth.ToShortDateString());
            }
            else
            {
                MessageBox.Show("Không có hóa đơn trong thời gian từ " + firstDayOfMonth.ToShortDateString() + " đến " + lastDayOfMonth.ToShortDateString());
            }
        }

        //Category
        private void dtgCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgCategory.Rows[e.RowIndex];
                SelectedCategoryID = (int)row.Cells[0].Value;
                txtNameCategory.Text = row.Cells[1].Value.ToString();
            }
            
        }
        private void btnThemCategory_Click(object sender, EventArgs e)
        {
            btnLuuCategory.Enabled = true;
            grbThemLoai.Enabled = true;
            txtNameCategory.Clear();
        }

        private void btnXoaCategory_Click(object sender, EventArgs e)
        {
            string name = txtNameCategory.Text;
            DialogResult result1 = MessageBox.Show("Đồ uống có loại " + name + " cũng sẽ bị xóa hết","Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result1 == DialogResult.Yes)
            {
                BUS_Category category = new BUS_Category();
                category.DeleteCaterogy(SelectedCategoryID);
                dtgCategory.DataSource = category.LoadCategory();
            }
            txtNameCategory.Clear();
        }

        private void btnSuaCategory_Click(object sender, EventArgs e)
        {
            grbInfoCategory.Enabled = true;
            btnLuuCategory.Enabled = true;
        }

        private void btnLuuCategory_Click(object sender, EventArgs e)
        {
            string nameUpdate = txtNameCategory.Text;
            string nameAdd = txtThemLoai.Text;
            BUS_Category category = new BUS_Category();
            if (grbInfoCategory.Enabled == true)
            {
                category.UpdateDrink(SelectedCategoryID, nameUpdate);
                MessageBox.Show("Sửa thành công");
                
            }
            if(grbThemLoai.Enabled==true)
            {
                if (category.InsertCategory(nameAdd))
                {
                    MessageBox.Show("Thêm thành công");
                }
                else
                {
                    MessageBox.Show("Thêm không thành công");
                }
            }
            dtgCategory.DataSource = category.LoadCategory();
            btnLuuCategory.Enabled = false;
            grbInfoCategory.Enabled = false;
            grbThemLoai.Enabled = false;
            txtThemLoai.Clear();
        }

        //Drink
        private void btnThemDrink_Click(object sender, EventArgs e)
        {
            btnLuuDoUong.Enabled = true;
            grbInsertDrink.Enabled = true;
        }

        void LoadCategory_1()
        {
            BUS_Category category = new BUS_Category();
            cmbCategory.DataSource = category.LoadCategory();
            cmbCategory.DisplayMember = "Name";
        }

        void LoadCategory_2()
        {
            BUS_Category category = new BUS_Category();
            cmbTenLoai.DataSource = category.LoadCategory();
            cmbTenLoai.DisplayMember = "Name";
        }

        private void dtgDrink_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BUS_Category category = new BUS_Category();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgDrink.Rows[e.RowIndex];
                
                txtDrinkName.Text = row.Cells[1].Value.ToString();
                cmbCategory.Text = category.GetNameCategoryByID((int)row.Cells[2].Value);
                txtPrice.Text = row.Cells[3].Value.ToString();
                SelectedDrinkID = (int)row.Cells[0].Value;
            }
        }

        private void btnXoaDrink_Click(object sender, EventArgs e)
        {
            BUS_Drink drink = new BUS_Drink();
            drink.DeleteDrink(SelectedDrinkID);
            dtgDrink.DataSource = drink.LoadAllDrink();
            MessageBox.Show("Xóa thành công ");
        }

        private void btnSuaDrink_Click(object sender, EventArgs e)
        {
            btnLuuDoUong.Enabled= true;
            grbInfoDrink.Enabled= true;
            
        }

        private void btnLuuDoUong_Click(object sender, EventArgs e)
        {
            BUS_Category category = new BUS_Category();
            BUS_Drink drink = new BUS_Drink();
            //insert drink
            if (grbInsertDrink.Enabled == true )
            {
                if(txtTenDoUong.Text != "" && txtGia.Text != "")
                {
                    string name = txtTenDoUong.Text;
                    string categoryname = cmbTenLoai.Text;
                    float price = float.Parse(txtGia.Text);
                    int IDCategory = category.GetIDCategoryByName(categoryname);

                    if (drink.InsertDrink(name, IDCategory, price))
                    {
                        MessageBox.Show("Thêm thành công");
                        txtTenDoUong.Clear();
                        txtGia.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Đồ uống đã tồn tại");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn hãy nhập đủ thông tin đồ uống cần thêm");
                }
            }
            
            //update drink
            if (grbInfoDrink.Enabled == true)
            {
                if (txtDrinkName.Text != "" && txtPrice.Text != "")
                {
                    int id = SelectedDrinkID;
                    string name = txtDrinkName.Text;
                    int IDCategory = category.GetIDCategoryByName(cmbCategory.Text);
                    float price = float.Parse(txtPrice.Text);

                    if (drink.UpdateDrink(id, name, IDCategory, price))
                    {
                        MessageBox.Show("Cập nhật đồ uống thành công");
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật đồ uống không thành công");
                    }
                }
                else
                {
                    MessageBox.Show("Bạn hãy nhập đủ thông tin đồ uống cần sửa");
                }
            }

            dtgDrink.DataSource = drink.LoadAllDrink();
            Enable(false);
        }

        //table
        private void dtgShowTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgShowTable.Rows[e.RowIndex];

                txtTenBan.Text = row.Cells[1].Value.ToString();
                txtTrangThaiBan.Text = row.Cells[2].Value.ToString();
                SelectedTableID = (int)row.Cells[0].Value;
                
            }
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            if (tables.InsertTable())
            {
                MessageBox.Show("Thêm bàn thành công");
            }
            dtgShowTable.DataSource = tables.LoadTable();

        }

        private void btnAutoAdd_Click(object sender, EventArgs e)
        {
            int count = (int)nmCountTable.Value;
            tables.AutoTable(count);
            dtgShowTable.DataSource = tables.LoadTable();
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            if (tables.DeleteTable(SelectedTableID))
            {
                MessageBox.Show("Xóa bàn thành công");
            }
            dtgShowTable.DataSource = tables.LoadTable();
        }

        //Account
        private void dtgLoadAllAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgLoadAllAccount.Rows[e.RowIndex];

                txtTenTaiKhoan.Text = row.Cells[1].Value.ToString();
                txtLoaiTaiKhoan.Text = row.Cells[0].Value.ToString();
                SelectedAccount = row.Cells[1].Value.ToString();
            }
        }

        private void btnThemAccount_Click(object sender, EventArgs e)
        {
            grbThemAccount.Enabled= true;
            btnLuuAccount.Enabled= true;
        }

        private void btnXoaAccount_Click(object sender, EventArgs e)
        {
            BUS_Account acc= new BUS_Account();
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa tài khoản " + SelectedAccount + " không", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                account.DeleteAccount(SelectedAccount);
                dtgLoadAllAccount.DataSource = acc.LoadAllAccount();
                

            }
        }

        private void btnLuuAccount_Click(object sender, EventArgs e)
        {
            BUS_Account acc = new BUS_Account();
            
            if (grbThemAccount.Enabled)
            {
                acc.InsertAccountAdmin(txtThemTenTaiKhoan.Text, SecurityUtils.SaltedHash(txtThemMatKhau.Text));
                dtgLoadAllAccount.DataSource = acc.LoadAllAccount();
                grbThemAccount.Enabled= false;
            }

            btnLuuAccount.Enabled= false;
        }
    }
}
