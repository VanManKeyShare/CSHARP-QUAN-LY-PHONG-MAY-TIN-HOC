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
    public partial class FrmMain : Form
    {
        public FrmMain() { InitializeComponent(); }

        public void vmk_Open_Only_One_Child_Form(Form Form_Child, Form Form_Main)
        {
            FormCollection FC = Application.OpenForms;
            foreach (Form vFrm in FC)
            {
                if (vFrm.Name.ToLower() == Form_Child.Name.ToLower())
                {
                    Form_Child.Focus();
                    vFrm.Focus();
                    return;
                }
            }
            Form_Child.MdiParent = Form_Main;
            Form_Child.Show();
        }

        public void Kiem_Tra_Trang_Thai_Login()
        {
            if (ManMan89_Main.STATUS_LOGIN == false)
            {
                vmkMenu_hethong_login.Visible = true;
                vmkMenu_hethong_logout.Visible = false;
                vmkMenu_cauhinh.Visible = false;
                vmkMenu_quanly.Visible = false;

                FrmLogin Frm = new FrmLogin();
                Frm.WindowState = FormWindowState.Maximized;
                vmk_Open_Only_One_Child_Form(Frm, this);

                return;
            }
            else
            {
                vmkMenu_hethong_login.Visible = false;
                vmkMenu_hethong_logout.Visible = true;
                vmkMenu_cauhinh.Visible = true;
                vmkMenu_quanly.Visible = true;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // CÀI ĐẶT MÀU NỀN FORM MAIN

            foreach (Control vConTrol in this.Controls)
            {
                if ((vConTrol) is MdiClient)
                {
                    vConTrol.BackColor = Color.SteelBlue;
                }
            }

            // KIỂM TRA ĐÃ ĐĂNG NHẬP HAY CHƯA //

            Kiem_Tra_Trang_Thai_Login();
        }

        private void vmkMenu_hethong_login_Click(object sender, EventArgs e)
        {
            FrmLogin Frm = new FrmLogin();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_hethong_exit_Click(object sender, EventArgs e) { this.Dispose(); }

        private void vmkMenu_hethong_logout_Click(object sender, EventArgs e)
        {
            ManMan89_Main.STATUS_LOGIN = false;
            ManMan89_Main.TAI_KHOAN = "";
            ManMan89_Main.MA_NV_LOGGED = 0;
            Kiem_Tra_Trang_Thai_Login();
        }

        private void vmkMenu_thongtin_Click(object sender, EventArgs e)
        {
            FrmTT Frm = new FrmTT();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_cauhinh_nhanvien_Click(object sender, EventArgs e)
        {  
            FrmNV Frm = new FrmNV();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_cauhinh_giaovien_Click(object sender, EventArgs e)
        {
            FrmGV Frm = new FrmGV();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_cauhinh_phong_Click(object sender, EventArgs e)
        {
            FrmPHONG Frm = new FrmPHONG();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_cauhinh_thietbi_Click(object sender, EventArgs e)
        {
            FrmTB Frm = new FrmTB();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        public void Open_Form_LoaiThietBi()
        {
            FrmLTB Frm = new FrmLTB();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_cauhinh_tiethoc_Click(object sender, EventArgs e)
        {
            FrmTH Frm = new FrmTH();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_quanly_sudungthietbi_Click(object sender, EventArgs e)
        {
            FrmTBSD Frm = new FrmTBSD();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

        private void vmkMenu_quanly_sudungphong_Click(object sender, EventArgs e)
        {
            FrmSDP Frm = new FrmSDP();
            Frm.WindowState = FormWindowState.Maximized;
            vmk_Open_Only_One_Child_Form(Frm, this);
        }

    }
}
