using BUS;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using ComboBox = System.Windows.Forms.ComboBox;


namespace QuanLyQuanCaFe
{
    public partial class fTableManager : Form
    {
        private BUS_Table tables;
        private BUS_Bill bills;
        private BUS_BillInfo bi;
        private string TenDangNhap;
        private string MatKhau;
        private string SelectedDrinkName;
        public fTableManager(string tdn, string mk)
        {
            InitializeComponent();
            TenDangNhap= tdn;
            MatKhau= mk;
        }
        private void LoadTable()
        {
            tables = new BUS_Table();
            foreach (DTO_Table table in tables.LoadTable())
            {
                Button btn = new Button
                {
                    Width = DAL_Table.TableWidth,
                    Height = DAL_Table.TableHeight,
                    Text = table.Name + Environment.NewLine + table.Status,
                    Tag = table,
                    Cursor = Cursors.Hand
                };
                btn.Click += btn_Click;
                switch (table.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.Blue;
                        break;
                }
                
                flpTable.Controls.Add(btn);
            }
        }
        
        private void ResetTable()
        {
            flpTable.Controls.Clear();
            LoadTable();
        }

        private void ShowBill(int table_id)
        {

            bills = new BUS_Bill();
            dtgBill.DataSource = bills.LoadBillInfo(table_id);
            dtgBill.Columns[0].HeaderText = "Tên món";
            dtgBill.Columns[1].HeaderText = "Số lượng";
            dtgBill.Columns[2].HeaderText = "Đơn giá";
            dtgBill.Columns[3].HeaderText = "Thành tiền";
        }

        public float TotalPrice(int table_id)
        {
            float total = 0;
            bills = new BUS_Bill();
            foreach (DTO_Menu price in bills.LoadBillInfo(table_id))
            {
                total += price.TotalPrice;
            }

            return total;
        }

        void LoadCategory()
        {
            BUS_Category category = new BUS_Category();
            cmbCategory.DataSource= category.LoadCategory();
            cmbCategory.DisplayMember= "Name";
        }

        void LoadDrinkByCategory(int IdCategory)
        {
            BUS_Drink drink = new BUS_Drink();
            cmbListDrink.DataSource = drink.LoadDrinkByCategory(IdCategory);
            cmbListDrink.DisplayMember = "DrinkName";
        }

        void btn_Click(object sender, EventArgs e)
        {
            // Convert sender to Button, sender that button was click
            Button btn = sender as Button;
            // Convert btn to DTO_Table to getId
            DTO_Table tb = btn.Tag as DTO_Table;
            int table_id = tb.Id;
            
            dtgBill.Tag = tb;
            ShowBill(table_id);
            txtTotalPrice.Text = TotalPrice(table_id).ToString();
            if (tb.Status == "Có người")
            {
                LoadEmptyTable();
            }
            
        }

        private int LoadIDTable(int table_id)
        {
            DTO_Table selected = cmbChuyenBan.SelectedValue as DTO_Table;
            
            foreach (DTO_Table tb in tables.LoadTable())
            {
                if (tb.Name == selected.Name)
                {
                    table_id = selected.Id;
                }
            }
            
            return table_id;
        }
        
        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            bi = new BUS_BillInfo();
            bills = new BUS_Bill();
            
            DTO_Table table = dtgBill.Tag as DTO_Table;
            int bill_id_old = bills.CheckIdBill(table.Id);
            int table_id=0;
            table_id=LoadIDTable(table_id);
            int bill_id_new = bills.CheckIdBill(table_id);

            if (bill_id_new == -1)
            {
                bills.InsertBill(table_id);
                foreach (DTO_BillInfo bin in bi.LoadBillInfo(bill_id_old))
                {
                    
                    bills.InsertBillInfo(bills.CheckIdBill(table_id), bin.DrinkID, bin.Count);
                }
                bills.UpdateStatusTable(table.Id, "Trống");
                bills.UpdateStatusTable(table_id, "Có người");
                bills.UpdateBill(bill_id_old, 0, 0);
            }
            else
            {
                foreach (DTO_BillInfo bin in bi.LoadBillInfo(bill_id_old))
                {
                    if (!bills.CheckDrinkIdFromBillIn(bin.DrinkID, bill_id_new))
                    {
                        bills.InsertBillInfo(bills.CheckIdBill(table_id), bin.DrinkID, bin.Count);
                        
                    }
                    else
                    {
                        bills.UpdateBillInfo(bill_id_new, bin.DrinkID, bin.Count);
                    }

                }

                bills.UpdateBill(bill_id_old, 0, 0);
                bills.UpdateStatusTable(table.Id, "Trống");
            }

            ResetTable();
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {
            ResetTable();
            LoadCategory();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            DTO_Category selected = cb.SelectedItem as DTO_Category;
            LoadDrinkByCategory(selected.ID);
        }
        
