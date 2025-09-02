Imports DataAccess.Staff
Namespace Staff
    Public Class StaffController
        Implements IStaffController

#Region "Private Members"

        Private _objStaffDA As IStaffDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL
        Private Shared ReadOnly _instance As IStaffController = New StaffController

#End Region

#Region "Constructors"

        Private Sub New()
            _objStaffDA = DataAccess.Factory.Instance.CreateStaffDA

        End Sub

#End Region

#Region "Public Properties"

        Public Shared ReadOnly Property Instance() As IStaffController
            Get
                Return _instance
            End Get
        End Property

#End Region


        Public Function GetStaffList() As System.Data.DataTable Implements IStaffController.GetStaffList
            Return _objStaffDA.GetStaffList()
        End Function
        Public Function GetStaffListByLocation(ByVal LocationID As String) As System.Data.DataTable Implements IStaffController.GetStaffListByLocation
            Return _objStaffDA.GetStaffListByLocation(LocationID)
        End Function
        Public Function InsertStaff(ByVal _dtStaff As System.Data.DataTable) As Boolean Implements IStaffController.InsertStaff
            Dim dr As DataRow
            Dim objStaff As CommonInfo.StaffInfo
            Dim objGeneralController As General.IGeneralController
            objGeneralController = Factory.Instance.CreateGeneralController

            For Each dr In _dtStaff.Rows
                objStaff = New CommonInfo.StaffInfo
                If dr.RowState = DataRowState.Deleted Then
                 
                        _objStaffDA.DeleteStaff(dr("StaffID", DataRowVersion.Original))
                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Staff.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.DELETE.ToString, _
                                          dr("StaffID", DataRowVersion.Original), _
                                          "Delete Staff")

                ElseIf dr.RowState = DataRowState.Added Then
                    If Not IsDBNull(dr("Staff_")) Then
                        objStaff.StaffID = objGeneralController.GenerateKey(CommonInfo.EnumSetting.GenerateKeyType.Staff, "StaffID", Now)
                        objStaff.Staff = dr("Staff_")

                        _objStaffDA.InsertStaff(objStaff)
                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                          DateTime.Now, _
                                          Global_UserID, _
                                          CommonInfo.EnumSetting.GenerateKeyType.Staff.ToString, _
                                          CommonInfo.EnumSetting.DataChangedStatus.INSERT.ToString, _
                                          objStaff.StaffID, _
                                          "Insert Staff")
                    End If
                ElseIf dr.RowState = DataRowState.Modified Then
                    If Not IsDBNull(dr("Staff_")) Then
                        objStaff.StaffID = dr("StaffID")
                        objStaff.Staff = dr("Staff_")
                        _objStaffDA.UpdateStaff(objStaff)
                        _objEventLogDAL.AddLog(CommonInfo.EnumSetting.EventState.Information, _
                                           DateTime.Now, _
                                           Global_UserID, _
                                           CommonInfo.EnumSetting.GenerateKeyType.Staff.ToString, _
                                           CommonInfo.EnumSetting.DataChangedStatus.UPDATE.ToString, _
                                           objStaff.StaffID, _
                                           "Update Staff")
                    End If
                End If
            Next
            Return True
        End Function

        Public Function GetStaff(ByVal StaffID As String) As CommonInfo.StaffInfo Implements IStaffController.GetStaff
            Return _objStaffDA.GetStaff(StaffID)
        End Function

        Public Function GetrptStaff() As DataTable Implements IStaffController.GetrptStaff
            Return _objStaffDA.GetrptStaff()
        End Function
        Public Function GetStaffDataByStaff(ByVal Staff As String, Optional StaffID As String = "") As DataTable Implements IStaffController.GetStaffDataByStaff
            Return _objStaffDA.GetStaffDataByStaff(Staff, StaffID)
        End Function
    End Class
End Namespace
