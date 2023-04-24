using BUS;
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
    public partial class fReport : Form
    {
        BUS_Report report = new BUS_Report();
        private int bill_id;
        Bitmap bmp;
        public fReport(int bill_id)
        {
            this.bill_id = bill_id;
            InitializeComponent();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            dtgReport.DataSource = report.GetReport(bill_id);
            dtgReport.Columns["stt"].HeaderText = "STT";
            dtgReport.Columns["Name"].HeaderText = "Tên";
            dtgReport.Columns["Price"].HeaderText = "Đơn giá";
            dtgReport.Columns["Count"].HeaderText = "Số lượng";
            dtgReport.Columns["Tt"].HeaderText = "Thành tiền";
            lblTong.Text = report.GetTotal(bill_id).ToString();
            lblGiamGia.Text = report.GetDiscount(bill_id).ToString();
            lblConLai.Text = (report.GetTotal(bill_id) - report.GetDiscount(bill_id)).ToString();
            lblBan.Text = report.GetTableName(bill_id).ToString();

           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);

        }

        private void fReport_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);

            printPreviewDialog1.ShowDialog();
        }
    }
}
