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
    public partial class FrmTB : Form
    {
        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmTB() { InitializeComponent(); }

        private void FrmTB_Load(object sender, EventArgs e)
        {
            Mode_Xem();
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
            btn_loaithietbi.Enabled = false;

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
            btn_loaithietbi.Enabled = true;

            txt_tentb.Text = "";
            cbo_loaitb.Text = null;
            txt_mota.Text = "";
            txt_soluong.Text = "";
            chk_khoa.Checked = false;
            txt_lydokhoa.Text = "";

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Xem);

            Load_Csdl();
        }

        private void Load_CSDL_LTB()
        {
            cbo_loaitb.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ma_loai_tb, ten_loai_tb from thiet_bi_loai order by ten_loai_tb asc";

            ArrayList KQ = vmk.SQLITE_SELECT();

            if (KQ[0].ToString() == "ERROR")
            {
                FrmMsg msg = new FrmMsg();
                msg.tieu_de = "THÔNG BÁO";
                msg.noi_dung = "KHÔNG LẤY ĐƯỢC DỮ LIỆU LOẠI THIẾT BỊ";
                msg.noi_dung_chi_tiet = KQ[1].ToString();
                msg.ShowDialog();
                return;
            }

            DataTable dt_LoaiTB = (DataTable)KQ[2];
            if (dt_LoaiTB.Rows.Count != 0)
            {
                cbo_loaitb.DataSource = dt_LoaiTB;
                cbo_loaitb.DisplayMember = "ten_loai_tb";
                cbo_loaitb.ValueMember = "ma_loai_tb";
            }
        }

        private void Load_Csdl()
        {
            gv_list_data.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select "+
                " ma_tb as 'MÃ SỐ', "+
                " ten_tb as 'TÊN THIẾT BỊ', " +
                " (select ten_loai_tb from thiet_bi_loai where ma_loai_tb = thiet_bi.ma_loai_tb) as 'LOẠI THIẾT BỊ', " +
                " mo_ta as 'MÔ TẢ', " +
                " so_luong as 'SỐ LƯỢNG', " +
                " khoa as 'KHÓA', " +
                " ly_do_khoa as 'LÝ DO KHÓA' " +
                " from thiet_bi " +
                " order by khoa asc, ma_tb desc"
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

            // TẠO DANH SÁCH LOẠI THIẾT BỊ //

            Load_CSDL_LTB();

        }

        private void btn_xem_Click(object sender, EventArgs e) { Mode_Xem(); }

        private void btn_them_Click(object sender, EventArgs e)
        {
            Mode_Them_Sua(true);
            TabP_Them_Sua.Text = "THÊM THIẾT BỊ";
            Load_CSDL_LTB();
            txt_tentb.Focus();
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

                    vmk.SQLITE_QUERY = "delete from thiet_bi where lower(ma_tb) = @ma_tb";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_tb", id_item_for_delete.ToLower());
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

            TabP_Them_Sua.Text = "SỬA THÔNG TIN THIẾT BỊ";

            Load_CSDL_LTB();

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString().Trim();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ten_tb, ma_loai_tb, mo_ta, so_luong, khoa, ly_do_khoa " +
                " from thiet_bi " +
                " where lower(ma_tb) = @ma_tb "
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_tb", ID_ITEM_FOR_EDIT);
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
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA THIẾT BỊ CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            txt_tentb.Text = dt.Rows[0][0].ToString();
            cbo_loaitb.SelectedValue = dt.Rows[0][1].ToString();
            txt_mota.Text = dt.Rows[0][2].ToString();
            txt_soluong.Text = dt.Rows[0][3].ToString();
            chk_khoa.Checked = (bool)dt.Rows[0][4];
            txt_lydokhoa.Text = dt.Rows[0][5].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ten_tb = txt_tentb.Text.Trim();

            string ma_loai_tb = "";
            if (cbo_loaitb.SelectedValue != null)
            {
                ma_loai_tb = cbo_loaitb.SelectedValue.ToString().Trim();
            }

            string mo_ta = txt_mota.Text.Trim();
            string so_luong = txt_soluong.Text.Trim();
            string khoa = "0";
            string ly_do_khoa = txt_lydokhoa.Text.Trim();

            txt_tentb.Text = ten_tb;
            txt_mota.Text = mo_ta;
            txt_soluong.Text = so_luong;
            txt_lydokhoa.Text = ly_do_khoa;

            if (chk_khoa.Checked == true) { khoa = "1"; } else { khoa = "0"; }

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ten_tb == "" || ma_loai_tb == "" || mo_ta == "" || so_luong == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");

                if (ten_tb == "") { txt_tentb.Focus(); return; }
                if (ma_loai_tb == "") { cbo_loaitb.Focus(); return; }
                if (mo_ta == "") { txt_soluong.Focus(); return; }
                if (so_luong == "") { txt_mota.Focus(); return; }

                return;
            }

            if (chk_khoa.Checked == false) { ly_do_khoa = ""; }
            if (chk_khoa.Checked == true && ly_do_khoa == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP LÝ DO KHÓA", "THÔNG BÁO");
                if (ly_do_khoa == "") { txt_lydokhoa.Focus(); return; }
                return;
            }

            // KIỂM TRA TÊN THEO CHUẨN VIỆT NAM //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(ten_tb, true) == false)
            {
                MessageBox.Show("TÊN THIẾT BỊ CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_tentb.Focus();
                return;
            }

            // KIỂM TRA MÔ TẢ THEO CHUẨN VIỆT NAM //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(mo_ta, true, true) == false)
            {
                MessageBox.Show("MÔ TẢ THIẾT BỊ CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_mota.Focus();
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
                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into thiet_bi (ten_tb, ma_loai_tb, mo_ta, so_luong, khoa, ly_do_khoa) " +
                    " values (@ten_tb, @ma_loai_tb, @mo_ta, @so_luong, @khoa, @ly_do_khoa)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ten_tb", ten_tb);
                SQLITE_PARAM.Rows.Add("@ma_loai_tb", ma_loai_tb.ToLower());
                SQLITE_PARAM.Rows.Add("@mo_ta", mo_ta);
                SQLITE_PARAM.Rows.Add("@so_luong", so_luong.ToLower());
                SQLITE_PARAM.Rows.Add("@khoa", khoa);
                SQLITE_PARAM.Rows.Add("@ly_do_khoa", ly_do_khoa);
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
                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "update thiet_bi set " +
                    " ten_tb = @ten_tb, ma_loai_tb = @ma_loai_tb, mo_ta = @mo_ta, so_luong = @so_luong, khoa = @khoa, ly_do_khoa = @ly_do_khoa " +
                    " where lower(ma_tb) = @ma_tb "
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_tb", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ten_tb", ten_tb);
                SQLITE_PARAM.Rows.Add("@ma_loai_tb", ma_loai_tb.ToLower());
                SQLITE_PARAM.Rows.Add("@mo_ta", mo_ta);
                SQLITE_PARAM.Rows.Add("@so_luong", so_luong.ToLower());
                SQLITE_PARAM.Rows.Add("@khoa", khoa);
                SQLITE_PARAM.Rows.Add("@ly_do_khoa", ly_do_khoa);
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

        private void btn_loaithietbi_Click(object sender, EventArgs e)
        {
            ((FrmMain)this.MdiParent).Open_Form_LoaiThietBi();
        }

        private void chk_khoa_CheckedChanged(object sender, EventArgs e) { txt_lydokhoa.Enabled = ((CheckBox)sender).Checked; }

        private void chk_khoa_Click(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true) { txt_lydokhoa.Focus(); }
        }

        private void btn_reload_ltb_Click(object sender, EventArgs e) { Load_CSDL_LTB(); }

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

    }
}
