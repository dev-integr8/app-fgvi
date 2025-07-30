Public Class frmBranch
    Dim TransID As String

    Private Sub tsbSave_Click(sender As System.Object, e As System.EventArgs) Handles tsbSave.Click
        Try
            If txtBranchCode.Text = "" Then
                MsgBox("Please Enter Branch Code!", MsgBoxStyle.Exclamation)
            ElseIf txtBranchName.Text = "" Then
                MsgBox("Please Enter Branch Name!", MsgBoxStyle.Exclamation)
            ElseIf IfExist(txtBranchCode.Text) And TransID = "" Then
                MsgBox("BranchCode" & " " & txtBranchCode.Text & " already exist!", MsgBoxStyle.Exclamation)
            ElseIf TransID = "" Then
                If MsgBox("Saving New Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                    SaveBranch()
                    Msg("Record Saved Succesfully!", MsgBoxStyle.Information)
                    loadBranch()
                    ClearText()
                    EnableControl(False)
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbDelete.Enabled = False
                    tsbClose.Enabled = False
                    tsbExit.Enabled = True
                End If
            Else
                If MsgBox("Updating Record, Click Yes to confirm", MsgBoxStyle.Information + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then

                    UpdateBranch()
                    Msg("Record Updated Succesfully!", MsgBoxStyle.Information)
                    loadBranch()
                    ClearText()
                    EnableControl(False)
                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbDelete.Enabled = False
                    tsbClose.Enabled = False
                    tsbExit.Enabled = True
                End If
            End If
        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "BranchCode")
        End Try
    End Sub

    Private Sub SaveBranch()
        Dim SQL1 As New SQLControl
        SQL1.BeginTransaction()
        Try
            Dim insertSQL As String
            activityStatus = True
            insertSQL = " INSERT INTO " & _
                        " tblBranch (BranchCode, Description, Branch_TIN, Branch_Code, Region, Province, " & _
                        " RegisteredAddress, COR_No, COR_Date, LatestAmendment_No, LatestAmendment_Date, With_COE, " & _
                        " COE_No, COE_DateIssued, COE_Status, COE_DateStart, COE_DateEnd, WhoCreated) " & _
                        " VALUES (@BranchCode, @Description, @Branch_TIN, @Branch_Code, @Region, @Province, " & _
                        " @RegisteredAddress, @COR_No, @COR_Date, @LatestAmendment_No, @LatestAmendment_Date, @With_COE, " & _
                        " @COE_No, @COE_DateIssued, @COE_Status, @COE_DateStart, @COE_DateEnd, @WhoCreated)"
            SQL1.FlushParams()
            SQL1.AddParam("@BranchCode", txtBranchCode.Text)
            SQL1.AddParam("@Description", txtBranchName.Text)
            SQL1.AddParam("@Branch_TIN", txtTinNo.Text)
            SQL1.AddParam("@Branch_Code", txtBranchCode.Text)
            SQL1.AddParam("@Region", txtRegion.Text)
            SQL1.AddParam("@Province", txtProvince.Text)
            SQL1.AddParam("@RegisteredAddress", txtAddress.Text)
            SQL1.AddParam("@COR_No", txtCOR_No.Text)
            SQL1.AddParam("@COR_Date", dtpRegDate.Value)
            SQL1.AddParam("@LatestAmendment_No", txtLatestAmendmentNo.Text)
            SQL1.AddParam("@LatestAmendment_Date", dtpLastDateAmendment.Value)
            SQL1.AddParam("@With_COE", chkwCTE.Checked)
            SQL1.AddParam("@COE_No", txtCTENo.Text)
            SQL1.AddParam("@COE_DateIssued", dtpDateIssue.Value)
            SQL1.AddParam("@COE_Status", "Active")
            SQL1.AddParam("@COE_DateStart", dtpStartEffectDate.Value)
            SQL1.AddParam("@COE_DateEnd", dtpEndEffectDate.Value)
            SQL1.AddParam("@WhoCreated", UserID)
            SQL1.ExecNonQuery(insertSQL)

            SQL1.Commit()
        Catch ex As Exception
            SQL1.Rollback()
            TransID = ""
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, "BranchCode", Me.Name.ToString, "INSERT", "BranchCode", txtBranchCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL1.FlushParams()
        End Try
    End Sub

    Private Sub UpdateBranch()
        Dim SQL As New SQLControl
        SQL.BeginTransaction()
        Try
            Dim updateSQL As String
            activityStatus = True

            updateSQL = " UPDATE tblBranch  " & _
                        " SET    Description = @Description, Branch_TIN = @Branch_TIN, Branch_Code = @Branch_Code, Region = @Region, Province = @Province," & _
                        "        RegisteredAddress = @RegisteredAddress, COR_No = @COR_No, COR_Date = @COR_Date, LatestAmendment_No = @LatestAmendment_No, " & _
                        "       LatestAmendment_Date = @LatestAmendment_Date, With_COE = @With_COE, COE_No = @COE_No, COE_DateIssued = @COE_DateIssued, WhoModified = @WhoModified, DateModified = GETDATE(), " & _
                        "        COE_Status = @COE_Status, COE_DateStart = @COE_DateStart, COE_DateEnd = @COE_DateEnd" & _
                        " WHERE BranchCode = @BranchCode "
            SQL.FlushParams()
            SQL.AddParam("@BranchCode", TransID)
            SQL.AddParam("@Description", txtBranchName.Text)
            SQL.AddParam("@Branch_TIN", txtTinNo.Text)
            SQL.AddParam("@Branch_Code", txtBranchCode.Text)
            SQL.AddParam("@Region", txtRegion.Text)
            SQL.AddParam("@Province", txtProvince.Text)
            SQL.AddParam("@RegisteredAddress", txtAddress.Text)
            SQL.AddParam("@COR_No", txtCOR_No.Text)
            SQL.AddParam("@COR_Date", dtpRegDate.Value)
            SQL.AddParam("@LatestAmendment_No", txtLatestAmendmentNo.Text)
            SQL.AddParam("@LatestAmendment_Date", dtpLastDateAmendment.Value)
            SQL.AddParam("@With_COE", chkwCTE.Checked)
            SQL.AddParam("@COE_No", txtCTENo.Text)
            SQL.AddParam("@COE_DateIssued", dtpDateIssue.Value)
            SQL.AddParam("@COE_Status", "Active")
            SQL.AddParam("@COE_DateStart", dtpStartEffectDate.Value)
            SQL.AddParam("@COE_DateEnd", dtpEndEffectDate.Value)
            SQL.AddParam("@WhoModified", UserID)
            SQL.ExecNonQuery(updateSQL)

            SQL.Commit()
        Catch ex As Exception
            SQL.Rollback()
            activityStatus = False
            Throw New Exception(ex.Message, ex)
        Finally
            RecordActivity(UserID, "BranchCode", Me.Name.ToString, "UPDATE", "BranchCode", txtBranchCode.Text, BusinessType, BranchCode, "", activityStatus)
            SQL.FlushParams()
        End Try
    End Sub

    Private Function IfExist(ByVal ID As String) As Boolean
        Dim query As String
        query = " SELECT * FROM tblBranch WHERE BranchCode ='" & ID & "'  "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub frmBranch_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
            loadBranch()
            If TransID <> "" Then
                    LoadData(TransID)
            Else
                tsbNew.Enabled = True
                tsbEdit.Enabled = False
                tsbSave.Enabled = False
                tsbDelete.Enabled = False
                tsbClose.Enabled = False
                tsbExit.Enabled = True
                EnableControl(False)
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "Branch")
        End Try
    End Sub

    Private Sub loadBranch()
        Dim query As String
        query = " SELECT   BranchCode, Description, Main  " & _
                " FROM     tblBranch " & _
                " WHERE    Status = 'Active' "
        SQL.ReadQuery(query)
        dgvBranch.Rows.Clear()
        While SQL.SQLDR.Read
            dgvBranch.Rows.Add(SQL.SQLDR("BranchCode").ToString, SQL.SQLDR("Description").ToString)
        End While
    End Sub

    Private Sub dgvBranch_CellClick(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBranch.CellClick
        If dgvBranch.SelectedRows.Count = 1 Then
            TransID = dgvBranch.SelectedRows(0).Cells(0).Value.ToString
            If TransID <> "" Then
                LoadData(TransID)
            End If
        End If
    End Sub

    Private Sub dgvBranch_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvBranch.CellContentClick

    End Sub

    Private Sub LoadData(ByVal ID As String)

        Dim query As String
        query = " SELECT        BranchCode, Description,   " & _
                "               Branch_TIN, Branch_Code, Region, Province, RegisteredAddress, COR_No, ISNULL(COR_Date,'1900-01-01') AS COR_Date, LatestAmendment_No,  " & _
                "                          ISNULL(LatestAmendment_Date,'1900-01-01') AS LatestAmendment_Date, With_COE, COE_No, ISNULL(COE_DateIssued,'1900-01-01') AS COE_DateIssued, " & _
                " COE_Status, ISNULL(COE_DateStart,'1900-01-01') AS  COE_DateStart, ISNULL(COE_DateEnd,'1900-01-01') AS COE_DateEnd, Status " & _
                " FROM            dbo.tblBranch " & _
                " WHERE   BranchCode = '" & ID & "' "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TransID = SQL.SQLDR("BranchCode").ToString
            txtBranchCode.Text = SQL.SQLDR("BranchCode").ToString
            txtBranchName.Text = SQL.SQLDR("Description").ToString
            txtTinNo.Text = SQL.SQLDR("Branch_TIN").ToString
            txtRegion.Text = SQL.SQLDR("Region").ToString
            txtProvince.Text = SQL.SQLDR("Province").ToString
            txtAddress.Text = SQL.SQLDR("RegisteredAddress").ToString
            txtCOR_No.Text = SQL.SQLDR("COR_No").ToString
            dtpRegDate.Value = SQL.SQLDR("COR_Date").ToString
            txtLatestAmendmentNo.Text = SQL.SQLDR("LatestAmendment_No").ToString
            dtpLastDateAmendment.Value = SQL.SQLDR("LatestAmendment_Date").ToString
            chkwCTE.Checked = SQL.SQLDR("With_COE").ToString
            txtCTENo.Text = SQL.SQLDR("COE_No").ToString
            dtpDateIssue.Value = SQL.SQLDR("COE_DateIssued").ToString
            dtpStartEffectDate.Value = SQL.SQLDR("COE_DateStart").ToString
            dtpEndEffectDate.Value = SQL.SQLDR("COE_DateEnd").ToString



            ' TOOLSTRIP BUTTONS
            If SQL.SQLDR("Status").ToString = "Inactive" Then
                tsbEdit.Enabled = False
                tsbDelete.Enabled = True
            Else
                tsbEdit.Enabled = True
                tsbDelete.Enabled = True
            End If
            loadBusinessType(TransID)
            tsbClose.Enabled = True
            tsbSave.Enabled = False
            tsbNew.Enabled = True
            tsbExit.Enabled = True
            EnableControl(False)
        Else
            ClearText()
        End If
    End Sub

    Private Sub loadBusinessType(ByVal ID As String)
        Dim query As String
        query = " SELECT   Description, CASE WHEN MemberOnly = 1 THEN  'Members Only' ELSE 'Members and Non Members' END AS MemberOnly " & _
                " FROM     tblBusinessType " & _
                " WHERE    Status = 'Active' And BranchCode = '" & ID & "' "
        SQL.ReadQuery(query)
        dgvBusinessType.Rows.Clear()
        While SQL.SQLDR.Read
            dgvBusinessType.Rows.Add(SQL.SQLDR("Description").ToString, SQL.SQLDR("MemberOnly").ToString)
        End While
    End Sub


    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub tsbClose_Click(sender As System.Object, e As System.EventArgs) Handles tsbClose.Click

        ' Toolstrip Buttons
        If TransID = "" Then
            ClearText()
            EnableControl(False)
            tsbEdit.Enabled = False
            tsbDelete.Enabled = False
        Else
            LoadData(TransID)
            tsbEdit.Enabled = True
            tsbDelete.Enabled = False
        End If
        tsbNew.Enabled = True
        tsbSave.Enabled = False
        tsbClose.Enabled = False
        tsbExit.Enabled = True
    End Sub

    Private Sub tsbDelete_Click(sender As System.Object, e As System.EventArgs) Handles tsbDelete.Click
        If TransID <> "" Then
            If MsgBox("Are you sure you want to cancel this record?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "GR8 Message Alert") = MsgBoxResult.Yes Then
                Try

                    Dim updateSQL As String
                    updateSQL = " UPDATE  tblBranch SET Status ='Inactive' WHERE BranchCode = @BranchCode "
                    SQL.FlushParams()
                    SQL.AddParam("@BranchCode", TransID)
                    SQL.ExecNonQuery(updateSQL)

                    tsbNew.Enabled = True
                    tsbEdit.Enabled = False
                    tsbSave.Enabled = False
                    tsbDelete.Enabled = False
                    tsbClose.Enabled = False
                    tsbExit.Enabled = True
                    'EnableControl(False)

                    LoadData(TransID)
                Catch ex As Exception
                    activityStatus = True
                    SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, "BranchCode")
                Finally
                    RecordActivity(UserID, "BranchCode", Me.Name.ToString, "CANCEL", "BranchCode", TransID, BusinessType, BranchCode, "", activityStatus)
                    SQL.FlushParams()
                End Try
            End If
        End If
    End Sub

    Private Sub tsbNew_Click(sender As System.Object, e As System.EventArgs) Handles tsbNew.Click
        ClearText()
        TransID = ""

        ' Toolstrip Buttons
        tsbNew.Enabled = False
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbDelete.Enabled = False
        tsbClose.Enabled = True
        tsbExit.Enabled = False
        EnableControl(True)
    End Sub


    Private Sub ClearText()
        txtBranchCode.Text = ""
        txtBranchName.Text = ""
        txtTinNo.Text = ""
        txtRegion.Text = ""
        txtProvince.Text = ""
        txtAddress.Text = ""
        txtCOR_No.Text = ""
        dtpRegDate.Value = Date.Today.Date
        txtLatestAmendmentNo.Text = ""
        dtpLastDateAmendment.Value = Date.Today.Date
        chkwCTE.Checked = False
        txtCTENo.Text = ""
        dtpDateIssue.Value = Date.Today.Date
        dtpStartEffectDate.Value = Date.Today.Date
        dtpEndEffectDate.Value = Date.Today.Date
        dgvBusinessType.Rows.Clear()
    End Sub

    Private Sub EnableControl(ByVal Value As Boolean)
        If TransID = "" Then
            txtBranchCode.Enabled = Value
        Else
            txtBranchCode.Enabled = False
        End If
        txtBranchName.Enabled = Value
        txtTinNo.Enabled = Value
        txtRegion.Enabled = Value
        txtProvince.Enabled = Value
        txtAddress.Enabled = Value
        txtCOR_No.Enabled = Value
        dtpRegDate.Enabled = Value
        txtLatestAmendmentNo.Enabled = Value
        dtpLastDateAmendment.Enabled = Value
        chkwCTE.Enabled = Value
        txtCTENo.Enabled = Value
        dtpDateIssue.Enabled = Value
        dtpStartEffectDate.Enabled = Value
        dtpEndEffectDate.Enabled = Value


        dgvBusinessType.AllowUserToDeleteRows = Value
        dgvBusinessType.ReadOnly = Not Value
        If Value = True Then
            dgvBusinessType.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
        Else
            dgvBusinessType.EditMode = DataGridViewEditMode.EditProgrammatically
        End If
    End Sub

    Private Sub tsbEdit_Click(sender As System.Object, e As System.EventArgs) Handles tsbEdit.Click

        EnableControl(True)

        ' Toolstrip Buttons
        tsbNew.Enabled = False
        tsbEdit.Enabled = False
        tsbSave.Enabled = True
        tsbClose.Enabled = True
        tsbExit.Enabled = False
    End Sub
End Class