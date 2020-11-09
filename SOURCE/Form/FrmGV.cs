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
    public partial class FrmGV : Form
    {
        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmGV() { InitializeComponent(); }

        private void FrmGV_Load(object sender, EventArgs e)
        {
            Mode_Xem();

            // TẠO DỮ LIỆU CHO CÁC CONTROL //

            for (int i = 1; i < 32; i++)
            {
                cbo_ngay.Items.Add(i);
                if (i < 13) { cbo_thang.Items.Add(i); }
            }
            for (int i = DateTime.Now.Year - 99; i <= DateTime.Now.Year - 18; i++)
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

        private void Mode_Xem()
        {
            STATUS_THEM_SUA = "";

            btn_xem.Enabled = true;
            btn_them.Enabled = true;
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;

            txt_hoten.Text = "";
            cbo_ngay.Text = null;
            cbo_thang.Text = null;
            cbo_nam.Text = null;
            cbo_gioitinh.Text = null;
            txt_email.Text = "";
            txt_sdt.Text = "";
            txt_diachi.Text = "";

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
                " ma_gv as 'MÃ SỐ', "+
                " ten_gv as 'HỌ & TÊN', " +

                " ( " + 
                "   (CASE WHEN (LENGTH(ngay_sinh) < 2) THEN ('0' || ngay_sinh) ELSE ('' || ngay_sinh) END) || '/' || " +
                "   (CASE WHEN (LENGTH(thang_sinh) < 2) THEN ('0' || thang_sinh) ELSE ('' || thang_sinh) END) || '/' || nam_sinh " +
                " ) as 'NGÀY SINH', " +

                " gioi_tinh as 'GIỚI TÍNH', " +
                " email as 'EMAIL', " +
                " sdt as 'SĐT', " +
                " dia_chi as 'ĐỊA CHỈ' " + 
                " from giao_vien " +
                " order by nam_sinh desc, thang_sinh desc, ngay_sinh desc, ma_gv desc"
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
            TabP_Them_Sua.Text = "THÊM GIÁO VIÊN";
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
                    // TIẾN HÀNH XÓA TRONG CSDL //

                    ManMan89_CSDL vmk = new ManMan89_CSDL();

                    vmk.SQLITE_QUERY = "delete from giao_vien where lower(ma_gv) = @ma_gv";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_gv", id_item_for_delete.ToLower());
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

            TabP_Them_Sua.Text = "SỬA THÔNG TIN GIÁO VIÊN";

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString().Trim();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ten_gv, ngay_sinh, thang_sinh, nam_sinh, gioi_tinh, email, sdt, dia_chi" +
                " from giao_vien " + 
                " where lower(ma_gv) = @ma_gv "
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_gv", ID_ITEM_FOR_EDIT.ToLower());
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
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA GIÁO VIÊN CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            txt_hoten.Text = dt.Rows[0][0].ToString();
            cbo_ngay.Text = dt.Rows[0][1].ToString();
            cbo_thang.Text = dt.Rows[0][2].ToString();
            cbo_nam.Text = dt.Rows[0][3].ToString();
            if (((bool)dt.Rows[0][4]) == false) { cbo_gioitinh.Text = "Nam"; } else { cbo_gioitinh.Text = "Nữ"; }
            txt_email.Text = dt.Rows[0][5].ToString();
            txt_sdt.Text = dt.Rows[0][6].ToString();
            txt_diachi.Text = dt.Rows[0][7].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ho_ten = txt_hoten.Text.Trim();
            string ngay = cbo_ngay.Text.Trim();
            string thang = cbo_thang.Text.Trim();
            string nam = cbo_nam.Text.Trim();
            string gioi_tinh = cbo_gioitinh.Text.Trim();
            string email = txt_email.Text.Trim();
            string sdt = txt_sdt.Text.Trim();
            string dia_chi = txt_diachi.Text.Trim();

            txt_hoten.Text = ho_ten;
            txt_email.Text = email;
            txt_sdt.Text = sdt;
            txt_diachi.Text = dia_chi;

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ho_ten == "" || ngay == "" || thang == "" || nam == "" || gioi_tinh == "" || email == "" || sdt == "" || dia_chi == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");

                if (ho_ten == "") { txt_hoten.Focus(); return; }
                if (ngay == "") { cbo_ngay.Focus(); return; }
                if (thang == "") { cbo_thang.Focus(); return; }
                if (nam == "") { cbo_nam.Focus(); return; }
                if (gioi_tinh == "") { cbo_gioitinh.Focus(); return; }
                if (email == "") { txt_email.Focus(); return; }
                if (sdt == "") { txt_sdt.Focus(); return; }
                if (dia_chi == "") { txt_diachi.Focus(); return; }

                return;
            }

            if (gioi_tinh.ToLower() == "nam") { gioi_tinh = "0"; } else { gioi_tinh = "1"; }

            // KIỂM TRA TÊN VIỆT NAM //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(ho_ten) == false)
            {
                MessageBox.Show("HỌ & TÊN CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_hoten.Focus();
                return;
            }

            // KIỂM TRA NGÀY THÁNG NĂM //

            if (ManMan89_Main.IS_VALID_DATE(Convert.ToInt16(nam), Convert.ToInt16(thang), Convert.ToInt16(ngay)) == false)
            {
                MessageBox.Show("THỜI GIAN KHÔNG ĐÚNG" + "\n\n" + "NGÀY  " + ngay + "  THÁNG  " + thang + "  NĂM  " + nam + "\n\n" + "KHÔNG TỒN TẠI", "THÔNG BÁO");
                return;
            }

            // KIỂM TRA EMAIL //

            if (ManMan89_Main.IS_VALID_EMAIL_ADDRESS(email) == false)
            {
                MessageBox.Show("EMAIL KHÔNG ĐÚNG", "THÔNG BÁO");
                txt_email.Focus();
                return;
            }

            // KIỂM TRA SỐ //

            Int64 int_number_checked;
            bool int_check_number;
            
                // ĐIỆN THOẠI //

                int_check_number = Int64.TryParse(sdt, out int_number_checked);
                if (!int_check_number || int_number_checked <= 0)
                {
                    MessageBox.Show("ĐIỆN THOẠI PHẢI LÀ SỐ NGUYÊN VÀ LỚN HƠN 0", "THÔNG BÁO");
                    txt_sdt.Focus();
                    return;
                }
                sdt = int_number_checked.ToString();

            // KIỂM TRA ĐỊA CHỈ VIỆT NAM //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(dia_chi,true,true) == false)
            {
                MessageBox.Show("ĐỊA CHỈ CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_hoten.Focus();
                return;
            }

            #endregion

            #region THÊM DỮ LIỆU

            if (STATUS_THEM_SUA == "them")
            {
                // KIỂM TRA TỒN TẠI //

                DataTable sql_param = new DataTable();
                sql_param.Columns.Add("key", typeof(String));
                sql_param.Columns.Add("value", typeof(String));

                // KIỂM TRA SĐT //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@sdt", sdt.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from giao_vien where lower(sdt) = @sdt", sql_param) == true)
                {
                    MessageBox.Show("SỐ ĐIỆN THOẠI BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // KIỂM TRA EMAIL //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@email", email.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from giao_vien where lower(email) = @email", sql_param) == true)
                {
                    MessageBox.Show("EMAIL BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into giao_vien (ten_gv, ngay_sinh, thang_sinh, nam_sinh, gioi_tinh, email, sdt, dia_chi) " +
                    " values (@ten_gv, @ngay_sinh, @thang_sinh, @nam_sinh, @gioi_tinh, @email, @sdt, @dia_chi)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ten_gv", ho_ten);
                SQLITE_PARAM.Rows.Add("@ngay_sinh", ngay);
                SQLITE_PARAM.Rows.Add("@thang_sinh", thang);
                SQLITE_PARAM.Rows.Add("@nam_sinh", nam);
                SQLITE_PARAM.Rows.Add("@gioi_tinh", gioi_tinh);
                SQLITE_PARAM.Rows.Add("@email", email.ToLower());
                SQLITE_PARAM.Rows.Add("@sdt", sdt.ToLower());
                SQLITE_PARAM.Rows.Add("@dia_chi", dia_chi);
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

                // KIỂM TRA SĐT //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_gv", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@sdt", sdt.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from giao_vien where lower(sdt) = @sdt and lower(ma_gv) <> @ma_gv", sql_param) == true)
                {
                    MessageBox.Show("SỐ ĐIỆN THOẠI BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // KIỂM TRA EMAIL //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_gv", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@email", email.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from giao_vien where lower(email) = @email and lower(ma_gv) <> @ma_gv", sql_param) == true)
                {
                    MessageBox.Show("EMAIL BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "update giao_vien set " +
                    " ten_gv = @ten_gv, ngay_sinh = @ngay_sinh, thang_sinh = @thang_sinh, nam_sinh = @nam_sinh, " + 
                    " gioi_tinh = @gioi_tinh, email = @email, sdt = @sdt, dia_chi = @dia_chi " +
                    " where lower(ma_gv) = @ma_gv "
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_gv", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ten_gv", ho_ten);
                SQLITE_PARAM.Rows.Add("@ngay_sinh", ngay);
                SQLITE_PARAM.Rows.Add("@thang_sinh", thang);
                SQLITE_PARAM.Rows.Add("@nam_sinh", nam);
                SQLITE_PARAM.Rows.Add("@gioi_tinh", gioi_tinh);
                SQLITE_PARAM.Rows.Add("@email", email.ToLower());
                SQLITE_PARAM.Rows.Add("@sdt", sdt.ToLower());
                SQLITE_PARAM.Rows.Add("@dia_chi", dia_chi);
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

        private void txt_sdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != Convert.ToChar(Keys.Back))
            {
                if (!char.IsNumber(e.KeyChar)) { e.Handled = true; }
            }
        }

    }
}
