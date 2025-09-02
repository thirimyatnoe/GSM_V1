Imports CommonInfo
Imports Microsoft.Practices.EnterpriseLibrary.Common
Imports Microsoft.Practices.EnterpriseLibrary.data
Imports System.Data.Common

Namespace General

    Public Class GeneralDA
        Implements IGeneralDA

#Region "Private Members"

        Private DB As Database
        Private Shared ReadOnly _instance As IGeneralDA = New GeneralDA

#End Region

#Region "Constructors"

        Private Sub New()
            DB = DatabaseFactory.CreateDatabase()
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IGeneralDA
            Get
                Return _instance
            End Get
        End Property

#End Region


        ' <summary>
        ' Generate ID base on custom format, date, prefix and postfix
        ''</summary>
        ' <param name="KeyType"></param>
        ' <param name="GenerateFormat">ddMMMyyyy0000</param>
        ' <param name="GenerateDate"></param>
        ' <param name="Prefix"></param>
        ' <param name="Postfix"></param>
        ' <returns></returns>
        ' <remarks>Create ZMN - 15/Feb/2008</remarks>
        Private Function GetMaximumCode(ByVal TableName As String, ByVal ColName As String, Optional ByVal GenerateOn As String = "") As Integer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            strCommandText = "SELECT MAX(" & ColName & ") FROM " & TableName & "" & GenerateOn & ""
            DBComm = DB.GetSqlStringCommand(strCommandText)

            If IsNumeric(IIf(IsDBNull(DB.ExecuteScalar(DBComm)), "0", DB.ExecuteScalar(DBComm))) Then
                GetMaximumCode = CInt(IIf(IsDBNull(DB.ExecuteScalar(DBComm)), "0", DB.ExecuteScalar(DBComm)))
            Else
                GetMaximumCode = 0
            End If

        End Function
        Public Function GetGenerateKey(ByVal KeyType As String, ByVal GenerateFormat As String, ByVal GenerateOn As String, ByVal GenerateDate As Date, ByVal Prefix As String, ByVal Postfix As String) As String Implements IGeneralDA.GetGenerateKey

            Dim retStr As String
            Dim strFormat As String
            Dim intGenerateID As Integer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim cristr As String = ""
            Dim test As String = ""

            If KeyType = CommonInfo.EnumSetting.GenerateKeyType.ItemCode.ToString Then
                If GenerateOn.Length = 3 Then
                    cristr = " Where substring(ItemCode,1,7) = '" & GenerateOn.Substring(0, 3) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,8,4)", cristr)
                ElseIf GenerateOn.Length = 4 Then
                    cristr = " Where substring(ItemCode,1,8) = '" & GenerateOn.Substring(0, 4) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,9,4)", cristr)
                ElseIf GenerateOn.Length = 5 Then
                    cristr = " Where substring(ItemCode,1,9) = '" & GenerateOn.Substring(0, 5) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,10,4)", cristr)
                ElseIf GenerateOn.Length = 6 Then
                    cristr = " Where substring(ItemCode,1,10) = '" & GenerateOn.Substring(0, 6) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,11,4)", cristr)
                End If

                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                retStr = intGenerateID.ToString(GenerateFormat)


            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.CustomerCode.ToString Then
                cristr = " Where substring(CustomerCode,1,2) = '" & Format(GenerateDate, "yy") & "'"
                intGenerateID = GetMaximumCode("tbl_Customer", "SubString(CustomerCode, 3, 8)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                Prefix = Format(GenerateDate, "yy")
                retStr = Prefix & intGenerateID.ToString(GenerateFormat)

            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.SupplierCode.ToString Then
                cristr = " Where substring(SupplierCode,1,2) = '" & Format(GenerateDate, "yy") & "'"
                intGenerateID = GetMaximumCode("tbl_Supplier", "SubString(SupplierCode, 3, 4)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                Prefix = Format(GenerateDate, "yy")
                retStr = Prefix & intGenerateID.ToString(GenerateFormat)
            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.GoldSmithCode.ToString Then
                cristr = " Where substring(GoldSmithCode,1,2) = '" & Format(GenerateDate, "yy") & "'"
                intGenerateID = GetMaximumCode("tbl_GoldSmith", "SubString(GoldSmithCode, 3, 4)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                Prefix = Format(GenerateDate, "yy")
                retStr = Prefix & intGenerateID.ToString(GenerateFormat)

            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoiceCode.ToString Then
                cristr = " Where Substring(convert(varchar(10),ReceiveDate,105),7,10) = '" & GenerateOn & "'"
                intGenerateID = GetMaximumCode("tbl_MortgageInvoice", "SubString(MortgageInvoiceCode,5,9)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                retStr = intGenerateID.ToString(GenerateFormat)

            Else

                If GenerateFormat.IndexOf("#") < 0 And GenerateFormat.IndexOf("0") < 0 Then   ''Generate format does not include place for generated ID.
                    Return GenerateFormat
                End If
                If GenerateFormat = "0" Or GenerateFormat = "#" Then   ''To overcome the converting error of single 0 or # char to dateformat
                    strFormat = Prefix & GenerateFormat & Postfix
                ElseIf GenerateFormat = "yyyy-0000000" Then
                    strFormat = Prefix & GenerateDate.ToString(GenerateFormat) & Postfix
                Else
                    strFormat = Prefix & GenerateDate.ToString(GenerateFormat) & Postfix
                End If
                strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType AND GenerateOn=@GenerateOn "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, KeyType)
                DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                intGenerateID = DB.ExecuteScalar(DBComm)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                    strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType and GenerateOn=@GenerateOn"
                Else
                    intGenerateID = 1
                    strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1, @GenerateOn)"
                End If

                retStr = intGenerateID.ToString(GenerateFormat)

                If GenerateFormat = "0" Or GenerateFormat = "#" Then   ''To overcome the converting error of single 0 or # char to dateformat
                    retStr = Prefix & retStr & Postfix
                Else
                    retStr = Prefix & GenerateDate.ToString(retStr) & Postfix
                End If
            End If

            Return retStr
        End Function
        Public Function GenerateKey(ByVal KeyType As String, ByVal GenerateFormat As String, ByVal GenerateOn As String, ByVal GenerateDate As Date, ByVal Prefix As String, ByVal Postfix As String) As String Implements IGeneralDA.GenerateKey

            Dim retStr As String
            Dim strFormat As String
            Dim intGenerateID As Integer
            Dim strCommandText As String
            Dim DBComm As DbCommand

            Dim cristr As String = ""
            Dim test As String = ""


            If KeyType = CommonInfo.EnumSetting.GenerateKeyType.ItemCode.ToString Then
                If GenerateOn.Length = 3 Then
                    cristr = " Where substring(ItemCode,1,7) = '" & GenerateOn.Substring(0, 3) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,8,4)", cristr)
                ElseIf GenerateOn.Length = 4 Then
                    cristr = " Where substring(ItemCode,1,8) = '" & GenerateOn.Substring(0, 4) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,9,4)", cristr)
                ElseIf GenerateOn.Length = 5 Then
                    cristr = " Where substring(ItemCode,1,9) = '" & GenerateOn.Substring(0, 5) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,10,4)", cristr)
                ElseIf GenerateOn.Length = 6 Then
                    cristr = " Where substring(ItemCode,1,10) = '" & GenerateOn.Substring(0, 6) & Format(GenerateDate, "MM") + Format(GenerateDate, "yy") & "'"
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,11,4)", cristr)
                End If

                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                retStr = intGenerateID.ToString(GenerateFormat)


            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.ReuseCode.ToString Then
                Dim ItemCodeCount As Integer = 0
                ItemCodeCount = CInt(GenerateOn.Substring(0, 1))

                cristr = " Where Substring(ItemCode," & ItemCodeCount + 2 & ",2) = '" & Format(GenerateDate, "yy") & "' And ItemCode like '" & GenerateOn.Substring(1) & "%' And IsExit = '1'"
                intGenerateID = GetMaximumCode("tbl_ForSale", "SubString(ItemCode," & ItemCodeCount + 4 & ", 3)", cristr)
                'cristr = " Where Substring(ItemCode,6,2) = '" & Format(GenerateDate, "yy") & "' And ItemCode like '" & GenerateOn & "%' And IsExit = '1'"
                'intGenerateID = GetMaximumCode("tbl_ForSale", "SubString(ItemCode, 8, 3)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                retStr = Prefix & intGenerateID.ToString(GenerateFormat)

            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.MortgageInvoiceCode.ToString Then
                cristr = " Where Substring(convert(varchar(10),ReceiveDate,105),7,10) = '" & GenerateOn & "'"
                intGenerateID = GetMaximumCode("tbl_MortgageInvoice", "SubString(MortgageInvoiceCode,5,9)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                retStr = intGenerateID.ToString(GenerateFormat)


            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.CustomerCode.ToString Then
                cristr = " Where substring(CustomerCode,1,2) = '" & Format(GenerateDate, "yy") & "'"
                intGenerateID = GetMaximumCode("tbl_Customer", "SubString(CustomerCode, 3, 8)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                Prefix = Format(GenerateDate, "yy")
                retStr = Prefix & intGenerateID.ToString(GenerateFormat)

            ElseIf KeyType = CommonInfo.EnumSetting.GenerateKeyType.SupplierCode.ToString Then
                cristr = " Where substring(SupplierCode,1,2) = '" & Format(GenerateDate, "yy") & "'"
                intGenerateID = GetMaximumCode("tbl_Supplier", "SubString(SupplierCode, 3, 4)", cristr)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                Else
                    intGenerateID = 1
                End If
                Prefix = Format(GenerateDate, "yy")
                retStr = Prefix & intGenerateID.ToString(GenerateFormat)

            Else
                If GenerateFormat.IndexOf("#") < 0 And GenerateFormat.IndexOf("0") < 0 Then   ''Generate format does not include place for generated ID.
                    Return GenerateFormat
                End If
                If GenerateFormat = "0" Or GenerateFormat = "#" Then   ''To overcome the converting error of single 0 or # char to dateformat
                    strFormat = Prefix & GenerateFormat & Postfix
                ElseIf GenerateFormat = "yyyy-0000000" Then
                    strFormat = Prefix & GenerateDate.ToString(GenerateFormat) & Postfix
                Else
                    strFormat = Prefix & GenerateDate.ToString(GenerateFormat) & Postfix
                End If
                strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType AND GenerateOn=@GenerateOn "
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, KeyType)
                DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                intGenerateID = DB.ExecuteScalar(DBComm)
                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                    strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType and GenerateOn=@GenerateOn"
                Else
                    intGenerateID = 1
                    strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1, @GenerateOn)"
                End If

                retStr = intGenerateID.ToString(GenerateFormat)

                If GenerateFormat = "0" Or GenerateFormat = "#" Then   ''To overcome the converting error of single 0 or # char to dateformat
                    retStr = Prefix & retStr & Postfix
                Else
                    retStr = Prefix & GenerateDate.ToString(retStr) & Postfix
                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, KeyType)
                DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                DB.ExecuteNonQuery(DBComm)
            End If
            Return retStr
        End Function

        Public Function CheckRecordsExistOrNot(table_1 As String, table_2 As String, IDName As String, argID As String) As DataTable Implements IGeneralDA.CheckRecordsExistOrNot
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                If table_2 = "" Then
                    strCommandText = " Select SUM(M.Res) As Res from ("
                    strCommandText += " Select Count(" & IDName & ") As Res from  " & table_1
                    strCommandText += " Where " & IDName & "='" & argID & "' "
                    strCommandText += " ) As M"
                Else
                    strCommandText = " Select SUM(M.Res) As Res from ("
                    strCommandText += " Select Count(" & IDName & ") As Res from  " & table_1
                    strCommandText += " Where " & IDName & "='" & argID & "' "
                    strCommandText += " Union all "
                    strCommandText += " Select Count(" & IDName & ") As Res from  " & table_2
                    strCommandText += " Where " & IDName & "='" & argID & "' "
                    strCommandText += " ) As M"

                End If

                DBComm = DB.GetSqlStringCommand(strCommandText)

                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function

        Public Function GetMaxID(tbName As String, FName As String, Optional CriStr As String = "") As Integer Implements IGeneralDA.GetMaxID
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim RetObj As Object
            Try
                strCommandText = "SELECT Max(" & FName & ") As MaxID FROM " & tbName & " " & IIf(CriStr = vbNullString, "", " WHERE " & CriStr)
                DBComm = DB.GetSqlStringCommand(strCommandText)
                RetObj = DB.ExecuteScalar(DBComm)
                If IsDBNull(RetObj) Then
                    GetMaxID = 1
                Else
                    GetMaxID = CInt(RetObj) + 1
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "School Management System")
            End Try

        End Function

        'Public Function GetGenerateKeyForFormat(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "") As String Implements IGeneralDA.GetGenerateKeyForFormat
        '    Dim retStr As String
        '    Dim strFormat As String = ""
        '    Dim intGenerateID As Integer
        '    Dim strCommandText As String
        '    Dim DBComm As DbCommand

        '    If objgenerate.GenerateFormat = Nothing Then
        '        MsgBox("That related keyword not define in database", MsgBoxStyle.Information, "Data Access Error")
        '    Else

        '        strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
        '    End If
        '    Try
        '        If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
        '            If GenerateOn = "" Then
        '                strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType"
        '            Else
        '                strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType And GenerateOn = @GenerateOn"
        '            End If

        '        Else
        '            strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType"
        '        End If


        '        DBComm = DB.GetSqlStringCommand(strCommandText)
        '        DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
        '        DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
        '        DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
        '        intGenerateID = DB.ExecuteScalar(DBComm)

        '    Catch ex As Exception

        '        Throw ex
        '    End Try
        '    If intGenerateID > 0 Then
        '        intGenerateID = intGenerateID + 1

        '    Else
        '        intGenerateID = 1
        '        'If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
        '        '    If GenerateOn = "" Then
        '        '        strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
        '        '    Else
        '        '        strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,@GenerateOn)"
        '        '    End If
        '        'Else
        '        '    strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
        '        'End If


        '    End If
        '    retStr = intGenerateID.ToString(objgenerate.NumberCount)
        '    '' ''DBComm = DB.GetSqlStringCommand(strCommandText)
        '    '' ''DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
        '    '' ''DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
        '    '' ''DB.ExecuteNonQuery(DBComm)
        '    retStr = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
        '    Return retStr
        'End Function


        Public Function GetGenerateKeyForFormat(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String Implements IGeneralDA.GetGenerateKeyForFormat
            Dim retStr As String
            Dim strFormat As String = ""
            Dim strCode As String = ""
            Dim intGenerateID As Integer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim cristr As String = ""
           
            If objgenerate.GenerateFormat = Nothing Then
                MsgBox("That related keyword not define in database", MsgBoxStyle.Information, "Data Access Error")
            ElseIf objgenerate.GenerateFormat = "PurchaseStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim

            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "Barcode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "DiamondBarcode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "MortgageInvoiceCode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "MortgageReceive" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SaleLooseDiamond" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            Else : objgenerate.GenerateFormat = "MortgageItemCode"
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            End If

            If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
                If objgenerate.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                    strCode = GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim

                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)

                ElseIf objgenerate.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                    strCode = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim & GenerateOn.ToString
                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,len('" & strFormat & "')-len('" & objgenerate.NumberCount.Trim & "')) = '" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND SubString(ItemCode,len('" & strFormat & "')+1,len(ItemCode)-len('" & strFormat & "')) ='" & GenerateOn.ToString.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where SubString(ItemCode,len('" & strFormat & "')+1,len(ItemCode)-len('" & strFormat & "')) ='" & GenerateOn.ToString.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & GenerateOn.ToString.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)

                ElseIf objgenerate.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                    strCode = GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If

                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                End If

            Else
                strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType"
                DBComm = DB.GetSqlStringCommand(strCommandText)
                DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
                DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                intGenerateID = DB.ExecuteScalar(DBComm)
            End If

            If intGenerateID > 0 Then
                intGenerateID = intGenerateID + 1
                'If Len(intGenerateID.ToString) > Len(objgenerate.NumberCount) Then
                '    intGenerateID = 1
                'End If
            Else
                intGenerateID = 1
            End If
            retStr = intGenerateID.ToString(objgenerate.NumberCount)

            If objgenerate.GenerateFormat = "PurchaseStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr

            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "MortgageInvoiceCode" Then
                retStr = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "MortgageReceive" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleLooseDiamond" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            Else
                retStr = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            End If
            Return retStr
        End Function

        

        Public Function GenerateKeyForFormat(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String Implements IGeneralDA.GenerateKeyForFormat
            Dim retStr As String
            Dim strFormat As String = ""
            Dim strCode As String = ""
            Dim intGenerateID As Integer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim cristr As String = ""
            If objgenerate.GenerateFormat = Nothing Then
                MsgBox("That related keyword not define in database", MsgBoxStyle.Information, "Data Access Error")
            ElseIf objgenerate.GenerateFormat = "PurchaseStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim

            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "Barcode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "DiamondBarcode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "MortgageInvoiceCode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "MortgageReceive" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SaleLooseDiamond" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            Else : objgenerate.GenerateFormat = "MortgageItemCode"
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            End If

               If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
                If objgenerate.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                    strCode = GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                        intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                    Else
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                        intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                    End If

                ElseIf objgenerate.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                    strCode = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim & GenerateOn.ToString

                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,len('" & strFormat & "')-len('" & objgenerate.NumberCount.Trim & "')) = '" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND SubString(ItemCode,len('" & strFormat & "')+1,len(ItemCode)-len('" & strFormat & "')) ='" & GenerateOn.ToString.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where SubString(ItemCode,len('" & strFormat & "')+1,len(ItemCode)-len('" & strFormat & "')) ='" & GenerateOn.ToString.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & GenerateOn.ToString.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)

                ElseIf objgenerate.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                    strCode = GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                End If

                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                    'If Len(intGenerateID.ToString) > Len(objgenerate.NumberCount) Then
                    '    intGenerateID = 1
                    'End If
                Else
                    intGenerateID = 1
                End If

                Else
                    strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType"
                    DBComm = DB.GetSqlStringCommand(strCommandText)

                    DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                    DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
                    DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)

                    intGenerateID = DB.ExecuteScalar(DBComm)
                If intGenerateID > 0 Then

                    intGenerateID = intGenerateID + 1

                    If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
                        If GenerateOn = "" Then
                            strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat "
                        Else
                            strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat and GenerateOn  = @GenerateOn  "
                        End If
                    Else
                        strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat "
                    End If


                    'strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat "
                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
                    DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                    DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                    DB.ExecuteNonQuery(DBComm)
                    '******************** Test code
                Else
                    intGenerateID = 1

                    If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
                        If GenerateOn = "" Then
                            strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
                        Else
                            strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,@GenerateOn)"
                        End If
                    Else
                        strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
                    End If

                    'strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
                    DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                    DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                    DB.ExecuteNonQuery(DBComm)
                End If
                End If
            retStr = intGenerateID.ToString(objgenerate.NumberCount)

            If objgenerate.GenerateFormat = "PurchaseStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "MortgageInvoiceCode" Then
                retStr = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "MortgageReceive" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleLooseDiamond" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            Else
                retStr = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            End If
            Return retStr
        End Function

        Public Function CheckExitVoucherForCashReceipt(ByVal tbl_Name As String, ByVal Cristr As String) As DataTable Implements IGeneralDA.CheckExitVoucherForCashReceipt
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim dtResult As DataTable

            Try
                strCommandText = " Select *  from " & tbl_Name & " WHERE 1=1" & Cristr
                DBComm = DB.GetSqlStringCommand(strCommandText)
                dtResult = DB.ExecuteDataSet(DBComm).Tables(0)
                Return dtResult

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "GoldSmith Management System")
                Return New DataTable
            End Try
        End Function
        Public Function UpdateGenerateKeyForFormat(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal MaxID As Integer = 0) As Boolean Implements IGeneralDA.UpdateGenerateKeyForFormat
            Dim strFormat As String = ""
            Dim strCode As String = ""
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim cristr As String = ""

            If objgenerate.GenerateFormat = Nothing Then
                MsgBox("That related keyword not define in database", MsgBoxStyle.Information, "Data Access Error")
            ElseIf objgenerate.GenerateFormat = "PurchaseStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim

            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "Barcode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            Else : objgenerate.GenerateFormat = "MortgageItemCode"
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            End If

            If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.MortgageItemCode.ToString() Then
                If GenerateOn = "" Then
                    strCommandText = "UPDATE tbl_key_generate SET generateid = @MaxID WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat "
                Else
                    strCommandText = "UPDATE tbl_key_generate SET generateid = @MaxID WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat  "
                End If
            Else
                strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat "
            End If


            'strCommandText = "UPDATE tbl_key_generate SET generateid = generateid + 1 WHERE GenerateKeyType = @GenerateKeyType and generateformat = @generateformat "
            DBComm = DB.GetSqlStringCommand(strCommandText)
            DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
            DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
            DB.AddInParameter(DBComm, "@MaxID", DbType.String, MaxID)
            DB.ExecuteNonQuery(DBComm)
            '******************** Test code

            Return True

        End Function

        Public Function GenerateKeyForMortgageCode(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String Implements IGeneralDA.GenerateKeyForMortgageCode
            Dim retStr As String
            Dim strFormat As String = ""
            Dim strCode As String = ""
            Dim intGenerateID As Integer
            Dim strCommandText As String
            Dim DBComm As DbCommand
            Dim cristr As String = ""
            If objgenerate.GenerateFormat = Nothing Then
                MsgBox("That related keyword not define in database", MsgBoxStyle.Information, "Data Access Error")
            ElseIf objgenerate.GenerateFormat = "PurchaseStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim

            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                strFormat = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            ElseIf objgenerate.GenerateFormat = "Barcode" Then
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            Else : objgenerate.GenerateFormat = "MortgageItemCode"
                strFormat = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
            End If

            If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.Barcode.ToString() Then
                If objgenerate.PrefixPlace = EnumSetting.PrefixPlace.First.ToString() Then
                    strCode = GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                        intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                    Else
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                        intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                    End If

                ElseIf objgenerate.PrefixPlace = EnumSetting.PrefixPlace.Last.ToString() Then
                    strCode = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim & GenerateOn.ToString

                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,len('" & strFormat & "')-len('" & objgenerate.NumberCount.Trim & "')) = '" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND SubString(ItemCode,len('" & strFormat & "')+1,len(ItemCode)-len('" & strFormat & "')) ='" & GenerateOn.ToString.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where SubString(ItemCode,len('" & strFormat & "')+1,len(ItemCode)-len('" & strFormat & "')) ='" & GenerateOn.ToString.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & GenerateOn.ToString.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)

                ElseIf objgenerate.PrefixPlace = EnumSetting.PrefixPlace.NotPrefix.ToString() Then
                    strCode = GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & objgenerate.NumberCount.Trim
                    If objgenerate.IsGenerate = False Then
                        cristr = " Where substring(ItemCode,1,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))) = '" & GenerateOn.ToString & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & "' AND Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    Else
                        cristr = " Where Len(ItemCode)=Len('" & strCode & "') AND ForSaleID<>'" & ForSaleID & "'"
                    End If
                    intGenerateID = GetMaximumCode("tbl_ForSale", "Substring(ItemCode,(len(ItemCode)-len('" & objgenerate.NumberCount.Trim & "'))+1,len('" & objgenerate.NumberCount.Trim & "'))", cristr)
                End If

                If intGenerateID > 0 Then
                    intGenerateID = intGenerateID + 1
                    'If Len(intGenerateID.ToString) > Len(objgenerate.NumberCount) Then
                    '    intGenerateID = 1
                    'End If
                Else
                    intGenerateID = 1
                End If

            Else
                strCommandText = "SELECT generateid FROM tbl_key_generate WHERE generateformat = @generateformat AND GenerateKeyType = @GenerateKeyType"
                DBComm = DB.GetSqlStringCommand(strCommandText)

                DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
                DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)

                intGenerateID = DB.ExecuteScalar(DBComm)
                If intGenerateID <= 0 Then
                    intGenerateID = 1

                    If objgenerate.GenerateFormat = CommonInfo.EnumSetting.TableType.MortgageItemCode.ToString() Then
                        If GenerateOn = "" Then
                            strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
                        Else
                            strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,@GenerateOn)"
                        End If
                    Else
                        strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
                    End If

                    'strCommandText = "INSERT INTO tbl_key_generate VALUES (@GenerateKeyType, @generateformat, 1,'_')"
                    DBComm = DB.GetSqlStringCommand(strCommandText)
                    DB.AddInParameter(DBComm, "@GenerateKeyType", DbType.String, objgenerate.GenerateFormat)
                    DB.AddInParameter(DBComm, "@generateformat", DbType.String, strFormat)
                    DB.AddInParameter(DBComm, "@GenerateOn", DbType.String, GenerateOn)
                    DB.ExecuteNonQuery(DBComm)
                End If
            End If
            retStr = intGenerateID.ToString(objgenerate.NumberCount)

            If objgenerate.GenerateFormat = "PurchaseStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "WholeSaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "ConsignmentSaleStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "WholeSaleReturnStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SalesGem" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "ReturnAdvance" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "SaleVolumeStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "OrderStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            ElseIf objgenerate.GenerateFormat = "RepairStock" Then
                retStr = Global_CurrentLocationID & "-" & objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            Else
                retStr = objgenerate.Prefix.Trim & IIf(objgenerate.FormatDate1.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate1.Trim), "") & IIf(objgenerate.FormatDate2.Trim <> "", Format(IIf(FromDate <> #12:00:00 AM#, FromDate, System.DateTime.Now.Date), objgenerate.FormatDate2.Trim), "") & objgenerate.Prefix2.Trim & retStr
            End If
            Return retStr
        End Function

    End Class

    
End Namespace