Public Class frmUpdate
    Dim a As Integer = 0
    Private Sub frmUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Timer1.Start()
        Catch
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        a += 1
        If a = 1 Then
            Timer1.Stop()

            Dim App_Path As String
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName

            'execute the report update here
            Dim App_Path2 As String
            App_Path2 = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
            App_Path2 = App_Path & "\CR_Reports\" & database & ""
            My.Computer.FileSystem.CopyDirectory("\\192.168.254.121\ako\IKAW\Debug\CR_Reports\" & database & "", App_Path2, True)
            MsgBox("Update Report Succesfully!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub

End Class