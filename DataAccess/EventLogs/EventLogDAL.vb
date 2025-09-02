Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data
Imports System.Data.Common

Namespace EventLogs
    Public Class EventLogDAL
        Private DB As Database

        Public Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

        Private m_strsql As String = ""

        Public Sub AddLog(ByVal State As EnumSetting.EventState, ByVal LogDateTime As Nullable(Of DateTime), ByVal LogInUser As String, ByVal Source As String, ByVal DataChanged As String, ByVal AffectedID As String, ByVal LogMessage As String, Optional ByVal ErrMessage As String = "")
            Try
                Dim DBComm As DbCommand
                If LogMessage.Length > 200 Then
                    LogMessage = LogMessage.Substring(0, 199)
                End If
                If ErrMessage IsNot Nothing Then
                    If ErrMessage.Length > 500 Then
                        ErrMessage = ErrMessage.Substring(0, 499)
                    End If
                End If


                m_strsql = "INSERT INTO tb_GE_EventLogs(LogType, LogDateTime, LogInUser, Source, DataChange, AffectedID, LogMessage, ErrMessage) VALUES (@LogType, @LogDateTime, @LogInUser, @Source, @DataChanged, @AffectedID, @LogMessage, @ErrMessage)"
                DBComm = DB.GetSqlStringCommand(m_strsql)
                DB.AddInParameter(DBComm, "@LogType", DbType.String, State)
                DB.AddInParameter(DBComm, "@LogDateTime", DbType.Date, LogDateTime)
                DB.AddInParameter(DBComm, "@LogInUser", DbType.String, LogInUser)
                DB.AddInParameter(DBComm, "@Source", DbType.String, Source)
                DB.AddInParameter(DBComm, "@DataChanged", DbType.String, DataChanged)
                DB.AddInParameter(DBComm, "@AffectedID", DbType.String, AffectedID)
                DB.AddInParameter(DBComm, "@LogMessage", DbType.String, LogMessage)
                DB.AddInParameter(DBComm, "@ErrMessage", DbType.String, ErrMessage)
                DB.ExecuteNonQuery(DBComm)
            Catch ex As Exception

            End Try
        End Sub

        Public Function ReadLog(ByVal argFromDate As Nullable(Of DateTime), ByVal argToDate As Nullable(Of DateTime), ByVal argType As String, ByVal argSource As String, ByVal argAffected As String, Optional ByVal argDataChange As String = "") As DataTable
            Dim dt As New DataTable
            Dim sqlStr As String = ""
            Dim Par As String = ""
            Dim DBComm As DbCommand

            'If argSource = CommonInfo.EnumSetting.GenerateKeyType.BarcodeNo.ToString() Then
            '    sqlStr = " Select G.*,IsNull((SELECT ItemCode FROM tbl_ForSale F Where F.ForSaleID=G.AffectedID),'-') AS ItemCode From tb_GE_EventLogs G "
            'Else
            sqlStr = " Select * From tb_GE_EventLogs "
            'End If


            If argFromDate.HasValue = True Then
                sqlStr += " Where LogDateTime  BETWEEN @argFromDate and @argToDate "
            End If
            If argType <> "" Then
                If (argFromDate.HasValue) Then sqlStr += " And " Else sqlStr += "Where "
                sqlStr += "LogType = '" & argType & "'"
            End If
            If argSource <> "" Then
                'If argFromDate.HasValue = False AndAlso argType = "" Then
                '    sqlStr += " Where "
                'Else
                'sqlStr += " And "
                sqlStr += " And Source = '" & argSource & "'"
            End If

            If argAffected <> "" Then
                sqlStr += argAffected
            End If

            If argDataChange <> "" Then  'tt
                If (argFromDate.HasValue) Then sqlStr += " And " Else sqlStr += "Where "
                sqlStr += "DataChange = '" & argDataChange & "'"
            End If

            sqlStr += " Order By LogDateTime desc"
            'End If
            DBComm = DB.GetSqlStringCommand(sqlStr)
            If argFromDate.HasValue = True Then
                DB.AddInParameter(DBComm, "@argFromDate", DbType.DateTime, argFromDate.Value)
                DB.AddInParameter(DBComm, "@argToDate", DbType.DateTime, argToDate.Value.AddDays(1))
            End If
            dt.Load(DB.ExecuteReader(DBComm))
            Return dt
        End Function

        Public Function ClearAllLogNow() As Boolean

            Dim strCommandText As String = ""
            Dim DBComm As DbCommand

            strCommandText = "DELETE FROM tb_GE_EventLogs"
            Try
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.ExecuteNonQuery(DBComm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function ClearAllLogOlder(ByVal OldDate As Date) As Boolean

            Dim strCommandText As String = ""
            Dim DBComm As DbCommand

            strCommandText = "DELETE FROM tb_GE_EventLogs WHERE LogDateTime < @OldDate "
            Try
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@OldDate", DbType.DateTime, OldDate)
                DB.ExecuteNonQuery(DBComm)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function GetForAffectedIDByBarcodeNo(ByVal argAffected As String) As String
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand

            strCommandText = "SELECT Substring(AffectedID,CHARINDEX (',',AffectedID)+2,13) FROM tb_GE_EventLogs "
            strCommandText += "WHERE Source='BarcodeNo' AND CHARINDEX ( ',',AffectedID)>0 AND Substring(AffectedID,0,CHARINDEX ( ',',AffectedID)) =  '" & argAffected & "'"
            Try
                DBComm = DB.GetSqlStringCommand(strCommandText)
                GetForAffectedIDByBarcodeNo = CStr(DB.ExecuteScalar(DBComm))
            Catch ex As Exception
                Return ""
            End Try
        End Function
    End Class
End Namespace