Imports Newtonsoft.Json.Linq
Imports System.Threading.Tasks

Namespace WebService
    Public Class MemberCard
        Public Shared Async Function CheckAccessAllow(UserName As String, Password As String) As Task(Of Boolean)

            Dim jObj As New JObject()
            jObj.Add("username", UserName)
            jObj.Add("password", Password)
            jObj.Add("grant_type", "password")
            jObj.Add("LoginType", "1")

            Dim Result As String
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostLogin(jObj)
            Return Result

        End Function
        Public Shared Async Function GetDriverList() As Task(Of DataTable) 'Return dt include finger index comma separate values
            Dim Result As String
            Dim obj As New JObject()
            'obj.Add("EnrollNumber", EnrollNumber)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApiForm("Driver", "GetAllDriverList", obj, True)
            'Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "GetPlayerInfoByEnrollNumber", obj, True)

            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function
        Public Shared Async Function GetMemberByMemberCode(MemberCode As String, CompanyReferenceNo As String, Token As String) As Task(Of DataTable)
            Dim Result As String
            Dim obj As New JObject()
            obj.Add("MemberCode", MemberCode)
            obj.Add("CompanyReferenceNo", CompanyReferenceNo)
            obj.Add("Token", Token)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApiForm("Member", "GetMemberInfo", obj, True)
            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function
        Public Shared Async Function GetTransactionIDByMemberCode(MemberCode As String, CompanyReferenceNo As String, Token As String, InvoiceID As String) As Task(Of DataTable)
            Dim Result As String
            Dim obj As New JObject()
            obj.Add("MemberCode", MemberCode)
            obj.Add("CompanyReferenceNo", CompanyReferenceNo)
            obj.Add("Token", Token)
            obj.Add("InvoiceID", InvoiceID)

            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApiForm("Transaction", "GetTransactionID", obj, True)
            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function

        Public Shared Async Function GetRedeemIDByMemberCode(MemberCode As String, CompanyReferenceNo As String, Token As String, InvoiceID As String) As Task(Of DataTable)
            Dim Result As String
            Dim obj As New JObject()
            obj.Add("MemberCode", MemberCode)
            obj.Add("CompanyReferenceNo", CompanyReferenceNo)
            obj.Add("Token", Token)
            obj.Add("InvoiceID", InvoiceID)

            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApiForm("Redeem", "GetRedeemID", obj, True)
            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function


        Public Shared Async Function GetRedeemInfoByMemberCode(MemberCode As String, CompanyReferenceNo As String, Token As String) As Task(Of DataTable)
            Dim Result As String
            Dim obj As New JObject()
            obj.Add("MemberCode", MemberCode)
            obj.Add("CompanyReferenceNo", CompanyReferenceNo)
            obj.Add("Token", Token)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApiForm("Redeem", "GetRedeemInfo", obj, True)
            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function
        'Public Shared Async Function SaveSaleMemberCard(ByVal InvoiceID As String, ByVal InvoiceDate As String, ByVal TopupAmount As Integer, ByVal MemberCode As String, ByVal TransactionType As Integer, ByVal BranchCode As String, ByVal CompanyReferenceNo As String, ByVal Token As String) As Task(Of Boolean)
        '    Dim Result As String

        '    Dim obj As New JObject()
        '    obj.Add("InvoiceID", InvoiceID)
        '    obj.Add("InvoiceDate", InvoiceDate)
        '    obj.Add("TopupAmount", TopupAmount)
        '    obj.Add("MemberCode", MemberCode)
        '    obj.Add("TransactionType", TransactionType)
        '    obj.Add("BranchCode", BranchCode)
        '    obj.Add("CompanyReferenceNo", CompanyReferenceNo)
        '    obj.Add("Token", Token)


        '    Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Transaction", "AddTransaction", obj, True)

        '    Return Result
        'End Function
        Public Shared Async Function SaveSaleMemberCard(ByVal objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo, ByVal Status As Integer, ByVal Global_CompanyName As String) As Task(Of Boolean)
            Dim Result As String
            Dim RegName As String = Global_CompanyName
            Dim obj As New JObject()
            obj.Add("InvoiceID", objSaleInvoice.SaleInvoiceHeaderID)
            obj.Add("InvoiceDate", objSaleInvoice.SaleDate)
            obj.Add("TopupAmount", objSaleInvoice.PaidAmount)
            obj.Add("TopupPoint", objSaleInvoice.TopupPoint)
            obj.Add("MemberCode", objSaleInvoice.MemberCode)
            obj.Add("TransactionType", 0)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("CompanyReferenceNo", Global_CompanyName)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)


            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Transaction", "AddTransaction", obj, True)

            Return Result
        End Function
        Public Shared Async Function SaveSaleMemberCardForWholeSale(ByVal objWholeSaleInvoice As CommonInfo.WholeSaleInvoiceInfo, ByVal Status As Integer, ByVal Global_CompanyName As String) As Task(Of Boolean)
            Dim Result As String
            Dim RegName As String = Global_CompanyName
            Dim obj As New JObject()
            obj.Add("InvoiceID", objWholeSaleInvoice.WholesaleInvoiceID)
            obj.Add("InvoiceDate", objWholeSaleInvoice.WDate)
            obj.Add("TopupAmount", objWholeSaleInvoice.PaidAmount)
            obj.Add("TopupPoint", objWholeSaleInvoice.TopupPoint)
            obj.Add("MemberCode", objWholeSaleInvoice.MemberCode)
            obj.Add("TransactionType", 0)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("CompanyReferenceNo", Global_CompanyName)
            obj.Add("Token", objWholeSaleInvoice.Token)
            obj.Add("Status", Status)


            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Transaction", "AddTransaction", obj, True)

            Return Result
        End Function
        Public Shared Async Function SaveSaleMemberCardForConsignment(ByVal objSaleInvoice As CommonInfo.ConsignmentSaleInfo, ByVal Status As Integer, ByVal Global_CompanyName As String) As Task(Of Boolean)
            Dim Result As String
            Dim RegName As String = Global_CompanyName
            Dim obj As New JObject()
            obj.Add("InvoiceID", objSaleInvoice.ConsignmentSaleID)
            obj.Add("InvoiceDate", objSaleInvoice.ConsignDate)
            obj.Add("TopupAmount", objSaleInvoice.PaidAmount)
            obj.Add("TopupPoint", objSaleInvoice.TopupPoint)
            obj.Add("MemberCode", objSaleInvoice.MemberCode)
            obj.Add("TransactionType", 0)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("CompanyReferenceNo", Global_CompanyName)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)


            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Transaction", "AddTransaction", obj, True)

            Return Result
        End Function
        Public Shared Async Function SaveSaleMemberCardForVolume(ByVal objSaleInvoice As CommonInfo.SalesVolumeHeaderInfo, ByVal Status As Integer, ByVal Global_CompanyName As String) As Task(Of Boolean)
            Dim Result As String
            Dim RegName As String = Global_CompanyName
            Dim obj As New JObject()
            obj.Add("InvoiceID", objSaleInvoice.SalesVolumeID)
            obj.Add("InvoiceDate", objSaleInvoice.SaleDate)
            obj.Add("TopupAmount", objSaleInvoice.PaidAmount)
            obj.Add("TopupPoint", objSaleInvoice.TopupPoint)
            obj.Add("MemberCode", objSaleInvoice.MemberCode)
            obj.Add("TransactionType", 0)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("CompanyReferenceNo", Global_CompanyName)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)


            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Transaction", "AddTransaction", obj, True)

            Return Result
        End Function
        Public Shared Async Function SaveSaleMemberCardForDiamond(ByVal objSaleInvoice As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal Status As Integer, ByVal Global_CompanyName As String) As Task(Of Boolean)
            Dim Result As String
            Dim RegName As String = Global_CompanyName
            Dim obj As New JObject()
            obj.Add("InvoiceID", objSaleInvoice.SaleLooseDiamondID)
            obj.Add("InvoiceDate", objSaleInvoice.SaleDate)
            obj.Add("TopupAmount", objSaleInvoice.PaidAmount)
            obj.Add("TopupPoint", objSaleInvoice.TopupPoint)
            obj.Add("MemberCode", objSaleInvoice.MemberCode)
            obj.Add("TransactionType", 0)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("CompanyReferenceNo", Global_CompanyName)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)


            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Transaction", "AddTransaction", obj, True)

            Return Result
        End Function

        Public Shared Async Function UpdateRedeemForWholeSale(ByVal objWholeSaleInvoice As CommonInfo.WholeSaleInvoiceInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objWholeSaleInvoice.MemberCode)
            obj.Add("RedeemID", objWholeSaleInvoice.RedeemID)
            obj.Add("InvoiceID", objWholeSaleInvoice.WholesaleInvoiceID)
            obj.Add("InvoiceDate", objWholeSaleInvoice.WDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objWholeSaleInvoice.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "UpdateRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function UpdateRedeemForConsignmentSale(ByVal objSaleInvoice As CommonInfo.ConsignmentSaleInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleInvoice.MemberCode)
            obj.Add("RedeemID", objSaleInvoice.RedeemID)
            obj.Add("InvoiceID", objSaleInvoice.ConsignmentSaleID)
            obj.Add("InvoiceDate", objSaleInvoice.ConsignDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "UpdateRedeem", obj, True)

            Return Result
        End Function

        Public Shared Async Function UpdateRedeem(ByVal objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleInvoice.MemberCode)
            obj.Add("RedeemID", objSaleInvoice.RedeemID)
            'obj.Add("RedeemPoint", objSaleInvoice.RedeemPoint)
            'obj.Add("RedeemValue", objSaleInvoice.RedeemValue)
            obj.Add("InvoiceID", objSaleInvoice.SaleInvoiceHeaderID)
            obj.Add("InvoiceDate", objSaleInvoice.SaleDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "UpdateRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function AddRedeem(ByVal objSaleInvoice As CommonInfo.SaleInvoiceHeaderInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleInvoice.MemberCode)

            obj.Add("RedeemPoint", objSaleInvoice.RedeemPoint)
            obj.Add("RedeemValue", objSaleInvoice.RedeemValue)
            'obj.Add("RedeemType", False)
            obj.Add("InvoiceID", objSaleInvoice.SaleInvoiceHeaderID)
            obj.Add("InvoiceDate", objSaleInvoice.SaleDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "AddRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function UpdateRedeemForVolume(ByVal objSaleVolume As CommonInfo.SalesVolumeHeaderInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleVolume.MemberCode)
            obj.Add("RedeemID", objSaleVolume.RedeemID)
            'obj.Add("RedeemPoint", objSaleInvoice.RedeemPoint)
            'obj.Add("RedeemValue", objSaleInvoice.RedeemValue)
            obj.Add("InvoiceID", objSaleVolume.SalesVolumeID)
            obj.Add("InvoiceDate", objSaleVolume.SaleDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleVolume.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "UpdateRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function AddRedeemForVolume(ByVal objSaleVolume As CommonInfo.SalesVolumeHeaderInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleVolume.MemberCode)

            obj.Add("RedeemPoint", objSaleVolume.RedeemPoint)
            obj.Add("RedeemValue", objSaleVolume.RedeemValue)
            obj.Add("InvoiceID", objSaleVolume.SalesVolumeID)
            obj.Add("InvoiceDate", objSaleVolume.SaleDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleVolume.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "AddRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function UpdateRedeemForDiamond(ByVal objSaleDiamond As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleDiamond.MemberCode)
            obj.Add("RedeemID", objSaleDiamond.RedeemID)
            'obj.Add("RedeemPoint", objSaleInvoice.RedeemPoint)
            'obj.Add("RedeemValue", objSaleInvoice.RedeemValue)
            obj.Add("InvoiceID", objSaleDiamond.SaleLooseDiamondID)
            obj.Add("InvoiceDate", objSaleDiamond.SaleDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleDiamond.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "UpdateRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function AddRedeemForDiamond(ByVal objSaleDiamond As CommonInfo.SaleLooseDiamondHeaderInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleDiamond.MemberCode)

            obj.Add("RedeemPoint", objSaleDiamond.RedeemPoint)
            obj.Add("RedeemValue", objSaleDiamond.RedeemValue)
            obj.Add("InvoiceID", objSaleDiamond.SaleLooseDiamondID)
            obj.Add("InvoiceDate", objSaleDiamond.SaleDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleDiamond.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "AddRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function AddRedeemForWholeSale(ByVal objWholeSaleInvoice As CommonInfo.WholeSaleInvoiceInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objWholeSaleInvoice.MemberCode)

            obj.Add("RedeemPoint", objWholeSaleInvoice.RedeemPoint)
            obj.Add("RedeemValue", objWholeSaleInvoice.RedeemValue)
            ' obj.Add("RedeemType", False)
            obj.Add("InvoiceID", objWholeSaleInvoice.WholesaleInvoiceID)
            obj.Add("InvoiceDate", objWholeSaleInvoice.WDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objWholeSaleInvoice.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "AddRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function AddRedeemForConsignmentSale(ByVal objSaleInvoice As CommonInfo.ConsignmentSaleInfo, ByVal Status As Integer) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("CompanyReferenceNo", "GWT")
            obj.Add("MemberCode", objSaleInvoice.MemberCode)

            obj.Add("RedeemPoint", objSaleInvoice.RedeemPoint)
            obj.Add("RedeemValue", objSaleInvoice.RedeemValue)
            ' obj.Add("RedeemType", False)
            obj.Add("InvoiceID", objSaleInvoice.ConsignmentSaleID)
            obj.Add("InvoiceDate", objSaleInvoice.ConsignDate)
            obj.Add("BranchCode", Global_CurrentLocationID)
            obj.Add("Token", objSaleInvoice.Token)
            obj.Add("Status", Status)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Redeem", "AddRedeem", obj, True)

            Return Result
        End Function
        Public Shared Async Function CheckCardNo(DriverID As String, CardNo As String) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("driverID", DriverID)
            obj.Add("cardno", CardNo)

            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Driver", "Checkcardno", obj, True)

            Return Result
        End Function


        Public Shared Async Function PrintDriverByID(DriverID As String) As Task(Of Boolean)
            Dim Result As String

            Dim obj As New JObject()
            obj.Add("driverID", DriverID)

            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("Driver", "PrintDriver", obj, True)
            Return Result
        End Function

        Public Shared Async Function GetTemplateID(PlayerID As Integer, FingerIndex As Integer) As Task(Of Integer)
            Dim jObj As New JObject()
            Dim jReturn As New JArray()

            jObj.Add("PlayerID", PlayerID)
            jObj.Add("FingerIndex", FingerIndex)
            Dim Result As String
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "GetTemplateID", jObj, True)

            jReturn = ConvertFunction.GlobalFunction.ConvertStringToJsonArray(Result)
            If (jReturn.Count > 0) Then
                Return jReturn.Item(0).Value(Of Integer)("templateid")
            Else
                Return 0
            End If




        End Function

        Public Shared Async Function DeleteFingerTemplate(PlayerID As Integer, FingerIndex As Integer) As Task(Of Boolean)
            Dim Result As String
            Dim jObj As New JObject()
            jObj.Add("PlayerID", PlayerID)
            jObj.Add("FingerIndex", FingerIndex)

            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "DeleteFingerTemplate", jObj, True)
            Return Result

        End Function
        Public Shared Async Function GetPlayerInfoByTemplateID(TemplateID As Integer) As Task(Of DataTable) 'Return dt include finger index comma separate values
            Dim Result As String
            Dim obj As New JObject()
            obj.Add("TemplateID", TemplateID)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "GetPlayerInfoByTemplateID", obj, True)
            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function

        Public Shared Async Function GetPlayerInfoByEnrollNumber(EnrollNumber As String) As Task(Of DataTable) 'Return dt include finger index comma separate values
            Dim Result As String
            Dim obj As New JObject()
            obj.Add("EnrollNumber", EnrollNumber)
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApiForm("fingertemplate", "GetPlayerInfoByEnrollNumber", obj, True)
            'Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "GetPlayerInfoByEnrollNumber", obj, True)

            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function

        Public Shared Async Function GetAllFingerTemplates() As Task(Of DataTable)
            Dim Result As String
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "GetAllFingerTemplates", Nothing, True)
            Return ConvertFunction.GlobalFunction.ConvertJSONToDataTable(Result)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="PlayerID"></param>
        ''' <param name="FingerIndex"></param>
        ''' <param name="FingerTemplate">Base64String Encoded String</param>
        ''' <returns>Generate TemplateID</returns>
        ''' <remarks></remarks>
        Public Shared Async Function SaveFingerTemplateZK(PlayerID As Integer, FingerIndex As Integer, FingerTemplate As String) As Task(Of Integer)
            Dim jObj As New JObject()
            jObj.Add("PlayerID", PlayerID)
            jObj.Add("FingerIndex", FingerIndex)
            jObj.Add("FingerTemplate", FingerTemplate)
            Dim Result As String
            Result = Await ConvertFunction.HttpGlobalFunction.HttpPostApi("fingertemplate", "SaveFingerPrint", jObj, True)
            If IsNumeric(Result) Then
                Return CInt(Result)
            Else
                Throw New Exception(Result)
            End If
        End Function

    End Class
End Namespace
