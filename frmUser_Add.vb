Imports System.IO
Public Class frmUser_Add
    Public User_ID As Integer
    Public Type As String
    Public password As String
    Dim ModuleID As String = "UAR"
    Dim sigPath As String

    Public Overloads Function ShowDialog(ByVal User As Integer) As Boolean
        User_ID = User
        MyBase.ShowDialog()
        Return True
    End Function


    Private Sub frmUser_Add_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ClearText()
        LoadUserLevel()
        If Type = "ADD" Then
            EnableControl(True)
            txtOldPass.Visible = False
            lblOldPass.Visible = False
        ElseIf Type = "CHANGE PASS" Then
            If User_ID <> 0 Then
                LoadUser(User_ID)
                txtOldPass.Visible = True
                lblOldPass.Visible = True
                EnableControl(True)
            End If
        ElseIf Type = "CHANGE SIGNATURE" Then
            If User_ID <> 0 Then
                LoadUser(User_ID)
                txtOldPass.Visible = False
                lblOldPass.Visible = False
                EnableControl(True)
            End If
        Else
            txtOldPass.Visible = False
            lblOldPass.Visible = False
            EnableControl(True)
        End If
    End Sub

    Protected Sub LoadUser(User_ID)

        activityStatus = True
        Dim query As String
        query = " SELECT UserName, LoginName, Password, UserLevel, RefID, EmailAddress, ContactNo, Signature, Position " & _
              " FROM tblUser WHERE UserID = '" & User_ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtName.Text = SQL.SQLDR("UserName").ToString
            txtUsername.Text = SQL.SQLDR("LoginName").ToString
            password = SQL.SQLDR("Password").ToString
            txtOldPass.Text = password
            cbUserLevel.SelectedItem = SQL.SQLDR("UserLevel").ToString
            txtID.Text = SQL.SQLDR("RefID").ToString
            txtEmail.Text = SQL.SQLDR("EmailAddress").ToString
            txtContact.Text = SQL.SQLDR("ContactNo").ToString
            If Not IsDBNull(SQL.SQLDR("Signature")) Then
                pbSignature.Image = New Bitmap(byteArrayToImage(SQL.SQLDR("Signature")))
                pbSignature.SizeMode = PictureBoxSizeMode.StretchImage
            Else
                LoadDefaultImage("Signature")
            End If
            txtPosition.Text = SQL.SQLDR("Position").ToString
        End If

    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)

        If Type = "ADD" Then
            cbUserLevel.Enabled = True
            txtID.Enabled = True
            txtName.Enabled = True
            txtEmail.Enabled = True
            txtContact.Enabled = True
            txtUsername.Enabled = True
            txtPassword.Enabled = True
            txtConfirmPass.Enabled = True
            txtOldPass.Enabled = True
            btnSearchVCE.Enabled = True
        ElseIf Type = "CHANGE PASS" Then
            If User_ID <> 0 Then
                cbUserLevel.Enabled = True
                txtID.Enabled = False
                txtName.Enabled = False
                txtEmail.Enabled = True
                txtContact.Enabled = True
                txtUsername.Enabled = False
                txtPassword.Enabled = True
                txtConfirmPass.Enabled = True
                txtOldPass.Enabled = False
                btnSearchVCE.Enabled = False
            ElseIf Type = "CHANGE SIGNATURE" Then
                If User_ID <> 0 Then
                    cbUserLevel.Enabled = True
                    txtID.Enabled = False
                    txtName.Enabled = False
                    txtEmail.Enabled = True
                    txtContact.Enabled = True
                    btnUploadSig.Enabled = True
                    txtUsername.Enabled = False
                    txtPassword.Enabled = False
                    txtConfirmPass.Enabled = False
                    txtOldPass.Enabled = False
                    btnSearchVCE.Enabled = True
                End If
            End If

        End If
    End Sub

    Private Sub ClearText()
        cbUserLevel.SelectedIndex = -1
        txtID.Clear()
        txtName.Clear()
        txtEmail.Clear()
        txtContact.Clear()
        txtUsername.Clear()
        txtPassword.Clear()
        txtConfirmPass.Clear()
        txtOldPass.Clear()

    End Sub

    Private Sub LoadUserLevel()
        Dim query As String
        query = " SELECT DISTINCT UserLevel FROM tblUser_Level WHERE UserLevel <> 'Master Admin' "
        SQL.ReadQuery(query)
        cbUserLevel.Items.Clear()
        While SQL.SQLDR.Read
            cbUserLevel.Items.Add(SQL.SQLDR("UserLevel").ToString)
        End While
        If cbUserLevel.Items.Count > 0 Then
            cbUserLevel.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Type = "ADD" Then
                If MessageBox.Show("Are you sure you want to Save this User?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If txtName.Text = "" Or txtPassword.Text = "" Or txtUsername.Text = "" Then
                        msgRequired()
                    ElseIf txtPassword.Text <> txtConfirmPass.Text Then
                        MsgBox("Password confirmation doesn't match!", MsgBoxStyle.Exclamation)
                    ElseIf UserExist() Then
                        MsgBox("Username already exist! Please choose another.", MsgBoxStyle.Exclamation)
                        txtUsername.Focus()
                    ElseIf txtPassword.TextLength < 6 Then
                        MsgBox("Password with less than 6 character is not acceptable, it is considered a weak Password.", vbInformation)
                    Else
                        SaveUser()
                        msgsave()
                        Me.Close()
                    End If
                End If
            ElseIf Type = "CHANGE PASS" Then
                If MessageBox.Show("Are you sure you want to Update this User?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If txtPassword.Text = "" Then
                        msgRequired()
                    ElseIf txtPassword.Text <> txtConfirmPass.Text Then
                        MsgBox("Password confirmation doesn't match!", MsgBoxStyle.Exclamation)
                        txtUsername.Focus()
                    ElseIf txtOldPass.Text <> password Then
                        MsgBox("Old password is invalid!", MsgBoxStyle.Exclamation)
                        txtUsername.Focus()
                    ElseIf txtPassword.TextLength < 6 Then
                        MsgBox("Password with less than 6 character is not acceptable, it is considered a weak Password.", vbInformation)
                    Else
                        UpdateUser()
                        msgupdated()
                        Me.Close()
                    End If
                End If
            ElseIf Type = "CHANGE SIGNATURE" Then
                If MessageBox.Show("Are you sure you want to Update this User?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    If txtID.Text = "" Then
                        msgRequired()
                    Else
                        UpdateUser()
                        msgupdated()
                        Me.Close()
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Sub SaveUser()
        Try
            ' HASH PASSWORD
            Dim hash As String = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, WorkFactor)
            activityStatus = True
            Dim insertSQL, query As String
            insertSQL = " INSERT INTO " & _
                  " tblUser (UserName, LoginName, Password, UserLevel, RefID, EmailAddress, ContactNo, WhoCreated, Signature, Position) " & _
                  " VALUES (@UserName, @LoginName, @Password, @UserLevel, @RefID, @EmailAddress, @ContactNo, @WhoCreated, @Signature, @Position)"
            SQL.AddParam("@UserName", txtName.Text)
            SQL.AddParam("@LoginName", txtUsername.Text)
            SQL.AddParam("@Password", hash)
            SQL.AddParam("@UserLevel", cbUserLevel.SelectedItem)
            SQL.AddParam("@RefID", txtID.Text)
            SQL.AddParam("@EmailAddress", txtEmail.Text)
            SQL.AddParam("@ContactNo", txtContact.Text)
            SQL.AddParam("@WhoCreated", UserID)

            'SAVE SIGNATURE
            Dim imgStreamSig As MemoryStream = New MemoryStream()
            If sigPath <> "" AndAlso My.Computer.FileSystem.FileExists(sigPath) Then
                Image.FromFile(sigPath).Save(imgStreamSig, System.Drawing.Imaging.ImageFormat.Jpeg)
            Else
                Dim imgSig As Image
                imgSig = pbSignature.Image
                imgSig.Save(imgStreamSig, System.Drawing.Imaging.ImageFormat.Png)
            End If
            imgStreamSig.Close()
            Dim byteArraySig As Byte() = imgStreamSig.ToArray()
            SQL.AddParam("@Signature", byteArraySig, SqlDbType.Image)
            SQL.AddParam("@Position", txtPosition.Text)
            SQL.ExecNonQuery(insertSQL)

            Dim NewUser_ID As Integer
            query = " SELECT  UserID, LoginName, Password, UserLevel, Status, FirstLogin, " & _
                           "         UserName AS Name " & _
                           " FROM    tblUser " & _
                           " WHERE   LoginName = '" & txtUsername.Text & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Dim hashPW As String = SQL.SQLDR("Password")
                If BCrypt.Net.BCrypt.Verify(txtPassword.Text, hashPW) Then
                    NewUser_ID = SQL.SQLDR("UserID").ToString()

                    query = " SELECT Type, Code, isAllowed, Status FROM tblDefaultAccess " & _
                            " WHERE isAllowed = 1 AND UserLevel = '" & cbUserLevel.SelectedItem & "' AND Status = 'Active' "
                    SQL.ReadQuery(query)
                    While SQL.SQLDR.Read
                        Dim Type As String = SQL.SQLDR("Type").ToString()
                        Dim Code As String = SQL.SQLDR("Code").ToString()
                        Dim Status As String = SQL.SQLDR("Status").ToString()
                        Dim isAllowed As Boolean = SQL.SQLDR("isAllowed").ToString()

                        insertSQL = " INSERT INTO " & _
                              " tblUser_Access (UserID, Type, Code, isAllowed, Status) " & _
                              " VALUES(@UserID, @Type, @Code, @isAllowed, @Status) "
                        SQL.AddParam("@UserID", NewUser_ID)
                        SQL.AddParam("@Type", Type)
                        SQL.AddParam("@Code", Code)
                        SQL.AddParam("@isAllowed", isAllowed)
                        SQL.AddParam("@Status", Status)
                        SQL.ExecNonQuery(insertSQL)
                    End While
                End If
            End If
        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "INSERT", "UserID", User_ID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try

    End Sub

    Protected Sub UpdateUser()
        Try
            activityStatus = True
            Dim hash As String = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text, WorkFactor)
            Dim updateSQL As String

            If Type = "CHANGE SIGNATURE" Then
                updateSQL = " UPDATE tblUser " & _
                       " SET    UserName = @UserName, LoginName = @LoginName, " & _
                       "        UserLevel = @UserLevel, RefID = @RefID, EmailAddress = @EmailAddress, ContactNo = @ContactNo, " & _
                       "        WhoModified = @WhoModified, DateModified = GETDATE(), Signature = @Signature, Position = @Position " & _
                       " WHERE  UserID = @UserID "
                SQL.AddParam("@UserID", User_ID)
                SQL.AddParam("@UserName", txtName.Text)
                SQL.AddParam("@LoginName", txtUsername.Text)
                SQL.AddParam("@UserLevel", cbUserLevel.SelectedItem)
                SQL.AddParam("@RefID", txtID.Text)
                SQL.AddParam("@EmailAddress", txtEmail.Text)
                SQL.AddParam("@ContactNo", txtContact.Text)
                SQL.AddParam("@WhoModified", UserID)

                ' SAVE SIGNATURE

                Dim imgStreamSig As MemoryStream = New MemoryStream()
                If sigPath <> "" AndAlso My.Computer.FileSystem.FileExists(sigPath) Then
                    Image.FromFile(sigPath).Save(imgStreamSig, System.Drawing.Imaging.ImageFormat.Jpeg)
                Else
                    Dim imgSig As Image
                    imgSig = pbSignature.Image
                    imgSig.Save(imgStreamSig, System.Drawing.Imaging.ImageFormat.Png)
                End If
                imgStreamSig.Close()
                Dim byteArraySig As Byte() = imgStreamSig.ToArray()
                SQL.AddParam("@Signature", byteArraySig, SqlDbType.Image)
                SQL.AddParam("@Position", txtPosition.Text)

                SQL.ExecNonQuery(updateSQL)
            Else
                updateSQL = " UPDATE tblUser " & _
                        " SET    UserName = @UserName, LoginName = @LoginName, Password = @Password, FirstLogin = @FirstLogin, " & _
                        "        UserLevel = @UserLevel, RefID = @RefID, EmailAddress = @EmailAddress, ContactNo = @ContactNo, " & _
                        "        WhoModified = @WhoModified, DateModified = GETDATE(), Signature = @Signature, Position = @Position " & _
                        " WHERE  UserID = @UserID "
                SQL.AddParam("@UserID", User_ID)
                SQL.AddParam("@UserName", txtName.Text)
                SQL.AddParam("@LoginName", txtUsername.Text)
                SQL.AddParam("@Password", hash)
                SQL.AddParam("@FirstLogin", True)
                SQL.AddParam("@UserLevel", cbUserLevel.SelectedItem)
                SQL.AddParam("@RefID", txtID.Text)
                SQL.AddParam("@EmailAddress", txtEmail.Text)
                SQL.AddParam("@ContactNo", txtContact.Text)
                SQL.AddParam("@WhoModified", UserID)
                ' SAVE SIGNATURE

                Dim imgStreamSig As MemoryStream = New MemoryStream()
                If sigPath <> "" AndAlso My.Computer.FileSystem.FileExists(sigPath) Then
                    Image.FromFile(sigPath).Save(imgStreamSig, System.Drawing.Imaging.ImageFormat.Jpeg)
                Else
                    Dim imgSig As Image
                    imgSig = pbSignature.Image
                    imgSig.Save(imgStreamSig, System.Drawing.Imaging.ImageFormat.Png)
                End If
                imgStreamSig.Close()
                Dim byteArraySig As Byte() = imgStreamSig.ToArray()
                SQL.AddParam("@Signature", byteArraySig, SqlDbType.Image)
                SQL.AddParam("@Position", txtPosition.Text)
                SQL.ExecNonQuery(updateSQL)
            End If

        Catch ex As Exception
            activityStatus = False
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
        Finally
            RecordActivity(UserID, ModuleID, Me.Name.ToString, "UPDATE", "UserID", User_ID, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Sub txtUsername_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUsername.GotFocus
        txtUsername.SelectionStart = 0
        txtUsername.SelectionLength = Len(txtUsername.Text)
    End Sub

    Private Function UserExist() As Boolean
        Dim query As String
        Try
            query = "SELECT LoginName FROM tblUser WHERE LoginName = @LoginName "
            SQL.FlushParams()
            SQL.AddParam("@LoginName", txtUsername.Text)
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, ModuleID)
            Return False
        Finally
            SQL.FlushParams()
        End Try
    End Function

    Private Sub btnSearchVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnSearchVCE.Click
        Dim f As New frmVCE_Search
        f.ShowDialog()
        LoadUserInfo(f.VCECode)
        f.Dispose()
    End Sub

    Private Sub LoadUserInfo(ByVal ID As String)
        Dim query As String
        query = " SELECT    VCECode, VCEName  " & _
                " FROM      viewVCE_Master " & _
                " WHERE     VCECode ='" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtID.Text = SQL.SQLDR("VCECode").ToString
            txtName.Text = SQL.SQLDR("VCEName").ToString
        Else

            txtID.Clear()
            txtName.Clear()
            txtEmail.Clear()
            txtContact.Clear()
        End If
    End Sub

    Private Sub btnUploadSig_Click(sender As System.Object, e As System.EventArgs) Handles btnUploadSig.Click
        With OpenFileDialog1
            .InitialDirectory = "C:\"
            .Filter = "All Files|*.*|Bitmaps|*.bmp|GIFs|*.gif|JPEGs|*.jpg"
            .FilterIndex = 4
        End With
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            With pbSignature
                .Image = Image.FromFile(OpenFileDialog1.FileName)
                .SizeMode = PictureBoxSizeMode.StretchImage
                .BorderStyle = BorderStyle.Fixed3D
                sigPath = OpenFileDialog1.FileName
            End With
        End If
    End Sub

    Private Sub btnVCE_Click(sender As System.Object, e As System.EventArgs) Handles btnVCE.Click
        frmUser_Level_Maintenance.Show()
        frmUser_Level_Maintenance.Select()
    End Sub
End Class