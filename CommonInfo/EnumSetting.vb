Public Class EnumSetting
    Public Enum GenerateKeyType

        ''
        'Counter                         '**                    00
        'CounterNo                         '**                    00
        'GoldSmith                       '**                    GS-yyyyMMdd-0000
        'GoldSmithCode                   '**                    0000
        'GoldSmithAdvanceItem            '**                    GSC-yyyyMMdd-0000
        'GoldSmithRepair                 '**                    R-yyyyMMdd-0000
        'GoldSmithRepairItem         '**                    RI-yyyyMMdd-0000
        'PayGemsToBOD                    '**                    JP-yyyyMMdd-0000
        'PayGemsItemToBOD                '**                    JPI-yyyyMMdd-0000
        'PayGoldToBOD                    '**                    GP-yyyyMMdd-0000
        'PayGoldGemsItemToBOD            '**                    GPI-yyyyMMdd-0000
        'RepayGemsByBOD                  '**                    JR-yyyyMMdd-0000
        'RepayGemsItemByBOD              '**                    JRI-yyyyMMdd-0000
        'RepayGoldByBOD                  '**                    GR-yyyyMMdd-0000
        'RepayGoldItemByBOD              '**                    GRI-yyyyMMdd-0000
        ''



        ''
        'GoldMelting                     '**                    M-yyyyMMdd-0000
        'GoldMeltingInItem               '**                    MI-yyyyMMdd-0000
        'GoldMeltingOutItem              '**                    MO-yyyyMMdd-0000
        'RMeltingInCode
        'FirstMeltingCode
        'SecondGoldMelting
        'SecondMeltingCode
        'ThirdGoldMelting
        'ThirdMeltingCode
        'GoldStick                       '**                    ST-yyyyMMdd-0000
        'GoldStickCode                   '**                    0000
        'GoldStickHeader
        'GoldPieces                       '**                   PI-yyyyMMdd-0000
        'GoldPiecesCode                   '**                    0000
        'GoldPiecesHeader
        'GoldSmithTransaction            '**                    T-yyyyMMdd-0000
        'GoldSmithTransactionGemsItem    '**                    TI-yyyyMMdd-0000
        ''**
        'GoldSmithTransactionReturn            '**             yyyyMMdd-0000
        'GoldSmithTransactionGemsReturn    '**                 yyyyMMdd-0000
        ''***
        ''**
        'GoldSmithTransactionAdjustment          '**             yyyyMMdd-0000
        ''***
        SalesSolidGold
        SalesSolidGoldItem
        SalesOrder
        SalesOrderGemsItem
        
        ''Add New Form for package at 12/09/2013''
        KeywordHeader                   '**                    0
        KeywordItem                     '**                    0
        UserLevel                       '**                    0
        GoldQuality                     '**                    0
        Location                        '**                    0
        PurchaseGems                    '**  with LocationID:: 0PJ-yyyyMMdd-0000
        PurchaseGemsItem                '**  with LocationID:: 0PJI-yyyyMMdd-0000
        PurchaseInvoice                 '**  with LocationID:: 0P-yyyyMMdd-0000
        PurchaseInvoiceGemsItem         '**  with LocationID:: 0PI-yyyyMMdd-0000
        SalesInvoice                    '**  with LocationID:: 0S-yyyyMMdd-0000
        SalesInvoiceGemsItem            '**  with LocationID:: 0SI-yyyyMMdd-0000

        CurrentPrice                    '**                    PN-yyyyMMdd-0000
        ItemCategory                    '**                    0000
        GemsCategory                    '**                    0000
        DiamondCategory                 '**                    0000
        Damage                          '**  with LocationID:: 0D-yyyyMMdd-0000
        DamageItem                      '**  with LocationID:: 0DI-yyyyMMdd-0000
        Staff                           '**                    yyyyMMdd-0000
        'StaffByLocation
        ItemName                        '**                    0000
        SaleGems                        '**  with LocationID:: 0SG-yyyyMMdd-0000
        SaleGemsItem                    '**  with LocationID:: 0SG-yyyyMMdd-0000
        ForSale                         '**                    I-yyyyMMdd-0000
        ForSalesGemsItem                '**                    II-yyyyMMdd-0000
        ForSaleHeader                   '**                    IH-yyyyMMdd-0000
        ItemCode                        '**                    0000
        OrderInvoice                    '**  with LocationID:: 0O-yyyyMMdd-0000
        OrderInvoiceGemsItem            '**  with LocationID:: 0OI-yyyyMMdd-0000
        OrderReturnGemsItem            '**  with LocationID:: 0OI-yyyyMMdd-0000
        OrderReceiveDetail

        WholeSale                       '**                  LocationID-yyyyMMdd-0000
        WholeSaleItem                   '**                     LocationID-yyyyMMdd-0000
        Customer                        '**                     LocationID-yyyyMMdd-0000
        CustomerCode                        '**                    0000
        Transfer                        '***             LocationID-yyyyMMdd-0000
        TransferItem                    '***             LocationID- yyyyMMdd-0000
        CashReceiptOnCredit
        TransferDiamond                 '***             LocationID- yyyyMMdd-0000
        TransferDiamondItem

        PurchaseHeader                  '** with LocationID 
        PurchaseDetail                  '** with LocationID
        PurchaseGem                     '** with LocationID
        PurchaseOutItem                 '**                 yyyyMMdd-0000
        PurchaseOutItemDetail           '**                 yyyyMMdd-0000
        ''End New Form''

        ''******** Daily Income Expense
        DailyExpense                    '** with LocationID::  0CO-yyyyMMdd-0000
        DailyExpenseItem                '** with LocationID::  0COI-yyyyMMdd-0000
        DailyIncome                     '** with LocationID::  0CI-yyyyMMdd-0000
        DailyIncomeItem                 '** with LocationID::  0CII-yyyyMMdd-0000


        ''******** Cash In Out Management s
        'Account                         '**                    A-yyyyMMdd-0000
        'AccountActivity                 '**                    AC-yyyyMMdd-0000
        'Card                            '**                    C-yyyyMMdd-0000
        'CashPayment                     '**                    CP-yyyyMMdd-0000
        'CashPaymentItem                 '**                    CPI-yyyyMMdd-0000
        CashReceipt                     '**                    CR-yyyyMMdd-0000
        CashReceiptItem                 '**                    CRI-yyyyMMdd-0000
        'GeneralJournalEntry             '**                    GJ-yyyyMMdd-0000
        'GeneralJournalEntryItem         '**                    GJI-yyyyMMdd-0000
        'JournalTransaction              '**                    JN-yyyyMMdd-0000

        ''******** Store Management
        'StationeryItems                 '**                    N-yyyyMMdd-0000
        'StationeryIssue                 '**                    NI-yyyyMMdd-0000
        'StationeryIssueItems            '**                    NII-yyyyMMdd-0000
        'StationeryReceive               '**                    NR-yyyyMMdd-0000
        'StationeryReceiveItems          '**                    NRI-yyyyMMdd-0000

        GeneralLedgerByLocation
        SaleInvoiceHeader
        SaleInvoiceDetail
        WholeSaleInvoice               '**                     yyyyMMdd-0000
        WholeSaleInvoiceItem           '**                     yyyyMMdd-0000
        WholeSaleReturn
        WholeSaleReturnItem
        ConsignmentSale
        ConsignmentSaleItem
        SaleInvoiceGemItem
        OrderInvoiceDetail
        SalesVolume
        SalesVolumeDetail
        OrderInvoiceDetailGems
        SalesGem
        PurchaseRowMaterialItem
        SalesInvoice_Volume
        'add new form fro CustomReport
        CustomReport
        CompanyProfile
        'add new reuse Barcode 
        ReuseCode
        OrderReturnHeader
        OrderReturnDetail
        Repair
        RepairDetail
        RepairReturnHeader
        RepairReturnDetail
        RepairReturnGemsItem
        'Add new name for transaction event log 10.07.2015
        BarcodeNo
        SaleStock
        WholeSaleStock
        WholeSaleReturnStock
        ConsignmentSaleStock
        SaleVolumeStock
        PurchaseStock
        OrderStock
        OrderStockReturn
        RepairStock
        RepairStockReturn
        GemsStock
        'End Add New
        Supplier
        SupplierCode
        GoldSmith
        GoldSmithCode
        PurchaseFromSupplier
        PurchaseFromSupplierItem
        CashType
        OtherCash
        Branch
        ExportData                        '**                    0
        WasteSetUp                        '**                    0000
        WasteSetUpDetail
        ReturnAdvance                      '**  with LocationID:: 0SG-yyyyMMdd-0000
        ReturnAdvanceItem
        MortgageInvoice                 '**  with LocationID:: 0MG-yyyyMMdd-0000
        MortgageInvoiceItem             '**  with LocationID:: 0MGI-yyyyMMdd-0000
        MortgageInvoiceCode
        MortgageInterest
        MortgagePayback
        MortgagePaybackItem
        TransferReturn                        '***             LocationID-yyyyMMdd-0000
        TransferReturnItem                    '***             LocationID- yyyyMMdd-0000

        CheckStock
        CheckStockItem
        ECheckStockItem
        MortgageItemCode
        MortgageReturn
        MortgageReturnItem
        MortgageReceive
        InternationalDiamond
        SaleLooseDiamond
        TransferReturnDiamond
        TransferReturnDiamondItem
    End Enum
    Public Enum ButtonEnableStage
        Save
        Update
    End Enum
    Public Enum EventState
        Information
        Warning
        [Error]
    End Enum
    Public Enum FixType
        Fix
        ByWeight
        ByQTY
    End Enum
  
