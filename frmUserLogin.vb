Imports System.IO
Imports System.Data.SqlClient

Public Class frmUserLogin
    Inherits MetroFramework.Forms.MetroForm
    Dim moduleID As String = "UL"
    Dim counter As Integer = 1
    Dim masterCon As SqlConnection
    Dim masterCmd As SqlCommand
    Dim masterReader As SqlDataReader

    Private Sub FrmLogin_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadDefaultServer()
        Me.FocusMe()
        txtUsername.Select()
        If isRegistered Then
            lblTrial.Visible = False
            DaysLeft = SubsRemainingDays()
            If DaysLeft = 0 Then
                Me.Hide()
                Me.Enabled = False
                SubsExpired = True
                frmInit.Show()
            ElseIf DaysLeft <= 7 Then
                ' TRIAL VERSION
                lblTrial.Visible = True
                lblTrial.Text = "Gr8Books Windows Subscription (" & DaysLeft & " days left)"
            ElseIf DaysLeft = -1 Then
                MsgBox("System Error, Please contact your support specialist")
                Application.Exit()
            End If
        ElseIf isTrial Then
            DaysLeft = TrialRemainingDay()

            If DaysLeft = 0 Then
                ' TRIAL PERIOD IS OVER
                Me.Hide()
                Me.Enabled = False
                TrialExpired = True
                frmInit.Show()
            ElseIf DaysLeft > 0 Then
                ' TRIAL VERSION
                lblTrial.Visible = True
                lblTrial.Text = "Gr8Bookws Windows (" & DaysLeft & " days left)"
            ElseIf DaysLeft = -1 Then
                MsgBox("System Error, Please contact your support specialist")
                Application.Exit()
            End If
        End If
       
    End Sub

    Private Sub LoadDefaultServer()
        Try
            Dim query As String
            query = "SELECT  Server, UserName, Password, DefaultDB FROM Database_Info  "
            cbServer.Items.Clear()
            AlphaQuery(query)
            If AlphaReader.Read Then
                cbServer.Text = AlphaReader("Server").ToString
                txtUser.Text = AlphaReader("UserName").ToString
                txtPass.Text = AlphaReader("Password").ToString
                cbDatabase.SelectedItem = AlphaReader("DefaultDB").ToString
                DefaultServer = cbServer.Text
                DBUser = txtUser.Text
                DBPassword = txtPass.Text
                If Not AlphaReader("DefaultDB").ToString.Contains("FGVI_") Then
                    database = "FGVI_" & AlphaReader("DefaultDB").ToString
                Else
                    database = AlphaReader("DefaultDB").ToString

                End If

                If HasConnection() Then
                    LoadDatabase()
                End If
                ShowSettings(False)
            Else
                Msg("No default server, please select server to connect or contact your system administrator", MsgBoxStyle.Exclamation)
                ShowSettings(True)
            End If
        Catch ex As Exception
            Msg(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Function HasConnection() As Boolean
        Try
            Dim valid As Boolean = True

            ' IF IP ADDRESS OR HOSTNAME, PING FIRST
            'If cbServer.Text.Contains(".") Then
            '    If Not My.Computer.Network.Ping(cbServer.Text, 2000) Then
            '        valid = False
            '    End If
            'End If

            If valid Then
                masterCon = New SqlConnection With {.ConnectionString = ("Server=" & cbServer.Text & ";Database=master;integrated security=sspi;Uid=" & txtUser.Text & ";Pwd=" & txtPass.Text & ";Trusted_Connection=no;MultipleActiveResultSets=True;Max Pool Size=200;")}
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


    Private Sub LoadDatabase()
        SetDatabase()
        Dim query As String
        query = " SELECT REPLACE(name,'FGVI_','') AS DBName  " &
                " FROM   sys.databases  " &
                " WHERE  name LIKE 'FGVI%' "
        SQL.ReadQuery(query)
        cbDatabase.Items.Clear()
        While SQL.SQLDR.Read
            cbDatabase.Items.Add(SQL.SQLDR("DBName").ToString)
        End While
        If cbDatabase.Items.Count > 0 Then
            cbDatabase.SelectedIndex = 0
        End If
    End Sub

    Private Function PatchUpdated() As Boolean
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        If FileVersionInfo.GetVersionInfo(App_Path & "\jade.exe").FileVersion <> FindVersion() Then
            Return False
        Else
            Return True
        End If

        Return True
    End Function

    Private Function getBusinessType() As String
        Dim query As String
        SetDatabase()
        query = " SELECT BusinessCode FROM tblBusinessType WHERE Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count = 1 Then
            Return SQL.SQLDS.Tables(0).Rows(0).Item(0).ToString
        Else
            Return ""
        End If
    End Function

    Private Function getBranchCode() As String
        Dim query As String
        SetDatabase()
        query = " SELECT BranchCode FROM tblBranch WHERE Status ='Active' "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count = 1 Then
            Return SQL.SQLDS.Tables(0).Rows(0).Item(0).ToString
        Else
            Return ""
        End If
    End Function

    Private Function FindVersion() As String
        Dim query As String
        query = " SELECT Version FROM tblSystemVersion WHERE Status ='Active' "
        SQL.ReadQuery(query)
        SQL.SQLDR.Read()
        Return SQL.SQLDR("Version").ToString
    End Function

    Protected Sub UpdateLastLogin(ByVal UserID As String)
        Dim updateSQL As String
        updateSQL = " UPDATE tblUser SET DateLastLogin = GETDATE()  WHERE UserID = '" & UserID & "' "
        SQL.ExecNonQuery(updateSQL)
    End Sub

    Private Sub ShowSettings(ByVal Show As Boolean)
        If Show = True Then
            Me.Width = 688
            Me.Height = 409
            btnOption.Text = "Options <<"
            txtUser.TabStop = True
            txtPass.TabStop = True
            btnConnect.TabStop = True
            cbServer.TabStop = True
        Else
            Me.Width = 273
            Me.Height = 409
            btnOption.Text = "Options >>"
            txtUser.TabStop = False
            txtPass.TabStop = False
            btnConnect.TabStop = False
            cbServer.TabStop = False
        End If
    End Sub

    Private Sub SaveServer()
        Dim deleteSQL, insertSQL As String
        deleteSQL = "  DELETE FROM Database_Info "
        AlphaExecute(deleteSQL)
        insertSQL = " INSERT INTO " & _
                    " Database_Info([Server], [Username], [Password]) " & _
                    " VALUES('" & cbServer.Text & "','" & txtUser.Text & "','" & txtPass.Text & "')"
        AlphaExecute(insertSQL)
        DBUser = txtUser.Text
        DBPassword = txtPass.Text

    End Sub

    Private Sub UpdateDefaultDB()
        Dim updateSQL As String
        updateSQL = "  UPDATE Database_Info SET DefaultDB = '" & cbDatabase.SelectedItem & "' "
        AlphaExecute(updateSQL)
    End Sub

    Private Sub btnLogin_Click(sender As System.Object, e As System.EventArgs) Handles btnLogin.Click
        Try
            If cbDatabase.SelectedIndex = -1 Then
                Msg("Please select database!", MsgBoxStyle.Exclamation)
            Else
                activityStatus = True
                database = "FGVI_" & cbDatabase.SelectedItem
                Dim query As String
                SetDatabase()
                If txtUsername.Text = "" Or txtPassword.Text = "" Then
                    MsgBox("Enter your username or password!", MsgBoxStyle.Exclamation)
                    txtUsername.Focus()
                Else
                    query = " SELECT  UserID, LoginName, Password, UserLevel, Status, FirstLogin, " & _
                            "         UserName AS Name " & _
                            " FROM    tblUser " & _
                            " WHERE   LoginName = '" & txtUsername.Text & "' "
                    SQL.ReadQuery(query)
                    If SQL.SQLDR.Read Then
                        Dim hash As String = SQL.SQLDR("Password")
                        If BCrypt.Net.BCrypt.Verify(txtPassword.Text, hash) Then
                            If SQL.SQLDR("Status").ToString = "Locked" Then
                                MsgBox("Account locked, Please try again or contact the administrator", MsgBoxStyle.Information)
                                activityStatus = False
                            Else
                                If SQL.SQLDR("FirstLogin") = True Then
                                    MsgBox("Welcome! This is your first login, Please change your password", MsgBoxStyle.Information)
                                    frmUserChange.lblPassword.Text = SQL.SQLDR("Password").ToString
                                    frmUserChange.lblUserID.Text = SQL.SQLDR("UserID").ToString
                                    frmUserChange.txtUsername.Text = SQL.SQLDR("LoginName").ToString
                                    frmUserChange.ShowDialog()
                                Else
                                    UserID = SQL.SQLDR("UserID").ToString
                                    UserName = SQL.SQLDR("LoginName").ToString
                                    Password = SQL.SQLDR("Password").ToString
                                    UserLevel = SQL.SQLDR("UserLevel").ToString
                                    Name = SQL.SQLDR("Name").ToString
                                    DBUser = txtUser.Text
                                    DBPassword = txtPass.Text

                                    UpdateLastLogin(UserID)
                                    UpdateDefaultDB()
                                    BusinessType = getBusinessType()
                                    BranchCode = getBranchCode()
                                    LoadBaseCurrency()
                                    If BusinessType = "" Then
                                        If chkShow.Checked = True Then
                                            query = " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date) AS Deadline,  DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date)) AS DateDifference    " & _
                                                    " 	FROM    tblTax_Deadline    " & _
                                                    " 	WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date)) <=  dbo.GetBIR_RemindersDay() " & _
                                                    " 		AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date) " & _
                                                    " 	UNION ALL " & _
                                                    " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date) AS Deadline,  DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)) AS DateDifference    " & _
                                                    " 	FROM    tblTax_Deadline " & _
                                                    " 	WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)) <=  dbo.GetBIR_RemindersDay()  " & _
                                                    " 		AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)  "

                                            SQL.ReadQuery(query)

                                            If SQL.SQLDR.Read Then
                                                frmBIRReminder.Show()
                                            Else
                                                frmOption.Show()
                                            End If
                                        Else
                                            frmOption.Show()
                                        End If
                                    ElseIf BranchCode = "" Then
                                        If chkShow.Checked = True Then
                                            query = " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date) AS Deadline,  DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date)) AS DateDifference    " & _
                                                    " 	FROM    tblTax_Deadline    " & _
                                                    " 	WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date)) <=  dbo.GetBIR_RemindersDay() " & _
                                                    " 		AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date) " & _
                                                    " 	UNION ALL " & _
                                                    " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date) AS Deadline,  DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)) AS DateDifference    " & _
                                                    " 	FROM    tblTax_Deadline " & _
                                                    " 	WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)) <=  dbo.GetBIR_RemindersDay()  " & _
                                                    " 		AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, DATEADD(year,1,GETDATE()))) AS Date)  "

                                            SQL.ReadQuery(query)

                                            If SQL.SQLDR.Read Then
                                                frmBIRReminder.Show()
                                            Else
                                                frmOption.Show()
                                            End If
                                        Else
                                            frmOption.Show()
                                        End If

                                    Else
                                        If chkShow.Checked = True Then
                                            query = " SELECT  Birfrm, Period, Month, Date , CAST(CONCAT(Month,'-',DAte,'-', DATEPART(yyyy, getdate())) AS Date) AS Deadline, " & _
                                           " DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date)) AS DateDifference " & _
                                           "  FROM    tblTax_Deadline " & _
                                           "  WHERE DATEDIFF(DAY,GETDATE(),CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date)) <= Within " & _
                                           "  AND  GETDATE() <=  CAST(CONCAT(Month,'-',DAte,'-',DATEPART(yyyy, getdate())) AS Date) "

                                            SQL.ReadQuery(query)

                                            If SQL.SQLDR.Read Then
                                                frmBIRReminder.Show()
                                            End If
                                        End If
                                        LoadSetup()
                                        Main_JADE.Show()
                                    End If
                                    Me.Hide()
                                End If
                            End If
                        Else
                            If counter = 3 Then
                                Msg("Invalid Username or password. This is your 3rd attempt to login " & vbNewLine & "Kindly contact your system administrator. The program will now close.", MsgBoxStyle.Exclamation)
                                Application.Exit()
                            End If
                            Msg("Invalid Username or Password!", MsgBoxStyle.Information)
                            txtUsername.Text = ""
                            txtPassword.Text = ""
                            txtUsername.Focus()
                            activityStatus = False
                            counter += 1
                        End If
                        Else
                            If counter = 3 Then
                                Msg("Invalid Username or password. This is your 3rd attempt to login " & vbNewLine & "Kindly contact your system administrator. The program will now close.", MsgBoxStyle.Exclamation)
                                Application.Exit()
                            End If
                            Msg("Invalid Username or Password!", MsgBoxStyle.Information)
                            txtUsername.Text = ""
                            txtPassword.Text = ""
                            txtUsername.Focus()
                            activityStatus = False
                            counter += 1
                        End If
                    End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, vbExclamation, "Message Alert")
            activityStatus = False
        Finally
            If BusinessType <> "" Then
                RecordActivity(UserID, moduleID, Me.Name.ToString, "Login", "UserID", UserName, BusinessType, BranchCode, "", activityStatus)
            End If
        End Try
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT    Inv_Computation" & _
                " FROM      tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Inv_ComputationMethod = SQL.SQLDR("Inv_Computation")
        End If
    End Sub

    Private Sub btnOption_Click(sender As System.Object, e As System.EventArgs) Handles btnOption.Click
        If btnOption.Text = "Options >>" Then
            ShowSettings(True)
        Else
            ShowSettings(False)
        End If
    End Sub

    Private Sub btnConnect_Click(sender As System.Object, e As System.EventArgs) Handles btnConnect.Click
        If HasConnection() Then
            SaveServer()
            Msg("Connected Succesfully!" & vbNewLine & "Please select your database", MsgBoxStyle.Information)
            LoadDatabase()
            ShowSettings(False)
        Else
            Msg("Can't connect to server!" & vbNewLine & "Please check your database connection or contact your network administrator", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs)
        frmModuleSelect.Show()
    End Sub

    Private Sub lnkModuleSelect_LinkClicked(sender As System.Object, e As System.Windows.Forms.LinkLabelLinkClickedEventArgs)
        Me.Dispose()
        frmModuleSelect.Show()
        lsJADE.Dispose()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btnClose.Click
        End
    End Sub

    Private Sub connectServer_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles cbServer.KeyDown, txtPass.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnConnect.PerformClick()
        End If
    End Sub

    Private Sub panelLogin_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles panelLogin.Paint

    End Sub

    Private Sub lblTrial_Click(sender As System.Object, e As System.EventArgs) Handles lblTrial.Click

    End Sub

End Class