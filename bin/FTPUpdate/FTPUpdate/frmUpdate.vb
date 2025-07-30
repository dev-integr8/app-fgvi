Public Class frmUpdate
    Dim a As Integer = 0
    Dim Connectiontxt As String
    Dim r As IO.StreamReader

    Private Sub frmUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            CloseEVO()
            Timer1.Start()
        Catch
        End Try
    End Sub

    Private Sub CloseEVO()
        For Each prog As Process In Process.GetProcesses
            If prog.ProcessName = "jade" Then
                prog.Kill()
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim App_Server As String
        App_Server = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        r = New IO.StreamReader(App_Server & "\Data\eMRP.ini")
        While (r.Peek() > -1)
            Connectiontxt = r.ReadLine
        End While
        r.Close()

        a += 1
        If a = 1 Then
            Timer1.Stop()
            Dim App_Path As String
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
            Dim FileToDelete As String = App_Path & "\jade.exe"
            If My.Computer.FileSystem.FileExists(FileToDelete) Then
                My.Computer.FileSystem.DeleteFile(FileToDelete)
            End If
            My.Computer.FileSystem.CopyFile("\\" & IIf(Connectiontxt.Split(",")(0).Trim = "122.49.216.242", Connectiontxt.Split(",")(0).Trim, "192.168.1.199") & "\accc_mis\AKO SYSTEMS\PATCH\JADE\Jade.exe", App_Path & "\jade.exe")

            System.Diagnostics.Process.Start(App_Path & "\jade.exe")
            End
        End If
    End Sub
End Class