#Region " Event Logs "
    Public Enum DataChangedStatus
        INSERT
        UPDATE
        DELETE
        VIEW
    End Enum
#End Region


#Region " Table Type"
    Public Enum TableType
        Barcode
        PurchaseStock
        SalesGem
        SaleStock
        SaleVolumeStock
        OrderStock
        RepairStock
        RepairReturn
        GemsBarcode
        ReturnAdvance
        WholeSaleStock
        WholeSaleReturnStock
        ConsignmentSaleStock
        MortgageItemCode
        MortgageInvoiceCode
        MortgageReceive
        DiamondBarcode
        SaleLooseDiamond
    End Enum

    Public Enum PrefixPlace
        First
        Last
        NotPrefix

    End Enum
    
#End Region
    '#Region " Cash In Out Management "
    'Public Enum CardType
    '    Personal
    '    GoldSmith
    '    Employee
    '    Supplier
    '    Customer
    'End Enum
    'Public Enum JournalTransactionType
    '    CashPayment
    '    CashReceipt
    '    GeneralJournalEntry
    '    Mortgage
    '    MortgageReturn
    '    MortgageInterest
    '    PurchaseGems
    '    PurchaseInvoice
    '    SalesGems
    '    SalesInvoice
    '    SalesOrder
    '    OrderInvoice
    '    SaleInvoiceVolume
    'End Enum
    '#End Region
