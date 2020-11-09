namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    partial class FrmMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMsg));
            this.TabC1 = new System.Windows.Forms.TabControl();
            this.TabP1 = new System.Windows.Forms.TabPage();
            this.txt_rutgon = new System.Windows.Forms.TextBox();
            this.TabP2 = new System.Windows.Forms.TabPage();
            this.txt_chitiet = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.TabC1.SuspendLayout();
            this.TabP1.SuspendLayout();
            this.TabP2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabC1
            // 
            this.TabC1.Controls.Add(this.TabP1);
            this.TabC1.Controls.Add(this.TabP2);
            this.TabC1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TabC1.Location = new System.Drawing.Point(12, 12);
            this.TabC1.Name = "TabC1";
            this.TabC1.SelectedIndex = 0;
            this.TabC1.Size = new System.Drawing.Size(477, 202);
            this.TabC1.TabIndex = 0;
            // 
            // TabP1
            // 
            this.TabP1.Controls.Add(this.txt_rutgon);
            this.TabP1.Cursor = System.Windows.Forms.Cursors.Default;
            this.TabP1.Location = new System.Drawing.Point(4, 25);
            this.TabP1.Name = "TabP1";
            this.TabP1.Padding = new System.Windows.Forms.Padding(3);
            this.TabP1.Size = new System.Drawing.Size(469, 173);
            this.TabP1.TabIndex = 0;
            this.TabP1.Text = "NỘI DUNG";
            this.TabP1.UseVisualStyleBackColor = true;
            // 
            // txt_rutgon
            // 
            this.txt_rutgon.BackColor = System.Drawing.Color.White;
            this.txt_rutgon.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_rutgon.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_rutgon.ForeColor = System.Drawing.Color.Blue;
            this.txt_rutgon.Location = new System.Drawing.Point(6, 6);
            this.txt_rutgon.Multiline = true;
            this.txt_rutgon.Name = "txt_rutgon";
            this.txt_rutgon.ReadOnly = true;
            this.txt_rutgon.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_rutgon.Size = new System.Drawing.Size(457, 161);
            this.txt_rutgon.TabIndex = 1;
            // 
            // TabP2
            // 
            this.TabP2.Controls.Add(this.txt_chitiet);
            this.TabP2.Location = new System.Drawing.Point(4, 25);
            this.TabP2.Name = "TabP2";
            this.TabP2.Padding = new System.Windows.Forms.Padding(3);
            this.TabP2.Size = new System.Drawing.Size(469, 173);
            this.TabP2.TabIndex = 1;
            this.TabP2.Text = "CHI TIẾT";
            this.TabP2.UseVisualStyleBackColor = true;
            // 
            // txt_chitiet
            // 
            this.txt_chitiet.BackColor = System.Drawing.Color.White;
            this.txt_chitiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_chitiet.Cursor = System.Windows.Forms.Cursors.Default;
            this.txt_chitiet.ForeColor = System.Drawing.Color.Blue;
            this.txt_chitiet.Location = new System.Drawing.Point(6, 6);
            this.txt_chitiet.Multiline = true;
            this.txt_chitiet.Name = "txt_chitiet";
            this.txt_chitiet.ReadOnly = true;
            this.txt_chitiet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_chitiet.Size = new System.Drawing.Size(457, 161);
            this.txt_chitiet.TabIndex = 1;
            // 
            // btn_close
            // 
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Location = new System.Drawing.Point(11, 220);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(477, 35);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "&OK";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // FrmMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_close;
            this.ClientSize = new System.Drawing.Size(500, 266);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.TabC1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TIÊU ĐỀ";
            this.Load += new System.EventHandler(this.FrmMsg_Load);
            this.TabC1.ResumeLayout(false);
            this.TabP1.ResumeLayout(false);
            this.TabP1.PerformLayout();
            this.TabP2.ResumeLayout(false);
            this.TabP2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabC1;
        private System.Windows.Forms.TabPage TabP1;
        private System.Windows.Forms.TabPage TabP2;
        private System.Windows.Forms.TextBox txt_rutgon;
        private System.Windows.Forms.TextBox txt_chitiet;
        private System.Windows.Forms.Button btn_close;

    }
}