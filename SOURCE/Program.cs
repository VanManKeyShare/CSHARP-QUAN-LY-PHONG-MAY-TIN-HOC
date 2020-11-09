using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (!ManMan89_Main.IS_SINGLE_INSTANCE())
            {
                MessageBox.Show("CHƯƠNG TRÌNH NÀY ĐÃ ĐƯỢC MỞ VÀ ĐANG CHẠY", "THÔNG BÁO");
                return;
            }

            Application.Run(new FrmMain());
        }
    }
}
