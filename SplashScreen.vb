Imports System.Data.SqlClient

Public NotInheritable Class SplashScreen
    Dim a As Integer
    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).

    Dim masterCon As SqlConnection
    Dim masterCmd As SqlCommand
    Dim masterReader As SqlDataReader

    Private Sub SplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Format the version information using the text set into the Version control at design time as the
        'formatting string.  This allows for effective localization if desired.
        'Build and revision information could be included by using the following code and changing the 
        'Version control's designtime text to "Version {0}.{1:00}.{2}.{3}" or something similar


        'Copyright info
        Copyright.Text = My.Application.Info.Copyright
        CheckForUpdates()
        If ConnectToServer() = True Then
            If isInitialized() Then
                Timer1.Start()
            Else
                frmInit.Show()
            End If
        Else
            frmServerSetup.ShowDialog()
        End If
    End Sub

    Private Sub CheckForUpdates()
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        Dim CurrentVersion As String = My.Application.Info.Version.ToString
        Dim a As String = My.Application.Info.DirectoryPath
        Dim name As String = Environment.GetCommandLineArgs(0).ToString.Replace(a, "").Replace("\", "")
        Dim pHelp As New ProcessStartInfo
        If My.Computer.FileSystem.FileExists("Gr8Updater.exe") Then
            pHelp.FileName = "Gr8Updater.exe"
            pHelp.Arguments = CurrentVersion & " " & name & " /S "
            pHelp.UseShellExecute = True
            pHelp.WindowStyle = ProcessWindowStyle.Normal
            Dim proc As Process = Process.Start(pHelp)
            Version.Text = CurrentVersion
        End If
    End Sub
    Private Function ConnectToServer() As Boolean
        Try
            Dim query As String
            query = "SELECT  Server, UserName, Password, DefaultDB FROM Database_Info  "
            AlphaQuery(query)
            If AlphaReader.Read Then
                DefaultServer = AlphaReader("Server").ToString
                DBUser = AlphaReader("UserName").ToString
                DBPassword = AlphaReader("Password").ToString
                database = "FGVI_" & AlphaReader("DefaultDB").ToString

                If HasConnection() Then
                    Return True
                Else
                    Msg("Cannot connect to server, please check your connection and make sure your server name is correct", MsgBoxStyle.Exclamation)
                    Return False
                End If
            Else
                Msg("No default server, please enter the server name", MsgBoxStyle.Exclamation)
                Return False
            End If
        Catch ex As Exception
            Msg(ex.Message, MsgBoxStyle.Exclamation)
            Return False
        End Try
    End Function
    Private Function HasConnection() As Boolean
        Try
            Dim valid As Boolean = True
            If valid Then
                masterCon = New SqlConnection With {.ConnectionString = ("Server=" & DefaultServer & ";Database=" & database & ";integrated security=sspi;Uid=" & DBUser & ";Pwd=" & DBPassword & ";Trusted_Connection=no;MultipleActiveResultSets=True;Max Pool Size=200;")}
                If masterCon.State = ConnectionState.Open Then
                    masterCon.Close()
                Else
                End If
                masterCon.Open()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
            Msg(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Function
    Private Sub LoadLoginForm()
        frmUserLogin.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        a += 1
        If a = 10 Then
            Timer1.Stop()
            LoadLoginForm()
        End If
    End Sub

End Class
