Public Class OrderInvoiceInfo
#Region "Private Property"
    Private _OrderInvoiceID As String
    Private _OrderDate As Date
    Private _StaffID As String
    Private _UserID As String
    Private _CustomerID As String
    Private _ItemNameID As String
    Private _Address As String
    Private _ForSaleID As String
    Private _ItemCode As String
    Private _ItemName As String
    Private _WidthLength As String
    Private _Width As String
    Private _ItemCategoryID As String
    Private _GoldQualityID As String
    Private _OrderRate As Integer

    Private _PayGoldTK As Decimal
    Private _PayGoldTG As Decimal
    Private _PayGoldK As Integer
    Private _PayGoldP As Integer
    Private _PayGoldY As Integer
    Private _PayGoldC As Decimal

    Private _EstimateGoldTK As Decimal
    Private _EstimateGoldTG As Decimal
    Private _EstimateGoldK As Integer
    Private _EstimateGoldP As Integer
    Private _EstimateGoldY As Integer
    Private _EstimateGoldC As Decimal

    Private _ReturnGoldK As Integer
    Private _ReturnGoldP As Integer
    Private _ReturnGoldY As Integer
    Private _ReturnGoldC As Decimal
    Private _ReturnGoldTK As Decimal
    Private _ReturnGoldTG As Decimal

    Private _WasteGoldTK As Decimal
    Private _WasteGoldTG As Decimal
    Private _WasteGoldK As Integer
    Private _WasteGoldP As Integer
    Private _WasteGoldY As Integer
    Private _WasteGoldC As Decimal

    Private _ReturnGemsTK As Decimal
    Private _ReturnGemsTG As Decimal
    Private _ReturnGemsK As Integer
    Private _ReturnGemsP As Integer
    Private _ReturnGemsY As Integer
    Private _ReturnGemsC As Decimal


    Private _ReturnWasteK As Integer
    Private _ReturnWasteP As Integer
    Private _ReturnWasteY As Integer
    Private _ReturnWasteC As Decimal
    Private _ReturnWasteTK As Decimal
    Private _ReturnWasteTG As Decimal

    Private _ReturnTotalTK As Decimal
    Private _ReturnTotalTG As Decimal
    Private _ReturnTotalK As Integer
    Private _ReturnTotalP As Integer
    Private _ReturnTotalY As Integer
    Private _ReturnTotalC As Decimal

    Private _AdditionalGoldTK As Decimal
    Private _AdditionalGoldTG As Decimal
    Private _AdditionalGoldK As Integer
    Private _AdditionalGoldP As Integer
    Private _AdditionalGoldY As Integer
    Private _AdditionalGoldC As Decimal

    Private _GemsTK As Decimal
    Private _GemsTG As Decimal
    Private _GemsK As Integer
    Private _GemsP As Integer
    Private _GemsY As Integer
    Private _GemsC As Decimal

    Private _TotalTK As Decimal
    Private _TotalTG As Decimal
    Private _TotalK As Integer
    Private _TotalP As Integer
    Private _TotalY As Integer
    Private _TotalC As Decimal

    Private _GoldPrice As Integer
    Private _GemsPrice As Integer
    Private _DesignCharges As Integer
    Private _AllTotalAmount As Long
    Private _AllAddOrSub As Integer
    Private _AdvanceAmount As Integer
    Private _PaidAmount As Integer
    Private _DiscountAmount As Integer
    Private _Remark As String
    Private _IsRetrieved As Boolean
    Private _OrderRetrieveDate As Date
    Private _CounterID As String
    Private _LocationID As String
    Private _OrderBarcodeNo As String
    Private _Photo As String
    Private _PayGoldQualityID As String
    Private _SecondAdvanceDate As Date
    Private _SecondAdvanceAmount As Integer
    Private _RRemark As String
    Private _RTotalAmount As Integer
    Private _RAddOrSub As Integer
    Private _FromGoldAmount As Integer
    Private _IsCancel As Boolean
    Private _QTY As Integer
    Private _LossQTY As Integer
    Private _NetAmount As Integer
    Private _RStaffID As String
    Private _DueDate As Date
    Private _IsAddGold As Boolean
    Private _TotalAmount As Integer
    Private _AddOrSub As Integer
    Private _CustomerName As String
