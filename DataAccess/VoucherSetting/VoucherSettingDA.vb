Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.data
Imports System.Data.Common
Namespace VoucherSetting
    Public Class VoucherSettingDA
        Implements IVoucherSettingDA

#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As IVoucherSettingDA = New VoucherSettingDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IVoucherSettingDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function GetAllVoucherSetting() As System.Data.DataTable Implements IVoucherSettingDA.GetAllVoucherSetting
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * FROM tbl_VoucherSetting"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function GetAllVoucherSettingByVoucher() As System.Data.DataTable Implements IVoucherSettingDA.GetAllVoucherSettingByVoucher
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT TitleType, Title, FontName, Bold, Italic, FontSize, FontColor, Photo, FontR, FontG, FontB FROM tbl_VoucherSetting"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function InsertVoucherSetting(ByVal info As VoucherSettingInfo) As Boolean Implements IVoucherSettingDA.InsertVoucherSetting
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " INSERT INTO tbl_VoucherSetting VALUES (@TitleType, @Title, @FontName, @Bold, @Italic, @FontSize, @FontColor, @Photo, @FontR, @FontG, @FontB)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@TitleType", DbType.String, info.TitleType)
                DB.AddInParameter(DBComm, "@Title", DbType.String, info.Title)
                DB.AddInParameter(DBComm, "@FontName", DbType.String, info.FontName)
                DB.AddInParameter(DBComm, "@Bold", DbType.Boolean, info.Bold)
                DB.AddInParameter(DBComm, "@Italic", DbType.Boolean, info.Italic)
                DB.AddInParameter(DBComm, "@FontSize", DbType.Int64, info.FontSize)
                DB.AddInParameter(DBComm, "@FontColor", DbType.String, info.FontColor)
                DB.AddInParameter(DBComm, "@Photo", DbType.String, info.Photo)
                DB.AddInParameter(DBComm, "@FontR", DbType.Int64, info.FontR)
                DB.AddInParameter(DBComm, "@FontG", DbType.Int64, info.FontG)
                DB.AddInParameter(DBComm, "@FontB", DbType.Int64, info.FontB)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function

        Public Function DeleteVoucherSetting() As Boolean Implements IVoucherSettingDA.DeleteVoucherSetting
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_VoucherSetting"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                If DB.ExecuteNonQuery(DBComm) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                MsgBox("Cannot Delete ", MsgBoxStyle.Critical, "GoldSmith Management System")
                Return False
            End Try
        End Function
    End Class

End Namespace