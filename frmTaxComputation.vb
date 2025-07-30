Public Class frmTaxComputation
    Public Amount, VATAmount, EWTAmount, GrossAmount As Decimal
    Public TAX_IV, TAX_EWT, TAX_VatPayable, TAX_CWT, AR_OutputVAT As String
    Public VATType As String = ""
    Public moduleType As String = ""
    Public ATCType As String = ""
    Public ATCCode As String = ""
    Public VCECode As String = ""
    Public Classification As String = ""
    Dim disableEvent As Boolean = False
    Public VAT, VATInc As Boolean

    Public Overloads Function ShowDialog(Total_Amount As Decimal, Optional ByVal Type As String = "", Optional ByVal ATC_Type As String = "", Optional ByVal VCE_Code As String = "", Optional ByVal VAT As Boolean = False, Optional ByVal VAT_Inc As Boolean = False) As Boolean
        Amount = Total_Amount
        disableEvent = True
        chkVAT.Checked = VAT
        chkVATInc.Checked = VAT_Inc
        disableEvent = False
        moduleType = Type
        ATCType = ATC_Type
        VCECode = VCE_Code
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmTaxComputation_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        If moduleType = "BS" Then
            chkVATInc.Visible = True
            Me.Size = New Size(511, 335)
        Else
            chkVATInc.Visible = False
            Me.Size = New Size(425, 335)
        End If
        txtGrossAmount.Text = Amount
        LoadCompany()
        LoadSetup()
        VATDescription()
        EWTDescription()
    End Sub

    Private Sub LoadCompany()
        Dim query As String
        query = " SELECT  Classification FROM tblCompany "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            Classification = SQL.SQLDR("Classification").ToString
        End If
    End Sub

    Private Sub LoadSetup()
        Dim query As String
        query = " SELECT  TAX_IV, TAX_EWT, TAX_VatPayable, TAX_CWT, TAX_OV  FROM tblSystemSetup "
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            TAX_IV = SQL.SQLDR("TAX_IV").ToString
            TAX_EWT = SQL.SQLDR("TAX_EWT").ToString
            TAX_VatPayable = SQL.SQLDR("TAX_VatPayable").ToString
            TAX_CWT = SQL.SQLDR("TAX_CWT").ToString
            AR_OutputVAT = SQL.SQLDR("TAX_OV").ToString
        End If
    End Sub

    Private Sub VATDescription()
        Dim query As String
        If moduleType = "PJ" Then
            cbVATCode.Items.Clear()
            cbVATCode.Items.Add("-SELECT VAT RATE-")
            cbVATCode.Items.Add("Exempt")
            cbVATCode.Items.Add("Zero-rated")
            cbVATCode.Items.Add("Services")
            cbVATCode.Items.Add("Capital Goods")
            cbVATCode.Items.Add("Other Than Capital Goods")
            cbVATCode.SelectedIndex = 0
        Else
            query = " SELECT TaxDescription FROM tblTaxMaintenance WHERE Status = 'Active' AND TaxType = 'VAT' "
            SQL.ReadQuery(query)
            cbVATCode.Items.Clear()
            cbVATCode.Items.Add("-SELECT VAT RATE-")
            While SQL.SQLDR.Read
                cbVATCode.Items.Add(SQL.SQLDR("TaxDescription").ToString)
            End While

            If moduleType = "BS" AndAlso chkVAT.Checked = True Then
                cbVATCode.SelectedItem = "VAT (12%)"
            Else
                cbVATCode.SelectedIndex = 0
            End If
        End If

    End Sub


    Private Sub ComputeVaTAmount()
        Dim gross, baseVAT, VATRate As Decimal
        'If VATAmount <> 0 Then
        '    txtVATAmount.Text = VATAmount
        'Else
        If cbVATCode.SelectedIndex <> 0 Then
            VATRate = CDec(txtVATRate.Text / 100)
            gross = CDec(IIf(txtGrossAmount.Text = "", "0.00", txtGrossAmount.Text)).ToString("N2")
            If moduleType = "BS" Then
                If chkVATInc.Checked = True Then
                    baseVAT = CDec(gross / (1 + VATRate)).ToString("N2")
                    txtVATAmount.Text = CDec(baseVAT * VATRate).ToString("N2")
                Else
                    baseVAT = CDec(gross).ToString("N2")
                    txtVATAmount.Text = CDec(gross * VATRate).ToString("N2")
                End If
            Else
                baseVAT = CDec(gross / (1 + VATRate)).ToString("N2")
                txtVATAmount.Text = CDec(baseVAT * VATRate).ToString("N2")
            End If
            txtNetAmount.Text = CDec(baseVAT).ToString("N2")
        Else
            txtVATAmount.Text = "0.00"
            txtNetAmount.Text = "0.00"
        End If
    End Sub

    Private Sub ComputeEWTAmount()
        Dim net, baseEWT, EWTRate As Decimal
        If cbEWTCode.SelectedIndex <> 0 Then
            If txtVATRate.Text = "0.00" Then
                Dim grossAmount, baseVAT, VATRate As Decimal
                VATRate = 0
                grossAmount = CDec(IIf(txtGrossAmount.Text = "", "0.00", txtGrossAmount.Text)).ToString("N2")
                'baseVAT = CDec(grossAmount / (1 + VATRate)).ToString("N2")
                'txtNetAmount.Text = CDec(baseVAT).ToString("N2")
                If moduleType = "BS" Then
                    If chkVATInc.Checked = True Then
                        baseVAT = CDec(grossAmount / (1 + VATRate)).ToString("N2")
                        txtVATAmount.Text = CDec(baseVAT * VATRate).ToString("N2")
                    Else
                        baseVAT = CDec(grossAmount).ToString("N2")
                        txtVATAmount.Text = CDec(grossAmount * VATRate).ToString("N2")
                    End If
                Else
                    baseVAT = CDec(grossAmount / (1 + VATRate)).ToString("N2")
                    txtVATAmount.Text = CDec(baseVAT * VATRate).ToString("N2")
                End If


                EWTRate = CDec(txtEWTRate.Text / 100)
                net = CDec(IIf(txtNetAmount.Text = "", "0.00", txtNetAmount.Text)).ToString("N2")
                baseEWT = CDec(net * EWTRate).ToString("N2")
                txtEWTAmount.Text = CDec(baseEWT).ToString("N2")
            Else
                EWTRate = CDec(txtEWTRate.Text / 100)
                net = CDec(IIf(txtNetAmount.Text = "", "0.00", txtNetAmount.Text)).ToString("N2")
                baseEWT = CDec(net * EWTRate).ToString("N2")
                txtEWTAmount.Text = CDec(baseEWT).ToString("N2")
            End If
            Else
                txtEWTAmount.Text = "0.00"
            End If
    End Sub

    Private Sub EWTDescription()
        Dim query As String
        If Classification = "Non-Individual" Then
            query = " SELECT Code + ' | ' + Description AS ATCCode FROM tblATC"
            SQL.ReadQuery(query)
            cbEWTCode.Items.Clear()
            cbEWTCode.Items.Add("-SELECT EWT RATE-")
            While SQL.SQLDR.Read
                cbEWTCode.Items.Add(SQL.SQLDR("ATCCode").ToString)
            End While
            cbEWTCode.SelectedIndex = 0
        Else
            query = " SELECT Code + ' | ' + Description AS ATCCode FROM tblATC WHERE Classification = 'IND'"
            SQL.ReadQuery(query)
            cbEWTCode.Items.Clear()
            cbEWTCode.Items.Add("-SELECT EWT RATE-")
            While SQL.SQLDR.Read
                cbEWTCode.Items.Add(SQL.SQLDR("ATCCode").ToString)
            End While
            cbEWTCode.SelectedIndex = 0
        End If
        
    End Sub

    Private Sub cbVATCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbVATCode.SelectedIndexChanged
        Dim query As String
        If cbVATCode.SelectedItem <> "-SELECT VAT RATE-" Then
            If moduleType = "PJ" Then
                If cbVATCode.SelectedItem = "Exempt" Or cbVATCode.SelectedItem = "Zero-rated" Then
                    VATType = cbVATCode.SelectedItem
                    txtVATRate.Text = "0.00"
                Else
                    VATType = cbVATCode.SelectedItem
                    txtVATRate.Text = "12.00"
                End If
            Else
                query = " SELECT TaxRate, AccntCode, TaxDescription FROM tblTaxMaintenance WHERE TaxDescription = '" & cbVATCode.SelectedItem & "' "
                SQL.ReadQuery(query)
                If SQL.SQLDR.Read Then
                    VATType = SQL.SQLDR("TaxDescription").ToString
                    txtVATRate.Text = SQL.SQLDR("TaxRate").ToString
                End If

                If moduleType = "BS" Then
                    If VATType = "VAT (12%)" Then
                        chkVATInc.Enabled = True
                        chkVAT.Checked = True
                    Else
                        chkVATInc.Enabled = False
                        chkVATInc.Checked = False
                        chkVAT.Checked = False
                    End If
                End If
            End If
        Else
            txtVATRate.Text = "0.00"
        End If
        ComputeVaTAmount()
    End Sub

    Private Sub cbEWTCode_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbEWTCode.SelectedIndexChanged
        Dim query As String
        If cbVATCode.SelectedItem <> "-SELECT EWT RATE-" Then
            query = " SELECT  Rate, Code FROM tblATC WHERE Code + ' | ' + Description = '" & cbEWTCode.SelectedItem & "' "
            SQL.ReadQuery(query)
            If SQL.SQLDR.Read Then
                txtEWTRate.Text = SQL.SQLDR("Rate").ToString
                ATCCode = SQL.SQLDR("Code").ToString
            End If
        Else
            txtEWTRate.Text = "0.00%"
        End If
        ComputeEWTAmount()
    End Sub

    Private Sub btnCompute_Click(sender As System.Object, e As System.EventArgs) Handles btnCompute.Click
        VATAmount = txtVATAmount.Text
        EWTAmount = txtEWTAmount.Text
        GrossAmount = txtNetAmount.Text
        VAT = chkVAT.Checked
        VATInc = chkVATInc.Checked
        Me.Close()
    End Sub

    Private Sub chkVATInc_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkVATInc.CheckedChanged
        If disableEvent = False Then
            ComputeVaTAmount()
            ComputeEWTAmount()
        End If
    End Sub
End Class