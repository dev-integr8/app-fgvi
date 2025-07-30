Public Class frmTemplate
    Public TransID As String

    Private Sub frmTemplate_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'ClearText()
    End Sub

    Public Overloads Function ShowDialog(ByVal ID As String, Name As String) As Boolean
        TransID = ID
        txtTemplateName.Text = Name
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub ClearText()
        TransID = ""
        txtTemplateName.Text = ""
    End Sub

    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        If txtTemplateName.Text = "" Then
            MsgBox("Please enter Template Name!", MsgBoxStyle.Exclamation)
        ElseIf MessageBox.Show("Tag as Template" & vbNewLine & " Continue?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            TagAsTemplate(TransID)
            MsgBox("New Record Saved successfully!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

    Private Sub TagAsTemplate(ID As String)
        activityStatus = True
        Try
            Dim updateSQL As String
            updateSQL = " UPDATE    tblJV " & _
                      " SET       TemplateName = @TemplateName, isTemplate = @isTemplate " & _
                      " WHERE     TransID = @TransID  "
            SQL.FlushParams()
            SQL.AddParam("@TransID", TransID)
            SQL.AddParam("@TemplateName", txtTemplateName.Text)
            SQL.AddParam("@isTemplate", True)
            SQL.ExecNonQuery(updateSQL)
        Catch ex As Exception
            MsgBox(ex.Message)
            activityStatus = False
        Finally
            '        RecordActivity(UserID, ModuleID, "INSERT", "Inserted VCECode : " & VCECode, activityStatus)
        End Try

    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        TransID = ""
        Me.Close()
    End Sub
End Class