namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.vmkMenu = new System.Windows.Forms.MenuStrip();
            this.vmkMenu_hethong = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_hethong_login = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_hethong_logout = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_hethong_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_cauhinh = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_cauhinh_nhanvien = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_cauhinh_giaovien = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_cauhinh_phong = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_cauhinh_thietbi = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_cauhinh_tiethoc = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_quanly = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_quanly_sudungthietbi = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_quanly_sudungphong = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu_thongtin = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // vmkMenu
            // 
            this.vmkMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vmkMenu_hethong,
            this.vmkMenu_cauhinh,
            this.vmkMenu_quanly,
            this.vmkMenu_thongtin});
            this.vmkMenu.Location = new System.Drawing.Point(0, 0);
            this.vmkMenu.Name = "vmkMenu";
            this.vmkMenu.Size = new System.Drawing.Size(929, 24);
            this.vmkMenu.TabIndex = 0;
            // 
            // vmkMenu_hethong
            // 
            this.vmkMenu_hethong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vmkMenu_hethong_login,
            this.vmkMenu_hethong_logout,
            this.vmkMenu_hethong_exit});
            this.vmkMenu_hethong.Name = "vmkMenu_hethong";
            this.vmkMenu_hethong.Size = new System.Drawing.Size(79, 20);
            this.vmkMenu_hethong.Text = "&HỆ THỐNG";
            // 
            // vmkMenu_hethong_login
            // 
            this.vmkMenu_hethong_login.Name = "vmkMenu_hethong_login";
            this.vmkMenu_hethong_login.Size = new System.Drawing.Size(143, 22);
            this.vmkMenu_hethong_login.Text = "ĐĂNG &NHẬP";
            this.vmkMenu_hethong_login.Click += new System.EventHandler(this.vmkMenu_hethong_login_Click);
            // 
            // vmkMenu_hethong_logout
            // 
            this.vmkMenu_hethong_logout.Name = "vmkMenu_hethong_logout";
            this.vmkMenu_hethong_logout.Size = new System.Drawing.Size(143, 22);
            this.vmkMenu_hethong_logout.Text = "ĐĂNG &XUẤT";
            this.vmkMenu_hethong_logout.Click += new System.EventHandler(this.vmkMenu_hethong_logout_Click);
            // 
            // vmkMenu_hethong_exit
            // 
            this.vmkMenu_hethong_exit.Name = "vmkMenu_hethong_exit";
            this.vmkMenu_hethong_exit.Size = new System.Drawing.Size(143, 22);
            this.vmkMenu_hethong_exit.Text = "&THOÁT";
            this.vmkMenu_hethong_exit.Click += new System.EventHandler(this.vmkMenu_hethong_exit_Click);
            // 
            // vmkMenu_cauhinh
            // 
            this.vmkMenu_cauhinh.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vmkMenu_cauhinh_nhanvien,
            this.vmkMenu_cauhinh_giaovien,
            this.vmkMenu_cauhinh_phong,
            this.vmkMenu_cauhinh_thietbi,
            this.vmkMenu_cauhinh_tiethoc});
            this.vmkMenu_cauhinh.Name = "vmkMenu_cauhinh";
            this.vmkMenu_cauhinh.Size = new System.Drawing.Size(93, 20);
            this.vmkMenu_cauhinh.Text = "&1 - CẤU HÌNH";
            // 
            // vmkMenu_cauhinh_nhanvien
            // 
            this.vmkMenu_cauhinh_nhanvien.Name = "vmkMenu_cauhinh_nhanvien";
            this.vmkMenu_cauhinh_nhanvien.Size = new System.Drawing.Size(154, 22);
            this.vmkMenu_cauhinh_nhanvien.Text = "&1 - NHÂN VIÊN";
            this.vmkMenu_cauhinh_nhanvien.Click += new System.EventHandler(this.vmkMenu_cauhinh_nhanvien_Click);
            // 
            // vmkMenu_cauhinh_giaovien
            // 
            this.vmkMenu_cauhinh_giaovien.Name = "vmkMenu_cauhinh_giaovien";
            this.vmkMenu_cauhinh_giaovien.Size = new System.Drawing.Size(154, 22);
            this.vmkMenu_cauhinh_giaovien.Text = "&2 - GIÁO VIÊN";
            this.vmkMenu_cauhinh_giaovien.Click += new System.EventHandler(this.vmkMenu_cauhinh_giaovien_Click);
            // 
            // vmkMenu_cauhinh_phong
            // 
            this.vmkMenu_cauhinh_phong.Name = "vmkMenu_cauhinh_phong";
            this.vmkMenu_cauhinh_phong.Size = new System.Drawing.Size(154, 22);
            this.vmkMenu_cauhinh_phong.Text = "&3 - PHÒNG";
            this.vmkMenu_cauhinh_phong.Click += new System.EventHandler(this.vmkMenu_cauhinh_phong_Click);
            // 
            // vmkMenu_cauhinh_thietbi
            // 
            this.vmkMenu_cauhinh_thietbi.Name = "vmkMenu_cauhinh_thietbi";
            this.vmkMenu_cauhinh_thietbi.Size = new System.Drawing.Size(154, 22);
            this.vmkMenu_cauhinh_thietbi.Text = "&4 - THIẾT BỊ";
            this.vmkMenu_cauhinh_thietbi.Click += new System.EventHandler(this.vmkMenu_cauhinh_thietbi_Click);
            // 
            // vmkMenu_cauhinh_tiethoc
            // 
            this.vmkMenu_cauhinh_tiethoc.Name = "vmkMenu_cauhinh_tiethoc";
            this.vmkMenu_cauhinh_tiethoc.Size = new System.Drawing.Size(154, 22);
            this.vmkMenu_cauhinh_tiethoc.Text = "&5 - TIẾT HỌC";
            this.vmkMenu_cauhinh_tiethoc.Click += new System.EventHandler(this.vmkMenu_cauhinh_tiethoc_Click);
            // 
            // vmkMenu_quanly
            // 
            this.vmkMenu_quanly.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vmkMenu_quanly_sudungthietbi,
            this.vmkMenu_quanly_sudungphong});
            this.vmkMenu_quanly.Name = "vmkMenu_quanly";
            this.vmkMenu_quanly.Size = new System.Drawing.Size(86, 20);
            this.vmkMenu_quanly.Text = "&2 - QUẢN LÝ";
            // 
            // vmkMenu_quanly_sudungthietbi
            // 
            this.vmkMenu_quanly_sudungthietbi.Name = "vmkMenu_quanly_sudungthietbi";
            this.vmkMenu_quanly_sudungthietbi.Size = new System.Drawing.Size(189, 22);
            this.vmkMenu_quanly_sudungthietbi.Text = "&1 - SỬ DỤNG THIẾT BỊ";
            this.vmkMenu_quanly_sudungthietbi.Click += new System.EventHandler(this.vmkMenu_quanly_sudungthietbi_Click);
            // 
            // vmkMenu_quanly_sudungphong
            // 
            this.vmkMenu_quanly_sudungphong.Name = "vmkMenu_quanly_sudungphong";
            this.vmkMenu_quanly_sudungphong.Size = new System.Drawing.Size(189, 22);
            this.vmkMenu_quanly_sudungphong.Text = "&2 - SỬ DỤNG PHÒNG";
            this.vmkMenu_quanly_sudungphong.Click += new System.EventHandler(this.vmkMenu_quanly_sudungphong_Click);
            // 
            // vmkMenu_thongtin
            // 
            this.vmkMenu_thongtin.Name = "vmkMenu_thongtin";
            this.vmkMenu_thongtin.Size = new System.Drawing.Size(83, 20);
            this.vmkMenu_thongtin.Text = "&THÔNG TIN";
            this.vmkMenu_thongtin.Click += new System.EventHandler(this.vmkMenu_thongtin_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(929, 515);
            this.Controls.Add(this.vmkMenu);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.vmkMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Phòng Máy Tin Học";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.vmkMenu.ResumeLayout(false);
            this.vmkMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip vmkMenu;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_hethong;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_hethong_exit;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_cauhinh;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_thongtin;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_cauhinh_giaovien;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_cauhinh_phong;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_cauhinh_thietbi;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_quanly;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_cauhinh_tiethoc;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_quanly_sudungthietbi;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_quanly_sudungphong;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_hethong_login;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_hethong_logout;
        private System.Windows.Forms.ToolStripMenuItem vmkMenu_cauhinh_nhanvien;
    }
}

