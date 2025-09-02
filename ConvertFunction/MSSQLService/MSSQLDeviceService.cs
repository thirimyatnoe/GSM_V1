using GlobalEMS_AttendanceSync.Controller;
using GlobalEMS_AttendanceSync.Info;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalEMS_AttendanceSync.MSSQLService
{
    class MSSQLDeviceService
    {
        public string ConnectionString = "Data Source=kaunghtetwin\\SQLEXPRESS;" +
            "Initial Catalog=GlobalEMS;" +
            "User id=sa;Max Pool Size=500;Pooling=True;" +
            "Password=gwt;MultipleActiveResultSets=true";
        Synchronization syn = new Synchronization();
        EventLogController eventlog = new EventLogController();
        internal int SaveDeviceAuthurizeTimeZone(int machineID)
        {
            DateTime currentDateTime = DateTime.Now;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            int insertCount = 0;
            var stet = currentDateTime.DayOfWeek;
            string selectDatetimesql = "SELECT user_lastupdatedtime FROM tbl_AC_AllMachine WHERE id=" + machineID;
            SqlCommand selectcommand = new SqlCommand(selectDatetimesql, conn);
            object Time = selectcommand.ExecuteScalar();
            //string strTime = Time['lastupdatedtime'].ToString();// "7/29/2015 10:02:45 AM";
            DateTime date = DateTime.Parse(Time.ToString(), System.Globalization.CultureInfo.CurrentCulture);
            string sql = "SELECT (CASE WHEN " + stet + " > 0 THEN " + stet + "  ELSE ShiftID END) as TimeZoneID,E.EmployeeID,E.CardID,(CASE WHEN E.LastModifiedDate > EP.LastModifiedDate THEN E.LastModifiedDate ELSE EP.LastModifiedDate END) AS ModifiedDate "
                            + " FROM  tb_TA_EmployeePolicy AS EP "
                            +" LEFT JOIN  tb_GE_DeviceMapping AS D ON D.EmployeeID= EP.EmployeeID "
                              + " LEFT JOIN  tb_GE_Employee AS E ON EP.EmployeeID= E.EmployeeID WHERE DeviceNo=1 AND ( E.LastModifiedDate > @date OR EP.LastModifiedDate > @date ) ORDER BY E.LastModifiedDate ASC";

            string userlevel = "";
            string user = "";
            var lastsyntime = "";
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@date", date);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ShiftID = reader["TimeZoneID"].ToString();
                        var EmployeeID = reader["EmployeeID"].ToString();
                        var CardID = reader["CardID"].ToString();
                        var Lastmodifieddate = reader["ModifiedDate"].ToString();
                        lastsyntime = Lastmodifieddate;
                        String Usersql = "IF EXISTS (SELECT 1 FROM tbl_AC_Turnstile1 WHERE EmployeeID=@EmployeeID)"
                                            + " BEGIN "
                                                + " UPDATE tbl_AC_Turnstile1 SET ShiftID=@ShiftID,CardID=@CardID,Lastmodifieddate=@Lastmodifieddate WHERE EmployeeID=@EmployeeID"
                                            + " END "
                                        + "IF NOT EXISTS (SELECT 1 FROM tbl_AC_Turnstile1 WHERE EmployeeID=@EmployeeID) "
                                            + "BEGIN "
                                                + " INSERT INTO tbl_AC_Turnstile1 (ShiftID,EmployeeID,CardID,Lastmodifieddate) "
                                                        + "VALUES(@ShiftID,@EmployeeID,@CardID,@Lastmodifieddate) "
                                            + " END";
                        SqlCommand Usercommand = new SqlCommand(Usersql, conn);
                        Usercommand.Parameters.AddWithValue("@ShiftID", ShiftID);
                        Usercommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        Usercommand.Parameters.AddWithValue("@CardID", CardID);
                        Usercommand.Parameters.AddWithValue("@Lastmodifieddate", Lastmodifieddate);
                        int id = Usercommand.ExecuteNonQuery();
                        if (id > 0)
                        {
                            //var msg = "EmployeeID=" + EmployeeID + ",ShiftID=" + ShiftID + ",CardID=" + CardID + ",Lastmodifieddate=" + Lastmodifieddate;
                            //  var source="tbl_AC_Turnstile1";
                            // eventlog.SaveEventLog(1, source, msg);
                            if (user == "")
                            {
                                user += "CardNo=" + CardID + "\tPin=" + EmployeeID + "\tPassword=1";
                            }
                            else
                            {
                                user += "\r\nCardNo=" + CardID + "\tPin=" + EmployeeID + "\tPassword=1";
                            }

                            if (userlevel == "")
                            {
                                userlevel += "Pin=" + EmployeeID + "\tAuthorizeTimezoneId=" + ShiftID + "\tAuthorizeDoorId=15";//+useraccesslevel.Userlevel.DoorID;
                            }
                            else
                            {
                                userlevel += "\r\nPin=" + EmployeeID + "\tAuthorizeTimezoneId=" + ShiftID + "\tAuthorizeDoorId=15";//+ useraccesslevel.Userlevel.DoorID;
                            }
                            insertCount++;
                        }
                    }
                }
                
            }
            
            if (user != "" || userlevel != "")
            {
                bool result = syn.AddUserAndUserLevel(user, userlevel);
                if (result && lastsyntime != "")
                {
                    string updateDatetimesql = "UPDATE tbl_AC_AllMachine  SET user_lastupdatedtime=@lastsyntime WHERE id=" + machineID;
                    SqlCommand updatecommand = new SqlCommand(updateDatetimesql, conn);
                    updatecommand.Parameters.AddWithValue("@lastsyntime", lastsyntime);
                    updatecommand.ExecuteNonQuery();
                }
            }
            conn.Close();
            DeleteTurnstileUser();
            return insertCount;
        }
        private void DeleteTurnstileUser()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string DiffUsersql = "SELECT T.EmployeeID FROM  tbl_AC_Turnstile1 AS T  " +
                                 " WHERE  T.EmployeeID NOT IN (SELECT EP.EmployeeID FROM  tb_TA_EmployeePolicy AS EP " +
                                     "  LEFT JOIN tb_GE_DeviceMapping AS D ON D.EmployeeID= EP.EmployeeID " +
                                         " WHERE DeviceNo=1)";
            using (SqlCommand command = new SqlCommand(DiffUsersql, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployeeID = Convert.ToInt16(reader["EmployeeID"]);
                        string Deletesql = "DELETE FROM tbl_AC_Turnstile1 WHERE EmployeeID=@EmployeeID";
                        SqlCommand deletecommand = new SqlCommand(Deletesql, conn);
                        deletecommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                        deletecommand.ExecuteNonQuery();


                        /*string DeletCOsql = "DELETE FROM adms_db.dbo.checkinout WHERE userid=@userid";
                        SqlCommand deleteCOcommand = new SqlCommand(DeletCOsql, conn);
                        deleteCOcommand.Parameters.AddWithValue("@userid", userid);
                        deleteCOcommand.ExecuteNonQuery();

                        string Deletesql = "DELETE FROM adms_db.dbo.userinfo WHERE userid=@userid";
                        SqlCommand deleteUsercommand = new SqlCommand(Deletesql, conn);
                        deleteUsercommand.Parameters.AddWithValue("@userid", userid);
                        deleteUsercommand.ExecuteNonQuery();*/

                        syn.DeleteUserFromDevice(EmployeeID);
                        syn.DeleteUserLevelFromDevice(EmployeeID);
                    }
                }
            }
        }

        internal string GetDeciveLog()
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string Alllog = syn.GetAllDeviceLog();
            string[] stringSeparators = new string[] { "\r\n" };
            string[] log_arr = Alllog.Split(stringSeparators, StringSplitOptions.None);
            foreach (string log in log_arr)
            {
                string[] tmp = log.Split(',');
                //Cardno,Pin,Verified,DoorID,EventType,InOutState,Time_second
                //3432575,1,6,3,20,0,500566098
                //1161321,0,6,1,27,0,496247160
                //4998383,10001,6,1,0,0,495893494
                int tt = tmp.GetLength(0);
                if (tt > 1)
                {
                    string Cardno = tmp[0];
                    string Pin = tmp[1];
                    string Verified = tmp[2];
                    string DoorID = tmp[3];
                    if (Cardno != "Cardno" && DoorID != "0" && Pin != "0")
                    {

                        string EventType = tmp[4];
                        string InOutState = tmp[5];
                        string Time_seconds = tmp[6];
                        int datetime = Convert.ToInt32(Time_seconds);
                        DateTime ActionTime = TurnstileDateStringToDateTime(datetime);

                        String Countsql = "IF NOT EXISTS (SELECT 1 FROM tbl_AC_TurnstileLog WHERE Cardno=@Cardno AND EmployeeID=@EmployeeID AND InOutState=@InOutState AND ActionTime=@ActionTime)"
                                            + "BEGIN "
                                                + " INSERT INTO tbl_AC_TurnstileLog (Cardno,EmployeeID,InOutState,ActionTime) "
                                                    + "VALUES(@Cardno,@EmployeeID,@InOutState,@ActionTime) "
                                            + " END";
                        SqlCommand Countcommand = new SqlCommand(Countsql, conn);
                        Countcommand.Parameters.AddWithValue("@Cardno", Cardno);
                        Countcommand.Parameters.AddWithValue("@EmployeeID", Pin);
                        Countcommand.Parameters.AddWithValue("@InOutState", InOutState);
                        Countcommand.Parameters.AddWithValue("@ActionTime", ActionTime);
                        int id = Countcommand.ExecuteNonQuery();
                        if (id > 0)
                        {
                            // TO Save GEMS Table;
                            String Insertsql = "INSERT INTO tb_IOTA_EmployeeLog (EmployeeID,Press_Time,InActive,MachineID,Is_In) VALUES (@EmployeeID,@Press_Time,'False',1,@Is_In);";
                            SqlCommand Insertcommand = new SqlCommand(Insertsql, conn);
                            Insertcommand.Parameters.AddWithValue("@EmployeeID", Pin);
                            Insertcommand.Parameters.AddWithValue("@Press_Time", ActionTime);
                            Insertcommand.Parameters.AddWithValue("@Is_In", InOutState);
                            //Insertcommand.ExecuteNonQuery();
                            try
                            {
                                int Elog = Insertcommand.ExecuteNonQuery();
                                if (Elog > 0)
                                {
                                    Console.Write("Save TO GEMS Table");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.Write("ERROR TO GEMS Table" + e);
                            }
                        }
                    }
                }
            }
            conn.Close();
            return "aa";
        }

        private DateTime TurnstileDateStringToDateTime(int Time_seconds)
        {
            int second = Time_seconds % 60;
            Time_seconds = Time_seconds / 60;
            int minute = Time_seconds % 60;
            Time_seconds = Time_seconds / 60;
            int hour = Time_seconds % 24;
            Time_seconds = Time_seconds / 24;
            int day = Time_seconds % 31 + 1;
            Time_seconds = Time_seconds / 31;
            int month = Time_seconds % 12 + 1;
            Time_seconds = Time_seconds / 12;
            int year = Time_seconds + 2000;
            var daynight = "AM";
            if (hour > 12)
            {
                hour = hour - 12;
                daynight = "PM";
            }

            string dateTime = month.ToString() + "/" + day.ToString() + "/" + year.ToString() + " " + hour.ToString() + ":" + minute.ToString() + ":" + second.ToString() + " " + daynight;
            DateTime date = DateTime.Parse(dateTime, System.Globalization.CultureInfo.CurrentCulture);
            return date;
        }

        internal string FpSynchronization(int machineID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string selectDatetimesql = "SELECT user_lastupdatedtime FROM tbl_AC_AllMachine WHERE id="+ machineID;
            SqlCommand selectcommand = new SqlCommand(selectDatetimesql, conn);
            object Time = selectcommand.ExecuteScalar();
            //string strTime = Time['lastupdatedtime'].ToString();// "7/29/2015 10:02:45 AM";
            DateTime date = DateTime.Parse(Time.ToString(), System.Globalization.CultureInfo.CurrentCulture);
            string sql = "SELECT E.EmployeeName,E.EmployeeID,E.CardID,E.LastModifiedDate AS ModifiedDate " +
                            " FROM  tb_GE_DeviceMapping  AS D " +
                                 " LEFT JOIN  tb_GE_Employee AS E ON D.EmployeeID= E.EmployeeID " +
                                     "WHERE DeviceNo=2 AND E.LastModifiedDate > @date ORDER BY E.LastModifiedDate ASC";
            var lastsyntime = "";
            int insertCount = 0;
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@date", date);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var EmployeeID = reader["EmployeeID"].ToString();
                        var EmployeeName = reader["EmployeeName"].ToString();
                        var CardID = reader["CardID"].ToString();

                        var Lastmodifieddate = reader["ModifiedDate"].ToString();
                        lastsyntime = Lastmodifieddate;

                        int insertedid = this.SaveADMSUser(EmployeeID, EmployeeName, CardID);
                        if (insertedid>0)
                        {
                            var cmdtodevice = "DATA USER\tPIN=" + EmployeeID + "\tName=" + EmployeeName + "\tPri=0\tGrp=1";
                            this.SaveADMSCmd(cmdtodevice);//Save User cmd to ADMS
                        }
                        insertCount++;
                    }
                }
                if (lastsyntime != "")
                {
                    string updateDatetimesql = "UPDATE tbl_AC_AllMachine SET user_lastupdatedtime=@lastsyntime WHERE id="+ machineID;
                    SqlCommand updatecommand = new SqlCommand(updateDatetimesql, conn);
                    updatecommand.Parameters.AddWithValue("@lastsyntime", lastsyntime);
                    updatecommand.ExecuteNonQuery();
                }
            }
            conn.Close();
            DeleteFPUser();
            FPTemplateSyn();
            return insertCount.ToString();
        }

        private void DeleteFPUser()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string DiffUsersql = "SELECT badgenumber,userid FROM  adms_db.dbo.userinfo AS U  " +
                                 " WHERE badgenumber NOT IN (SELECT EmployeeID FROM  tb_GE_DeviceMapping  AS D WHERE DeviceNo=2)";
            using (SqlCommand command = new SqlCommand(DiffUsersql, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userid = reader["userid"].ToString();
                        var badgenumber = reader["badgenumber"].ToString();
                        string DeletFPesql = "DELETE FROM adms_db.dbo.template WHERE userid=@userid";
                        SqlCommand deleteFPcommand = new SqlCommand(DeletFPesql, conn);
                        deleteFPcommand.Parameters.AddWithValue("@userid", userid);
                        deleteFPcommand.ExecuteNonQuery();

                        
                        string DeletCOsql = "DELETE FROM adms_db.dbo.checkinout WHERE userid=@userid";
                        SqlCommand deleteCOcommand = new SqlCommand(DeletCOsql, conn);
                        deleteCOcommand.Parameters.AddWithValue("@userid", userid);
                        deleteCOcommand.ExecuteNonQuery();

                        string Deletesql = "DELETE FROM adms_db.dbo.userinfo WHERE userid=@userid";
                        SqlCommand deleteUsercommand = new SqlCommand(Deletesql, conn);
                        deleteUsercommand.Parameters.AddWithValue("@userid", userid);
                        deleteUsercommand.ExecuteNonQuery();

                        var cmdtodevice = "DATA DEL_USER PIN=" + badgenumber;
                        this.SaveADMSCmd(cmdtodevice);//Save User cmd to ADMS
                    }
                }
            }
        }

        private void SaveADMSCmd(string cmdtodevice)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            String FPusersql = "INSERT INTO adms_db.dbo.devcmds (SN_id, CmdContent, CmdCommitTime, CmdTransTime, CmdOverTime, CmdReturn, User_id) VALUES" +
                                   "('3281152800001', @cmdtodevice, GETDATE(), NULL, NULL, NULL, NULL);";
            SqlCommand Usercommand = new SqlCommand(FPusersql, conn);
            Usercommand.Parameters.AddWithValue("@cmdtodevice", cmdtodevice);
            int id = Usercommand.ExecuteNonQuery();
        }

        private int SaveADMSUser(string EmployeeID, string EmployeeName, string CardID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            String usersql = "IF EXISTS (SELECT 1 FROM adms_db.dbo.userinfo WHERE badgenumber=@badgenumber)"
                                + " BEGIN "
                                    + " UPDATE adms_db.dbo.userinfo SET Card=@CardID,name=@name WHERE badgenumber=@badgenumber"
                                + " END "
                          + "IF NOT EXISTS (SELECT 1 FROM adms_db.dbo.userinfo WHERE badgenumber=@badgenumber) "
                                + "BEGIN "
                                        +"INSERT INTO adms_db.dbo.userinfo(badgenumber,defaultdeptid,name,Card,AccGroup) " +
                                            "VALUES (@badgenumber,1,@name,@CardID,1) "
                                + " END";

            SqlCommand Usercommand = new SqlCommand(usersql, conn);
            Usercommand.Parameters.AddWithValue("@badgenumber", EmployeeID);
            Usercommand.Parameters.AddWithValue("@name", EmployeeName);
            Usercommand.Parameters.AddWithValue("@CardID", CardID);
            int id = Usercommand.ExecuteNonQuery();
            conn.Close();
            return id;
        }
        internal string FPTemplateSyn()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string selectDatetimesql = "SELECT fp_lastupdatedtime FROM tbl_AC_AllMachine WHERE id=2";
            SqlCommand selectcommand = new SqlCommand(selectDatetimesql, conn);
            object Time = selectcommand.ExecuteScalar();
            //string strTime = Time['lastupdatedtime'].ToString();// "7/29/2015 10:02:45 AM";
            DateTime date = DateTime.Parse(Time.ToString(), System.Globalization.CultureInfo.CurrentCulture);
            string sql = "SELECT FP.EmployeeID,FP.LastModifiedDate AS ModifiedDate " +
                            ",FP.FingerID,FP.Template FROM  tb_GE_DeviceMapping  AS D " +
                                 " LEFT JOIN  tb_IO_FingerTemplateZK AS FP ON D.EmployeeID= FP.EmployeeID " +
                                     "WHERE DeviceNo=2 AND FP.LastModifiedDate > @date ORDER BY FP.LastModifiedDate ASC";
            var lastsyntime = "";
            int insertCount = 0;
            using (SqlCommand command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@date", date);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         var EmployeeID = reader["EmployeeID"].ToString();
                         var FingerID = reader["FingerID"].ToString();
                         var FPTemplate = "";
                         var Lastmodifieddate = reader["ModifiedDate"].ToString();
                         lastsyntime = Lastmodifieddate;
                         if (FingerID !="") { 
                             byte[] FPTemplateByte = (byte[])reader["Template"];
                             FPTemplate = Convert.ToBase64String(FPTemplateByte);
                         }

                         var FPlength = FPTemplate.Length;
                         int insertedid = this.SaveADMSUserFPTemplate(EmployeeID, FPTemplate, FingerID, Lastmodifieddate);
                        if (FingerID != "" && insertedid>0)
                        {
                            var cmdtodevice = "DATA FP\tPIN=" + EmployeeID + "\tFID=" + FingerID + "\tSize=" + FPlength + "\tValid=1\tTMP=" + FPTemplate;
                            this.SaveADMSCmd(cmdtodevice);//Save cmd to ADMS
                        }
                        insertCount++;
                    }
                   
                }
            }
            if (lastsyntime != "")
            {
                string updateDatetimesql = "UPDATE tbl_AC_AllMachine SET fp_lastupdatedtime=@lastsyntime WHERE id=2";
                SqlCommand updatecommand = new SqlCommand(updateDatetimesql, conn);
                updatecommand.Parameters.AddWithValue("@lastsyntime", lastsyntime);
                updatecommand.ExecuteNonQuery();
            }
            DeleteFPTemplateUser();
            return insertCount.ToString();
        }

        private void DeleteFPTemplateUser()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string DiffUsersql = "SELECT badgenumber,T.userid,FingerID FROM  adms_db.dbo.userinfo AS U  " +
                                     " LEFT JOIN adms_db.dbo.template AS T ON T.userid= U.userid" +
                                         " WHERE badgenumber NOT IN (SELECT FP.EmployeeID FROM  tb_IO_FingerTemplateZK AS FP " +
                                            "  LEFT JOIN  tb_GE_DeviceMapping AS D ON FP.EmployeeID= D.EmployeeID " +
                                                " WHERE DeviceNo=2)";
            using (SqlCommand command = new SqlCommand(DiffUsersql, conn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var userid = reader["userid"].ToString();
                        var badgenumber = reader["badgenumber"].ToString();
                        var FingerID = reader["FingerID"].ToString();
                        string DeletFPesql = "DELETE FROM adms_db.dbo.template WHERE userid=@userid AND FingerID=@FingerID";
                        SqlCommand deleteFPcommand = new SqlCommand(DeletFPesql, conn);
                        deleteFPcommand.Parameters.AddWithValue("@userid", userid);
                        deleteFPcommand.Parameters.AddWithValue("@FingerID", FingerID);
                        deleteFPcommand.ExecuteNonQuery();

                        var cmdtodevice = "DATA DEL_FP PIN=" + badgenumber;
                        this.SaveADMSCmd(cmdtodevice);//Save User cmd to ADMS
                    }
                }
            }
        }
        private int SaveADMSUserFPTemplate(string EmployeeID, string FPTemplate, string FingerID, string UTime)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string selectUserIDsql = "SELECT userid FROM adms_db.dbo.userinfo WHERE badgenumber=@EmployeeID";
            SqlCommand selectcommand = new SqlCommand(selectUserIDsql, conn);
            selectcommand.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            object userid = selectcommand.ExecuteScalar();
            String usersql = "IF EXISTS (SELECT 1 FROM adms_db.dbo.template WHERE userid=@userid AND FingerID=@FingerID)"
                                + " BEGIN "
                                    + " UPDATE adms_db.dbo.template SET userid=@userid,Template=@Template WHERE userid=@userid AND FingerID=@FingerID "
                                + " END "
                          + "IF NOT EXISTS (SELECT 1 FROM adms_db.dbo.template WHERE userid=@userid AND FingerID=@FingerID) "
                                + "BEGIN "
                                        + "INSERT INTO adms_db.dbo.template(userid,Template,FingerID,Valid,SN,UTime) " +
                                            "VALUES (@userid,@Template,@FingerID,1,'3281152800001',@UTime) "
                                + " END";

            SqlCommand Usercommand = new SqlCommand(usersql, conn);
            Usercommand.Parameters.AddWithValue("@userid", userid);
            Usercommand.Parameters.AddWithValue("@Template", FPTemplate);
            Usercommand.Parameters.AddWithValue("@FingerID", FingerID);
            Usercommand.Parameters.AddWithValue("@UTime", UTime);
            int id = Usercommand.ExecuteNonQuery();
            conn.Close();
            return id;
        }
        internal string FPLog()
        {

            // TO Save GEMS Table;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConnectionString;
            conn.Open();
            string selectDatetimesql = "SELECT log_lastupdatedtime FROM tbl_AC_AllMachine WHERE id=2";
            SqlCommand selectcommand = new SqlCommand(selectDatetimesql, conn);
            object Time = selectcommand.ExecuteScalar();
            //string strTime = Time['lastupdatedtime'].ToString();// "7/29/2015 10:02:45 AM";
            DateTime date = DateTime.Parse(Time.ToString(), System.Globalization.CultureInfo.CurrentCulture);
            var lastsyntime = "";
            string DiffUsersql = "SELECT CO.userid,checktime,checktype,verifycode,CO.SN,badgenumber FROM adms_db.dbo.checkinout AS CO  " +
                                 " LEFT JOIN adms_db.dbo.userinfo AS U ON CO.userid=U.userid WHERE checktime>@date ORDER BY checktime ASC";
            using (SqlCommand command = new SqlCommand(DiffUsersql, conn))
            {
                command.Parameters.AddWithValue("@date", date);
                using (SqlDataReader reader = command.ExecuteReader())
                {  
                    while (reader.Read())
                    {
                        var badgenumber = reader["badgenumber"].ToString();
                        var checktime = reader["checktime"].ToString();
                        var checktype = reader["checktype"].ToString();
                        lastsyntime = checktime;
                        String Insertsql = "INSERT INTO tb_IOTA_EmployeeLog (EmployeeID,Press_Time,InActive,MachineID,Is_In) VALUES (@EmployeeID,@Press_Time,'False',2,@Is_In);";
                        SqlCommand Insertcommand = new SqlCommand(Insertsql, conn);
                        Insertcommand.Parameters.AddWithValue("@EmployeeID", badgenumber);
                        Insertcommand.Parameters.AddWithValue("@Press_Time", checktime);
                        Insertcommand.Parameters.AddWithValue("@Is_In", checktype);
                        //Insertcommand.ExecuteNonQuery();
                        try
                        {
                            int Elog = Insertcommand.ExecuteNonQuery();
                            if (Elog > 0)
                            {
                                Console.Write("Save TO GEMS Table");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.Write("ERROR TO GEMS Table" + e);
                        }
                  }
                if (lastsyntime != "")
                {
                    string updateDatetimesql = "UPDATE tbl_AC_AllMachine SET log_lastupdatedtime=@lastsyntime WHERE id=2";
                    SqlCommand updatecommand = new SqlCommand(updateDatetimesql, conn);
                    updatecommand.Parameters.AddWithValue("@lastsyntime", lastsyntime);
                    updatecommand.ExecuteNonQuery();
                }
              }
         }
         return "aaa";
      }
    }
}
