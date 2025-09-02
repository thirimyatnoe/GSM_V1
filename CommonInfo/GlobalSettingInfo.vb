Public Class GlobalSettingInfo
#Region "Private Property"
    Private _Photo As String
    Private _DatabaseSharePath As String
    Private _IsCarat As Integer
    Private _IsReuseBarcode As Boolean
    Private _AllowDis As Integer
    Private _IsCash As Boolean
    Private _IsExactPrice As Boolean
    Private _DiffPurchaseRate As Integer
    Private _DiffChangeRate As Integer
    Private _IsSpeedEntry As Boolean
    Private _DecimalFormat As Integer
    Private _IsAllowUpdate As Boolean
    Private _InterestRate As Integer
    Private _InterestPeriod As Integer
    Private _EnablePaidAmount As Boolean
    Private _IsAllowSaleReturn As Boolean
    Private _IsAllowSale As Boolean
    Private _IsAllowStock As Boolean
    Private _QRCode As String
    Private _IsUsedSettingPeriod As Boolean
    Private _AllowEditWeight As Decimal
    Private _IsOneMonthCalculation As Boolean
    Private _OverDay As Integer
    Private _IsHoToBranch As Boolean
    Private _MachineType As String
    Private _Prefix As Integer
    Private _Postfix As Integer
    Private _IsHoMaster As Boolean
    Private _SoftwareVendorSetting As Boolean
    Private _IsFixPrice As Boolean
    Private _IsUseMember As Boolean
    Private _IsMemberCustomer As Boolean
    Private _RegName As String
#End Region

