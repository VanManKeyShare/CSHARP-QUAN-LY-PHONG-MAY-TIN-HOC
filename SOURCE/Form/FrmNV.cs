using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;

namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    public partial class FrmNV : Form
    {
        int MA_NV_LOGGED = 0;

        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmNV() { InitializeComponent(); }

        private void FrmNV_Load(object sender, EventArgs e)
        {
            Mode_Xem();
            MA_NV_LOGGED = ManMan89_Main.MA_NV_LOGGED;
        }

        private bool SQLITE_CHECK_EXISTS(string sql_query, DataTable sql_param)
        {
            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = sql_query;
            vmk.SQLITE_PARAM = sql_param;

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return true;
            }

            if (((DataTable)KQ[2]).Rows.Count != 0) { return true; }

            return false;
        }

        private void Mode_Xem()
        {
            STATUS_THEM_SUA = "";

            btn_xem.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;

            txt_hoten.Text = "";
            txt_taikhoan.Text = "";
            txt_matkhau.Text = "";

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Xem);

            Load_Csdl();
        }

        private void Mode_Them_Sua(bool Them)
        {
            if (Them == true) { STATUS_THEM_SUA = "them"; } else { STATUS_THEM_SUA = "sua"; }

            btn_xem.Enabled = false;
            btn_them.Enabled = false;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Them_Sua);
        }

        private void Remove_All_TabPages()
        {
            foreach (TabPage TC in TabC1.TabPages)
            {
                TabC1.TabPages.Remove(TC);
            }
        }

        private void Load_Csdl()
        {
            gv_list_data.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select "+
                " ma_nv as 'MÃ SỐ', "+
                " ten_nv as 'HỌ & TÊN', " +
                " tai_khoan as 'TÀI KHOẢN' " +
                " from nhan_vien " + 
                " order by ma_nv desc"
            ;

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            if (((DataTable)KQ[2]).Rows.Count != 0) { gv_list_data.DataSource = (DataTable)KQ[2]; }
        }

        private void btn_xem_Click(object sender, EventArgs e) { Mode_Xem(); }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Mode_Them_Sua(true);
            TabP_Them_Sua.Text = "THÊM NHÂN VIÊN";
            label_thongbao_matkhau.Visible = false;
            txt_hoten.Focus();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (gv_list_data.SelectedRows.Count == 0)
            {
                MessageBox.Show("BẠN CHƯA CHỌN ĐỐI TƯỢNG CẦN XÓA", "THÔNG BÁO");
                return;
            }

            foreach (DataGridViewRow gvr in gv_list_data.SelectedRows)
            {
                string id_item_for_delete = gvr.Cells[0].Value.ToString().Trim();

                if (id_item_for_delete == "")
                {
                    MessageBox.Show("KHÔNG LẤY ĐƯỢC ID ĐỐI TƯỢNG CẦN XÓA", "THÔNG BÁO");
                    continue;
                }

                DialogResult dr = MessageBox.Show("BẠN CÓ CHẮC MUỐN XÓA ĐỐI TƯỢNG BẠN ĐANG CHỌN", "XÁC NHẬN", MessageBoxButtons.YesNo);

                if (dr == DialogResult.Yes)
                {
                    if (MA_NV_LOGGED.ToString() == gvr.Cells[0].Value.ToString())
                    {
                        MessageBox.Show("KHÔNG THỂ XÓA TÀI KHOẢN BẠN ĐANG CHỌN" + "\n\n" + "TÀI KHOẢN NÀY BẠN ĐANG SỬ DỤNG", "THÔNG BÁO");
                        continue;
                    }
                    
                    // TIẾN HÀNH XÓA TRONG CSDL //

                    ManMan89_CSDL vmk = new ManMan89_CSDL();
                    vmk.SQLITE_QUERY = "delete from nhan_vien where lower(ma_nv) = @ma_nv";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_nv", id_item_for_delete.ToLower());
                    vmk.SQLITE_PARAM = SQLITE_PARAM;

                    String[] KQ = vmk.SQLITE_INSERT_DELETE_UPDATE();

                    if (KQ[0].ToString() == "ERROR")
                    {
                        if (KQ[1].ToString().ToLower().IndexOf("foreign key constraint failed") >= 0)
                        {
                            MessageBox.Show("ĐỐI TƯỢNG BẠN CHỌN ĐANG ĐƯỢC SỬ DỤNG Ở BẢNG KHÁC" + "\n\n" + "BẠN MUỐN XÓA THÌ CẦN PHẢI XÓA ĐỐI TƯỢNG NÀY Ở CÁC BẢNG ĐÓ TRƯỚC", "THÔNG BÁO");
                            continue;
                        }
                    }

                    if (KQ[0].ToString() == "ERROR")
                    {
                        FrmMsg msg = new FrmMsg();
                        msg.tieu_de = "THÔNG BÁO";
                        msg.noi_dung = "KHÔNG XÓA ĐƯỢC DỮ LIỆU";
                        msg.noi_dung_chi_tiet = KQ[1].ToString();
                        msg.ShowDialog();
                        continue;
                    }
                }
            }

            Mode_Xem();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            if (gv_list_data.SelectedRows.Count == 0)
            {
                MessageBox.Show("BẠN CHƯA CHỌN ĐỐI TƯỢNG CẦN CẬP NHẬT THÔNG TIN", "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            Mode_Them_Sua(false);

            TabP_Them_Sua.Text = "SỬA THÔNG TIN NHÂN VIÊN";

            label_thongbao_matkhau.Visible = true;

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();
            vmk.SQLITE_QUERY = "select ten_nv, tai_khoan " + 
                " from nhan_vien " + 
                " where lower(ma_nv) = @ma_nv"
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_nv", ID_ITEM_FOR_EDIT.ToLower());
            vmk.SQLITE_PARAM = SQLITE_PARAM;

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                Mode_Xem();
                return;
            }

            DataTable dt = (DataTable)KQ[2];
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA NHÂN VIÊN CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            txt_hoten.Text = dt.Rows[0][0].ToString();
            txt_taikhoan.Text = dt.Rows[0][1].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ho_ten = txt_hoten.Text.Trim();
            string tai_khoan = txt_taikhoan.Text.Trim();
            string mat_khau = txt_matkhau.Text.Trim();

            txt_hoten.Text = ho_ten;
            txt_taikhoan.Text = tai_khoan;
            txt_matkhau.Text = mat_khau;

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ho_ten == "" || tai_khoan == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");
                if (ho_ten == "") { txt_hoten.Focus(); return; }
                if (tai_khoan == "") { txt_taikhoan.Focus(); return; }
                return;
            }

            // KIỂM TRA TÊN VIỆT NAM //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(ho_ten) == false)
            {
                MessageBox.Show("HỌ & TÊN CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_hoten.Focus();
                return;
            }

            // KIỂM TRA TÀI KHOẢN //

            if (System.Text.RegularExpressions.Regex.IsMatch(tai_khoan, @"^[a-zA-Z0-9]+$") == false)
            {
                MessageBox.Show("TÀI KHOẢN CHỈ CHẤP NHẬN CHỮ CÁI VÀ SỐ", "THÔNG BÁO");
                txt_taikhoan.Focus();
                return;
            }

            #endregion

            #region THÊM DỮ LIỆU

            if (STATUS_THEM_SUA == "them")
            {
                if (mat_khau == "")
                {
                    MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");
                    txt_matkhau.Focus();
                    return;
                }

                // KIỂM TRA TỒN TẠI //

                DataTable sql_param = new DataTable();
                sql_param.Columns.Add("key", typeof(String));
                sql_param.Columns.Add("value", typeof(String));

                // KIỂM TRA TÀI KHOẢN //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@tai_khoan", tai_khoan.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from nhan_vien where lower(tai_khoan) = @tai_khoan", sql_param) == true)
                {
                    MessageBox.Show("TÀI KHOẢN BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into nhan_vien (ten_nv, tai_khoan, mat_khau) " +
                    " values (@ten_nv, @tai_khoan, @mat_khau)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ten_nv", ho_ten);
                SQLITE_PARAM.Rows.Add("@tai_khoan", tai_khoan.ToLower());
                SQLITE_PARAM.Rows.Add("@mat_khau", mat_khau);
                vmk.SQLITE_PARAM = SQLITE_PARAM;

                String[] KQ = vmk.SQLITE_INSERT_DELETE_UPDATE();

                if (KQ[0].ToString() == "ERROR")
                {
                    FrmMsg msg = new FrmMsg();
                    msg.tieu_de = "THÔNG BÁO";
                    msg.noi_dung = "KHÔNG THÊM ĐƯỢC DỮ LIỆU";
                    msg.noi_dung_chi_tiet = KQ[1].ToString();
                    msg.ShowDialog();
                    return;
                }
            }

            #endregion

            #region SỬA DỮ LIỆU

            if (STATUS_THEM_SUA == "sua")
            {
                // KIỂM TRA TỒN TẠI //

                DataTable sql_param = new DataTable();
                sql_param.Columns.Add("key", typeof(String));
                sql_param.Columns.Add("value", typeof(String));

                // KIỂM TRA TÀI KHOẢN //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_nv", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@tai_khoan", tai_khoan.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from nhan_vien where lower(tai_khoan) = @tai_khoan and lower(ma_nv) <> @ma_nv", sql_param) == true)
                {
                    MessageBox.Show("TÀI KHOẢN BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                string sql_query = "";

                if (mat_khau == "")
                {
                    sql_query = "update nhan_vien " +
                        " set ten_nv = @ten_nv, tai_khoan = @tai_khoan " +
                        " where lower(ma_nv) = @ma_nv "
                    ;
                }
                else
                {
                    sql_query = "update nhan_vien " +
                        " set ten_nv = @ten_nv, tai_khoan = @tai_khoan, mat_khau = @mat_khau " +
                        " where lower(ma_nv) = @ma_nv "
                    ;
                }

                ManMan89_CSDL vmk = new ManMan89_CSDL();
                vmk.SQLITE_QUERY = sql_query;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_nv", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ten_nv", ho_ten);
                SQLITE_PARAM.Rows.Add("@tai_khoan", tai_khoan.ToLower());
                SQLITE_PARAM.Rows.Add("@mat_khau", mat_khau);
                vmk.SQLITE_PARAM = SQLITE_PARAM;

                String[] KQ = vmk.SQLITE_INSERT_DELETE_UPDATE();

                if (KQ[0].ToString() == "ERROR")
                {
                    FrmMsg msg = new FrmMsg();
                    msg.tieu_de = "THÔNG BÁO";
                    msg.noi_dung = "KHÔNG CẬP NHẬT ĐƯỢC DỮ LIỆU";
                    msg.noi_dung_chi_tiet = KQ[1].ToString();
                    msg.ShowDialog();
                    return;
                }
            }

            #endregion

            Mode_Xem();
        }

        private void btn_khongluu_Click(object sender, EventArgs e) { Mode_Xem(); }

        private void btn_close_Click(object sender, EventArgs e) { this.Close(); }

        private void vmkRiMenu_Xoa_Click(object sender, EventArgs e) { btn_xoa_Click(sender, e); }

        private void vmkRiMenu_Sua_Click(object sender, EventArgs e) { btn_sua_Click(sender, e); }

        private void vmkRiMenu_Opening(object sender, CancelEventArgs e)
        {
            if (gv_list_data.SelectedRows.Count == 0)
            {
                vmkRiMenu_Sua.Enabled = false;
                vmkRiMenu_Xoa.Enabled = false;
            }
            else
            {
                vmkRiMenu_Sua.Enabled = true;
                vmkRiMenu_Xoa.Enabled = true;
            }
        }

    }
}
