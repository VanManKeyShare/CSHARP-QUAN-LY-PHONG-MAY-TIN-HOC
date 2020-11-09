namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    partial class FrmTBSD
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTBSD));
            this.gv_list_data = new System.Windows.Forms.DataGridView();
            this.vmkRiMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.vmkRiMenu_Xoa = new System.Windows.Forms.ToolStripMenuItem();
            this.vmkRiMenu_Sua = new System.Windows.Forms.ToolStripMenuItem();
            this.TabC1 = new System.Windows.Forms.TabControl();
            this.TabP_Xem = new System.Windows.Forms.TabPage();
            this.TabP_Them_Sua = new System.Windows.Forms.TabPage();
            this.btn_reload_thietbi = new System.Windows.Forms.Button();
            this.btn_reload_phong = new System.Windows.Forms.Button();
            this.cbo_nam = new System.Windows.Forms.ComboBox();
            this.cbo_thietbi = new System.Windows.Forms.ComboBox();
            this.cbo_phong = new System.Windows.Forms.ComboBox();
            this.cbo_thang = new System.Windows.Forms.ComboBox();
            this.cbo_ngay = new System.Windows.Forms.ComboBox();
            this.btn_khongluu = new System.Windows.Forms.Button();
            this.btn_luu = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_soluong = new System.Windows.Forms.TextBox();
            this.btn_xem = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_data)).BeginInit();
            this.vmkRiMenu.SuspendLayout();
            this.TabC1.SuspendLayout();
            this.TabP_Xem.SuspendLayout();
            this.TabP_Them_Sua.SuspendLayout();
            this.SuspendLayout();
            // 
            // gv_list_data
            // 
            this.gv_list_data.AllowUserToAddRows = false;
            this.gv_list_data.AllowUserToDeleteRows = false;
            this.gv_list_data.AllowUserToResizeRows = false;
            this.gv_list_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gv_list_data.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.gv_list_data.BackgroundColor = System.Drawing.Color.White;
            this.gv_list_data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gv_list_data.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gv_list_data.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gv_list_data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gv_list_data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_list_data.ContextMenuStrip = this.vmkRiMenu;
            this.gv_list_data.GridColor = System.Drawing.Color.White;
            this.gv_list_data.Location = new System.Drawing.Point(3, 6);
            this.gv_list_data.MultiSelect = false;
            this.gv_list_data.Name = "gv_list_data";
            this.gv_list_data.ReadOnly = true;
            this.gv_list_data.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gv_list_data.RowHeadersVisible = false;
            this.gv_list_data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv_list_data.Size = new System.Drawing.Size(822, 318);
            this.gv_list_data.TabIndex = 0;
            // 
            // vmkRiMenu
            // 
            this.vmkRiMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vmkRiMenu_Xoa,
            this.vmkRiMenu_Sua});
            this.vmkRiMenu.Name = "vmkRiMenu";
            this.vmkRiMenu.Size = new System.Drawing.Size(201, 48);
            this.vmkRiMenu.Opening += new System.ComponentModel.CancelEventHandler(this.vmkRiMenu_Opening);
            // 
            // vmkRiMenu_Xoa
            // 
            this.vmkRiMenu_Xoa.Name = "vmkRiMenu_Xoa";
            this.vmkRiMenu_Xoa.Size = new System.Drawing.Size(200, 22);
            this.vmkRiMenu_Xoa.Text = "NGỪNG SỬ DỤNG";
            this.vmkRiMenu_Xoa.Click += new System.EventHandler(this.vmkRiMenu_Xoa_Click);
            // 
            // vmkRiMenu_Sua
            // 
            this.vmkRiMenu_Sua.Name = "vmkRiMenu_Sua";
            this.vmkRiMenu_Sua.Size = new System.Drawing.Size(200, 22);
            this.vmkRiMenu_Sua.Text = "CẬP NHẬT THÔNG TIN";
            this.vmkRiMenu_Sua.Click += new System.EventHandler(this.vmkRiMenu_Sua_Click);
            // 
            // TabC1
            // 
            this.TabC1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabC1.Controls.Add(this.TabP_Xem);
            this.TabC1.Controls.Add(this.TabP_Them_Sua);
            this.TabC1.Location = new System.Drawing.Point(20, 20);
            this.TabC1.Name = "TabC1";
            this.TabC1.SelectedIndex = 0;
            this.TabC1.Size = new System.Drawing.Size(839, 356);
            this.TabC1.TabIndex = 0;
            // 
            // TabP_Xem
            // 
            this.TabP_Xem.Controls.Add(this.gv_list_data);
            this.TabP_Xem.Location = new System.Drawing.Point(4, 25);
            this.TabP_Xem.Name = "TabP_Xem";
            this.TabP_Xem.Padding = new System.Windows.Forms.Padding(3);
            this.TabP_Xem.Size = new System.Drawing.Size(831, 327);
            this.TabP_Xem.TabIndex = 0;
            this.TabP_Xem.Text = "THỐNG KÊ SỬ DỤNG THIẾT BỊ";
            this.TabP_Xem.UseVisualStyleBackColor = true;
            // 
            // TabP_Them_Sua
            // 
            this.TabP_Them_Sua.Controls.Add(this.btn_reload_thietbi);
            this.TabP_Them_Sua.Controls.Add(this.btn_reload_phong);
            this.TabP_Them_Sua.Controls.Add(this.cbo_nam);
            this.TabP_Them_Sua.Controls.Add(this.cbo_thietbi);
            this.TabP_Them_Sua.Controls.Add(this.cbo_phong);
            this.TabP_Them_Sua.Controls.Add(this.cbo_thang);
            this.TabP_Them_Sua.Controls.Add(this.cbo_ngay);
            this.TabP_Them_Sua.Controls.Add(this.btn_khongluu);
            this.TabP_Them_Sua.Controls.Add(this.btn_luu);
            this.TabP_Them_Sua.Controls.Add(this.label12);
            this.TabP_Them_Sua.Controls.Add(this.label6);
            this.TabP_Them_Sua.Controls.Add(this.label11);
            this.TabP_Them_Sua.Controls.Add(this.label5);
            this.TabP_Them_Sua.Controls.Add(this.label3);
            this.TabP_Them_Sua.Controls.Add(this.label8);
            this.TabP_Them_Sua.Controls.Add(this.txt_soluong);
            this.TabP_Them_Sua.Location = new System.Drawing.Point(4, 25);
            this.TabP_Them_Sua.Name = "TabP_Them_Sua";
            this.TabP_Them_Sua.Padding = new System.Windows.Forms.Padding(3);
            this.TabP_Them_Sua.Size = new System.Drawing.Size(831, 327);
            this.TabP_Them_Sua.TabIndex = 1;
            this.TabP_Them_Sua.Text = "THÊM & SỬA";
            this.TabP_Them_Sua.UseVisualStyleBackColor = true;
            // 
            // btn_reload_thietbi
            // 
            this.btn_reload_thietbi.BackgroundImage = global::QUAN_LY_PHONG_MAY_TIN_HOC.Properties.Resources.RELOAD;
            this.btn_reload_thietbi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_reload_thietbi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reload_thietbi.Location = new System.Drawing.Point(434, 54);
            this.btn_reload_thietbi.Name = "btn_reload_thietbi";
            this.btn_reload_thietbi.Size = new System.Drawing.Size(37, 26);
            this.btn_reload_thietbi.TabIndex = 9;
            this.btn_reload_thietbi.UseVisualStyleBackColor = true;
            this.btn_reload_thietbi.Click += new System.EventHandler(this.btn_reload_thietbi_Click);
            // 
            // btn_reload_phong
            // 
            this.btn_reload_phong.BackgroundImage = global::QUAN_LY_PHONG_MAY_TIN_HOC.Properties.Resources.RELOAD;
            this.btn_reload_phong.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_reload_phong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reload_phong.Location = new System.Drawing.Point(434, 23);
            this.btn_reload_phong.Name = "btn_reload_phong";
            this.btn_reload_phong.Size = new System.Drawing.Size(37, 26);
            this.btn_reload_phong.TabIndex = 8;
            this.btn_reload_phong.UseVisualStyleBackColor = true;
            this.btn_reload_phong.Click += new System.EventHandler(this.btn_reload_phong_Click);
            // 
            // cbo_nam
            // 
            this.cbo_nam.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbo_nam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_nam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cbo_nam.FormattingEnabled = true;
            this.cbo_nam.Location = new System.Drawing.Point(380, 114);
            this.cbo_nam.Name = "cbo_nam";
            this.cbo_nam.Size = new System.Drawing.Size(91, 24);
            this.cbo_nam.TabIndex = 5;
            // 
            // cbo_thietbi
            // 
            this.cbo_thietbi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbo_thietbi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_thietbi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cbo_thietbi.FormattingEnabled = true;
            this.cbo_thietbi.Location = new System.Drawing.Point(144, 55);
            this.cbo_thietbi.Name = "cbo_thietbi";
            this.cbo_thietbi.Size = new System.Drawing.Size(284, 24);
            this.cbo_thietbi.TabIndex = 1;
            // 
            // cbo_phong
            // 
            this.cbo_phong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbo_phong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_phong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cbo_phong.FormattingEnabled = true;
            this.cbo_phong.Location = new System.Drawing.Point(144, 25);
            this.cbo_phong.Name = "cbo_phong";
            this.cbo_phong.Size = new System.Drawing.Size(284, 24);
            this.cbo_phong.TabIndex = 0;
            // 
            // cbo_thang
            // 
            this.cbo_thang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbo_thang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_thang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cbo_thang.FormattingEnabled = true;
            this.cbo_thang.Location = new System.Drawing.Point(269, 114);
            this.cbo_thang.Name = "cbo_thang";
            this.cbo_thang.Size = new System.Drawing.Size(65, 24);
            this.cbo_thang.TabIndex = 4;
            // 
            // cbo_ngay
            // 
            this.cbo_ngay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbo_ngay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_ngay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cbo_ngay.FormattingEnabled = true;
            this.cbo_ngay.Location = new System.Drawing.Point(144, 114);
            this.cbo_ngay.Name = "cbo_ngay";
            this.cbo_ngay.Size = new System.Drawing.Size(65, 24);
            this.cbo_ngay.TabIndex = 3;
            // 
            // btn_khongluu
            // 
            this.btn_khongluu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_khongluu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_khongluu.ForeColor = System.Drawing.Color.Purple;
            this.btn_khongluu.Location = new System.Drawing.Point(343, 162);
            this.btn_khongluu.Name = "btn_khongluu";
            this.btn_khongluu.Size = new System.Drawing.Size(128, 34);
            this.btn_khongluu.TabIndex = 7;
            this.btn_khongluu.Text = "&KHÔNG LƯU";
            this.btn_khongluu.UseVisualStyleBackColor = true;
            this.btn_khongluu.Click += new System.EventHandler(this.btn_khongluu_Click);
            // 
            // btn_luu
            // 
            this.btn_luu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_luu.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_luu.ForeColor = System.Drawing.Color.Purple;
            this.btn_luu.Location = new System.Drawing.Point(144, 162);
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Size = new System.Drawing.Size(190, 34);
            this.btn_luu.TabIndex = 6;
            this.btn_luu.Text = "&LƯU DỮ LIỆU";
            this.btn_luu.UseVisualStyleBackColor = true;
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Blue;
            this.label12.Location = new System.Drawing.Point(31, 58);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "CHỌN THIẾT BỊ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(340, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "NĂM";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(31, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "CHỌN PHÒNG";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(215, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "THÁNG";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(31, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "NGÀY SỬ DỤNG";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(31, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "SỐ LƯỢNG";
            // 
            // txt_soluong
            // 
            this.txt_soluong.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_soluong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txt_soluong.Location = new System.Drawing.Point(144, 85);
            this.txt_soluong.MaxLength = 10;
            this.txt_soluong.Name = "txt_soluong";
            this.txt_soluong.Size = new System.Drawing.Size(327, 23);
            this.txt_soluong.TabIndex = 2;
            this.txt_soluong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_soluong_KeyPress);
            // 
            // btn_xem
            // 
            this.btn_xem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_xem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xem.ForeColor = System.Drawing.Color.Purple;
            this.btn_xem.Location = new System.Drawing.Point(19, 382);
            this.btn_xem.Name = "btn_xem";
            this.btn_xem.Size = new System.Drawing.Size(100, 33);
            this.btn_xem.TabIndex = 1;
            this.btn_xem.Text = "&XEM";
            this.btn_xem.UseVisualStyleBackColor = true;
            this.btn_xem.Click += new System.EventHandler(this.btn_xem_Click);
            // 
            // btn_them
            // 
            this.btn_them.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_them.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_them.ForeColor = System.Drawing.Color.Purple;
            this.btn_them.Location = new System.Drawing.Point(125, 382);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(100, 33);
            this.btn_them.TabIndex = 2;
            this.btn_them.Text = "&THÊM";
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_xoa.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xoa.ForeColor = System.Drawing.Color.Purple;
            this.btn_xoa.Location = new System.Drawing.Point(417, 382);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(160, 33);
            this.btn_xoa.TabIndex = 4;
            this.btn_xoa.Text = "NGỪNG &SỬ DỤNG";
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_sua
            // 
            this.btn_sua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_sua.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_sua.ForeColor = System.Drawing.Color.Purple;
            this.btn_sua.Location = new System.Drawing.Point(231, 382);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(180, 33);
            this.btn_sua.TabIndex = 3;
            this.btn_sua.Text = "&CẬP NHẬT THÔNG TIN";
            this.btn_sua.UseVisualStyleBackColor = true;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_close
            // 
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.Color.Red;
            this.btn_close.Location = new System.Drawing.Point(583, 382);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(100, 33);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "ĐÓ&NG";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // FrmTBSD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(877, 430);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_sua);
            this.Controls.Add(this.btn_them);
            this.Controls.Add(this.btn_xem);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.TabC1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "FrmTBSD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SỬ DỤNG THIẾT BỊ";
            this.Load += new System.EventHandler(this.FrmTBSD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_list_data)).EndInit();
            this.vmkRiMenu.ResumeLayout(false);
            this.TabC1.ResumeLayout(false);
            this.TabP_Xem.ResumeLayout(false);
            this.TabP_Them_Sua.ResumeLayout(false);
            this.TabP_Them_Sua.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gv_list_data;
        private System.Windows.Forms.TabControl TabC1;
        private System.Windows.Forms.TabPage TabP_Xem;
        private System.Windows.Forms.TabPage TabP_Them_Sua;
        private System.Windows.Forms.Button btn_xem;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_luu;
        private System.Windows.Forms.Button btn_khongluu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbo_nam;
        private System.Windows.Forms.ComboBox cbo_thang;
        private System.Windows.Forms.ComboBox cbo_ngay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_soluong;
        private System.Windows.Forms.ContextMenuStrip vmkRiMenu;
        private System.Windows.Forms.ToolStripMenuItem vmkRiMenu_Xoa;
        private System.Windows.Forms.ToolStripMenuItem vmkRiMenu_Sua;
        private System.Windows.Forms.ComboBox cbo_thietbi;
        private System.Windows.Forms.ComboBox cbo_phong;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_reload_phong;
        private System.Windows.Forms.Button btn_reload_thietbi;
    }
}