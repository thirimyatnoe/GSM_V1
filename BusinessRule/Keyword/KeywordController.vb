Imports DataAccess.Keyword

Namespace Keyword

    Friend Class KeywordController
        Implements IKeywordController

#Region "Private Members"

        Private _objKeywordDA As IKeywordDA
        Private Shared ReadOnly _instance As IKeywordController = New KeywordController

#End Region

#Region "Constructors"

        Private Sub New()
            _objKeywordDA = DataAccess.Factory.Instance.CreateKeyWordDA
        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IKeywordController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function DeleteKeyword(ByVal KeywordHeaderID As String) As Boolean Implements IKeywordController.DeleteKeyword
            Return _objKeywordDA.DeleteKeywordHeader(KeywordHeaderID)
        End Function

        Public Function GetKeywordHeader(ByVal KeywordHeaderID As String) As CommonInfo.KeywordHeaderInfo Implements IKeywordController.GetKeywordHeader
            Return _objKeywordDA.GetKeywordHeader(KeywordHeaderID)
        End Function

        Public Function GetKeywordHeaderList() As System.Data.DataTable Implements IKeywordController.GetKeywordHeaderList
            Return _objKeywordDA.GetKeywordHeaderList()
        End Function

        Public Function GetKeywordItems(ByVal KeywordHeaderID As String) As System.Data.DataTable Implements IKeywordController.GetKeywordItems
            Return _objKeywordDA.GetKeywordItems(KeywordHeaderID)
        End Function

        Public Function GetKeywordItemsByHeaderName(ByVal KeywordName As String) As System.Data.DataTable Implements IKeywordController.GetKeywordItemsByHeaderName
            Return _objKeywordDA.GetKeywordItemsByHeaderName(KeywordName)
        End Function

        Public Function SaveKeyword(ByRef KeywordHeaderObj As CommonInfo.KeywordHeaderInfo, ByVal KeyWordsItems As System.Data.DataTable) As Boolean Implements IKeywordController.SaveKeyword
            Dim objGeneralController As General.IGeneralController
            Dim objKeywordItem As CommonInfo.KeywordItemInfo
            Dim dr As DataRow
            Dim bolRet As Boolean

            objGeneralController = Factory.Instance.CreateGeneralController

            If KeywordHeaderObj.KeywordID = 0 Then
                KeywordHeaderObj.KeywordID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.KeywordHeader, "KeywordHeaderID", Now)
                bolRet = _objKeywordDA.InsertKeywordHeader(KeywordHeaderObj)
            Else
                bolRet = _objKeywordDA.UpdateKeywordHeader(KeywordHeaderObj)
            End If

            If bolRet = True Then
                For Each dr In KeyWordsItems.Rows
                    objKeywordItem = New CommonInfo.KeywordItemInfo

                    If dr.RowState = DataRowState.Deleted Then
                        _objKeywordDA.DeleteKeywordItem(dr("ItemID", DataRowVersion.Original))

                    ElseIf dr.RowState = DataRowState.Added Then
                        objKeywordItem.ItemID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.KeywordItem, "KeywordItemID", Now)
                        objKeywordItem.KeywordID = KeywordHeaderObj.KeywordID
                        objKeywordItem.ItemName = dr("ItemName")
                        _objKeywordDA.InsertKeywordItem(objKeywordItem)

                    ElseIf dr.RowState = DataRowState.Modified Then
                        objKeywordItem.ItemID = dr("ItemID")
                        objKeywordItem.KeywordID = KeywordHeaderObj.KeywordID
                        objKeywordItem.ItemName = dr("ItemName")
                        _objKeywordDA.UpdateKeywordItem(objKeywordItem)
                    End If
                Next
            End If

            Return bolRet
        End Function

        Public Function GetKeywordItemsName() As System.Data.DataTable Implements IKeywordController.GetKeywordItemsName
            Return _objKeywordDA.GetKeywordItemsName()

        End Function

        Public Function GetKeywordHeaderIDByName(ByVal KeywordName As String) As System.Data.DataTable Implements IKeywordController.GetKeywordHeaderIDByName
            Return _objKeywordDA.GetKeywordHeaderIDByName(KeywordName)
        End Function

        Public Function GetKeywordItemByItemID(ByVal ItemID As String) As CommonInfo.KeywordItemInfo Implements IKeywordController.GetKeywordItemByItemID
            Return _objKeywordDA.GetKeywordItemByItemID(ItemID)
        End Function
    End Class
End Namespace