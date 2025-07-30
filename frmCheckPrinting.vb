﻿Public Class frmCheckPrinting
    Dim SQL As New SQLControl
    Public CVTransID As Integer
    Public CV_No As String

    Private Sub frmCheckPrinting_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            LoadCheckNo()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadCheckNo()
        Dim query As String
        query = " SELECT RefNo FROM tblCV_BankRef WHERE CV_No ='" & CVTransID & "'  "
        SQL.ReadQuery(query)
        cbCheckNo.Items.Clear()
        While SQL.SQLDR.Read
            cbCheckNo.Items.Add(SQL.SQLDR("RefNo").ToString)
        End While
        If cbCheckNo.Items.Count > 0 Then
            cbCheckNo.SelectedIndex = 0
        End If
    End Sub

    Private Sub GetCheckDetails()
          Dim query As String
        query = " SELECT  tblCV.CV_No, VCEName, RefAmount, RefDate, RefName " & _
                " FROM    tblCV_BankRef INNER JOIN tblCV " & _
                " ON      tblCV_BankRef.CV_No = tblCV.TransID " & _
                " INNER JOIN viewVCE_Master " & _
                " ON      tblCV.VCECode = viewVCE_Master.VCECode " & _
                " WHERE   tblCV.TransID ='" & CVTransID & "' AND RefNo ='" & cbCheckNo.SelectedItem & "'"
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtName.Text = SQL.SQLDR("RefName").ToString
            txtAmount.Text = SQL.SQLDR("RefAmount").ToString
            dtpDate.Value = SQL.SQLDR("RefDate")
            CV_No = SQL.SQLDR("CV_No")
        End If
    End Sub

    Private Sub btnPrint_Click(sender As System.Object, e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim f As New frmReport_Display
            f.ShowDialog("Check", CV_No, txtName.Text, dtpDate.Value.Date, txtAmount.Text)
            f.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            RecordActivity(UserID, "Check Print", Me.Name.ToString, "PRINT", "CV_No", CV_No, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub cbCheckNo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbCheckNo.SelectedIndexChanged
        Try
            GetCheckDetails()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class