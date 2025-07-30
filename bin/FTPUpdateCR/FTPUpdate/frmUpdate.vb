Public Class frmUpdate
    Dim w As IO.StreamWriter
    Dim r As IO.StreamReader
    Private Sub frmUpdate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ControlBox = False
        DateTime.Interval = 10000 '1 seconds
        DateTime.Enabled = True
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Value += 5
        If ProgressBar1.Value = 100 Then
            Timer1.Enabled = False
        End If
    End Sub

    Private Sub DateTime_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTime.Tick
        Try
            'CloseEVO()

            Dim App_Path As String
            'BIN FOLDER OR THE DIRECTORY NAME OF YOUR APPLICATION
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
            ''DELETE APPLICATION VERSON, SAMPLE EHR APP
            'Dim FileToDelete As String
            'FileToDelete = App_Path & "\EvoSolutions.exe"
            ''FileToDelete = "C:\Users\MIS Admin\Desktop\Cooperative\Cooperative\bin\Debug\Cooperative.exe"
            'If System.IO.File.Exists(FileToDelete) = True Then
            '    System.IO.File.Delete(FileToDelete)
            'End If


            'DOWNLOAD UPDATE APPLICATION ON THE FTP FOLDER, SAMPLE EHR APP
            '  My.Computer.Network.DownloadFile(New Uri("ftp://192.168.1.119/../HPPHI.exe"), App_Path & "\HPPHI.exe")
            ' My.Computer.FileSystem.CopyFile("\\WIN-STQTQMVG3Q7\Evo\EvoSolution\bin\Debug\HPPHI.exe", App_Path & "\HPPHI.exe")


            'My.Computer.FileSystem.CopyFile("\\192.168.1.119\d$\STOPNGAS\HPPHI.exe", App_Path & "\HPPHI.exe")
            ' My.Computer.FileSystem.CopyFile("\\192.168.1.40\Users\Public\Documents\JADE ACCOUNTING\Debug\EvoSolutions.exe", App_Path & "\EvoSolutions.exe")
            ' My.Computer.FileSystem.MoveDirectory(Application.StartupPath & "\\192.168.1.40\Users\Public\Documents\JADE ACCOUNTING\Debug\CR_Reports", Application.StartupPath & "\CR_Reports", True)
            'My.Computer.FileSystem.CopyDirectory("\\192.168.1.254\evo_shared\JADE\Debug\CR_Reports\JADE_01", App_Path & "\CR_Reports\JADE_01", True)
            Try
                My.Computer.FileSystem.CopyDirectory("\\Dcci_server\ako_dcci\AKO Systems\JADE_01\CR_Reports\JADE_01\", App_Path & "\CR_Reports\JADE_01", True)
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            System.Diagnostics.Process.Start(App_Path & "\jade.exe")
            DateTime.Enabled = False

            'KILL THIS UPDATE APP
            End
        Catch

        End Try


    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub CloseEVO()

        For Each prog As Process In Process.GetProcesses
            If prog.ProcessName = "EvoSolutions" Then
                prog.Kill()
            End If
        Next
    End Sub


End Class