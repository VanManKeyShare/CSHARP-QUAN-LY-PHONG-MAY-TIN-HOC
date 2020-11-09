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
    public partial class FrmTH : Form
    {
        string ID_ITEM_FOR_EDIT = "";

        string STATUS_THEM_SUA = "";

        public FrmTH() { InitializeComponent(); }

        private void FrmTH_Load(object sender, EventArgs e)
        {
            Mode_Xem();

            // TẠO DỮ LIỆU CHO CÁC CONTROL //

            for (int i = 0; i < 60; i++)
            {
                cbo_phut_begin.Items.Add(i);
                cbo_phut_end.Items.Add(i);
                if (i < 24)
                {
                    cbo_gio_begin.Items.Add(i);
                    cbo_gio_end.Items.Add(i);
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

            txt_tenth.Text = "";
            cbo_gio_begin.Text = null;
            cbo_phut_begin.Text = null;
            cbo_gio_end.Text = null;
            cbo_phut_end.Text = null;

            Remove_All_TabPages();

            TabC1.TabPages.Add(TabP_Xem);

            Load_Csdl();
        }

        private void Load_Csdl()
        {
            gv_list_data.DataSource = null;

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select "+
                " ma_th as 'MÃ SỐ', "+
                " ten_th as 'TÊN TIẾT HỌC', " +

                " gio_begin as 'GIỜ BẮT ĐẦU', " +
                " phut_begin as 'PHÚT BẮT ĐẦU', " +

                " gio_end as 'GIỜ KẾT THÚC', " +
                " phut_end as 'PHÚT KẾT THÚC' " +

                " from tiet_hoc " +
                " order by gio_begin asc, phut_begin asc, gio_end asc, phut_end asc, ma_th desc"
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
            TabP_Them_Sua.Text = "THÊM TIẾT HỌC";
            txt_tenth.Focus();
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

                    vmk.SQLITE_QUERY = "delete from tiet_hoc where lower(ma_th) = @ma_th";

                    DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                    SQLITE_PARAM.Rows.Add("@ma_th", id_item_for_delete.ToLower());
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

            TabP_Them_Sua.Text = "SỬA THÔNG TIN TIẾT HỌC";

            ID_ITEM_FOR_EDIT = gv_list_data.SelectedRows[0].Cells[0].Value.ToString().Trim();

            // TIẾN HÀNH LẤY DỮ LIỆU TỪ CSDL ĐƯA LÊN GIAO DIỆN //

            ManMan89_CSDL vmk = new ManMan89_CSDL();

            vmk.SQLITE_QUERY = "select ten_th, gio_begin, phut_begin, gio_end, phut_end " +
                " from tiet_hoc " +
                " where lower(ma_th) = @ma_th "
            ;

            DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
            SQLITE_PARAM.Rows.Add("@ma_th", ID_ITEM_FOR_EDIT.ToLower());
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
                MessageBox.Show("KHÔNG TÌM THẤY DỮ LIỆU CỦA TIẾT HỌC CÓ MÃ SỐ " + ID_ITEM_FOR_EDIT.ToUpper(), "THÔNG BÁO");
                Mode_Xem();
                return;
            }

            txt_tenth.Text = dt.Rows[0][0].ToString();
            cbo_gio_begin.Text = dt.Rows[0][1].ToString();
            cbo_phut_begin.Text = dt.Rows[0][2].ToString();
            cbo_gio_end.Text = dt.Rows[0][3].ToString();
            cbo_phut_end.Text = dt.Rows[0][4].ToString();
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            string ten_th = txt_tenth.Text.Trim();
            string gio_begin = cbo_gio_begin.Text.Trim();
            string phut_begin = cbo_phut_begin.Text.Trim();
            string gio_end = cbo_gio_end.Text.Trim();
            string phut_end = cbo_phut_end.Text.Trim();

            txt_tenth.Text = ten_th;

            #region KIỂM TRA DỮ LIỆU

            // KIỂM TRA DỮ LIỆU //

            if (ten_th == "" || gio_begin == "" || phut_begin == "" || gio_end == "" || phut_end == "")
            {
                MessageBox.Show("BẠN CHƯA NHẬP ĐẦY ĐỦ DỮ LIỆU", "THÔNG BÁO");

                if (ten_th == "") { txt_tenth.Focus(); return; }
                if (gio_begin == "") { cbo_gio_begin.Focus(); return; }
                if (phut_begin == "") { cbo_phut_begin.Focus(); return; }
                if (gio_end == "") { cbo_gio_end.Focus(); return; }
                if (phut_end == "") { cbo_phut_end.Focus(); return; }

                return;
            }

            // KIỂM TRA TÊN TIẾT HỌC //

            if (ManMan89_Main.IS_CHARACTERS_VIET_NAM(ten_th, true) == false)
            {
                MessageBox.Show("TÊN TIẾT HỌC CÓ CHỨA KÝ TỰ KHÔNG HỢP LỆ", "THÔNG BÁO");
                txt_tenth.Focus();
                return;
            }

            // KIỂM TRA THỜI GIAN //

            if(Convert.ToInt16(gio_end) < Convert.ToInt16(gio_begin))
            {
                MessageBox.Show("GIỜ KẾT THÚC PHẢI LỚN HƠN HOẶC BẰNG GIỜ BẮT ĐẦU", "THÔNG BÁO");
                cbo_gio_begin.Focus();
                return;
            }

            if (Convert.ToInt16(gio_end) == Convert.ToInt16(gio_begin))
            {
                if (Convert.ToInt16(phut_end) <= Convert.ToInt16(phut_begin))
                {
                    MessageBox.Show("PHÚT KẾT THÚC PHẢI LỚN HƠN PHÚT BẮT ĐẦU", "THÔNG BÁO");
                    cbo_gio_begin.Focus();
                    return;
                }
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

                // KIỂM TRA TÊN TIẾT HỌC //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ten_th", ten_th.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from tiet_hoc where lower(ten_th) = @ten_th", sql_param) == true)
                {
                    MessageBox.Show("TÊN TIẾT HỌC BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "insert into tiet_hoc (ten_th, gio_begin, phut_begin, gio_end, phut_end) " +
                    " values (@ten_th, @gio_begin, @phut_begin, @gio_end, @phut_end)"
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ten_th", ten_th);
                SQLITE_PARAM.Rows.Add("@gio_begin", gio_begin);
                SQLITE_PARAM.Rows.Add("@phut_begin", phut_begin);
                SQLITE_PARAM.Rows.Add("@gio_end", gio_end);
                SQLITE_PARAM.Rows.Add("@phut_end", phut_end);
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

                // KIỂM TRA TÊN TIẾT HỌC //

                sql_param.Rows.Clear();
                sql_param.Rows.Add("@ma_th", ID_ITEM_FOR_EDIT.ToLower());
                sql_param.Rows.Add("@ten_th", ten_th.ToLower());

                if (SQLITE_CHECK_EXISTS("select * from tiet_hoc where lower(ten_th) = @ten_th and lower(ma_th) <> @ma_th", sql_param) == true)
                {
                    MessageBox.Show("TÊN TIẾT HỌC BẠN CHỌN ĐÃ ĐƯỢC SỬ DỤNG", "THÔNG BÁO");
                    return;
                }

                // TIẾN HÀNH LƯU DỮ LIỆU //

                ManMan89_CSDL vmk = new ManMan89_CSDL();

                vmk.SQLITE_QUERY = "update tiet_hoc set " +
                    " ten_th = @ten_th, gio_begin = @gio_begin, phut_begin = @phut_begin, gio_end = @gio_end, phut_end = @phut_end " +
                    " where lower(ma_th) = @ma_th "
                ;

                DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
                SQLITE_PARAM.Rows.Add("@ma_th", ID_ITEM_FOR_EDIT.ToLower());
                SQLITE_PARAM.Rows.Add("@ten_th", ten_th);
                SQLITE_PARAM.Rows.Add("@gio_begin", gio_begin);
                SQLITE_PARAM.Rows.Add("@phut_begin", phut_begin);
                SQLITE_PARAM.Rows.Add("@gio_end", gio_end);
                SQLITE_PARAM.Rows.Add("@phut_end", phut_end);
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
