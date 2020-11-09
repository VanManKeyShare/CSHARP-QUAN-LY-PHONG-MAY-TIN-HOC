using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    public partial class FrmMsg : Form
    {
        public string tieu_de = "";
        public string noi_dung = "";
        public string noi_dung_chi_tiet = "";

        public FrmMsg() { InitializeComponent(); }

        private void FrmMsg_Load(object sender, EventArgs e)
        {
            this.Text = tieu_de;
            txt_rutgon.Text = noi_dung;
            txt_chitiet.Text = noi_dung_chi_tiet;
            btn_close.Focus();
        }

        private void btn_close_Click(object sender, EventArgs e) { this.Close(); }

        private void btn_cancel_Click(object sender, EventArgs e) { this.Close(); }
    }
}
