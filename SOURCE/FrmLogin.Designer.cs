namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_taikhoan = new System.Windows.Forms.TextBox();
            this.txt_matkhau = new System.Windows.Forms.TextBox();
            this.btn_login = new System.Windows.Forms.Button();
            this.chk_remember_taikhoan = new System.Windows.Forms.CheckBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "TÀI KHOẢN";
            // 
            // txt_taikhoan
            // 
            this.txt_taikhoan.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_taikhoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txt_taikhoan.Location = new System.Drawing.Point(114, 27);
            this.txt_taikhoan.Name = "txt_taikhoan";
            this.txt_taikhoan.Size = new System.Drawing.Size(247, 23);
            this.txt_taikhoan.TabIndex = 0;
            this.txt_taikhoan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_taikhoan_KeyPress);
            // 
            // txt_matkhau
            // 
            this.txt_matkhau.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_matkhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txt_matkhau.Location = new System.Drawing.Point(114, 56);
            this.txt_matkhau.Name = "txt_matkhau";
            this.txt_matkhau.PasswordChar = '●';
            this.txt_matkhau.Size = new System.Drawing.Size(247, 23);
            this.txt_matkhau.TabIndex = 1;
            this.txt_matkhau.Enter += new System.EventHandler(this.txt_matkhau_Enter);
            this.txt_matkhau.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_matkhau_KeyPress);
            // 
            // btn_login
            // 
            this.btn_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_login.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_login.ForeColor = System.Drawing.Color.Purple;
            this.btn_login.Location = new System.Drawing.Point(113, 111);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(153, 34);
            this.btn_login.TabIndex = 3;
            this.btn_login.Text = "ĐĂNG &NHẬP";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // chk_remember_taikhoan
            // 
            this.chk_remember_taikhoan.AutoSize = true;
            this.chk_remember_taikhoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chk_remember_taikhoan.ForeColor = System.Drawing.Color.OrangeRed;
            this.chk_remember_taikhoan.Location = new System.Drawing.Point(114, 85);
            this.chk_remember_taikhoan.Name = "chk_remember_taikhoan";
            this.chk_remember_taikhoan.Size = new System.Drawing.Size(120, 20);
            this.chk_remember_taikhoan.TabIndex = 2;
            this.chk_remember_taikhoan.Text = "&NHỚ TÀI KHOẢN";
            this.chk_remember_taikhoan.UseVisualStyleBackColor = true;
            this.chk_remember_taikhoan.CheckedChanged += new System.EventHandler(this.chk_remember_taikhoan_CheckedChanged);
            // 
            // btn_close
            // 
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close.ForeColor = System.Drawing.Color.Purple;
            this.btn_close.Location = new System.Drawing.Point(274, 111);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(88, 34);
            this.btn_close.TabIndex = 4;
            this.btn_close.Text = "ĐÓN&G";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(23, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "MẬT KHẨU";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(383, 165);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.chk_remember_taikhoan);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.txt_matkhau);
            this.Controls.Add(this.txt_taikhoan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "FrmLogin";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ĐĂNG NHẬP";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_taikhoan;
        private System.Windows.Forms.TextBox txt_matkhau;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.CheckBox chk_remember_taikhoan;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label3;
    }
}