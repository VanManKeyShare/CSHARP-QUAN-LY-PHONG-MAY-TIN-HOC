using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Collections;
using System.Threading;

namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    public class ManMan89_Main
    {
        public static bool STATUS_LOGIN = false;

        public static string TAI_KHOAN = "";

        public static int MA_NV_LOGGED = 1;

        static Mutex VMK_INSTANCE;

        public static bool IS_SINGLE_INSTANCE()
        {
            try
            {
                Mutex.OpenExisting("ManMan89_QuanLyPhongMayTinHoc");
            }
            catch
            {
                ManMan89_Main.VMK_INSTANCE = new Mutex(true, "ManMan89_QuanLyPhongMayTinHoc");
                return true;
            }
            return false;
        }

        public static bool IS_VALID_EMAIL_ADDRESS(string Email_Address)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(Email_Address, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
        }

        public static bool IS_VALID_DATE(int Year, int Month, int Day)
        {
            bool checker = false;
            if (Year <= DateTime.MaxValue.Year && Year >= DateTime.MinValue.Year)
            {
                if (Month >= 1 && Month <= 12)
                {
                    if (DateTime.DaysInMonth(Year, Month) >= Day && Day >= 1) { checker = true; }
                }
            }
            return checker;
        }

        public static bool IS_CHARACTERS_VIET_NAM(string Name, bool Allow_Number = false, bool Allow_Special_Characters = false)
        {
            string VIET_NAM_CHAR_ALLOWS =

                Convert.ToChar(32) +
                "aáàảãạ" + "ăắằẳẵặ" + "âấầẩẫậ" + "bcdđ" + "eéèẻẽẹ" + "êếềểễệ" + "fgh" + "iíìỉĩị" + "jklmn" + 
                "oóòỏõọ" + "ôốồổỗộ" + "ơớờởỡợ" + "pqrst" + "uúùủũụ" + "ưứừửữự" + "vwx" + "yýỳỷỹỵ" + "z" +
                "AÁÀẢÃẠ" + "ĂẮẰẲẴẶ" + "ÂẤẦẨẪẬ" + "BCDĐ" + "EÉÈẺẼẸ" + "ÊẾỀỂỄỆ" + "FGH" + "IÍÌỈĨỊ" + "JKLMN" +
                "OÓÒỎÕỌ" + "ÔỐỒỔỖỘ" + "ƠỚỜỞỠỢ" + "PQRST" + "UÚÙỦŨỤ" + "ƯỨỪỬỮỰ" + "VWX" + "YÝỲỶỸỴ" + "Z"
            ;

            if (Allow_Number == true) { VIET_NAM_CHAR_ALLOWS += "0123456789"; }

            if (Allow_Special_Characters == true) { VIET_NAM_CHAR_ALLOWS += "/.,"; }

            for (int i = 0; i < Name.Length; i++)
            {
                if (VIET_NAM_CHAR_ALLOWS.IndexOf(Name[i]) < 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
