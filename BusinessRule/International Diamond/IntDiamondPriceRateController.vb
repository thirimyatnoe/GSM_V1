Imports DataAccess.InternationalDiamond
Namespace InternationalDiamond
    Public Class IntDiamondPriceRateController
        Implements IIntDiamondPriceRateController

#Region "Private Members"

        Private _objIntDia As IIntDiamondPriceRateDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IIntDiamondPriceRateController = New IntDiamondPriceRateController

#End Region

#Region "Constructors"

        Private Sub New()
            _objIntDia = DataAccess.Factory.Instance.CreateIntDiamondDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IIntDiamondPriceRateController
            Get
                Return _instance
            End Get
        End Property

#End Region



        Public Function GetIntDiamondByIntDiamondID(ByVal DefineID As String) As CommonInfo.IntDiamondPriceRateInfo Implements IIntDiamondPriceRateController.GetIntDiamondByIntDiamondID
            Return _objIntDia.GetIntDiamondByIntDiamondID(DefineID)
        End Function

        Public Function GetIntDiamondData(ByVal Carat As Decimal) As CommonInfo.IntDiamondPriceRateInfo Implements IIntDiamondPriceRateController.GetIntDiamondData
            Return _objIntDia.GetIntDiamondData(Carat)
        End Function

        Public Function GetIntDiamondList(ByVal argShape As String, ByVal argCaratFrom As Double, ByVal argCaratTo As Double) As System.Data.DataTable Implements IIntDiamondPriceRateController.GetIntDiamondList
            Return _objIntDia.GetIntDiamondList(argShape, argCaratFrom, argCaratTo)
        End Function

        Public Function SaveIntDiamond(ByVal obj As CommonInfo.IntDiamondPriceRateInfo) As Boolean Implements IIntDiamondPriceRateController.SaveIntDiamond
            'Dim dr As DataRow
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            ''For i As Integer = 0 To _dtIntDiamond.Rows.Count - 1
            ''    For j As Integer = 1 To _dtIntDiamond.Columns.Count - 1
            ''        If (Not IsDBNull(_dtIntDiamond.Rows(i).Item(j))) Then
            If obj.DefineID = "0" Then
                obj.DefineID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.InternationalDiamond, CommonInfo.EnumSetting.GenerateKeyType.InternationalDiamond.ToString(), Now)
                _objIntDia.InsertIntDiamond(obj)
            Else
                _objIntDia.UpdateIntDiamond(obj)
            End If

            ''        End If
            ''    Next
            ''Next
            Return True
        End Function

        Public Function GetSaleReturnPercentByMaxDate(Optional ByVal cristr As String = "") As CommonInfo.IntDiamondPriceRateInfo Implements IIntDiamondPriceRateController.GetSaleReturnPercentByMaxDate
            Return _objIntDia.GetSaleReturnPercentByMaxDate(cristr)
        End Function

        Public Function GetIntDiamondListForView() As System.Data.DataTable Implements IIntDiamondPriceRateController.GetIntDiamondListForView
            Return _objIntDia.GetIntDiamondListForView()
        End Function
        Public Function DeleteIntDiamondPrice(ByVal DefineID As String) As Boolean Implements IIntDiamondPriceRateController.DeleteIntDiamondPrice
            If _objIntDia.DeleteIntDiamond(DefineID) = True Then
                _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                DateTime.Now, _
                Global_UserID, _
                CommonInfo.EnumSetting.GenerateKeyType.CurrentPrice.ToString, _
                CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                DefineID, _
                "Delete CurrentPrice")
                Return True
            Else
                Return False
            End If
        End Function
        Public Function GetAllDiamondPrice() As System.Data.DataTable Implements IIntDiamondPriceRateController.GetAllDiamondPrice
            Return _objIntDia.GetAllDiamondPrice()
        End Function
    End Class
End Namespace

