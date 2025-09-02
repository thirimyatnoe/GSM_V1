Imports DataAccess.General
Imports CommonInfo

Namespace General
    Public Class GeneralController
        Implements IGeneralController

#Region "Private Members"

        Public _objGeneralDA As IGeneralDA
        Public Shared ReadOnly _instance As IGeneralController = New GeneralController

#End Region

#Region "Constructors"

        Public Sub New()
            _objGeneralDA = DataAccess.Factory.Instance.CreateGeneralDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance(Optional ByVal Connection As String = "") As IGeneralController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function GetGenerateKey(ByVal KeyType As CommonInfo.EnumSetting.GenerateKeyType, ByVal GenerateOn As String, ByVal GenerateDate As Date) As String Implements IGeneralController.GetGenerateKey
            Dim strGenerateFormat As String
            Dim strGenerateKeyType As String
            Dim strPrefix As String
            Dim strPostfix As String
            strPrefix = ""
            strPostfix = ""
            If GenerateDate = CDate("0:00:00") Then
                GenerateDate = Now
            End If
            Select Case KeyType
                Case EnumSetting.GenerateKeyType.KeywordHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.KeywordHeader.ToString
                    strGenerateFormat = "0"
                Case EnumSetting.GenerateKeyType.KeywordItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.KeywordItem.ToString
                    strGenerateFormat = "0"
                Case EnumSetting.GenerateKeyType.UserLevel
                    strGenerateKeyType = EnumSetting.GenerateKeyType.UserLevel.ToString
                    strGenerateFormat = "0"
                Case EnumSetting.GenerateKeyType.GoldQuality
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldQuality.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.Location
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Location.ToString
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.PurchaseGems
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseGems.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseInvoice.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseInvoiceGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseInvoiceGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldMelting
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMelting.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldMeltingInItem
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMeltingInItem.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldMeltingOutItem
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMeltingOutItem.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.RMeltingInCode
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.RMeltingInCode.ToString
                    ''    strGenerateFormat = "00000"
                    ''Case EnumSetting.GenerateKeyType.FirstMeltingCode
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.FirstMeltingCode.ToString
                    ''    strGenerateFormat = "00000"
                    ''Case EnumSetting.GenerateKeyType.SecondGoldMelting
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.SecondGoldMelting.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.SecondMeltingCode
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.SecondMeltingCode.ToString
                    ''    strGenerateFormat = "00000"
                    ''Case EnumSetting.GenerateKeyType.ThirdGoldMelting
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.ThirdGoldMelting.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.ThirdMeltingCode
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.ThirdMeltingCode.ToString
                    ''    strGenerateFormat = "00000"
                    ''Case EnumSetting.GenerateKeyType.GoldStickHeader
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldStickHeader.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldStick
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldStick.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldStickCode
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldStickCode.ToString
                    ''    strGenerateFormat = "0000"
                    ''Case EnumSetting.GenerateKeyType.GoldSmithTransaction
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransaction.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldSmithTransactionGemsItem
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionGemsItem.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldSmithTransactionReturn
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionReturn.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldSmithTransactionGemsReturn
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionGemsReturn.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                    ''Case EnumSetting.GenerateKeyType.GoldSmithTransactionAdjustment
                    ''    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionAdjustment.ToString
                    ''    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesInvoice.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesInvoiceGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesInvoiceGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesOrder
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesOrder.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleInvoice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleInvoiceItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleInvoiceItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleReturn.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleReturnItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleReturnItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ConsignmentSale
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ConsignmentSale.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ConsignmentSaleItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ConsignmentSaleItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesOrderGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesOrderGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CurrentPrice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CurrentPrice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ItemCategory
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ItemCategory.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.GemsCategory
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GemsCategory.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.Damage
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Damage.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DamageItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DamageItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.Staff
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Staff.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yy0000"
                Case EnumSetting.GenerateKeyType.SaleGems
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleGems.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SaleGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ReturnAdvance
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ReturnAdvance.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ReturnAdvanceItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ReturnAdvanceItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ForSale
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ForSale.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ForSaleHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ForSaleHeader.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ForSalesGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ForSalesGemsItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ItemCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ItemCode.ToString
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.OrderInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoiceDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoiceDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoiceGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderReceiveDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderReceiveDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderReturnGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderReturnGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyExpense
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyExpense.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyExpenseItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyExpenseItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyIncome
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyIncome.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyIncomeItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyIncomeItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ItemName
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ItemName.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.Customer
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Customer.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CustomerCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CustomerCode.ToString
                    strGenerateFormat = "00000000"
                Case EnumSetting.GenerateKeyType.Transfer
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Transfer.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturn.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturnItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturnItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CashReceiptOnCredit
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"

                    ''Add New Form
                Case EnumSetting.GenerateKeyType.PurchaseHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseGem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseGem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"

                Case EnumSetting.GenerateKeyType.PurchaseOutItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseOutItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseOutItemDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseOutItemDetail.ToString
                    strGenerateFormat = "yyyyMMdd-0000"


                Case EnumSetting.GenerateKeyType.SaleInvoiceDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleInvoiceDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SaleInvoiceGemItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SaleInvoiceHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleInvoiceHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesVolume
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesVolume.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesVolumeDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesVolumeDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoiceDetailGems
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoiceDetailGems.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CompanyProfile
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CompanyProfile.ToString
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.Repair
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Repair.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.RepairDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.RepairReturnHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairReturnHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.RepairReturnDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairReturnDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.GemsStock
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GemsStock.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.Supplier
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Supplier.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SupplierCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SupplierCode.ToString
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.GoldSmith
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmith.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.GoldSmithCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithCode.ToString
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.PurchaseFromSupplier
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseFromSupplier.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseFromSupplierItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseFromSupplierItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CashType
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CashType.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.OtherCash
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OtherCash.ToString
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.WasteSetUp
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WasteSetUp.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WasteSetUpDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WasteSetUpDetail.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.RepairStockReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairStockReturn.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageInvoiceCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoiceCode.ToString
                    strGenerateFormat = "00000"
                Case EnumSetting.GenerateKeyType.MortgageInterest
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInterest.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgagePayback
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgagePayback.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageReturn.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CheckStock
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CheckStock.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.InternationalDiamond
                    strGenerateKeyType = EnumSetting.GenerateKeyType.InternationalDiamond.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferDiamond
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferDiamond.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturnDiamond
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturnDiamond.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case Else
                    strGenerateKeyType = "0"
                    strGenerateFormat = "0"
            End Select
            Return _objGeneralDA.GetGenerateKey(strGenerateKeyType, strGenerateFormat, GenerateOn, GenerateDate, strPrefix, strPostfix)
        End Function
        Public Function GenerateKey(ByVal KeyType As CommonInfo.EnumSetting.GenerateKeyType, ByVal GenerateOn As String, ByVal GenerateDate As Date) As String Implements IGeneralController.GenerateKey
            Dim strGenerateFormat As String
            Dim strGenerateKeyType As String
            Dim strPrefix As String
            Dim strPostfix As String

            strPrefix = ""
            strPostfix = ""
            If GenerateDate = CDate("0:00:00") Then
                GenerateDate = Now
            End If

            Select Case KeyType
                Case EnumSetting.GenerateKeyType.KeywordHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.KeywordHeader.ToString
                    strGenerateFormat = "0"
                Case EnumSetting.GenerateKeyType.KeywordItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.KeywordItem.ToString
                    strGenerateFormat = "0"
                Case EnumSetting.GenerateKeyType.UserLevel
                    strGenerateKeyType = EnumSetting.GenerateKeyType.UserLevel.ToString
                    strGenerateFormat = "0"
                Case EnumSetting.GenerateKeyType.GoldQuality
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldQuality.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.Location
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Location.ToString
                    strGenerateFormat = "00"

                Case EnumSetting.GenerateKeyType.PurchaseGems
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseGems.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseInvoice.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseInvoiceGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseInvoiceGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldMelting
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMelting.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldMeltingInItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMeltingInItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldMeltingOutItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMeltingOutItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.RMeltingInCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.RMeltingInCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.FirstMeltingCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.FirstMeltingCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.SecondGoldMelting
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.SecondGoldMelting.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.SecondMeltingCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.SecondMeltingCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.ThirdGoldMelting
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.ThirdGoldMelting.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.ThirdMeltingCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.ThirdMeltingCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.GoldStickHeader
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldStickHeader.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldStick
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldStick.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldStickCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldStickCode.ToString
                    '    strGenerateFormat = "0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransaction
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransaction.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionGemsItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionGemsItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionReturn
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionReturn.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionGemsReturn
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionGemsReturn.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionAdjustment
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionAdjustment.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesInvoice.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesInvoiceGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesInvoiceGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesOrder
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesOrder.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesOrderGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesOrderGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CurrentPrice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CurrentPrice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ItemCategory
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ItemCategory.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.GemsCategory
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GemsCategory.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.Damage
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Damage.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DamageItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DamageItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.Staff
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Staff.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yy0000"
                Case EnumSetting.GenerateKeyType.SaleGems
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleGems.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SaleGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleGemsItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleInvoice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleInvoiceItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleInvoiceItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleReturn.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WholeSaleReturnItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleReturnItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ConsignmentSale
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ConsignmentSale.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ConsignmentSaleItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ConsignmentSaleItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ReturnAdvance
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ReturnAdvance.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ReturnAdvanceItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ReturnAdvanceItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ForSale
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ForSale.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ForSaleHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ForSaleHeader.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ForSalesGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ForSalesGemsItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ItemCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ItemCode.ToString
                    strGenerateFormat = "0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithDebt
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithDebt.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithDebtItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithDebtItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MortgageInvoice
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoice.ToString
                    '    strPrefix = Global_CurrentLocationID & "-"
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MortgageInvoiceItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoiceItem.ToString
                    '    strPrefix = Global_CurrentLocationID & "-"
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MortgageInvoiceCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoiceCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.MortgageInterest
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInterest.ToString
                    '    strPrefix = Global_CurrentLocationID & "-"
                    '    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoiceDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoiceDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoiceGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoiceGemsItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderReceiveDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderReceiveDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderReturnGemsItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderReturnGemsItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"

                Case EnumSetting.GenerateKeyType.DailyExpense
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyExpense.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyExpenseItem
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyExpenseItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyIncome
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyIncome.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.DailyIncomeItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.DailyIncomeItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.StationeryItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.StationeryItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.StationeryIssue
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.StationeryIssue.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.StationeryIssueItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.StationeryIssueItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.StationeryReceive
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.StationeryReceive.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.StationeryReceiveItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.StationeryReceiveItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.PurchaseDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.PurchaseDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.BODDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.BODDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.BODDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.BODDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoodItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoodItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoodItemCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoodItemCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.MeltingItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MeltingItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MeltingItemsCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MeltingItemsCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.RepairItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.RepairItemsCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairItemsCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.PolishItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.PolishItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.PolishItemsCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.PolishItemsCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.SPItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.SPItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.SPItemsCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.SPItemsCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithRepairDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithRepairDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithRepairDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithRepairDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldPiecesHeader
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldPiecesHeader.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldPieces
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldPieces.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldPiecesCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldPiecesCode.ToString
                    '    strGenerateFormat = "0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionGemsDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionGemsDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithTransactionGemsDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithTransactionGemsDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.DamageDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.DamageDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.DamageDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.DamageDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MainStoreItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MainStoreItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MainStoreItemsCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MainStoreItemsCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.RepairStoreItems
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairStoreItems.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.RepairStoreItemsCode
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairStoreItemsCode.ToString
                    '    strGenerateFormat = "00000"
                    'Case EnumSetting.GenerateKeyType.MainStoreDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MainStoreDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.MainStoreDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.MainStoreDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldMeltingReturnDivide
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMeltingReturnDivide.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldMeltingReturnDivideItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldMeltingReturnDivideItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.ItemName
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ItemName.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.GoldSmithWaste
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithWaste.ToString
                    '    strGenerateFormat = "0000"
                    'Case EnumSetting.GenerateKeyType.StaffByLocation
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.StaffByLocation.ToString
                    '    strGenerateFormat = "0000"
                    'Case EnumSetting.GenerateKeyType.PurchaseSupplier
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseSupplier.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.PurchaseSupplierItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseSupplierItem.ToString
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.WholeSale
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSale.ToString
                    '    strPrefix = Global_CurrentLocationID & "-"
                    '    strGenerateFormat = "yyyyMMdd-0000"
                    'Case EnumSetting.GenerateKeyType.WholeSaleItem
                    '    strGenerateKeyType = EnumSetting.GenerateKeyType.WholeSaleItem.ToString
                    '    strPrefix = Global_CurrentLocationID & "-"
                    '    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.Customer
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Customer.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CustomerCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CustomerCode.ToString
                    strGenerateFormat = "00000000"
                Case EnumSetting.GenerateKeyType.Transfer
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Transfer.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturn.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturnItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturnItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CashReceiptOnCredit
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CashReceiptOnCredit.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"

                    ''Add New Form
                Case EnumSetting.GenerateKeyType.PurchaseHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseGem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseGem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"

                Case EnumSetting.GenerateKeyType.PurchaseOutItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseOutItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseOutItemDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseOutItemDetail.ToString
                    strGenerateFormat = "yyyyMMdd-0000"

                Case EnumSetting.GenerateKeyType.SaleInvoiceDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleInvoiceDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderReturnHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderReturnHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                    ''End New Form
                Case EnumSetting.GenerateKeyType.SaleInvoiceGemItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleInvoiceGemItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SaleInvoiceHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SaleInvoiceHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesVolume
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesVolume.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SalesVolumeDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SalesVolumeDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.OrderInvoiceDetailGems
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OrderInvoiceDetailGems.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CompanyProfile
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CompanyProfile.ToString
                    strGenerateFormat = "00"
                Case EnumSetting.GenerateKeyType.ReuseCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.ReuseCode.ToString
                    strGenerateFormat = "000"
                    'strPrefix = "-"
                Case EnumSetting.GenerateKeyType.Repair
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Repair.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.RepairDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.RepairReturnHeader
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairReturnHeader.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.RepairReturnDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.RepairReturnDetail.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.GemsStock
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GemsStock.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.Supplier
                    strGenerateKeyType = EnumSetting.GenerateKeyType.Supplier.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.SupplierCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.SupplierCode.ToString
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.GoldSmith
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmith.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.GoldSmithCode
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GoldSmithCode.ToString
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.PurchaseFromSupplier
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseFromSupplier.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.PurchaseFromSupplierItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.PurchaseFromSupplierItem.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.CashType
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CashType.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "0000"
                Case EnumSetting.GenerateKeyType.OtherCash
                    strGenerateKeyType = EnumSetting.GenerateKeyType.OtherCash.ToString
                    strGenerateFormat = "yyyyMMdd-000"
                Case EnumSetting.GenerateKeyType.WasteSetUp
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WasteSetUp.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.WasteSetUpDetail
                    strGenerateKeyType = EnumSetting.GenerateKeyType.WasteSetUpDetail.ToString
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageInvoice
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoice.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageInvoiceItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInvoiceItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageInterest
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageInterest.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgagePayback
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgagePayback.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgagePaybackItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgagePaybackItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageReturn
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageReturn.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.MortgageReturnItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.MortgageReturnItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.GeneralLedgerByLocation
                    strGenerateKeyType = EnumSetting.GenerateKeyType.GeneralLedgerByLocation.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yy0000"
                Case EnumSetting.GenerateKeyType.CheckStock
                    strGenerateKeyType = EnumSetting.GenerateKeyType.CheckStock.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.InternationalDiamond
                    strGenerateKeyType = EnumSetting.GenerateKeyType.InternationalDiamond.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferDiamond
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferDiamond.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturnDiamond
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturnDiamond.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferReturnDiamondItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferReturnDiamondItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case EnumSetting.GenerateKeyType.TransferDiamondItem
                    strGenerateKeyType = EnumSetting.GenerateKeyType.TransferDiamondItem.ToString
                    strPrefix = Global_CurrentLocationID & "-"
                    strGenerateFormat = "yyyyMMdd-0000"
                Case Else
                    strGenerateKeyType = "0"
                    strGenerateFormat = "0"
            End Select
            Return _objGeneralDA.GenerateKey(strGenerateKeyType, strGenerateFormat, GenerateOn, GenerateDate, strPrefix, strPostfix)
        End Function

        Public Function CheckRecordsExistOrNot(table_1 As String, table_2 As String, IDName As String, argID As String) As DataTable Implements IGeneralController.CheckRecordsExistOrNot
            Return _objGeneralDA.CheckRecordsExistOrNot(table_1, table_2, IDName, argID)
        End Function

        Public Function GetMaxID(tbName As String, FName As String, Optional CriStr As String = "") As Integer Implements IGeneralController.GetMaxID
            Return _objGeneralDA.GetMaxID(tbName, FName, CriStr)
        End Function

        Public Function GetGenerateKeyForFormat(objGenerateFormat As GenerateFormatInfo, Optional SaleDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String Implements IGeneralController.GetGenerateKeyForFormat
            Return _objGeneralDA.GetGenerateKeyForFormat(objGenerateFormat, SaleDate, GenerateOn, ForSaleID)
        End Function
        Public Function GenerateKeyForFormat(objGenerateFormat As GenerateFormatInfo, Optional SaleDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String Implements IGeneralController.GenerateKeyForFormat
            Return _objGeneralDA.GenerateKeyForFormat(objGenerateFormat, SaleDate, GenerateOn, ForSaleID)
        End Function
        Public Function UpdateGenerateKeyForFormat(objgenerate As GenerateFormatInfo, Optional FromDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal MaxID As Integer = 0) As Boolean Implements IGeneralController.UpdateGenerateKeyForFormat
            Return _objGeneralDA.UpdateGenerateKeyForFormat(objgenerate, FromDate, GenerateOn, MaxID)
        End Function

        Public Function CheckExitVoucherForCashReceipt(table_1 As String, ByVal Cristr As String) As DataTable Implements IGeneralController.CheckExitVoucherForCashReceipt
            Return _objGeneralDA.CheckExitVoucherForCashReceipt(table_1, Cristr)
        End Function
        Public Function GenerateKeyForMortgageCode(objGenerateFormat As GenerateFormatInfo, Optional SaleDate As Date = #12:00:00 AM#, Optional ByVal GenerateOn As String = "", Optional ByVal ForSaleID As String = "") As String Implements IGeneralController.GenerateKeyForMortgageCode
            Return _objGeneralDA.GenerateKeyForMortgageCode(objGenerateFormat, SaleDate, GenerateOn, ForSaleID)
        End Function
    End Class
End Namespace

