Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class SQLControl
    Private SQLCon As SqlConnection
    Private SQLCon1 As SqlConnection
    Private SQLCon2 As SqlConnection
    Private SQLCon3 As SqlConnection
   Private SQLCmd As SqlCommand
    Public SQLDA As SqlDataAdapter
    Public SQLDS As DataSet
    Public SQLDR As SqlDataReader
    Public SQLDRRFID As SqlDataReader
    Public SQLDR2 As SqlDataReader
    Public SQLDR3 As SqlDataReader
    Public SQLDT As DataTable
    Public RecordCount As Integer
    Public SQLParams As New List(Of SqlParameter)
    Dim r As StreamReader
    Dim server As String
    Dim Transaction As SqlTransaction

    Public Sub New(Optional ByVal System As String = "JADE")
        If System = "JADE" Then
            Dim App_Path As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
            server = DefaultServer
            SQLCon = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=" & database & ";integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=10000000;")}
            SQLCon1 = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=" & database & ";integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=10000000;")}
            SQLCon2 = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=" & database & ";integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=10000000;")}
            SQLCon3 = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=" & database & ";integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=10000000;")}
        ElseIf System = "RUBY" Then
            Dim App_Path As String = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
            ' server = "122.52.201.210"
            server = DefaultServer
            SQLCon = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=RUBY_ePeople;integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
            SQLCon1 = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=RUBY_ePeople;integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
            SQLCon2 = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=RUBY_ePeople;integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
            SQLCon3 = New SqlConnection With {.ConnectionString = ("Server=" & Trim(server) & ";Database=RUBY_ePeople;integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
        ElseIf System = "LICENSE SERVER" Then
            server = "(local)"
            SQLCon = New SqlConnection With {.ConnectionString = ("Server=122.52.201.210;Database=paypal;integrated security=sspi;Uid=sa;Pwd=@dm1nEvo;Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
            SQLCon1 = New SqlConnection With {.ConnectionString = ("Server=122.52.201.210;Database=paypal;integrated security=sspi;Uid=sa;Pwd=@dm1nEvo;Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
            SQLCon2 = New SqlConnection With {.ConnectionString = ("Server=122.52.201.210;Database=paypal;integrated security=sspi;Uid=sa;Pwd=@dm1nEvo;Trusted_Connection=no;MultipleActiveResultSets=True;Connection Timeout=30;Command Timeout=300;")}
            SQLCon3 = New SqlConnection With {.ConnectionString = ("Server=122.52.201.210;Database=paypal;integrated security=sspi;Uid=sa;Pwd=@dm1nEvo;Trusted_Connection=no;MultipleActiveResultSets=True'Connection Timeout=30;Command Timeout=300;")}
        End If
      
    End Sub


    Public Sub GetDataTable(ByVal CommandText As String, Optional ByVal Server As Integer = 0, Optional ByVal With_Param As Boolean = False)
        Try

            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Using SQLCmd = New SqlCommand(CommandText, SQLCon)
                SQLCon.Open()
                For Each p As SqlParameter In SQLParams
                    SQLCmd.Parameters.Add(p)
                    SQLCmd.Parameters(p.ParameterName).Value = p.Value
                Next
                SQLDA = New SqlDataAdapter(SQLCmd)
                SQLDT = New DataTable
                RecordCount = SQLDA.Fill(SQLDT)
                If With_Param Then
                    FlushParams()
                End If
            End Using
        Catch ex As Exception

        Finally
            CloseCon(Server)
            FlushParams()
        End Try
    End Sub

    Public Sub GetQuery(ByVal Query As String, Optional ByVal Server As Integer = 86, Optional ByVal With_Param As Boolean = False)
        Try
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            Using SQLCmd = New SqlCommand(Query, SQLCon)
                SQLCon.Open()
                If Not Query.Contains("SELECT") Then
                    SQLCmd.CommandType = CommandType.StoredProcedure
                End If
                For Each p As SqlParameter In SQLParams
                    SQLCmd.Parameters.Add(p)
                    SQLCmd.Parameters(p.ParameterName).Value = p.Value
                Next
                SQLDA = New SqlDataAdapter(SQLCmd)
                SQLDS = New DataSet
                RecordCount = SQLDA.Fill(SQLDS)
                If With_Param Then
                    FlushParams()
                End If
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            CloseCon(Server)
        End Try
    End Sub

    Public Sub ReadQuery(ByVal Query As String, Optional ByVal Connection As Integer = 86)
        Try
            If Connection = 86 Then
                If SQLCon.State = ConnectionState.Open Then
                    SQLCon.Close()
                End If
                Using SQLCmd = New SqlCommand(Query, SQLCon)
                    SQLCon.Open()
                    If Not Query.Contains("SELECT") Then
                        SQLCmd.CommandType = CommandType.StoredProcedure
                    End If
                    For Each p As SqlParameter In SQLParams
                        SQLCmd.Parameters.Add(p)
                        SQLCmd.Parameters(p.ParameterName).Value = p.Value
                    Next
                    SQLDR = SQLCmd.ExecuteReader
                End Using
            ElseIf Connection = 2 Then
                If SQLCon2.State = ConnectionState.Open Then
                    SQLCon2.Close()
                End If
                Using SQLCmd = New SqlCommand(Query, SQLCon2)
                    SQLCon2.Open()
                    If Not Query.Contains("SELECT") Then
                        SQLCmd.CommandType = CommandType.StoredProcedure
                    End If
                    For Each p As SqlParameter In SQLParams
                        SQLCmd.Parameters.Add(p)
                        SQLCmd.Parameters(p.ParameterName).Value = p.Value
                    Next
                    SQLDR2 = SQLCmd.ExecuteReader
                End Using
            ElseIf Connection = 3 Then
                If SQLCon3.State = ConnectionState.Open Then
                    SQLCon3.Close()
                End If
                Using SQLCmd = New SqlCommand(Query, SQLCon3)
                    SQLCon3.Open()
                    SQLDR3 = SQLCmd.ExecuteReader
                End Using
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            FlushParams()
        End Try
    End Sub

    Public Function ExecNonQuery(ByVal Query As String) As Integer
        Try
            Dim RecordChanged As Integer
            Using SQLCmd = New SqlCommand(Query, SQLCon1)
                SQLCmd.CommandTimeout = 0

                If Transaction Is Nothing Then
                    If SQLCon1.State = 1 Then
                        SQLCon1.Close()
                    End If
                    SQLCon1.Open()
                Else
                    SQLCmd.Connection = Transaction.Connection
                    SQLCmd.Transaction = Transaction
                End If


                For Each p As SqlParameter In SQLParams
                    SQLCmd.Parameters.Add(p)
                    SQLCmd.Parameters(p.ParameterName).Value = p.Value
                Next
                RecordChanged = SQLCmd.ExecuteNonQuery()
            End Using
            Return RecordChanged
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
            Return -1
        Finally
            SQLCon1.Close()
            FlushParams()
        End Try
    End Function

    Public Sub UploadExcel(ByVal path As String, ByVal DTable As String, ByVal STable As String)
        Try
            Dim ExcelConnection As New System.Data.OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & path & ";Extended Properties=""Excel 12.0;HDR=YES;""")
            ExcelConnection.Open()
            Dim query As String
            query = " SELECT * INTO #Payroll_Payroll FROM [" & STable & "$]"
            Dim objCmdSelect As OleDbCommand = New OleDbCommand(query, ExcelConnection)
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
                SQLCon.Open()
                objCmdSelect.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message, ex.InnerException)
        Finally
            SQLCon.Close()
        End Try
    End Sub

    Public Sub AddParam(ByVal Name As String, ByVal Value As Object, Optional ByVal DataType As SqlDbType = SqlDbType.NVarChar)
        Dim newParam As New SqlParameter With {.ParameterName = Name, .Value = Value, .SqlDbType = DataType}
        SQLParams.Add(newParam)
    End Sub

    Public Sub FlushParams()
        SQLParams.Clear()
    End Sub

    Public Sub CloseCon(Optional ByVal Server As Integer = 0)
        If Server = 0 Then
            If SQLCon.State = ConnectionState.Open Then
                SQLCon.Close()
            End If
            If SQLCon1.State = ConnectionState.Open Then
                SQLCon1.Close()
            End If
            If SQLCon2.State = ConnectionState.Open Then
                SQLCon2.Close()
            End If
        End If
    End Sub

    Public Sub BeginTransaction()
        SQLCon.Open()
        Transaction = SQLCon.BeginTransaction
    End Sub

    Public Sub Commit()
        Transaction.Commit()
        Transaction = Nothing
    End Sub
    Public Sub Rollback()
        Transaction.Rollback()
        Transaction = Nothing
    End Sub
End Class
