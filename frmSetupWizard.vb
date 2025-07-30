Public Class frmSetupWizard
    Dim disabletab As Boolean = False
    Dim CompanyProfile As New frmCompanyProfile
    Dim VCEUploader As New frmVCE_Uploader
    Dim ItemUploader As New frmItem_Master_Upload
    Dim Settings As New frmSettings

    Private Sub frmSetupWizard_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        CompanyProfile.isSetup = True
        LoadForms(CompanyProfile, TabControl1.TabPages.IndexOfKey(tpCompany.Name))
        LoadForms(New frmCOA, TabControl1.TabPages.IndexOfKey(tpCOAUploader.Name))
        VCEUploader.isSetup = True
        LoadForms(VCEUploader, TabControl1.TabPages.IndexOfKey(tpVCEUploader.Name))
        LoadForms(New frmMasterfile_Bank, TabControl1.TabPages.IndexOfKey(tpBankMasterfile.Name))
        LoadForms(ItemUploader, TabControl1.TabPages.IndexOfKey(tpItemMasterUploader.Name))
        Settings.isSetup = True
        LoadForms(Settings, TabControl1.TabPages.IndexOfKey(tpSettings.Name))

    End Sub

    Private Sub LoadForms(ByVal FormName As Form, ByVal tabcount As Integer)
        FormName.TopLevel = False
        FormName.Location = New Point(0, 0)
        FormName.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.TabControl1.TabPages(tabcount).Controls.Add(FormName)
        FormName.Size = Me.TabControl1.TabPages(tabcount).Size
        FormName.Show()

    End Sub
    Private PreviousTab As New TabPage
    Private CurrentTab As New TabPage
    Private Sub TabControl1_Deselected(sender As Object, e As System.Windows.Forms.TabControlEventArgs) Handles TabControl1.Deselected
        If disabletab = False Then
            PreviousTab = e.TabPage
            disabletab = True
        End If
    End Sub

    Private Sub TabControl1_Selected(sender As Object, e As System.Windows.Forms.TabControlEventArgs) Handles TabControl1.Selected
        CurrentTab = e.TabPage
        If disabletab = True Then
            If PreviousTab.Name = tpWelcome.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpCOAUploader.Name) Or (CurrentTab.Name = tpVCEUploader.Name) Or (CurrentTab.Name = tpItemMasterUploader.Name) Or (CurrentTab.Name = tpBankMasterfile.Name) Or (CurrentTab.Name = tpFinished.Name) Or (CurrentTab.Name = tpSettings.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpCompany.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpVCEUploader.Name) Or (CurrentTab.Name = tpBankMasterfile.Name) Or (CurrentTab.Name = tpItemMasterUploader.Name) Or (CurrentTab.Name = tpSettings.Name) Or (CurrentTab.Name = tpFinished.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpCOAUploader.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpBankMasterfile.Name) Or (CurrentTab.Name = tpSettings.Name) Or (CurrentTab.Name = tpItemMasterUploader.Name) Or (CurrentTab.Name = tpWelcome.Name) Or (CurrentTab.Name = tpFinished.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpVCEUploader.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpCompany.Name) Or (CurrentTab.Name = tpSettings.Name) Or (CurrentTab.Name = tpWelcome.Name) Or (CurrentTab.Name = tpFinished.Name) Or (CurrentTab.Name = tpBankMasterfile.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpItemMasterUploader.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpCompany.Name) Or (CurrentTab.Name = tpSettings.Name) Or (CurrentTab.Name = tpCOAUploader.Name) Or (CurrentTab.Name = tpWelcome.Name) Or (CurrentTab.Name = tpFinished.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpBankMasterfile.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpCompany.Name) Or (CurrentTab.Name = tpCOAUploader.Name) Or (CurrentTab.Name = tpWelcome.Name) Or (CurrentTab.Name = tpFinished.Name) Or (CurrentTab.Name = tpVCEUploader.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpSettings.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpCompany.Name) Or (CurrentTab.Name = tpItemMasterUploader.Name) Or (CurrentTab.Name = tpCOAUploader.Name) Or (CurrentTab.Name = tpVCEUploader.Name) Or (CurrentTab.Name = tpWelcome.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            ElseIf PreviousTab.Name = tpFinished.Name Then
                If (PreviousTab.Name <> CurrentTab.Name) And ((CurrentTab.Name = tpCompany.Name) Or (CurrentTab.Name = tpItemMasterUploader.Name) Or (CurrentTab.Name = tpCOAUploader.Name) Or (CurrentTab.Name = tpVCEUploader.Name) Or (CurrentTab.Name = tpBankMasterfile.Name) Or (CurrentTab.Name = tpWelcome.Name)) Then
                    TabControl1.SelectedTab = PreviousTab
                End If
            End If
        End If
            disabletab = False
    End Sub

    Private Sub TabControl1_TabIndexChanged(sender As Object, e As System.EventArgs) Handles TabControl1.TabIndexChanged
     
    End Sub

    Private Sub tsbNext_Click(sender As System.Object, e As System.EventArgs) Handles tsbNext.Click
        If TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpWelcome.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCompany.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCompany.Name) Then
            CompanyProfile.btnSave.Visible = True
            CompanyProfile.btnSave.PerformClick()
            If CompanyProfile.btnSave.Visible = True Then
                CompanyProfile.btnSave.Visible = False
                TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCOAUploader.Name)
            End If
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCOAUploader.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpVCEUploader.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpVCEUploader.Name) Then
            VCEUploader.tsbSave.Visible = True
            VCEUploader.tsbSave.PerformClick()
            If VCEUploader.tsbSave.Visible = True Then
                VCEUploader.tsbSave.Visible = False
                TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpItemMasterUploader.Name)
            End If
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpItemMasterUploader.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpBankMasterfile.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpBankMasterfile.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpSettings.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpSettings.Name) Then
            Settings.btnSave.Visible = True
            Settings.btnSave.PerformClick()
            If Settings.btnSave.Visible = True Then
                Settings.btnSave.Visible = False
                TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpFinished.Name)
            End If
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpFinished.Name) Then
            Me.Close()
        End If
    End Sub

    Private Sub tsbPrevious_Click(sender As Object, e As System.EventArgs) Handles tsbPrevious.Click
        If TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpFinished.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpSettings.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpSettings.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpBankMasterfile.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpBankMasterfile.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpItemMasterUploader.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpItemMasterUploader.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpVCEUploader.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpVCEUploader.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCOAUploader.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCOAUploader.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCompany.Name)
        ElseIf TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpCompany.Name) Then
            TabControl1.SelectedIndex = TabControl1.TabPages.IndexOfKey(tpWelcome.Name)
        End If
    End Sub

    Private Sub tsbExit_Click(sender As System.Object, e As System.EventArgs) Handles tsbExit.Click
        Me.Close()
    End Sub
End Class