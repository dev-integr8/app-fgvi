Public Class frmData_Search
    Dim ModID, p1, p2 As String
    Public data As String
    Public Overloads Function ShowDialog(ByVal ID As String, Optional ByVal Param1 As String = "", Optional ByVal Param2 As String = "") As Boolean
        p1 = Param1
        p2 = Param2
        ModID = ID
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub frmData_Search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub
    Private Sub LoadData()
        Try
            Dim filter As String = ""
            Dim query As String = ""

            Select Case ModID
                Case "BM_Destination"
                    ' QUERY 
                    query = " SELECT DISTINCT Destination FROM tblBM_Tariff " &
                            " WHERE Location = @Location AND Destination LIKE '%' + @Destination + '%' " &
                            " ORDER BY Destination "
                    SQL.FlushParams()
                    SQL.AddParam("@Location", p1)
                    SQL.AddParam("@Destination", p2)
            End Select
            If query <> "" Then
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    dgvList.DataSource = SQL.SQLDS.Tables(0)
                    dgvList.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                End If
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModID)
        Finally
            SQL.FlushParams()
        End Try

    End Sub

    Private Sub ChooseRecord()
        If dgvList.SelectedRows.Count = 1 Then
            data = dgvList.SelectedRows(0).Cells(0).Value
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If dgvList.SelectedRows(0).Index <> -1 Then
            ChooseRecord()
        End If
    End Sub

    Private Sub dgvList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            ChooseRecord()
        End If
    End Sub

    Private Sub dgvList_DoubleClick(sender As Object, e As EventArgs) Handles dgvList.DoubleClick
        If dgvList.SelectedRows(0).Index <> -1 Then
            ChooseRecord()
        End If
    End Sub
End Class