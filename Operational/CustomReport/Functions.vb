Imports System.Configuration
Public Class Functions

    Public Shared Function SelectData(ByVal Query As String, ByVal TableName As String, ByVal Customer As String, ByVal GoldQuality As String, ByVal ItemCategory As String, ByVal ItemName As String, ByVal GemsCategory As String, ByVal Staff As String, ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
        Dim _ConnectionString As String = ""

        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim Connection As New ConnectionStringSettings
        Dim connections As New ConnectionStringSettingsCollection
        If config.HasFile Then
            connections = config.ConnectionStrings.ConnectionStrings
            Dim conEnum As IEnumerator = connections.GetEnumerator()
            While conEnum.MoveNext
                Connection = conEnum.Current
                _ConnectionString = Connection.ConnectionString

            End While
        End If
        Dim SqlConn As SqlClient.SqlConnection = New SqlClient.SqlConnection(_ConnectionString)

        Dim sqlcmd As SqlClient.SqlCommand = SqlConn.CreateCommand()
        Dim dtResult As New System.Data.DataTable
        Dim sqldr As SqlClient.SqlDataReader

        sqlcmd.CommandText = Query
        'sqlcmd.CommandText += " Where 1=1 "

        If Customer <> "" Then
            sqlcmd.CommandText += " And H.CustomerID = @Customer"
            sqlcmd.Parameters.Add("@Customer", SqlDbType.VarChar)
            sqlcmd.Parameters("@Customer").Value = Customer
        End If
        If GoldQuality <> "" Then
            sqlcmd.CommandText += " And F.GoldQualityID = @GoldQuality"
            sqlcmd.Parameters.Add("@GoldQuality", SqlDbType.VarChar)
            sqlcmd.Parameters("@GoldQuality").Value = GoldQuality
        End If
        If ItemCategory <> "" Then
            sqlcmd.CommandText += " And F.ItemCategoryID = @ItemCategory"
            sqlcmd.Parameters.Add("@ItemCategory", SqlDbType.VarChar)
            sqlcmd.Parameters("@ItemCategory").Value = ItemCategory
        End If

        If ItemName <> "" Then
            sqlcmd.CommandText += " And F.ItemNameID = @ItemName"
            sqlcmd.Parameters.Add("@ItemName", SqlDbType.VarChar)
            sqlcmd.Parameters("@ItemName").Value = ItemName
        End If

        If GemsCategory <> "" Then
            sqlcmd.CommandText += " And GemsCategoryID = @GemsCategory"
            sqlcmd.Parameters.Add("@GemsCategory", SqlDbType.VarChar)
            sqlcmd.Parameters("@GemsCategory").Value = GemsCategory
        End If
        If Staff <> "" Then
            sqlcmd.CommandText += " And H.StaffID = @Staff"
            sqlcmd.Parameters.Add("@Staff", SqlDbType.VarChar)
            sqlcmd.Parameters("@Staff").Value = Staff
        End If

        If Not IsNothing(FromDate) Then
            sqlcmd.Parameters.Add("@FromDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@FromDate").Value = CDate(FromDate & " 00:00:00")
        End If
        If Not IsNothing(ToDate) Then
            sqlcmd.Parameters.Add("@ToDate", SqlDbType.DateTime)
            sqlcmd.Parameters("@ToDate").Value = CDate(ToDate & " 23:59:59")
        End If

        'sqlcmd.CommandText += " And H.SaleDate Between @FromDate And @ToDate"
        ''SqlConn.Open()

        ''sqldr = sqlcmd.ExecuteReader(CommandBehavior.CloseConnection)

        ''dtResult.Load(sqldr)
        ''dtResult.TableName = TableName
        ' ''sqldr.Close()
        ''Return dtResult

        SqlConn.Open()
        sqldr = sqlcmd.ExecuteReader
        dtResult.Load(sqldr)
        dtResult.TableName = TableName
        sqldr.Close()
        Return dtResult
    End Function

End Class
