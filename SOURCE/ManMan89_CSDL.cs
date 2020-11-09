/*
    EXAMPLE SELECT

        ManMan89_CSDL vmk = new ManMan89_CSDL();
        vmk.SQLITE_QUERY = "select vmk_value from manman89 where vmk_key=@vmk_key";

        DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
        SQLITE_PARAM.Rows.Add("@vmk_key", "12-05-1989");
        vmk.SQLITE_PARAM = SQLITE_PARAM;

        ArrayList KQ = vmk.SQLITE_SELECT();

        // KQ[0] -> "OK" or "ERROR"
        // KQ[1] -> "" or "ERROR MESSAGE"
        // KQ[2] -> DATATABLE or EMPTY DATATABLE
 
   EXAMPLE INSERT

        ManMan89_CSDL vmk = new ManMan89_CSDL();
        vmk.SQLITE_QUERY = "insert into manman89 (vmk_key,vmk_value) values (@vmk_key, @vmk_value)";

        DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
        SQLITE_PARAM.Rows.Add("@vmk_key", "12-05-1989");
        SQLITE_PARAM.Rows.Add("@vmk_value", "Have a nice day");
        vmk.SQLITE_PARAM = SQLITE_PARAM;

        String[] KQ =  vmk.SQLITE_INSERT_DELETE_UPDATE();
        // KQ[0] -> "OK" or "ERROR"
        // KQ[1] -> "SQL STATUS > 0" or "ERROR MESSAGE"

    EXAMPLE DELETE

        ManMan89_CSDL vmk = new ManMan89_CSDL();
        vmk.SQLITE_QUERY = "delete from manman89 where vmk_key=@vmk_key";
 
        DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
        SQLITE_PARAM.Rows.Add("@vmk_key", "12-05-1989");
        vmk.SQLITE_PARAM = SQLITE_PARAM;

        String[] KQ =  vmk.SQLITE_INSERT_DELETE_UPDATE();
        // KQ[0] -> "OK" or "ERROR"
        // KQ[1] -> "SQL STATUS > 0" or "ERROR MESSAGE"

    EXAMPLE UPDATE

        ManMan89_CSDL vmk = new ManMan89_CSDL();
        vmk.SQLITE_QUERY = "update manman89 set vmk_value=@vmk_value where vmk_key=@vmk_key";

        DataTable SQLITE_PARAM = vmk.SQLITE_PARAM;
        SQLITE_PARAM.Rows.Add("@vmk_key", "12-05-1989");
        SQLITE_PARAM.Rows.Add("@vmk_value", "Good Luck");
        vmk.SQLITE_PARAM = SQLITE_PARAM;

        String[] KQ =  vmk.SQLITE_INSERT_DELETE_UPDATE();
        // KQ[0] -> "OK" or "ERROR"
        // KQ[1] -> "SQL STATUS > 0" or "ERROR MESSAGE"

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SQLite;
using System.Collections;

namespace QUAN_LY_PHONG_MAY_TIN_HOC
{
    public class ManMan89_CSDL
    {
        public static string FILE_CSDL = "QUAN_LY_PHONG_MAY.DB3";

        private string SQLITE_CONNECTION_STRING = @"Data Source=" + FILE_CSDL + ";Version=3;Foreign Keys=True;";

        public String SQLITE_QUERY;

        public DataTable SQLITE_PARAM;

        public ManMan89_CSDL() { SQLITE_PARAM = CREATE_SQLITE_PARAM(); }

        private DataTable CREATE_SQLITE_PARAM()
        {
            DataTable SQLITE_PARAM = new DataTable();
            SQLITE_PARAM.Columns.Add("key", typeof(String));
            SQLITE_PARAM.Columns.Add("value", typeof(String));
            return SQLITE_PARAM;
        }

        public static bool CHECK_FILE_CSDL() { return System.IO.File.Exists(FILE_CSDL.Trim()); }

        public ArrayList SQLITE_SELECT()
        {
            ArrayList KQ = new ArrayList(3);
            DataTable DT = new DataTable();

            if (CHECK_FILE_CSDL() == false)
            {
                KQ.Add("ERROR");
                KQ.Add("KHÔNG TÌM THẤY FILE  " + Convert.ToChar(34) + FILE_CSDL.ToUpper() + Convert.ToChar(34));
                KQ.Add(DT);
                return KQ;
            }

            try
            {
                // CÂU LỆNH KẾT NỐI CSDL

                SQLiteConnection SQLITE_CONN = new SQLiteConnection(SQLITE_CONNECTION_STRING);
                SQLITE_CONN.Open();

                // CÂU LỆNH TRUY VẤN CSDL

                SQLiteCommand SQLITE_CMD = new SQLiteCommand(SQLITE_QUERY, SQLITE_CONN);
                SQLITE_CMD.Parameters.Clear();

                for (int i = 0; i < SQLITE_PARAM.Rows.Count; i++)
                {
                    SQLITE_CMD.Parameters.Add(new SQLiteParameter(SQLITE_PARAM.Rows[i][0].ToString(), SQLITE_PARAM.Rows[i][1].ToString()));
                }

                SQLiteDataReader SQLITE_DATA_READER = SQLITE_CMD.ExecuteReader();

                // LẤY DỮ LIỆU TRẢ VỀ TỪ CÂU LỆNH TRUY VẤN ĐƯA VÀO ĐỐI TƯỢNG DATA TABLE

                DT.Load(SQLITE_DATA_READER);

                // ĐÓNG KẾT NỐI CSDL

                SQLITE_CONN.Close();

                KQ.Clear();
                KQ.Add("OK");
                KQ.Add("");
                KQ.Add(DT);
            }
            catch (Exception ex)
            {
                KQ.Clear();
                KQ.Add("ERROR");
                KQ.Add(ex.ToString());
                KQ.Add(DT);
            }

            return KQ;
        }

        public String[] SQLITE_INSERT_DELETE_UPDATE()
        {
            String[] KQ = { "", "" };

            if (CHECK_FILE_CSDL() == false)
            {
                KQ[0] = "ERROR";
                KQ[1] = "KHÔNG TÌM THẤY FILE  " + Convert.ToChar(34) + FILE_CSDL.ToUpper() + Convert.ToChar(34);
                return KQ;
            }

            try
            {
                // CÂU LỆNH KẾT NỐI CSDL

                SQLiteConnection SQLITE_CONN = new SQLiteConnection(SQLITE_CONNECTION_STRING);
                SQLITE_CONN.Open();

                // CÂU LỆNH TRUY VẤN CSDL

                SQLiteCommand SQLITE_CMD = new SQLiteCommand(SQLITE_QUERY, SQLITE_CONN);
                SQLITE_CMD.Parameters.Clear();

                for (int i = 0; i < SQLITE_PARAM.Rows.Count; i++)
                {
                    SQLITE_CMD.Parameters.Add(new SQLiteParameter(SQLITE_PARAM.Rows[i][0].ToString(), SQLITE_PARAM.Rows[i][1].ToString()));
                }

                int SQLITE_CMD_STATUS = SQLITE_CMD.ExecuteNonQuery();

                // ĐÓNG KẾT NỐI CSDL

                SQLITE_CONN.Close();

                KQ[0] = "OK";
                KQ[1] = SQLITE_CMD_STATUS.ToString();
            }
            catch (Exception ex)
            {
                KQ[0] = "ERROR";
                KQ[1] = ex.ToString();
            }

            return KQ;
        }
    }
}
