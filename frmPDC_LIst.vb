Imports System.ComponentModel
Imports System.IO
Imports Microsoft.Office.Interop

Public Class frmPDC_List
    Dim ModuleID As String = "PDC"

    Private Sub frmItem_Pricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Text = "(" & database & ") - PDC List "
            LoadList()


        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        End Try
    End Sub

    Private Sub LoadList()
        dgvList.Rows.Clear()
        Dim query As String
        query = " SELECT  TransID, DatePDC, Bank, CheckNumber, Amount, VCECode, VCEName, Remarks  " &
                " FROM    viewPDC_Active " &
                " ORDER BY DatePDC, Bank, CheckNumber "
        SQL.FlushParams()
        SQL.ReadQuery(query)
        While SQL.SQLDR.Read
            dgvList.Rows.Add(SQL.SQLDR("TransID").ToString, CDate(SQL.SQLDR("DatePDC")).ToString("MM/dd/yyyy"),
                             SQL.SQLDR("Bank").ToString, SQL.SQLDR("CheckNumber").ToString,
                            CDec(SQL.SQLDR("Amount")).ToString("N2"),
                          SQL.SQLDR("VCECode").ToString, SQL.SQLDR("VCEName").ToString, SQL.SQLDR("Remarks").ToString)
        End While
        lblCounter.Text = "Record Count : " & dgvList.Rows.Count
    End Sub

    Private Sub tsbNew_Click(sender As Object, e As EventArgs) Handles tsbNew.Click
        If Not AllowAccess("PDC_ADD") Then
            msgRestricted()
        Else
            frmPDC.ShowDialog()
        End If
    End Sub

    Private Sub tsbExit_Click(sender As Object, e As EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub dgvList_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList.CellDoubleClick
        If e.RowIndex <> -1 AndAlso e.ColumnIndex <> -1 Then
            Dim f As New frmPDC
            f.ShowDialog(dgvList.Item(dgcID.Index, e.RowIndex).Value)
            f.Dispose()
        End If
    End Sub
End Class