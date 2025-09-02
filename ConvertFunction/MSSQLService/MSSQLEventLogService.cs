using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEMS_AttendanceSync.MSSQLService
{
    class MSSQLEventLogService
    {
        public string ConnectionString = "Data Source=kaunghtetwin\\SQLEXPRESS;" +
                                            "Initial Catalog=GlobalEMS;" +
                                            "User id=sa;" +
                                            "Password=gwt;MultipleActiveResultSets=true";
        internal void SaveEventLog(int p, string source, string msg)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            string updateDatetimesql = " INSERT INTO tbl_AC_TurnstileEventLog (LogType,LogSource,LogDatetime,LogMessage) "
                                        + "VALUES(@LogType,@LogSource,GETDATE(),@LogMessage) ";
            conn.Open();
            SqlCommand updatecommand = new SqlCommand(updateDatetimesql, conn);
            updatecommand.Parameters.AddWithValue("@LogType", p);
            updatecommand.Parameters.AddWithValue("@LogSource", source);
            updatecommand.Parameters.AddWithValue("@LogMessage", msg);
            //updatecommand.ExecuteNonQuery();
            //conn.Close();
        }
    }
}
