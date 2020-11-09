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
    public partial class FrmSDP : Form
    {
        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmSDP() { InitializeComponent(); }

        private void FrmSDP_Load(object sender, EventArgs e)
        {
            Mode_Xem();

            // TẠO DỮ LIỆU CHO CÁC CONTROL //

            for (int i = 1; i < 32; i++)
            {
                cbo_ngay.Items.Add(i);
                if (i < 13) { cbo_thang.Items.Add(i); }
            }
            for (int i = DateTime.Now.Year - 5; i <= DateTime.Now.Year + 5; i++)
            {
                if (i > 2013)
                {
                    cbo_nam.Items.Add(i);
                }
            }
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

        private void Mode_Xem()
        {
            STATUS_THEM_SUA = "";

            btn_xem.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;

            cbo_giaovien.Text = null;
            cbo_phong.Text = null;
            cbo_tiethoc.Text = null;
            cbo_ngay.Text = null;
            cbo_thang.Text = null;
            cbo_nam.Text = null;

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Xem);

            Load_Csdl();
        }

        private void Load_CSDL_GV()
        {
            cbo_giaovien.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_gv, ten_gv from giao_vien order by ten_gv asc";

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU GIÁO VIÊN";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            DataTable dt = (DataTable)KQ[2];
            if (dt.Rows.Count != 0)
            {
                cbo_giaovien.DataSource = dt;
                cbo_giaovien.DisplayMember = "ten_gv";
                cbo_giaovien.ValueMember = "ma_gv";
            }
        }

        private void Load_CSDL_Phong()
        {
            cbo_phong.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_phong, ten_phong from phong order by ten_phong asc";

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU PHÒNG";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            DataTable dt = (DataTable)KQ[2];
            if (dt.Rows.Count != 0)
            {
                cbo_phong.DataSource = dt;
                cbo_phong.DisplayMember = "ten_phong";
                cbo_phong.ValueMember = "ma_phong";
            }
        }

        private void Load_CSDL_TH()
        {
            cbo_tiethoc.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_th, ten_th from tiet_hoc order by gio_begin asc, phut_begin asc, gio_end asc, phut_end asc";

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU TIẾT HỌC";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            DataTable dt = (DataTable)KQ[2];
            if (dt.Rows.Count != 0)
            {
                cbo_tiethoc.DataSource = dt;
                cbo_tiethoc.DisplayMember = "ten_th";
                cbo_tiethoc.ValueMember = "ma_th";
            }
        }

        private void Load_Csdl()
        {
            gv_list_data.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select "+
                " ma_dp as 'MÃ SỐ', "+
                
                " ( " +
                "   (CASE WHEN (LENGTH(ngay_dp) < 2) THEN ('0' || ngay_dp) ELSE ('' || ngay_dp) END) || '/' || " +
                "   (CASE WHEN (LENGTH(thang_dp) < 2) THEN ('0' || thang_dp) ELSE ('' || thang_dp) END) || '/' || nam_dp " +
                " ) as 'NGÀY ĐẶT PHÒNG', " +

                " (select ten_gv from giao_vien where ma_gv = dat_phong.ma_gv) as 'GIÁO VIÊN', " +
                " (select ten_phong from phong where ma_phong = dat_phong.ma_phong) as 'TÊN PHÒNG', " +
                " (select ten_th from tiet_hoc where ma_th = dat_phong.ma_th) as 'TIẾT HỌC' " +

                " from dat_phong, tiet_hoc " +
                " where dat_phong.ma_th = tiet_hoc.ma_th " +
                " order by nam_dp desc, thang_dp desc, ngay_dp desc, gio_end desc, phut_end desc, gio_begin desc, phut_begin desc, ma_dp desc"
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

            DataTable dt = (DataTable)KQ[2];

            if (dt.Rows.Count != 0) { gv_list_data.DataSource = dt; }

            // LẤY DỮ LIỆU GIÁO VIÊN & PHÒNG & TIẾT HỌC //

            Load_CSDL_GV();
            Load_CSDL_Phong();
            Load_CSDL_TH();
        }

        private void btn_xem_Click(object sender, EventArgs e) { Mode_Xem(); }

        private void btn_them_Click(object sender, EventArgs e)
        {
            // LẤY DỮ LIỆU PHÒNG & TB //

            Load_CSDL_GV();
            Load_CSDL_Phong();
            Load_CSDL_TH();

            // CẤU HÌNH THÊM //

            Mode_Them_Sua(true);
            TabP_Them_Sua.Text = "ĐẶT PHÒNG";
            cbo_giaovien.Focus();
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
                    // TIẾN HÀNH XÓA TRONG CSDL //

                    ManMan89_CSDL vmk = new ManMan89_CSDL();

                    vmk.SQLITE_QUERY = "delete from dat_phong where lower(ma_dp) = @ma_dp";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_dp", id_item_for_delete.ToLower());
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

            // LẤY DỮ LIỆU GIÁO VIÊN & PHÒNG & TIẾT HỌC //

            Load_CSDL_GV();
            Load_CSDL_Phong();
            Load_CSDL_TH();

            // BEGIN //

            Mode_Them_Sua(false);

            TabP_Them_Sua.Text = "CẬP NHẬT THÔNG TIN ĐẶT PHÒNG";

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString().Trim();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_gv, ma_phong, ma_th, ngay_dp, thang_dp, nam_dp " +
                " from dat_phong " +
                " where lower(ma_dp) = @ma_dp "
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_dp", ID_ITEM_FOR_EDIT.ToLower());
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
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA LỊCH ĐẶT PHÒNG CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            cbo_giaovien.SelectedValue = dt.Rows[0][0].ToString();
            cbo_phong.SelectedValue = dt.Rows[0][1].ToString();
            cbo_tiethoc.SelectedValue = dt.Rows[0][2].ToString();
            cbo_ngay.Text = dt.Rows[0][3].ToString();
            cbo_thang.Text = dt.Rows[0][4].ToString();
            cbo_nam.Text = dt.Rows[0][5].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ma_gv = "";
            string ma_phong = "";
            string ma_th = "";

            if (cbo_giaovien.SelectedValue != null)
            {
                ma_gv = cbo_giaovien.SelectedValue.ToString().Trim();
            }
            if (cbo_phong.SelectedValue != null)
            {
                ma_phong = cbo_phong.SelectedValue.ToString().Trim();
            }
            if (cbo_tiethoc.SelectedValue != null)
            {
                ma_th = cbo_tiethoc.SelectedValue.ToString().Trim();
            }

            string ngay = cbo_ngay.Text.Trim();
            string thang = cbo_thang.Text.Trim();
            string nam = cbo_nam.Text.Trim();

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ma_gv == "" || ma_phong == "" || ma_th == "" || ngay == "" || thang == "" || nam == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");

                if (ma_gv == "") { cbo_giaovien.Focus(); return; }
                if (ma_phong == "") { cbo_phong.Focus(); return; }
                if (ma_th == "") { cbo_tiethoc.Focus(); return; }
                if (ngay == "") { cbo_ngay.Focus(); return; }
                if (thang == "") { cbo_thang.Focus(); return; }
                if (nam == "") { cbo_nam.Focus(); return; }

                return;
            }

            // KIỂM TRA NGÀY THÁNG NĂM //

            if (ManMan89_Main.IS_VALID_DATE(Convert.ToInt16(nam), Convert.ToInt16(thang), Convert.ToInt16(ngay)) == false)
            {
                MessageBox.Show("THỜI GIAN KHÔNG ĐÚNG" + "\n\n" + "NGÀY  " + ngay + "  THÁNG  " + thang + "  NĂM  " + nam + "\n\n" + "KHÔNG TỒN TẠI", "THÔNG BÁO");
                return;
            }

            #endregion

            #region THÊM DỮ LIỆU

            // BEGIN //

            if (STATUS_THEM_SUA == "them")
            {
                // KIỂM TRA TỒN TẠI //

                DataTable sql_param = new DataTable();
                sql_param.Columns.Add("key", typeof(String));
                sql_param.Columns.Add("value", typeof(String));

                // KIỂM TRA THIẾT BỊ CÓ TRONG PHÒNG //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_phong", ma_phong.ToLower());
                sql_param.Rows.Add("@ma_th", ma_th.ToLower());
                sql_param.Rows.Add("@ngay_dp", ngay.ToLower());
                sql_param.Rows.Add("@thang_dp", thang.ToLower());
                sql_param.Rows.Add("@nam_dp", nam.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from dat_phong " +
                    " where lower(ma_phong) = @ma_phong " +
                    " and lower(ma_th) = @ma_th " +
                    " and lower(ngay_dp) = @ngay_dp " +
                    " and lower(thang_dp) = @thang_dp " +
                    " and lower(nam_dp) = @nam_dp ", sql_param) == true)
                {
                    MessageBox.Show("PHÒNG BẠN CHỌN ĐÃ ĐƯỢC NGƯỜI KHÁC ĐẶT TRONG THỜI GIAN TRÊN", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into dat_phong (ma_gv, ma_phong, ma_th, ngay_dp, thang_dp, nam_dp) " +
                    " values (@ma_gv, @ma_phong, @ma_th, @ngay_dp, @thang_dp, @nam_dp)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_gv", ma_gv.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_phong", ma_phong.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_th", ma_th.ToLower());
                SQLITE_PARAM.Rows.Add("@ngay_dp", ngay);
                SQLITE_PARAM.Rows.Add("@thang_dp", thang);
                SQLITE_PARAM.Rows.Add("@nam_dp", nam);
                vmk.SQLITE_PARAM = SQLITE_PARAM;

                String[] KQ =  vmk.SQLITE_INSERT_DELETE_UPDATE();

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

                // KIỂM TRA THIẾT BỊ CÓ TRONG PHÒNG //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_dp", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@ma_phong", ma_phong.ToLower());
                sql_param.Rows.Add("@ma_th", ma_th.ToLower());
                sql_param.Rows.Add("@ngay_dp", ngay.ToLower());
                sql_param.Rows.Add("@thang_dp", thang.ToLower());
                sql_param.Rows.Add("@nam_dp", nam.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from dat_phong " +
                    " where lower(ma_phong) = @ma_phong " +
                    " and lower(ma_th) = @ma_th " +
                    " and lower(ngay_dp) = @ngay_dp " +
                    " and lower(thang_dp) = @thang_dp " +
                    " and lower(nam_dp) = @nam_dp " + 
                    " and lower(ma_dp) <> @ma_dp ", sql_param) == true)
                {
                    MessageBox.Show("PHÒNG BẠN CHỌN ĐÃ ĐƯỢC NGƯỜI KHÁC ĐẶT TRONG THỜI GIAN TRÊN", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "update dat_phong set " +
                    " ma_gv = @ma_gv, ma_phong = @ma_phong, ma_th = @ma_th, ngay_dp = @ngay_dp, thang_dp = @thang_dp, nam_dp = @nam_dp " +
                    " where lower(ma_dp) = @ma_dp "
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_dp", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_gv", ma_gv.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_phong", ma_phong.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_th", ma_th.ToLower());
                SQLITE_PARAM.Rows.Add("@ngay_dp", ngay);
                SQLITE_PARAM.Rows.Add("@thang_dp", thang);
                SQLITE_PARAM.Rows.Add("@nam_dp", nam);
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

        private void txt_soluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (!char.IsNumber(e.KeyChar)) { e.Handled = true; }
            }
        }

        private void btn_reload_phong_Click(object sender, EventArgs e)
        {
            Load_CSDL_Phong();
        }

        private void btn_reload_tiethoc_Click(object sender, EventArgs e)
        {
            Load_CSDL_TH();
        }

        private void btn_reload_giaovien_Click(object sender, EventArgs e)
        {
            Load_CSDL_GV();
        }

    }
}