#Region "Cash Receipt on Credit"
    Public Enum CardReceiptOnCredit
        PurchaseGems
        PurchaseInvoice
        SalesGems
        SalesInvoice
        WholeSalesInvoice
        OrderInvoice
        SalesInvoiceVolume
        RepairReturn
        ConsignmentSaleInvoice
        SaleLooseDiamond
    End Enum
#End Region

    Public Enum LicenseStatus
        Registered
        UnRegistered
        InvalidLicense
        ExpiredLicense
        UnlimitedDayLicense
    End Enum
    '#Region " Store Management "
    '    Public Enum StationeryTransactionType
    '        OpeningStationery
    '        Issue
    '        Receive
    '    End Enum
    '#End Region

    '#Region "BOD Divide "
    '    Public Enum BODDivideType
    '        Good
    '        Melt
    '        Repair
    '        FinalBOD
    '    End Enum
    '    Public Enum BODInType
    '        PurchaseDivide
    '        GSTransDivide
    '        GSRepairDivide
    '        MainStoreDivide
    '    End Enum

    '#End Region
    '#Region "GoodItems"
    '    Public Enum GoodItemsDivideType
    '        PurchaseDivide
    '        GoldSmithTransactionDivide
    '        GoldSmithRepairDivide
    '        BODDivide
    '    End Enum
    '#End Region
    '#Region "RepairDivide "
    '    Public Enum RepairDivideType
    '        Good
    '        BOD
    '        Melt

    '    End Enum
    '#End Region
    '#Region "MeltingDivide"
    '    Public Enum MeltingDivideType
    '        GoldStick
    '        GoldPieces
    '    End Enum
    '#End Region
    '#Region "GoldSmithTransactionDivide "
    '    Public Enum GoldSmithTransactionDivideType
    '        Good
    '        BOD
    '        Polish

    '        Items
    '        OthersEggsPieces

    '        Machine
    '        Colour
    '    End Enum
    '#End Region
    '#Region "MeltingItems"
    '    Public Enum MeltingItemsDivideType
    '        PurchaseDivide
    '        GoldSmithTransactionDivide
    '        GoldSmithRepairDivide
    '        BODDivide
    '    End Enum
    '#End Region
    '#Region "RepairItems"
    '    Public Enum RepairItemsDivideType
    '        PurchaseDivide
    '        BODDivide
    '        DamageDivide
    '    End Enum
    '#End Region
    '#Region "PolishItems"
    '    Public Enum PolishItemsDivideType
    '        GoldSmithTransactionDivide
    '        GoldSmithRepairDivide
    '    End Enum
    '#End Region
    '#Region "PolishDivide "
    '    Public Enum PolishDivideType
    '        Good
    '        Polish
    '        BOD
    '    End Enum
    '#End Region
    '#Region "RepairStoreItems"
    '    Public Enum RepairStoreItemsType
    '        PurchaseRepairItems
    '        DamageRepairItems
    '        BODRepairItems
    '    End Enum
    '#End Region
    '#Region "RepairStoreItems"
    '    
    '#End Region
    Public Enum VisualStyle
        Office2007Black
        Office2007Blue
        Office2007Silver
        Office2010Black
        Office2010Blue
        Office2010Silver

    End Enum
End Class
