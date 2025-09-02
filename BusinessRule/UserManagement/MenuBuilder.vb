Imports DataAccess.UserManagement
Imports CommonInfo
'Imports C1.Win.C1Ribbon

Namespace UserManagement

    Public Class MenuBuilder
        Private objMenuSetupDAL As New MenuBuliderDAL
        Private _strMenuIDList As String

        'UserLevelID = 0 For Administrator
        Public Function GetMenuUserLevel(ByVal UserLevelID As Integer, ByVal UserLevel As String) As DataTable
            Return objMenuSetupDAL.GetMenuUserLevel(UserLevelID, UserLevel)
        End Function

        Public Function UpdateMenuUserLevel(ByVal argUserLevelID As Integer, ByVal argMenuID As String, ByVal argState As Boolean) As Boolean
            Return objMenuSetupDAL.UpdateMenuUserLevel(argUserLevelID, argMenuID, argState)
        End Function

        Public Function DeleteUserLevel(ByVal argSysID As Integer, Optional ByVal argForceDeleteUser As Boolean = False) As Boolean
            Return objMenuSetupDAL.DeleteUserLevel(argSysID, argForceDeleteUser)
        End Function

        Public Function UserLevelMenuNameCheck(ByVal UserLevelID As Integer, ByVal UserLevel As String, ByVal MenuText As String) As Boolean
            Return objMenuSetupDAL.UserLevelMenuNameCheck(MenuText, UserLevel, UserLevelID)
        End Function

        Private Sub UserMenuLevelCheck(ByVal argMenu As System.Windows.Forms.MenuItem, ByVal argMenuID As String, ByVal argUserLevel As String, ByVal argUserLevelID As Integer, ByVal argPackageType As String)
            Dim i As System.Windows.Forms.MenuItem

            'Determin menu permission. 
            'Add post fix '*' to Use Item 
            'If menu item is new, add menu item

            _strMenuIDList = _strMenuIDList & "'" & argMenuID & "##" & argMenu.Text.Replace("'", "''") & "',"

            If Not CheckMenuForPackageType(argMenu.Tag, argPackageType) Then

                argMenu.Visible = False
            Else
                argMenu.Visible = objMenuSetupDAL.UserMenuLevelCheck(argMenuID, argMenu.Text, argUserLevel, argUserLevelID)
            End If

            For Each i In argMenu.MenuItems
                UserMenuLevelCheck(i, argMenuID & i.Index.ToString("00"), argUserLevel, argUserLevelID, argPackageType)
            Next

        End Sub

        Public Sub UserMenuLevelCheck(ByVal argMainMenu As System.Windows.Forms.MainMenu, ByVal argUserLevel As String, ByVal argUserLevelID As Integer, ByVal argPackageType As String)
            Dim i As System.Windows.Forms.MenuItem

            _strMenuIDList = ""
            For Each i In argMainMenu.MenuItems
                UserMenuLevelCheck(i, i.Index.ToString("00"), argUserLevel, argUserLevelID, argPackageType)
            Next
            objMenuSetupDAL.DeleteUnuseMenu(_strMenuIDList)

            'Clear all unuse menu items without post fix '*'
            'objMenuSetupDAL.DeleteUserLevelMenu()
        End Sub

        Public Function CheckMenuForPackageType(ByVal argMenuText As String, ByVal argPackageType As String) As Boolean
            Try
                Dim tmpstr As String
                Dim i As String
                If String.IsNullOrEmpty(argMenuText) Then
                    Return False ' Or handle it as needed
                End If
                tmpstr = argMenuText.Split("_")(1)

                Do While tmpstr.Length >= 2
                    i = Left(tmpstr, 2)
                    tmpstr = tmpstr.Substring(2)
                    If i = "GE" Then
                        Return True
                    ElseIf InStr(argPackageType, i) > 0 Then
                        Return True
                    End If
                Loop

                Return False

            Catch ex As Exception
                Return False
            End Try

        End Function

        Public Function IsMenuVisible(ByVal argMenuItem As System.Windows.Forms.MenuItem, ByVal argMenuText As String) As Boolean
            If argMenuItem.Text = argMenuText Then
                Return argMenuItem.Visible
            Else
                Dim i As System.Windows.Forms.MenuItem
                For Each i In argMenuItem.MenuItems
                    If IsMenuVisible(i, argMenuText) Then
                        Return True
                    End If
                Next
                Return False
            End If
        End Function

        Public Function IsMenuVisible(ByVal argMainMenu As System.Windows.Forms.MainMenu, ByVal argMenuText As String) As Boolean
            Dim i As System.Windows.Forms.MenuItem

            For Each i In argMainMenu.MenuItems
                If IsMenuVisible(i, argMenuText) Then
                    Return True
                End If
            Next

            Return False
        End Function
        '-------------------''''''''''''
        'Public Sub UserMenuLevelCheckByRibbon(ByVal argMainMenu As C1Ribbon, ByVal argUserLevel As String, ByVal argUserLevelID As Integer, ByVal argPackageType As String)
        '    Dim dtMenu As DataTable
        '    dtMenu = objMenuSetupDAL.GetMenuUserLevelForRibbon(argUserLevelID, argUserLevel)
        '    SetupUserMenu(dtMenu, argUserLevelID, argMainMenu)
        'End Sub
        'Sub SetupUserMenu(ByVal dt As DataTable, ByVal id As String, ByVal c1 As C1Ribbon)
        '    Dim CurDateAttRow As DataRow() = dt.Select(id = True)
        '    If CurDateAttRow.Length > 0 Then
        '        For Each dataCurRow As DataRow In CurDateAttRow
        '            EnableSelectedMenu(dataCurRow("MenuName"), c1, dt)
        '        Next
        '    End If

        'End Sub

        'Sub EnableSelectedMenu(ByVal name As String, ByVal C1 As C1Ribbon, ByVal dt As DataTable)
        '    Dim bool As Boolean = False
        '    For i As Integer = 0 To C1.Tabs.Count - 1
        '        If C1.Tabs(i).Tag = name Then
        '            C1.Tabs(i).Enabled = True
        '            Exit For
        '        Else
        '            If C1.Tabs(i).Groups.Count > 0 Then
        '                For x As Integer = 0 To C1.Tabs(i).Groups.Count - 1
        '                    If C1.Tabs(i).Groups(x).Tag = name Then

        '                        C1.Tabs(i).Groups(x).Enabled = True
        '                        If name = "Data Synchronization" Then
        '                            For z As Integer = 0 To C1.Tabs(i).Groups(x).Items.Count - 1
        '                                Dim ribbonmenu As C1.Win.C1Ribbon.RibbonMenu = CType(C1.Tabs(i).Groups(x).Items(z), RibbonMenu)
        '                                If ribbonmenu.Items.Count > 0 Then
        '                                    For h As Integer = 0 To ribbonmenu.Items.Count - 1
        '                                        For Each dr As DataRow In dt.Rows
        '                                            If ribbonmenu.Items(h).Tag = dr.Item("MenuName") Then
        '                                                ribbonmenu.Items(h).Enabled = True
        '                                                Exit For
        '                                            Else
        '                                                ribbonmenu.Items(h).Enabled = False
        '                                            End If
        '                                        Next

        '                                    Next
        '                                End If
        '                            Next
        '                        End If
        '                        Exit Sub
        '                    End If
        '                Next
        '            End If
        '        End If
        '    Next
        'End Sub

        '--------------------''''''''''''


        '--------------------------------------
        'Edited by Nay Chi Zaw ( 18 Feb 2008 )
        Public Function SaveMenuUserEmployee(ByVal argUserLevelID As Integer, ByVal argEmployeeID As Integer) As Boolean
            Return objMenuSetupDAL.SaveMenuUserEmployee(argUserLevelID, argEmployeeID)
        End Function

        Public Function DeleteMenuUserEmployee(ByVal argSysID As Integer) As Boolean
            Return objMenuSetupDAL.DeleteMenuUserEmployee(argSysID)
        End Function


        Protected Sub Unselect(ByVal Node As System.Windows.Forms.TreeNode)
            If Not Node Is Nothing Then
                '   Node.Checked = False
                For I As Integer = 0 To Node.Nodes.Count - 1
                    If Node.Nodes(I).Nodes.Count > 0 Then
                        Unselect(Node.Nodes(I))
                    Else
                        Node.Nodes(I).Checked = False
                    End If
                Next
            End If
        End Sub


        Public Function GetEmployeebyLocation(ByVal Cri As String) As DataTable
            GetEmployeebyLocation = objMenuSetupDAL.GetEmployeebyLocation(Cri)
        End Function
        '-------------------
    End Class

End Namespace