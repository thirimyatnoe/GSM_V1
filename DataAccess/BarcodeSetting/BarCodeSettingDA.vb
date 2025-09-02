Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Data.Common

Namespace BarcodeSetting
    Public Class BarCodeSettingDA
        Implements IBarcodeSettingDA

#Region "Private Member"
        Private DB As Database
        Private Shared ReadOnly _instance As IBarcodeSettingDA = New BarCodeSettingDA
#End Region
#Region "Constructor"
        Private Sub New()
            DB = DatabaseFactory.CreateDatabase
        End Sub
#End Region

#Region "Public Property"
        Public Shared ReadOnly Property Instance() As IBarcodeSettingDA
            Get
                Return _instance
            End Get
        End Property
#End Region

        Public Function DeleteBarcodeSetting() As Boolean Implements IBarcodeSettingDA.DeleteBarcodeSetting
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_BarcodeSetting "
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
        Public Function InsertBarcodSetting(objBarcode As BarcodePrinterInfo) As Boolean Implements IBarcodeSettingDA.InsertBarcodSetting
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " INSERT INTO tbl_BarcodeSetting (PaperWidth,PaperHeight,X,Y,Height,Narrow,Wide,PrinterName, IsLocationName,EngName,MMName,RightPositionX,IsIncludeGemWgt,IsIncludeGemPrice,IsPrefix,IsFixGemWeight,IsFixGold,IsLength,IsAllDetail,IsFixItem,IsFixGemQTY, IsOriginalCode, IsPriceCode, IsWaste, IsDescription, IsGram,IsShowGW,IsFixPrice,LeftFontSize,RightFontSize)  VALUES (@PaperWidth,@PaperHeight,@X,@Y,@Height,@Narrow,@Wide,@PrinterName, @IsLocationName,@EngName,@MMName,@RightPositionX,@IsIncludeGemWgt,@IsIncludeGemPrice,@IsPrefix,@IsFixGemWeight,@IsFixGold,@IsLength,@IsAllDetail,@IsFixItem,@IsFixGemQTY, @IsOriginalCode, @IsPriceCode, @IsWaste, @IsDescription, @IsGram,@IsShowGW,@IsFixPrice,@LeftFontSize,@RightFontSize)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@PaperWidth", DbType.String, objBarcode.PaperWidth)
                DB.AddInParameter(DBComm, "@PaperHeight", DbType.String, objBarcode.PaperHeight)
                DB.AddInParameter(DBComm, "@X", DbType.String, objBarcode.X)
                DB.AddInParameter(DBComm, "@Y", DbType.String, objBarcode.Y)
                DB.AddInParameter(DBComm, "@Height", DbType.String, objBarcode.Height)
                DB.AddInParameter(DBComm, "@Narrow", DbType.String, objBarcode.Narrow)
                DB.AddInParameter(DBComm, "@Wide", DbType.String, objBarcode.Wide)
                DB.AddInParameter(DBComm, "@PrinterName", DbType.String, objBarcode.PrinterName)
                DB.AddInParameter(DBComm, "@IsLocationName", DbType.Boolean, objBarcode.IsLocationName)
                DB.AddInParameter(DBComm, "@EngName", DbType.String, objBarcode.EngName)
                DB.AddInParameter(DBComm, "@MMName", DbType.String, objBarcode.MMName)
                DB.AddInParameter(DBComm, "@RightPositionX", DbType.String, objBarcode.RightPositionX)
                DB.AddInParameter(DBComm, "@IsIncludeGemWgt", DbType.Boolean, objBarcode.IsIncludeGemWgt)
                DB.AddInParameter(DBComm, "@IsIncludeGemPrice", DbType.Boolean, objBarcode.IsIncludeGemPrice)
                DB.AddInParameter(DBComm, "@IsPrefix", DbType.Boolean, objBarcode.IsPrefix)
                DB.AddInParameter(DBComm, "@IsFixGemWeight", DbType.Boolean, objBarcode.IsFixGemWeight)
                DB.AddInParameter(DBComm, "@IsFixGold", DbType.Boolean, objBarcode.IsFixGold)
                DB.AddInParameter(DBComm, "@IsLength", DbType.Boolean, objBarcode.IsLength)
                DB.AddInParameter(DBComm, "@IsAllDetail", DbType.Boolean, objBarcode.IsAllDetail)
                DB.AddInParameter(DBComm, "@IsFixItem", DbType.Boolean, objBarcode.IsFixItem)
                DB.AddInParameter(DBComm, "@IsFixGemQTY", DbType.Boolean, objBarcode.IsFixGemQTY)
                DB.AddInParameter(DBComm, "@IsOriginalCode", DbType.Boolean, objBarcode.IsOriginalCode)
                DB.AddInParameter(DBComm, "@IsPriceCode", DbType.Boolean, objBarcode.IsPriceCode)
                DB.AddInParameter(DBComm, "@IsWaste", DbType.Boolean, objBarcode.IsWaste)
                DB.AddInParameter(DBComm, "@IsDescription", DbType.Boolean, objBarcode.IsDescription)
                DB.AddInParameter(DBComm, "@IsGram", DbType.Boolean, objBarcode.IsShowGram)
                DB.AddInParameter(DBComm, "@IsShowGW", DbType.Boolean, objBarcode.IsShowGW)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, objBarcode.IsFixPrice)
                DB.AddInParameter(DBComm, "@LeftFontSize", DbType.Int32, objBarcode.LeftFontSize)
                DB.AddInParameter(DBComm, "@RightFontSize", DbType.Int32, objBarcode.RightFontSize)

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

        Public Function GetBarcode() As DataTable Implements IBarcodeSettingDA.GetBarcode
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * FROM tbl_BarcodeSetting"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
    End Class
End Namespace

