Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.data
Imports System.Data.Common
Namespace GlobalSetting
    Public Class GlobalSettingDA
        Implements IGlobalSettingDA

#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As IGlobalSettingDA = New GlobalSettingDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGlobalSettingDA
            Get
                Return _instance
            End Get
        End Property

#End Region
        Public Function GetAllGlobalSetting() As System.Data.DataTable Implements IGlobalSettingDA.GetAllGlobalSetting
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable
            Try
                strCommandText = "SELECT * FROM tbl_GlobalSetting"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function InsertGlobalSetting(ByVal info As GlobalSettingInfo) As Boolean Implements IGlobalSettingDA.InsertGlobalSetting
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Try
                strCommandText = " INSERT INTO tbl_GlobalSetting(Photo,DatabaseSharePath,IsCarat,IsReuseBarcode,AllowDis,IsCash,IsExactPrice,DiffPurchaseRate,DiffChangeRate,IsSpeedEntry,DecimalFormat,IsAllowUpdate,InterestRate,InterestPeriod,EnablePaidAmount,IsAllowSaleReturn,IsAllowSale,IsAllowStock,QRCode,IsUsedSettingPeriod,AllowStockWeight,IsOneMonthCalculation,OverDay,IsHoToBranch,MachineType,Prefix,PostFix,IsHoMaster,SoftwareVendorSetting,IsFixPrice,IsUseMember,IsMemberCustomer,RegName) VALUES (@Photo,@DatabaseSharePath,@IsCarat,@IsReuseBarcode,@AllowDis, @IsCash, @IsExactPrice, @DiffPurchaseRate, @DiffChangeRate,@IsSpeedEntry,@DecimalFormat,@IsAllowUpdate,@InterestRate,@InterestPeriod,@EnablePaidAmount,@IsAllowSaleReturn,@IsAllowSale,@IsAllowStock,@QRCode,@IsUsedSettingPeriod,@AllowStockWeight,@IsOneMonthCalculation,@OverDay,@IsHoToBranch,@MachineType,@Prefix,@PostFix,@IsHoMaster,@SoftwareVendorSetting,@IsFixPrice,@IsUseMember,@IsMemberCustomer,@RegName)"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@Photo", DbType.String, info.Photo)
                DB.AddInParameter(DBComm, "@DatabaseSharePath", DbType.String, info.DatabaseSharePath)
                DB.AddInParameter(DBComm, "@IsCarat", DbType.Int32, info.IsCarat)
                DB.AddInParameter(DBComm, "@IsReuseBarcode", DbType.Boolean, info.IsReuseBarcode)
                DB.AddInParameter(DBComm, "@AllowDis", DbType.Int32, info.AllowDis)
                DB.AddInParameter(DBComm, "@IsCash", DbType.Boolean, info.IsCash)
                DB.AddInParameter(DBComm, "@IsExactPrice", DbType.Boolean, info.IsExactPrice)
                DB.AddInParameter(DBComm, "@DiffPurchaseRate", DbType.Int32, info.DiffPurchaseRate)
                DB.AddInParameter(DBComm, "@DiffChangeRate", DbType.Int32, info.DiffChangeRate)
                DB.AddInParameter(DBComm, "@IsSpeedEntry", DbType.Boolean, info.IsSpeedEntry)
                DB.AddInParameter(DBComm, "@DecimalFormat", DbType.Int32, info.DecimalFormat)
                DB.AddInParameter(DBComm, "@IsAllowUpdate", DbType.Boolean, info.IsAllowUpdate)
                DB.AddInParameter(DBComm, "@InterestRate", DbType.Int32, info.InterestRate)
                DB.AddInParameter(DBComm, "@InterestPeriod", DbType.Int32, info.InterestPeriod)
                DB.AddInParameter(DBComm, "@EnablePaidAmount", DbType.Boolean, info.EnablePaidAmount)
                DB.AddInParameter(DBComm, "@IsAllowSaleReturn", DbType.Boolean, info.IsAllowSaleReturn)
                DB.AddInParameter(DBComm, "@IsAllowSale", DbType.Boolean, info.IsAllowSale)
                DB.AddInParameter(DBComm, "@IsAllowStock", DbType.Boolean, info.IsAllowStock)
                DB.AddInParameter(DBComm, "@QRCode", DbType.String, info.QRCode)
                DB.AddInParameter(DBComm, "@IsUsedSettingPeriod", DbType.Boolean, info.IsUsedSettingPeriod)
                DB.AddInParameter(DBComm, "@AllowStockWeight", DbType.Decimal, info.AllowEditWeight)
                DB.AddInParameter(DBComm, "@IsOneMonthCalculation", DbType.Boolean, info.IsOneMonthCalculation)
                DB.AddInParameter(DBComm, "@OverDay", DbType.Int32, info.OverDay)
                DB.AddInParameter(DBComm, "@IsHoToBranch", DbType.Boolean, info.IsHoToBranch)
                DB.AddInParameter(DBComm, "@MachineType", DbType.String, info.MachineType)
                DB.AddInParameter(DBComm, "@Prefix", DbType.Int32, info.Prefix)
                DB.AddInParameter(DBComm, "@Postfix", DbType.Int32, info.Postfix)
                DB.AddInParameter(DBComm, "@IsHoMaster", DbType.Boolean, info.IsHoMaster)
                DB.AddInParameter(DBComm, "@SoftwareVendorSetting", DbType.Boolean, info.SoftwareVendorSetting)
                DB.AddInParameter(DBComm, "@IsFixPrice", DbType.Boolean, info.IsFixPrice)
                DB.AddInParameter(DBComm, "@IsUseMember", DbType.Boolean, info.IsUseMember)
                DB.AddInParameter(DBComm, "@IsMemberCustomer", DbType.Boolean, info.IsMemberCustomer)
                DB.AddInParameter(DBComm, "@RegName", DbType.String, info.RegName)

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

        Public Function DeleteGlobalSetting() As Boolean Implements IGlobalSettingDA.DeleteGlobalSetting
            Try
                Dim strCommandText As String
                Dim DBComm As DbCommand
                strCommandText = "DELETE FROM tbl_GlobalSetting"
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

        Public Function GetAllGlobalSettingInfo() As CommonInfo.GlobalSettingInfo Implements IGlobalSettingDA.GetAllGlobalSettingInfo
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim drResult As IDataReader
            Dim objGlobalInfo As New GlobalSettingInfo
            Try
                strCommandText = " select * from tbl_GlobalSetting "
                DBComm = DB.GetSqlStringCommand(strCommandText)

                drResult = DB.ExecuteReader(DBComm)
                If drResult.Read() Then
                    With objGlobalInfo
                        .Photo = drResult("Photo")
                        .DatabaseSharePath = drResult("DatabaseSharePath")
                        .IsCarat = drResult("IsCarat")
                        .IsReuseBarcode = drResult("IsReuseBarcode")
                        .AllowDis = drResult("AllowDis")
                        .IsExactPrice = drResult("IsExactPrice")
                        .DiffChangeRate = drResult("DiffChangeRate")
                        .DiffPurchaseRate = drResult("DiffPurchaseRate")
                        .IsSpeedEntry = drResult("IsSpeedEntry")
                        .DecimalFormat = drResult("DecimalFormat")
                        .IsAllowUpdate = drResult("IsAllowUpdate")
                        .InterestRate = drResult("InterestRate")
                        .InterestPeriod = drResult("InterestPeriod")
                        .EnablePaidAmount = drResult("EnablePaidAmount")
                        .IsAllowSaleReturn = drResult("IsAllowSaleReturn")
                        .IsAllowSale = drResult("IsAllowSale")
                        .IsAllowStock = drResult("IsAllowStock")
                        .QRCode = drResult("QRCode")
                        .IsUsedSettingPeriod = drResult("IsUsedSettingPeriod")
                        .AllowEditWeight = drResult("AllowStockWeight")
                        .IsOneMonthCalculation = drResult("IsOneMonthCalculation")
                        .OverDay = drResult("OverDay")
                        .IsHoToBranch = drResult("IsHoToBranch")
                        .MachineType = drResult("MachineType")
                        .Prefix = drResult("Prefix")
                        .Postfix = drResult("Postfix")
                        .IsHoMaster = drResult("IsHoMaster")
                        .IsFixPrice = drResult("IsFixPrice")
                    End With
                End If
                drResult.Close()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Critical, "GoldSmith Management System")
            End Try
            Return objGlobalInfo
        End Function
    End Class

End Namespace