#End Region

#Region "Properties "
    Public Property OrderInvoiceID() As String
        Get
            OrderInvoiceID = _OrderInvoiceID
        End Get
        Set(ByVal value As String)
            _OrderInvoiceID = value
        End Set
    End Property
    Public Property OrderDate() As Date
        Get
            OrderDate = _OrderDate
        End Get
        Set(ByVal value As Date)
            _OrderDate = value
        End Set
    End Property
    Public Property StaffID() As String
        Get
            StaffID = _StaffID
        End Get
        Set(ByVal value As String)
            _StaffID = value
        End Set
    End Property
    Public Property ItemNameID() As String
        Get
            ItemNameID = _ItemNameID
        End Get
        Set(ByVal value As String)
            _ItemNameID = value
        End Set
    End Property
    Public Property UserID() As String
        Get
            UserID = _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property
    Public Property CustomerID() As String
        Get
            CustomerID = _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property
    Public Property Address() As String
        Get
            Address = _Address
        End Get
        Set(ByVal value As String)
            _Address = value
        End Set
    End Property
    Public Property ItemName() As String
        Get
            ItemName = _ItemName
        End Get
        Set(ByVal value As String)
            _ItemName = value
        End Set
    End Property
    Public Property WidthLength() As String
        Get
            WidthLength = _WidthLength
        End Get
        Set(ByVal value As String)
            _WidthLength = value
        End Set
    End Property
    Public Property Width() As String
        Get
            Width = _Width
        End Get
        Set(ByVal value As String)
            _Width = value
        End Set
    End Property
    Public Property ForSaleID() As String
        Get
            ForSaleID = _ForSaleID
        End Get
        Set(ByVal value As String)
            _ForSaleID = value
        End Set
    End Property
    Public Property ItemCode() As String
        Get
            ItemCode = _ItemCode
        End Get
        Set(ByVal value As String)
            _ItemCode = value
        End Set
    End Property
    Public Property ItemCategoryID() As String
        Get
            ItemCategoryID = _ItemCategoryID
        End Get
        Set(ByVal value As String)
            _ItemCategoryID = value
        End Set
    End Property
    Public Property GoldQualityID() As String
        Get
            GoldQualityID = _GoldQualityID
        End Get
        Set(ByVal value As String)
            _GoldQualityID = value
        End Set
    End Property
    Public Property OrderRate() As Integer
        Get
            OrderRate = _OrderRate
        End Get
        Set(ByVal value As Integer)
            _OrderRate = value
        End Set
    End Property
    Public Property PayGoldTK() As Decimal
        Get
            PayGoldTK = _PayGoldTK
        End Get
        Set(ByVal value As Decimal)
            _PayGoldTK = value
        End Set
    End Property
    Public Property PayGoldTG() As Decimal
        Get
            PayGoldTG = _PayGoldTG
        End Get
        Set(ByVal value As Decimal)
            _PayGoldTG = value
        End Set
    End Property
    Public Property PayGoldK() As Integer
        Get
            PayGoldK = _PayGoldK
        End Get
        Set(ByVal value As Integer)
            _PayGoldK = value
        End Set
    End Property
    Public Property PayGoldP() As Integer
        Get
            PayGoldP = _PayGoldP
        End Get
        Set(ByVal value As Integer)
            _PayGoldP = value
        End Set
    End Property
    Public Property PayGoldY() As Integer
        Get
            PayGoldY = _PayGoldY
        End Get
        Set(ByVal value As Integer)
            _PayGoldY = value
        End Set
    End Property
    Public Property PayGoldC() As Decimal
        Get
            PayGoldC = _PayGoldC
        End Get
        Set(ByVal value As Decimal)
            _PayGoldC = value
        End Set
    End Property
    Public Property EstimateGoldTK() As Decimal
        Get
            EstimateGoldTK = _EstimateGoldTK
        End Get
        Set(ByVal value As Decimal)
            _EstimateGoldTK = value
        End Set
    End Property
    Public Property EstimateGoldTG() As Decimal
        Get
            EstimateGoldTG = _EstimateGoldTG
        End Get
        Set(ByVal value As Decimal)
            _EstimateGoldTG = value
        End Set
    End Property
    Public Property EstimateGoldK() As Integer
        Get
            EstimateGoldK = _EstimateGoldK
        End Get
        Set(ByVal value As Integer)
            _EstimateGoldK = value
        End Set
    End Property
    Public Property EstimateGoldP() As Integer
        Get
            EstimateGoldP = _EstimateGoldP
        End Get
        Set(ByVal value As Integer)
            _EstimateGoldP = value
        End Set
    End Property
    Public Property EstimateGoldY() As Integer
        Get
            EstimateGoldY = _EstimateGoldY
        End Get
        Set(ByVal value As Integer)
            _EstimateGoldY = value
        End Set
    End Property
    Public Property EstimateGoldC() As Decimal
        Get
            EstimateGoldC = _EstimateGoldC
        End Get
        Set(ByVal value As Decimal)
            _EstimateGoldC = value
        End Set
    End Property
    Public Property ReturnGoldK() As Integer
        Get
            ReturnGoldK = _ReturnGoldK
        End Get
        Set(ByVal value As Integer)
            _ReturnGoldK = value
        End Set
    End Property
    Public Property ReturnGoldP() As Integer
        Get
            ReturnGoldP = _ReturnGoldP
        End Get
        Set(ByVal value As Integer)
            _ReturnGoldP = value
        End Set
    End Property
    Public Property ReturnGoldY() As Integer
        Get
            ReturnGoldY = _ReturnGoldY
        End Get
        Set(ByVal value As Integer)
            _ReturnGoldY = value
        End Set
    End Property
    Public Property ReturnGoldC() As Decimal
        Get
            ReturnGoldC = _ReturnGoldC
        End Get
        Set(ByVal value As Decimal)
            _ReturnGoldC = value
        End Set
    End Property
    Public Property ReturnGoldTK() As Decimal
        Get
            ReturnGoldTK = _ReturnGoldTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnGoldTK = value
        End Set
    End Property
    Public Property ReturnGoldTG() As Decimal
        Get
            ReturnGoldTG = _ReturnGoldTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnGoldTG = value
        End Set
    End Property
    Public Property WasteGoldTK() As Decimal
        Get
            WasteGoldTK = _WasteGoldTK
        End Get
        Set(ByVal value As Decimal)
            _WasteGoldTK = value
        End Set
    End Property
    Public Property WasteGoldTG() As Decimal
        Get
            WasteGoldTG = _WasteGoldTG
        End Get
        Set(ByVal value As Decimal)
            _WasteGoldTG = value
        End Set
    End Property
    Public Property WasteGoldK() As Integer
        Get
            WasteGoldK = _WasteGoldK
        End Get
        Set(ByVal value As Integer)
            _WasteGoldK = value
        End Set
    End Property
    Public Property WasteGoldP() As Integer
        Get
            WasteGoldP = _WasteGoldP
        End Get
        Set(ByVal value As Integer)
            _WasteGoldP = value
        End Set
    End Property
    Public Property WasteGoldY() As Integer
        Get
            WasteGoldY = _WasteGoldY
        End Get
        Set(ByVal value As Integer)
            _WasteGoldY = value
        End Set
    End Property
    Public Property WasteGoldC() As Decimal
        Get
            WasteGoldC = _WasteGoldC
        End Get
        Set(ByVal value As Decimal)
            _WasteGoldC = value
        End Set
    End Property
    Public Property GemsTK() As Decimal
        Get
            GemsTK = _GemsTK
        End Get
        Set(ByVal value As Decimal)
            _GemsTK = value
        End Set
    End Property
    Public Property GemsTG() As Decimal
        Get
            GemsTG = _GemsTG
        End Get
        Set(ByVal value As Decimal)
            _GemsTG = value
        End Set
    End Property
    Public Property GemsK() As Integer
        Get
            GemsK = _GemsK
        End Get
        Set(ByVal value As Integer)
            _GemsK = value
        End Set
    End Property
    Public Property GemsP() As Integer
        Get
            GemsP = _GemsP
        End Get
        Set(ByVal value As Integer)
            _GemsP = value
        End Set
    End Property
    Public Property GemsY() As Integer
        Get
            GemsY = _GemsY
        End Get
        Set(ByVal value As Integer)
            _GemsY = value
        End Set
    End Property
    Public Property GemsC() As Decimal
        Get
            GemsC = _GemsC
        End Get
        Set(ByVal value As Decimal)
            _GemsC = value
        End Set
    End Property
    Public Property TotalTK() As Decimal
        Get
            TotalTK = _TotalTK
        End Get
        Set(ByVal value As Decimal)
            _TotalTK = value
        End Set
    End Property
    Public Property TotalTG() As Decimal
        Get
            TotalTG = _TotalTG
        End Get
        Set(ByVal value As Decimal)
            _TotalTG = value
        End Set
    End Property
    Public Property TotalK() As Integer
        Get
            TotalK = _TotalK
        End Get
        Set(ByVal value As Integer)
            _TotalK = value
        End Set
    End Property
    Public Property TotalP() As Integer
        Get
            TotalP = _TotalP
        End Get
        Set(ByVal value As Integer)
            _TotalP = value
        End Set
    End Property
    Public Property TotalY() As Integer
        Get
            TotalY = _TotalY
        End Get
        Set(ByVal value As Integer)
            _TotalY = value
        End Set
    End Property
    Public Property TotalC() As Decimal
        Get
            TotalC = _TotalC
        End Get
        Set(ByVal value As Decimal)
            _TotalC = value
        End Set
    End Property
    Public Property GoldPrice() As Integer
        Get
            GoldPrice = _GoldPrice
        End Get
        Set(ByVal value As Integer)
            _GoldPrice = value
        End Set
    End Property
    Public Property GemsPrice() As Integer
        Get
            GemsPrice = _GemsPrice
        End Get
        Set(ByVal value As Integer)
            _GemsPrice = value
        End Set
    End Property
    Public Property DesignCharges() As Integer
        Get
            DesignCharges = _DesignCharges
        End Get
        Set(ByVal value As Integer)
            _DesignCharges = value
        End Set
    End Property
    Public Property AllTotalAmount() As Long
        Get
            AllTotalAmount = _AllTotalAmount
        End Get
        Set(ByVal value As Long)
            _AllTotalAmount = value
        End Set
    End Property
    Public Property AllAddOrSub() As Integer
        Get
            AllAddOrSub = _AllAddOrSub
        End Get
        Set(ByVal value As Integer)
            _AllAddOrSub = value
        End Set
    End Property
    Public Property AdvanceAmount() As Integer
        Get
            AdvanceAmount = _AdvanceAmount
        End Get
        Set(ByVal value As Integer)
            _AdvanceAmount = value
        End Set
    End Property
    Public Property PaidAmount() As Integer
        Get
            PaidAmount = _PaidAmount
        End Get
        Set(ByVal value As Integer)
            _PaidAmount = value
        End Set
    End Property
    Public Property DiscountAmount() As Integer
        Get
            DiscountAmount = _DiscountAmount
        End Get
        Set(ByVal value As Integer)
            _DiscountAmount = value
        End Set
    End Property
    Public Property Remark() As String
        Get
            Remark = _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Public Property IsRetrieved() As Boolean
        Get
            IsRetrieved = _IsRetrieved
        End Get
        Set(ByVal value As Boolean)
            _IsRetrieved = value
        End Set
    End Property
    Public Property OrderRetrieveDate() As Date
        Get
            OrderRetrieveDate = _OrderRetrieveDate
        End Get
        Set(ByVal value As Date)
            _OrderRetrieveDate = value
        End Set
    End Property
    Public Property CounterID() As String
        Get
            CounterID = _CounterID
        End Get
        Set(ByVal value As String)
            _CounterID = value
        End Set
    End Property
    Public Property LocationID() As String
        Get
            LocationID = _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property

    Public Property OrderBarcodeNo() As String
        Get
            OrderBarcodeNo = _OrderBarcodeNo
        End Get
        Set(ByVal value As String)
            _OrderBarcodeNo = value
        End Set
    End Property

    ' Field add  08-07-2013

    Public Property ReturnGemsTK() As Decimal
        Get
            ReturnGemsTK = _ReturnGemsTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnGemsTK = value
        End Set
    End Property
    Public Property ReturnGemsTG() As Decimal
        Get
            ReturnGemsTG = _ReturnGemsTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnGemsTG = value
        End Set
    End Property
    Public Property ReturnGemsK() As Integer
        Get
            ReturnGemsK = _ReturnGemsK
        End Get
        Set(ByVal value As Integer)
            _ReturnGemsK = value
        End Set
    End Property
    Public Property ReturnGemsP() As Integer
        Get
            ReturnGemsP = _ReturnGemsP
        End Get
        Set(ByVal value As Integer)
            _ReturnGemsP = value
        End Set
    End Property
    Public Property ReturnGemsY() As Integer
        Get
            ReturnGemsY = _ReturnGemsY
        End Get
        Set(ByVal value As Integer)
            _ReturnGemsY = value
        End Set
    End Property
    Public Property ReturnGemsC() As Decimal
        Get
            ReturnGemsC = _ReturnGemsC
        End Get
        Set(ByVal value As Decimal)
            _ReturnGemsC = value
        End Set
    End Property

    Public Property ReturnTotalTK() As Decimal
        Get
            ReturnTotalTK = _ReturnTotalTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnTotalTK = value
        End Set
    End Property
    Public Property ReturnTotalTG() As Decimal
        Get
            ReturnTotalTG = _ReturnTotalTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnTotalTG = value
        End Set
    End Property
    Public Property ReturnTotalK() As Integer
        Get
            ReturnTotalK = _ReturnTotalK
        End Get
        Set(ByVal value As Integer)
            _ReturnTotalK = value
        End Set
    End Property
    Public Property ReturnTotalP() As Integer
        Get
            ReturnTotalP = _ReturnTotalP
        End Get
        Set(ByVal value As Integer)
            _ReturnTotalP = value
        End Set
    End Property
    Public Property ReturnTotalY() As Integer
        Get
            ReturnTotalY = _ReturnTotalY
        End Get
        Set(ByVal value As Integer)
            _ReturnTotalY = value
        End Set
    End Property
    Public Property ReturnTotalC() As Decimal
        Get
            ReturnTotalC = _ReturnTotalC
        End Get
        Set(ByVal value As Decimal)
            _ReturnTotalC = value
        End Set
    End Property
    '
    Public Property AdditionalGoldTK() As Decimal
        Get
            AdditionalGoldTK = _AdditionalGoldTK
        End Get
        Set(ByVal value As Decimal)
            _AdditionalGoldTK = value
        End Set
    End Property
    Public Property AdditionalGoldTG() As Decimal
        Get
            AdditionalGoldTG = _AdditionalGoldTG
        End Get
        Set(ByVal value As Decimal)
            _AdditionalGoldTG = value
        End Set
    End Property
    Public Property AdditionalGoldK() As Integer
        Get
            AdditionalGoldK = _AdditionalGoldK
        End Get
        Set(ByVal value As Integer)
            _AdditionalGoldK = value
        End Set
    End Property
    Public Property AdditionalGoldP() As Integer
        Get
            AdditionalGoldP = _AdditionalGoldP
        End Get
        Set(ByVal value As Integer)
            _AdditionalGoldP = value
        End Set
    End Property
    Public Property AdditionalGoldY() As Integer
        Get
            AdditionalGoldY = _AdditionalGoldY
        End Get
        Set(ByVal value As Integer)
            _AdditionalGoldY = value
        End Set
    End Property
    Public Property AdditionalGoldC() As Decimal
        Get
            AdditionalGoldC = _AdditionalGoldC
        End Get
        Set(ByVal value As Decimal)
            _AdditionalGoldC = value
        End Set
    End Property

    Public Property ReturnWasteK() As Integer
        Get
            ReturnWasteK = _ReturnWasteK
        End Get
        Set(ByVal value As Integer)
            _ReturnWasteK = value
        End Set
    End Property
    Public Property ReturnWasteP() As Integer
        Get
            ReturnWasteP = _ReturnWasteP
        End Get
        Set(ByVal value As Integer)
            _ReturnWasteP = value
        End Set
    End Property
    Public Property ReturnWasteY() As Integer
        Get
            ReturnWasteY = _ReturnWasteY
        End Get
        Set(ByVal value As Integer)
            _ReturnWasteY = value
        End Set
    End Property
    Public Property ReturnWasteC() As Decimal
        Get
            ReturnWasteC = _ReturnWasteC
        End Get
        Set(ByVal value As Decimal)
            _ReturnWasteC = value
        End Set
    End Property
    Public Property ReturnWasteTK() As Decimal
        Get
            ReturnWasteTK = _ReturnWasteTK
        End Get
        Set(ByVal value As Decimal)
            _ReturnWasteTK = value
        End Set
    End Property
    Public Property ReturnWasteTG() As Decimal
        Get
            ReturnWasteTG = _ReturnWasteTG
        End Get
        Set(ByVal value As Decimal)
            _ReturnWasteTG = value
        End Set
    End Property
    Public Property Photo() As String
        Get
            Photo = _Photo
        End Get
        Set(ByVal value As String)
            _Photo = value
        End Set
    End Property

    Public Property PayGoldQualityID() As String
        Get
            PayGoldQualityID = _PayGoldQualityID
        End Get
        Set(ByVal value As String)
            _PayGoldQualityID = value
        End Set
    End Property

    Public Property SecondAdvanceDate() As Date
        Get
            SecondAdvanceDate = _SecondAdvanceDate
        End Get
        Set(ByVal value As Date)
            _SecondAdvanceDate = value
        End Set
    End Property

    Public Property RRemark() As String
        Get
            RRemark = _RRemark
        End Get
        Set(ByVal value As String)
            _RRemark = value
        End Set
    End Property
    Public Property SecondAdvanceAmount() As Integer
        Get
            SecondAdvanceAmount = _SecondAdvanceAmount
        End Get
        Set(ByVal value As Integer)
            _SecondAdvanceAmount = value
        End Set
    End Property
    Public Property RTotalAmount() As Integer
        Get
            RTotalAmount = _RTotalAmount
        End Get
        Set(ByVal value As Integer)
            _RTotalAmount = value
        End Set
    End Property
    Public Property RAddOrSub() As Integer
        Get
            RAddOrSub = _RAddOrSub
        End Get
        Set(ByVal value As Integer)
            _RAddOrSub = value
        End Set
    End Property
    Public Property FromGoldAmount() As Integer
        Get
            FromGoldAmount = _FromGoldAmount
        End Get
        Set(ByVal value As Integer)
            _FromGoldAmount = value
        End Set
    End Property

    Public Property IsCancel() As Boolean
        Get
            IsCancel = _IsCancel
        End Get
        Set(ByVal value As Boolean)
            _IsCancel = value
        End Set
    End Property

    Public Property QTY() As Integer
        Get
            QTY = _QTY
        End Get
        Set(ByVal value As Integer)
            _QTY = value
        End Set
    End Property
    Public Property LossQTY() As Integer
        Get
            LossQTY = _LossQTY
        End Get
        Set(ByVal value As Integer)
            _LossQTY = value
        End Set
    End Property
    Public Property NetAmount() As Integer
        Get
            NetAmount = _NetAmount
        End Get
        Set(ByVal value As Integer)
            _NetAmount = value
        End Set
    End Property
    Public Property RStaffID() As String
        Get
            RStaffID = _RStaffID
        End Get
        Set(ByVal value As String)
            _RStaffID = value
        End Set
    End Property
    Public Property DueDate() As Date
        Get
            DueDate = _DueDate
        End Get
        Set(ByVal value As Date)
            _DueDate = value
        End Set
    End Property

    Public Property IsAddGold() As Boolean
        Get
            IsAddGold = _IsAddGold
        End Get
        Set(ByVal value As Boolean)
            _IsAddGold = value
        End Set
    End Property

    Public Property TotalAmount() As Integer
        Get
            TotalAmount = _TotalAmount
        End Get
        Set(ByVal value As Integer)
            _TotalAmount = value
        End Set
    End Property
    Public Property AddOrSub() As Integer
        Get
            AddOrSub = _AddOrSub
        End Get
        Set(ByVal value As Integer)
            _AddOrSub = value
        End Set
    End Property

    Public Property CustomerName() As String
        Get
            CustomerName = _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property
#End Region
End Class