#Region "Properties "
    Public Property Photo() As String
        Get
            Photo = _Photo
        End Get
        Set(ByVal value As String)
            _Photo = value
        End Set
    End Property
    Public Property DatabaseSharePath() As String
        Get
            DatabaseSharePath = _DatabaseSharePath
        End Get
        Set(ByVal value As String)
            _DatabaseSharePath = value
        End Set
    End Property
    Public Property IsCarat() As Integer
        Get
            IsCarat = _IsCarat
        End Get
        Set(ByVal value As Integer)
            _IsCarat = value
        End Set
    End Property
    Public Property IsReuseBarcode() As Boolean
        Get
            IsReuseBarcode = _IsReuseBarcode
        End Get
        Set(ByVal value As Boolean)
            _IsReuseBarcode = value
        End Set
    End Property
    Public Property AllowDis() As Integer
        Get
            AllowDis = _AllowDis
        End Get
        Set(value As Integer)
            _AllowDis = value
        End Set
    End Property
    Public Property IsCash() As Boolean
        Get
            IsCash = _IsCash
        End Get
        Set(ByVal value As Boolean)
            _IsCash = value
        End Set
    End Property
    Public Property IsExactPrice() As Boolean
        Get
            IsExactPrice = _IsExactPrice
        End Get
        Set(ByVal value As Boolean)
            _IsExactPrice = value
        End Set
    End Property
    Public Property DiffPurchaseRate() As Integer
        Get
            DiffPurchaseRate = _DiffPurchaseRate
        End Get
        Set(ByVal value As Integer)
            _DiffPurchaseRate = value
        End Set
    End Property
    Public Property DiffChangeRate() As Integer
        Get
            DiffChangeRate = _DiffChangeRate
        End Get
        Set(ByVal value As Integer)
            _DiffChangeRate = value
        End Set
    End Property
    Public Property IsSpeedEntry() As Boolean
        Get
            IsSpeedEntry = _IsSpeedEntry
        End Get
        Set(ByVal value As Boolean)
            _IsSpeedEntry = value
        End Set
    End Property
    Public Property DecimalFormat() As Integer
        Get
            DecimalFormat = _DecimalFormat
        End Get
        Set(ByVal value As Integer)
            _DecimalFormat = value
        End Set
    End Property

    Public Property IsAllowUpdate() As Integer
        Get
            IsAllowUpdate = _IsAllowUpdate
        End Get
        Set(ByVal value As Integer)
            _IsAllowUpdate = value
        End Set
    End Property

    Public Property InterestRate() As Integer
        Get
            InterestRate = _InterestRate
        End Get
        Set(ByVal value As Integer)
            _InterestRate = value
        End Set
    End Property
    Public Property InterestPeriod() As Integer
        Get
            InterestPeriod = _InterestPeriod
        End Get
        Set(ByVal value As Integer)
            _InterestPeriod = value
        End Set
    End Property
   
    Public Property EnablePaidAmount() As Boolean
        Get
            EnablePaidAmount = _EnablePaidAmount
        End Get
        Set(ByVal value As Boolean)
            _EnablePaidAmount = value
        End Set
    End Property

    Public Property IsAllowSaleReturn() As Boolean
        Get
            IsAllowSaleReturn = _IsAllowSaleReturn
        End Get
        Set(ByVal value As Boolean)
            _IsAllowSaleReturn = value
        End Set
    End Property

    Public Property IsAllowSale() As Boolean
        Get
            IsAllowSale = _IsAllowSale
        End Get
        Set(ByVal value As Boolean)
            _IsAllowSale = value
        End Set
    End Property

    Public Property IsAllowStock() As Boolean
        Get
            IsAllowStock = _IsAllowStock
        End Get
        Set(ByVal value As Boolean)
            _IsAllowStock = value
        End Set
    End Property

    Public Property QRCode() As String
        Get
            QRCode = _QRCode
        End Get
        Set(ByVal value As String)
            _QRCode = value
        End Set
    End Property

    Public Property IsUsedSettingPeriod() As Boolean
        Get
            IsUsedSettingPeriod = _IsUsedSettingPeriod
        End Get
        Set(ByVal value As Boolean)
            _IsUsedSettingPeriod = value
        End Set
    End Property
    Public Property AllowEditWeight() As Decimal
        Get
            AllowEditWeight = _AllowEditWeight
        End Get
        Set(ByVal value As Decimal)
            _AllowEditWeight = value
        End Set
    End Property
    Public Property OverDay() As Integer
        Get
            OverDay = _OverDay
        End Get
        Set(ByVal value As Integer)
            _OverDay = value
        End Set
    End Property

    Public Property IsOneMonthCalculation() As Boolean
        Get
            IsOneMonthCalculation = _IsOneMonthCalculation
        End Get
        Set(ByVal value As Boolean)
            _IsOneMonthCalculation = value
        End Set
    End Property
    Public Property IsHoToBranch() As Boolean
        Get
            IsHoToBranch = _IsHoToBranch
        End Get
        Set(ByVal value As Boolean)
            _IsHoToBranch = value
        End Set
    End Property
    Public Property MachineType() As String
        Get
            MachineType = _MachineType
        End Get
        Set(ByVal value As String)
            _MachineType = value
        End Set
    End Property
    Public Property Prefix() As Integer
        Get
            Prefix = _Prefix
        End Get
        Set(ByVal value As Integer)
            _Prefix = value
        End Set
    End Property
    Public Property Postfix() As Integer
        Get
            Postfix = _Postfix
        End Get
        Set(ByVal value As Integer)
            _Postfix = value
        End Set
    End Property
    Public Property IsHoMaster() As Boolean
        Get
            IsHoMaster = _IsHoMaster
        End Get
        Set(ByVal value As Boolean)
            _IsHoMaster = value
        End Set
    End Property
    'Software Vendor Setting
    Public Property SoftwareVendorSetting() As Boolean
        Get
            SoftwareVendorSetting = _SoftwareVendorSetting
        End Get
        Set(ByVal value As Boolean)
            _SoftwareVendorSetting = value
        End Set
    End Property
    Public Property IsFixPrice() As Boolean
        Get
            IsFixPrice = _IsFixPrice
        End Get
        Set(ByVal value As Boolean)
            _IsFixPrice = value
        End Set
    End Property
    Public Property IsUseMember() As Boolean
        Get
            IsUseMember = _IsUseMember
        End Get
        Set(ByVal value As Boolean)
            _IsUseMember = value
        End Set
    End Property
    Public Property IsMemberCustomer() As Boolean
        Get
            IsMemberCustomer = _IsMemberCustomer
        End Get
        Set(ByVal value As Boolean)
            _IsMemberCustomer = value
        End Set
    End Property
    Public Property RegName() As String
        Get
            RegName = _RegName
        End Get
        Set(ByVal value As String)
            _RegName = value
        End Set
    End Property
#End Region
End Class
