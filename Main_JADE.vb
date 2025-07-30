Imports System.Windows.Forms
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security
Imports System.Security.Principal
Imports System.Net.NetworkInformation

Public Class Main_JADE
    Public Overloads Function ShowDialog(ByVal DTBSE As String, ByVal IPADD As String) As Boolean
        TxtDatabase.Text = DTBSE
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub OpenFile(ByVal sender As Object, ByVal e As EventArgs)
        Dim OpenFileDialog As New OpenFileDialog
        OpenFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        If (OpenFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = OpenFileDialog.FileName
            ' TODO: Add code here to open the file.
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles btnMasterfile.Click
        mtcMenu.SelectedTab = MetroTabPage1
        btnMasterfile.BackColor = Color.Blue
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub



    Private Sub btnCollection_Click(sender As System.Object, e As System.EventArgs) Handles btnCollection.Click
        mtcMenu.SelectedTab = MetroTabPage3
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Blue
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub btnSales_Click(sender As System.Object, e As System.EventArgs) Handles btnSales.Click
        mtcMenu.SelectedTab = MetroTabPage4
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Blue
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub


    Private Sub btnGeneralJournal_Click(sender As System.Object, e As System.EventArgs) Handles btnGeneralJournal.Click
        mtcMenu.SelectedTab = MetroTabPage6
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Blue
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub btnDisbursement_Click(sender As System.Object, e As System.EventArgs) Handles btnDisbursement.Click
        mtcMenu.SelectedTab = MetroTabPage7
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Blue
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub btnPosting_Click(sender As System.Object, e As System.EventArgs) Handles btnPosting.Click
        mtcMenu.SelectedTab = MetroTabPage8
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Blue
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
    End Sub

    Private Sub btnReports_Click(sender As System.Object, e As System.EventArgs) Handles btnReports.Click
        mtcMenu.SelectedTab = MetroTabPage9
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Blue
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub btnOtherModules_Click(sender As System.Object, e As System.EventArgs) Handles btnOtherModules.Click
        mtcMenu.SelectedTab = MetroTabPage10
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Blue
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub MetroTile9_Click(sender As System.Object, e As System.EventArgs)
        frmPO.Show()
        frmPO.Select()
    End Sub

    Private Sub MetroTile1_Click(sender As System.Object, e As System.EventArgs) Handles tileVCEMaster.Click
        frmVCE_Master.Show()
        frmVCE_Master.Select()
    End Sub

    Private Sub tileItemMaster_Click(sender As System.Object, e As System.EventArgs) Handles tileItemMaster.Click
        frmItem_Master.Show()
        frmItem_Master.Select()
        'Frm_ItemMaster.Show()
    End Sub

    Private Sub tileChartofAccount_Click(sender As System.Object, e As System.EventArgs) Handles tileChartofAccount.Click
        frmCOA.Show()
        frmCOA.Select()
    End Sub

    Private Sub tileBankList_Click(sender As System.Object, e As System.EventArgs) Handles tileBankList.Click
        frmMasterfile_Bank.Show()
        frmMasterfile_Bank.Select()
    End Sub

    Private Sub tileUserMaster_Click(sender As System.Object, e As System.EventArgs) Handles tileUserMaster.Click
        If Not AllowAccess("UAR_VIEW") Then
            msgRestrictedMod()
        Else
            frmUser_Master.Show()
            frmUser_Master.Select()
        End If

    End Sub

    Private Sub tileWarehouseMaster_Click(sender As System.Object, e As System.EventArgs) Handles tileWarehouseMaster.Click
        frmWH.Show()
        frmWH.Select()
    End Sub

    Private Sub tileBillsofMaterials_Click(sender As System.Object, e As System.EventArgs)
        frmBOM_FG.Show()
        frmBOM_FG.Select()
    End Sub

    Private Sub MainForm1_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'To avoid borderless form cover the taskbar
        Me.WindowState = FormWindowState.Normal
        Me.StartPosition = FormStartPosition.Manual
        With Screen.PrimaryScreen.WorkingArea
            Me.SetBounds(.Left, .Top, .Width, .Height)
        End With

        mtcMenu.SelectedTab = MetroHomeTab

    End Sub

    Private Sub MetroTile8_Click(sender As System.Object, e As System.EventArgs)
        frmPR.Show()
        frmPR.Select()
    End Sub

    Private Sub MetroTile10_Click(sender As System.Object, e As System.EventArgs) Handles tileAccountsPayableVoucher.Click
        frmAPV.Show()
        frmAPV.Select()
    End Sub

    Private Sub tileOR_Click(sender As System.Object, e As System.EventArgs) Handles tileOfficialReceipt.Click
        frmCollection.TransType = "OR"
        frmCollection.Book = "Cash Receipts"
        frmCollection.Show()
        frmCollection.Select()
    End Sub

    Private Sub tileCollectionReceipt_Click(sender As System.Object, e As System.EventArgs)
        frmCollection.TransType = "CR"
        frmCollection.Book = "Cash Receipts"
        frmCollection.Show()
        frmCollection.Select()
    End Sub

    Private Sub tileBankDeposit_Click(sender As System.Object, e As System.EventArgs) Handles tileBankDeposit.Click
        frmBD.Show()
        frmBD.Select()
    End Sub

    Private Sub tileAcknowledgementReceipt_Click(sender As System.Object, e As System.EventArgs) Handles tileAcknowledgementReceipt.Click
        frmCollection.TransType = "AR"
        frmCollection.Book = "Cash Receipts"
        frmCollection.Show()
        frmCollection.Select()
    End Sub

    Private Sub tileBankRecon_Click(sender As System.Object, e As System.EventArgs) Handles tileBankRecon.Click
        FrmBR.Show()
        FrmBR.Select()
    End Sub

    Private Sub tileSalesOrder_Click(sender As System.Object, e As System.EventArgs)
        frmSO.Show()
        frmSO.Select()
    End Sub

    Private Sub tileSalesInvoice_Click(sender As System.Object, e As System.EventArgs) Handles tileSalesInvoice.Click
        frmSI.Show()
        frmSI.Select()
    End Sub



    Private Sub tileDeliveryReceipt_Click(sender As System.Object, e As System.EventArgs)
        frmDR.Show()
        frmDR.Select()
    End Sub

    Private Sub tileInventoryTransfer_Click(sender As System.Object, e As System.EventArgs) Handles tileIT.Click
        frmIT.Show()
        frmIT.Select()
    End Sub

    Private Sub tileJournalVoucher_Click(sender As System.Object, e As System.EventArgs) Handles tileJournalVoucher.Click
        frmJV.Show()
        frmJV.Select()
    End Sub

    Private Sub tileCashDisbursement_Click(sender As System.Object, e As System.EventArgs) Handles tileCashDisbursement.Click
        frmCV.Show()
        frmCV.Select()
    End Sub

    Private Sub tileAdvances_Click(sender As System.Object, e As System.EventArgs) Handles tileAdvances.Click
        frmAdvances.Show()
        frmAdvances.Select()
    End Sub

    Private Sub tileBatchPosting_Click(sender As System.Object, e As System.EventArgs) Handles tileBatchPosting.Click
        frmPosting_Main.Show()
        frmPosting_Main.Select()
    End Sub

    Private Sub picLogout_Click(sender As System.Object, e As System.EventArgs) Handles picLogout.Click
        Dim myForms As FormCollection = Application.OpenForms
        Dim listBox As New ListBox
        For Each frmName As Form In myForms
            If frmName IsNot SplashScreen AndAlso frmName IsNot frmUserLogin AndAlso frmName IsNot Me Then
                listBox.Items.Add(frmName.Name.ToString)
            End If
        Next
        If listBox.Items.Count > 1 Then
            If MsgBox("There are still opened forms, Are you sure you want to logout?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                For Each item In listBox.Items
                    If item.ToString <> "" AndAlso item.ToString <> "MainForm" AndAlso item.ToString <> "LoadingScreen" Then
                        Dim myForm As Form = Application.OpenForms(item.ToString)
                        myForm.Close()

                    End If
                Next
                frmUserLogin.Show()
                frmUserLogin.txtPassword.Text = ""
                Me.Close()
            End If
        Else
            frmUserLogin.Show()
            frmUserLogin.txtPassword.Text = ""
            Me.Close()
        End If
        SQL.CloseCon()
    End Sub

    Private Sub picLogout_MouseEnter(sender As Object, e As System.EventArgs) Handles picLogout.MouseHover
        picLogout.BackColor = Color.Blue
    End Sub

    Private Sub picLogout_MouseLeave(sender As Object, e As System.EventArgs) Handles picLogout.MouseLeave
        picLogout.BackColor = Color.Transparent
    End Sub

    Private Sub picSettings_MouseEnter(sender As Object, e As System.EventArgs) Handles picSettings.MouseEnter
        picSettings.BackColor = Color.Blue
    End Sub

    Private Sub picSettings_MouseLeave(sender As Object, e As System.EventArgs) Handles picSettings.MouseLeave
        picSettings.BackColor = Color.Transparent
    End Sub

    Private Sub MetroTile2_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile2.Click
        frmGLFinacialReportGenerator.Show()
        frmGLFinacialReportGenerator.Select()
    End Sub

    Private Sub MetroTile1_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile1.Click
        frmReport_Generator.Show()
        frmReport_Generator.Select()
    End Sub

    Private Sub picSettings_Click(sender As System.Object, e As System.EventArgs) Handles picSettings.Click
        frmSettings.Show()
        frmSettings.Select()
    End Sub

    Private Sub MetroTile4_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile4.Click
        frmBS_Software.Show()
        frmBS_Software.Select()
    End Sub

    Private Sub MetroTile3_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile3.Click
        frmBilling_Manpower.Show()
        frmBilling_Manpower.Select()
    End Sub

    Private Sub MetroTile5_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile5.Click
        frmSP.Show()
        frmSP.Select()
    End Sub

    Private Sub MetroTile6_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile6.Click
        frmSC.Show()
        frmSC.Select()
    End Sub

    Private Sub MetroTile7_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile7.Click
        'frmUploader.Show()
        frmBooks_Uploader.Show()
        frmBooks_Uploader.Select()
    End Sub

    Private Sub MetroTile8_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile8.Click
        frmUploadHistory.Show()
        frmUploadHistory.Select()
    End Sub

    Private Sub MetroTile9_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile9.Click
        frmRFP.Show()
        frmRFP.Select()
    End Sub

    Private Sub MetroTile10_Click_1(sender As System.Object, e As System.EventArgs) Handles mtCC.Click
        frmCC.Show()
        frmCC.Select()
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles btnProduction.Click
        mtcMenu.SelectedTab = mtpProd
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Blue
        btnCoop.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub frmJO_Click(sender As System.Object, e As System.EventArgs) Handles tileJO.Click
        frmJO.Show()
        frmJO.Select()
    End Sub

    Private Sub frmBOM_Click(sender As System.Object, e As System.EventArgs) Handles tileBOMFG.Click
        frmBOM_FG.Show()
        frmBOM_FG.Select()
    End Sub

    Private Sub MetroTile12_Click(sender As System.Object, e As System.EventArgs)
        frmBOM_FG.Show()
        frmBOM_FG.Select()
    End Sub

    Private Sub mtPC_Click(sender As System.Object, e As System.EventArgs) Handles mtPC.Click
        frmPC.Show()
        frmPC.Select()
    End Sub

    Private Sub MetroTile10_Click_2(sender As System.Object, e As System.EventArgs) Handles MetroTile10.Click
        frmCompanyProfile.Show()
        frmCompanyProfile.Select()
    End Sub

    Private Sub MetroTile15_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile15.Click
        frmPWH.Show()
        frmPWH.Select()
    End Sub

    Private Sub btnCoop_Click(sender As System.Object, e As System.EventArgs) Handles btnCoop.Click
        mtcMenu.SelectedTab = mtpCoop
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Blue
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

   
    Private Sub tilePL_Click(sender As System.Object, e As System.EventArgs) Handles tilePL.Click
        frmPL.Show()
        frmPL.Select()
    End Sub

    Private Sub tileBOMSFG_Click(sender As System.Object, e As System.EventArgs) Handles tileBOMSFG.Click
        frmBOM_SFG.Show()
        frmBOM_SFG.Select()
    End Sub

    Private Sub tileBOMconv_Click(sender As System.Object, e As System.EventArgs) Handles tileBOMconv.Click
        frmBOMC.Show()
        frmBOMC.Select()
    End Sub

    Private Sub tileBOMExp_Click(sender As System.Object, e As System.EventArgs) Handles tileBOMExp.Click
        frmBOME.Show()
        frmBOME.Select()
    End Sub

    Private Sub MetroTile11_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile11.Click
        frmMR.Show()
        frmMR.Select()
    End Sub

    Private Sub tilePettyCashFund_Click(sender As System.Object, e As System.EventArgs) Handles tilePettyCashFund.Click
        frmPCV.Show()
        frmPCV.Select()
    End Sub

    Private Sub MetroTile12_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile12.Click
        frmCIP.Show()
        frmCIP.Select()
    End Sub

    Private Sub MetroTile20_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile20.Click
        frmBS.Show()
        frmBS.Select()
    End Sub

    Private Sub MetroTile13_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile13.Click
        frmBI.Show()
        frmBI.Select()
    End Sub

    Private Sub MetroTile21_Click(sender As System.Object, e As System.EventArgs)
        frmIA.Show()
        frmIA.Select()
    End Sub

    Private Sub Panel3_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel3.Paint
        Dim query As String

        query = "SELECT Company_Logo, Employer_Name FROM tblcompany"
        SQL.FlushParams()
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Label4.Text = SQL.SQLDR("Employer_Name").ToString
            If Not IsDBNull(SQL.SQLDR("Company_Logo")) Then
                pbPicture.Image = New Bitmap(byteArrayToImage(SQL.SQLDR("Company_Logo")))
                pbPicture.SizeMode = PictureBoxSizeMode.StretchImage
            Else
                LoadDefaultImage()
            End If
        End If
    End Sub
    Private Sub LoadDefaultImage()
        Dim App_Path As String
        App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName
        pbPicture.ImageLocation = App_Path & "\Images\DefaultLogo.jpg"
        pbPicture.SizeMode = PictureBoxSizeMode.StretchImage
    End Sub


    Private Sub MetroTile25_Click(sender As System.Object, e As System.EventArgs)
        frmGI.Show()
        frmGI.Select()
    End Sub

    Private Sub MetroTile26_Click(sender As System.Object, e As System.EventArgs)
        frmGR.Show()
        frmGR.Select()
    End Sub


    Private Sub MetroTile22_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile22.Click
        frmECS.Show()
        frmECS.Select()
    End Sub


    Private Sub MetroTile23_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile23.Click
        frmVerifier.Show()
        frmVerifier.Select()
    End Sub

  

    Private Sub MetroTile29_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile29.Click
        frmFund_Maintenance.Show()
        frmFund_Maintenance.Select()
    End Sub


    Private Sub MetroTile31_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile31.Click
        frmMember_Master.Show()
        frmFund_Maintenance.Select()
    End Sub

    Private Sub tileCashAdvance_Click(sender As System.Object, e As System.EventArgs) Handles tileCashAdvance.Click
        frmCashAdvance.Show()
        frmCashAdvance.Select()
    End Sub

    Private Sub MetroTile34_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile34.Click
        If Not AllowAccess("AT") Then
            msgRestrictedMod()
        Else
            frmAuditTrail.Show()
            frmAuditTrail.Select()
        End If
    End Sub

    Private Sub MetroTile35_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile35.Click
        frmMasterfile_Terms.Show()
        frmMasterfile_Terms.Select()
    End Sub


    Private Sub MetroTile37_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile37.Click
        frmFormMaintenance.Show()
        frmFormMaintenance.Select()
    End Sub


    Private Sub MetroTile39_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile39.Click
        Frm_Collector.Show()
        Frm_Collector.Select()
    End Sub

    Private Sub MetroTile33_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile33.Click
        frmSJ.Show()
        frmSJ.Select()
    End Sub

    Private Sub MetroTile32_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile32.Click
        frmPJ.Show()
        frmPJ.Select()
    End Sub

    Private Sub MetroTile36_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile36.Click
        frmBB.Show()
        frmBB.Select()
    End Sub


    Private Sub MetroTabPage1_Click(sender As System.Object, e As System.EventArgs) Handles MetroTabPage1.Click

    End Sub

    Private Sub MetroTile40_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile40.Click
        frmPCVRR.Show()
        frmPCVRR.Select()
    End Sub

    Private Sub MetroTile42_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile42.Click
        frmCollection.TransType = "CR"
        frmCollection.Book = "Cash Receipts"
        frmCollection.Show()
        frmCollection.Select()
    End Sub

    Private Sub MetroTile41_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile41.Click
        frmCollection.TransType = "PR"
        frmCollection.Book = "Cash Receipts"
        frmCollection.Show()
        frmCollection.Select()
    End Sub

    Private Sub MetroTile43_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile43.Click
        frmVCE_Uploader.Show()
        frmVCE_Uploader.Select()
    End Sub

    Private Sub MetroTile44_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile44.Click
        frmOC_Uploader.Show()
        frmOC_Uploader.Select()
    End Sub

    Private Sub btnPayable_Click(sender As System.Object, e As System.EventArgs) Handles btnPayable.Click
        mtcMenu.SelectedTab = MetroTabPage2
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Blue
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub btnPurchasing_Click(sender As System.Object, e As System.EventArgs) Handles btnPurchasing.Click
        mtcMenu.SelectedTab = MetroTabPage5
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Blue
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Transparent
    End Sub

    Private Sub btnInventory_Click(sender As System.Object, e As System.EventArgs) Handles btnInventory.Click
        mtcMenu.SelectedTab = MetroTabPage11
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnInventory.BackColor = Color.Blue
        btnLoan.BackColor = Color.Transparent
    End Sub



    Private Sub tileRR_Click(sender As System.Object, e As System.EventArgs) Handles tileRR.Click
        frmRR.Show()
        frmRR.Select()
    End Sub

    Private Sub MetroTile25_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile25.Click
        frmGI.Show()
        frmGI.Select()
    End Sub

    Private Sub MetroTile26_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile26.Click
        frmGR.Show()
        frmGR.Select()
    End Sub

    Private Sub tileDR_Click(sender As System.Object, e As System.EventArgs) Handles tileDR.Click
        frmDR.Show()
        frmDR.Select()
    End Sub

    Private Sub MetroTile45_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile45.Click
        frmCurrency.Show()
        frmCurrency.Select()
    End Sub


    Private Sub MetroTile21_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile21.Click
        frmIA.Show()
        frmIA.Select()
    End Sub


    Private Sub tileSalesOrder_Click_1(sender As System.Object, e As System.EventArgs) Handles tileSalesOrder.Click
        frmSO.Show()
        frmSO.Select()
    End Sub

    Private Sub MetroTile46_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile46.Click
        frmITEM_Uploader.Show()
        frmITEM_Uploader.Select()
    End Sub

    Private Sub MetroTile47_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile47.Click
        frmVCE_Updater.Show()
        frmVCE_Updater.Select()
    End Sub


    Private Sub MetroTile49_Click(sender As Object, e As EventArgs) Handles MetroTile49.Click
        frmMRISATD.Show()
        frmMRISATD.Select()
    End Sub

    Private Sub MetroTile50_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile50.Click
        frmMR.Show()
        frmMR.Select()
    End Sub

    Private Sub MetroTile51_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile51.Click
        frmSI_Cash.TransType = "Cash"
        frmSI_Cash.Show()
        frmSI_Cash.Select()
    End Sub

    Private Sub MetroTile52_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile52.Click
        frmItem_Master.Show()
        frmItem_Master.Select()
    End Sub

    Private Sub MetroTile53_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile53.Click
        frmBook_Maintenance.Show()
        frmBook_Maintenance.Select()
    End Sub

    Private Sub btnLoan_Click(sender As System.Object, e As System.EventArgs) Handles btnLoan.Click
        mtcMenu.SelectedTab = mtcLoan
        btnMasterfile.BackColor = Color.Transparent
        btnPayable.BackColor = Color.Transparent
        btnCollection.BackColor = Color.Transparent
        btnSales.BackColor = Color.Transparent
        btnPurchasing.BackColor = Color.Transparent
        btnGeneralJournal.BackColor = Color.Transparent
        btnDisbursement.BackColor = Color.Transparent
        btnPosting.BackColor = Color.Transparent
        btnReports.BackColor = Color.Transparent
        btnOtherModules.BackColor = Color.Transparent
        btnProduction.BackColor = Color.Transparent
        btnCoop.BackColor = Color.Transparent
        btnLoan.BackColor = Color.Blue
    End Sub

    Private Sub MetroTile18_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile18.Click
        frmLoan_Setup.Show()
        frmLoan_Setup.Select()
    End Sub

    Private Sub MetroTile19_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile19.Click
        frmLoan_Window.Show()
        frmLoan_Window.Select()
    End Sub


    Private Sub MetroTile16_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile16.Click
        frmMember_Master.Show()
        frmMember_Master.Select()
    End Sub

    Private Sub MetroTile24_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile24.Click
        frmSavings_Maintenance.Show()
        frmSavings_Maintenance.Select()
    End Sub

    Private Sub MetroTile28_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile28.Click
        frmSavings_Interest.Show()
        frmSavings_Interest.Select()
    End Sub


    Private Sub MetroTile27_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile27.Click
        frmSavings_Account.Show()
        frmSavings_Account.Select()
    End Sub

    Private Sub MetroTile54_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile54.Click
        frmPO_Uploader.Show()
        frmPO_Uploader.Select()
    End Sub

    Private Sub MetroTile55_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile55.Click
        frmSI_Cash.TransType = "Charge"
        frmSI_Cash.Show()
        frmSI_Cash.Select()
    End Sub

    Private Sub MetroTile56_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile56.Click
        frmItem_PriceUpload.Show()
        frmItem_PriceUpload.Select()
    End Sub

    Private Sub MetroTile57_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile57.Click
        frmChargeSlip.Show()
        frmChargeSlip.Select()
    End Sub

    Private Sub MetroTile58_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile58.Click
        frmSQ.Show()
        frmSQ.Select()
    End Sub


    Private Sub MetroTile60_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile60.Click
        frmBranch.Show()
        frmBranch.Select()
    End Sub

    Private Sub MetroTile61_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile61.Click
        frmTIMTA.Show()
        frmTIMTA.Select()
    End Sub

    Private Sub MetroTile62_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile62.Click
        frmIC.Show()
        frmIC.Select()
    End Sub

    Private Sub MetroTile63_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile63.Click
        frmTransactionApproval.Show()
        frmTransactionApproval.Select()
    End Sub

    Private Sub MetroTile64_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile64.Click
        frmLC.Show()
        frmLC.Select()
    End Sub

    Private Sub MetroTile65_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile65.Click
        frmRE.Show()
        frmRE.Select()
    End Sub


    Private Sub btnGetStarted_Click(sender As System.Object, e As System.EventArgs) Handles btnGetStarted.Click
        frmSetupWizard.Show()
        frmSetupWizard.Select()
    End Sub

    Private Sub tilePurchaseRequisition_Click(sender As System.Object, e As System.EventArgs) Handles tilePurchaseRequisition.Click
        frmPR.Show()
        frmPR.Select()
    End Sub

    Private Sub MetroTile14_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile14.Click
        frmCF.Show()
        frmCF.Select()
    End Sub

    Private Sub tilePurchaseOrder_Click_1(sender As System.Object, e As System.EventArgs) Handles tilePurchaseOrder.Click
        frmPO.Show()
        frmPO.Select()
    End Sub

    Private Sub MetroTile59_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile59.Click
        frmDepreciation.Show()
        frmDepreciation.Select()
    End Sub

    Private Sub MetroTile48_Click_1(sender As System.Object, e As System.EventArgs) Handles MetroTile48.Click
        frmATD.Show()
        frmATD.Select()
    End Sub

    Private Sub MetroTile38_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile38.Click
        frmUpload_FileArchive.Show()
        frmUpload_FileArchive.Select()
    End Sub

    Private Sub MetroTile67_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile67.Click
        frmItemBarcodePrinting.Show()
    End Sub

    Private Sub MetroTile68_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile68.Click
        frmItem_Master_Upload.Show()
    End Sub

    Private Sub MetroTile69_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile69.Click
        frmBooking.Show()
        frmBooking.Select()
    End Sub

    Private Sub MetroTile70_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile70.Click
        frmIPL.Show()
        frmIPL.Select()
    End Sub

    Private Sub MetroTile71_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile71.Click
        frmPDC.Show()
        frmPDC.Select()
    End Sub

    Private Sub MetroTile72_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile72.Click
        frmBudget.Show()
        frmBudget.Select()
    End Sub

    Private Sub MetroTile30_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile30.Click

    End Sub

    Private Sub MetroTile73_Click(sender As System.Object, e As System.EventArgs) Handles MetroTile73.Click
        frmTP.Show()
        frmTP.Select()
    End Sub

    Private Sub MetroTile74_Click(sender As Object, e As EventArgs) Handles MetroTile74.Click
        frmCashFlow_Maintenance.Show()
        frmCashFlow_Maintenance.Select()
    End Sub

    Private Sub MetroTile75_Click(sender As Object, e As EventArgs)

    End Sub
End Class