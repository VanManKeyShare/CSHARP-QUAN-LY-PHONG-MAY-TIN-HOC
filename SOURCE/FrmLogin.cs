using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;
using System.Data.SQLite;
using System.Collections;

namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    public partial class FrmLogin : Form
    {
        public FrmLogin() { InitializeComponent(); }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string check_remember_taikhoan_from_regedit = (string)Registry.GetValue("HKEY_CURRENT_USER", "ManMan89_QLPMTH_Check_Remember_TK", "0");
            string taikhoan_from_regedit = (string)Registry.GetValue("HKEY_CURRENT_USER", "ManMan89_QLPMTH_Remember_TK", "");
            
            if (check_remember_taikhoan_from_regedit != "0")
            {
                txt_taikhoan.Text = taikhoan_from_regedit.Trim();
                chk_remember_taikhoan.Checked = true;
            }

            if (txt_taikhoan.Text != "") { txt_taikhoan.TabStop = false; }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            txt_taikhoan.Text = txt_taikhoan.Text.Trim();

            string tai_khoan = txt_taikhoan.Text;
            string mat_khau_from_client = txt_matkhau.Text;

            // KIỂM TRA DỮ LIỆU //

            if (tai_khoan == "" || mat_khau_from_client == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");
                if (tai_khoan == "") { txt_taikhoan.Focus(); return; }
                if (mat_khau_from_client == "") { txt_matkhau.Focus(); return; }
                return;
            }

            // TIẾN HÀNH ĐĂNG NHẬP //

            ManMan89_CSDL vmk = new ManMan89_CSDL();
            vmk.SQLITE_QUERY = "select mat_khau, ma_nv from nhan_vien where tai_khoan = @tai_khoan";
            
            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@tai_khoan", tai_khoan);
            vmk.SQLITE_PARAM = SQLITE_PARAM;

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "ĐĂNG NHẬP KHÔNG THÀNH CÔNG";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            DataTable dt = (DataTable)KQ[2];
            if (dt.Rows.Count == 0)
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "TÀI KHOẢN HOẶC MẬT KHẨU KHÔNG ĐÚNG";
                msg.noi_dung_chi_tiet = "";
                msg.ShowDialog();
                return;
            }

            string mat_khau_from_csdl = dt.Rows[0][0].ToString();
            int ma_nv_from_csdl = Convert.ToInt16(dt.Rows[0][1].ToString());

            if (mat_khau_from_client != mat_khau_from_csdl)
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "TÀI KHOẢN HOẶC MẬT KHẨU KHÔNG ĐÚNG";
                msg.noi_dung_chi_tiet = "";
                msg.ShowDialog();
                return;
            }

            // ĐĂNG NHẬP THÀNH CÔNG //

            if (chk_remember_taikhoan.Checked == true)
            {
                Registry.SetValue("HKEY_CURRENT_USER", "ManMan89_QLPMTH_Check_Remember_TK", "1");
                Registry.SetValue("HKEY_CURRENT_USER", "ManMan89_QLPMTH_Remember_TK", txt_taikhoan.Text);
            }

            ManMan89_Main.STATUS_LOGIN = true;
            ManMan89_Main.TAI_KHOAN = tai_khoan;
            ManMan89_Main.MA_NV_LOGGED = ma_nv_from_csdl;

            ((FrmMain)this.MdiParent).Kiem_Tra_Trang_Thai_Login();

            this.Close();
        }

        private void txt_taikhoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13)) { btn_login_Click(sender, e); }
        }

        private void txt_matkhau_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(13)) { btn_login_Click(sender, e); }
        }

        private void chk_remember_taikhoan_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_remember_taikhoan.Checked != true)
            {
                try
                {
                    Registry.CurrentUser.DeleteValue("ManMan89_QLPMTH_Check_Remember_TK");
                    Registry.CurrentUser.DeleteValue("ManMan89_QLPMTH_Remember_TK");
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
        }

        private void btn_close_Click(object sender, EventArgs e) { this.Close(); }

        private void txt_matkhau_Enter(object sender, EventArgs e) { txt_taikhoan.TabStop = true; }
    }
}