        private void dtgBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgBill.Rows[e.RowIndex];

                SelectedDrinkName = row.Cells[0].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            
            bills = new BUS_Bill();
            DTO_Table table = dtgBill.Tag as DTO_Table;

            int count = (int)nmCount.Value;
            int drink_id = -1;
            if(cmbListDrink.SelectedItem != null)
            {
                drink_id = (cmbListDrink.SelectedItem as DTO_Drink).ID;
            }
            
            int bill_id;
            if(table!= null)
            {
                bill_id = bills.CheckIdBill(table.Id);
                if (drink_id != -1)
                {
                    if (bill_id == -1)
                    {
                        bills.InsertBill(table.Id);
                        bills.InsertBillInfo(bills.CheckIdBill(table.Id), drink_id, count);
                        bills.UpdateStatusTable(table.Id, "Có người");
                        ResetTable();
                        ShowBillAndTotal();

                    }
                    else
                    {
                        if (!bills.CheckDrinkIdFromBillIn(drink_id, bill_id))
                        {
                            bills.InsertBillInfo(bills.CheckIdBill(table.Id), drink_id, count);
                            ShowBillAndTotal();
                        }
                        else
                        {
                            bills.UpdateBillInfo(bill_id, drink_id, count);
                            ShowBillAndTotal();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không có đồ uống nào trong danh sách");
                }

            }
            else
            {
                MessageBox.Show("Bạn hãy chọn bàn");
            }
            
            void ShowBillAndTotal()
            {
                ShowBill(table.Id);
                txtTotalPrice.Text = TotalPrice(table.Id).ToString();
            }
            
        }

        private int GetSelectedDrinkID()
        {
            DTO_Table table = dtgBill.Tag as DTO_Table;
            BUS_Drink drinks = new BUS_Drink();

            int drink_id=-1;
            int bill_id = bills.CheckIdBill(table.Id);

            foreach (DTO_Drink drink in drinks.GetDrinkID(bill_id))
            {
                if (drink.DrinkName == SelectedDrinkName)
                {
                    drink_id = drink.ID;
                }
            }

            return drink_id;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            
            DTO_Table table = dtgBill.Tag as DTO_Table;
   
            int count = (int)nmXoa.Value;
            int bill_id = bills.CheckIdBill(table.Id);
            int drink_id = GetSelectedDrinkID();

            if(drink_id!= -1)
            {

            }
            if (int.Parse(txtTotalPrice.Text) == 0)
            {
                bills.DeleteBillInfo(bill_id);
                bills.UpdateStatusTable(table.Id, "Trống");
                ShowBillAndTotal();
                ResetTable();
            }
            else
            {
                bills.UpdateBillInfo(bill_id, drink_id, -count);
                ShowBillAndTotal();
                
            }

            void ShowBillAndTotal()
            {
                ShowBill(table.Id);
                txtTotalPrice.Text = TotalPrice(table.Id).ToString();
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            bills = new BUS_Bill();
            DTO_Table table = dtgBill.Tag as DTO_Table;
            int bill_id;
            if(table != null)
            {
                bill_id = bills.CheckIdBill(table.Id);
                bills.UpdateStatusTable(table.Id, "Trống");

                float discount = (float)(nmGiamGia.Value / 100) * TotalPrice(table.Id);
                float total = TotalPrice(table.Id);
                bills.UpdateBill(bill_id, discount, total);
                ShowBill(table.Id);
                ResetTable();
                txtTotalPrice.Clear();
            }
            else
            {
                MessageBox.Show("Hãy chọn bàn cần thanh toán");
            }
            bills.Delete();
            
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnGiamGia_Click(object sender, EventArgs e)
        {  
            DTO_Table table = dtgBill.Tag as DTO_Table;
            float discount = (float)nmGiamGia.Value / 100;
            float newtotalprice = TotalPrice(table.Id)-TotalPrice(table.Id) * discount;
            txtTotalPrice.Text = newtotalprice.ToString();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BUS_Account acc= new BUS_Account();
            
            
            if (acc.CheckAdmin(TenDangNhap, SecurityUtils.SaltedHash(MatKhau)))
            {
                this.Hide();
                fAdmin f = new fAdmin();
                f.ShowDialog();
                this.Show();
                ResetTable();
                LoadCategory();
            }
            else
            {
                MessageBox.Show("Bạn cần có quyền truy cập");
            }
            
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            fAccountProfile f = new fAccountProfile(TenDangNhap,MatKhau);
            f.ShowDialog();
            this.Show();
        }

        private void fTableManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất tài khoản không?", "Thông báo", MessageBoxButtons.OKCancel);
            if (dialogResult == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void LoadEmptyTable()
        {
            
            cmbChuyenBan.DataSource = tables.LoadTable();
            cmbChuyenBan.DisplayMember = "name";
        }

    }
}
