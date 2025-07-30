<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Chart of Account")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Transaction ID")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("VCE Setup")
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("User Account")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("ATC Table")
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Branch Setup")
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Business Type Setup")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Maintenance Group")
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("General", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2, TreeNode3, TreeNode4, TreeNode5, TreeNode6, TreeNode7, TreeNode8})
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Purchasing")
        Dim TreeNode11 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Sales")
        Dim TreeNode12 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Collection")
        Dim TreeNode13 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Inventory")
        Dim TreeNode14 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Production")
        Dim TreeNode15 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Cooperative")
        Dim TreeNode16 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Default Entries")
        Dim TreeNode17 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Email")
        Dim TreeNode18 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("SLS and SLP Maintenance")
        Dim TreeNode19 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("BIR Reminders")
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSettings))
        Me.tcSettings = New System.Windows.Forms.TabControl()
        Me.tpUA = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CheckBox13 = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBox11 = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CheckBox10 = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CheckBox9 = New System.Windows.Forms.CheckBox()
        Me.CheckBox8 = New System.Windows.Forms.CheckBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.nupUAminLen = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.attemptLock = New System.Windows.Forms.CheckBox()
        Me.tpCOA = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCOAFormat = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbCOAformat = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnCatDown = New System.Windows.Forms.Button()
        Me.btnCatUp = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.lvType = New System.Windows.Forms.ListView()
        Me.chType = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDesc = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chDigit = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chOrder = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chkCOAauto = New System.Windows.Forms.CheckBox()
        Me.tpGeneral = New System.Windows.Forms.TabPage()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chkReversalEntries = New System.Windows.Forms.CheckBox()
        Me.chkForApproval = New System.Windows.Forms.CheckBox()
        Me.chkTransAuto = New System.Windows.Forms.CheckBox()
        Me.dgvTransDetail = New System.Windows.Forms.DataGridView()
        Me.dgcTransBranch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransBusType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransPrefix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransDigits = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.chkGlobal = New System.Windows.Forms.CheckBox()
        Me.dgvTransDetailsAll = New System.Windows.Forms.DataGridView()
        Me.dgcTransAllType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransAllBranch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransAllBus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransAllPrefix = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransAlldigit = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransAllStart = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvTransID = New System.Windows.Forms.DataGridView()
        Me.dgcTransType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcTransAuto = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcTransGlobal = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcForApproval = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dgcForReversal = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.tpVCE = New System.Windows.Forms.TabPage()
        Me.gbVCE = New System.Windows.Forms.GroupBox()
        Me.NumericUpDown5 = New System.Windows.Forms.NumericUpDown()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox12 = New System.Windows.Forms.CheckBox()
        Me.tpEntries = New System.Windows.Forms.TabPage()
        Me.PanelEntries = New System.Windows.Forms.Panel()
        Me.GroupBox28 = New System.Windows.Forms.GroupBox()
        Me.txtBM_SRCode = New System.Windows.Forms.TextBox()
        Me.txtBM_SRTitle = New System.Windows.Forms.TextBox()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.txtBM_APCode = New System.Windows.Forms.TextBox()
        Me.txtBM_APTitle = New System.Windows.Forms.TextBox()
        Me.Label88 = New System.Windows.Forms.Label()
        Me.txtBM_COSCode = New System.Windows.Forms.TextBox()
        Me.txtBM_COSTitle = New System.Windows.Forms.TextBox()
        Me.Label89 = New System.Windows.Forms.Label()
        Me.GroupBox27 = New System.Windows.Forms.GroupBox()
        Me.txtInv_VarianceAccntCode = New System.Windows.Forms.TextBox()
        Me.txtInv_VarianceAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.GroupBox24 = New System.Windows.Forms.GroupBox()
        Me.txtLM_AdvanceRentCode = New System.Windows.Forms.TextBox()
        Me.txtLM_AdvanceRentTitle = New System.Windows.Forms.TextBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.txtLM_RentalIncomeCode = New System.Windows.Forms.TextBox()
        Me.txtLM_RentalIncomeTitle = New System.Windows.Forms.TextBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtLM_NFCode = New System.Windows.Forms.TextBox()
        Me.txtLM_NFTitle = New System.Windows.Forms.TextBox()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.txtLM_DSTCode = New System.Windows.Forms.TextBox()
        Me.txtLM_DSTTitle = New System.Windows.Forms.TextBox()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.txtLM_DepositCode = New System.Windows.Forms.TextBox()
        Me.txtLM_DepositTitle = New System.Windows.Forms.TextBox()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.GroupBox23 = New System.Windows.Forms.GroupBox()
        Me.txtRE_ARCode = New System.Windows.Forms.TextBox()
        Me.txtRE_ARTitle = New System.Windows.Forms.TextBox()
        Me.Label95 = New System.Windows.Forms.Label()
        Me.txtRE_ARMiscFeeCode = New System.Windows.Forms.TextBox()
        Me.txtRE_ARMiscFeeTitle = New System.Windows.Forms.TextBox()
        Me.Label94 = New System.Windows.Forms.Label()
        Me.txtRE_MiscFeeCode = New System.Windows.Forms.TextBox()
        Me.txtRE_MiscFeeTitle = New System.Windows.Forms.TextBox()
        Me.Label93 = New System.Windows.Forms.Label()
        Me.txtRE_OutputVatCode = New System.Windows.Forms.TextBox()
        Me.txtRE_OutputVatTitle = New System.Windows.Forms.TextBox()
        Me.Label92 = New System.Windows.Forms.Label()
        Me.txtRE_NetOfVATCode = New System.Windows.Forms.TextBox()
        Me.txtRE_NetOfVATTitle = New System.Windows.Forms.TextBox()
        Me.Label91 = New System.Windows.Forms.Label()
        Me.txtRE_SalesCode = New System.Windows.Forms.TextBox()
        Me.txtRE_SalesTitle = New System.Windows.Forms.TextBox()
        Me.Label90 = New System.Windows.Forms.Label()
        Me.txtRE_CommissionCode = New System.Windows.Forms.TextBox()
        Me.txtRE_CommissionTitle = New System.Windows.Forms.TextBox()
        Me.Label87 = New System.Windows.Forms.Label()
        Me.txtRE_AccountCode = New System.Windows.Forms.TextBox()
        Me.txtRE_AccountTitle = New System.Windows.Forms.TextBox()
        Me.Label62 = New System.Windows.Forms.Label()
        Me.txtRE_ReserveCode = New System.Windows.Forms.TextBox()
        Me.txtRE_ReserveTitle = New System.Windows.Forms.TextBox()
        Me.Label64 = New System.Windows.Forms.Label()
        Me.txtRE_PenaltyCode = New System.Windows.Forms.TextBox()
        Me.txtRE_PenaltyTitle = New System.Windows.Forms.TextBox()
        Me.Label69 = New System.Windows.Forms.Label()
        Me.txtRE_InterestCode = New System.Windows.Forms.TextBox()
        Me.txtRE_InterestTitle = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.txtRE_EquityCode = New System.Windows.Forms.TextBox()
        Me.txtRE_EquityTitle = New System.Windows.Forms.TextBox()
        Me.Label71 = New System.Windows.Forms.Label()
        Me.GroupBox19 = New System.Windows.Forms.GroupBox()
        Me.txtPOS_VATSalesCode = New System.Windows.Forms.TextBox()
        Me.txtPOS_VATSalesTitle = New System.Windows.Forms.TextBox()
        Me.Label54 = New System.Windows.Forms.Label()
        Me.txtPOS_DiscountCode = New System.Windows.Forms.TextBox()
        Me.txtPOS_DiscountTitle = New System.Windows.Forms.TextBox()
        Me.Label56 = New System.Windows.Forms.Label()
        Me.txtPOS_ZeroRatedCode = New System.Windows.Forms.TextBox()
        Me.txtPOS_ZeroRatedTitle = New System.Windows.Forms.TextBox()
        Me.Label58 = New System.Windows.Forms.Label()
        Me.txtPOS_VATExemptCode = New System.Windows.Forms.TextBox()
        Me.txtPOS_VATExemptTitle = New System.Windows.Forms.TextBox()
        Me.Label59 = New System.Windows.Forms.Label()
        Me.txtPOS_VATAmountCode = New System.Windows.Forms.TextBox()
        Me.txtPOS_VATAmountTitle = New System.Windows.Forms.TextBox()
        Me.Label60 = New System.Windows.Forms.Label()
        Me.GroupBox22 = New System.Windows.Forms.GroupBox()
        Me.txtGPA_SalesAccntCode = New System.Windows.Forms.TextBox()
        Me.txtGPA_SalesAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label61 = New System.Windows.Forms.Label()
        Me.txtGPA_SaleReturnAccntCode = New System.Windows.Forms.TextBox()
        Me.txtGPA_SaleReturnAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label65 = New System.Windows.Forms.Label()
        Me.txtGPA_SaleDiscountAccntCode = New System.Windows.Forms.TextBox()
        Me.txtGPA_SaleDiscountAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label66 = New System.Windows.Forms.Label()
        Me.txtGPA_COSAccntCode = New System.Windows.Forms.TextBox()
        Me.txtGPA_COSAccntTitle = New System.Windows.Forms.TextBox()
        Me.Label67 = New System.Windows.Forms.Label()
        Me.gbAP = New System.Windows.Forms.GroupBox()
        Me.nupBankPeriod = New System.Windows.Forms.NumericUpDown()
        Me.Label86 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.chkPOapproval = New System.Windows.Forms.CheckBox()
        Me.txtIVcode = New System.Windows.Forms.TextBox()
        Me.txtATScode = New System.Windows.Forms.TextBox()
        Me.txtPAPcode = New System.Windows.Forms.TextBox()
        Me.CheckBox15 = New System.Windows.Forms.CheckBox()
        Me.txtPAPtitle = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lbPayables = New System.Windows.Forms.ListBox()
        Me.txtATStitle = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtIVtitle = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnRemovePayables = New System.Windows.Forms.Button()
        Me.btnAddPayables = New System.Windows.Forms.Button()
        Me.GroupBox14 = New System.Windows.Forms.GroupBox()
        Me.txtCOH_Code = New System.Windows.Forms.TextBox()
        Me.txtCOH_Title = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtPEC_Code = New System.Windows.Forms.TextBox()
        Me.txtPEC_Title = New System.Windows.Forms.TextBox()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.txtCWT_Code = New System.Windows.Forms.TextBox()
        Me.txtCWT_Title = New System.Windows.Forms.TextBox()
        Me.Label50 = New System.Windows.Forms.Label()
        Me.txtFWT_Code = New System.Windows.Forms.TextBox()
        Me.txtFWT_Title = New System.Windows.Forms.TextBox()
        Me.Label49 = New System.Windows.Forms.Label()
        Me.txtEWT_Code = New System.Windows.Forms.TextBox()
        Me.txtEWT_Title = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtVP_Code = New System.Windows.Forms.TextBox()
        Me.txtVP_Title = New System.Windows.Forms.TextBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.txtPT_Code = New System.Windows.Forms.TextBox()
        Me.txtPT_Title = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.txtDOV_Code = New System.Windows.Forms.TextBox()
        Me.txtDOV_Title = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtOV_Code = New System.Windows.Forms.TextBox()
        Me.lbReceivables = New System.Windows.Forms.ListBox()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.txtOV_Title = New System.Windows.Forms.TextBox()
        Me.Label47 = New System.Windows.Forms.Label()
        Me.btnRemoveReceivable = New System.Windows.Forms.Button()
        Me.btnAddReceivables = New System.Windows.Forms.Button()
        Me.GroupBox11 = New System.Windows.Forms.GroupBox()
        Me.lblCashAdvance = New System.Windows.Forms.ListBox()
        Me.Label52 = New System.Windows.Forms.Label()
        Me.btnRemoveCA = New System.Windows.Forms.Button()
        Me.btnAddCA = New System.Windows.Forms.Button()
        Me.tpPurchase = New System.Windows.Forms.TabPage()
        Me.gbPO = New System.Windows.Forms.GroupBox()
        Me.CheckBox14 = New System.Windows.Forms.CheckBox()
        Me.gbPR = New System.Windows.Forms.GroupBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.cbPRstock = New System.Windows.Forms.ComboBox()
        Me.tpATC = New System.Windows.Forms.TabPage()
        Me.GroupBox10 = New System.Windows.Forms.GroupBox()
        Me.dgvATC = New System.Windows.Forms.DataGridView()
        Me.dgcATCCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcATCDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcATCRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpBranch = New System.Windows.Forms.TabPage()
        Me.GroupBox13 = New System.Windows.Forms.GroupBox()
        Me.dgvBranch = New System.Windows.Forms.DataGridView()
        Me.dgcBranchOldCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBranchCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBranchName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBranchMain = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.tpBusiType = New System.Windows.Forms.TabPage()
        Me.GroupBox12 = New System.Windows.Forms.GroupBox()
        Me.dgvBusType = New System.Windows.Forms.DataGridView()
        Me.dgcBusTypeOld = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBusType = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgcBusTypeDesc = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tpMaintGroup = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox25 = New System.Windows.Forms.GroupBox()
        Me.txtInv_Group1 = New System.Windows.Forms.TextBox()
        Me.Label63 = New System.Windows.Forms.Label()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtInv_Group5 = New System.Windows.Forms.TextBox()
        Me.txtInv_Group2 = New System.Windows.Forms.TextBox()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.Label75 = New System.Windows.Forms.Label()
        Me.txtInv_Group4 = New System.Windows.Forms.TextBox()
        Me.txtInv_Group3 = New System.Windows.Forms.TextBox()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.gbProdWH = New System.Windows.Forms.GroupBox()
        Me.txtPWHG1 = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtPWHG5 = New System.Windows.Forms.TextBox()
        Me.txtPWHG2 = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtPWHG4 = New System.Windows.Forms.TextBox()
        Me.txtPWHG3 = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.gbPC = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtPCG5 = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtPCG4 = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.txtPCG3 = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtPCG2 = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtPCG1 = New System.Windows.Forms.TextBox()
        Me.gbCC = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtCCG5 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtCCG4 = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtCCG3 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtCCG2 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCCG1 = New System.Windows.Forms.TextBox()
        Me.gbInvWH = New System.Windows.Forms.GroupBox()
        Me.txtWHG1 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtWHG5 = New System.Windows.Forms.TextBox()
        Me.txtWHG2 = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtWHG4 = New System.Windows.Forms.TextBox()
        Me.txtWHG3 = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.tpSales = New System.Windows.Forms.TabPage()
        Me.gbSO = New System.Windows.Forms.GroupBox()
        Me.chkSOreqDelivDate = New System.Windows.Forms.CheckBox()
        Me.chkSOstaggered = New System.Windows.Forms.CheckBox()
        Me.chkSOreqPO = New System.Windows.Forms.CheckBox()
        Me.chkSOeditPrice = New System.Windows.Forms.CheckBox()
        Me.tpInventory = New System.Windows.Forms.TabPage()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.GroupBox18 = New System.Windows.Forms.GroupBox()
        Me.rbPOS_POS = New System.Windows.Forms.RadioButton()
        Me.rbPOS_CSI = New System.Windows.Forms.RadioButton()
        Me.GroupBox17 = New System.Windows.Forms.GroupBox()
        Me.rbCSI_JV = New System.Windows.Forms.RadioButton()
        Me.rbCSI_Inventory = New System.Windows.Forms.RadioButton()
        Me.GroupBox16 = New System.Windows.Forms.GroupBox()
        Me.rbRR_Purchase = New System.Windows.Forms.RadioButton()
        Me.rbRR_Inventory = New System.Windows.Forms.RadioButton()
        Me.GroupBox15 = New System.Windows.Forms.GroupBox()
        Me.rbInv_WAUC = New System.Windows.Forms.RadioButton()
        Me.rbInv_SC = New System.Windows.Forms.RadioButton()
        Me.chkRR_RestrictWHSEItem = New System.Windows.Forms.CheckBox()
        Me.tpCoop = New System.Windows.Forms.TabPage()
        Me.gbCoop = New System.Windows.Forms.GroupBox()
        Me.txtTCPcode = New System.Windows.Forms.TextBox()
        Me.txtTCPtitle = New System.Windows.Forms.TextBox()
        Me.txtTCCcode = New System.Windows.Forms.TextBox()
        Me.txtTCCtitle = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.txtDFCScode = New System.Windows.Forms.TextBox()
        Me.txtDFCStitle = New System.Windows.Forms.TextBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.txtPUCPcode = New System.Windows.Forms.TextBox()
        Me.txtPUCPtitle = New System.Windows.Forms.TextBox()
        Me.txtPUCCcode = New System.Windows.Forms.TextBox()
        Me.txtPUCCtitle = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.txtSRPcode = New System.Windows.Forms.TextBox()
        Me.txtSRPtitle = New System.Windows.Forms.TextBox()
        Me.txtSRCcode = New System.Windows.Forms.TextBox()
        Me.txtSRCtitle = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtSCPcode = New System.Windows.Forms.TextBox()
        Me.txtSCPtitle = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.txtSCCcode = New System.Windows.Forms.TextBox()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtSCCtitle = New System.Windows.Forms.TextBox()
        Me.tpCollection = New System.Windows.Forms.TabPage()
        Me.tpProduction = New System.Windows.Forms.TabPage()
        Me.gbJO = New System.Windows.Forms.GroupBox()
        Me.chkJOperSOitem = New System.Windows.Forms.CheckBox()
        Me.tpEmail = New System.Windows.Forms.TabPage()
        Me.MetroLabel3 = New MetroFramework.Controls.MetroLabel()
        Me.txtEmailPass = New MetroFramework.Controls.MetroTextBox()
        Me.MetroLabel4 = New MetroFramework.Controls.MetroLabel()
        Me.txtEmailAddress = New MetroFramework.Controls.MetroTextBox()
        Me.tpSLS_SLP = New System.Windows.Forms.TabPage()
        Me.GroupBox21 = New System.Windows.Forms.GroupBox()
        Me.lvlSLP = New System.Windows.Forms.ListBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.btnRemove_SLP = New System.Windows.Forms.Button()
        Me.btnAdd_SLP = New System.Windows.Forms.Button()
        Me.GroupBox20 = New System.Windows.Forms.GroupBox()
        Me.Label55 = New System.Windows.Forms.Label()
        Me.lvlSLS = New System.Windows.Forms.ListBox()
        Me.btnRemove_SLS = New System.Windows.Forms.Button()
        Me.btnAdd_SLS = New System.Windows.Forms.Button()
        Me.tpBIRReminders = New System.Windows.Forms.TabPage()
        Me.GroupBox26 = New System.Windows.Forms.GroupBox()
        Me.nudBIR_WithIN = New System.Windows.Forms.NumericUpDown()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.btnBIR_New = New System.Windows.Forms.Button()
        Me.btnBIR_Remove = New System.Windows.Forms.Button()
        Me.btnBIR_Add = New System.Windows.Forms.Button()
        Me.p = New System.Windows.Forms.GroupBox()
        Me.cbBIR_Period = New System.Windows.Forms.ComboBox()
        Me.cbBIR_Date = New System.Windows.Forms.ComboBox()
        Me.cbBIR_Month = New System.Windows.Forms.ComboBox()
        Me.Label83 = New System.Windows.Forms.Label()
        Me.Label82 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.txtBIR_Reminder = New System.Windows.Forms.TextBox()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.lvlBIRReminders = New System.Windows.Forms.ListView()
        Me.chBIR_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBIR_Description = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBIR_Period = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBIR_Month = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.chBIR_Date = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnUpdateReport = New System.Windows.Forms.Button()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.txtNI_Code = New System.Windows.Forms.TextBox()
        Me.txtNI_Title = New System.Windows.Forms.TextBox()
        Me.Label96 = New System.Windows.Forms.Label()
        Me.tcSettings.SuspendLayout()
        Me.tpUA.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.nupUAminLen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpCOA.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.tpGeneral.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        CType(Me.dgvTransDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTransDetailsAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvTransID, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpVCE.SuspendLayout()
        Me.gbVCE.SuspendLayout()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpEntries.SuspendLayout()
        Me.PanelEntries.SuspendLayout()
        Me.GroupBox28.SuspendLayout()
        Me.GroupBox27.SuspendLayout()
        Me.GroupBox24.SuspendLayout()
        Me.GroupBox23.SuspendLayout()
        Me.GroupBox19.SuspendLayout()
        Me.GroupBox22.SuspendLayout()
        Me.gbAP.SuspendLayout()
        CType(Me.nupBankPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox14.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox11.SuspendLayout()
        Me.tpPurchase.SuspendLayout()
        Me.gbPO.SuspendLayout()
        Me.gbPR.SuspendLayout()
        Me.tpATC.SuspendLayout()
        Me.GroupBox10.SuspendLayout()
        CType(Me.dgvATC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBranch.SuspendLayout()
        Me.GroupBox13.SuspendLayout()
        CType(Me.dgvBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpBusiType.SuspendLayout()
        Me.GroupBox12.SuspendLayout()
        CType(Me.dgvBusType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpMaintGroup.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox25.SuspendLayout()
        Me.gbProdWH.SuspendLayout()
        Me.gbPC.SuspendLayout()
        Me.gbCC.SuspendLayout()
        Me.gbInvWH.SuspendLayout()
        Me.tpSales.SuspendLayout()
        Me.gbSO.SuspendLayout()
        Me.tpInventory.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox18.SuspendLayout()
        Me.GroupBox17.SuspendLayout()
        Me.GroupBox16.SuspendLayout()
        Me.GroupBox15.SuspendLayout()
        Me.tpCoop.SuspendLayout()
        Me.gbCoop.SuspendLayout()
        Me.tpProduction.SuspendLayout()
        Me.gbJO.SuspendLayout()
        Me.tpEmail.SuspendLayout()
        Me.tpSLS_SLP.SuspendLayout()
        Me.GroupBox21.SuspendLayout()
        Me.GroupBox20.SuspendLayout()
        Me.tpBIRReminders.SuspendLayout()
        Me.GroupBox26.SuspendLayout()
        CType(Me.nudBIR_WithIN, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.p.SuspendLayout()
        Me.SuspendLayout()
        '
        'tcSettings
        '
        Me.tcSettings.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.tcSettings.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tcSettings.Controls.Add(Me.tpUA)
        Me.tcSettings.Controls.Add(Me.tpCOA)
        Me.tcSettings.Controls.Add(Me.tpGeneral)
        Me.tcSettings.Controls.Add(Me.tpVCE)
        Me.tcSettings.Controls.Add(Me.tpEntries)
        Me.tcSettings.Controls.Add(Me.tpPurchase)
        Me.tcSettings.Controls.Add(Me.tpATC)
        Me.tcSettings.Controls.Add(Me.tpBranch)
        Me.tcSettings.Controls.Add(Me.tpBusiType)
        Me.tcSettings.Controls.Add(Me.tpMaintGroup)
        Me.tcSettings.Controls.Add(Me.tpSales)
        Me.tcSettings.Controls.Add(Me.tpInventory)
        Me.tcSettings.Controls.Add(Me.tpCoop)
        Me.tcSettings.Controls.Add(Me.tpCollection)
        Me.tcSettings.Controls.Add(Me.tpProduction)
        Me.tcSettings.Controls.Add(Me.tpEmail)
        Me.tcSettings.Controls.Add(Me.tpSLS_SLP)
        Me.tcSettings.Controls.Add(Me.tpBIRReminders)
        Me.tcSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.tcSettings.ItemSize = New System.Drawing.Size(25, 1)
        Me.tcSettings.Location = New System.Drawing.Point(285, 7)
        Me.tcSettings.Multiline = True
        Me.tcSettings.Name = "tcSettings"
        Me.tcSettings.SelectedIndex = 0
        Me.tcSettings.Size = New System.Drawing.Size(794, 617)
        Me.tcSettings.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tcSettings.TabIndex = 0
        '
        'tpUA
        '
        Me.tpUA.Controls.Add(Me.Panel1)
        Me.tpUA.Location = New System.Drawing.Point(5, 4)
        Me.tpUA.Name = "tpUA"
        Me.tpUA.Padding = New System.Windows.Forms.Padding(3)
        Me.tpUA.Size = New System.Drawing.Size(785, 609)
        Me.tpUA.TabIndex = 0
        Me.tpUA.Text = "User Account"
        Me.tpUA.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(779, 603)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.CheckBox13)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(755, 73)
        Me.GroupBox4.TabIndex = 16
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Registration"
        '
        'CheckBox13
        '
        Me.CheckBox13.AutoSize = True
        Me.CheckBox13.Location = New System.Drawing.Point(30, 27)
        Me.CheckBox13.Name = "CheckBox13"
        Me.CheckBox13.Size = New System.Drawing.Size(193, 21)
        Me.CheckBox13.TabIndex = 0
        Me.CheckBox13.Text = "Allow user self-registration"
        Me.CheckBox13.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.NumericUpDown4)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.CheckBox11)
        Me.GroupBox3.Controls.Add(Me.NumericUpDown3)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.CheckBox10)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 358)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(755, 104)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Password Expiration"
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Location = New System.Drawing.Point(122, 54)
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(42, 23)
        Me.NumericUpDown4.TabIndex = 6
        Me.NumericUpDown4.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(170, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(152, 17)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "days prior to expiration"
        '
        'CheckBox11
        '
        Me.CheckBox11.AutoSize = True
        Me.CheckBox11.Location = New System.Drawing.Point(50, 56)
        Me.CheckBox11.Name = "CheckBox11"
        Me.CheckBox11.Size = New System.Drawing.Size(75, 21)
        Me.CheckBox11.TabIndex = 4
        Me.CheckBox11.Text = "Remind"
        Me.CheckBox11.UseVisualStyleBackColor = True
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Location = New System.Drawing.Point(160, 29)
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(42, 23)
        Me.NumericUpDown3.TabIndex = 3
        Me.NumericUpDown3.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(208, 31)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(206, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Maximum Failed Login Attempts"
        '
        'CheckBox10
        '
        Me.CheckBox10.AutoSize = True
        Me.CheckBox10.Location = New System.Drawing.Point(30, 31)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(146, 21)
        Me.CheckBox10.TabIndex = 0
        Me.CheckBox10.Text = "Expire Password in"
        Me.CheckBox10.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.CheckBox9)
        Me.GroupBox2.Controls.Add(Me.CheckBox8)
        Me.GroupBox2.Controls.Add(Me.CheckBox7)
        Me.GroupBox2.Controls.Add(Me.CheckBox6)
        Me.GroupBox2.Controls.Add(Me.CheckBox5)
        Me.GroupBox2.Controls.Add(Me.CheckBox4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.nupUAminLen)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.CheckBox3)
        Me.GroupBox2.Controls.Add(Me.CheckBox2)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 83)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(755, 269)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Password Policy"
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = True
        Me.CheckBox9.Location = New System.Drawing.Point(30, 21)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(182, 21)
        Me.CheckBox9.TabIndex = 14
        Me.CheckBox9.Text = "Enforce Password Policy"
        Me.CheckBox9.UseVisualStyleBackColor = True
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.Location = New System.Drawing.Point(67, 238)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(268, 21)
        Me.CheckBox8.TabIndex = 13
        Me.CheckBox8.Text = "Username cannot be part of password"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Location = New System.Drawing.Point(67, 217)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(456, 21)
        Me.CheckBox7.TabIndex = 12
        Me.CheckBox7.Text = "At least 1 special character in password  E.g.  !, @, #, $, %, ^, &&, *, _"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(67, 196)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(217, 21)
        Me.CheckBox6.TabIndex = 11
        Me.CheckBox6.Text = "At least 1 number in password"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(67, 175)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(232, 21)
        Me.CheckBox5.TabIndex = 10
        Me.CheckBox5.Text = "At least 1 lowercase in password"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(67, 154)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(236, 21)
        Me.CheckBox4.TabIndex = 9
        Me.CheckBox4.Text = "At least 1 uppercase in password"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(43, 136)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(213, 17)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Password Mandatory Characters"
        '
        'nupUAminLen
        '
        Me.nupUAminLen.Location = New System.Drawing.Point(50, 106)
        Me.nupUAminLen.Name = "nupUAminLen"
        Me.nupUAminLen.Size = New System.Drawing.Size(42, 23)
        Me.nupUAminLen.TabIndex = 7
        Me.nupUAminLen.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(98, 110)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(176, 17)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Password Minimum Length"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(50, 85)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(181, 21)
        Me.CheckBox3.TabIndex = 5
        Me.CheckBox3.Text = "Enable Forgot Password"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(50, 43)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(285, 21)
        Me.CheckBox2.TabIndex = 0
        Me.CheckBox2.Text = "Require Password Change on First Login"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(50, 64)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(209, 21)
        Me.CheckBox1.TabIndex = 4
        Me.CheckBox1.Text = "Generate Random Password"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.attemptLock)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 468)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(755, 96)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Account Locking"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(47, 53)
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(42, 23)
        Me.NumericUpDown1.TabIndex = 3
        Me.NumericUpDown1.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(95, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(206, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Maximum Failed Login Attempts"
        '
        'attemptLock
        '
        Me.attemptLock.AutoSize = True
        Me.attemptLock.Location = New System.Drawing.Point(30, 31)
        Me.attemptLock.Name = "attemptLock"
        Me.attemptLock.Size = New System.Drawing.Size(168, 21)
        Me.attemptLock.TabIndex = 0
        Me.attemptLock.Text = "Account Lock Enabled"
        Me.attemptLock.UseVisualStyleBackColor = True
        '
        'tpCOA
        '
        Me.tpCOA.Controls.Add(Me.GroupBox6)
        Me.tpCOA.Location = New System.Drawing.Point(5, 4)
        Me.tpCOA.Name = "tpCOA"
        Me.tpCOA.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCOA.Size = New System.Drawing.Size(785, 609)
        Me.tpCOA.TabIndex = 1
        Me.tpCOA.Text = "Chart of Account"
        Me.tpCOA.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.BackColor = System.Drawing.Color.White
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.txtCOAFormat)
        Me.GroupBox6.Controls.Add(Me.Label6)
        Me.GroupBox6.Controls.Add(Me.cbCOAformat)
        Me.GroupBox6.Controls.Add(Me.GroupBox5)
        Me.GroupBox6.Controls.Add(Me.chkCOAauto)
        Me.GroupBox6.Location = New System.Drawing.Point(12, 8)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox6.Size = New System.Drawing.Size(761, 308)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Chart of Account Settings"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(291, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(105, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Desired Format"
        '
        'txtCOAFormat
        '
        Me.txtCOAFormat.Location = New System.Drawing.Point(402, 47)
        Me.txtCOAFormat.Name = "txtCOAFormat"
        Me.txtCOAFormat.Size = New System.Drawing.Size(100, 23)
        Me.txtCOAFormat.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(27, 50)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(92, 17)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Format Type "
        '
        'cbCOAformat
        '
        Me.cbCOAformat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCOAformat.FormattingEnabled = True
        Me.cbCOAformat.Items.AddRange(New Object() {"Auto Increment"})
        Me.cbCOAformat.Location = New System.Drawing.Point(125, 47)
        Me.cbCOAformat.Name = "cbCOAformat"
        Me.cbCOAformat.Size = New System.Drawing.Size(137, 24)
        Me.cbCOAformat.TabIndex = 2
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.BackColor = System.Drawing.Color.White
        Me.GroupBox5.Controls.Add(Me.btnCatDown)
        Me.GroupBox5.Controls.Add(Me.btnCatUp)
        Me.GroupBox5.Controls.Add(Me.Button4)
        Me.GroupBox5.Controls.Add(Me.Button3)
        Me.GroupBox5.Controls.Add(Me.lvType)
        Me.GroupBox5.Location = New System.Drawing.Point(18, 73)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox5.Size = New System.Drawing.Size(726, 153)
        Me.GroupBox5.TabIndex = 1
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Account Type"
        '
        'btnCatDown
        '
        Me.btnCatDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatDown.Location = New System.Drawing.Point(421, 48)
        Me.btnCatDown.Name = "btnCatDown"
        Me.btnCatDown.Size = New System.Drawing.Size(28, 27)
        Me.btnCatDown.TabIndex = 1354
        Me.btnCatDown.Text = "↓"
        Me.btnCatDown.UseVisualStyleBackColor = True
        '
        'btnCatUp
        '
        Me.btnCatUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCatUp.Location = New System.Drawing.Point(421, 19)
        Me.btnCatUp.Name = "btnCatUp"
        Me.btnCatUp.Size = New System.Drawing.Size(28, 27)
        Me.btnCatUp.TabIndex = 1353
        Me.btnCatUp.Text = "↑"
        Me.btnCatUp.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button4.Location = New System.Drawing.Point(421, 109)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(112, 30)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Remove"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.Button3.Location = New System.Drawing.Point(421, 77)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 30)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "Add"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'lvType
        '
        Me.lvType.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chType, Me.chDesc, Me.chDigit, Me.chOrder})
        Me.lvType.HideSelection = False
        Me.lvType.Location = New System.Drawing.Point(11, 19)
        Me.lvType.Name = "lvType"
        Me.lvType.Size = New System.Drawing.Size(405, 119)
        Me.lvType.TabIndex = 0
        Me.lvType.UseCompatibleStateImageBehavior = False
        Me.lvType.View = System.Windows.Forms.View.Details
        '
        'chType
        '
        Me.chType.Text = "Type"
        Me.chType.Width = 88
        '
        'chDesc
        '
        Me.chDesc.Text = "Description"
        Me.chDesc.Width = 221
        '
        'chDigit
        '
        Me.chDigit.Text = "Digit"
        Me.chDigit.Width = 90
        '
        'chOrder
        '
        Me.chOrder.Text = "Order"
        Me.chOrder.Width = 0
        '
        'chkCOAauto
        '
        Me.chkCOAauto.AutoSize = True
        Me.chkCOAauto.Location = New System.Drawing.Point(30, 27)
        Me.chkCOAauto.Name = "chkCOAauto"
        Me.chkCOAauto.Size = New System.Drawing.Size(266, 21)
        Me.chkCOAauto.TabIndex = 0
        Me.chkCOAauto.Text = "Generate Account Code Automatically"
        Me.chkCOAauto.UseVisualStyleBackColor = True
        '
        'tpGeneral
        '
        Me.tpGeneral.Controls.Add(Me.GroupBox7)
        Me.tpGeneral.Controls.Add(Me.dgvTransID)
        Me.tpGeneral.Location = New System.Drawing.Point(5, 4)
        Me.tpGeneral.Name = "tpGeneral"
        Me.tpGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.tpGeneral.Size = New System.Drawing.Size(785, 609)
        Me.tpGeneral.TabIndex = 2
        Me.tpGeneral.Text = "TabPage1"
        Me.tpGeneral.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.chkReversalEntries)
        Me.GroupBox7.Controls.Add(Me.chkForApproval)
        Me.GroupBox7.Controls.Add(Me.chkTransAuto)
        Me.GroupBox7.Controls.Add(Me.dgvTransDetail)
        Me.GroupBox7.Controls.Add(Me.chkGlobal)
        Me.GroupBox7.Controls.Add(Me.dgvTransDetailsAll)
        Me.GroupBox7.Location = New System.Drawing.Point(326, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(448, 350)
        Me.GroupBox7.TabIndex = 2
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Transaction ID Details"
        '
        'chkReversalEntries
        '
        Me.chkReversalEntries.AutoSize = True
        Me.chkReversalEntries.Location = New System.Drawing.Point(26, 86)
        Me.chkReversalEntries.Name = "chkReversalEntries"
        Me.chkReversalEntries.Size = New System.Drawing.Size(179, 21)
        Me.chkReversalEntries.TabIndex = 8
        Me.chkReversalEntries.Text = "Enable Reversal Entries"
        Me.chkReversalEntries.UseVisualStyleBackColor = True
        '
        'chkForApproval
        '
        Me.chkForApproval.AutoSize = True
        Me.chkForApproval.Location = New System.Drawing.Point(26, 66)
        Me.chkForApproval.Name = "chkForApproval"
        Me.chkForApproval.Size = New System.Drawing.Size(131, 21)
        Me.chkForApproval.TabIndex = 7
        Me.chkForApproval.Text = "Enable Approval"
        Me.chkForApproval.UseVisualStyleBackColor = True
        '
        'chkTransAuto
        '
        Me.chkTransAuto.AutoSize = True
        Me.chkTransAuto.Location = New System.Drawing.Point(26, 27)
        Me.chkTransAuto.Name = "chkTransAuto"
        Me.chkTransAuto.Size = New System.Drawing.Size(270, 21)
        Me.chkTransAuto.TabIndex = 5
        Me.chkTransAuto.Text = "Generate Transaction ID Automatically"
        Me.chkTransAuto.UseVisualStyleBackColor = True
        '
        'dgvTransDetail
        '
        Me.dgvTransDetail.AllowUserToAddRows = False
        Me.dgvTransDetail.AllowUserToDeleteRows = False
        Me.dgvTransDetail.BackgroundColor = System.Drawing.Color.White
        Me.dgvTransDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransDetail.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcTransBranch, Me.dgcTransBusType, Me.dgcTransPrefix, Me.dgcTransDigits, Me.dgcTransStart})
        Me.dgvTransDetail.Location = New System.Drawing.Point(26, 113)
        Me.dgvTransDetail.Name = "dgvTransDetail"
        Me.dgvTransDetail.RowHeadersVisible = False
        Me.dgvTransDetail.Size = New System.Drawing.Size(396, 173)
        Me.dgvTransDetail.TabIndex = 1
        '
        'dgcTransBranch
        '
        Me.dgcTransBranch.HeaderText = "Branch"
        Me.dgcTransBranch.Name = "dgcTransBranch"
        Me.dgcTransBranch.ReadOnly = True
        '
        'dgcTransBusType
        '
        Me.dgcTransBusType.HeaderText = "Business Type"
        Me.dgcTransBusType.Name = "dgcTransBusType"
        Me.dgcTransBusType.ReadOnly = True
        Me.dgcTransBusType.Width = 140
        '
        'dgcTransPrefix
        '
        Me.dgcTransPrefix.HeaderText = "Prefix"
        Me.dgcTransPrefix.Name = "dgcTransPrefix"
        Me.dgcTransPrefix.Width = 60
        '
        'dgcTransDigits
        '
        Me.dgcTransDigits.HeaderText = "Digits"
        Me.dgcTransDigits.Name = "dgcTransDigits"
        Me.dgcTransDigits.Width = 60
        '
        'dgcTransStart
        '
        Me.dgcTransStart.HeaderText = "Start"
        Me.dgcTransStart.Name = "dgcTransStart"
        '
        'chkGlobal
        '
        Me.chkGlobal.AutoSize = True
        Me.chkGlobal.Location = New System.Drawing.Point(26, 46)
        Me.chkGlobal.Name = "chkGlobal"
        Me.chkGlobal.Size = New System.Drawing.Size(112, 21)
        Me.chkGlobal.TabIndex = 2
        Me.chkGlobal.Text = "Global Series"
        Me.chkGlobal.UseVisualStyleBackColor = True
        '
        'dgvTransDetailsAll
        '
        Me.dgvTransDetailsAll.AllowUserToAddRows = False
        Me.dgvTransDetailsAll.AllowUserToDeleteRows = False
        Me.dgvTransDetailsAll.BackgroundColor = System.Drawing.Color.White
        Me.dgvTransDetailsAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransDetailsAll.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcTransAllType, Me.dgcTransAllBranch, Me.dgcTransAllBus, Me.dgcTransAllPrefix, Me.dgcTransAlldigit, Me.dgcTransAllStart})
        Me.dgvTransDetailsAll.Location = New System.Drawing.Point(26, 113)
        Me.dgvTransDetailsAll.Name = "dgvTransDetailsAll"
        Me.dgvTransDetailsAll.RowHeadersVisible = False
        Me.dgvTransDetailsAll.Size = New System.Drawing.Size(396, 173)
        Me.dgvTransDetailsAll.TabIndex = 6
        Me.dgvTransDetailsAll.Visible = False
        '
        'dgcTransAllType
        '
        Me.dgcTransAllType.HeaderText = "Type"
        Me.dgcTransAllType.Name = "dgcTransAllType"
        '
        'dgcTransAllBranch
        '
        Me.dgcTransAllBranch.HeaderText = "Branch"
        Me.dgcTransAllBranch.Name = "dgcTransAllBranch"
        '
        'dgcTransAllBus
        '
        Me.dgcTransAllBus.HeaderText = "Business Type"
        Me.dgcTransAllBus.Name = "dgcTransAllBus"
        Me.dgcTransAllBus.Width = 140
        '
        'dgcTransAllPrefix
        '
        Me.dgcTransAllPrefix.HeaderText = "Prefix"
        Me.dgcTransAllPrefix.Name = "dgcTransAllPrefix"
        Me.dgcTransAllPrefix.Width = 60
        '
        'dgcTransAlldigit
        '
        Me.dgcTransAlldigit.HeaderText = "Digits"
        Me.dgcTransAlldigit.Name = "dgcTransAlldigit"
        Me.dgcTransAlldigit.Width = 60
        '
        'dgcTransAllStart
        '
        Me.dgcTransAllStart.HeaderText = "Start"
        Me.dgcTransAllStart.Name = "dgcTransAllStart"
        '
        'dgvTransID
        '
        Me.dgvTransID.AllowDrop = True
        Me.dgvTransID.AllowUserToAddRows = False
        Me.dgvTransID.AllowUserToDeleteRows = False
        Me.dgvTransID.AllowUserToOrderColumns = True
        Me.dgvTransID.AllowUserToResizeColumns = False
        Me.dgvTransID.AllowUserToResizeRows = False
        Me.dgvTransID.BackgroundColor = System.Drawing.Color.White
        Me.dgvTransID.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTransID.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcTransType, Me.dgcTransDesc, Me.dgcTransAuto, Me.dgcTransGlobal, Me.dgcForApproval, Me.dgcForReversal})
        Me.dgvTransID.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvTransID.Location = New System.Drawing.Point(6, 6)
        Me.dgvTransID.MultiSelect = False
        Me.dgvTransID.Name = "dgvTransID"
        Me.dgvTransID.RowHeadersVisible = False
        Me.dgvTransID.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvTransID.Size = New System.Drawing.Size(314, 350)
        Me.dgvTransID.TabIndex = 0
        '
        'dgcTransType
        '
        Me.dgcTransType.HeaderText = "Type"
        Me.dgcTransType.Name = "dgcTransType"
        Me.dgcTransType.Width = 50
        '
        'dgcTransDesc
        '
        Me.dgcTransDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcTransDesc.HeaderText = "Description"
        Me.dgcTransDesc.Name = "dgcTransDesc"
        '
        'dgcTransAuto
        '
        Me.dgcTransAuto.HeaderText = "Auto"
        Me.dgcTransAuto.Name = "dgcTransAuto"
        Me.dgcTransAuto.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgcTransAuto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.dgcTransAuto.Visible = False
        '
        'dgcTransGlobal
        '
        Me.dgcTransGlobal.HeaderText = "Global"
        Me.dgcTransGlobal.Name = "dgcTransGlobal"
        Me.dgcTransGlobal.Visible = False
        '
        'dgcForApproval
        '
        Me.dgcForApproval.HeaderText = "For Approval"
        Me.dgcForApproval.Name = "dgcForApproval"
        Me.dgcForApproval.Visible = False
        '
        'dgcForReversal
        '
        Me.dgcForReversal.HeaderText = "Reversal Entries"
        Me.dgcForReversal.Name = "dgcForReversal"
        Me.dgcForReversal.Visible = False
        '
        'tpVCE
        '
        Me.tpVCE.Controls.Add(Me.gbVCE)
        Me.tpVCE.Location = New System.Drawing.Point(5, 4)
        Me.tpVCE.Name = "tpVCE"
        Me.tpVCE.Padding = New System.Windows.Forms.Padding(3)
        Me.tpVCE.Size = New System.Drawing.Size(785, 609)
        Me.tpVCE.TabIndex = 3
        Me.tpVCE.Text = "VCE"
        Me.tpVCE.UseVisualStyleBackColor = True
        '
        'gbVCE
        '
        Me.gbVCE.BackColor = System.Drawing.Color.White
        Me.gbVCE.Controls.Add(Me.NumericUpDown5)
        Me.gbVCE.Controls.Add(Me.Label9)
        Me.gbVCE.Controls.Add(Me.Label8)
        Me.gbVCE.Controls.Add(Me.TextBox1)
        Me.gbVCE.Controls.Add(Me.CheckBox12)
        Me.gbVCE.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbVCE.Location = New System.Drawing.Point(3, 3)
        Me.gbVCE.Margin = New System.Windows.Forms.Padding(0)
        Me.gbVCE.Name = "gbVCE"
        Me.gbVCE.Padding = New System.Windows.Forms.Padding(0)
        Me.gbVCE.Size = New System.Drawing.Size(779, 603)
        Me.gbVCE.TabIndex = 3
        Me.gbVCE.TabStop = False
        Me.gbVCE.Text = "VCE Settings"
        '
        'NumericUpDown5
        '
        Me.NumericUpDown5.Location = New System.Drawing.Point(120, 52)
        Me.NumericUpDown5.Name = "NumericUpDown5"
        Me.NumericUpDown5.Size = New System.Drawing.Size(100, 23)
        Me.NumericUpDown5.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(66, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 17)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Digits :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(66, 78)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(51, 17)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Prefix :"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(120, 75)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 23)
        Me.TextBox1.TabIndex = 4
        '
        'CheckBox12
        '
        Me.CheckBox12.AutoSize = True
        Me.CheckBox12.Location = New System.Drawing.Point(30, 27)
        Me.CheckBox12.Name = "CheckBox12"
        Me.CheckBox12.Size = New System.Drawing.Size(242, 21)
        Me.CheckBox12.TabIndex = 0
        Me.CheckBox12.Text = "Generate VCE Code Automatically"
        Me.CheckBox12.UseVisualStyleBackColor = True
        '
        'tpEntries
        '
        Me.tpEntries.Controls.Add(Me.PanelEntries)
        Me.tpEntries.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.tpEntries.Location = New System.Drawing.Point(5, 4)
        Me.tpEntries.Name = "tpEntries"
        Me.tpEntries.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEntries.Size = New System.Drawing.Size(785, 609)
        Me.tpEntries.TabIndex = 4
        Me.tpEntries.Text = "a"
        Me.tpEntries.UseVisualStyleBackColor = True
        '
        'PanelEntries
        '
        Me.PanelEntries.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelEntries.AutoScroll = True
        Me.PanelEntries.Controls.Add(Me.GroupBox28)
        Me.PanelEntries.Controls.Add(Me.GroupBox27)
        Me.PanelEntries.Controls.Add(Me.GroupBox24)
        Me.PanelEntries.Controls.Add(Me.GroupBox23)
        Me.PanelEntries.Controls.Add(Me.GroupBox19)
        Me.PanelEntries.Controls.Add(Me.GroupBox22)
        Me.PanelEntries.Controls.Add(Me.gbAP)
        Me.PanelEntries.Controls.Add(Me.GroupBox14)
        Me.PanelEntries.Controls.Add(Me.GroupBox9)
        Me.PanelEntries.Controls.Add(Me.GroupBox11)
        Me.PanelEntries.Location = New System.Drawing.Point(3, 3)
        Me.PanelEntries.Name = "PanelEntries"
        Me.PanelEntries.Size = New System.Drawing.Size(776, 595)
        Me.PanelEntries.TabIndex = 20
        '
        'GroupBox28
        '
        Me.GroupBox28.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox28.BackColor = System.Drawing.Color.White
        Me.GroupBox28.Controls.Add(Me.txtBM_SRCode)
        Me.GroupBox28.Controls.Add(Me.txtBM_SRTitle)
        Me.GroupBox28.Controls.Add(Me.Label85)
        Me.GroupBox28.Controls.Add(Me.txtBM_APCode)
        Me.GroupBox28.Controls.Add(Me.txtBM_APTitle)
        Me.GroupBox28.Controls.Add(Me.Label88)
        Me.GroupBox28.Controls.Add(Me.txtBM_COSCode)
        Me.GroupBox28.Controls.Add(Me.txtBM_COSTitle)
        Me.GroupBox28.Controls.Add(Me.Label89)
        Me.GroupBox28.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox28.Location = New System.Drawing.Point(5, 1687)
        Me.GroupBox28.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox28.Name = "GroupBox28"
        Me.GroupBox28.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox28.Size = New System.Drawing.Size(671, 113)
        Me.GroupBox28.TabIndex = 35
        Me.GroupBox28.TabStop = False
        Me.GroupBox28.Text = "Booking"
        '
        'txtBM_SRCode
        '
        Me.txtBM_SRCode.Location = New System.Drawing.Point(555, 20)
        Me.txtBM_SRCode.Name = "txtBM_SRCode"
        Me.txtBM_SRCode.Size = New System.Drawing.Size(80, 21)
        Me.txtBM_SRCode.TabIndex = 42
        '
        'txtBM_SRTitle
        '
        Me.txtBM_SRTitle.Location = New System.Drawing.Point(215, 20)
        Me.txtBM_SRTitle.Name = "txtBM_SRTitle"
        Me.txtBM_SRTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtBM_SRTitle.TabIndex = 41
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Location = New System.Drawing.Point(61, 20)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(151, 15)
        Me.Label85.TabIndex = 40
        Me.Label85.Text = "Service Revenue Account :"
        '
        'txtBM_APCode
        '
        Me.txtBM_APCode.Location = New System.Drawing.Point(555, 74)
        Me.txtBM_APCode.Name = "txtBM_APCode"
        Me.txtBM_APCode.Size = New System.Drawing.Size(80, 21)
        Me.txtBM_APCode.TabIndex = 27
        '
        'txtBM_APTitle
        '
        Me.txtBM_APTitle.Location = New System.Drawing.Point(215, 74)
        Me.txtBM_APTitle.Name = "txtBM_APTitle"
        Me.txtBM_APTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtBM_APTitle.TabIndex = 26
        '
        'Label88
        '
        Me.Label88.AutoSize = True
        Me.Label88.Location = New System.Drawing.Point(57, 77)
        Me.Label88.Name = "Label88"
        Me.Label88.Size = New System.Drawing.Size(155, 15)
        Me.Label88.TabIndex = 25
        Me.Label88.Text = "Accounts Payable Account :"
        '
        'txtBM_COSCode
        '
        Me.txtBM_COSCode.Location = New System.Drawing.Point(555, 47)
        Me.txtBM_COSCode.Name = "txtBM_COSCode"
        Me.txtBM_COSCode.Size = New System.Drawing.Size(80, 21)
        Me.txtBM_COSCode.TabIndex = 24
        '
        'txtBM_COSTitle
        '
        Me.txtBM_COSTitle.Location = New System.Drawing.Point(215, 47)
        Me.txtBM_COSTitle.Name = "txtBM_COSTitle"
        Me.txtBM_COSTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtBM_COSTitle.TabIndex = 8
        '
        'Label89
        '
        Me.Label89.AutoSize = True
        Me.Label89.Location = New System.Drawing.Point(67, 51)
        Me.Label89.Name = "Label89"
        Me.Label89.Size = New System.Drawing.Size(145, 15)
        Me.Label89.TabIndex = 6
        Me.Label89.Text = "Cost of Services Account :"
        '
        'GroupBox27
        '
        Me.GroupBox27.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox27.BackColor = System.Drawing.Color.White
        Me.GroupBox27.Controls.Add(Me.txtInv_VarianceAccntCode)
        Me.GroupBox27.Controls.Add(Me.txtInv_VarianceAccntTitle)
        Me.GroupBox27.Controls.Add(Me.Label53)
        Me.GroupBox27.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox27.Location = New System.Drawing.Point(5, 757)
        Me.GroupBox27.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox27.Name = "GroupBox27"
        Me.GroupBox27.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox27.Size = New System.Drawing.Size(671, 64)
        Me.GroupBox27.TabIndex = 34
        Me.GroupBox27.TabStop = False
        Me.GroupBox27.Text = "Inventory"
        '
        'txtInv_VarianceAccntCode
        '
        Me.txtInv_VarianceAccntCode.Location = New System.Drawing.Point(555, 28)
        Me.txtInv_VarianceAccntCode.Name = "txtInv_VarianceAccntCode"
        Me.txtInv_VarianceAccntCode.Size = New System.Drawing.Size(80, 21)
        Me.txtInv_VarianceAccntCode.TabIndex = 34
        '
        'txtInv_VarianceAccntTitle
        '
        Me.txtInv_VarianceAccntTitle.Location = New System.Drawing.Point(215, 28)
        Me.txtInv_VarianceAccntTitle.Name = "txtInv_VarianceAccntTitle"
        Me.txtInv_VarianceAccntTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtInv_VarianceAccntTitle.TabIndex = 33
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(51, 28)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(158, 15)
        Me.Label53.TabIndex = 32
        Me.Label53.Text = "Inventory Variance Account :"
        '
        'GroupBox24
        '
        Me.GroupBox24.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox24.BackColor = System.Drawing.Color.White
        Me.GroupBox24.Controls.Add(Me.txtLM_AdvanceRentCode)
        Me.GroupBox24.Controls.Add(Me.txtLM_AdvanceRentTitle)
        Me.GroupBox24.Controls.Add(Me.Label72)
        Me.GroupBox24.Controls.Add(Me.txtLM_RentalIncomeCode)
        Me.GroupBox24.Controls.Add(Me.txtLM_RentalIncomeTitle)
        Me.GroupBox24.Controls.Add(Me.Label74)
        Me.GroupBox24.Controls.Add(Me.txtLM_NFCode)
        Me.GroupBox24.Controls.Add(Me.txtLM_NFTitle)
        Me.GroupBox24.Controls.Add(Me.Label76)
        Me.GroupBox24.Controls.Add(Me.txtLM_DSTCode)
        Me.GroupBox24.Controls.Add(Me.txtLM_DSTTitle)
        Me.GroupBox24.Controls.Add(Me.Label77)
        Me.GroupBox24.Controls.Add(Me.txtLM_DepositCode)
        Me.GroupBox24.Controls.Add(Me.txtLM_DepositTitle)
        Me.GroupBox24.Controls.Add(Me.Label78)
        Me.GroupBox24.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox24.Location = New System.Drawing.Point(5, 1147)
        Me.GroupBox24.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox24.Name = "GroupBox24"
        Me.GroupBox24.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox24.Size = New System.Drawing.Size(671, 166)
        Me.GroupBox24.TabIndex = 33
        Me.GroupBox24.TabStop = False
        Me.GroupBox24.Text = "Lease Management"
        '
        'txtLM_AdvanceRentCode
        '
        Me.txtLM_AdvanceRentCode.Location = New System.Drawing.Point(555, 20)
        Me.txtLM_AdvanceRentCode.Name = "txtLM_AdvanceRentCode"
        Me.txtLM_AdvanceRentCode.Size = New System.Drawing.Size(80, 21)
        Me.txtLM_AdvanceRentCode.TabIndex = 42
        '
        'txtLM_AdvanceRentTitle
        '
        Me.txtLM_AdvanceRentTitle.Location = New System.Drawing.Point(215, 20)
        Me.txtLM_AdvanceRentTitle.Name = "txtLM_AdvanceRentTitle"
        Me.txtLM_AdvanceRentTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtLM_AdvanceRentTitle.TabIndex = 41
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Location = New System.Drawing.Point(68, 23)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(134, 15)
        Me.Label72.TabIndex = 40
        Me.Label72.Text = "Advance Rent Account :"
        '
        'txtLM_RentalIncomeCode
        '
        Me.txtLM_RentalIncomeCode.Location = New System.Drawing.Point(555, 128)
        Me.txtLM_RentalIncomeCode.Name = "txtLM_RentalIncomeCode"
        Me.txtLM_RentalIncomeCode.Size = New System.Drawing.Size(80, 21)
        Me.txtLM_RentalIncomeCode.TabIndex = 36
        '
        'txtLM_RentalIncomeTitle
        '
        Me.txtLM_RentalIncomeTitle.Location = New System.Drawing.Point(215, 128)
        Me.txtLM_RentalIncomeTitle.Name = "txtLM_RentalIncomeTitle"
        Me.txtLM_RentalIncomeTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtLM_RentalIncomeTitle.TabIndex = 35
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(63, 131)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(139, 15)
        Me.Label74.TabIndex = 34
        Me.Label74.Text = "Rental Income Account :"
        '
        'txtLM_NFCode
        '
        Me.txtLM_NFCode.Location = New System.Drawing.Point(555, 101)
        Me.txtLM_NFCode.Name = "txtLM_NFCode"
        Me.txtLM_NFCode.Size = New System.Drawing.Size(80, 21)
        Me.txtLM_NFCode.TabIndex = 30
        '
        'txtLM_NFTitle
        '
        Me.txtLM_NFTitle.Location = New System.Drawing.Point(215, 101)
        Me.txtLM_NFTitle.Name = "txtLM_NFTitle"
        Me.txtLM_NFTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtLM_NFTitle.TabIndex = 29
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(124, 104)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(78, 15)
        Me.Label76.TabIndex = 28
        Me.Label76.Text = "NF Account : "
        '
        'txtLM_DSTCode
        '
        Me.txtLM_DSTCode.Location = New System.Drawing.Point(555, 74)
        Me.txtLM_DSTCode.Name = "txtLM_DSTCode"
        Me.txtLM_DSTCode.Size = New System.Drawing.Size(80, 21)
        Me.txtLM_DSTCode.TabIndex = 27
        '
        'txtLM_DSTTitle
        '
        Me.txtLM_DSTTitle.Location = New System.Drawing.Point(215, 74)
        Me.txtLM_DSTTitle.Name = "txtLM_DSTTitle"
        Me.txtLM_DSTTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtLM_DSTTitle.TabIndex = 26
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(119, 77)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(83, 15)
        Me.Label77.TabIndex = 25
        Me.Label77.Text = "DST Account :"
        '
        'txtLM_DepositCode
        '
        Me.txtLM_DepositCode.Location = New System.Drawing.Point(555, 47)
        Me.txtLM_DepositCode.Name = "txtLM_DepositCode"
        Me.txtLM_DepositCode.Size = New System.Drawing.Size(80, 21)
        Me.txtLM_DepositCode.TabIndex = 24
        '
        'txtLM_DepositTitle
        '
        Me.txtLM_DepositTitle.Location = New System.Drawing.Point(215, 47)
        Me.txtLM_DepositTitle.Name = "txtLM_DepositTitle"
        Me.txtLM_DepositTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtLM_DepositTitle.TabIndex = 8
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(101, 51)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(101, 15)
        Me.Label78.TabIndex = 6
        Me.Label78.Text = "Deposit Account :"
        '
        'GroupBox23
        '
        Me.GroupBox23.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox23.BackColor = System.Drawing.Color.White
        Me.GroupBox23.Controls.Add(Me.txtRE_ARCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_ARTitle)
        Me.GroupBox23.Controls.Add(Me.Label95)
        Me.GroupBox23.Controls.Add(Me.txtRE_ARMiscFeeCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_ARMiscFeeTitle)
        Me.GroupBox23.Controls.Add(Me.Label94)
        Me.GroupBox23.Controls.Add(Me.txtRE_MiscFeeCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_MiscFeeTitle)
        Me.GroupBox23.Controls.Add(Me.Label93)
        Me.GroupBox23.Controls.Add(Me.txtRE_OutputVatCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_OutputVatTitle)
        Me.GroupBox23.Controls.Add(Me.Label92)
        Me.GroupBox23.Controls.Add(Me.txtRE_NetOfVATCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_NetOfVATTitle)
        Me.GroupBox23.Controls.Add(Me.Label91)
        Me.GroupBox23.Controls.Add(Me.txtRE_SalesCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_SalesTitle)
        Me.GroupBox23.Controls.Add(Me.Label90)
        Me.GroupBox23.Controls.Add(Me.txtRE_CommissionCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_CommissionTitle)
        Me.GroupBox23.Controls.Add(Me.Label87)
        Me.GroupBox23.Controls.Add(Me.txtRE_AccountCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_AccountTitle)
        Me.GroupBox23.Controls.Add(Me.Label62)
        Me.GroupBox23.Controls.Add(Me.txtRE_ReserveCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_ReserveTitle)
        Me.GroupBox23.Controls.Add(Me.Label64)
        Me.GroupBox23.Controls.Add(Me.txtRE_PenaltyCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_PenaltyTitle)
        Me.GroupBox23.Controls.Add(Me.Label69)
        Me.GroupBox23.Controls.Add(Me.txtRE_InterestCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_InterestTitle)
        Me.GroupBox23.Controls.Add(Me.Label70)
        Me.GroupBox23.Controls.Add(Me.txtRE_EquityCode)
        Me.GroupBox23.Controls.Add(Me.txtRE_EquityTitle)
        Me.GroupBox23.Controls.Add(Me.Label71)
        Me.GroupBox23.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox23.Location = New System.Drawing.Point(5, 1322)
        Me.GroupBox23.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox23.Name = "GroupBox23"
        Me.GroupBox23.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox23.Size = New System.Drawing.Size(671, 365)
        Me.GroupBox23.TabIndex = 32
        Me.GroupBox23.TabStop = False
        Me.GroupBox23.Text = "Real Estate"
        '
        'txtRE_ARCode
        '
        Me.txtRE_ARCode.Location = New System.Drawing.Point(555, 315)
        Me.txtRE_ARCode.Name = "txtRE_ARCode"
        Me.txtRE_ARCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_ARCode.TabIndex = 63
        '
        'txtRE_ARTitle
        '
        Me.txtRE_ARTitle.Location = New System.Drawing.Point(215, 315)
        Me.txtRE_ARTitle.Name = "txtRE_ARTitle"
        Me.txtRE_ARTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_ARTitle.TabIndex = 62
        '
        'Label95
        '
        Me.Label95.AutoSize = True
        Me.Label95.Location = New System.Drawing.Point(83, 315)
        Me.Label95.Name = "Label95"
        Me.Label95.Size = New System.Drawing.Size(126, 15)
        Me.Label95.TabIndex = 61
        Me.Label95.Text = "Accounts Receivable :"
        '
        'txtRE_ARMiscFeeCode
        '
        Me.txtRE_ARMiscFeeCode.Location = New System.Drawing.Point(555, 288)
        Me.txtRE_ARMiscFeeCode.Name = "txtRE_ARMiscFeeCode"
        Me.txtRE_ARMiscFeeCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_ARMiscFeeCode.TabIndex = 60
        '
        'txtRE_ARMiscFeeTitle
        '
        Me.txtRE_ARMiscFeeTitle.Location = New System.Drawing.Point(215, 288)
        Me.txtRE_ARMiscFeeTitle.Name = "txtRE_ARMiscFeeTitle"
        Me.txtRE_ARMiscFeeTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_ARMiscFeeTitle.TabIndex = 59
        '
        'Label94
        '
        Me.Label94.AutoSize = True
        Me.Label94.Location = New System.Drawing.Point(73, 288)
        Me.Label94.Name = "Label94"
        Me.Label94.Size = New System.Drawing.Size(136, 15)
        Me.Label94.TabIndex = 58
        Me.Label94.Text = "AR Miscellaneous Fee :"
        '
        'txtRE_MiscFeeCode
        '
        Me.txtRE_MiscFeeCode.Location = New System.Drawing.Point(555, 261)
        Me.txtRE_MiscFeeCode.Name = "txtRE_MiscFeeCode"
        Me.txtRE_MiscFeeCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_MiscFeeCode.TabIndex = 57
        '
        'txtRE_MiscFeeTitle
        '
        Me.txtRE_MiscFeeTitle.Location = New System.Drawing.Point(215, 261)
        Me.txtRE_MiscFeeTitle.Name = "txtRE_MiscFeeTitle"
        Me.txtRE_MiscFeeTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_MiscFeeTitle.TabIndex = 56
        '
        'Label93
        '
        Me.Label93.AutoSize = True
        Me.Label93.Location = New System.Drawing.Point(92, 261)
        Me.Label93.Name = "Label93"
        Me.Label93.Size = New System.Drawing.Size(117, 15)
        Me.Label93.TabIndex = 55
        Me.Label93.Text = "Miscellaneous Fee :"
        '
        'txtRE_OutputVatCode
        '
        Me.txtRE_OutputVatCode.Location = New System.Drawing.Point(555, 234)
        Me.txtRE_OutputVatCode.Name = "txtRE_OutputVatCode"
        Me.txtRE_OutputVatCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_OutputVatCode.TabIndex = 54
        '
        'txtRE_OutputVatTitle
        '
        Me.txtRE_OutputVatTitle.Location = New System.Drawing.Point(215, 234)
        Me.txtRE_OutputVatTitle.Name = "txtRE_OutputVatTitle"
        Me.txtRE_OutputVatTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_OutputVatTitle.TabIndex = 53
        '
        'Label92
        '
        Me.Label92.AutoSize = True
        Me.Label92.Location = New System.Drawing.Point(136, 234)
        Me.Label92.Name = "Label92"
        Me.Label92.Size = New System.Drawing.Size(73, 15)
        Me.Label92.TabIndex = 52
        Me.Label92.Text = "Output VAT :"
        '
        'txtRE_NetOfVATCode
        '
        Me.txtRE_NetOfVATCode.Location = New System.Drawing.Point(555, 207)
        Me.txtRE_NetOfVATCode.Name = "txtRE_NetOfVATCode"
        Me.txtRE_NetOfVATCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_NetOfVATCode.TabIndex = 51
        '
        'txtRE_NetOfVATTitle
        '
        Me.txtRE_NetOfVATTitle.Location = New System.Drawing.Point(215, 207)
        Me.txtRE_NetOfVATTitle.Name = "txtRE_NetOfVATTitle"
        Me.txtRE_NetOfVATTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_NetOfVATTitle.TabIndex = 50
        '
        'Label91
        '
        Me.Label91.AutoSize = True
        Me.Label91.Location = New System.Drawing.Point(137, 207)
        Me.Label91.Name = "Label91"
        Me.Label91.Size = New System.Drawing.Size(72, 15)
        Me.Label91.TabIndex = 49
        Me.Label91.Text = "Net of VAT  :"
        '
        'txtRE_SalesCode
        '
        Me.txtRE_SalesCode.Location = New System.Drawing.Point(555, 180)
        Me.txtRE_SalesCode.Name = "txtRE_SalesCode"
        Me.txtRE_SalesCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_SalesCode.TabIndex = 48
        '
        'txtRE_SalesTitle
        '
        Me.txtRE_SalesTitle.Location = New System.Drawing.Point(215, 180)
        Me.txtRE_SalesTitle.Name = "txtRE_SalesTitle"
        Me.txtRE_SalesTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_SalesTitle.TabIndex = 47
        '
        'Label90
        '
        Me.Label90.AutoSize = True
        Me.Label90.Location = New System.Drawing.Point(103, 180)
        Me.Label90.Name = "Label90"
        Me.Label90.Size = New System.Drawing.Size(104, 15)
        Me.Label90.TabIndex = 46
        Me.Label90.Text = "Real State Sales :"
        '
        'txtRE_CommissionCode
        '
        Me.txtRE_CommissionCode.Location = New System.Drawing.Point(555, 153)
        Me.txtRE_CommissionCode.Name = "txtRE_CommissionCode"
        Me.txtRE_CommissionCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_CommissionCode.TabIndex = 45
        '
        'txtRE_CommissionTitle
        '
        Me.txtRE_CommissionTitle.Location = New System.Drawing.Point(215, 153)
        Me.txtRE_CommissionTitle.Name = "txtRE_CommissionTitle"
        Me.txtRE_CommissionTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_CommissionTitle.TabIndex = 44
        '
        'Label87
        '
        Me.Label87.AutoSize = True
        Me.Label87.Location = New System.Drawing.Point(81, 156)
        Me.Label87.Name = "Label87"
        Me.Label87.Size = New System.Drawing.Size(128, 15)
        Me.Label87.TabIndex = 43
        Me.Label87.Text = "Commission Account :"
        '
        'txtRE_AccountCode
        '
        Me.txtRE_AccountCode.Location = New System.Drawing.Point(555, 20)
        Me.txtRE_AccountCode.Name = "txtRE_AccountCode"
        Me.txtRE_AccountCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_AccountCode.TabIndex = 42
        '
        'txtRE_AccountTitle
        '
        Me.txtRE_AccountTitle.Location = New System.Drawing.Point(215, 20)
        Me.txtRE_AccountTitle.Name = "txtRE_AccountTitle"
        Me.txtRE_AccountTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_AccountTitle.TabIndex = 41
        '
        'Label62
        '
        Me.Label62.AutoSize = True
        Me.Label62.Location = New System.Drawing.Point(87, 20)
        Me.Label62.Name = "Label62"
        Me.Label62.Size = New System.Drawing.Size(122, 15)
        Me.Label62.TabIndex = 40
        Me.Label62.Text = "Real Estate Account :"
        '
        'txtRE_ReserveCode
        '
        Me.txtRE_ReserveCode.Location = New System.Drawing.Point(555, 128)
        Me.txtRE_ReserveCode.Name = "txtRE_ReserveCode"
        Me.txtRE_ReserveCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_ReserveCode.TabIndex = 36
        '
        'txtRE_ReserveTitle
        '
        Me.txtRE_ReserveTitle.Location = New System.Drawing.Point(215, 128)
        Me.txtRE_ReserveTitle.Name = "txtRE_ReserveTitle"
        Me.txtRE_ReserveTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_ReserveTitle.TabIndex = 35
        '
        'Label64
        '
        Me.Label64.AutoSize = True
        Me.Label64.Location = New System.Drawing.Point(105, 131)
        Me.Label64.Name = "Label64"
        Me.Label64.Size = New System.Drawing.Size(104, 15)
        Me.Label64.TabIndex = 34
        Me.Label64.Text = "Reserve Account :"
        '
        'txtRE_PenaltyCode
        '
        Me.txtRE_PenaltyCode.Location = New System.Drawing.Point(555, 101)
        Me.txtRE_PenaltyCode.Name = "txtRE_PenaltyCode"
        Me.txtRE_PenaltyCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_PenaltyCode.TabIndex = 30
        '
        'txtRE_PenaltyTitle
        '
        Me.txtRE_PenaltyTitle.Location = New System.Drawing.Point(215, 101)
        Me.txtRE_PenaltyTitle.Name = "txtRE_PenaltyTitle"
        Me.txtRE_PenaltyTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_PenaltyTitle.TabIndex = 29
        '
        'Label69
        '
        Me.Label69.AutoSize = True
        Me.Label69.Location = New System.Drawing.Point(110, 104)
        Me.Label69.Name = "Label69"
        Me.Label69.Size = New System.Drawing.Size(99, 15)
        Me.Label69.TabIndex = 28
        Me.Label69.Text = "Penalty Account :"
        '
        'txtRE_InterestCode
        '
        Me.txtRE_InterestCode.Location = New System.Drawing.Point(555, 74)
        Me.txtRE_InterestCode.Name = "txtRE_InterestCode"
        Me.txtRE_InterestCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_InterestCode.TabIndex = 27
        '
        'txtRE_InterestTitle
        '
        Me.txtRE_InterestTitle.Location = New System.Drawing.Point(215, 74)
        Me.txtRE_InterestTitle.Name = "txtRE_InterestTitle"
        Me.txtRE_InterestTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_InterestTitle.TabIndex = 26
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(110, 77)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(99, 15)
        Me.Label70.TabIndex = 25
        Me.Label70.Text = "Interest Account :"
        '
        'txtRE_EquityCode
        '
        Me.txtRE_EquityCode.Location = New System.Drawing.Point(555, 47)
        Me.txtRE_EquityCode.Name = "txtRE_EquityCode"
        Me.txtRE_EquityCode.Size = New System.Drawing.Size(80, 21)
        Me.txtRE_EquityCode.TabIndex = 24
        '
        'txtRE_EquityTitle
        '
        Me.txtRE_EquityTitle.Location = New System.Drawing.Point(215, 47)
        Me.txtRE_EquityTitle.Name = "txtRE_EquityTitle"
        Me.txtRE_EquityTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtRE_EquityTitle.TabIndex = 8
        '
        'Label71
        '
        Me.Label71.AutoSize = True
        Me.Label71.Location = New System.Drawing.Point(117, 51)
        Me.Label71.Name = "Label71"
        Me.Label71.Size = New System.Drawing.Size(92, 15)
        Me.Label71.TabIndex = 6
        Me.Label71.Text = "Equity Account :"
        '
        'GroupBox19
        '
        Me.GroupBox19.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox19.BackColor = System.Drawing.Color.White
        Me.GroupBox19.Controls.Add(Me.txtPOS_VATSalesCode)
        Me.GroupBox19.Controls.Add(Me.txtPOS_VATSalesTitle)
        Me.GroupBox19.Controls.Add(Me.Label54)
        Me.GroupBox19.Controls.Add(Me.txtPOS_DiscountCode)
        Me.GroupBox19.Controls.Add(Me.txtPOS_DiscountTitle)
        Me.GroupBox19.Controls.Add(Me.Label56)
        Me.GroupBox19.Controls.Add(Me.txtPOS_ZeroRatedCode)
        Me.GroupBox19.Controls.Add(Me.txtPOS_ZeroRatedTitle)
        Me.GroupBox19.Controls.Add(Me.Label58)
        Me.GroupBox19.Controls.Add(Me.txtPOS_VATExemptCode)
        Me.GroupBox19.Controls.Add(Me.txtPOS_VATExemptTitle)
        Me.GroupBox19.Controls.Add(Me.Label59)
        Me.GroupBox19.Controls.Add(Me.txtPOS_VATAmountCode)
        Me.GroupBox19.Controls.Add(Me.txtPOS_VATAmountTitle)
        Me.GroupBox19.Controls.Add(Me.Label60)
        Me.GroupBox19.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox19.Location = New System.Drawing.Point(5, 976)
        Me.GroupBox19.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox19.Name = "GroupBox19"
        Me.GroupBox19.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox19.Size = New System.Drawing.Size(671, 167)
        Me.GroupBox19.TabIndex = 31
        Me.GroupBox19.TabStop = False
        Me.GroupBox19.Text = "Point of Sale"
        '
        'txtPOS_VATSalesCode
        '
        Me.txtPOS_VATSalesCode.Location = New System.Drawing.Point(555, 20)
        Me.txtPOS_VATSalesCode.Name = "txtPOS_VATSalesCode"
        Me.txtPOS_VATSalesCode.Size = New System.Drawing.Size(80, 21)
        Me.txtPOS_VATSalesCode.TabIndex = 42
        '
        'txtPOS_VATSalesTitle
        '
        Me.txtPOS_VATSalesTitle.Location = New System.Drawing.Point(215, 20)
        Me.txtPOS_VATSalesTitle.Name = "txtPOS_VATSalesTitle"
        Me.txtPOS_VATSalesTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtPOS_VATSalesTitle.TabIndex = 41
        '
        'Label54
        '
        Me.Label54.AutoSize = True
        Me.Label54.Location = New System.Drawing.Point(68, 23)
        Me.Label54.Name = "Label54"
        Me.Label54.Size = New System.Drawing.Size(134, 15)
        Me.Label54.TabIndex = 40
        Me.Label54.Text = "Vatable Sales Account :"
        '
        'txtPOS_DiscountCode
        '
        Me.txtPOS_DiscountCode.Location = New System.Drawing.Point(555, 128)
        Me.txtPOS_DiscountCode.Name = "txtPOS_DiscountCode"
        Me.txtPOS_DiscountCode.Size = New System.Drawing.Size(80, 21)
        Me.txtPOS_DiscountCode.TabIndex = 36
        '
        'txtPOS_DiscountTitle
        '
        Me.txtPOS_DiscountTitle.Location = New System.Drawing.Point(215, 128)
        Me.txtPOS_DiscountTitle.Name = "txtPOS_DiscountTitle"
        Me.txtPOS_DiscountTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtPOS_DiscountTitle.TabIndex = 35
        '
        'Label56
        '
        Me.Label56.AutoSize = True
        Me.Label56.Location = New System.Drawing.Point(95, 131)
        Me.Label56.Name = "Label56"
        Me.Label56.Size = New System.Drawing.Size(107, 15)
        Me.Label56.TabIndex = 34
        Me.Label56.Text = "Discount Account :"
        '
        'txtPOS_ZeroRatedCode
        '
        Me.txtPOS_ZeroRatedCode.Location = New System.Drawing.Point(555, 101)
        Me.txtPOS_ZeroRatedCode.Name = "txtPOS_ZeroRatedCode"
        Me.txtPOS_ZeroRatedCode.Size = New System.Drawing.Size(80, 21)
        Me.txtPOS_ZeroRatedCode.TabIndex = 30
        '
        'txtPOS_ZeroRatedTitle
        '
        Me.txtPOS_ZeroRatedTitle.Location = New System.Drawing.Point(215, 101)
        Me.txtPOS_ZeroRatedTitle.Name = "txtPOS_ZeroRatedTitle"
        Me.txtPOS_ZeroRatedTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtPOS_ZeroRatedTitle.TabIndex = 29
        '
        'Label58
        '
        Me.Label58.AutoSize = True
        Me.Label58.Location = New System.Drawing.Point(85, 104)
        Me.Label58.Name = "Label58"
        Me.Label58.Size = New System.Drawing.Size(117, 15)
        Me.Label58.TabIndex = 28
        Me.Label58.Text = "ZeroRated Account :"
        '
        'txtPOS_VATExemptCode
        '
        Me.txtPOS_VATExemptCode.Location = New System.Drawing.Point(555, 74)
        Me.txtPOS_VATExemptCode.Name = "txtPOS_VATExemptCode"
        Me.txtPOS_VATExemptCode.Size = New System.Drawing.Size(80, 21)
        Me.txtPOS_VATExemptCode.TabIndex = 27
        '
        'txtPOS_VATExemptTitle
        '
        Me.txtPOS_VATExemptTitle.Location = New System.Drawing.Point(215, 74)
        Me.txtPOS_VATExemptTitle.Name = "txtPOS_VATExemptTitle"
        Me.txtPOS_VATExemptTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtPOS_VATExemptTitle.TabIndex = 26
        '
        'Label59
        '
        Me.Label59.AutoSize = True
        Me.Label59.Location = New System.Drawing.Point(77, 77)
        Me.Label59.Name = "Label59"
        Me.Label59.Size = New System.Drawing.Size(125, 15)
        Me.Label59.TabIndex = 25
        Me.Label59.Text = "VAT Exempt Account :"
        '
        'txtPOS_VATAmountCode
        '
        Me.txtPOS_VATAmountCode.Location = New System.Drawing.Point(555, 47)
        Me.txtPOS_VATAmountCode.Name = "txtPOS_VATAmountCode"
        Me.txtPOS_VATAmountCode.Size = New System.Drawing.Size(80, 21)
        Me.txtPOS_VATAmountCode.TabIndex = 24
        '
        'txtPOS_VATAmountTitle
        '
        Me.txtPOS_VATAmountTitle.Location = New System.Drawing.Point(215, 47)
        Me.txtPOS_VATAmountTitle.Name = "txtPOS_VATAmountTitle"
        Me.txtPOS_VATAmountTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtPOS_VATAmountTitle.TabIndex = 8
        '
        'Label60
        '
        Me.Label60.AutoSize = True
        Me.Label60.Location = New System.Drawing.Point(77, 51)
        Me.Label60.Name = "Label60"
        Me.Label60.Size = New System.Drawing.Size(125, 15)
        Me.Label60.TabIndex = 6
        Me.Label60.Text = "VAT Amount Account :"
        '
        'GroupBox22
        '
        Me.GroupBox22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox22.BackColor = System.Drawing.Color.White
        Me.GroupBox22.Controls.Add(Me.txtGPA_SalesAccntCode)
        Me.GroupBox22.Controls.Add(Me.txtGPA_SalesAccntTitle)
        Me.GroupBox22.Controls.Add(Me.Label61)
        Me.GroupBox22.Controls.Add(Me.txtGPA_SaleReturnAccntCode)
        Me.GroupBox22.Controls.Add(Me.txtGPA_SaleReturnAccntTitle)
        Me.GroupBox22.Controls.Add(Me.Label65)
        Me.GroupBox22.Controls.Add(Me.txtGPA_SaleDiscountAccntCode)
        Me.GroupBox22.Controls.Add(Me.txtGPA_SaleDiscountAccntTitle)
        Me.GroupBox22.Controls.Add(Me.Label66)
        Me.GroupBox22.Controls.Add(Me.txtGPA_COSAccntCode)
        Me.GroupBox22.Controls.Add(Me.txtGPA_COSAccntTitle)
        Me.GroupBox22.Controls.Add(Me.Label67)
        Me.GroupBox22.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox22.Location = New System.Drawing.Point(5, 826)
        Me.GroupBox22.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox22.Name = "GroupBox22"
        Me.GroupBox22.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox22.Size = New System.Drawing.Size(671, 141)
        Me.GroupBox22.TabIndex = 30
        Me.GroupBox22.TabStop = False
        Me.GroupBox22.Text = "Gross Profit Analysis"
        '
        'txtGPA_SalesAccntCode
        '
        Me.txtGPA_SalesAccntCode.Location = New System.Drawing.Point(555, 20)
        Me.txtGPA_SalesAccntCode.Name = "txtGPA_SalesAccntCode"
        Me.txtGPA_SalesAccntCode.Size = New System.Drawing.Size(80, 21)
        Me.txtGPA_SalesAccntCode.TabIndex = 42
        '
        'txtGPA_SalesAccntTitle
        '
        Me.txtGPA_SalesAccntTitle.Location = New System.Drawing.Point(215, 20)
        Me.txtGPA_SalesAccntTitle.Name = "txtGPA_SalesAccntTitle"
        Me.txtGPA_SalesAccntTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtGPA_SalesAccntTitle.TabIndex = 41
        '
        'Label61
        '
        Me.Label61.AutoSize = True
        Me.Label61.Location = New System.Drawing.Point(119, 23)
        Me.Label61.Name = "Label61"
        Me.Label61.Size = New System.Drawing.Size(90, 15)
        Me.Label61.TabIndex = 40
        Me.Label61.Text = "Sales Account :"
        '
        'txtGPA_SaleReturnAccntCode
        '
        Me.txtGPA_SaleReturnAccntCode.Location = New System.Drawing.Point(555, 101)
        Me.txtGPA_SaleReturnAccntCode.Name = "txtGPA_SaleReturnAccntCode"
        Me.txtGPA_SaleReturnAccntCode.Size = New System.Drawing.Size(80, 21)
        Me.txtGPA_SaleReturnAccntCode.TabIndex = 30
        '
        'txtGPA_SaleReturnAccntTitle
        '
        Me.txtGPA_SaleReturnAccntTitle.Location = New System.Drawing.Point(215, 101)
        Me.txtGPA_SaleReturnAccntTitle.Name = "txtGPA_SaleReturnAccntTitle"
        Me.txtGPA_SaleReturnAccntTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtGPA_SaleReturnAccntTitle.TabIndex = 29
        '
        'Label65
        '
        Me.Label65.AutoSize = True
        Me.Label65.Location = New System.Drawing.Point(79, 104)
        Me.Label65.Name = "Label65"
        Me.Label65.Size = New System.Drawing.Size(130, 15)
        Me.Label65.TabIndex = 28
        Me.Label65.Text = "Sales Return Account :"
        '
        'txtGPA_SaleDiscountAccntCode
        '
        Me.txtGPA_SaleDiscountAccntCode.Location = New System.Drawing.Point(555, 74)
        Me.txtGPA_SaleDiscountAccntCode.Name = "txtGPA_SaleDiscountAccntCode"
        Me.txtGPA_SaleDiscountAccntCode.Size = New System.Drawing.Size(80, 21)
        Me.txtGPA_SaleDiscountAccntCode.TabIndex = 27
        '
        'txtGPA_SaleDiscountAccntTitle
        '
        Me.txtGPA_SaleDiscountAccntTitle.Location = New System.Drawing.Point(215, 74)
        Me.txtGPA_SaleDiscountAccntTitle.Name = "txtGPA_SaleDiscountAccntTitle"
        Me.txtGPA_SaleDiscountAccntTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtGPA_SaleDiscountAccntTitle.TabIndex = 26
        '
        'Label66
        '
        Me.Label66.AutoSize = True
        Me.Label66.Location = New System.Drawing.Point(68, 77)
        Me.Label66.Name = "Label66"
        Me.Label66.Size = New System.Drawing.Size(141, 15)
        Me.Label66.TabIndex = 25
        Me.Label66.Text = "Sales Discount Account :"
        '
        'txtGPA_COSAccntCode
        '
        Me.txtGPA_COSAccntCode.Location = New System.Drawing.Point(555, 47)
        Me.txtGPA_COSAccntCode.Name = "txtGPA_COSAccntCode"
        Me.txtGPA_COSAccntCode.Size = New System.Drawing.Size(80, 21)
        Me.txtGPA_COSAccntCode.TabIndex = 24
        '
        'txtGPA_COSAccntTitle
        '
        Me.txtGPA_COSAccntTitle.Location = New System.Drawing.Point(215, 47)
        Me.txtGPA_COSAccntTitle.Name = "txtGPA_COSAccntTitle"
        Me.txtGPA_COSAccntTitle.Size = New System.Drawing.Size(338, 21)
        Me.txtGPA_COSAccntTitle.TabIndex = 8
        '
        'Label67
        '
        Me.Label67.AutoSize = True
        Me.Label67.Location = New System.Drawing.Point(79, 51)
        Me.Label67.Name = "Label67"
        Me.Label67.Size = New System.Drawing.Size(130, 15)
        Me.Label67.TabIndex = 6
        Me.Label67.Text = "Cost of Sales Account :"
        '
        'gbAP
        '
        Me.gbAP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbAP.BackColor = System.Drawing.Color.White
        Me.gbAP.Controls.Add(Me.nupBankPeriod)
        Me.gbAP.Controls.Add(Me.Label86)
        Me.gbAP.Controls.Add(Me.Label13)
        Me.gbAP.Controls.Add(Me.chkPOapproval)
        Me.gbAP.Controls.Add(Me.txtIVcode)
        Me.gbAP.Controls.Add(Me.txtATScode)
        Me.gbAP.Controls.Add(Me.txtPAPcode)
        Me.gbAP.Controls.Add(Me.CheckBox15)
        Me.gbAP.Controls.Add(Me.txtPAPtitle)
        Me.gbAP.Controls.Add(Me.Label15)
        Me.gbAP.Controls.Add(Me.lbPayables)
        Me.gbAP.Controls.Add(Me.txtATStitle)
        Me.gbAP.Controls.Add(Me.Label11)
        Me.gbAP.Controls.Add(Me.txtIVtitle)
        Me.gbAP.Controls.Add(Me.Label10)
        Me.gbAP.Controls.Add(Me.btnRemovePayables)
        Me.gbAP.Controls.Add(Me.btnAddPayables)
        Me.gbAP.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.gbAP.Location = New System.Drawing.Point(5, 524)
        Me.gbAP.Margin = New System.Windows.Forms.Padding(0)
        Me.gbAP.Name = "gbAP"
        Me.gbAP.Padding = New System.Windows.Forms.Padding(0)
        Me.gbAP.Size = New System.Drawing.Size(671, 228)
        Me.gbAP.TabIndex = 3
        Me.gbAP.TabStop = False
        Me.gbAP.Text = "Payables"
        '
        'nupBankPeriod
        '
        Me.nupBankPeriod.DecimalPlaces = 2
        Me.nupBankPeriod.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nupBankPeriod.Location = New System.Drawing.Point(215, 171)
        Me.nupBankPeriod.Maximum = New Decimal(New Integer() {1000000, 0, 0, 0})
        Me.nupBankPeriod.Name = "nupBankPeriod"
        Me.nupBankPeriod.Size = New System.Drawing.Size(94, 22)
        Me.nupBankPeriod.TabIndex = 1439
        Me.nupBankPeriod.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.nupBankPeriod.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Label86
        '
        Me.Label86.AutoSize = True
        Me.Label86.Location = New System.Drawing.Point(56, 175)
        Me.Label86.Name = "Label86"
        Me.Label86.Size = New System.Drawing.Size(156, 15)
        Me.Label86.TabIndex = 26
        Me.Label86.Text = "Bank Staled Period (Days) :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(94, 23)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(113, 30)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Accounts for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tracking Payables :"
        '
        'chkPOapproval
        '
        Me.chkPOapproval.AutoSize = True
        Me.chkPOapproval.Location = New System.Drawing.Point(468, 125)
        Me.chkPOapproval.Name = "chkPOapproval"
        Me.chkPOapproval.Size = New System.Drawing.Size(140, 19)
        Me.chkPOapproval.TabIndex = 0
        Me.chkPOapproval.Text = "Require PO Approval"
        Me.chkPOapproval.UseVisualStyleBackColor = True
        Me.chkPOapproval.Visible = False
        '
        'txtIVcode
        '
        Me.txtIVcode.Location = New System.Drawing.Point(555, 100)
        Me.txtIVcode.Name = "txtIVcode"
        Me.txtIVcode.Size = New System.Drawing.Size(80, 21)
        Me.txtIVcode.TabIndex = 24
        '
        'txtATScode
        '
        Me.txtATScode.Location = New System.Drawing.Point(555, 197)
        Me.txtATScode.Name = "txtATScode"
        Me.txtATScode.Size = New System.Drawing.Size(80, 21)
        Me.txtATScode.TabIndex = 22
        Me.txtATScode.Visible = False
        '
        'txtPAPcode
        '
        Me.txtPAPcode.Location = New System.Drawing.Point(555, 146)
        Me.txtPAPcode.Name = "txtPAPcode"
        Me.txtPAPcode.Size = New System.Drawing.Size(80, 21)
        Me.txtPAPcode.TabIndex = 21
        '
        'CheckBox15
        '
        Me.CheckBox15.AutoSize = True
        Me.CheckBox15.Location = New System.Drawing.Point(215, 125)
        Me.CheckBox15.Name = "CheckBox15"
        Me.CheckBox15.Size = New System.Drawing.Size(207, 19)
        Me.CheckBox15.TabIndex = 20
        Me.CheckBox15.Text = "Setup Pending AP Entry upon RR"
        Me.CheckBox15.UseVisualStyleBackColor = True
        '
        'txtPAPtitle
        '
        Me.txtPAPtitle.Location = New System.Drawing.Point(215, 146)
        Me.txtPAPtitle.Name = "txtPAPtitle"
        Me.txtPAPtitle.Size = New System.Drawing.Size(338, 21)
        Me.txtPAPtitle.TabIndex = 18
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(71, 148)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(141, 15)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "Pending AP Account (L) :"
        '
        'lbPayables
        '
        Me.lbPayables.FormattingEnabled = True
        Me.lbPayables.ItemHeight = 15
        Me.lbPayables.Location = New System.Drawing.Point(215, 17)
        Me.lbPayables.Name = "lbPayables"
        Me.lbPayables.Size = New System.Drawing.Size(338, 79)
        Me.lbPayables.TabIndex = 16
        '
        'txtATStitle
        '
        Me.txtATStitle.Location = New System.Drawing.Point(215, 197)
        Me.txtATStitle.Name = "txtATStitle"
        Me.txtATStitle.Size = New System.Drawing.Size(338, 21)
        Me.txtATStitle.TabIndex = 10
        Me.txtATStitle.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(61, 200)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(151, 15)
        Me.Label11.TabIndex = 9
        Me.Label11.Text = "Advances to Suppliers (A) :"
        Me.Label11.Visible = False
        '
        'txtIVtitle
        '
        Me.txtIVtitle.Location = New System.Drawing.Point(215, 100)
        Me.txtIVtitle.Name = "txtIVtitle"
        Me.txtIVtitle.Size = New System.Drawing.Size(338, 21)
        Me.txtIVtitle.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(130, 103)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 15)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Input VAT (A) :"
        '
        'btnRemovePayables
        '
        Me.btnRemovePayables.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnRemovePayables.Location = New System.Drawing.Point(555, 48)
        Me.btnRemovePayables.Name = "btnRemovePayables"
        Me.btnRemovePayables.Size = New System.Drawing.Size(80, 30)
        Me.btnRemovePayables.TabIndex = 4
        Me.btnRemovePayables.Text = "Remove"
        Me.btnRemovePayables.UseVisualStyleBackColor = True
        '
        'btnAddPayables
        '
        Me.btnAddPayables.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnAddPayables.Location = New System.Drawing.Point(555, 16)
        Me.btnAddPayables.Name = "btnAddPayables"
        Me.btnAddPayables.Size = New System.Drawing.Size(80, 30)
        Me.btnAddPayables.TabIndex = 3
        Me.btnAddPayables.Text = "Add"
        Me.btnAddPayables.UseVisualStyleBackColor = True
        '
        'GroupBox14
        '
        Me.GroupBox14.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox14.BackColor = System.Drawing.Color.White
        Me.GroupBox14.Controls.Add(Me.txtNI_Code)
        Me.GroupBox14.Controls.Add(Me.txtNI_Title)
        Me.GroupBox14.Controls.Add(Me.Label96)
        Me.GroupBox14.Controls.Add(Me.txtCOH_Code)
        Me.GroupBox14.Controls.Add(Me.txtCOH_Title)
        Me.GroupBox14.Controls.Add(Me.Label14)
        Me.GroupBox14.Controls.Add(Me.txtPEC_Code)
        Me.GroupBox14.Controls.Add(Me.txtPEC_Title)
        Me.GroupBox14.Controls.Add(Me.Label51)
        Me.GroupBox14.Controls.Add(Me.txtCWT_Code)
        Me.GroupBox14.Controls.Add(Me.txtCWT_Title)
        Me.GroupBox14.Controls.Add(Me.Label50)
        Me.GroupBox14.Controls.Add(Me.txtFWT_Code)
        Me.GroupBox14.Controls.Add(Me.txtFWT_Title)
        Me.GroupBox14.Controls.Add(Me.Label49)
        Me.GroupBox14.Controls.Add(Me.txtEWT_Code)
        Me.GroupBox14.Controls.Add(Me.txtEWT_Title)
        Me.GroupBox14.Controls.Add(Me.Label46)
        Me.GroupBox14.Controls.Add(Me.txtVP_Code)
        Me.GroupBox14.Controls.Add(Me.txtVP_Title)
        Me.GroupBox14.Controls.Add(Me.Label44)
        Me.GroupBox14.Controls.Add(Me.txtPT_Code)
        Me.GroupBox14.Controls.Add(Me.txtPT_Title)
        Me.GroupBox14.Controls.Add(Me.Label48)
        Me.GroupBox14.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox14.Location = New System.Drawing.Point(5, 1)
        Me.GroupBox14.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox14.Name = "GroupBox14"
        Me.GroupBox14.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox14.Size = New System.Drawing.Size(671, 242)
        Me.GroupBox14.TabIndex = 28
        Me.GroupBox14.TabStop = False
        Me.GroupBox14.Text = "General"
        '
        'txtCOH_Code
        '
        Me.txtCOH_Code.Location = New System.Drawing.Point(555, 20)
        Me.txtCOH_Code.Name = "txtCOH_Code"
        Me.txtCOH_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtCOH_Code.TabIndex = 42
        '
        'txtCOH_Title
        '
        Me.txtCOH_Title.Location = New System.Drawing.Point(215, 20)
        Me.txtCOH_Title.Name = "txtCOH_Title"
        Me.txtCOH_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtCOH_Title.TabIndex = 41
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(68, 23)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(139, 15)
        Me.Label14.TabIndex = 40
        Me.Label14.Text = "Cash On Hand Account :"
        '
        'txtPEC_Code
        '
        Me.txtPEC_Code.Location = New System.Drawing.Point(555, 182)
        Me.txtPEC_Code.Name = "txtPEC_Code"
        Me.txtPEC_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtPEC_Code.TabIndex = 39
        '
        'txtPEC_Title
        '
        Me.txtPEC_Title.Location = New System.Drawing.Point(215, 182)
        Me.txtPEC_Title.Name = "txtPEC_Title"
        Me.txtPEC_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtPEC_Title.TabIndex = 38
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(127, 185)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(80, 15)
        Me.Label51.TabIndex = 37
        Me.Label51.Text = "PEC Account:"
        '
        'txtCWT_Code
        '
        Me.txtCWT_Code.Location = New System.Drawing.Point(555, 128)
        Me.txtCWT_Code.Name = "txtCWT_Code"
        Me.txtCWT_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtCWT_Code.TabIndex = 36
        '
        'txtCWT_Title
        '
        Me.txtCWT_Title.Location = New System.Drawing.Point(215, 128)
        Me.txtCWT_Title.Name = "txtCWT_Title"
        Me.txtCWT_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtCWT_Title.TabIndex = 35
        '
        'Label50
        '
        Me.Label50.AutoSize = True
        Me.Label50.Location = New System.Drawing.Point(49, 131)
        Me.Label50.Name = "Label50"
        Me.Label50.Size = New System.Drawing.Size(160, 15)
        Me.Label50.TabIndex = 34
        Me.Label50.Text = "Creditable Withholding Tax :"
        '
        'txtFWT_Code
        '
        Me.txtFWT_Code.Location = New System.Drawing.Point(555, 155)
        Me.txtFWT_Code.Name = "txtFWT_Code"
        Me.txtFWT_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtFWT_Code.TabIndex = 33
        '
        'txtFWT_Title
        '
        Me.txtFWT_Title.Location = New System.Drawing.Point(215, 155)
        Me.txtFWT_Title.Name = "txtFWT_Title"
        Me.txtFWT_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtFWT_Title.TabIndex = 32
        '
        'Label49
        '
        Me.Label49.AutoSize = True
        Me.Label49.Location = New System.Drawing.Point(76, 158)
        Me.Label49.Name = "Label49"
        Me.Label49.Size = New System.Drawing.Size(131, 15)
        Me.Label49.TabIndex = 31
        Me.Label49.Text = "Final Withholding Tax :"
        '
        'txtEWT_Code
        '
        Me.txtEWT_Code.Location = New System.Drawing.Point(555, 101)
        Me.txtEWT_Code.Name = "txtEWT_Code"
        Me.txtEWT_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtEWT_Code.TabIndex = 30
        '
        'txtEWT_Title
        '
        Me.txtEWT_Title.Location = New System.Drawing.Point(215, 101)
        Me.txtEWT_Title.Name = "txtEWT_Title"
        Me.txtEWT_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtEWT_Title.TabIndex = 29
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(49, 104)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(160, 15)
        Me.Label46.TabIndex = 28
        Me.Label46.Text = "Expanded Withholding Tax :"
        '
        'txtVP_Code
        '
        Me.txtVP_Code.Location = New System.Drawing.Point(555, 74)
        Me.txtVP_Code.Name = "txtVP_Code"
        Me.txtVP_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtVP_Code.TabIndex = 27
        '
        'txtVP_Title
        '
        Me.txtVP_Title.Location = New System.Drawing.Point(215, 74)
        Me.txtVP_Title.Name = "txtVP_Title"
        Me.txtVP_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtVP_Title.TabIndex = 26
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(128, 77)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(81, 15)
        Me.Label44.TabIndex = 25
        Me.Label44.Text = "VAT Payable :"
        '
        'txtPT_Code
        '
        Me.txtPT_Code.Location = New System.Drawing.Point(555, 47)
        Me.txtPT_Code.Name = "txtPT_Code"
        Me.txtPT_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtPT_Code.TabIndex = 24
        '
        'txtPT_Title
        '
        Me.txtPT_Title.Location = New System.Drawing.Point(215, 47)
        Me.txtPT_Title.Name = "txtPT_Title"
        Me.txtPT_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtPT_Title.TabIndex = 8
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(61, 51)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(146, 15)
        Me.Label48.TabIndex = 6
        Me.Label48.Text = "Percentage Tax Payable :"
        '
        'GroupBox9
        '
        Me.GroupBox9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox9.BackColor = System.Drawing.Color.White
        Me.GroupBox9.Controls.Add(Me.txtDOV_Code)
        Me.GroupBox9.Controls.Add(Me.txtDOV_Title)
        Me.GroupBox9.Controls.Add(Me.Label12)
        Me.GroupBox9.Controls.Add(Me.txtOV_Code)
        Me.GroupBox9.Controls.Add(Me.lbReceivables)
        Me.GroupBox9.Controls.Add(Me.Label45)
        Me.GroupBox9.Controls.Add(Me.txtOV_Title)
        Me.GroupBox9.Controls.Add(Me.Label47)
        Me.GroupBox9.Controls.Add(Me.btnRemoveReceivable)
        Me.GroupBox9.Controls.Add(Me.btnAddReceivables)
        Me.GroupBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox9.Location = New System.Drawing.Point(5, 340)
        Me.GroupBox9.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox9.Size = New System.Drawing.Size(671, 184)
        Me.GroupBox9.TabIndex = 25
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = "Receivables"
        '
        'txtDOV_Code
        '
        Me.txtDOV_Code.Location = New System.Drawing.Point(555, 143)
        Me.txtDOV_Code.Name = "txtDOV_Code"
        Me.txtDOV_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtDOV_Code.TabIndex = 27
        '
        'txtDOV_Title
        '
        Me.txtDOV_Title.Location = New System.Drawing.Point(215, 143)
        Me.txtDOV_Title.Name = "txtDOV_Title"
        Me.txtDOV_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtDOV_Title.TabIndex = 26
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(85, 146)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(124, 15)
        Me.Label12.TabIndex = 25
        Me.Label12.Text = "Deferred Output VAT :"
        '
        'txtOV_Code
        '
        Me.txtOV_Code.Location = New System.Drawing.Point(555, 116)
        Me.txtOV_Code.Name = "txtOV_Code"
        Me.txtOV_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtOV_Code.TabIndex = 24
        '
        'lbReceivables
        '
        Me.lbReceivables.FormattingEnabled = True
        Me.lbReceivables.ItemHeight = 15
        Me.lbReceivables.Location = New System.Drawing.Point(215, 33)
        Me.lbReceivables.Name = "lbReceivables"
        Me.lbReceivables.Size = New System.Drawing.Size(338, 79)
        Me.lbReceivables.TabIndex = 19
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(82, 33)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(130, 30)
        Me.Label45.TabIndex = 13
        Me.Label45.Text = "Accounts for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Tracking Receivables :"
        '
        'txtOV_Title
        '
        Me.txtOV_Title.Location = New System.Drawing.Point(215, 116)
        Me.txtOV_Title.Name = "txtOV_Title"
        Me.txtOV_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtOV_Title.TabIndex = 8
        '
        'Label47
        '
        Me.Label47.AutoSize = True
        Me.Label47.Location = New System.Drawing.Point(136, 119)
        Me.Label47.Name = "Label47"
        Me.Label47.Size = New System.Drawing.Size(73, 15)
        Me.Label47.TabIndex = 6
        Me.Label47.Text = "Output VAT :"
        '
        'btnRemoveReceivable
        '
        Me.btnRemoveReceivable.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnRemoveReceivable.Location = New System.Drawing.Point(555, 64)
        Me.btnRemoveReceivable.Name = "btnRemoveReceivable"
        Me.btnRemoveReceivable.Size = New System.Drawing.Size(80, 30)
        Me.btnRemoveReceivable.TabIndex = 4
        Me.btnRemoveReceivable.Text = "Remove"
        Me.btnRemoveReceivable.UseVisualStyleBackColor = True
        '
        'btnAddReceivables
        '
        Me.btnAddReceivables.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnAddReceivables.Location = New System.Drawing.Point(555, 32)
        Me.btnAddReceivables.Name = "btnAddReceivables"
        Me.btnAddReceivables.Size = New System.Drawing.Size(80, 30)
        Me.btnAddReceivables.TabIndex = 3
        Me.btnAddReceivables.Text = "Add"
        Me.btnAddReceivables.UseVisualStyleBackColor = True
        '
        'GroupBox11
        '
        Me.GroupBox11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox11.BackColor = System.Drawing.Color.White
        Me.GroupBox11.Controls.Add(Me.lblCashAdvance)
        Me.GroupBox11.Controls.Add(Me.Label52)
        Me.GroupBox11.Controls.Add(Me.btnRemoveCA)
        Me.GroupBox11.Controls.Add(Me.btnAddCA)
        Me.GroupBox11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox11.Location = New System.Drawing.Point(5, 243)
        Me.GroupBox11.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox11.Name = "GroupBox11"
        Me.GroupBox11.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox11.Size = New System.Drawing.Size(671, 97)
        Me.GroupBox11.TabIndex = 29
        Me.GroupBox11.TabStop = False
        Me.GroupBox11.Text = "Cash Advance"
        '
        'lblCashAdvance
        '
        Me.lblCashAdvance.FormattingEnabled = True
        Me.lblCashAdvance.ItemHeight = 15
        Me.lblCashAdvance.Location = New System.Drawing.Point(215, 25)
        Me.lblCashAdvance.Name = "lblCashAdvance"
        Me.lblCashAdvance.Size = New System.Drawing.Size(338, 49)
        Me.lblCashAdvance.TabIndex = 19
        '
        'Label52
        '
        Me.Label52.AutoSize = True
        Me.Label52.Location = New System.Drawing.Point(122, 28)
        Me.Label52.Name = "Label52"
        Me.Label52.Size = New System.Drawing.Size(90, 30)
        Me.Label52.TabIndex = 13
        Me.Label52.Text = "Accounts for " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Cash Advance :"
        '
        'btnRemoveCA
        '
        Me.btnRemoveCA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnRemoveCA.Location = New System.Drawing.Point(555, 56)
        Me.btnRemoveCA.Name = "btnRemoveCA"
        Me.btnRemoveCA.Size = New System.Drawing.Size(80, 30)
        Me.btnRemoveCA.TabIndex = 4
        Me.btnRemoveCA.Text = "Remove"
        Me.btnRemoveCA.UseVisualStyleBackColor = True
        '
        'btnAddCA
        '
        Me.btnAddCA.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnAddCA.Location = New System.Drawing.Point(555, 24)
        Me.btnAddCA.Name = "btnAddCA"
        Me.btnAddCA.Size = New System.Drawing.Size(80, 30)
        Me.btnAddCA.TabIndex = 3
        Me.btnAddCA.Text = "Add"
        Me.btnAddCA.UseVisualStyleBackColor = True
        '
        'tpPurchase
        '
        Me.tpPurchase.Controls.Add(Me.gbPO)
        Me.tpPurchase.Controls.Add(Me.gbPR)
        Me.tpPurchase.Location = New System.Drawing.Point(5, 4)
        Me.tpPurchase.Name = "tpPurchase"
        Me.tpPurchase.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPurchase.Size = New System.Drawing.Size(785, 609)
        Me.tpPurchase.TabIndex = 5
        Me.tpPurchase.Text = "TabPage1"
        Me.tpPurchase.UseVisualStyleBackColor = True
        '
        'gbPO
        '
        Me.gbPO.Controls.Add(Me.CheckBox14)
        Me.gbPO.Location = New System.Drawing.Point(6, 139)
        Me.gbPO.Name = "gbPO"
        Me.gbPO.Size = New System.Drawing.Size(773, 136)
        Me.gbPO.TabIndex = 2
        Me.gbPO.TabStop = False
        Me.gbPO.Text = "Purchase Order (PO)"
        '
        'CheckBox14
        '
        Me.CheckBox14.AutoSize = True
        Me.CheckBox14.Location = New System.Drawing.Point(24, 42)
        Me.CheckBox14.Name = "CheckBox14"
        Me.CheckBox14.Size = New System.Drawing.Size(428, 21)
        Me.CheckBox14.TabIndex = 1
        Me.CheckBox14.Text = "Separate Transaction Series for Stock, Non-Stock and Services"
        Me.CheckBox14.UseVisualStyleBackColor = True
        '
        'gbPR
        '
        Me.gbPR.Controls.Add(Me.Label36)
        Me.gbPR.Controls.Add(Me.cbPRstock)
        Me.gbPR.Location = New System.Drawing.Point(6, 3)
        Me.gbPR.Name = "gbPR"
        Me.gbPR.Size = New System.Drawing.Size(773, 136)
        Me.gbPR.TabIndex = 1
        Me.gbPR.TabStop = False
        Me.gbPR.Text = "Purchase Requisition (PR)"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(34, 31)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(151, 17)
        Me.Label36.TabIndex = 1
        Me.Label36.Text = "Check Stock Level by :"
        '
        'cbPRstock
        '
        Me.cbPRstock.FormattingEnabled = True
        Me.cbPRstock.Location = New System.Drawing.Point(191, 28)
        Me.cbPRstock.Name = "cbPRstock"
        Me.cbPRstock.Size = New System.Drawing.Size(261, 24)
        Me.cbPRstock.TabIndex = 0
        '
        'tpATC
        '
        Me.tpATC.Controls.Add(Me.GroupBox10)
        Me.tpATC.Location = New System.Drawing.Point(5, 4)
        Me.tpATC.Name = "tpATC"
        Me.tpATC.Padding = New System.Windows.Forms.Padding(3)
        Me.tpATC.Size = New System.Drawing.Size(785, 609)
        Me.tpATC.TabIndex = 6
        Me.tpATC.Text = "Tax"
        Me.tpATC.UseVisualStyleBackColor = True
        '
        'GroupBox10
        '
        Me.GroupBox10.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox10.Controls.Add(Me.dgvATC)
        Me.GroupBox10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox10.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox10.Name = "GroupBox10"
        Me.GroupBox10.Size = New System.Drawing.Size(778, 402)
        Me.GroupBox10.TabIndex = 3
        Me.GroupBox10.TabStop = False
        Me.GroupBox10.Text = "Alphanumeric Tax Codes"
        '
        'dgvATC
        '
        Me.dgvATC.BackgroundColor = System.Drawing.Color.White
        Me.dgvATC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvATC.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcATCCode, Me.dgcATCDesc, Me.dgcATCRate})
        Me.dgvATC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvATC.Location = New System.Drawing.Point(3, 17)
        Me.dgvATC.Name = "dgvATC"
        Me.dgvATC.RowHeadersWidth = 25
        Me.dgvATC.Size = New System.Drawing.Size(772, 382)
        Me.dgvATC.TabIndex = 2
        '
        'dgcATCCode
        '
        Me.dgcATCCode.HeaderText = "Code"
        Me.dgcATCCode.Name = "dgcATCCode"
        '
        'dgcATCDesc
        '
        Me.dgcATCDesc.HeaderText = "Description"
        Me.dgcATCDesc.Name = "dgcATCDesc"
        Me.dgcATCDesc.Width = 550
        '
        'dgcATCRate
        '
        Me.dgcATCRate.HeaderText = "Rate"
        Me.dgcATCRate.Name = "dgcATCRate"
        '
        'tpBranch
        '
        Me.tpBranch.Controls.Add(Me.GroupBox13)
        Me.tpBranch.Location = New System.Drawing.Point(5, 4)
        Me.tpBranch.Name = "tpBranch"
        Me.tpBranch.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBranch.Size = New System.Drawing.Size(785, 609)
        Me.tpBranch.TabIndex = 7
        Me.tpBranch.Text = "Branch"
        Me.tpBranch.UseVisualStyleBackColor = True
        '
        'GroupBox13
        '
        Me.GroupBox13.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox13.Controls.Add(Me.dgvBranch)
        Me.GroupBox13.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox13.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox13.Name = "GroupBox13"
        Me.GroupBox13.Size = New System.Drawing.Size(773, 405)
        Me.GroupBox13.TabIndex = 2
        Me.GroupBox13.TabStop = False
        Me.GroupBox13.Text = "Branches"
        '
        'dgvBranch
        '
        Me.dgvBranch.AllowDrop = True
        Me.dgvBranch.AllowUserToOrderColumns = True
        Me.dgvBranch.AllowUserToResizeColumns = False
        Me.dgvBranch.AllowUserToResizeRows = False
        Me.dgvBranch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBranch.BackgroundColor = System.Drawing.Color.White
        Me.dgvBranch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBranch.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBranchOldCode, Me.dgcBranchCode, Me.dgcBranchName, Me.dgcBranchMain})
        Me.dgvBranch.Location = New System.Drawing.Point(6, 21)
        Me.dgvBranch.MultiSelect = False
        Me.dgvBranch.Name = "dgvBranch"
        Me.dgvBranch.RowHeadersWidth = 25
        Me.dgvBranch.Size = New System.Drawing.Size(398, 381)
        Me.dgvBranch.TabIndex = 1
        '
        'dgcBranchOldCode
        '
        Me.dgcBranchOldCode.HeaderText = "OldCode"
        Me.dgcBranchOldCode.Name = "dgcBranchOldCode"
        Me.dgcBranchOldCode.Visible = False
        '
        'dgcBranchCode
        '
        Me.dgcBranchCode.HeaderText = "Code"
        Me.dgcBranchCode.Name = "dgcBranchCode"
        Me.dgcBranchCode.Width = 80
        '
        'dgcBranchName
        '
        Me.dgcBranchName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcBranchName.HeaderText = "Description"
        Me.dgcBranchName.Name = "dgcBranchName"
        '
        'dgcBranchMain
        '
        Me.dgcBranchMain.HeaderText = "Default"
        Me.dgcBranchMain.Name = "dgcBranchMain"
        Me.dgcBranchMain.Width = 55
        '
        'tpBusiType
        '
        Me.tpBusiType.Controls.Add(Me.GroupBox12)
        Me.tpBusiType.Location = New System.Drawing.Point(5, 4)
        Me.tpBusiType.Name = "tpBusiType"
        Me.tpBusiType.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBusiType.Size = New System.Drawing.Size(785, 609)
        Me.tpBusiType.TabIndex = 8
        Me.tpBusiType.Text = "TabPage1"
        Me.tpBusiType.UseVisualStyleBackColor = True
        '
        'GroupBox12
        '
        Me.GroupBox12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox12.Controls.Add(Me.dgvBusType)
        Me.GroupBox12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.GroupBox12.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox12.Name = "GroupBox12"
        Me.GroupBox12.Size = New System.Drawing.Size(773, 402)
        Me.GroupBox12.TabIndex = 3
        Me.GroupBox12.TabStop = False
        Me.GroupBox12.Text = "Business Type"
        '
        'dgvBusType
        '
        Me.dgvBusType.AllowDrop = True
        Me.dgvBusType.AllowUserToOrderColumns = True
        Me.dgvBusType.AllowUserToResizeColumns = False
        Me.dgvBusType.AllowUserToResizeRows = False
        Me.dgvBusType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBusType.BackgroundColor = System.Drawing.Color.White
        Me.dgvBusType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBusType.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgcBusTypeOld, Me.dgcBusType, Me.dgcBusTypeDesc})
        Me.dgvBusType.Location = New System.Drawing.Point(6, 21)
        Me.dgvBusType.MultiSelect = False
        Me.dgvBusType.Name = "dgvBusType"
        Me.dgvBusType.RowHeadersWidth = 25
        Me.dgvBusType.Size = New System.Drawing.Size(355, 374)
        Me.dgvBusType.TabIndex = 2
        '
        'dgcBusTypeOld
        '
        Me.dgcBusTypeOld.HeaderText = "OldCode"
        Me.dgcBusTypeOld.Name = "dgcBusTypeOld"
        Me.dgcBusTypeOld.Visible = False
        '
        'dgcBusType
        '
        Me.dgcBusType.HeaderText = "Code"
        Me.dgcBusType.Name = "dgcBusType"
        Me.dgcBusType.Width = 80
        '
        'dgcBusTypeDesc
        '
        Me.dgcBusTypeDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgcBusTypeDesc.HeaderText = "Description"
        Me.dgcBusTypeDesc.Name = "dgcBusTypeDesc"
        '
        'tpMaintGroup
        '
        Me.tpMaintGroup.Controls.Add(Me.Panel2)
        Me.tpMaintGroup.Location = New System.Drawing.Point(5, 4)
        Me.tpMaintGroup.Name = "tpMaintGroup"
        Me.tpMaintGroup.Padding = New System.Windows.Forms.Padding(3)
        Me.tpMaintGroup.Size = New System.Drawing.Size(785, 609)
        Me.tpMaintGroup.TabIndex = 9
        Me.tpMaintGroup.Text = "TabPage1"
        Me.tpMaintGroup.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.Add(Me.GroupBox25)
        Me.Panel2.Controls.Add(Me.gbProdWH)
        Me.Panel2.Controls.Add(Me.gbPC)
        Me.Panel2.Controls.Add(Me.gbCC)
        Me.Panel2.Controls.Add(Me.gbInvWH)
        Me.Panel2.Location = New System.Drawing.Point(3, 6)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(776, 564)
        Me.Panel2.TabIndex = 20
        '
        'GroupBox25
        '
        Me.GroupBox25.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox25.Controls.Add(Me.txtInv_Group1)
        Me.GroupBox25.Controls.Add(Me.Label63)
        Me.GroupBox25.Controls.Add(Me.Label68)
        Me.GroupBox25.Controls.Add(Me.txtInv_Group5)
        Me.GroupBox25.Controls.Add(Me.txtInv_Group2)
        Me.GroupBox25.Controls.Add(Me.Label73)
        Me.GroupBox25.Controls.Add(Me.Label75)
        Me.GroupBox25.Controls.Add(Me.txtInv_Group4)
        Me.GroupBox25.Controls.Add(Me.txtInv_Group3)
        Me.GroupBox25.Controls.Add(Me.Label79)
        Me.GroupBox25.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.GroupBox25.Location = New System.Drawing.Point(9, 656)
        Me.GroupBox25.Name = "GroupBox25"
        Me.GroupBox25.Size = New System.Drawing.Size(316, 158)
        Me.GroupBox25.TabIndex = 38
        Me.GroupBox25.TabStop = False
        Me.GroupBox25.Text = "Item Master Group"
        '
        'txtInv_Group1
        '
        Me.txtInv_Group1.Location = New System.Drawing.Point(104, 25)
        Me.txtInv_Group1.Name = "txtInv_Group1"
        Me.txtInv_Group1.Size = New System.Drawing.Size(258, 22)
        Me.txtInv_Group1.TabIndex = 10
        '
        'Label63
        '
        Me.Label63.AutoSize = True
        Me.Label63.Location = New System.Drawing.Point(41, 128)
        Me.Label63.Name = "Label63"
        Me.Label63.Size = New System.Drawing.Size(61, 16)
        Me.Label63.TabIndex = 19
        Me.Label63.Text = "Group 5 :"
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(41, 28)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(61, 16)
        Me.Label68.TabIndex = 11
        Me.Label68.Text = "Group 1 :"
        '
        'txtInv_Group5
        '
        Me.txtInv_Group5.Location = New System.Drawing.Point(104, 125)
        Me.txtInv_Group5.Name = "txtInv_Group5"
        Me.txtInv_Group5.Size = New System.Drawing.Size(258, 22)
        Me.txtInv_Group5.TabIndex = 18
        '
        'txtInv_Group2
        '
        Me.txtInv_Group2.Location = New System.Drawing.Point(104, 50)
        Me.txtInv_Group2.Name = "txtInv_Group2"
        Me.txtInv_Group2.Size = New System.Drawing.Size(258, 22)
        Me.txtInv_Group2.TabIndex = 12
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(41, 103)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(61, 16)
        Me.Label73.TabIndex = 17
        Me.Label73.Text = "Group 4 :"
        '
        'Label75
        '
        Me.Label75.AutoSize = True
        Me.Label75.Location = New System.Drawing.Point(41, 53)
        Me.Label75.Name = "Label75"
        Me.Label75.Size = New System.Drawing.Size(61, 16)
        Me.Label75.TabIndex = 13
        Me.Label75.Text = "Group 2 :"
        '
        'txtInv_Group4
        '
        Me.txtInv_Group4.Location = New System.Drawing.Point(104, 100)
        Me.txtInv_Group4.Name = "txtInv_Group4"
        Me.txtInv_Group4.Size = New System.Drawing.Size(258, 22)
        Me.txtInv_Group4.TabIndex = 16
        '
        'txtInv_Group3
        '
        Me.txtInv_Group3.Location = New System.Drawing.Point(104, 75)
        Me.txtInv_Group3.Name = "txtInv_Group3"
        Me.txtInv_Group3.Size = New System.Drawing.Size(258, 22)
        Me.txtInv_Group3.TabIndex = 14
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(41, 78)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(61, 16)
        Me.Label79.TabIndex = 15
        Me.Label79.Text = "Group 3 :"
        '
        'gbProdWH
        '
        Me.gbProdWH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbProdWH.Controls.Add(Me.txtPWHG1)
        Me.gbProdWH.Controls.Add(Me.Label31)
        Me.gbProdWH.Controls.Add(Me.Label32)
        Me.gbProdWH.Controls.Add(Me.txtPWHG5)
        Me.gbProdWH.Controls.Add(Me.txtPWHG2)
        Me.gbProdWH.Controls.Add(Me.Label33)
        Me.gbProdWH.Controls.Add(Me.Label34)
        Me.gbProdWH.Controls.Add(Me.txtPWHG4)
        Me.gbProdWH.Controls.Add(Me.txtPWHG3)
        Me.gbProdWH.Controls.Add(Me.Label35)
        Me.gbProdWH.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.gbProdWH.Location = New System.Drawing.Point(9, 490)
        Me.gbProdWH.Name = "gbProdWH"
        Me.gbProdWH.Size = New System.Drawing.Size(316, 158)
        Me.gbProdWH.TabIndex = 22
        Me.gbProdWH.TabStop = False
        Me.gbProdWH.Text = "Production Warehouse Group"
        '
        'txtPWHG1
        '
        Me.txtPWHG1.Location = New System.Drawing.Point(104, 25)
        Me.txtPWHG1.Name = "txtPWHG1"
        Me.txtPWHG1.Size = New System.Drawing.Size(258, 22)
        Me.txtPWHG1.TabIndex = 10
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(41, 128)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(61, 16)
        Me.Label31.TabIndex = 19
        Me.Label31.Text = "Group 5 :"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(41, 28)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(61, 16)
        Me.Label32.TabIndex = 11
        Me.Label32.Text = "Group 1 :"
        '
        'txtPWHG5
        '
        Me.txtPWHG5.Location = New System.Drawing.Point(104, 125)
        Me.txtPWHG5.Name = "txtPWHG5"
        Me.txtPWHG5.Size = New System.Drawing.Size(258, 22)
        Me.txtPWHG5.TabIndex = 18
        '
        'txtPWHG2
        '
        Me.txtPWHG2.Location = New System.Drawing.Point(104, 50)
        Me.txtPWHG2.Name = "txtPWHG2"
        Me.txtPWHG2.Size = New System.Drawing.Size(258, 22)
        Me.txtPWHG2.TabIndex = 12
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(41, 103)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(61, 16)
        Me.Label33.TabIndex = 17
        Me.Label33.Text = "Group 4 :"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(41, 53)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(61, 16)
        Me.Label34.TabIndex = 13
        Me.Label34.Text = "Group 2 :"
        '
        'txtPWHG4
        '
        Me.txtPWHG4.Location = New System.Drawing.Point(104, 100)
        Me.txtPWHG4.Name = "txtPWHG4"
        Me.txtPWHG4.Size = New System.Drawing.Size(258, 22)
        Me.txtPWHG4.TabIndex = 16
        '
        'txtPWHG3
        '
        Me.txtPWHG3.Location = New System.Drawing.Point(104, 75)
        Me.txtPWHG3.Name = "txtPWHG3"
        Me.txtPWHG3.Size = New System.Drawing.Size(258, 22)
        Me.txtPWHG3.TabIndex = 14
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(41, 78)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(61, 16)
        Me.Label35.TabIndex = 15
        Me.Label35.Text = "Group 3 :"
        '
        'gbPC
        '
        Me.gbPC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbPC.Controls.Add(Me.Label26)
        Me.gbPC.Controls.Add(Me.txtPCG5)
        Me.gbPC.Controls.Add(Me.Label27)
        Me.gbPC.Controls.Add(Me.txtPCG4)
        Me.gbPC.Controls.Add(Me.Label28)
        Me.gbPC.Controls.Add(Me.txtPCG3)
        Me.gbPC.Controls.Add(Me.Label29)
        Me.gbPC.Controls.Add(Me.txtPCG2)
        Me.gbPC.Controls.Add(Me.Label30)
        Me.gbPC.Controls.Add(Me.txtPCG1)
        Me.gbPC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbPC.Location = New System.Drawing.Point(9, 161)
        Me.gbPC.Name = "gbPC"
        Me.gbPC.Size = New System.Drawing.Size(316, 158)
        Me.gbPC.TabIndex = 21
        Me.gbPC.TabStop = False
        Me.gbPC.Text = "Profit Center Group"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(42, 126)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(61, 16)
        Me.Label26.TabIndex = 9
        Me.Label26.Text = "Group 5 :"
        '
        'txtPCG5
        '
        Me.txtPCG5.Location = New System.Drawing.Point(104, 123)
        Me.txtPCG5.Name = "txtPCG5"
        Me.txtPCG5.Size = New System.Drawing.Size(258, 22)
        Me.txtPCG5.TabIndex = 8
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(42, 101)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(61, 16)
        Me.Label27.TabIndex = 7
        Me.Label27.Text = "Group 4 :"
        '
        'txtPCG4
        '
        Me.txtPCG4.Location = New System.Drawing.Point(104, 98)
        Me.txtPCG4.Name = "txtPCG4"
        Me.txtPCG4.Size = New System.Drawing.Size(258, 22)
        Me.txtPCG4.TabIndex = 6
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(42, 76)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(61, 16)
        Me.Label28.TabIndex = 5
        Me.Label28.Text = "Group 3 :"
        '
        'txtPCG3
        '
        Me.txtPCG3.Location = New System.Drawing.Point(104, 73)
        Me.txtPCG3.Name = "txtPCG3"
        Me.txtPCG3.Size = New System.Drawing.Size(258, 22)
        Me.txtPCG3.TabIndex = 4
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(42, 51)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(61, 16)
        Me.Label29.TabIndex = 3
        Me.Label29.Text = "Group 2 :"
        '
        'txtPCG2
        '
        Me.txtPCG2.Location = New System.Drawing.Point(104, 48)
        Me.txtPCG2.Name = "txtPCG2"
        Me.txtPCG2.Size = New System.Drawing.Size(258, 22)
        Me.txtPCG2.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(42, 26)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(61, 16)
        Me.Label30.TabIndex = 1
        Me.Label30.Text = "Group 1 :"
        '
        'txtPCG1
        '
        Me.txtPCG1.Location = New System.Drawing.Point(104, 23)
        Me.txtPCG1.Name = "txtPCG1"
        Me.txtPCG1.Size = New System.Drawing.Size(258, 22)
        Me.txtPCG1.TabIndex = 0
        '
        'gbCC
        '
        Me.gbCC.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbCC.Controls.Add(Me.Label20)
        Me.gbCC.Controls.Add(Me.txtCCG5)
        Me.gbCC.Controls.Add(Me.Label19)
        Me.gbCC.Controls.Add(Me.txtCCG4)
        Me.gbCC.Controls.Add(Me.Label18)
        Me.gbCC.Controls.Add(Me.txtCCG3)
        Me.gbCC.Controls.Add(Me.Label17)
        Me.gbCC.Controls.Add(Me.txtCCG2)
        Me.gbCC.Controls.Add(Me.Label16)
        Me.gbCC.Controls.Add(Me.txtCCG1)
        Me.gbCC.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gbCC.Location = New System.Drawing.Point(9, 5)
        Me.gbCC.Name = "gbCC"
        Me.gbCC.Size = New System.Drawing.Size(316, 154)
        Me.gbCC.TabIndex = 4
        Me.gbCC.TabStop = False
        Me.gbCC.Text = "Cost Center Group"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(42, 123)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(61, 16)
        Me.Label20.TabIndex = 9
        Me.Label20.Text = "Group 5 :"
        '
        'txtCCG5
        '
        Me.txtCCG5.Location = New System.Drawing.Point(104, 120)
        Me.txtCCG5.Name = "txtCCG5"
        Me.txtCCG5.Size = New System.Drawing.Size(258, 22)
        Me.txtCCG5.TabIndex = 8
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(42, 98)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(61, 16)
        Me.Label19.TabIndex = 7
        Me.Label19.Text = "Group 4 :"
        '
        'txtCCG4
        '
        Me.txtCCG4.Location = New System.Drawing.Point(104, 95)
        Me.txtCCG4.Name = "txtCCG4"
        Me.txtCCG4.Size = New System.Drawing.Size(258, 22)
        Me.txtCCG4.TabIndex = 6
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(42, 73)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(61, 16)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Group 3 :"
        '
        'txtCCG3
        '
        Me.txtCCG3.Location = New System.Drawing.Point(104, 70)
        Me.txtCCG3.Name = "txtCCG3"
        Me.txtCCG3.Size = New System.Drawing.Size(258, 22)
        Me.txtCCG3.TabIndex = 4
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(42, 48)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(61, 16)
        Me.Label17.TabIndex = 3
        Me.Label17.Text = "Group 2 :"
        '
        'txtCCG2
        '
        Me.txtCCG2.Location = New System.Drawing.Point(104, 45)
        Me.txtCCG2.Name = "txtCCG2"
        Me.txtCCG2.Size = New System.Drawing.Size(258, 22)
        Me.txtCCG2.TabIndex = 2
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(42, 23)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(61, 16)
        Me.Label16.TabIndex = 1
        Me.Label16.Text = "Group 1 :"
        '
        'txtCCG1
        '
        Me.txtCCG1.Location = New System.Drawing.Point(104, 20)
        Me.txtCCG1.Name = "txtCCG1"
        Me.txtCCG1.Size = New System.Drawing.Size(258, 22)
        Me.txtCCG1.TabIndex = 0
        '
        'gbInvWH
        '
        Me.gbInvWH.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbInvWH.Controls.Add(Me.txtWHG1)
        Me.gbInvWH.Controls.Add(Me.Label21)
        Me.gbInvWH.Controls.Add(Me.Label25)
        Me.gbInvWH.Controls.Add(Me.txtWHG5)
        Me.gbInvWH.Controls.Add(Me.txtWHG2)
        Me.gbInvWH.Controls.Add(Me.Label22)
        Me.gbInvWH.Controls.Add(Me.Label24)
        Me.gbInvWH.Controls.Add(Me.txtWHG4)
        Me.gbInvWH.Controls.Add(Me.txtWHG3)
        Me.gbInvWH.Controls.Add(Me.Label23)
        Me.gbInvWH.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.gbInvWH.Location = New System.Drawing.Point(9, 326)
        Me.gbInvWH.Name = "gbInvWH"
        Me.gbInvWH.Size = New System.Drawing.Size(316, 158)
        Me.gbInvWH.TabIndex = 20
        Me.gbInvWH.TabStop = False
        Me.gbInvWH.Text = "Inventory Warehouse Group"
        '
        'txtWHG1
        '
        Me.txtWHG1.Location = New System.Drawing.Point(104, 25)
        Me.txtWHG1.Name = "txtWHG1"
        Me.txtWHG1.Size = New System.Drawing.Size(258, 22)
        Me.txtWHG1.TabIndex = 10
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(41, 128)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(61, 16)
        Me.Label21.TabIndex = 19
        Me.Label21.Text = "Group 5 :"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(41, 28)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 11
        Me.Label25.Text = "Group 1 :"
        '
        'txtWHG5
        '
        Me.txtWHG5.Location = New System.Drawing.Point(104, 125)
        Me.txtWHG5.Name = "txtWHG5"
        Me.txtWHG5.Size = New System.Drawing.Size(258, 22)
        Me.txtWHG5.TabIndex = 18
        '
        'txtWHG2
        '
        Me.txtWHG2.Location = New System.Drawing.Point(104, 50)
        Me.txtWHG2.Name = "txtWHG2"
        Me.txtWHG2.Size = New System.Drawing.Size(258, 22)
        Me.txtWHG2.TabIndex = 12
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(41, 103)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(61, 16)
        Me.Label22.TabIndex = 17
        Me.Label22.Text = "Group 4 :"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(41, 53)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(61, 16)
        Me.Label24.TabIndex = 13
        Me.Label24.Text = "Group 2 :"
        '
        'txtWHG4
        '
        Me.txtWHG4.Location = New System.Drawing.Point(104, 100)
        Me.txtWHG4.Name = "txtWHG4"
        Me.txtWHG4.Size = New System.Drawing.Size(258, 22)
        Me.txtWHG4.TabIndex = 16
        '
        'txtWHG3
        '
        Me.txtWHG3.Location = New System.Drawing.Point(104, 75)
        Me.txtWHG3.Name = "txtWHG3"
        Me.txtWHG3.Size = New System.Drawing.Size(258, 22)
        Me.txtWHG3.TabIndex = 14
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(41, 78)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(61, 16)
        Me.Label23.TabIndex = 15
        Me.Label23.Text = "Group 3 :"
        '
        'tpSales
        '
        Me.tpSales.Controls.Add(Me.gbSO)
        Me.tpSales.Location = New System.Drawing.Point(5, 4)
        Me.tpSales.Name = "tpSales"
        Me.tpSales.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSales.Size = New System.Drawing.Size(785, 609)
        Me.tpSales.TabIndex = 10
        Me.tpSales.Text = "TabPage1"
        Me.tpSales.UseVisualStyleBackColor = True
        '
        'gbSO
        '
        Me.gbSO.Controls.Add(Me.chkSOreqDelivDate)
        Me.gbSO.Controls.Add(Me.chkSOstaggered)
        Me.gbSO.Controls.Add(Me.chkSOreqPO)
        Me.gbSO.Controls.Add(Me.chkSOeditPrice)
        Me.gbSO.Location = New System.Drawing.Point(6, 6)
        Me.gbSO.Name = "gbSO"
        Me.gbSO.Size = New System.Drawing.Size(773, 136)
        Me.gbSO.TabIndex = 0
        Me.gbSO.TabStop = False
        Me.gbSO.Text = "Sales Order"
        '
        'chkSOreqDelivDate
        '
        Me.chkSOreqDelivDate.AutoSize = True
        Me.chkSOreqDelivDate.Location = New System.Drawing.Point(50, 100)
        Me.chkSOreqDelivDate.Name = "chkSOreqDelivDate"
        Me.chkSOreqDelivDate.Size = New System.Drawing.Size(174, 21)
        Me.chkSOreqDelivDate.TabIndex = 3
        Me.chkSOreqDelivDate.Text = "Required Delivery Date"
        Me.chkSOreqDelivDate.UseVisualStyleBackColor = True
        '
        'chkSOstaggered
        '
        Me.chkSOstaggered.AutoSize = True
        Me.chkSOstaggered.Location = New System.Drawing.Point(50, 77)
        Me.chkSOstaggered.Name = "chkSOstaggered"
        Me.chkSOstaggered.Size = New System.Drawing.Size(196, 21)
        Me.chkSOstaggered.TabIndex = 2
        Me.chkSOstaggered.Text = "Enable Staggered Delivery"
        Me.chkSOstaggered.UseVisualStyleBackColor = True
        '
        'chkSOreqPO
        '
        Me.chkSOreqPO.AutoSize = True
        Me.chkSOreqPO.Location = New System.Drawing.Point(50, 54)
        Me.chkSOreqPO.Name = "chkSOreqPO"
        Me.chkSOreqPO.Size = New System.Drawing.Size(261, 21)
        Me.chkSOreqPO.TabIndex = 1
        Me.chkSOreqPO.Text = "Require Customer PO Reference No."
        Me.chkSOreqPO.UseVisualStyleBackColor = True
        '
        'chkSOeditPrice
        '
        Me.chkSOeditPrice.AutoSize = True
        Me.chkSOeditPrice.Location = New System.Drawing.Point(50, 32)
        Me.chkSOeditPrice.Name = "chkSOeditPrice"
        Me.chkSOeditPrice.Size = New System.Drawing.Size(154, 21)
        Me.chkSOeditPrice.TabIndex = 0
        Me.chkSOeditPrice.Text = "Enable Price Editing"
        Me.chkSOeditPrice.UseVisualStyleBackColor = True
        '
        'tpInventory
        '
        Me.tpInventory.Controls.Add(Me.GroupBox8)
        Me.tpInventory.Location = New System.Drawing.Point(5, 4)
        Me.tpInventory.Name = "tpInventory"
        Me.tpInventory.Padding = New System.Windows.Forms.Padding(3)
        Me.tpInventory.Size = New System.Drawing.Size(785, 609)
        Me.tpInventory.TabIndex = 11
        Me.tpInventory.Text = "TabPage1"
        Me.tpInventory.UseVisualStyleBackColor = True
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.GroupBox18)
        Me.GroupBox8.Controls.Add(Me.GroupBox17)
        Me.GroupBox8.Controls.Add(Me.GroupBox16)
        Me.GroupBox8.Controls.Add(Me.GroupBox15)
        Me.GroupBox8.Controls.Add(Me.chkRR_RestrictWHSEItem)
        Me.GroupBox8.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(773, 592)
        Me.GroupBox8.TabIndex = 2
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Inventory"
        '
        'GroupBox18
        '
        Me.GroupBox18.Controls.Add(Me.rbPOS_POS)
        Me.GroupBox18.Controls.Add(Me.rbPOS_CSI)
        Me.GroupBox18.Location = New System.Drawing.Point(220, 333)
        Me.GroupBox18.Name = "GroupBox18"
        Me.GroupBox18.Size = New System.Drawing.Size(338, 88)
        Me.GroupBox18.TabIndex = 35
        Me.GroupBox18.TabStop = False
        Me.GroupBox18.Text = "POS Inventory Movement"
        '
        'rbPOS_POS
        '
        Me.rbPOS_POS.AutoSize = True
        Me.rbPOS_POS.Location = New System.Drawing.Point(26, 50)
        Me.rbPOS_POS.Name = "rbPOS_POS"
        Me.rbPOS_POS.Size = New System.Drawing.Size(106, 21)
        Me.rbPOS_POS.TabIndex = 1
        Me.rbPOS_POS.TabStop = True
        Me.rbPOS_POS.Text = "Point of Sale"
        Me.rbPOS_POS.UseVisualStyleBackColor = True
        '
        'rbPOS_CSI
        '
        Me.rbPOS_CSI.AutoSize = True
        Me.rbPOS_CSI.Location = New System.Drawing.Point(26, 23)
        Me.rbPOS_CSI.Name = "rbPOS_CSI"
        Me.rbPOS_CSI.Size = New System.Drawing.Size(195, 21)
        Me.rbPOS_CSI.TabIndex = 0
        Me.rbPOS_CSI.TabStop = True
        Me.rbPOS_CSI.Text = "Cash/Charge Sales Invoice"
        Me.rbPOS_CSI.UseVisualStyleBackColor = True
        '
        'GroupBox17
        '
        Me.GroupBox17.Controls.Add(Me.rbCSI_JV)
        Me.GroupBox17.Controls.Add(Me.rbCSI_Inventory)
        Me.GroupBox17.Location = New System.Drawing.Point(220, 239)
        Me.GroupBox17.Name = "GroupBox17"
        Me.GroupBox17.Size = New System.Drawing.Size(338, 88)
        Me.GroupBox17.TabIndex = 34
        Me.GroupBox17.TabStop = False
        Me.GroupBox17.Text = "Cash Sales Invoice Cost of Sales Book"
        '
        'rbCSI_JV
        '
        Me.rbCSI_JV.AutoSize = True
        Me.rbCSI_JV.Location = New System.Drawing.Point(26, 50)
        Me.rbCSI_JV.Name = "rbCSI_JV"
        Me.rbCSI_JV.Size = New System.Drawing.Size(128, 21)
        Me.rbCSI_JV.TabIndex = 1
        Me.rbCSI_JV.TabStop = True
        Me.rbCSI_JV.Text = "General Journal"
        Me.rbCSI_JV.UseVisualStyleBackColor = True
        '
        'rbCSI_Inventory
        '
        Me.rbCSI_Inventory.AutoSize = True
        Me.rbCSI_Inventory.Location = New System.Drawing.Point(26, 23)
        Me.rbCSI_Inventory.Name = "rbCSI_Inventory"
        Me.rbCSI_Inventory.Size = New System.Drawing.Size(84, 21)
        Me.rbCSI_Inventory.TabIndex = 0
        Me.rbCSI_Inventory.TabStop = True
        Me.rbCSI_Inventory.Text = "Inventory"
        Me.rbCSI_Inventory.UseVisualStyleBackColor = True
        '
        'GroupBox16
        '
        Me.GroupBox16.Controls.Add(Me.rbRR_Purchase)
        Me.GroupBox16.Controls.Add(Me.rbRR_Inventory)
        Me.GroupBox16.Location = New System.Drawing.Point(220, 145)
        Me.GroupBox16.Name = "GroupBox16"
        Me.GroupBox16.Size = New System.Drawing.Size(338, 88)
        Me.GroupBox16.TabIndex = 33
        Me.GroupBox16.TabStop = False
        Me.GroupBox16.Text = "RR Book"
        '
        'rbRR_Purchase
        '
        Me.rbRR_Purchase.AutoSize = True
        Me.rbRR_Purchase.Location = New System.Drawing.Point(26, 50)
        Me.rbRR_Purchase.Name = "rbRR_Purchase"
        Me.rbRR_Purchase.Size = New System.Drawing.Size(86, 21)
        Me.rbRR_Purchase.TabIndex = 1
        Me.rbRR_Purchase.TabStop = True
        Me.rbRR_Purchase.Text = "Purchase"
        Me.rbRR_Purchase.UseVisualStyleBackColor = True
        '
        'rbRR_Inventory
        '
        Me.rbRR_Inventory.AutoSize = True
        Me.rbRR_Inventory.Location = New System.Drawing.Point(26, 23)
        Me.rbRR_Inventory.Name = "rbRR_Inventory"
        Me.rbRR_Inventory.Size = New System.Drawing.Size(84, 21)
        Me.rbRR_Inventory.TabIndex = 0
        Me.rbRR_Inventory.TabStop = True
        Me.rbRR_Inventory.Text = "Inventory"
        Me.rbRR_Inventory.UseVisualStyleBackColor = True
        '
        'GroupBox15
        '
        Me.GroupBox15.Controls.Add(Me.rbInv_WAUC)
        Me.GroupBox15.Controls.Add(Me.rbInv_SC)
        Me.GroupBox15.Location = New System.Drawing.Point(220, 51)
        Me.GroupBox15.Name = "GroupBox15"
        Me.GroupBox15.Size = New System.Drawing.Size(338, 88)
        Me.GroupBox15.TabIndex = 32
        Me.GroupBox15.TabStop = False
        Me.GroupBox15.Text = "Inventory Computation"
        '
        'rbInv_WAUC
        '
        Me.rbInv_WAUC.AutoSize = True
        Me.rbInv_WAUC.Location = New System.Drawing.Point(26, 50)
        Me.rbInv_WAUC.Name = "rbInv_WAUC"
        Me.rbInv_WAUC.Size = New System.Drawing.Size(111, 21)
        Me.rbInv_WAUC.TabIndex = 1
        Me.rbInv_WAUC.TabStop = True
        Me.rbInv_WAUC.Text = "Average Cost"
        Me.rbInv_WAUC.UseVisualStyleBackColor = True
        '
        'rbInv_SC
        '
        Me.rbInv_SC.AutoSize = True
        Me.rbInv_SC.Location = New System.Drawing.Point(26, 23)
        Me.rbInv_SC.Name = "rbInv_SC"
        Me.rbInv_SC.Size = New System.Drawing.Size(116, 21)
        Me.rbInv_SC.TabIndex = 0
        Me.rbInv_SC.TabStop = True
        Me.rbInv_SC.Text = "Standard Cost"
        Me.rbInv_SC.UseVisualStyleBackColor = True
        '
        'chkRR_RestrictWHSEItem
        '
        Me.chkRR_RestrictWHSEItem.AutoSize = True
        Me.chkRR_RestrictWHSEItem.Location = New System.Drawing.Point(220, 22)
        Me.chkRR_RestrictWHSEItem.Name = "chkRR_RestrictWHSEItem"
        Me.chkRR_RestrictWHSEItem.Size = New System.Drawing.Size(297, 21)
        Me.chkRR_RestrictWHSEItem.TabIndex = 2
        Me.chkRR_RestrictWHSEItem.Text = "Only show Item from accessible warehouse"
        Me.chkRR_RestrictWHSEItem.UseVisualStyleBackColor = True
        Me.chkRR_RestrictWHSEItem.Visible = False
        '
        'tpCoop
        '
        Me.tpCoop.Controls.Add(Me.gbCoop)
        Me.tpCoop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.tpCoop.Location = New System.Drawing.Point(5, 4)
        Me.tpCoop.Name = "tpCoop"
        Me.tpCoop.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCoop.Size = New System.Drawing.Size(785, 609)
        Me.tpCoop.TabIndex = 12
        Me.tpCoop.UseVisualStyleBackColor = True
        '
        'gbCoop
        '
        Me.gbCoop.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbCoop.Controls.Add(Me.txtTCPcode)
        Me.gbCoop.Controls.Add(Me.txtTCPtitle)
        Me.gbCoop.Controls.Add(Me.txtTCCcode)
        Me.gbCoop.Controls.Add(Me.txtTCCtitle)
        Me.gbCoop.Controls.Add(Me.Label43)
        Me.gbCoop.Controls.Add(Me.txtDFCScode)
        Me.gbCoop.Controls.Add(Me.txtDFCStitle)
        Me.gbCoop.Controls.Add(Me.Label42)
        Me.gbCoop.Controls.Add(Me.txtPUCPcode)
        Me.gbCoop.Controls.Add(Me.txtPUCPtitle)
        Me.gbCoop.Controls.Add(Me.txtPUCCcode)
        Me.gbCoop.Controls.Add(Me.txtPUCCtitle)
        Me.gbCoop.Controls.Add(Me.Label41)
        Me.gbCoop.Controls.Add(Me.txtSRPcode)
        Me.gbCoop.Controls.Add(Me.txtSRPtitle)
        Me.gbCoop.Controls.Add(Me.txtSRCcode)
        Me.gbCoop.Controls.Add(Me.txtSRCtitle)
        Me.gbCoop.Controls.Add(Me.Label40)
        Me.gbCoop.Controls.Add(Me.Label39)
        Me.gbCoop.Controls.Add(Me.txtSCPcode)
        Me.gbCoop.Controls.Add(Me.txtSCPtitle)
        Me.gbCoop.Controls.Add(Me.Label38)
        Me.gbCoop.Controls.Add(Me.txtSCCcode)
        Me.gbCoop.Controls.Add(Me.Label37)
        Me.gbCoop.Controls.Add(Me.txtSCCtitle)
        Me.gbCoop.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.gbCoop.Location = New System.Drawing.Point(6, 6)
        Me.gbCoop.Name = "gbCoop"
        Me.gbCoop.Size = New System.Drawing.Size(773, 210)
        Me.gbCoop.TabIndex = 0
        Me.gbCoop.TabStop = False
        Me.gbCoop.Text = " "
        '
        'txtTCPcode
        '
        Me.txtTCPcode.Location = New System.Drawing.Point(467, 124)
        Me.txtTCPcode.Name = "txtTCPcode"
        Me.txtTCPcode.Size = New System.Drawing.Size(70, 21)
        Me.txtTCPcode.TabIndex = 24
        '
        'txtTCPtitle
        '
        Me.txtTCPtitle.Location = New System.Drawing.Point(538, 124)
        Me.txtTCPtitle.Name = "txtTCPtitle"
        Me.txtTCPtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtTCPtitle.TabIndex = 23
        '
        'txtTCCcode
        '
        Me.txtTCCcode.Location = New System.Drawing.Point(158, 124)
        Me.txtTCCcode.Name = "txtTCCcode"
        Me.txtTCCcode.Size = New System.Drawing.Size(70, 21)
        Me.txtTCCcode.TabIndex = 22
        '
        'txtTCCtitle
        '
        Me.txtTCCtitle.Location = New System.Drawing.Point(229, 124)
        Me.txtTCCtitle.Name = "txtTCCtitle"
        Me.txtTCCtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtTCCtitle.TabIndex = 21
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(28, 160)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(184, 15)
        Me.Label43.TabIndex = 20
        Me.Label43.Text = "Deposit for Capital Subscription :"
        '
        'txtDFCScode
        '
        Me.txtDFCScode.Location = New System.Drawing.Point(218, 157)
        Me.txtDFCScode.Name = "txtDFCScode"
        Me.txtDFCScode.Size = New System.Drawing.Size(70, 21)
        Me.txtDFCScode.TabIndex = 19
        '
        'txtDFCStitle
        '
        Me.txtDFCStitle.Location = New System.Drawing.Point(289, 157)
        Me.txtDFCStitle.Name = "txtDFCStitle"
        Me.txtDFCStitle.Size = New System.Drawing.Size(308, 21)
        Me.txtDFCStitle.TabIndex = 18
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(51, 127)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(101, 15)
        Me.Label42.TabIndex = 17
        Me.Label42.Text = "Treasury Capital :"
        '
        'txtPUCPcode
        '
        Me.txtPUCPcode.Location = New System.Drawing.Point(467, 101)
        Me.txtPUCPcode.Name = "txtPUCPcode"
        Me.txtPUCPcode.Size = New System.Drawing.Size(70, 21)
        Me.txtPUCPcode.TabIndex = 16
        '
        'txtPUCPtitle
        '
        Me.txtPUCPtitle.Location = New System.Drawing.Point(538, 101)
        Me.txtPUCPtitle.Name = "txtPUCPtitle"
        Me.txtPUCPtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtPUCPtitle.TabIndex = 15
        '
        'txtPUCCcode
        '
        Me.txtPUCCcode.Location = New System.Drawing.Point(158, 101)
        Me.txtPUCCcode.Name = "txtPUCCcode"
        Me.txtPUCCcode.Size = New System.Drawing.Size(70, 21)
        Me.txtPUCCcode.TabIndex = 14
        '
        'txtPUCCtitle
        '
        Me.txtPUCCtitle.Location = New System.Drawing.Point(229, 101)
        Me.txtPUCCtitle.Name = "txtPUCCtitle"
        Me.txtPUCCtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtPUCCtitle.TabIndex = 13
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(55, 104)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(97, 15)
        Me.Label41.TabIndex = 12
        Me.Label41.Text = "Paid-up Capital :"
        '
        'txtSRPcode
        '
        Me.txtSRPcode.Location = New System.Drawing.Point(467, 78)
        Me.txtSRPcode.Name = "txtSRPcode"
        Me.txtSRPcode.Size = New System.Drawing.Size(70, 21)
        Me.txtSRPcode.TabIndex = 11
        '
        'txtSRPtitle
        '
        Me.txtSRPtitle.Location = New System.Drawing.Point(538, 78)
        Me.txtSRPtitle.Name = "txtSRPtitle"
        Me.txtSRPtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtSRPtitle.TabIndex = 10
        '
        'txtSRCcode
        '
        Me.txtSRCcode.Location = New System.Drawing.Point(158, 78)
        Me.txtSRCcode.Name = "txtSRCcode"
        Me.txtSRCcode.Size = New System.Drawing.Size(70, 21)
        Me.txtSRCcode.TabIndex = 9
        '
        'txtSRCtitle
        '
        Me.txtSRCtitle.Location = New System.Drawing.Point(229, 78)
        Me.txtSRCtitle.Name = "txtSRCtitle"
        Me.txtSRCtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtSRCtitle.TabIndex = 8
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(7, 81)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(145, 15)
        Me.Label40.TabIndex = 7
        Me.Label40.Text = "Subscription Receivable :"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(577, 32)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(58, 15)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "Preferred"
        '
        'txtSCPcode
        '
        Me.txtSCPcode.Location = New System.Drawing.Point(467, 55)
        Me.txtSCPcode.Name = "txtSCPcode"
        Me.txtSCPcode.Size = New System.Drawing.Size(70, 21)
        Me.txtSCPcode.TabIndex = 5
        '
        'txtSCPtitle
        '
        Me.txtSCPtitle.Location = New System.Drawing.Point(538, 55)
        Me.txtSCPtitle.Name = "txtSCPtitle"
        Me.txtSCPtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtSCPtitle.TabIndex = 4
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(257, 32)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(58, 15)
        Me.Label38.TabIndex = 3
        Me.Label38.Text = "Common"
        '
        'txtSCCcode
        '
        Me.txtSCCcode.Location = New System.Drawing.Point(158, 55)
        Me.txtSCCcode.Name = "txtSCCcode"
        Me.txtSCCcode.Size = New System.Drawing.Size(70, 21)
        Me.txtSCCcode.TabIndex = 2
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(36, 59)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(116, 15)
        Me.Label37.TabIndex = 1
        Me.Label37.Text = "Subscribed Capital :"
        '
        'txtSCCtitle
        '
        Me.txtSCCtitle.Location = New System.Drawing.Point(229, 55)
        Me.txtSCCtitle.Name = "txtSCCtitle"
        Me.txtSCCtitle.Size = New System.Drawing.Size(227, 21)
        Me.txtSCCtitle.TabIndex = 0
        '
        'tpCollection
        '
        Me.tpCollection.Location = New System.Drawing.Point(5, 4)
        Me.tpCollection.Name = "tpCollection"
        Me.tpCollection.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCollection.Size = New System.Drawing.Size(785, 609)
        Me.tpCollection.TabIndex = 13
        Me.tpCollection.Text = "TabPage1"
        Me.tpCollection.UseVisualStyleBackColor = True
        '
        'tpProduction
        '
        Me.tpProduction.Controls.Add(Me.gbJO)
        Me.tpProduction.Location = New System.Drawing.Point(5, 4)
        Me.tpProduction.Name = "tpProduction"
        Me.tpProduction.Padding = New System.Windows.Forms.Padding(3)
        Me.tpProduction.Size = New System.Drawing.Size(785, 609)
        Me.tpProduction.TabIndex = 14
        Me.tpProduction.Text = "TabPage1"
        Me.tpProduction.UseVisualStyleBackColor = True
        '
        'gbJO
        '
        Me.gbJO.Controls.Add(Me.chkJOperSOitem)
        Me.gbJO.Location = New System.Drawing.Point(3, 6)
        Me.gbJO.Name = "gbJO"
        Me.gbJO.Size = New System.Drawing.Size(773, 136)
        Me.gbJO.TabIndex = 1
        Me.gbJO.TabStop = False
        Me.gbJO.Text = "Job Order"
        '
        'chkJOperSOitem
        '
        Me.chkJOperSOitem.AutoSize = True
        Me.chkJOperSOitem.Location = New System.Drawing.Point(50, 32)
        Me.chkJOperSOitem.Name = "chkJOperSOitem"
        Me.chkJOperSOitem.Size = New System.Drawing.Size(310, 21)
        Me.chkJOperSOitem.TabIndex = 0
        Me.chkJOperSOitem.Text = "Create different Job Order per SO Line Items"
        Me.chkJOperSOitem.UseVisualStyleBackColor = True
        '
        'tpEmail
        '
        Me.tpEmail.Controls.Add(Me.MetroLabel3)
        Me.tpEmail.Controls.Add(Me.txtEmailPass)
        Me.tpEmail.Controls.Add(Me.MetroLabel4)
        Me.tpEmail.Controls.Add(Me.txtEmailAddress)
        Me.tpEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.tpEmail.Location = New System.Drawing.Point(5, 4)
        Me.tpEmail.Name = "tpEmail"
        Me.tpEmail.Padding = New System.Windows.Forms.Padding(3)
        Me.tpEmail.Size = New System.Drawing.Size(785, 609)
        Me.tpEmail.TabIndex = 15
        Me.tpEmail.UseVisualStyleBackColor = True
        '
        'MetroLabel3
        '
        Me.MetroLabel3.AutoSize = True
        Me.MetroLabel3.Location = New System.Drawing.Point(40, 22)
        Me.MetroLabel3.Name = "MetroLabel3"
        Me.MetroLabel3.Size = New System.Drawing.Size(48, 19)
        Me.MetroLabel3.TabIndex = 53
        Me.MetroLabel3.Text = "Email :"
        '
        'txtEmailPass
        '
        '
        '
        '
        Me.txtEmailPass.CustomButton.Image = Nothing
        Me.txtEmailPass.CustomButton.Location = New System.Drawing.Point(324, 1)
        Me.txtEmailPass.CustomButton.Name = ""
        Me.txtEmailPass.CustomButton.Size = New System.Drawing.Size(23, 23)
        Me.txtEmailPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtEmailPass.CustomButton.TabIndex = 1
        Me.txtEmailPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtEmailPass.CustomButton.UseSelectable = True
        Me.txtEmailPass.CustomButton.Visible = False
        Me.txtEmailPass.Lines = New String(-1) {}
        Me.txtEmailPass.Location = New System.Drawing.Point(90, 47)
        Me.txtEmailPass.MaxLength = 32767
        Me.txtEmailPass.Name = "txtEmailPass"
        Me.txtEmailPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(8226)
        Me.txtEmailPass.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEmailPass.SelectedText = ""
        Me.txtEmailPass.SelectionLength = 0
        Me.txtEmailPass.SelectionStart = 0
        Me.txtEmailPass.ShortcutsEnabled = True
        Me.txtEmailPass.Size = New System.Drawing.Size(348, 25)
        Me.txtEmailPass.Style = MetroFramework.MetroColorStyle.Black
        Me.txtEmailPass.TabIndex = 56
        Me.txtEmailPass.UseSelectable = True
        Me.txtEmailPass.UseStyleColors = True
        Me.txtEmailPass.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtEmailPass.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'MetroLabel4
        '
        Me.MetroLabel4.AutoSize = True
        Me.MetroLabel4.Location = New System.Drawing.Point(22, 53)
        Me.MetroLabel4.Name = "MetroLabel4"
        Me.MetroLabel4.Size = New System.Drawing.Size(66, 19)
        Me.MetroLabel4.TabIndex = 54
        Me.MetroLabel4.Text = "Password:"
        '
        'txtEmailAddress
        '
        '
        '
        '
        Me.txtEmailAddress.CustomButton.Image = Nothing
        Me.txtEmailAddress.CustomButton.Location = New System.Drawing.Point(324, 1)
        Me.txtEmailAddress.CustomButton.Name = ""
        Me.txtEmailAddress.CustomButton.Size = New System.Drawing.Size(23, 23)
        Me.txtEmailAddress.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtEmailAddress.CustomButton.TabIndex = 1
        Me.txtEmailAddress.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtEmailAddress.CustomButton.UseSelectable = True
        Me.txtEmailAddress.CustomButton.Visible = False
        Me.txtEmailAddress.Lines = New String(-1) {}
        Me.txtEmailAddress.Location = New System.Drawing.Point(90, 16)
        Me.txtEmailAddress.MaxLength = 32767
        Me.txtEmailAddress.Name = "txtEmailAddress"
        Me.txtEmailAddress.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtEmailAddress.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtEmailAddress.SelectedText = ""
        Me.txtEmailAddress.SelectionLength = 0
        Me.txtEmailAddress.SelectionStart = 0
        Me.txtEmailAddress.ShortcutsEnabled = True
        Me.txtEmailAddress.Size = New System.Drawing.Size(348, 25)
        Me.txtEmailAddress.Style = MetroFramework.MetroColorStyle.Black
        Me.txtEmailAddress.TabIndex = 55
        Me.txtEmailAddress.UseSelectable = True
        Me.txtEmailAddress.UseStyleColors = True
        Me.txtEmailAddress.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtEmailAddress.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'tpSLS_SLP
        '
        Me.tpSLS_SLP.Controls.Add(Me.GroupBox21)
        Me.tpSLS_SLP.Controls.Add(Me.GroupBox20)
        Me.tpSLS_SLP.Location = New System.Drawing.Point(5, 4)
        Me.tpSLS_SLP.Name = "tpSLS_SLP"
        Me.tpSLS_SLP.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSLS_SLP.Size = New System.Drawing.Size(785, 609)
        Me.tpSLS_SLP.TabIndex = 17
        Me.tpSLS_SLP.Text = "TabPage1"
        Me.tpSLS_SLP.UseVisualStyleBackColor = True
        '
        'GroupBox21
        '
        Me.GroupBox21.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox21.BackColor = System.Drawing.Color.White
        Me.GroupBox21.Controls.Add(Me.lvlSLP)
        Me.GroupBox21.Controls.Add(Me.Label57)
        Me.GroupBox21.Controls.Add(Me.btnRemove_SLP)
        Me.GroupBox21.Controls.Add(Me.btnAdd_SLP)
        Me.GroupBox21.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox21.Location = New System.Drawing.Point(19, 154)
        Me.GroupBox21.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox21.Name = "GroupBox21"
        Me.GroupBox21.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox21.Size = New System.Drawing.Size(720, 139)
        Me.GroupBox21.TabIndex = 31
        Me.GroupBox21.TabStop = False
        Me.GroupBox21.Text = "Summary List of Purchases"
        '
        'lvlSLP
        '
        Me.lvlSLP.FormattingEnabled = True
        Me.lvlSLP.ItemHeight = 15
        Me.lvlSLP.Location = New System.Drawing.Point(215, 33)
        Me.lvlSLP.Name = "lvlSLP"
        Me.lvlSLP.Size = New System.Drawing.Size(338, 79)
        Me.lvlSLP.TabIndex = 19
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Location = New System.Drawing.Point(116, 33)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(93, 30)
        Me.Label57.TabIndex = 13
        Me.Label57.Text = "Reference Type" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & " for SLP :"
        '
        'btnRemove_SLP
        '
        Me.btnRemove_SLP.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnRemove_SLP.Location = New System.Drawing.Point(555, 64)
        Me.btnRemove_SLP.Name = "btnRemove_SLP"
        Me.btnRemove_SLP.Size = New System.Drawing.Size(80, 30)
        Me.btnRemove_SLP.TabIndex = 4
        Me.btnRemove_SLP.Text = "Remove"
        Me.btnRemove_SLP.UseVisualStyleBackColor = True
        '
        'btnAdd_SLP
        '
        Me.btnAdd_SLP.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnAdd_SLP.Location = New System.Drawing.Point(555, 32)
        Me.btnAdd_SLP.Name = "btnAdd_SLP"
        Me.btnAdd_SLP.Size = New System.Drawing.Size(80, 30)
        Me.btnAdd_SLP.TabIndex = 3
        Me.btnAdd_SLP.Text = "Add"
        Me.btnAdd_SLP.UseVisualStyleBackColor = True
        '
        'GroupBox20
        '
        Me.GroupBox20.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox20.BackColor = System.Drawing.Color.White
        Me.GroupBox20.Controls.Add(Me.Label55)
        Me.GroupBox20.Controls.Add(Me.lvlSLS)
        Me.GroupBox20.Controls.Add(Me.btnRemove_SLS)
        Me.GroupBox20.Controls.Add(Me.btnAdd_SLS)
        Me.GroupBox20.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.GroupBox20.Location = New System.Drawing.Point(19, 12)
        Me.GroupBox20.Margin = New System.Windows.Forms.Padding(0)
        Me.GroupBox20.Name = "GroupBox20"
        Me.GroupBox20.Padding = New System.Windows.Forms.Padding(0)
        Me.GroupBox20.Size = New System.Drawing.Size(720, 128)
        Me.GroupBox20.TabIndex = 30
        Me.GroupBox20.TabStop = False
        Me.GroupBox20.Text = "Summary List of Sales"
        '
        'Label55
        '
        Me.Label55.AutoSize = True
        Me.Label55.Location = New System.Drawing.Point(113, 33)
        Me.Label55.Name = "Label55"
        Me.Label55.Size = New System.Drawing.Size(96, 30)
        Me.Label55.TabIndex = 20
        Me.Label55.Text = "Reference Type " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "for SLS :"
        '
        'lvlSLS
        '
        Me.lvlSLS.FormattingEnabled = True
        Me.lvlSLS.ItemHeight = 15
        Me.lvlSLS.Location = New System.Drawing.Point(215, 33)
        Me.lvlSLS.Name = "lvlSLS"
        Me.lvlSLS.Size = New System.Drawing.Size(338, 79)
        Me.lvlSLS.TabIndex = 19
        '
        'btnRemove_SLS
        '
        Me.btnRemove_SLS.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnRemove_SLS.Location = New System.Drawing.Point(555, 64)
        Me.btnRemove_SLS.Name = "btnRemove_SLS"
        Me.btnRemove_SLS.Size = New System.Drawing.Size(80, 30)
        Me.btnRemove_SLS.TabIndex = 4
        Me.btnRemove_SLS.Text = "Remove"
        Me.btnRemove_SLS.UseVisualStyleBackColor = True
        '
        'btnAdd_SLS
        '
        Me.btnAdd_SLS.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnAdd_SLS.Location = New System.Drawing.Point(555, 32)
        Me.btnAdd_SLS.Name = "btnAdd_SLS"
        Me.btnAdd_SLS.Size = New System.Drawing.Size(80, 30)
        Me.btnAdd_SLS.TabIndex = 3
        Me.btnAdd_SLS.Text = "Add"
        Me.btnAdd_SLS.UseVisualStyleBackColor = True
        '
        'tpBIRReminders
        '
        Me.tpBIRReminders.Controls.Add(Me.GroupBox26)
        Me.tpBIRReminders.Location = New System.Drawing.Point(5, 4)
        Me.tpBIRReminders.Name = "tpBIRReminders"
        Me.tpBIRReminders.Padding = New System.Windows.Forms.Padding(3)
        Me.tpBIRReminders.Size = New System.Drawing.Size(785, 609)
        Me.tpBIRReminders.TabIndex = 19
        Me.tpBIRReminders.Text = "TabPage1"
        Me.tpBIRReminders.UseVisualStyleBackColor = True
        '
        'GroupBox26
        '
        Me.GroupBox26.Controls.Add(Me.nudBIR_WithIN)
        Me.GroupBox26.Controls.Add(Me.Label84)
        Me.GroupBox26.Controls.Add(Me.btnBIR_New)
        Me.GroupBox26.Controls.Add(Me.btnBIR_Remove)
        Me.GroupBox26.Controls.Add(Me.btnBIR_Add)
        Me.GroupBox26.Controls.Add(Me.p)
        Me.GroupBox26.Controls.Add(Me.lvlBIRReminders)
        Me.GroupBox26.Location = New System.Drawing.Point(4, 6)
        Me.GroupBox26.Name = "GroupBox26"
        Me.GroupBox26.Size = New System.Drawing.Size(775, 533)
        Me.GroupBox26.TabIndex = 1353
        Me.GroupBox26.TabStop = False
        '
        'nudBIR_WithIN
        '
        Me.nudBIR_WithIN.Location = New System.Drawing.Point(209, 22)
        Me.nudBIR_WithIN.Name = "nudBIR_WithIN"
        Me.nudBIR_WithIN.Size = New System.Drawing.Size(134, 23)
        Me.nudBIR_WithIN.TabIndex = 1358
        Me.nudBIR_WithIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Location = New System.Drawing.Point(21, 19)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(160, 34)
        Me.Label84.TabIndex = 1357
        Me.Label84.Text = "Number of Days Before " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Reminders Show:"
        '
        'btnBIR_New
        '
        Me.btnBIR_New.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBIR_New.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBIR_New.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBIR_New.ForeColor = System.Drawing.Color.White
        Me.btnBIR_New.Location = New System.Drawing.Point(636, 84)
        Me.btnBIR_New.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBIR_New.Name = "btnBIR_New"
        Me.btnBIR_New.Size = New System.Drawing.Size(124, 24)
        Me.btnBIR_New.TabIndex = 1355
        Me.btnBIR_New.Text = "New"
        Me.btnBIR_New.UseVisualStyleBackColor = False
        '
        'btnBIR_Remove
        '
        Me.btnBIR_Remove.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBIR_Remove.Enabled = False
        Me.btnBIR_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBIR_Remove.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBIR_Remove.ForeColor = System.Drawing.Color.White
        Me.btnBIR_Remove.Location = New System.Drawing.Point(636, 148)
        Me.btnBIR_Remove.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBIR_Remove.Name = "btnBIR_Remove"
        Me.btnBIR_Remove.Size = New System.Drawing.Size(124, 24)
        Me.btnBIR_Remove.TabIndex = 1354
        Me.btnBIR_Remove.Text = "Remove"
        Me.btnBIR_Remove.UseVisualStyleBackColor = False
        '
        'btnBIR_Add
        '
        Me.btnBIR_Add.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnBIR_Add.Enabled = False
        Me.btnBIR_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnBIR_Add.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBIR_Add.ForeColor = System.Drawing.Color.White
        Me.btnBIR_Add.Location = New System.Drawing.Point(636, 116)
        Me.btnBIR_Add.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBIR_Add.Name = "btnBIR_Add"
        Me.btnBIR_Add.Size = New System.Drawing.Size(124, 24)
        Me.btnBIR_Add.TabIndex = 1353
        Me.btnBIR_Add.Text = "Add"
        Me.btnBIR_Add.UseVisualStyleBackColor = False
        '
        'p
        '
        Me.p.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.p.Controls.Add(Me.cbBIR_Period)
        Me.p.Controls.Add(Me.cbBIR_Date)
        Me.p.Controls.Add(Me.cbBIR_Month)
        Me.p.Controls.Add(Me.Label83)
        Me.p.Controls.Add(Me.Label82)
        Me.p.Controls.Add(Me.Label80)
        Me.p.Controls.Add(Me.txtBIR_Reminder)
        Me.p.Controls.Add(Me.Label81)
        Me.p.Location = New System.Drawing.Point(6, 69)
        Me.p.Name = "p"
        Me.p.Size = New System.Drawing.Size(614, 111)
        Me.p.TabIndex = 1352
        Me.p.TabStop = False
        '
        'cbBIR_Period
        '
        Me.cbBIR_Period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBIR_Period.FormattingEnabled = True
        Me.cbBIR_Period.Items.AddRange(New Object() {"Monthly", "Quarterly", "Yearly"})
        Me.cbBIR_Period.Location = New System.Drawing.Point(86, 74)
        Me.cbBIR_Period.Name = "cbBIR_Period"
        Me.cbBIR_Period.Size = New System.Drawing.Size(201, 24)
        Me.cbBIR_Period.TabIndex = 1323
        '
        'cbBIR_Date
        '
        Me.cbBIR_Date.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBIR_Date.FormattingEnabled = True
        Me.cbBIR_Date.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        Me.cbBIR_Date.Location = New System.Drawing.Point(390, 44)
        Me.cbBIR_Date.Name = "cbBIR_Date"
        Me.cbBIR_Date.Size = New System.Drawing.Size(201, 24)
        Me.cbBIR_Date.TabIndex = 1322
        '
        'cbBIR_Month
        '
        Me.cbBIR_Month.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbBIR_Month.FormattingEnabled = True
        Me.cbBIR_Month.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12"})
        Me.cbBIR_Month.Location = New System.Drawing.Point(86, 44)
        Me.cbBIR_Month.Name = "cbBIR_Month"
        Me.cbBIR_Month.Size = New System.Drawing.Size(201, 24)
        Me.cbBIR_Month.TabIndex = 1321
        '
        'Label83
        '
        Me.Label83.AutoSize = True
        Me.Label83.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label83.Location = New System.Drawing.Point(340, 47)
        Me.Label83.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label83.Name = "Label83"
        Me.Label83.Size = New System.Drawing.Size(43, 16)
        Me.Label83.TabIndex = 1320
        Me.Label83.Text = "Date :"
        '
        'Label82
        '
        Me.Label82.AutoSize = True
        Me.Label82.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label82.Location = New System.Drawing.Point(36, 47)
        Me.Label82.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label82.Name = "Label82"
        Me.Label82.Size = New System.Drawing.Size(52, 16)
        Me.Label82.TabIndex = 1319
        Me.Label82.Text = "Month :"
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label80.Location = New System.Drawing.Point(35, 82)
        Me.Label80.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(53, 16)
        Me.Label80.TabIndex = 1318
        Me.Label80.Text = "Period :"
        '
        'txtBIR_Reminder
        '
        Me.txtBIR_Reminder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBIR_Reminder.Location = New System.Drawing.Point(86, 16)
        Me.txtBIR_Reminder.Margin = New System.Windows.Forms.Padding(4)
        Me.txtBIR_Reminder.Multiline = True
        Me.txtBIR_Reminder.Name = "txtBIR_Reminder"
        Me.txtBIR_Reminder.Size = New System.Drawing.Size(505, 24)
        Me.txtBIR_Reminder.TabIndex = 3
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label81.Location = New System.Drawing.Point(7, 19)
        Me.Label81.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(81, 16)
        Me.Label81.TabIndex = 1317
        Me.Label81.Text = "Description :"
        '
        'lvlBIRReminders
        '
        Me.lvlBIRReminders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvlBIRReminders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chBIR_ID, Me.chBIR_Description, Me.chBIR_Period, Me.chBIR_Month, Me.chBIR_Date})
        Me.lvlBIRReminders.FullRowSelect = True
        Me.lvlBIRReminders.GridLines = True
        Me.lvlBIRReminders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lvlBIRReminders.HideSelection = False
        Me.lvlBIRReminders.Location = New System.Drawing.Point(6, 187)
        Me.lvlBIRReminders.Margin = New System.Windows.Forms.Padding(4)
        Me.lvlBIRReminders.MultiSelect = False
        Me.lvlBIRReminders.Name = "lvlBIRReminders"
        Me.lvlBIRReminders.Size = New System.Drawing.Size(754, 329)
        Me.lvlBIRReminders.TabIndex = 1350
        Me.lvlBIRReminders.UseCompatibleStateImageBehavior = False
        Me.lvlBIRReminders.View = System.Windows.Forms.View.Details
        '
        'chBIR_ID
        '
        Me.chBIR_ID.Text = "ID"
        Me.chBIR_ID.Width = 0
        '
        'chBIR_Description
        '
        Me.chBIR_Description.Text = "Description"
        Me.chBIR_Description.Width = 300
        '
        'chBIR_Period
        '
        Me.chBIR_Period.Text = "Period"
        Me.chBIR_Period.Width = 150
        '
        'chBIR_Month
        '
        Me.chBIR_Month.Text = "Month"
        Me.chBIR_Month.Width = 150
        '
        'chBIR_Date
        '
        Me.chBIR_Date.Text = "Date"
        Me.chBIR_Date.Width = 150
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.TreeView1.Location = New System.Drawing.Point(12, 7)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "nodeCOA"
        TreeNode1.Text = "Chart of Account"
        TreeNode2.Name = "nodeTrans"
        TreeNode2.Text = "Transaction ID"
        TreeNode3.Name = "nodeVCE"
        TreeNode3.Text = "VCE Setup"
        TreeNode4.Name = "nodeUser"
        TreeNode4.Text = "User Account"
        TreeNode5.Name = "nodeATC"
        TreeNode5.Text = "ATC Table"
        TreeNode6.Name = "nodeBranch"
        TreeNode6.Text = "Branch Setup"
        TreeNode7.Name = "nodeBustype"
        TreeNode7.Text = "Business Type Setup"
        TreeNode8.Name = "nodeGroup"
        TreeNode8.Text = "Maintenance Group"
        TreeNode9.Name = "nodeGeneral"
        TreeNode9.Text = "General"
        TreeNode10.Name = "nodePurchasing"
        TreeNode10.Text = "Purchasing"
        TreeNode11.Name = "nodeSales"
        TreeNode11.Text = "Sales"
        TreeNode12.Name = "nodeCollection"
        TreeNode12.Text = "Collection"
        TreeNode13.Name = "nodeInventory"
        TreeNode13.Text = "Inventory"
        TreeNode14.Name = "nodeProduction"
        TreeNode14.Text = "Production"
        TreeNode15.Name = "nodeCooperative"
        TreeNode15.Text = "Cooperative"
        TreeNode16.Name = "nodeDefault Entries"
        TreeNode16.Text = "Default Entries"
        TreeNode17.Name = "nodeEmailSetup"
        TreeNode17.Text = "Email"
        TreeNode18.Name = "nodeSPS_SLP"
        TreeNode18.Text = "SLS and SLP Maintenance"
        TreeNode19.Name = "chBIRReminders"
        TreeNode19.Text = "BIR Reminders"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode9, TreeNode10, TreeNode11, TreeNode12, TreeNode13, TreeNode14, TreeNode15, TreeNode16, TreeNode17, TreeNode18, TreeNode19})
        Me.TreeView1.ShowLines = False
        Me.TreeView1.Size = New System.Drawing.Size(267, 619)
        Me.TreeView1.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnSave.Location = New System.Drawing.Point(856, 628)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 30)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "Save Changes"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnClose.Location = New System.Drawing.Point(972, 628)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(112, 30)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnUpdateReport
        '
        Me.btnUpdateReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateReport.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnUpdateReport.Location = New System.Drawing.Point(406, 628)
        Me.btnUpdateReport.Name = "btnUpdateReport"
        Me.btnUpdateReport.Size = New System.Drawing.Size(112, 30)
        Me.btnUpdateReport.TabIndex = 7
        Me.btnUpdateReport.Text = "Update Report"
        Me.btnUpdateReport.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!)
        Me.btnUpdate.Location = New System.Drawing.Point(288, 628)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(112, 30)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "Update System"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'txtNI_Code
        '
        Me.txtNI_Code.Location = New System.Drawing.Point(555, 209)
        Me.txtNI_Code.Name = "txtNI_Code"
        Me.txtNI_Code.Size = New System.Drawing.Size(80, 21)
        Me.txtNI_Code.TabIndex = 45
        '
        'txtNI_Title
        '
        Me.txtNI_Title.Location = New System.Drawing.Point(215, 209)
        Me.txtNI_Title.Name = "txtNI_Title"
        Me.txtNI_Title.Size = New System.Drawing.Size(338, 21)
        Me.txtNI_Title.TabIndex = 44
        '
        'Label96
        '
        Me.Label96.AutoSize = True
        Me.Label96.Location = New System.Drawing.Point(127, 212)
        Me.Label96.Name = "Label96"
        Me.Label96.Size = New System.Drawing.Size(76, 15)
        Me.Label96.TabIndex = 43
        Me.Label96.Text = "Net Income :"
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(89, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1086, 665)
        Me.Controls.Add(Me.btnUpdateReport)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.tcSettings)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Settings"
        Me.tcSettings.ResumeLayout(False)
        Me.tpUA.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.nupUAminLen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpCOA.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.tpGeneral.ResumeLayout(False)
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        CType(Me.dgvTransDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTransDetailsAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvTransID, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpVCE.ResumeLayout(False)
        Me.gbVCE.ResumeLayout(False)
        Me.gbVCE.PerformLayout()
        CType(Me.NumericUpDown5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpEntries.ResumeLayout(False)
        Me.PanelEntries.ResumeLayout(False)
        Me.GroupBox28.ResumeLayout(False)
        Me.GroupBox28.PerformLayout()
        Me.GroupBox27.ResumeLayout(False)
        Me.GroupBox27.PerformLayout()
        Me.GroupBox24.ResumeLayout(False)
        Me.GroupBox24.PerformLayout()
        Me.GroupBox23.ResumeLayout(False)
        Me.GroupBox23.PerformLayout()
        Me.GroupBox19.ResumeLayout(False)
        Me.GroupBox19.PerformLayout()
        Me.GroupBox22.ResumeLayout(False)
        Me.GroupBox22.PerformLayout()
        Me.gbAP.ResumeLayout(False)
        Me.gbAP.PerformLayout()
        CType(Me.nupBankPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox14.ResumeLayout(False)
        Me.GroupBox14.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox11.ResumeLayout(False)
        Me.GroupBox11.PerformLayout()
        Me.tpPurchase.ResumeLayout(False)
        Me.gbPO.ResumeLayout(False)
        Me.gbPO.PerformLayout()
        Me.gbPR.ResumeLayout(False)
        Me.gbPR.PerformLayout()
        Me.tpATC.ResumeLayout(False)
        Me.GroupBox10.ResumeLayout(False)
        CType(Me.dgvATC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBranch.ResumeLayout(False)
        Me.GroupBox13.ResumeLayout(False)
        CType(Me.dgvBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpBusiType.ResumeLayout(False)
        Me.GroupBox12.ResumeLayout(False)
        CType(Me.dgvBusType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpMaintGroup.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox25.ResumeLayout(False)
        Me.GroupBox25.PerformLayout()
        Me.gbProdWH.ResumeLayout(False)
        Me.gbProdWH.PerformLayout()
        Me.gbPC.ResumeLayout(False)
        Me.gbPC.PerformLayout()
        Me.gbCC.ResumeLayout(False)
        Me.gbCC.PerformLayout()
        Me.gbInvWH.ResumeLayout(False)
        Me.gbInvWH.PerformLayout()
        Me.tpSales.ResumeLayout(False)
        Me.gbSO.ResumeLayout(False)
        Me.gbSO.PerformLayout()
        Me.tpInventory.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox18.ResumeLayout(False)
        Me.GroupBox18.PerformLayout()
        Me.GroupBox17.ResumeLayout(False)
        Me.GroupBox17.PerformLayout()
        Me.GroupBox16.ResumeLayout(False)
        Me.GroupBox16.PerformLayout()
        Me.GroupBox15.ResumeLayout(False)
        Me.GroupBox15.PerformLayout()
        Me.tpCoop.ResumeLayout(False)
        Me.gbCoop.ResumeLayout(False)
        Me.gbCoop.PerformLayout()
        Me.tpProduction.ResumeLayout(False)
        Me.gbJO.ResumeLayout(False)
        Me.gbJO.PerformLayout()
        Me.tpEmail.ResumeLayout(False)
        Me.tpEmail.PerformLayout()
        Me.tpSLS_SLP.ResumeLayout(False)
        Me.GroupBox21.ResumeLayout(False)
        Me.GroupBox21.PerformLayout()
        Me.GroupBox20.ResumeLayout(False)
        Me.GroupBox20.PerformLayout()
        Me.tpBIRReminders.ResumeLayout(False)
        Me.GroupBox26.ResumeLayout(False)
        Me.GroupBox26.PerformLayout()
        CType(Me.nudBIR_WithIN, System.ComponentModel.ISupportInitialize).EndInit()
        Me.p.ResumeLayout(False)
        Me.p.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents tcSettings As System.Windows.Forms.TabControl
    Friend WithEvents tpUA As System.Windows.Forms.TabPage
    Friend WithEvents tpCOA As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents attemptLock As System.Windows.Forms.CheckBox
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents nupUAminLen As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox13 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents NumericUpDown4 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lvType As System.Windows.Forms.ListView
    Friend WithEvents chType As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDesc As System.Windows.Forms.ColumnHeader
    Friend WithEvents chDigit As System.Windows.Forms.ColumnHeader
    Friend WithEvents chOrder As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCatDown As System.Windows.Forms.Button
    Friend WithEvents btnCatUp As System.Windows.Forms.Button
    Friend WithEvents chkCOAauto As System.Windows.Forms.CheckBox
    Friend WithEvents tpGeneral As System.Windows.Forms.TabPage
    Friend WithEvents cbCOAformat As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtCOAFormat As System.Windows.Forms.TextBox
    Friend WithEvents dgvTransDetail As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents chkGlobal As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransAuto As System.Windows.Forms.CheckBox
    Friend WithEvents tpVCE As System.Windows.Forms.TabPage
    Friend WithEvents gbVCE As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox12 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown5 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tpEntries As System.Windows.Forms.TabPage
    Friend WithEvents dgvTransID As System.Windows.Forms.DataGridView
    Friend WithEvents tpPurchase As System.Windows.Forms.TabPage
    Friend WithEvents tpATC As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox10 As System.Windows.Forms.GroupBox
    Friend WithEvents dgvATC As System.Windows.Forms.DataGridView
    Friend WithEvents tpBranch As System.Windows.Forms.TabPage
    Friend WithEvents dgvBranch As System.Windows.Forms.DataGridView
    Friend WithEvents dgcBranchOldCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBranchCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBranchName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBranchMain As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents tpBusiType As System.Windows.Forms.TabPage
    Friend WithEvents dgvBusType As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox12 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox13 As System.Windows.Forms.GroupBox
    Friend WithEvents dgcBusTypeOld As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBusType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcBusTypeDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tpMaintGroup As System.Windows.Forms.TabPage
    Friend WithEvents gbCC As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtCCG5 As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtCCG4 As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtCCG3 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtCCG2 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCCG1 As System.Windows.Forms.TextBox
    Friend WithEvents gbInvWH As System.Windows.Forms.GroupBox
    Friend WithEvents txtWHG1 As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtWHG5 As System.Windows.Forms.TextBox
    Friend WithEvents txtWHG2 As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtWHG4 As System.Windows.Forms.TextBox
    Friend WithEvents txtWHG3 As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents gbPC As System.Windows.Forms.GroupBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtPCG5 As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtPCG4 As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtPCG3 As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtPCG2 As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtPCG1 As System.Windows.Forms.TextBox
    Friend WithEvents tpSales As System.Windows.Forms.TabPage
    Friend WithEvents gbSO As System.Windows.Forms.GroupBox
    Friend WithEvents chkSOeditPrice As System.Windows.Forms.CheckBox
    Friend WithEvents chkSOreqPO As System.Windows.Forms.CheckBox
    Friend WithEvents gbProdWH As System.Windows.Forms.GroupBox
    Friend WithEvents txtPWHG1 As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents txtPWHG5 As System.Windows.Forms.TextBox
    Friend WithEvents txtPWHG2 As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtPWHG4 As System.Windows.Forms.TextBox
    Friend WithEvents txtPWHG3 As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents dgvTransDetailsAll As System.Windows.Forms.DataGridView
    Friend WithEvents gbPR As System.Windows.Forms.GroupBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents cbPRstock As System.Windows.Forms.ComboBox
    Friend WithEvents gbPO As System.Windows.Forms.GroupBox
    Friend WithEvents tpInventory As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents chkRR_RestrictWHSEItem As System.Windows.Forms.CheckBox
    Friend WithEvents tpCoop As System.Windows.Forms.TabPage
    Friend WithEvents gbCoop As System.Windows.Forms.GroupBox
    Friend WithEvents txtSCCcode As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtSCCtitle As System.Windows.Forms.TextBox
    Friend WithEvents txtTCPcode As System.Windows.Forms.TextBox
    Friend WithEvents txtTCPtitle As System.Windows.Forms.TextBox
    Friend WithEvents txtTCCcode As System.Windows.Forms.TextBox
    Friend WithEvents txtTCCtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents txtDFCScode As System.Windows.Forms.TextBox
    Friend WithEvents txtDFCStitle As System.Windows.Forms.TextBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents txtPUCPcode As System.Windows.Forms.TextBox
    Friend WithEvents txtPUCPtitle As System.Windows.Forms.TextBox
    Friend WithEvents txtPUCCcode As System.Windows.Forms.TextBox
    Friend WithEvents txtPUCCtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents txtSRPcode As System.Windows.Forms.TextBox
    Friend WithEvents txtSRPtitle As System.Windows.Forms.TextBox
    Friend WithEvents txtSRCcode As System.Windows.Forms.TextBox
    Friend WithEvents txtSRCtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtSCPcode As System.Windows.Forms.TextBox
    Friend WithEvents txtSCPtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents tpCollection As System.Windows.Forms.TabPage
    Friend WithEvents CheckBox14 As System.Windows.Forms.CheckBox
    Friend WithEvents chkSOreqDelivDate As System.Windows.Forms.CheckBox
    Friend WithEvents chkSOstaggered As System.Windows.Forms.CheckBox
    Friend WithEvents tpProduction As System.Windows.Forms.TabPage
    Friend WithEvents gbJO As System.Windows.Forms.GroupBox
    Friend WithEvents chkJOperSOitem As System.Windows.Forms.CheckBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents dgcATCCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcATCDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcATCRate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PanelEntries As System.Windows.Forms.Panel
    Friend WithEvents gbAP As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkPOapproval As System.Windows.Forms.CheckBox
    Friend WithEvents txtIVcode As System.Windows.Forms.TextBox
    Friend WithEvents txtATScode As System.Windows.Forms.TextBox
    Friend WithEvents txtPAPcode As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox15 As System.Windows.Forms.CheckBox
    Friend WithEvents txtPAPtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents lbPayables As System.Windows.Forms.ListBox
    Friend WithEvents txtATStitle As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtIVtitle As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnRemovePayables As System.Windows.Forms.Button
    Friend WithEvents btnAddPayables As System.Windows.Forms.Button
    Friend WithEvents GroupBox14 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPEC_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtPEC_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label51 As System.Windows.Forms.Label
    Friend WithEvents txtCWT_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtCWT_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label50 As System.Windows.Forms.Label
    Friend WithEvents txtFWT_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtFWT_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label49 As System.Windows.Forms.Label
    Friend WithEvents txtEWT_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtEWT_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtVP_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtVP_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents txtPT_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtPT_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDOV_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtDOV_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtOV_Code As System.Windows.Forms.TextBox
    Friend WithEvents lbReceivables As System.Windows.Forms.ListBox
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents txtOV_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label47 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveReceivable As System.Windows.Forms.Button
    Friend WithEvents btnAddReceivables As System.Windows.Forms.Button
    Friend WithEvents GroupBox11 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCashAdvance As System.Windows.Forms.ListBox
    Friend WithEvents Label52 As System.Windows.Forms.Label
    Friend WithEvents btnRemoveCA As System.Windows.Forms.Button
    Friend WithEvents btnAddCA As System.Windows.Forms.Button
    Friend WithEvents tpEmail As System.Windows.Forms.TabPage
    Friend WithEvents MetroLabel3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtEmailPass As MetroFramework.Controls.MetroTextBox
    Friend WithEvents MetroLabel4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtEmailAddress As MetroFramework.Controls.MetroTextBox
    Friend WithEvents txtCOH_Code As System.Windows.Forms.TextBox
    Friend WithEvents txtCOH_Title As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox15 As System.Windows.Forms.GroupBox
    Friend WithEvents rbInv_WAUC As System.Windows.Forms.RadioButton
    Friend WithEvents rbInv_SC As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox17 As System.Windows.Forms.GroupBox
    Friend WithEvents rbCSI_JV As System.Windows.Forms.RadioButton
    Friend WithEvents rbCSI_Inventory As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox16 As System.Windows.Forms.GroupBox
    Friend WithEvents rbRR_Purchase As System.Windows.Forms.RadioButton
    Friend WithEvents rbRR_Inventory As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox18 As System.Windows.Forms.GroupBox
    Friend WithEvents rbPOS_POS As System.Windows.Forms.RadioButton
    Friend WithEvents rbPOS_CSI As System.Windows.Forms.RadioButton
    Friend WithEvents chkForApproval As System.Windows.Forms.CheckBox
    Friend WithEvents tpSLS_SLP As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox21 As System.Windows.Forms.GroupBox
    Friend WithEvents lvlSLP As System.Windows.Forms.ListBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents btnRemove_SLP As System.Windows.Forms.Button
    Friend WithEvents btnAdd_SLP As System.Windows.Forms.Button
    Friend WithEvents GroupBox20 As System.Windows.Forms.GroupBox
    Friend WithEvents Label55 As System.Windows.Forms.Label
    Friend WithEvents lvlSLS As System.Windows.Forms.ListBox
    Friend WithEvents btnRemove_SLS As System.Windows.Forms.Button
    Friend WithEvents btnAdd_SLS As System.Windows.Forms.Button
    Friend WithEvents GroupBox22 As System.Windows.Forms.GroupBox
    Friend WithEvents txtGPA_SalesAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtGPA_SalesAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label61 As System.Windows.Forms.Label
    Friend WithEvents txtGPA_SaleReturnAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtGPA_SaleReturnAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label65 As System.Windows.Forms.Label
    Friend WithEvents txtGPA_SaleDiscountAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtGPA_SaleDiscountAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label66 As System.Windows.Forms.Label
    Friend WithEvents txtGPA_COSAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtGPA_COSAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label67 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateReport As System.Windows.Forms.Button
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents tpBIRReminders As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox26 As System.Windows.Forms.GroupBox
    Friend WithEvents p As System.Windows.Forms.GroupBox
    Friend WithEvents txtBIR_Reminder As System.Windows.Forms.TextBox
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents lvlBIRReminders As System.Windows.Forms.ListView
    Friend WithEvents chBIR_ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBIR_Description As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBIR_Period As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBIR_Month As System.Windows.Forms.ColumnHeader
    Friend WithEvents chBIR_Date As System.Windows.Forms.ColumnHeader
    Friend WithEvents cbBIR_Period As System.Windows.Forms.ComboBox
    Friend WithEvents cbBIR_Date As System.Windows.Forms.ComboBox
    Friend WithEvents cbBIR_Month As System.Windows.Forms.ComboBox
    Friend WithEvents Label83 As System.Windows.Forms.Label
    Friend WithEvents Label82 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents btnBIR_Remove As System.Windows.Forms.Button
    Friend WithEvents btnBIR_Add As System.Windows.Forms.Button
    Friend WithEvents btnBIR_New As System.Windows.Forms.Button
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents dgcTransAllType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransAllBranch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransAllBus As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransAllPrefix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransAlldigit As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransAllStart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransBranch As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransBusType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransPrefix As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransDigits As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransStart As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox25 As System.Windows.Forms.GroupBox
    Friend WithEvents txtInv_Group1 As System.Windows.Forms.TextBox
    Friend WithEvents Label63 As System.Windows.Forms.Label
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtInv_Group5 As System.Windows.Forms.TextBox
    Friend WithEvents txtInv_Group2 As System.Windows.Forms.TextBox
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents Label75 As System.Windows.Forms.Label
    Friend WithEvents txtInv_Group4 As System.Windows.Forms.TextBox
    Friend WithEvents txtInv_Group3 As System.Windows.Forms.TextBox
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents GroupBox19 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPOS_VATSalesCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPOS_VATSalesTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label54 As System.Windows.Forms.Label
    Friend WithEvents txtPOS_DiscountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPOS_DiscountTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label56 As System.Windows.Forms.Label
    Friend WithEvents txtPOS_ZeroRatedCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPOS_ZeroRatedTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label58 As System.Windows.Forms.Label
    Friend WithEvents txtPOS_VATExemptCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPOS_VATExemptTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label59 As System.Windows.Forms.Label
    Friend WithEvents txtPOS_VATAmountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtPOS_VATAmountTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label60 As System.Windows.Forms.Label
    Friend WithEvents GroupBox24 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLM_AdvanceRentCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLM_AdvanceRentTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents txtLM_RentalIncomeCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLM_RentalIncomeTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents txtLM_NFCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLM_NFTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents txtLM_DSTCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLM_DSTTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents txtLM_DepositCode As System.Windows.Forms.TextBox
    Friend WithEvents txtLM_DepositTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents GroupBox23 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRE_AccountCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRE_AccountTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label62 As System.Windows.Forms.Label
    Friend WithEvents txtRE_ReserveCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRE_ReserveTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label64 As System.Windows.Forms.Label
    Friend WithEvents txtRE_PenaltyCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRE_PenaltyTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label69 As System.Windows.Forms.Label
    Friend WithEvents txtRE_InterestCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRE_InterestTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents txtRE_EquityCode As System.Windows.Forms.TextBox
    Friend WithEvents txtRE_EquityTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label71 As System.Windows.Forms.Label
    Friend WithEvents GroupBox27 As System.Windows.Forms.GroupBox
    Friend WithEvents txtInv_VarianceAccntCode As System.Windows.Forms.TextBox
    Friend WithEvents txtInv_VarianceAccntTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents GroupBox28 As System.Windows.Forms.GroupBox
    Friend WithEvents txtBM_SRCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBM_SRTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents txtBM_APCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBM_APTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label88 As System.Windows.Forms.Label
    Friend WithEvents txtBM_COSCode As System.Windows.Forms.TextBox
    Friend WithEvents txtBM_COSTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label89 As System.Windows.Forms.Label
    Friend WithEvents chkReversalEntries As System.Windows.Forms.CheckBox
    Friend WithEvents dgcTransType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransDesc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgcTransAuto As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcTransGlobal As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcForApproval As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents dgcForReversal As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Label86 As System.Windows.Forms.Label
    Friend WithEvents nupBankPeriod As System.Windows.Forms.NumericUpDown
    Friend WithEvents nudBIR_WithIN As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtRE_CommissionCode As TextBox
    Friend WithEvents txtRE_CommissionTitle As TextBox
    Friend WithEvents Label87 As Label
    Friend WithEvents txtRE_SalesCode As TextBox
    Friend WithEvents txtRE_SalesTitle As TextBox
    Friend WithEvents Label90 As Label
    Friend WithEvents txtRE_ARCode As TextBox
    Friend WithEvents txtRE_ARTitle As TextBox
    Friend WithEvents Label95 As Label
    Friend WithEvents txtRE_ARMiscFeeCode As TextBox
    Friend WithEvents txtRE_ARMiscFeeTitle As TextBox
    Friend WithEvents Label94 As Label
    Friend WithEvents txtRE_MiscFeeCode As TextBox
    Friend WithEvents txtRE_MiscFeeTitle As TextBox
    Friend WithEvents Label93 As Label
    Friend WithEvents txtRE_OutputVatCode As TextBox
    Friend WithEvents txtRE_OutputVatTitle As TextBox
    Friend WithEvents Label92 As Label
    Friend WithEvents txtRE_NetOfVATCode As TextBox
    Friend WithEvents txtRE_NetOfVATTitle As TextBox
    Friend WithEvents Label91 As Label
    Friend WithEvents txtNI_Code As TextBox
    Friend WithEvents txtNI_Title As TextBox
    Friend WithEvents Label96 As Label
End Class
