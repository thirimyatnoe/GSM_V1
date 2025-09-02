Imports DataAccess.Converter
Imports System.Decimal


Namespace Converter
    Public Class ConverterController
        Implements IConverterController


#Region "Private Members"
        Private _objConverterDA As IConverterDA
        Dim _objEventLogDAL As New DataAccess.EventLogs.EventLogDAL

        Dim _objGlobalSettingCon As GlobalSetting.IGlobalSettingController = Factory.Instance.CreateGlobalSettingController
        Private Shared ReadOnly _instance As IConverterController = New ConverterController
#End Region

#Region "Constructors"

        Private Sub New()
            _objConverterDA = DataAccess.Factory.Instance.CreateConverterDA
        End Sub

#End Region

#Region "Public Properties"
        Public Shared ReadOnly Property Instance() As IConverterController
            Get
                Return _instance
            End Get
        End Property

#End Region

        Public Function ConvertKPYCToGoldTK(ByRef argGoldWeight As CommonInfo.GoldWeightInfo) As Decimal Implements IConverterController.ConvertKPYCToGoldTK
            With argGoldWeight
                Return (((.WeightC + .WeightY) / Global_PToY) + .WeightP) / 16 + .WeightK
            End With
        End Function




        Public Function ConvertGoldTKToKPYC(ByRef argGoldWeight As CommonInfo.GoldWeightInfo) As CommonInfo.GoldWeightInfo Implements IConverterController.ConvertGoldTKToKPYC
            Dim tmpKyat As Decimal = argGoldWeight.GoldTK
            Dim tmpweightC As Decimal = argGoldWeight.WeightC
            Dim tmpweightY As Integer = argGoldWeight.WeightY
            Dim tmpweightP As Integer = argGoldWeight.WeightP
            Dim tmpweightK As Integer = argGoldWeight.WeightK
            Dim tmpfractionKyat As Decimal
            Dim tmpdblWeightY As Decimal
            Dim Global_DecimalFormat As Integer
            Dim numberFormat As Integer

            Dim dtGlobal As New DataTable
            dtGlobal = _objGlobalSettingCon.GetAllGlobalSetting()
            Global_DecimalFormat = dtGlobal.Rows(0).Item("DecimalFormat")

            numberFormat = Global_DecimalFormat

            tmpweightK = Truncate(tmpKyat)
            tmpfractionKyat = (tmpKyat - tmpweightK)
            tmpweightP = Truncate(tmpfractionKyat * 16)
            tmpdblWeightY = ((tmpfractionKyat * 16) - tmpweightP) * Global_PToY

            If numberFormat = 1 Then
                tmpweightY = Truncate(tmpdblWeightY)
            Else
                tmpweightY = Format(Truncate(tmpdblWeightY), "0.00")

            End If

           
            'edit weight C 31.3.12
            tmpweightC = Format(tmpdblWeightY - tmpweightY, "0.00")
            If (tmpweightC >= 0.95) Then
                tmpweightY = tmpweightY + 1
                tmpweightC = 0.0
                If tmpweightY >= Global_PToY Then
                    tmpweightY = tmpweightY Mod Global_PToY
                    tmpweightP = tmpweightP + 1
                End If
                If tmpweightP >= 16 Then
                    tmpweightP = tmpweightP Mod 16
                    tmpweightK = tmpweightK + 1
                End If
            End If
            'end edit weight C 31.3.12


            If numberFormat = 1 Then
                argGoldWeight.WeightC = Math.Round(tmpweightC, 1)
            Else
                argGoldWeight.WeightC = Format(Math.Round(tmpweightC, 2), "0.00")
            End If




            argGoldWeight.WeightY = tmpweightY
            argGoldWeight.WeightP = tmpweightP
            argGoldWeight.WeightK = tmpweightK

            
            Return argGoldWeight


        End Function

        Public Function GetMeasurement(ByVal argFromMeasurement As String, ByVal argToMeasurement As String) As Decimal Implements IConverterController.GetMeasurement
            Return _objConverterDA.GetMeasurement(argFromMeasurement, argToMeasurement)
        End Function

     
    End Class
End Namespace

