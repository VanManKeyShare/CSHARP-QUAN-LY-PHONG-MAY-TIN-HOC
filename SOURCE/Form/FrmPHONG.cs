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
    public partial class FrmPHONG : Form
    {
        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmPHONG() { InitializeComponent(); }

        private void FrmPHONG_Load(object sender, EventArgs e)
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

            txt_tenphong.Text = "";
            cbo_ngay.Text = null;
            cbo_thang.Text = null;
            cbo_nam.Text = null;
            txt_chieudai.Text = "";
            txt_chieurong.Text = "";
            txt_chieucao.Text = "";

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Xem);

            Load_Csdl();
        }

        private void Load_Csdl()
        {
            gv_list_data.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select "+
                " ma_phong as 'MÃ SỐ', "+
                " ten_phong as 'TÊN PHÒNG', " +

                " ( " +
                "   (CASE WHEN (LENGTH(ngay_lap) < 2) THEN ('0' || ngay_lap) ELSE ('' || ngay_lap) END) || '/' || " +
                "   (CASE WHEN (LENGTH(thang_lap) < 2) THEN ('0' || thang_lap) ELSE ('' || thang_lap) END) || '/' || nam_lap " +
                " ) as 'NGÀY LẬP', " +

                " chieu_dai as 'CHIỀU DÀI', " +
                " chieu_rong as 'CHIỀU RỘNG', " +
                " chieu_cao as 'CHIỀU CAO' " +

                " from phong " +
                " order by nam_lap desc, thang_lap desc, ngay_lap desc, ma_phong desc"
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
        }

        private void btn_xem_Click(object sender, EventArgs e) { Mode_Xem(); }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Mode_Them_Sua(true);
            TabP_Them_Sua.Text = "THÊM PHÒNG";
            txt_tenphong.Focus();
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

                    vmk.SQLITE_QUERY = "delete from phong where lower(ma_phong) = @ma_phong";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_phong", id_item_for_delete.ToLower());
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

            TabP_Them_Sua.Text = "SỬA THÔNG TIN PHÒNG";

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString().Trim();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ten_phong, ngay_lap, thang_lap, nam_lap, chieu_dai, chieu_rong, chieu_cao " +
                " from phong " +
                " where lower(ma_phong) = @ma_phong "
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_phong", ID_ITEM_FOR_EDIT.ToLower());
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
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA PHÒNG CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            txt_tenphong.Text = dt.Rows[0][0].ToString();
            cbo_ngay.Text = dt.Rows[0][1].ToString();
            cbo_thang.Text = dt.Rows[0][2].ToString();
            cbo_nam.Text = dt.Rows[0][3].ToString();
            txt_chieudai.Text = dt.Rows[0][4].ToString();
            txt_chieurong.Text = dt.Rows[0][5].ToString();
            txt_chieucao.Text = dt.Rows[0][6].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ten_phong = txt_tenphong.Text.Trim();
            string ngay = cbo_ngay.Text.Trim();
            string thang = cbo_thang.Text.Trim();
            string nam = cbo_nam.Text.Trim();
            string chieu_dai = txt_chieudai.Text.Trim();
            string chieu_rong = txt_chieurong.Text.Trim();
            string chieu_cao = txt_chieucao.Text.Trim();

            txt_tenphong.Text = ten_phong;
            txt_chieudai.Text = chieu_dai;
            txt_chieurong.Text = chieu_rong;
            txt_chieucao.Text = chieu_cao;

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ten_phong == "" || ngay == "" || thang == "" || nam == "" || chieu_dai == "" || chieu_rong == "" || chieu_cao == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");

                if (ten_phong == "") { txt_tenphong.Focus(); return; }
                if (ngay == "") { cbo_ngay.Focus(); return; }
                if (thang == "") { cbo_thang.Focus(); return; }
                if (nam == "") { cbo_nam.Focus(); return; }
                if (chieu_dai == "") { txt_chieudai.Focus(); return; }
                if (chieu_rong == "") { txt_chieurong.Focus(); return; }
                if (chieu_cao == "") { txt_chieucao.Focus(); return; }

                return;
            }

            // KIỂM TRA TÊN VIỆT NAM //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(ten_phong,true) == false)
            {
                MessageBox.Show("TÊN PHÒNG CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_tenphong.Focus();
                return;
            }

            // KIỂM TRA NGÀY THÁNG NĂM //

            if (ManMan89_Main.IS_VALID_DATE(Convert.ToInt16(nam), Convert.ToInt16(thang), Convert.ToInt16(ngay)) == false)
            {
                MessageBox.Show("THỜI GIAN KHÔNG ĐÚNG" + "\n\n" + "NGÀY  " + ngay + "  THÁNG  " + thang + "  NĂM  " + nam + "\n\n" + "KHÔNG TỒN TẠI", "THÔNG BÁO");
                return;
            }

            // KIỂM TRA SỐ //

            double double_number_checked;
            bool double_check_number;

                // CHIỀU DÀI //

                double_check_number = double.TryParse(chieu_dai, out double_number_checked);
                if (!double_check_number || double_number_checked <= 0)
                {
                    MessageBox.Show("CHIỀU DÀI PHẢI LÀ SỐ THỰC VÀ LỚN HƠN 0", "THÔNG BÁO");
                    txt_chieudai.Focus();
                    return;
                }
                chieu_dai = double_number_checked.ToString();

                // CHIỀU RỘNG //

                double_check_number = double.TryParse(chieu_rong, out double_number_checked);
                if (!double_check_number || double_number_checked <= 0)
                {
                    MessageBox.Show("CHIỀU RỘNG PHẢI LÀ SỐ THỰC VÀ LỚN HƠN 0", "THÔNG BÁO");
                    txt_chieurong.Focus();
                    return;
                }
                chieu_rong = double_number_checked.ToString();

                // CHIỀU CAO //

                double_check_number = double.TryParse(chieu_cao, out double_number_checked);
                if (!double_check_number || double_number_checked <= 0)
                {
                    MessageBox.Show("CHIỀU CAO PHẢI LÀ SỐ THỰC VÀ LỚN HƠN 0", "THÔNG BÁO");
                    txt_chieucao.Focus();
                    return;
                }
                chieu_cao = double_number_checked.ToString();

            #endregion

            #region THÊM DỮ LIỆU

            // BEGIN //

            if (STATUS_THEM_SUA == "them")
            {
                // KIỂM TRA TỒN TẠI //

                DataTable sql_param = new DataTable();
                sql_param.Columns.Add("key", typeof(String));
                sql_param.Columns.Add("value", typeof(String));

                // KIỂM TRA TÊN PHÒNG //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ten_phong", ten_phong.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from phong where lower(ten_phong) = @ten_phong", sql_param) == true)
                {
                    MessageBox.Show("TÊN PHÒNG BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into phong (ten_phong, ngay_lap, thang_lap, nam_lap, chieu_dai, chieu_rong, chieu_cao) " +
                    " values (@ten_phong, @ngay_lap, @thang_lap, @nam_lap, @chieu_dai, @chieu_rong, @chieu_cao)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ten_phong", ten_phong);
                SQLITE_PARAM.Rows.Add("@ngay_lap", ngay);
                SQLITE_PARAM.Rows.Add("@thang_lap", thang);
                SQLITE_PARAM.Rows.Add("@nam_lap", nam);
                SQLITE_PARAM.Rows.Add("@chieu_dai", chieu_dai);
                SQLITE_PARAM.Rows.Add("@chieu_rong", chieu_rong);
                SQLITE_PARAM.Rows.Add("@chieu_cao", chieu_cao);
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

                // KIỂM TRA TÊN PHÒNG //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_phong", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@ten_phong", ten_phong.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from phong where lower(ten_phong) = @ten_phong and lower(ma_phong) <> @ma_phong", sql_param) == true)
                {
                    MessageBox.Show("TÊN PHÒNG BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "update phong set " +
                    " ten_phong = @ten_phong, ngay_lap = @ngay_lap, thang_lap = @thang_lap, nam_lap = @nam_lap, " +
                    " chieu_dai = @chieu_dai, chieu_rong = @chieu_rong, chieu_cao = @chieu_cao " +
                    " where lower(ma_phong) = @ma_phong "
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_phong", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ten_phong", ten_phong);
                SQLITE_PARAM.Rows.Add("@ngay_lap", ngay);
                SQLITE_PARAM.Rows.Add("@thang_lap", thang);
                SQLITE_PARAM.Rows.Add("@nam_lap", nam);
                SQLITE_PARAM.Rows.Add("@chieu_dai", chieu_dai);
                SQLITE_PARAM.Rows.Add("@chieu_rong", chieu_rong);
                SQLITE_PARAM.Rows.Add("@chieu_cao", chieu_cao);
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

        private void txt_chieudai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (!char.IsNumber(e.KeyChar)) { e.Handled = true; }
            }
        }

        private void txt_chieurong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (!char.IsNumber(e.KeyChar)) { e.Handled = true; }
            }
        }

        private void txt_chieucao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (!char.IsNumber(e.KeyChar)) { e.Handled = true; }
            }
        }

    }
}
