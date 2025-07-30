Imports Excel = Microsoft.Office.Interop.Excel
Public Class FrmSAWT
    Public ReportType As String
    Public DateFrom As Date
    Public DateTo As Date
    Public CostCenter As String

    'Test
    Private Sub FrmSAWT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case ReportType
            Case "MAP"
                lblTitle.Text = "Monthly Alphalist of Payees (MAP)"
                LoadMAP()
            Case "QAP"
                lblTitle.Text = "Quarterly Alphalist of Payees (QAP)"
                LoadMAP()
            Case "SAWT"
                lblTitle.Text = "Summary Alphalist of Withholding Taxes (SAWT)"
                LoadSAWT()
            Case "SLS"
                lblTitle.Text = "Summary List of Sales (SLS)"
                LoadSLS()
            Case "SLP"
                lblTitle.Text = "Summary List of Puchases (SLP)"
                LoadSLP()
        End Select
    End Sub

    Private Sub LoadMAP()
        dgvData.DataSource = Nothing
        Dim query As String
        query = " SELECT  AppDate AS Reporting_Month, LEFT(REPLACE(TIN_No, '-',''), 9)  AS Vendor_TIN, RIGHT(REPLACE(TIN_No, '-',''), 3) AS branchCode, VCEName AS companyName,  " &
                " 		   Last_Name AS surName, First_Name AS firstName, Middle_Name AS middleName, '' AS ATC, EWT / (2.0/100.0) AS income_payment,  " &
                " 		   2 AS ewt_rate, EWT AS tax_amount " &
                " FROM tblVCE_Master INNER JOIN " &
                " ( " &
                " 	SELECT   AppDate, VCECode,  RefType, RefTransID,  SUM(Credit-Debit) AS EWT " &
                " 	FROM     view_GL WHERE AccntCode = (SELECT TAX_EWT FROM tblSystemSetup) " &
                "   AND      AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "'  AND Status <> 'Cancelled'" &
                " 	GROUP BY AppDate, VCECode,  RefType, RefTransID " &
                " ) AS EWT " &
                " ON tblVCE_Master.VCECode = EWT.VCECode WHERE tblVCE_Master.VCECode <> 'V00004'"
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)

            dgvData.Columns(8).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(10).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString

            dgvData.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Else
            dgvData.DataSource = Nothing
        End If

    End Sub

    Public Sub LoadSAWT()
        dgvData.DataSource = Nothing
        Dim query As String
        query = " SELECT  AppDate AS Reporting_Month, LEFT(REPLACE(TIN_No, '-',''), 9)  AS Vendor_TIN, RIGHT(REPLACE(TIN_No, '-',''), 3) AS branchCode, VCEName AS companyName,  " & _
                  " 		   Last_Name AS surName, First_Name AS firstName, Middle_Name AS middleName, '' AS ATC, CWT / (2.0/100.0) AS income_payment,  " & _
                  " 		   2 AS ewt_rate, CWT AS tax_amount " & _
                  " FROM tblVCE_Master INNER JOIN " & _
                  " ( " & _
                  " 	SELECT   AppDate, VCECode,  RefType, RefTransID,  SUM(Debit-Credit) AS CWT " & _
                  " 	FROM     view_GL WHERE AccntCode = (SELECT TAX_CWT FROM tblSystemSetup) " & _
                  "   AND      AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "'  AND Status <> 'Cancelled'" & _
                  " 	GROUP BY AppDate, VCECode,  RefType, RefTransID " & _
                  " ) AS EWT " & _
                  " ON tblVCE_Master.VCECode = EWT.VCECode "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)


            dgvData.Columns(8).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(10).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString

            dgvData.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Else
            dgvData.DataSource = Nothing
        End If
    End Sub

    'Public Sub LoadSLS()
    '    dgvData.DataSource = Nothing
    '    Dim query As String
    '    query = " SELECT TIN_No AS client_TIN, VCEName as companyName, Last_Name AS lastName, " &
    '            " 		First_Name as firstName, Middle_Name as middleName, address1 , address2, " &
    '            " 		 exempt, zeroRated, taxableNetofVat, CASE WHEN taxableNetofVat = 0 THEN '0.00' ELSE '12.00' END as vatRate, " &
    '            " 		 outputVat,  totalSales, grossTaxable " &
    '            " FROM " &
    '            " 				( " &
    '            " 					SELECT " &
    '            " 						RefTransID, TIN_No, VCECode, VCEName, Last_Name, First_Name, Middle_Name, address1, address2, " &
    '            " 						ISNULL(SUM(Exempt),0) AS exempt, ISNULL(SUM([VAT (12%)]),0) AS taxableNetofVat, " &
    '            " 						ISNULL(SUM([Zero-rated]),0) AS zeroRated, ISNULL(SUM([Output VAT]),0) AS outputVat, " &
    '            " 						ISNULL(SUM(Exempt),0) + ISNULL(SUM([VAT (12%)]),0) + ISNULL(SUM([Zero-rated]),0) as totalSales, " &
    '            " 						ISNULL(SUM([VAT (12%)]),0) + ISNULL(SUM([Output VAT]),0) as grossTaxable " &
    '            " 					FROM    " &
    '            " 				( " &
    '            " 			SELECT  " &
    '            " 				RefTransID, TIN_No, tblJE_Details.VCECode, VCEName, " &
    '            " 				ISNULL(Last_Name,'') AS Last_Name, ISNULL(First_Name,'') AS First_Name, " &
    '            " 				ISNULL(Middle_Name,'') AS Middle_Name, '' as address1,'' as address2, " &
    '            " 				CASE WHEN Debit <> 0 THEN Debit ELSE Credit END AS Amount, VATType " &
    '            " 			FROM  dbo.tblJE_Header  " &
    '            " 				INNER JOIN dbo.tblJE_Details ON  " &
    '            " 				dbo.tblJE_Header.JE_No = dbo.tblJE_Details.JE_No " &
    '            " 				LEFT JOIN tblVCE_Master ON " &
    '            " 				tblVCE_Master.VCECode = tblJE_Details.VCECode " &
    '            " 		WHERE RefType IN (SELECT RefType FROM tblTax_RefType WHERE Report = 'SLS') " & vbCrLf &
    '            "   AND      AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "'  " &
    '            "               AND tblJE_Details.CostCenter = '" & CostCenter & "' " &
    '            " 				) t  " &
    '            " 		PIVOT( " &
    '            " 		SUM(Amount)  " &
    '            " 		FOR VATType IN ( " &
    '            " 			[Exempt],  " &
    '            " 			[VAT (12%)],  " &
    '            " 			[Zero-rated], " &
    '            " 			[Output VAT]) " &
    '            " 		) AS pt " &
    '            " 			GROUP BY  RefTransID, TIN_No, VCECode, VCEName, Last_Name,First_Name, Middle_Name, address1, address2  " &
    '            " 					) as SLS "
    '    SQL.GetQuery(query)
    '    If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
    '        dgvData.DataSource = SQL.SQLDS.Tables(0)
    '    Else
    '        dgvData.DataSource = Nothing
    '    End If
    'End Sub

    Public Sub LoadSLS()
        dgvData.DataSource = Nothing
        Dim query As String
        Dim costCenterFilter As String = "" ' Initialize an empty string for the filter

        ' Check the value of CostCenter
        If CostCenter <> "All" Then
            costCenterFilter = " AND tblJE_Details.CostCenter = '" & CostCenter & "' "
        End If

        query = " SELECT TIN_No AS client_TIN, VCEName as companyName, Last_Name AS lastName, " &
            " 		First_Name as firstName, Middle_Name as middleName, address1 , address2, " &
            " 		 exempt, zeroRated, taxableNetofVat, CASE WHEN taxableNetofVat = 0 THEN '0.00' ELSE '12.00' END as vatRate, " &
            " 		 outputVat,  totalSales, grossTaxable " &
            " FROM " &
            " 				( " &
            " 					SELECT " &
            " 						RefTransID, TIN_No, VCECode, VCEName, Last_Name, First_Name, Middle_Name, address1, address2, " &
            " 						ISNULL(SUM(Exempt),0) AS exempt, ISNULL(SUM([VAT (12%)]),0) AS taxableNetofVat, " &
            " 						ISNULL(SUM([Zero-rated]),0) AS zeroRated, ISNULL(SUM([Output VAT]),0) AS outputVat, " &
            " 						ISNULL(SUM(Exempt),0) + ISNULL(SUM([VAT (12%)]),0) + ISNULL(SUM([Zero-rated]),0) as totalSales, " &
            " 						ISNULL(SUM([VAT (12%)]),0) + ISNULL(SUM([Output VAT]),0) as grossTaxable " &
            " 					FROM   " &
            " 				( " &
            " 			SELECT  " &
            " 				RefTransID, TIN_No, tblJE_Details.VCECode, VCEName, " &
            " 				ISNULL(Last_Name,'') AS Last_Name, ISNULL(First_Name,'') AS First_Name, " &
            " 				ISNULL(Middle_Name,'') AS Middle_Name, '' as address1,'' as address2, " &
            " 				CASE WHEN Debit <> 0 THEN Debit ELSE Credit END AS Amount, VATType " &
            " 			FROM  dbo.tblJE_Header  " &
            " 				INNER JOIN dbo.tblJE_Details ON  " &
            " 				dbo.tblJE_Header.JE_No = dbo.tblJE_Details.JE_No " &
            " 				LEFT JOIN tblVCE_Master ON " &
            " 				tblVCE_Master.VCECode = tblJE_Details.VCECode " &
            " 		WHERE RefType IN (SELECT RefType FROM tblTax_RefType WHERE Report = 'SLS') " &
            "    AND   AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "'" &
            costCenterFilter & " " &
            " 				) t " &
            " 		PIVOT( " &
            " 		SUM(Amount)  " &
            " 		FOR VATType IN ( " &
            " 			[Exempt],  " &
            " 			[VAT (12%)],  " &
            " 			[Zero-rated], " &
            " 			[Output VAT]) " &
            " 		) AS pt " &
            " 			GROUP BY  RefTransID, TIN_No, VCECode, VCEName, Last_Name,First_Name, Middle_Name, address1, address2 " &
            " 					) as SLS "

        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)

            dgvData.Columns(7).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(8).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(9).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(10).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(11).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(12).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(13).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString


            dgvData.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        Else
            dgvData.DataSource = Nothing
        End If
    End Sub

    Public Sub LoadSLP()
        dgvData.DataSource = Nothing
        Dim query As String
        query = " SELECT TIN_No AS client_TIN, VCEName as companyName, Last_Name AS lastName,  " &
                " 	First_Name as firstName, Middle_Name as middleName, address1 , address2,  " &
                " 	exempt, zeroRated, services, capitalGoods, otherThancapitalGoods, taxableNetofVat,  " &
                " 	CASE WHEN taxableNetofVat = 0 THEN '0.00' ELSE '12.00' END as vatRate,  " &
                " 	inputVat,  totalPurchases  " &
                " FROM " &
                " 				( " &
                " 	SELECT " &
                " 		RefTransID, TIN_No, VCECode, VCEName, Last_Name, First_Name, Middle_Name, " &
                " 		address1, address2, ISNULL(SUM(Exempt),0) AS exempt, ISNULL(SUM([Zero-rated]),0) AS zeroRated, " &
                " 		ISNULL(SUM([Services]),0) AS services, ISNULL(SUM([Capital Goods]),0) AS capitalGoods, " &
                " 		ISNULL(SUM([Other Than Capital Goods]),0) AS otherThancapitalGoods, ISNULL(SUM([Services]),0) + ISNULL(SUM([Capital Goods]),0) + ISNULL(SUM([Other Than Capital Goods]),0) as taxableNetofVat, " &
                " 		ISNULL(SUM([Input VAT]),0)  as inputVat, ISNULL(SUM(Exempt),0) + ISNULL(SUM([Zero-rated]),0) + ISNULL(SUM([Services]),0) + ISNULL(SUM([Capital Goods]),0) + ISNULL(SUM([Other Than Capital Goods]),0) as totalPurchases " &
                " 	FROM    " &
                " 		( " &
                " 		SELECT  " &
                " 			RefTransID, TIN_No, tblJE_Details.VCECode, " &
                " 			VCEName, ISNULL(Last_Name,'') AS Last_Name, ISNULL(First_Name,'') AS First_Name, " &
                " 			ISNULL(Middle_Name,'') AS Middle_Name, '' as address1, '' as address2, " &
                " 			CASE WHEN Debit <> 0 THEN Debit ELSE Credit END AS Amount, VATType " &
                " 		FROM     " &
                " 			dbo.tblJE_Header INNER JOIN " &
                " 			dbo.tblJE_Details ON dbo.tblJE_Header.JE_No = dbo.tblJE_Details.JE_No " &
                " 			LEFT JOIN " &
                " 			tblVCE_Master ON " &
                " 			tblVCE_Master.VCECode = tblJE_Details.VCECode " &
                " 		WHERE RefType  IN (SELECT RefType FROM tblTax_RefType WHERE Report = 'SLP') " & vbCrLf &
                "               AND      AppDate BETWEEN '" & DateFrom & "' AND '" & DateTo & "'  " &
                " 		) t  " &
                " 			PIVOT( " &
                " 			SUM(Amount)  " &
                " 			FOR VATType IN ( " &
                " 			[Exempt],  " &
                " 			[Zero-rated],  " &
                " 			[Services], " &
                " 			[Capital Goods], " &
                " 			[Other Than Capital Goods], " &
                " 			[Input VAT]) " &
                " 			) AS pt " &
                " GROUP BY  RefTransID, TIN_No, VCECode, VCEName,Last_Name, First_Name, " &
                " 			Middle_Name, address1, address2  " &
                " 				) as SLP "
        SQL.GetQuery(query)
        If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
            dgvData.DataSource = SQL.SQLDS.Tables(0)

            dgvData.Columns(7).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(8).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(9).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(10).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(11).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(12).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(13).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(14).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString
            dgvData.Columns(15).DefaultCellStyle.Format = Format("#,###,###,###.00").ToString

            dgvData.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(8).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(10).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(11).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(12).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(13).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(14).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            dgvData.Columns(15).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        Else
            dgvData.DataSource = Nothing
        End If
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            If ((dgvData.Columns.Count = 0) Or (dgvData.Rows.Count = 0)) Then
                MsgBox("No Records to Export!", MsgBoxStyle.Exclamation, "Message Alert")
            Else
                If FolderBrowserDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Export(FolderBrowserDialog1.SelectedPath)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Export(ByVal Path As String)
        Dim DSet As New DataSet
        DSet.Tables.Add()
        For i As Integer = 0 To dgvData.ColumnCount - 1
            DSet.Tables(0).Columns.Add(dgvData.Columns(i).HeaderText)
        Next
        'add rows to the table
        Dim row As DataRow
        For i As Integer = 0 To dgvData.RowCount - 1
            row = DSet.Tables(0).NewRow
            For j As Integer = 0 To dgvData.Columns.Count - 1
                row(j) = dgvData.Rows(i).Cells(j).Value
            Next
            DSet.Tables(0).Rows.Add(row)
        Next

        Dim excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

        wBook = excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()

        Dim dt As System.Data.DataTable = DSet.Tables(0)
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            excel.Cells(1, colIndex) = dc.ColumnName
        Next

        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next
        wSheet.Columns.AutoFit()
        Dim strFileName As String
        'strFileName = FolderBrowserDialog1.SelectedPath & "\" & "SAWT" & Date.Today.Month.ToString("00") & Date.Today.Day.ToString("00") & Date.Today.Year.ToString("00") & Now.Hour.ToString("00") & Now.Minute.ToString("00") & ".xls"
        strFileName = FolderBrowserDialog1.SelectedPath & "\" & lblTitle.Text & "_" & Date.Today.Month.ToString("00") & Date.Today.Day.ToString("00") & Date.Today.Year.ToString("00") & Now.Hour.ToString("00") & Now.Minute.ToString("00") & ".xls"

        Dim blnFileOpen As Boolean = False

        If System.IO.File.Exists(strFileName) Then
            System.IO.File.Delete(strFileName)
        End If

        wBook.SaveAs(strFileName)
        excel.Workbooks.Open(strFileName)
        excel.Visible = True
        MsgBox("Exported Succesfully", MsgBoxStyle.Information, "Message Alert")
    End Sub

    Private Sub NAR(ByRef o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
End Class