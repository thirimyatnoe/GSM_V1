Imports DataAccess.Dashboard
Imports CommonInfo
Namespace Dashboard
    Public Class DashboardController
        Implements IDashboardController

#Region "Private Members"

        Private _objDashboardDA As IDashboardDA
        Private Shared ReadOnly _instance As IDashboardController = New DashboardController

#End Region

#Region "Constructors"

        Private Sub New()
            _objDashboardDA = DataAccess.Factory.Instance.CreateDashboardDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IDashboardController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function GetAllSaleByDate(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Type As String) As System.Data.DataTable Implements IDashboardController.GetAllSaleByDate
            Return _objDashboardDA.GetAllSaleByDate(FromDate, ToDate, Type)
        End Function
        'Public Function GetAllStockBalance(Optional CustomerName As String = "") As System.Data.DataTable Implements IDashboardController.GetAllCustomerForSearch
        '    Return _objCustomerDA.GetAllCustomerForSearch(CustomerName)
        'End Function
        'Public Function GetAllSaleAndCredit(Optional CustomerName As String = "") As System.Data.DataTable Implements IDashboardController.GetAllCustomerForSearch
        '    Return _objCustomerDA.GetAllCustomerForSearch(CustomerName)
        'End Function
        Public Function GetAllCashAndCredit(Optional SaleType As String = "", Optional ByVal Cristr As String = "", Optional ByVal DateType As String = "") As System.Data.DataTable Implements IDashboardController.GetAllCashAndCredit
            Return _objDashboardDA.GetAllCashAndCredit(SaleType, Cristr, DateType)
        End Function
        Public Function GetAllCredit() As System.Data.DataTable Implements IDashboardController.GetAllCredit
            Return _objDashboardDA.GetAllCredit()
        End Function
        'Public Function GetAllStockBalance() As System.Data.DataTable Implements IDashboardController.GetAllStockBalance
        '    Return _objDashboardDA.GetAllStockBalance()
        'End Function
        Public Function GetAllStockBalance(Optional ByVal SortingType As String = "", Optional ByVal BalanceType As String = "") As System.Data.DataTable Implements IDashboardController.GetAllStockBalance
            Return _objDashboardDA.GetAllStockBalance(SortingType, BalanceType)
        End Function

        Public Function GetAllSaleByCategory(ByVal FromDate As Date, ByVal ToDate As Date, ByVal Type As String, Optional ByVal ItemType As String = "") As System.Data.DataTable Implements IDashboardController.GetAllSaleByCategory
            Return _objDashboardDA.GetAllSaleByCategory(FromDate, ToDate, Type, ItemType)
        End Function
        Public Function GetAllSaleByLocation(Optional SaleType As String = "", Optional ByVal Cristr As String = "", Optional ByVal DateType As String = "") As System.Data.DataTable Implements IDashboardController.GetAllSaleByLocation
            Return _objDashboardDA.GetAllSaleByLocation(SaleType, Cristr, DateType)
        End Function
    End Class
End Namespace

