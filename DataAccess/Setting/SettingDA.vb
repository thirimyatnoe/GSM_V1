Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.data
Imports System.Data.Common
Namespace Setting
    Friend Class SettingDA
        Implements ISettingDA













        Private DB As Database
        Private Shared ReadOnly _instance As ISettingDA = New SettingDA
        Private objEvent As New EventLogs.EventLogDAL
        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub
        Public Shared ReadOnly Property Instance() As ISettingDA
            Get
                Return _instance
            End Get
        End Property

        Public Function SaveSetting(ByVal settingobj As CommonInfo.SettingInfo) As Boolean Implements ISettingDA.SaveSetting
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Try

                strCommandText = "Insert Into tbl_Setting(KeyName,Description,KeyValue) Values (@KeyName,@Description,@KeyValue)"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeyName", DbType.String, settingobj.KeyName)
                DB.AddInParameter(DBComm, "@Description", DbType.String, settingobj.Description)
                DB.AddInParameter(DBComm, "@KeyValue", DbType.String, settingobj.KeyValue)


                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Point Of Sale")
            End Try
        End Function
        Public Function DeleteSetting(ByVal KeyName As String) As Boolean Implements ISettingDA.DeleteSetting
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand

                strCommandText = "DELETE FROM tbl_Setting WHERE KeyName = @KeyName"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@KeyName", DbType.String, KeyName)

                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "Point Of Sale")
                Return False
            End Try


        End Function

        Public Function GetApplicationOptionByKeyName(ByVal KeyName As String) As Object Implements ISettingDA.GetApplicationOptionByKeyName
            Dim strCommandText As String = ""
            Dim retObj As Object

            strCommandText = "SELECT TOP 1 KeyValue FROM tbl_Setting WHERE KeyName = @KeyName"

            Try
                Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeyName", DbType.String, KeyName)
                retObj = DB.ExecuteScalar(DBComm)

                If retObj Is Nothing Then Return "" Else Return retObj
            Catch ex As Exception

                Return ""
            End Try
        End Function
        Public Function getdatabykeyname(ByVal KeyName As String) As CommonInfo.SettingInfo Implements ISettingDA.getdatabykeyname
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim Settingobj As New SettingInfo("", "", "")

            Try

                strCommandText = "select Description,KeyValue from tbl_Setting where KeyName=@KeyName"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeyName", DbType.String, KeyName)

                'drresult = DB.ExecuteDataSet(DBComm)
                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With Settingobj
                        .Description = drResult("Description")
                        .KeyValue = drResult("KeyValue")

                    End With
                End If

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Critical, "Point Of Sale")

            End Try
            DBComm.Connection.Close()
            Return Settingobj

        End Function
        Public Function GetKeyvalue(ByVal KeyName As String) As CommonInfo.SettingInfo Implements ISettingDA.GetKeyvalue
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim Settingobj As New SettingInfo("", "", "")

            Try

                strCommandText = "select KeyValue from tbl_Setting where KeyName=@KeyName"

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeyName", DbType.String, KeyName)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then

                    With Settingobj
                        .KeyValue = drResult("KeyValue")



                    End With
                End If

            Catch ex As Exception

                MsgBox(ex.Message, MsgBoxStyle.Critical, "Point Of Sale")

            End Try

            Return Settingobj

        End Function

        Public Function GetKeyName() As System.Data.DataTable Implements ISettingDA.GetKeyName
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "select KeyName, Description,KeyValue from tbl_Setting "

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Point Of Sale")
                Return New DataTable
            End Try
        End Function



        Public Function GetApplicationOptionByKeyNameBy(ByVal KeyName As String, ByVal CurrentUserID As String) As Object Implements ISettingDA.GetApplicationOptionByKeyNameBy
            Dim strCommandText As String = ""
            Dim retObj As Object

            strCommandText = "SELECT TOP 1 KeyValue FROM tbl_Setting WHERE KeyName = @KeyName"

            Try
                Dim DBComm As DbCommand = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeyName", DbType.String, KeyName)
                retObj = DB.ExecuteScalar(DBComm)

                If retObj Is Nothing Then Return "" Else Return retObj

            Catch ex As Exception
                'objEvent.AddLog(CommonInfo.EnumSetting.EventState.Error, Now, "GetApplicationOptionByKeyName, when trying to get Value with keyname " & KeyName, ex.Message.ToString(), ex.InnerException.ToString)
                Return ""
            End Try
        End Function

        Public Function SaveSettingBy(ByVal settingobj As CommonInfo.SettingInfo, ByVal CurrentUserID As String) As Boolean Implements ISettingDA.SaveSettingBy
            Dim strCommandText As String = ""
            Dim DBComm As DbCommand
            Try

                If settingobj.KeyName <> "FPSerialNumber" And settingobj.Description <> "FPSerialNumber" Then
                    strCommandText = "DELETE FROM tbl_Setting WHERE KeyName = @KeyName "
                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@KeyName", DbType.String, settingobj.KeyName)
                    DB.ExecuteNonQuery(DBComm)
                End If

                strCommandText = "INSERT INTO tbl_Setting Values(@KeyName, @Description, @KeyValue)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@KeyName", DbType.String, settingobj.KeyName)
                DB.AddInParameter(DBComm, "@Description", DbType.String, settingobj.Description)
                DB.AddInParameter(DBComm, "@KeyValue", DbType.String, settingobj.KeyValue)
                DB.ExecuteNonQuery(DBComm)

                'objEvent.AddLog(CommonInfo.EnumSetting.EventState.Information, Now, "frm_GE_ApplicationOption", "Save Successfully! Key Name = " & settingobj.KeyName)
                Return True

            Catch ex As Exception

                Return False
            End Try
        End Function
        Public Function GetVersion() As String Implements ISettingDA.GetVersion
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try

                strCommandText = "SELECT Top 1 VersionNo FROM tbl_Version order by VersionNo desc"

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult.Rows(0).Item(0)

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Point Of Sale")
                Return ""
            End Try
        End Function
    End Class

End Namespace
