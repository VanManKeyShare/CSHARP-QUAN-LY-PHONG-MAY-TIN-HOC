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
    public partial class FrmTBSD : Form
    {
        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmTBSD() { InitializeComponent(); }

        private void FrmTBSD_Load(object sender, EventArgs e)
        {
            Mode_Xem();

            // TẠO DỮ LIỆU CHO CÁC CONTROL //

            for (int i = 1; i < 32; i++)
            {
                cbo_ngay.Items.Add(i);
                if (i < 13) { cbo_thang.Items.Add(i); }
            }
            for (int i = DateTime.Now.Year - 35; i <= DateTime.Now.Year; i++)
            {
                cbo_nam.Items.Add(i);
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

            cbo_phong.Text = null;
            cbo_thietbi.Text = null;
            cbo_ngay.Text = null;
            cbo_thang.Text = null;
            cbo_nam.Text = null;
            txt_soluong.Text = "";

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Xem);

            Load_Csdl();
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

        private void Load_CSDL_TB()
        {
            cbo_thietbi.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_tb, ten_tb from thiet_bi where khoa = 0 order by ten_tb asc";

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU THIẾT BỊ";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            DataTable dt = (DataTable)KQ[2];
            if (dt.Rows.Count != 0)
            {
                cbo_thietbi.DataSource = dt;
                cbo_thietbi.DisplayMember = "ten_tb";
                cbo_thietbi.ValueMember = "ma_tb";
            }
        }

        private void Load_Csdl()
        {
            gv_list_data.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select "+
                " ma_tbsd as 'MÃ SỐ', "+
                " (select ten_phong from phong where ma_phong = thiet_bi_su_dung.ma_phong) as 'TÊN PHÒNG', " +
                " (select ten_tb from thiet_bi where ma_tb = thiet_bi_su_dung.ma_tb) as 'TÊN THIẾT BỊ', " +
                " so_luong_tb as 'SỐ LƯỢNG', " +

                " ( " +
                "   (CASE WHEN (LENGTH(ngay_sd) < 2) THEN ('0' || ngay_sd) ELSE ('' || ngay_sd) END) || '/' || " +
                "   (CASE WHEN (LENGTH(thang_sd) < 2) THEN ('0' || thang_sd) ELSE ('' || thang_sd) END) || '/' || nam_sd " +
                " ) as 'NGÀY SỬ DỤNG' " +

                " from thiet_bi_su_dung " +
                " order by nam_sd desc, thang_sd desc, ngay_sd desc, ma_tbsd desc"
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

            // LẤY DỮ LIỆU PHÒNG & TB //

            Load_CSDL_Phong();
            Load_CSDL_TB();
        }

        private void btn_xem_Click(object sender, EventArgs e) { Mode_Xem(); }

        private void btn_them_Click(object sender, EventArgs e)
        {
            // LẤY DỮ LIỆU PHÒNG & TB //

            Load_CSDL_Phong();
            Load_CSDL_TB();

            // CẤU HÌNH THÊM //

            Mode_Them_Sua(true);
            TabP_Them_Sua.Text = "THIẾT LẬP SỬ DỤNG";
            cbo_phong.Focus();
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

                    vmk.SQLITE_QUERY = "delete from thiet_bi_su_dung where lower(ma_tbsd) = @ma_tbsd";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_tbsd", id_item_for_delete.ToLower());
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

            // LẤY DỮ LIỆU PHÒNG & TB //

            Load_CSDL_Phong();
            Load_CSDL_TB();

            // BEGIN //

            Mode_Them_Sua(false);

            TabP_Them_Sua.Text = "CẬP NHẬT THÔNG TIN SỬ DỤNG THIẾT BỊ";

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString().Trim();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_phong, ma_tb, so_luong_tb, ngay_sd, thang_sd, nam_sd " +
                " from thiet_bi_su_dung " +
                " where lower(ma_tbsd) = @ma_tbsd "
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_tbsd", ID_ITEM_FOR_EDIT.ToLower());
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
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA THỐNG KÊ CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            cbo_phong.SelectedValue = dt.Rows[0][0].ToString();
            cbo_thietbi.SelectedValue = dt.Rows[0][1].ToString();
            txt_soluong.Text = dt.Rows[0][2].ToString();
            cbo_ngay.Text = dt.Rows[0][3].ToString();
            cbo_thang.Text = dt.Rows[0][4].ToString();
            cbo_nam.Text = dt.Rows[0][5].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ma_phong = "";
            string ma_tb = "";

            if (cbo_phong.SelectedValue != null)
            {
                ma_phong = cbo_phong.SelectedValue.ToString().Trim();
            }
            if (cbo_thietbi.SelectedValue != null)
            {
                ma_tb = cbo_thietbi.SelectedValue.ToString().Trim();
            }

            string ngay = cbo_ngay.Text.Trim();
            string thang = cbo_thang.Text.Trim();
            string nam = cbo_nam.Text.Trim();
            string so_luong = txt_soluong.Text.Trim();

            txt_soluong.Text = so_luong;

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ma_phong == "" || ma_tb == "" || ngay == "" || thang == "" || nam == "" || so_luong == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");

                if (ma_phong == "") { cbo_phong.Focus(); return; }
                if (ma_tb == "") { cbo_thietbi.Focus(); return; }
                if (ngay == "") { cbo_ngay.Focus(); return; }
                if (thang == "") { cbo_thang.Focus(); return; }
                if (nam == "") { cbo_nam.Focus(); return; }
                if (so_luong == "") { txt_soluong.Focus(); return; }

                return;
            }

            // KIỂM TRA NGÀY THÁNG NĂM //

            if (ManMan89_Main.IS_VALID_DATE(Convert.ToInt16(nam), Convert.ToInt16(thang), Convert.ToInt16(ngay)) == false)
            {
                MessageBox.Show("THỜI GIAN KHÔNG ĐÚNG" + "\n\n" + "NGÀY  " + ngay + "  THÁNG  " + thang + "  NĂM  " + nam + "\n\n" + "KHÔNG TỒN TẠI", "THÔNG BÁO");
                return;
            }

            // KIỂM TRA SỐ //

            double int_number_checked;
            bool int_check_number;

                // SỐ LƯỢNG //

                int_check_number = double.TryParse(so_luong, out int_number_checked);
                if (!int_check_number || int_number_checked <= 0)
                {
                    MessageBox.Show("SỐ LƯỢNG PHẢI LÀ SỐ NGUYÊN VÀ LỚN HƠN 0", "THÔNG BÁO");
                    txt_soluong.Focus();
                    return;
                }
                so_luong = int_number_checked.ToString();

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
                sql_param.Rows.Add("@ma_tb", ma_tb.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from thiet_bi_su_dung where lower(ma_phong) = @ma_phong and lower(ma_tb) = @ma_tb", sql_param) == true)
                {
                    MessageBox.Show("THIẾT BỊ BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG TRONG PHÒNG BẠN CHỌN", "THÔNG BÁO");
                    return;
                }

                // KIỂM TRA SỐ LƯỢNG THÊM & SỐ LƯỢNG TRONG KHO //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_tb", ma_tb.ToLower());
                sql_param.Rows.Add("@so_luong_them", so_luong.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from thiet_bi where ma_tb = @ma_tb " +
                    " and so_luong < ((select sum(so_luong_tb) from thiet_bi_su_dung where ma_tb = @ma_tb) + @so_luong_them) ", sql_param) == true)
                {
                    MessageBox.Show("SỐ LƯỢNG THIẾT BỊ CÒN LẠI TRONG KHO KHÔNG ĐỦ", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into thiet_bi_su_dung (ma_phong, ma_tb, so_luong_tb, ngay_sd, thang_sd, nam_sd) " +
                    " values (@ma_phong, @ma_tb, @so_luong_tb, @ngay_sd, @thang_sd, @nam_sd)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_phong", ma_phong.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_tb", ma_tb.ToLower());
                SQLITE_PARAM.Rows.Add("@so_luong_tb", so_luong.ToLower());
                SQLITE_PARAM.Rows.Add("@ngay_sd", ngay);
                SQLITE_PARAM.Rows.Add("@thang_sd", thang);
                SQLITE_PARAM.Rows.Add("@nam_sd", nam);
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
                sql_param.Rows.Add("@ma_tbsd", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@ma_phong", ma_phong.ToLower());
                sql_param.Rows.Add("@ma_tb", ma_tb.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from thiet_bi_su_dung where lower(ma_phong) = @ma_phong and lower(ma_tb) = @ma_tb and lower(ma_tbsd) <> @ma_tbsd", sql_param) == true)
                {
                    MessageBox.Show("THIẾT BỊ BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG TRONG PHÒNG BẠN CHỌN", "THÔNG BÁO");
                    return;
                }

                // KIỂM TRA SỐ LƯỢNG THÊM & SỐ LƯỢNG TRONG KHO //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_tbsd", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@ma_tb", ma_tb.ToLower());
                sql_param.Rows.Add("@so_luong_cap_nhat", so_luong.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from thiet_bi where ma_tb = @ma_tb " +
                    " and so_luong < ((select sum(so_luong_tb) from thiet_bi_su_dung where ma_tb = @ma_tb and ma_tbsd <> @ma_tbsd) + @so_luong_cap_nhat) ", sql_param) == true)
                {
                    MessageBox.Show("SỐ LƯỢNG THIẾT BỊ CÒN LẠI TRONG KHO KHÔNG ĐỦ", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "update thiet_bi_su_dung set " +
                    " ma_phong = @ma_phong, ma_tb = @ma_tb, so_luong_tb = @so_luong_tb, ngay_sd = @ngay_sd, thang_sd = @thang_sd, nam_sd = @nam_sd " +
                    " where lower(ma_tbsd) = @ma_tbsd "
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_tbsd", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_phong", ma_phong.ToLower());
                SQLITE_PARAM.Rows.Add("@ma_tb", ma_tb.ToLower());
                SQLITE_PARAM.Rows.Add("@so_luong_tb", so_luong.ToLower());
                SQLITE_PARAM.Rows.Add("@ngay_sd", ngay);
                SQLITE_PARAM.Rows.Add("@thang_sd", thang);
                SQLITE_PARAM.Rows.Add("@nam_sd", nam);
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

        private void btn_reload_thietbi_Click(object sender, EventArgs e)
        {
            Load_CSDL_TB();
        }

    }
}
