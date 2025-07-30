Public Class frmLoadTransactions
    Dim moduleID As String
    Public transID As String = ""
    Public BranchCode As String = ""
    Public itemCode As String = ""
    Public Re_schednum As String = ""
    Public p1 As String = ""
    Public batch As Boolean = False
    Dim disableEvent As Boolean = False

    Public Overloads Function ShowDialog(ByVal ModID As String, Optional ByVal Param1 As String = "") As Boolean
        p1 = Param1
        moduleID = ModID
        MyBase.ShowDialog()
        Return True
    End Function

    Private Sub frmLoadTransactions_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LoadData()
        LoadBranches()
        If moduleID = "Copy Member" Then
            cbFilter.Items.Clear()
            cbFilter.Items.Add("Member ID")
            cbFilter.Items.Add("Full Name")
            cbFilter.Items.Add("Status")
            Label2.Visible = True
            cbBranch.Visible = True
        ElseIf moduleID = "APV" OrElse moduleID = "CV" Then
            cbProject.Visible = True
            Label3.Visible = True
        Else
            cbProject.Visible = False
            Label3.Visible = False
        End If
        LoadCC()
    End Sub

    Private Sub LoadBranches()
        Dim query As String
        query = " SELECT    DISTINCT  tblBranch.BranchCode + ' - ' + Description AS BranchCode  " & _
                " FROM      tblBranch    "
        SQL.ReadQuery(query)
        cbBranch.Items.Clear()
        While SQL.SQLDR.Read
            cbBranch.Items.Add(SQL.SQLDR("BranchCode").ToString)
        End While
        cbBranch.SelectedIndex = 0
    End Sub

    Private Sub LoadData()
        Try
            Dim filter As String = ""
            Dim query As String = ""

            Select Case moduleID
                Case "JO_GI"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE JO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblJO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        tblJO.TransID, JO_No AS [JO No.], DateJO AS [JO Date],  VCEName AS Supplier,ItemName, tblJO.QTY, DateNeeded AS [Date Needed], tblJO.Remarks, tblJO.Status  " & _
                            " FROM          tblBOM INNER JOIN tblJO " & _
                            " ON            tblBOM.JO_Ref = tblJO.TransID " & _
                            " LEFT JOIN     tblVCE_Master " & _
                            " ON            tblJO.VCECode = tblVCE_Master.VCECode " & _
                            " LEFT JOIN tblItem_Master ON" & _
                            " tblJO.ItemCode = tblItem_Master.ItemCode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "User_Level"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE '' = '' ORDER BY Description"
                            Case "VCE Name"
                                filter = " WHERE '' = '' ORDER BY Description"
                            Case "Remarks"
                                filter = " WHERE '' = '' ORDER BY Description"
                            Case "Status"
                                filter = " WHERE '' = '' ORDER BY Description"
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT       UserLevel, Description FROM tblUser_Level " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "DR-SI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND DR_No LIKE '%' + @Filter + '%' AND ForECS = 0"
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' AND ForECS = 0"
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' AND ForECS = 0"
                            Case "Status"
                                filter = " AND tblDR.Status LIKE '%' + @Filter + '%' AND ForECS = 0"
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, DR_No AS [DR No.], DateDR AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], tblDR.Status  " & _
                            " FROM     tblDR LEFT JOIN tblVCE_Master " & _
                            " ON	   tblDR .VCECode = tblVCE_Master.VCECode " & _
                            " WHERE    TransID NOT IN (SELECT DR_Ref FROM tblSI WHERE Status <> 'Cancelled') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "DR-ECS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = "WHERE tblDR.ForECS = 1 AND DR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = "WHERE tblDR.ForECS = 1 AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = "WHERE tblDR.ForECS = 1 AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = "WHERE tblDR.ForECS = 1 AND viewDR_Status.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblDR.TransID, tblDR.DR_No AS [DR No.], DateDR AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], viewDR_Status.Status  " & _
                            " FROM     tblDR LEFT JOIN tblVCE_Master " & _
                            " ON	   tblDR.VCECode = tblVCE_Master.VCECode " & _
                            " LEFT JOIN	 viewDR_Status " & _
                            " ON		 tblDR.TransID = viewDR_Status.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "DR-LN"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = "WHERE  DR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = "WHERE  VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = "WHERE   Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = "WHERE viewDR_LN_Status.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT   tblDR.TransID, tblDR.DR_No AS [DR No.], DateDR AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], Unserved, viewDR_LN_Status.Status   " & _
                            "  FROM     tblDR LEFT JOIN tblVCE_Master  " & _
                            "  ON	   tblDR.VCECode = tblVCE_Master.VCECode  " & _
                            "  LEFT JOIN	 viewDR_LN_Status  " & _
                            "  ON		 tblDR.TransID = viewDR_LN_Status.TransID " & _
                            "  LEFT JOIN viewDR_LN_UNserved " & _
                            "  ON viewDR_LN_UNserved.TransID = tblDR.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "SO-PL"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblSO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT     DISTINCT  CAST(tblSO.TransID AS nvarchar)  AS RecordID, CAST(tblSO.SO_No AS nvarchar)  AS [SO No.],  " & _
                            "            DateSO AS [Date], VCEName AS [Customer], Remarks, ReferenceNo, tblSO.Status  " & _
                            " FROM       tblSO LEFT JOIN tblVCE_Master " & _
                            " ON	     tblSO.VCECode = tblVCE_Master.VCECode " & _
                            " INNER JOIN viewSO_Unallocated " & _
                            " ON         tblSO.TransID = viewSO_Unallocated.TransID " & _
                            " WHERE      tblSO.Status <> 'Modified' " & filter & _
                            " ORDER BY   [SO No.]"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PL-DR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND PL_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblPL.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT     DISTINCT tblPL.TransID, tblPL.PL_No AS [PL No.], DatePL AS [PL Date], VCEName AS Supplier, Remarks, tblPL.Status  " & _
                            " FROM       tblPL INNER JOIN tblPL_Details " & _
                            " ON	     tblPL.TransID = tblPL_Details.TransID " & _
                            " INNER JOIN viewPL_Unserved " & _
                            " ON        tblPL_Details.ItemCode = viewPL_Unserved.ItemCode " & _
                            " AND        tblPL_Details.LineNum = viewPL_Unserved.LineNum " & _
                            " AND        tblPL_Details.WHSE = viewPL_Unserved.WHSE " & _
                            " LEFT JOIN  tblVCE_Master " & _
                            " ON	     tblPL.VCECode = tblVCE_Master.VCECode " & _
                            " WHERE      tblPL.Status <> 'Modified' " & _
                            " AND        viewPL_Unserved.Unserved > 0 " & _
                            " AND        tblPL_Details.WHSE IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                            "                             FROM      tblWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "'" & _
                            " 			UNION ALL " & _
                            " 				SELECT    DISTINCT tblProdWarehouse.Code   " & _
                            "                 FROM      tblProdWarehouse INNER JOIN tblUser_Access  " & _
                            "                 ON        tblProdWarehouse.Code = tblUser_Access.Code  " & _
                            "                 AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'  " & _
                            "                 AND       tblUser_Access.Type = 'Production' AND isAllowed = 1  " & _
                            "                             WHERE     UserID ='" & UserID & "')" & filter & _
                            " ORDER BY [PL No.]"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)



                Case "BOMC"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND BOMC_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblBOMC.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT     tblBOMC.TransID,BOMC_No AS [BOMC No.],  tblBOMC.WHSE,  " & _
                            "  View_Warehouse_Production.Description,  DateBOMC AS [BOMC Date],  " & _
                            "   Remarks, tblBOMC.Status    " & _
                            "  FROM       tblBOMC  " & _
                            "  LEFT JOIN  View_Warehouse_Production  ON	      " & _
                            "  tblBOMC.WHSE = View_Warehouse_Production.Code   " & _
                            "  WHERE       " & _
                            "  tblBOMC.WHSE IN  " & _
                            "  (SELECT    DISTINCT tblProdWarehouse.Code                                " & _
                            "  FROM      tblProdWarehouse  " & _
                            "  INNER JOIN tblUser_Access  ON         " & _
                            "  tblProdWarehouse.Code = tblUser_Access.Code   AND        " & _
                            "  tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'   " & _
                            "   AND       tblUser_Access.Type = 'Production' AND isAllowed = 1    WHERE UserID ='" & UserID & "'" & _
                            " UNION ALL " & _
                            " SELECT    DISTINCT tblWarehouse.Code " & _
                            "  FROM      tblWarehouse  " & _
                            "  INNER JOIN tblUser_Access  ON         " & _
                            "  tblWarehouse.Code = tblUser_Access.Code   AND        " & _
                            "  tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                            "   AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1    WHERE UserID ='" & UserID & "') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "GI-GR-PWHSE"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND GI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblGI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID, GI_No AS [GI No.], DateGI AS [GI Date],  Description AS [Issued From], Remarks, tblGI.Status  " & _
                            " FROM          tblGI LEFT JOIN tblWarehouse " & _
                            " ON	        tblGI.WHSE_From = tblWarehouse.Code " & _
                            " WHERE         tblGI.withConfirm = 1 " & _
                            " AND           WHSE_To IN (SELECT    DISTINCT tblProdWarehouse.Code  " & _
                            "                             FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "GI-GR-WHSE"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND GI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblGI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID, GI_No AS [GI No.], DateGI AS [GI Date],  Description AS [Issued From], Remarks, tblGI.Status  " & _
                            " FROM          tblGI LEFT JOIN tblProdWarehouse " & _
                            " ON	        tblGI.WHSE_From = tblProdWarehouse.Code " & _
                            " WHERE         tblGI.withConfirm = 1 " & _
                            " AND           WHSE_To IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                            "                             FROM      tblWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "GI-DR-Consignment"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND GI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblGI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID, GI_No AS [GI No.], DateGI AS [GI Date],  Description AS [Issued From], Remarks, tblGI.Status  " & _
                            " FROM          tblGI LEFT JOIN tblProdWarehouse " & _
                            " ON	        tblGI.WHSE_From = tblProdWarehouse.Code " & _
                            " WHERE         tblGI.Type = 'Consigment Item'" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "MR-GI"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND MR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblMR.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT     DISTINCT tblMR_Details.TransID, MR_No AS [MR No.], DateMR AS [MR Date],  tblWarehouse.Description AS [Request From], Remarks, tblMR.Status   " & _
                            "  FROM       tblMR INNER JOIN tblMR_Details  " & _
                            "  ON			tblMR.TransID = tblMR_Details.TransID " & _
                            "  LEFT JOIN	tblWarehouse  " & _
                            "  ON	        tblMR.WHSE_From = tblWarehouse.Code  " & _
                            "  WHERE      tblMR_Details.WHSE  " & _
                            "  IN			(	SELECT    DISTINCT tblWarehouse.Code   " & _
                            "                 FROM      tblWarehouse INNER JOIN tblUser_Access  " & _
                            "                 ON        tblWarehouse.Code = tblUser_Access.Code  " & _
                            "                 AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'  " & _
                            "                 AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                            "                 WHERE     UserID ='" & UserID & "' " & _
                            " 			UNION ALL " & _
                            " 				SELECT    DISTINCT tblProdWarehouse.Code   " & _
                            "                 FROM      tblProdWarehouse INNER JOIN tblUser_Access  " & _
                            "                 ON        tblProdWarehouse.Code = tblUser_Access.Code  " & _
                            "                 AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'  " & _
                            "                 AND       tblUser_Access.Type = 'Production' AND isAllowed = 1  " & _
                            "                             WHERE     UserID ='" & UserID & "')  AND tblMR.TransID in (SELECT DISTINCT TransID FROM viewMR_Unserved)" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PCV-CV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND PCV_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND tblPCV.Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND CASE WHEN View_PCV_Balance.Ref_TransID IS NOT NULL THEN  'Active'  WHEN tblPCV_Entry.Status ='Active' THEN 'Closed' ELSE tblPCV_Entry.Status   END LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID,  tblBranch.Description as Branch, tblPCV_Entry.PCV_No AS [RF No.], DatePCV AS [Date], VCEName AS [Supplier], tblPCV_Entry.Remarks,  " & _
                            "           CASE WHEN View_PCV_Balance.Ref_TransID IS NOT NULL THEN  'Active' " & _
                            "                WHEN tblPCV_Entry.Status ='Active' THEN 'Closed' ELSE tblPCV_Entry.Status   END AS Status" & _
                            " FROM     tblPCV_Entry LEFT JOIN viewVCE_Master " & _
                            " ON	   tblPCV_Entry .VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN View_PCV_Balance " & _
                            " ON        tblPCV_Entry.TransID = View_PCV_Balance.Ref_TransID " & _
                             " LEFT JOIN tblBranch  ON	           " & _
                            " tblPCV_Entry.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblPCV_Entry.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PCVRR-CV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PCVRR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE View_PCVRR_Balance.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT  TransID, PCVRR_No AS [PCVRR No], DatePCVRR, View_PCVRR_Balance.VCECode, VCEName, PCVRR_Amount,  Remarks,    View_PCVRR_Balance.Status  " & _
                    " FROM View_PCVRR_Balance" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "PCVRR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PCVRR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE View_PCVRR_Balance.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblPCVRR.DatePCVRR BETWEEN  @DateFrom AND @DateTo ORDER BY PCVRR_No "

                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT  TransID, PCVRR_No AS [PCVRR No], DatePCVRR, View_PCVRR_Balance.VCECode, VCEName, PCVRR_Amount,  Remarks,   View_PCVRR_Balance.Status  " & _
                            " FROM View_PCVRR_Balance" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If



                Case "PCVRR-PCV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE TransID NOT IN (SELECT PCV_TransID FROM tblPCVRR_Details WHERE Status = 'Active') AND PCV_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE TransID NOT IN (SELECT PCV_TransID FROM tblPCVRR_Details WHERE Status = 'Active') AND  VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE TransID NOT IN (SELECT PCV_TransID FROM tblPCVRR_Details WHERE Status = 'Active') AND  tblPCV.Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE TransID NOT IN (SELECT PCV_TransID FROM tblPCVRR_Details WHERE Status = 'Active') AND  tblPCV.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = "SELECT TransID, PCV_No as [PCV No,], DatePCV as [PCV Date], tblPCV.VCECode, VCEName, Amount, Remarks, tblPCV.Status   " & _
                            "  FROM tblPCV " & _
                            "  LEFT JOIN" & _
                            " viewVCE_Master ON " & _
                            " viewVCE_Master.VCECode = tblPCV.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PL" ' PICK LIST

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PL_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblPL.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        tblPL.TransID, PL_No AS [PL No.], DatePL AS [PL Date],  VCEName AS Supplier, DateDeliver AS [Delivery Date], tblPL.Remarks, tblPL.Status  " & _
                            " FROM          tblPL LEFT JOIN tblVCE_Master " & _
                            " ON            tblPL.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PL-DR"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND PL_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblPL.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT DISTINCT    tblPL.TransID, tblPL_Details.WHSE, tblWarehouse.Description, PL_No AS [PL No.], DatePL AS [PL Date], VCEName AS Supplier, Remarks, tblPL.Status  " & _
                            " FROM       tblPL INNER JOIN tblPL_Details " & _
                            " ON	     tblPL.TransID = tblPL_Details.TransID " & _
                            " LEFT JOIN  tblVCE_Master " & _
                            " ON	     tblPL.VCECode = tblVCE_Master.VCECode " & _
                            " LEFT JOIN  tblWarehouse " & _
                            " ON	     tblPL_Details.WHSE = tblWarehouse.Code " & _
                            " WHERE      tblPL_Details.WHSE IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                            "                             FROM      tblWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "ITI-ITR"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND ITI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblITI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID, ITI_No AS [ITI No.], DateITI AS [ITI Date],  Description AS [Issued From], Remarks, tblITI.Status  " & _
                            " FROM          tblITI LEFT JOIN tblWarehouse " & _
                            " ON	        tblITI.WHSE_From = tblWarehouse.Code " & _
                            " WHERE         WHSE_To IN (SELECT    DISTINCT tblProdWarehouse.Code  " & _
                            "                             FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                Case "MR-ITI"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND MR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblMR.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID, MR_No AS [MR No.], DateMR AS [MR Date],  Description AS [Request From], Remarks, tblMR.Status  " & _
                            " FROM          tblMR LEFT JOIN tblProdWarehouse " & _
                            " ON	        tblMR.WHSE_From = tblProdWarehouse.Code " & _
                            " WHERE        ( WHSE_To IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                            "                             FROM      tblWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "') " & _
                            "               OR WHSE_To IN (SELECT    DISTINCT tblProdWarehouse.Code  " & _
                            "                             FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "'))" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "BI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblBI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, BI_No AS [BI No.], DateBI AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], tblBI.Status  " & _
                            " FROM     tblBI LEFT JOIN tblVCE_Master " & _
                            " ON	   tblBI .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "Book Upload"

                    ' QUERY 
                    query = " SELECT UploadID as TransID, UploadID, Book, SUM((Debit+Credit)/2) as TotalDBCR, CAST(tblJE_Header.datecreated as date) as UploadDate, LoginName, tblJE_Header.Status   FROM tblJE_Header " & _
                            " INNER JOIN tblJE_Details ON " & _
                            " tblJE_Details.JE_No = tblJE_Header.JE_No " & _
                            " LEFT JOIN tblUser ON " & _
                            " tblUser.UserID = tblJE_Header.WhoCreated " & _
                            " WHERE UploadID <> 0 " & _
                            " GROUP BY UploadID, Book, CAST(tblJE_Header.datecreated as date), LoginName, tblJE_Header.Status "
                    SQL.FlushParams()

                Case "OC/DIT Upload"

                    ' QUERY 
                    query = " SELECT TransID as TransID, TransID as UploadID, TotalAmount, Type, Remarks " & _
                            " FROM tblOCDIT "
                    SQL.FlushParams()

                Case "BS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BS_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblBS.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, BS_No AS [BS No.], DateBS AS [Date], VCEName AS [Customer], Remarks,  tblBS.Status  " & _
                            " FROM     tblBS LEFT JOIN viewVCE_Master " & _
                            " ON	   tblBS .VCECode = viewVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "CS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblCS.CS_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewCS_Status.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblCS.TransID, tblCS.CS_No AS [CS No.], DateCS AS [Date], VCEName AS [Customer], Remarks,  viewCS_Status.Status  " & _
                            " FROM     tblCS LEFT JOIN viewVCE_Master " & _
                            " ON	   tblCS .VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN viewCS_Status " & _
                            " ON	   viewCS_Status.TransID = tblCS.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "CS-CV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE viewCS_Status.Status = 'Closed' AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE viewCS_Status.Status = 'Closed' AND tblCS.CS_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE viewCS_Status.Status = 'Closed' AND ViewCS_Balance_Charges.Supplier LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE viewCS_Status.Status = 'Closed' AND tblCS.Remarks LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblCS.TransID, tblCS.CS_No AS [CS No.], DateCS AS [Date], ViewCS_Balance_Charges.Supplier, tblCS.Remarks,  viewCS_Status.Status  " & _
                            " FROM     tblCS " & _
                            " INNER JOIN ViewCS_Balance_Charges " & _
                            " ON	   ViewCS_Balance_Charges.TransID = tblCS.TransID " & _
                            " INNER JOIN viewCS_Status " & _
                            " ON	   viewCS_Status.TransID = tblCS.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "MR"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND MR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Description LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks  LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblMR.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID, MR_No AS [MR No.], DateMR AS [MR Date],  Description AS Warehouse, Remarks, tblMR.Status  " & _
                            " FROM          tblMR LEFT JOIN tblWarehouse " & _
                            " ON	        tblMR.WHSE_To = tblWarehouse.Code " & _
                            " WHERE         WHSE_From IN (SELECT    DISTINCT tblProdWarehouse.Code  " & _
                            "                             FROM      tblProdWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblProdWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Production' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "'" & _
                            "                               UNION ALL" & _
                            "                               SELECT    DISTINCT tblWarehouse.Code  " & _
                            "                             FROM      tblWarehouse INNER JOIN tblUser_Access " & _
                            "                             ON        tblWarehouse.Code = tblUser_Access.Code " & _
                            "                             AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active' " & _
                            "                             AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                            "                             WHERE     UserID ='" & UserID & "') " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                    ' ** LOANS **

                Case "LN"
                    chkBatch.Visible = False
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY Loan_No"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND Loan_No LIKE '%' + @Filter + '%' ORDER BY Loan_No "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE'%' + @Filter + '%' ORDER BY Loan_No "
                            Case "Remarks"
                                filter = " AND Remarks LIKE'%' + @Filter + '%' ORDER BY Loan_No  "
                            Case "Status"
                                filter = " AND tblLoan.Status LIKE '%' + @Filter + '%' ORDER BY Loan_No"
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID,  Loan_No AS [Loan No.], DateLoan AS [Date of Loan],  VCEName, LoanType, LoanAmount, tblLoan.Terms,  tblLoan.Status  " & _
                            " FROM          tblLoan LEFT JOIN viewVCE_Master " & _
                            " ON	        tblLoan.VCECode = viewVCE_Master.VCECode " & _
                            " INNER JOIN    tblLoan_Type " & _
                            " ON            tblLoan.LoanCode = tblLoan_Type.LoanCode " & _
                             " LEFT JOIN tblBranch  ON	           " & _
                            " tblLoan.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblLoan.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "Copy Loan"
                    chkBatch.Visible = True
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE  ForJV = 0 AND  Loan_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE  ForJV = 0 AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE  ForJV = 0 AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE  ForJV = 0 AND tblLoan.Status LIKE '%' + @Filter + '%'  "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID,  Loan_No AS [Loan No.], DateLoan AS [Date of Loan],  VCEName, LoanType, LoanAmount, tblLoan.Terms,  tblLoan.Status  " & _
                            " FROM          tblLoan LEFT JOIN viewVCE_Master " & _
                            " ON	        tblLoan.VCECode = viewVCE_Master.VCECode " & _
                            " INNER JOIN    tblLoan_Type " & _
                            " ON            tblLoan.LoanCode = tblLoan_Type.LoanCode " & filter
                    '" AND tblLoan.Loan_No NOT IN (SELECT DISTINCT REPLACE(RefNo, 'LN:', '') AS RefNo FROM View_GL WHERE Debit > 0 AND RefType IN ('CV', 'JV')) "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "Copy Loan DR"
                    chkBatch.Visible = True
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE  ForJV = 0 AND  Loan_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE  ForJV = 0 AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE  ForJV = 0 AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE  ForJV = 0 AND tblLoan.Status LIKE '%' + @Filter + '%'  "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID,  Loan_No AS [Loan No.], DateLoan AS [Date of Loan],  VCEName, LoanType, LoanAmount, tblLoan.Terms,  tblLoan.Status  " & _
                            " FROM          tblLoan LEFT JOIN viewVCE_Master " & _
                            " ON	        tblLoan.VCECode = viewVCE_Master.VCECode " & _
                            " INNER JOIN    tblLoan_Type " & _
                            " ON            tblLoan.LoanCode = tblLoan_Type.LoanCode " & filter
                    '" AND tblLoan.Loan_No NOT IN (SELECT DISTINCT REPLACE(RefNo, 'LN:', '') AS RefNo FROM View_GL WHERE Debit > 0 AND RefType IN ('CV', 'JV')) "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    'chkBatch.Visible = True
                    '' CONDITION OF QUERY
                    'If cbFilter.SelectedIndex = -1 Then
                    '    filter = " WHERE '' = ''"
                    'Else
                    '    Select Case cbFilter.SelectedItem
                    '        Case "Transaction ID"
                    '            filter = " WHERE (DR_Ref <> 0 OR ForJV = 1) AND Loan_No LIKE '%' + @Filter + '%' "
                    '        Case "VCE Name"
                    '            filter = " WHERE (DR_Ref <> 0 OR ForJV = 1)  AND VCEName '%' + @Filter + '%' "
                    '        Case "Remarks"
                    '            filter = " WHERE (DR_Ref <> 0 OR ForJV = 1)  AND Remarks '%' + @Filter + '%' "
                    '        Case "Status"
                    '            filter = " WHERE (DR_Ref <> 0 OR ForJV = 1)  AND tblLoan.Status IN ('Approved', 'Released') "
                    '    End Select
                    'End If

                    '' QUERY 
                    'query = " SELECT        TransID,  Loan_No AS [Loan No.], DateLoan AS [Date of Loan],  VCEName, LoanType, LoanAmount, tblLoan.Terms,  tblLoan.Status, CAST(0 as bit) AS [Check]  " & vbCrLf & _
                    '        " FROM          tblLoan LEFT JOIN viewVCE_Master " & vbCrLf & _
                    '        " ON	        tblLoan.VCECode = viewVCE_Master.VCECode " & vbCrLf & _
                    '        " INNER JOIN    tblLoan_Type " & vbCrLf & _
                    '        " ON            tblLoan.LoanCode = tblLoan_Type.LoanCode " & filter & vbCrLf & _
                    '        " AND tblLoan.Loan_No NOT IN (SELECT DISTINCT REPLACE(RefNo, 'LN:', '') AS RefNo FROM View_GL WHERE Debit > 0 AND RefType IN ('CV', 'PCV')) "
                    'SQL.FlushParams()
                    'SQL.AddParam("@Filter", txtFilter.Text)


                Case "Copy Loan PCV"
                    chkBatch.Visible = True
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE (DR_Ref <> 0 OR ForJV = 1) AND Loan_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE (DR_Ref <> 0 OR ForJV = 1)  AND VCEName '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE (DR_Ref <> 0 OR ForJV = 1)  AND Remarks '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE (DR_Ref <> 0 OR ForJV = 1)  AND tblLoan.Status IN ('Approved', 'Released') "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT        TransID,  Loan_No AS [Loan No.], DateLoan AS [Date of Loan],  VCEName, LoanType, LoanAmount, tblLoan.Terms,  tblLoan.Status, CAST(0 as bit) AS [Check]  " & vbCrLf & _
                            " FROM          tblLoan LEFT JOIN viewVCE_Master " & vbCrLf & _
                            " ON	        tblLoan.VCECode = viewVCE_Master.VCECode " & vbCrLf & _
                            " INNER JOIN    tblLoan_Type " & vbCrLf & _
                            " ON            tblLoan.LoanCode = tblLoan_Type.LoanCode " & filter & vbCrLf & _
                            " AND tblLoan.Loan_No NOT IN (SELECT DISTINCT REPLACE(RefNo, 'LN:', '') AS RefNo FROM View_GL WHERE Debit > 0 AND RefType IN ('CV', 'PCV')) "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                    ' ** LOANS END ** 
                    dgvList.EditMode = DataGridViewEditMode.EditOnKeystroke

                Case "BOM-SFG"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = '' ORDER BY tblBOM_SFG.BOM_Code"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BOM_Code LIKE '%' + @Filter + '%' ORDER BY tblBOM_SFG.BOM_Code "
                            Case "VCE Name"
                                filter = " WHERE ItemName LIKE '%' + @Filter + '%' ORDER BY tblBOM_SFG.BOM_Code "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' ORDER BY tblBOM_SFG.BOM_Code "
                            Case "Status"
                                filter = " WHERE tblBOM_SFG.Status LIKE '%' + @Filter + '%' ORDER BY tblBOM_SFG.BOM_Code "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT BOM_Code, BOM_Code AS [SFG Code], tblBOM_SFG.ItemCode, ItemName, tblBOM_SFG.UOM, tblBOM_SFG.QTY " & _
                            " FROM   tblBOM_SFG LEFT JOIN tblItem_Master " & _
                            " ON     tblBOM_SFG.ItemCode = tblItem_Master.ItemCode " & _
                            " AND    tblItem_Master.Status ='Active' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                Case "BOM-FG"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = '' ORDER BY tblBOM_FG.BOM_Code"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BOM_Code LIKE '%' + @Filter + '%' ORDER BY tblBOM_FG.BOM_Code"
                            Case "VCE Name"
                                filter = " WHERE ItemName LIKE '%' + @Filter + '%' ORDER BY tblBOM_FG.BOM_Code "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' ORDER BY tblBOM_FG.BOM_Code"
                            Case "Status"
                                filter = " WHERE tblBOM_FG.Status LIKE '%' + @Filter + '%' ORDER BY tblBOM_FG.BOM_Code "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT BOM_Code, BOM_Code AS [BOM Code], tblBOM_FG.ItemCode, ItemName, tblBOM_FG.UOM, tblBOM_FG.QTY " & _
                            " FROM   tblBOM_FG INNER JOIN tblItem_Master " & _
                            " ON     tblBOM_FG.ItemCode = tblItem_Master.ItemCode " & _
                            " AND    tblItem_Master.Status ='Active' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                Case "JO_BOM"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE JO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblJO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, JO_No AS [JO No.], DateJO AS [Date], VCEName, ItemCode, Description, QTY, Remarks  " & _
                            " FROM     tblJO LEFT JOIN tblVCE_Master " & _
                            " ON	   tblJO.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "BR"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BR_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblBR.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, BR_No AS [BR No.], DateBR AS [Date], Bank, Branch, AccountNo, Remarks, tblBR.Status  " & _
                            " FROM     tblBR INNER JOIN tblBank_Master " & _
                            " ON       tblBR.BankID = tblBank_Master.BankID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                Case "JO"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE JO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewJO_Status.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblJO.TransID, JO_No AS [JO No.], DateJO AS [Date], VCEName, ItemCode, Description, QTY, Remarks,  viewJO_Status.Status  " & _
                            " FROM     tblJO LEFT JOIN viewVCE_Master " & _
                            " ON	   tblJO.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN " & _
                            " ( SELECT TransID,  " & _
                            " CASE WHEN COUNT(DISTINCT STATUS) > 1 THEN 'Active' ELSE 'Closed' END AS Status   " & _
                            " FROM viewJO_Status  GROUP BY TransID) AS   viewJO_Status  " & _
                            " ON viewJO_Status.TransID = tblJO.TransID  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "JO-BOM"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' GROUP BY tblJO.TransID, tblJO.JO_No, DateJO , VCEName,  tblJO.Remarks,  tblJO.Status "
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblJO.JO_No LIKE '%' + @Filter + '%' GROUP BY tblJO.TransID, tblJO.JO_No, DateJO , VCEName,  tblJO.Remarks,  tblJO.Status"
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' GROUP BY tblJO.TransID, tblJO.JO_No, DateJO , VCEName,  tblJO.Remarks,  tblJO.Status"
                            Case "Remarks"
                                filter = " AND tblJO.Remarks LIKE '%' + @Filter + '%' GROUP BY tblJO.TransID, tblJO.JO_No, DateJO , VCEName,  tblJO.Remarks,  tblJO.Status"
                            Case "Status"
                                filter = " AND tblJO.Status LIKE '%' + @Filter + '%' GROUP BY tblJO.TransID, tblJO.JO_No, DateJO , VCEName,  tblJO.Remarks,  tblJO.Status"
                        End Select
                    End If
                    ' QUERY 
                    query = " SELECT   tblJO.TransID, tblJO.JO_No AS [JO No.], DateJO AS [Date], VCEName, Count(tblJO_Details.ItemCode) AS ItemCount, tblJO.Remarks,  tblJO.Status  " & _
                            " FROM     tblJO LEFT JOIN viewVCE_Master " & _
                            " ON	   tblJO.VCECode = viewVCE_Master.VCECode " & _
                            "   INNER JOIN tblJO_Details " & _
                            " ON	   tblJO_Details.TransID = tblJO.TransID " & _
                            " WHERE	   tblJO.TransID NOT IN (SELECT JO_Ref FROM tblBOM WHERE Status <> 'Cancelled') " & _
                            " AND tblJO.ForBOM = 1 " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "JO-BOMC-perLine"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE JO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT    CAST(TransID AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS TransID,  " & _
                            " 	        CAST(JO_No AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS [JO No.], " & _
                            " 	        DateJO AS [Date], VCEName AS [Customer], ItemCode, QTY,  Status   " & _
                            " FROM " & _
                            " ( " & _
                            " 	 SELECT		tblJO.TransID , tblJO.JO_No, DateJO, VCEName, tblJO_Details.ItemCode, tblJO_Details.QTY, viewJO_Status.Status, " & _
                            " 				ROW_NUMBER() OVER (PARTITION BY tblJO.JO_No ORDER BY tblJO.JO_No) AS RowID " & _
                            " 	 FROM		tblJO LEFT JOIN tblJO_Details   " & _
                            " 	 ON			tblJO.TransID = tblJO_Details.TransID  " & _
                            " 	 LEFT JOIN	viewVCE_Master  " & _
                            " 	 ON			tblJO.VCECode = viewVCE_Master.VCECode  " & _
                            "   LEFT JOIN viewJO_Status ON " & _
                            "   viewJO_Status.TransID = tblJO.TransID AND tblJO_Details.LineNum =  viewJO_Status.LineNum" & _
                            " ) AS JO " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                    'Case "JO-BOMC"

                    '    ' CONDITION OF QUERY
                    '    If cbFilter.SelectedIndex = -1 Then
                    '        filter = " AND '' = ''"
                    '    Else
                    '        Select Case cbFilter.SelectedItem
                    '            Case "Transaction ID"
                    '                filter = " AND JO_No LIKE '%' + @Filter + '%' "
                    '            Case "VCE Name"
                    '                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                    '            Case "Remarks"
                    '                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                    '            Case "Status"
                    '                filter = " AND tblJO.Status LIKE '%' + @Filter + '%' "
                    '        End Select
                    '    End If
                    '    ' QUERY 
                    '    query = " SELECT   TransID, JO_No AS [JO No.], DateJO AS [Date], VCEName, ItemCode, Description, QTY, Remarks,  tblJO.Status  " & _
                    '            " FROM     tblJO LEFT JOIN viewVCE_Master " & _
                    '            " ON	   tblJO.VCECode = viewVCE_Master.VCECode " & _
                    '            " WHERE	   TransID IN (SELECT JO_Ref FROM tblBOM WHERE Status <> 'Cancelled') " & _
                    '            " AND tblJO.ForBOM = 1 " & filter
                    '    SQL.FlushParams()
                    '    SQL.AddParam("@Filter", txtFilter.Text)

                Case "BOM_PR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BOM_No LIKE '%' + @Filter + '%' AND ForPR = 1 "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' AND ForPR = 1 "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' AND ForPR = 1 "
                            Case "Status"
                                filter = " WHERE tblBOM.Status LIKE '%' + @Filter + '%' AND ForPR = 1 "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, BOM_No AS [BOM No.], DateBOM AS [Date], VCEName, ItemCode, Description, QTY, Remarks,  tblBOM.Status  " & _
                            " FROM     tblBOM LEFT JOIN viewVCE_Master " & _
                            " ON	   tblBOM.VCECode = viewVCE_Master.VCECode " & filter & " AND TransID NOT IN (SELECT BOM_TransID FROM viewPR_SO_Served) AND tblBOM.Status <> 'Cancelled'"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "BOM"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE BOM_No LIKE '%' + @Filter + '%' AND ForPR = 1 "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' AND ForPR = 1 "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' AND ForPR = 1 "
                            Case "Status"
                                filter = " WHERE tblBOM.Status LIKE '%' + @Filter + '%' AND ForPR = 1 "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, BOM_No AS [BOM No.], DateBOM AS [Date], VCEName, ItemCode, Description, QTY, Remarks,  tblBOM.Status  " & _
                            " FROM     tblBOM LEFT JOIN tblVCE_Master " & _
                            " ON	   tblBOM.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "RFP"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE RFP_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblRFP.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, RFP_No AS [RFP No.], DateRFP AS [Date], VCEName, Remarks,  tblRFP.Status  " & _
                            " FROM     tblRFP LEFT JOIN tblVCE_Master " & _
                            " ON	   tblRFP.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PRQ"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblPR.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblPR.DatePR BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, PR_No AS [PR No.], DatePR AS [Date],  Remarks, DateNeeded, RequestedBy, tblPR.Status  " & _
                            " FROM     tblPR LEFT JOIN tblVCE_Master " & _
                            " ON	   tblPR.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "PO-ADV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblPO.PO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewPO_Status.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblPO.DatePO BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblPO.TransID, tblPO.PO_No AS [PO No.], DatePO AS [Date], VCEName AS [Supplier], Remarks, NetAmount, viewPO_Status.Status  " & _
                            " FROM     tblPO LEFT JOIN viewVCE_Master " & _
                            " ON	   tblPO.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN	 viewPO_Status " & _
                            " ON		 tblPO.TransID = viewPO_Status.TransID " & _
                            " INNER JOIN	 tblPO_PartialPayment_Header " & _
                            " ON		 tblPO.TransID = tblPO_PartialPayment_Header.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "MRIS-GR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblMRIS.MRIS_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewMRIS_GR_Status.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblMRIS.DateMRIS BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblMRIS.TransID, tblMRIS.MRIS_No AS [MRIS No.], DateMRIS AS [Date],  viewMRIS_GR_Status.Status, tblMRIS.WHSE_From  " & _
                            " FROM     tblMRIS  LEFT JOIN	 viewMRIS_GR_Status " & _
                            " ON		 tblMRIS.TransID = viewMRIS_GR_Status.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "IC-GR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblIC.IC_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewIC_GR_Status.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblIC.DateIC BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblIC.TransID, tblIC.IC_No AS [IC No.], DateIC AS [Date],  viewIC_GR_Status.Status, tblIC.WHSE  " & _
                            " FROM     tblIC  LEFT JOIN	 viewIC_GR_Status " & _
                            " ON		 tblIC.TransID = viewIC_GR_Status.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "IC-GI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblIC.IC_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewIC_GI_Status.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblIC.DateIC BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblIC.TransID, tblIC.IC_No AS [IC No.], DateIC AS [Date],  viewIC_GI_Status.Status, tblIC.WHSE  " & _
                            " FROM     tblIC  LEFT JOIN	 viewIC_GI_Status " & _
                            " ON		 tblIC.TransID = viewIC_GI_Status.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If



                Case "PO"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblPO.PO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE viewPO_Status.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblPO.DatePO BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblPO.TransID, tblPO.PO_No AS [PO No.], DatePO AS [Date], VCEName AS [Supplier], Remarks, NetAmount, viewPO_Status.Status  " & _
                            " FROM     tblPO LEFT JOIN viewVCE_Master " & _
                            " ON	   tblPO.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN	 viewPO_Status " & _
                            " ON		 tblPO.TransID = viewPO_Status.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "RR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY tblRR.TransID"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND RR_No LIKE '%' + @Filter + '%' ORDER BY tblRR.TransID "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' ORDER BY tblRR.TransID"
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' ORDER BY tblRR.TransID"
                            Case "Status"
                                filter = " AND tblRR.Status LIKE '%' + @Filter + '%' ORDER BY tblRR.TransID"
                            Case "Date Range"
                                filter = " AND tblRR.DateRR BETWEEN  @DateFrom AND @DateTo  ORDER BY tblRR.TransID "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, RR_No AS [RR No.], DateRR AS [Date], VCEName AS [Supplier], Remarks, PO_Ref AS [Reference PO], tblRR.Status  " & _
                            " FROM     tblRR LEFT JOIN viewVCE_Master " & _
                            " ON	   tblRR .VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN tblWarehouse  ON	         " & _
                            "  tblRR.WHSE = tblWarehouse.Code   " & _
                            "  WHERE         " & _
                            "  ( WHSE IN (SELECT    DISTINCT tblWarehouse.Code " & _
                            "  FROM      tblWarehouse  " & _
                            "  INNER JOIN tblUser_Access    ON         " & _
                            "  tblWarehouse.Code = tblUser_Access.Code  " & _
                            "   AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'  " & _
                            "   AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1 " & _
                            "   WHERE     UserID ='" & UserID & "'))  " & _
                            "   OR " & _
                            "   WHSE ='MW' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "RR-APV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' "
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND RR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks  LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblRR.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, RR_No AS [RR No.], DateRR AS [Date], VCEName AS [Supplier], Remarks, PO_Ref AS [Reference PO], tblRR.Status  " & _
                            " FROM     tblRR LEFT JOIN viewVCE_Master " & _
                            " ON	   tblRR .VCECode = viewVCE_Master.VCECode " & _
                            "WHERE TransID not in (SELECT ISNULL(RR_Ref,0) AS RR_Ref from tblAPV WHERE Status ='Active' UNION ALL SELECT ISNULL(RR_Ref,0) from tblCV WHERE Status ='Active')  " & filter & " ORDER BY tblRR.TransID "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "APV"
                    ' CONDITION OF QUERY
                    Dim project As String
                    project = cbProject.Text

                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND convert(nvarchar, tblAPV.APV_No) LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' "
                            Case "VCE Name"
                                filter = " AND  VCEName LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' "
                            Case "Remarks"
                                filter = " AND tblAPV.Remarks LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' "
                            Case "Status"
                                filter = " AND CASE  WHEN tblAPV.Status ='Cancelled' THEN 'Cancelled' WHEN View_APV_Balance.Ref_TransID IS NOT NULL THEN  'Active' WHEN tblAPV.Status ='Active' THEN 'Closed' ELSE tblAPV.Status  END LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' "
                            Case "Date Range"
                                filter = " AND tblAPV.DateAPV BETWEEN  @DateFrom AND @DateTo AND CostCenter LIKE '%" & project & "%' ORDER BY tblAPV.APV_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch, CASt(tblAPV.APV_No AS NVARCHAR) AS [APV No.], DateAPV AS [Date], VCEName AS [Supplier], tblAPV.Remarks, PO_Ref AS [Reference PO], " & vbCrLf &
                         "           CASE  WHEN tblAPV.Status ='Cancelled' THEN 'Cancelled' WHEN View_APV_Balance.Ref_TransID IS NOT NULL THEN  'Active' " & vbCrLf &
                         "                WHEN tblAPV.Status ='Active' THEN 'Closed' ELSE tblAPV.Status   END AS Status" & vbCrLf &
                         " FROM     tblAPV LEFT JOIN viewVCE_Master " & vbCrLf &
                         " ON	   tblAPV .VCECode = viewVCE_Master.VCECode " & vbCrLf &
                          " LEFT JOIN tblBranch  ON	           " & vbCrLf &
                         " tblAPV.BranchCode = tblBranch.BranchCode      " & vbCrLf &
                         " LEFT JOIN  (SELECT Ref_TransID FROM  View_APV_Balance GROUP BY  Ref_TransID) AS View_APV_Balance" & vbCrLf &
                         " ON        convert(nvarchar(100), tblAPV.APV_No) = convert(nvarchar(100), View_APV_Balance.Ref_TransID) " & vbCrLf &
                          " WHERE      1=1    " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "SQ"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblSQ.SQ_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblSQ.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblSQ.TransID, tblSQ.SQ_No AS [SQ No.], DateSQ AS [Date], VCEName AS [Customer], Remarks, NetAmount, tblSQ.Status  " & _
                            " FROM     tblSQ LEFT JOIN tblVCE_Master " & _
                            " ON	   tblSQ.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "SO"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblSO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   CAST(tblSO.TransID AS nvarchar)  AS TransID, CAST(tblSO.SO_No AS nvarchar)  AS [SO No.],  " & _
                            "          DateSO AS [Date], VCEName AS [Customer], tblSO.NetAmount, Remarks, ReferenceNo, tblSO.Status  " & _
                            " FROM     tblSO LEFT JOIN tblVCE_Master " & _
                            " ON	   tblSO.VCECode = tblVCE_Master.VCECode "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PR_SO"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblSO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   CAST(tblSO.TransID AS nvarchar)  AS TransID, CAST(tblSO.SO_No AS nvarchar)  AS [SO No.],  " & _
                            "          DateSO AS [Date], VCEName AS [Customer], tblSO.NetAmount, tblSO.Remarks, ReferenceNo, tblSO.Status  " & _
                            " FROM     tblSO LEFT JOIN viewVCE_MAster " & _
                            " ON	   tblSO.VCECode = viewVCE_MAster.VCECode " & _
                            " WHERE   tblso.transid IN ( select tblso.transid from tblso " & _
                            " INNER JOIN  tblJO ON  tblSO.TransID = tblJO.SO_Ref  " & _
                            " INNER JOIN  tblBOM ON " & _
                            " tblJO.TransID = tblBOM.JO_Ref " & _
                            " WHERE tblbom.transid NOT IN (SELECT BOM_TransID FROM viewPR_SO_Served)) AND tblSO.Status <> 'Cancelled'"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PO_SO"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE tblSO.SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblSO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT CAST(tblSO.TransID AS nvarchar)  AS TransID, CAST(tblSO.SO_No AS nvarchar)  AS [SO No.], " & _
                            " 		DateSO AS [Date], VCEName AS [Customer], tblSO.NetAmount, tblSO.Remarks, ReferenceNo, tblSO.Status  " & _
                            " FROM     tblSO  " & _
                            " LEFT JOIN viewVCE_MAster  ON	    " & _
                            " tblSO.VCECode = viewVCE_MAster.VCECode " & _
                            " WHERE tblSO.TransID NOT IN (SELECT SO_Ref FROM tblPO WHERE tblPO.Status <> 'Cancelled') " & _
                            "  AND tblSO.Status <> 'Cancelled' "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "SO-JO"
                    query = " SELECT   RefID, SO_No AS [SO No.], " & _
                            " 	       DateSO AS [Date], VCEName AS [Customer], ItemCode, QTY, DateDeliver AS [Date Needed], ReferenceNo, Status   " & _
                            " FROM     viewSO_SKU " & _
                            " WHERE    Status ='Active' " & _
                            " ORDER BY RefID "

                Case "SO-JO-perLine"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE SO_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT    CAST(TransID AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS TransID,  " & _
                            " 	        CAST(SO_No AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS [SO No.], " & _
                            " 	        DateSO AS [Date], VCEName AS [Customer], ItemCode, QTY, DateDeliver AS [Date Needed], ReferenceNo, Status   " & _
                            " FROM " & _
                            " ( " & _
                            " 	 SELECT		tblSO.TransID , tblSO.SO_No, DateSO, VCEName, ItemCode, SUM(QTY) AS QTY, ReferenceNo, viewSO_Status.Status, " & _
                            " 				ROW_NUMBER() OVER (PARTITION BY tblSO.SO_No ORDER BY tblSO.SO_No) AS RowID, MAX(tblSO_Details.DateDeliver) AS DateDeliver " & _
                            " 	 FROM		tblSO LEFT JOIN tblSO_Details   " & _
                            " 	 ON			tblSO.TransID = tblSO_Details.TransID  " & _
                            " 	 LEFT JOIN	tblVCE_Master  " & _
                            " 	 ON			tblSO.VCECode = tblVCE_Master.VCECode  " & _
                            "   LEFT JOIN viewSO_Status ON " & _
                            "   viewSO_Status.TransID = tblSO.TransID" & _
                            " 	 GROUP BY   tblSO.TransID , tblSO.SO_No, DateSO, VCEName, ItemCode, ReferenceNo, viewSO_Status.Status   " & _
                            " ) AS SO " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "RR-Depre-perLine"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''  ORDER by TransID"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE RR_No LIKE '%' + @Filter + '%'  ORDER by TransID"
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%'  ORDER by TransID"
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%'  ORDER by TransID"
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%'  ORDER by TransID"
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT    CAST(TransID AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS TransID,   " & _
                            "  	        CAST(RR_No AS nvarchar) + '-' + CAST(RowID AS nvarchar)  AS [RR No.],  " & _
                            "  	        DateRR AS [Date], VCEName AS [Customer], ItemCode, QTY,  Status    " & _
                            "  FROM  " & _
                            "  (  " & _
                            "   	 SELECT		tblRR.TransID , tblRR.RR_No, DateRR, VCEName, viewRR_ForDepreciation.ItemCode, SUM(UnProduced) AS QTY, viewRR_Depre_Status.Status,   " & _
                            "   				viewRR_ForDepreciation.SubID AS RowID " & _
                            "   	 FROM		tblRR  " & _
                            " 	    INNER JOIN viewRR_ForDepreciation     " & _
                            "   	 ON			tblRR.TransID = viewRR_ForDepreciation.TransID    " & _
                            "   	 INNER JOIN tblItem_Master     " & _
                            "   	 ON			tblItem_Master.ItemCode = viewRR_ForDepreciation.ItemCode  AND isFixAsset = 1   " & _
                            "   	 LEFT JOIN	viewVCE_Master    " & _
                            "   	 ON			tblRR.VCECode = viewVCE_Master.VCECode    " & _
                            "     LEFT JOIN viewRR_Depre_Status ON   " & _
                            "     viewRR_Depre_Status.TransID = viewRR_ForDepreciation.TransID AND viewRR_Depre_Status.LineNum= viewRR_ForDepreciation.SubID " & _
                            "   	 GROUP BY   tblRR.TransID , tblRR.RR_No, DateRR, VCEName, viewRR_ForDepreciation.ItemCode, UnProduced, viewRR_Depre_Status.Status, viewRR_ForDepreciation.SubID " & _
                            "  ) AS RR  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "DR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE DR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblDR.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblDR.DateDR BETWEEN  @DateFrom AND @DateTo ORDER BY DR_No "

                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, DR_No AS [DR No.], DateDR AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], tblDR.Status  " & _
                            " FROM     tblDR LEFT JOIN tblVCE_Master " & _
                            " ON	   tblDR .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "ECS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE ECS_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblECS.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblECS.ECS_No AS [ECS No.], DateECS AS [Date], VCEName AS [Customer], tblECS.Remarks, SO_Ref AS [Reference SO], " & _
                            "       tblECS.Status" & _
                            " FROM     tblECS LEFT JOIN tblVCE_Master " & _
                            " ON	   tblECS.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "SI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE SI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblSI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblSI.SI_No AS [SI No.], DateSI AS [Date], VCEName AS [Customer], tblSI.Remarks, SO_Ref AS [Reference SO], " & _
                            "           CASE WHEN View_SI_Balance.Ref_TransID IS NOT NULL THEN  'Active' " & _
                            "                WHEN tblSI.Status ='Active' THEN 'Closed' ELSE tblSI.Status   END AS Status" & _
                            " FROM     tblSI LEFT JOIN viewVCE_Master " & _
                            " ON	   tblSI.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN View_SI_Balance " & _
                            " ON        tblSI.TransID = View_SI_Balance.Ref_TransID " & filter

                    'query = " SELECT   TransID, SI_No AS [SI No.], DateSI AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], tblSI.Status  " & _
                    '        " FROM     tblSI LEFT JOIN tblVCE_Master " & _
                    '        " ON	   tblSI .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

               Case "ADV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE ADV_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE View_ADV_Balance.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, ADV_No AS [ADV No.], DateADV AS [Date], VCEName AS [Supplier], Balance, Remarks, View_ADV_Balance.Status  " & _
                            " FROM     View_ADV_Balance " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "CA"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE CA_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE View_CA_Balance.VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE View_CA_Balance.Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                'filter = " WHERE tblCA.Status LIKE '%' + @Filter + '%' "
                                filter = " WHERE View_CA_Balance.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblCA.DateCA BETWEEN  @DateFrom AND @DateTo ORDER BY CA_No "
                        End Select
                    End If

                    ' QUERY 
                    'query = " SELECT   TransID, CA_No AS [CA No.], DateCA AS [Date], VCEName AS [Name], Remarks,  tblCA.Status  " & _
                    '        " FROM     tblCA LEFT JOIN viewVCE_Master " & _
                    '        " ON	   tblCA .VCECode = viewVCE_Master.VCECode " & filter
                    query = "  SELECT   tblCA.TransID, CA_No AS [CA No.], DateCA AS [Date], View_CA_Balance.VCEName, tblCA.Remarks,  View_CA_Balance.Status   " & vbCrLf & _
                            "  FROM     tblCA  " & vbCrLf & _
                            "  LEFT JOIN viewVCE_Master  " & vbCrLf & _
                            "  ON	   tblCA.VCECode = viewVCE_Master.VCECode  " & vbCrLf & _
                            "  INNER JOIN View_CA_Balance ON " & vbCrLf & _
                            "  tblCA.TransID = View_CA_Balance.TransID " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "DP"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE DP_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblDepreciation.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblDepreciation.DateDP BETWEEN  @DateFrom AND @DateTo ORDER BY DP_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, DP_No AS [DP No.], DateDP AS [Date],  Remarks,  tblDepreciation.Status  " & _
                            " FROM     tblDepreciation " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "CA-JV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE CA_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE View_CA_Balance.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE View_CA_Balance.Date BETWEEN  @DateFrom AND @DateTo ORDER BY [CA No.] "
                        End Select
                    End If


                    query = " SELECT TransID, [CA No.], Date, VCEName, CA_Amount, ISNULL(Balance,0) AS Balance, Remarks,  Status    " &
                            " FROM     View_CA_Balance  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "CA-Liquidation"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE CA_No LIKE '%' + @Filter + '%' ORDER BY [CA No.]"
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' ORDER BY [CA No.]"
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' ORDER BY [CA No.]"
                            Case "Status"
                                filter = " WHERE View_CA_Balance.Status LIKE '%' + @Filter + '%' AND Balance < 0 ORDER BY [CA No.]"
                            Case "Date Range"
                                filter = " WHERE View_CA_Balance.Date BETWEEN  @DateFrom AND @DateTo ORDER BY [CA No.] "
                        End Select
                    End If


                    query = " SELECT TransID, [CA No.], Date, VCEName, CA_Amount, Balance, Remarks,  Status  " &
                            " FROM     View_CA_Balance  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "CV"
                    Dim project As String
                    project = cbProject.SelectedItem
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY CV_No"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND CV_No LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%'  ORDER BY CV_No"
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' ORDER BY CV_No "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' ORDER BY CV_No "
                            Case "Status"
                                filter = " AND tblCV.Status LIKE '%' + @Filter + '%' AND CostCenter LIKE '%" & project & "%' ORDER BY CV_No "
                            Case "Date Range"
                                filter = " AND tblCV.DateCV BETWEEN  @DateFrom AND @DateTo AND CostCenter LIKE '%" & project & "%' ORDER BY CV_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch, CV_No AS [CV No.], DateCV AS [Date], VCEName AS [Supplier], TotalAmount AS Amount, Remarks, APV_Ref AS [Reference APV], tblCV.Status  " & _
                            " FROM     tblCV LEFT JOIN viewVCE_Master " & _
                            " ON	   tblCV .VCECode = viewVCE_Master.VCECode " & _
                             " LEFT JOIN tblBranch  ON	           " & _
                            " tblCV.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblCV.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "ATD"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY ATD_No"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND ATD_No LIKE '%' + @Filter + '%' ORDER BY ATD_No"
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' ORDER BY ATD_No "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' ORDER BY ATD_No "
                            Case "Status"
                                filter = " AND tblATD.Status LIKE '%' + @Filter + '%' ORDER BY ATD_No "
                            Case "Date Range"
                                filter = " AND tblATD.DateATD BETWEEN  @DateFrom AND @DateTo ORDER BY ATD_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch, ATD_No AS [ATD No.], DateATD AS [Date], VCEName, Total_Amount AS Amount, Remarks, MRIS_Ref AS [Reference MRIS], tblATD.Status  " & _
                            " FROM     tblATD LEFT JOIN viewVCE_Master " & _
                            " ON	   tblATD .VCECode = viewVCE_Master.VCECode " & _
                             " LEFT JOIN tblBranch  ON	           " & _
                            " tblATD.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblATD.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "ATD-CV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY viewATD_Balance.ATD_No"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND viewATD_Balance.ATD_No LIKE '%' + @Filter + '%'  ORDER BY viewATD_Balance.ATD_No"
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%'  ORDER BY viewATD_Balance.ATD_No"
                            Case "Remarks"
                                filter = " AND viewATD_Balance.Remarks LIKE '%' + @Filter + '%' ORDER BY viewATD_Balance.ATD_No"
                            Case "Status"
                                filter = " AND viewATD_Balance.Status LIKE '%' + @Filter + '%' ORDER BY viewATD_Balance.ATD_No"
                            Case "Date Range"
                                filter = " AND viewATD_Balance.DateATD BETWEEN  @DateFrom AND @DateTo ORDER BY viewATD_Balance.ATD_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT	TransID, ATD_No, DateATD, viewATD_Balance.VCEName, Ledger_Code, Total_Amount, No_of_Payday, Start_Date, ATDType, viewVCE_Master.VCEName AS CustomerName " & _
                            " FROM	    viewATD_Balance " & _
                             " LEFT JOIN viewVCE_Master  ON	           " & _
                            " viewVCE_Master.VCECode = viewATD_Balance.CustomerCode      " & _
                            " WHERE 	viewATD_Balance.Status ='Active' " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "PAYROLL BS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY Billing_No"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND Billing_No LIKE '%' + @Filter + '%' ORDER BY Billing_No"
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' ORDER BY Billing_No "
                            Case "Status"
                                filter = " AND tblBilling_Header.Status LIKE '%' + @Filter + '%' ORDER BY Billing_No "
                            Case "Date Range"
                                filter = " AND tblBilling_Header.Paydate BETWEEN  @DateFrom AND @DateTo ORDER BY Billing_No "
                        End Select
                    End If
                    SetPayrollDatabase()
                    ' QUERY 
                    query = "  SELECT  TransID, Billing_No, tblBilling_Header.Payroll_Period, " & _
                            " 		CASE WHEN tblBilling_Header.Payroll_Period = 'SIL' " & _
                            " 			 THEN BillingDate ELSE Paydate " & _
                            " 		END AS Paydate,  " & _
                            " 		CASE WHEN tblBilling_Header.Payroll_Period = 'SIL' " & _
                            " 			 THEN Remarks + '(' + Org_Name + ')'  ELSE Remarks " & _
                            " 		END AS Remarks,  " & _
                            " 		Org_Name,   " & _
                            "  		GrossAmount, VATamount, EWTamount, AmountDue, BillingDate, tblBilling_Header.Status   " & _
                            "  FROM    tblBilling_Header LEFT JOIN tblPayroll_Period  " & _
                            "  ON		tblBilling_Header.Payroll_Period = tblPayroll_Period.Payroll_Period  " & _
                            "  WHERE   tblBilling_Header.EntryStatus = 0 AND tblBilling_Header.Status ='Posted' " & filter
                    SQL_RUBY.FlushParams()
                    SQL_RUBY.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL_RUBY.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL_RUBY.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                    SQL_RUBY.GetQuery(query)
                    If SQL_RUBY.SQLDS.Tables(0).Rows.Count > 0 Then
                        dgvList.DataSource = SQL_RUBY.SQLDS.Tables(0)
                        dgvList.Columns(0).Visible = False
                        If chkBatch.Visible = True Then
                            If chkBatch.Checked = True Then
                                Dim colX As New DataGridViewCheckBoxColumn
                                colX.HeaderText = "Include"
                                colX.Name = "dgcInc"
                                colX.Width = 50
                                colX.DefaultCellStyle.NullValue = False
                                dgvList.Columns.Add(colX)
                                colX.DisplayIndex = 1
                            End If
                        End If
                    End If
                    query = ""

                Case "PAYROLL-APV"
                    ' CONDITION OF QUERY
                    SetPayrollDatabase()
                    ' QUERY 
                    query = " SELECT DISTINCT viewPayroll_Ledger.Payroll_Period, viewPayroll_Ledger.Org_Name,  tblOrg_CostHeader.VCECode " & _
                            " FROM viewPayroll_Ledger  " & _
                            " LEFT JOIN  " & _
                            " ( " & _
                            " 	SELECT	Payroll_Period, OrgCost_ID  " & _
                            " 	FROM	tblPayroll_EntryPosting  " & _
                            " 	WHERE	Status ='Posted' " & _
                            " ) AS Posted " & _
                            " ON viewPayroll_Ledger.Payroll_Period = Posted.Payroll_Period " & _
                            " AND viewPayroll_Ledger.OrgCost_ID = Posted.OrgCost_ID " & _
                            " LEFT JOIN tblOrg_CostHeader " & _
                            " ON viewPayroll_Ledger.OrgCost_ID = tblOrg_CostHeader.OrgCost_ID " & _
                            " WHERE Posted.Payroll_Period IS NULL "
                    SQL_RUBY.FlushParams()

                    SQL_RUBY.GetQuery(query)
                    If SQL_RUBY.SQLDS.Tables(0).Rows.Count > 0 Then
                        dgvList.DataSource = SQL_RUBY.SQLDS.Tables(0)
                        dgvList.Columns(0).Visible = True

                        If chkBatch.Visible = True Then
                            If chkBatch.Checked = True Then
                                Dim colX As New DataGridViewCheckBoxColumn
                                colX.HeaderText = "Include"
                                colX.Name = "dgcInc"
                                colX.Width = 50
                                colX.DefaultCellStyle.NullValue = False
                                dgvList.Columns.Add(colX)
                                colX.DisplayIndex = 1
                            End If
                        End If
                    End If
                    query = ""
                Case "PCV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PCV_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblPCV.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblPCV.DatePCV BETWEEN  @DateFrom AND @DateTo ORDER BY PCV_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT  TransID, PCV_No AS [PCV No],  tblPCV.VCECode, VCEName, DatePCV, Amount, Remarks,    tblPCV.Status  " & _
                    " FROM tblPCV" & _
                     " LEFT JOIN tblVCE_Master  ON  " & _
                     " tblPCV.VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "BD"

                    ' QUERY 
                    query = "  SELECT   TransID, tblBranch.Description as Branch,BD_No AS [BD No.], DateBD AS [Date], Remarks, Bank, Branch, tblBD.Status " & _
                            "  FROM     tblBD LEFT JOIN tblBank_Master ON " & _
                            "  tblBank_Master.BankID = tblBD.BankID " & _
                            " LEFT JOIN tblBranch  ON	           " & _
                            " tblBD.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblBD.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )  "
                    SQL.FlushParams()

                Case "MRIS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND MRIS_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblMRIS.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblMRIS.DateMRIS BETWEEN  @DateFrom AND @DateTo ORDER BY MRIS_No "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT   TransID, MRIS_No AS [MRIS No.], DateMRIS AS [Date], VCEName AS [VCEName], Remarks,   tblMRIS.Status  " & _
                            "  FROM     tblMRIS LEFT JOIN viewVCE_Master " & _
                            "  ON	   tblMRIS.VCECode = viewVCE_Master.VCECode " & _
                            "   LEFT JOIN tblProdWarehouse  ON	          " & _
                            "   tblMRIS.WHSE_From = tblProdWarehouse.Code    " & _
                            "   WHERE          " & _
                            "   ( WHSE_From IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                            "   FROM      tblWarehouse   " & _
                            "   INNER JOIN tblUser_Access    ON          " & _
                            "   tblWarehouse.Code = tblUser_Access.Code   " & _
                            "    AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                            "    AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                            "    WHERE     UserID ='" & UserID & "')                  " & _
                            "    OR  " & _
                            "     WHSE_From IN (SELECT    DISTINCT tblProdWarehouse.Code   " & _
                            "     FROM      tblProdWarehouse INNER JOIN tblUser_Access     " & _
                            "      ON        tblProdWarehouse.Code = tblUser_Access.Code    " & _
                            "  	 AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'    " & _
                            "  	 AND       tblUser_Access.Type = 'Production' AND isAllowed = 1    " & _
                            "  	 WHERE     UserID ='" & UserID & "'))  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "GI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND GI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblGI.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblGI.DateGI BETWEEN  @DateFrom AND @DateTo ORDER BY GI_No "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT   TransID, GI_No AS [GI No.], DateGI AS [Date], VCEName AS [VCEName], Remarks, tblGI.Type AS [Type], ATDNo, tblGI.Status  " & _
                            "  FROM     tblGI LEFT JOIN viewVCE_Master " & _
                            "  ON	   tblGI.VCECode = viewVCE_Master.VCECode " & _
                            "   LEFT JOIN tblProdWarehouse  ON	          " & _
                            "   tblGI.WHSE_From = tblProdWarehouse.Code    " & _
                            "   WHERE          " & _
                            "   ( WHSE_From IN (SELECT    DISTINCT tblWarehouse.Code  " & _
                            "   FROM      tblWarehouse   " & _
                            "   INNER JOIN tblUser_Access    ON          " & _
                            "   tblWarehouse.Code = tblUser_Access.Code   " & _
                            "    AND       tblUser_Access.Status ='Active' AND tblWarehouse.Status ='Active'   " & _
                            "    AND       tblUser_Access.Type = 'Warehouse' AND isAllowed = 1  " & _
                            "    WHERE     UserID ='" & UserID & "')                  " & _
                            "    OR  " & _
                            "     WHSE_From IN (SELECT    DISTINCT tblProdWarehouse.Code   " & _
                            "     FROM      tblProdWarehouse INNER JOIN tblUser_Access     " & _
                            "      ON        tblProdWarehouse.Code = tblUser_Access.Code    " & _
                            "  	 AND       tblUser_Access.Status ='Active' AND tblProdWarehouse.Status ='Active'    " & _
                            "  	 AND       tblUser_Access.Type = 'Production' AND isAllowed = 1    " & _
                            "  	 WHERE     UserID ='" & UserID & "'))  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "GR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE GR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblGR.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblGR.DateGR BETWEEN  @DateFrom AND @DateTo ORDER BY GR_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, GR_No AS [GR No.], DateGR AS [Date], VCEName AS [VCEName], Remarks, Type AS [Type], tblGR.Status  " & _
                            " FROM     tblGR LEFT JOIN tblVCE_Master " & _
                            " ON	   tblGR .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "IC"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE IC_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblIC.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, IC_No AS [IC No.], DateIC AS [Date], CONCAT(WHSE,' | ',viewWarehouse_Maintenance.Description) AS WHSE,  Remarks,  tblIC.Status  " & _
                            " FROM     tblIC LEFT JOIN viewWarehouse_Maintenance " & _
                            " ON	   tblIC.WHSE = viewWarehouse_Maintenance.Code " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "ITI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE ITI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblITI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, ITI_No AS [ITI No.], DateITI AS [Date], VCEName AS [VCEName], Remarks, Type AS [Type], tblITI.Status  " & _
                            " FROM     tblITI LEFT JOIN tblVCE_Master " & _
                            " ON	   tblITI .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "ITR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE ITR_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblITR.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, ITR_No AS [ITR No.], DateITR AS [Date], VCEName AS [VCEName], Remarks, Type AS [Type], tblITR.Status  " & _
                            " FROM     tblITR LEFT JOIN tblVCE_Master " & _
                            " ON	   tblITR .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "IT"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE IT_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblIT.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, IT_No AS [IT No.], DateIT AS [Date], Remarks, Type AS [Type], tblIT.Status  " & _
                            " FROM     tblIT " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "RI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE RI_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblRI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, RI_No AS [RI No.], DateRI AS [Date], Remarks, Type AS [Type], tblRI.Status  " & _
                            " FROM     tblRI " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "JV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND JV_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblJV.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblJV.DateJV BETWEEN  @DateFrom AND @DateTo"
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch,  JV_No AS [JV No.], DateJV AS [Date], VCEName as [Name], Remarks, Type AS [Type], tblJV.Status  " & _
                            " FROM     tblJV LEFT JOIN viewVCE_Master " & _
                            " ON	   tblJV.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN tblBranch  ON	           " & _
                            " tblJV.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblJV.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter & _
                            "  ORDER BY tblBranch.Description, JV_No, Date "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "JV-Template"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND isTemplate  = 'True' AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND isTemplate = 'True' AND JV_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND isTemplate = 'True' AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND isTemplate = 'True' AND tblJV.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND isTemplate = 'True' AND tblJV.DateJV BETWEEN  @DateFrom AND @DateTo"
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, TemplateName  " & _
                            " FROM     tblJV LEFT JOIN viewVCE_Master " & _
                            " ON	   tblJV.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN tblBranch  ON	           " & _
                            " tblJV.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblJV.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter & _
                            "  ORDER BY tblBranch.Description, JV_No "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "BB"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND BB_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblBB.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblBB.DateBB BETWEEN  @DateFrom AND @DateTo ORDER BY BB_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch,  BB_No AS [BB No.], DateBB AS [Date], VCEName as [Name], Remarks, Type AS [Type], tblBB.Status  " & _
                            " FROM     tblBB LEFT JOIN viewVCE_Master " & _
                            " ON	   tblBB.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN tblBranch  ON	           " & _
                            " tblBB.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblBB.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter & _
                            "  ORDER BY tblBranch.Description, BB_No, Date "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If


                Case "PJ"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND PJ_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblPJ.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblPJ.DatePJ BETWEEN  @DateFrom AND @DateTo ORDER BY PJ_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch,  PJ_No AS [PJ No.], DatePJ AS [Date], VCEName as [Name], Remarks, Type AS [Type], tblPJ.Status  " & _
                            " FROM     tblPJ LEFT JOIN viewVCE_Master " & _
                            " ON	   tblPJ.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN tblBranch  ON	           " & _
                            " tblPJ.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblPJ.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter & _
                            "  ORDER BY tblBranch.Description, PJ_No, Date "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "SJ"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND SJ_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblSJ.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblSJ.DateSJ BETWEEN  @DateFrom AND @DateTo "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch,  SJ_No AS [SJ No.], DateSJ AS [Date], VCEName as [Name], Remarks, Type AS [Type], tblSJ.Status  " & _
                            " FROM     tblSJ LEFT JOIN viewVCE_Master " & _
                            " ON	   tblSJ.VCECode = viewVCE_Master.VCECode " & _
                            " LEFT JOIN tblBranch  ON	           " & _
                            " tblSJ.BranchCode = tblBranch.BranchCode      " & _
                            " WHERE          " & _
                            " 	( tblSJ.BranchCode IN  " & _
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                            " 	  FROM      tblBranch   " & _
                            " 	  INNER JOIN tblUser_Access    ON          " & _
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " 	   WHERE     UserID ='" & UserID & "'" & _
                            " 	   ) " & _
                            "     )   " & filter & _
                            "  ORDER BY tblBranch.Description, SJ_No, Date "
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "SP"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE SP_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblSP.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, SP_No AS [SP No.], DateSP AS [Date], VCEName, NetAmount,  Remarks, Type AS [Type], tblSP.Status  " & _
                            " FROM     tblSP " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)




                Case "OR-SJ"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE SJ_No LIKE '%' + @Filter + '%'"
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT TransID, SJ_No as [SJ No], DateSJ as Date, VCECode, VCEName, Terms, DueDate, Remarks, Amount, Status  " & _
                            " FROM View_SJ_Balance " & filter & _
                            " ORDER BY TransID"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "OR-Receivables"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE Reference LIKE '%' + @Filter + '%'"
                            Case "VCE Code"
                                filter = " WHERE VCECode  =  @Filter "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT    Reference, AppDate as Date, Reference, VCECode, VCEName, Total, Payment, Balance, Remarks, Status  " & _
                            " FROM      view_AR_Balance " & filter & _
                            " ORDER BY  Date"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "APVCV-Payables"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE Reference LIKE '%' + @Filter + '%'"
                            Case "VCE Code"
                                filter = " WHERE VCECode  =  @Filter "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT    Reference, AppDate as Date, Reference, VCECode, VCEName, Total, Payment, Balance, Remarks, Status  " & _
                            " FROM      view_AP_Balance " & filter & _
                            " ORDER BY  Date"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "OR"
                    query = GetQueryCollection(moduleID)
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If
                Case "AR"
                    query = GetQueryCollection(moduleID)
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If
                Case "CR"
                    query = GetQueryCollection(moduleID)
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If
                Case "CSI"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE CSI_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblCSI.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblCSI.CSI_No AS [CSI No.], DateCSI AS [Date], VCEName AS [Customer], tblCSI.Remarks, SO_Ref AS [Reference SO],  tblCSI.Status" & _
                            " FROM     tblCSI LEFT JOIN viewVCE_Master " & _
                            " ON	   tblCSI.VCECode = viewVCE_Master.VCECode " & filter

                    'query = " SELECT   TransID, SI_No AS [SI No.], DateSI AS [Date], VCEName AS [Customer], Remarks, SO_Ref AS [Reference SO], tblSI.Status  " & _
                    '        " FROM     tblSI LEFT JOIN tblVCE_Master " & _
                    '        " ON	   tblSI .VCECode = tblVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "PR"
                    query = GetQueryCollection(moduleID)
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "CF"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE CF_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblCF.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblCF.TransID, CF_No AS [CF No.], DateCF AS [Date],  TotalAmount,  tblCF.Remarks, DateNeeded, RequestedBy, tblCF.Status  " & _
                            " FROM     tblCF  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                    'Case "PCV"
                    '    ' CONDITION OF QUERY
                    '    If cbFilter.SelectedIndex = -1 Then
                    '        filter = " WHERE '' = ''"
                    '    Else
                    '        Select Case cbFilter.SelectedItem
                    '            Case "Transaction ID"
                    '                filter = " WHERE PCV_No LIKE '%' + @Filter + '%' "
                    '            Case "VCE Name"
                    '                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                    '            Case "Remarks"
                    '                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                    '            Case "Status"
                    '                filter = " WHERE tblPCV.Status LIKE '%' + @Filter + '%' "
                    '        End Select
                    '    End If

                    '    ' QUERY 
                    '    query = " SELECT  TransID, PCV_No AS [PCV No],  tblPCV.VCECode, VCEName, DatePCV, Amount, Remarks,    tblPCV.Status  " & _
                    '    " FROM tblPCV" & _
                    '     " LEFT JOIN tblVCE_Master  ON  " & _
                    '     " tblPCV.VCECode = tblVCE_Master.VCECode " & filter
                    '    SQL.FlushParams()
                    '    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PCV_Load"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PCV_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblPCV.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " WHERE tblPCV.DatePCV BETWEEN  @DateFrom AND @DateTo ORDER BY PCV_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT  TransID, PCV_No AS [PCV No],  tblPCV.VCECode, VCEName, DatePCV, Amount, Remarks,    tblPCV.Status  " & _
                    " FROM tblPCV" & _
                     " LEFT JOIN viewVCE_Master  ON  " & _
                     " tblPCV.VCECode = viewVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If



                Case "Sub CF"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE CF_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblCF.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblCF.TransID, SupplierCode, VCEName, CF_No AS [CF No.], DateCF AS [Date], CFdetials.TotalAmount, [No. of Items],  tblCF.Remarks,  tblCF.Status  " & _
                            " FROM     tblCF INNER JOIN  " & _
                            " ( " & _
                            "     SELECT  TransID, CASE WHEN ApproveSP ='Supplier 1' THEN S1code " & _
                            "                           WHEN ApproveSP ='Supplier 2' THEN S2code " & _
                            "                           WHEN ApproveSP ='Supplier 3' THEN S3code " & _
                            "                           WHEN ApproveSP ='Supplier 4' THEN S4code " & _
                            "                      END AS SupplierCode, " & _
                            "             SUM(TotalAmount) AS TotalAmount, COUNT(TotalAmount) AS [No. of Items] " & _
                            " FROM tblCF_Details " & _
                            " WHERE Status ='Active' " & _
                            " GROUP BY TransID, CASE WHEN ApproveSP ='Supplier 1' THEN S1code " & _
                            "                           WHEN ApproveSP ='Supplier 2' THEN S2code " & _
                            "                           WHEN ApproveSP ='Supplier 3' THEN S3code " & _
                            "                           WHEN ApproveSP ='Supplier 4' THEN S4code " & _
                            "                      END " & _
                            " ) AS CFdetials " & _
                            " ON        tblCF.TransID = CFdetials.TransID " & _
                            " LEFT JOIN tblVCE_Master " & _
                            " ON tblVCE_Master.VCECode = CFdetials.SupplierCode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "Sub PO"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE PO_No LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblPO.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT   tblPO.TransID, POdetials.VCECode, VCEName, PO_No AS [PO No.], DatePO AS [Date], POdetials.TotalAmount, [No. of Items],  tblPO.Remarks,  tblPO.Status   " & _
                            "  FROM     tblPO INNER JOIN   " & _
                            "  (  " & _
                            "      SELECT  TransID, VCECode, " & _
                            "              SUM(NetAmount) AS TotalAmount, COUNT(NetAmount) AS [No. of Items]  " & _
                            "  FROM tblPO_Details  " & _
                            "  WHERE Status ='Active'  " & _
                            "  GROUP BY TransID, VCECode " & _
                            "  ) AS POdetials  " & _
                            "  ON        tblPO.TransID = POdetials.TransID  " & _
                            "  LEFT JOIN tblVCE_Master  " & _
                            "  ON tblVCE_Master.VCECode = POdetials.VCECode  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "Copy Member"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Member ID"
                                filter = " AND Member_ID LIKE '%' + @Filter + '%' "
                            Case "Full Name"
                                filter = " AND Full_Name LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblMember_Master.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT Member_ID, Full_Name,Member_Type, tblMember_Master.BranchCode, Description as BranchName from tblMember_Master   " & _
                            "  INNER JOIN tblBranch ON   " & _
                            "  tblBranch.BranchCode = tblMember_Master.BranchCode  " & _
                            " INNER JOIN tblUser_Access    ON   " & _
                            " tblMember_Master.BranchCode = tblUser_Access.Code    " & _
                            " AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                            " AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                            " WHERE     UserID ='" & UserID & "'  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    'POS
                Case "POS-CASH"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem

                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT ID, zCounter AS  Counter, TerminalID AS Terminal, Date, CashPayment FROM [ONYX_01].dbo.viewPOS_Sales  " & _
                            "     WHERE  PaymentType = 'Cash' AND ID NOT IN (SELECT POS_Ref From tblCSI WHERE TransType = 'Cash' AND Status <> 'Cancelled') "
                    SQL.FlushParams()
                    'POS
                Case "POS-CHARGE"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem

                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT ID, zCounter AS  Counter, TerminalID AS Terminal, Date, ChargePayment FROM [ONYX_01].dbo.viewPOS_Sales  " & _
                            "     WHERE  PaymentType = 'Charge' AND ID NOT IN (SELECT POS_Ref From tblCSI WHERE TransType = 'Charge' AND Status <> 'Cancelled') "
                    SQL.FlushParams()


                Case "LC"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE LC_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblLC.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblLC.DateLC BETWEEN  @DateFrom AND @DateTo ORDER BY DR_No "

                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, LC_No AS [LC No.], DateLC AS [Date], VCEName AS [Customer], Description AS Property, Remarks, tblLC.Status  " &
                            " FROM     tblLC LEFT JOIN viewVCE_Master " &
                            " ON	   tblLC.VCECode = viewVCE_Master.VCECode " &
                            " LEFT JOIN tblLeaseProperty " &
                            " ON	   tblLC.PropCode = tblLeaseProperty.PropCode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

                Case "LC-OR"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND LC_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " "
                            Case "Status"
                                filter = " "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   RowID, LC_No AS [LC No.], VCEName, PeriodFrom, PeriodTo, [Amount]  " &
                            " FROM     viewLC_ForOR " &
                            " WHERE    1=1" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                Case "LPM"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND PropCode LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " "
                            Case "Remarks"
                                filter = " "
                            Case "Status"
                                filter = " AND tblLeaseProperty.Status LIKE '%' + @Filter + '%'  "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   PropCode, Description, PropType AS [Type], tblLeaseProperty.Status  " &
                            " FROM     tblLeaseProperty " &
                            " WHERE    1=1 " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "LC-BS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND LC_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblLC.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, LC_No AS [LC No.], DateLC AS [Date], VCEName AS [Lesee], RatePerMonth AS [Amount], Remarks,  tblLC.Status  " &
                            " FROM     tblLC LEFT JOIN viewVCE_Master " &
                            " ON	   tblLC .VCECode = viewVCE_Master.VCECode " &
                            " WHERE    1=1" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "BS-Receivables"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE Reference LIKE '%' + @Filter + '%'"
                            Case "VCE Code"
                                filter = " WHERE VCECode  =  @Filter "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT    Reference, AppDate as Date, Reference, VCECode, VCEName, Total, Payment, Balance, Remarks, Status  " &
                            " FROM      view_BS_Balance " & filter &
                            " ORDER BY  Date"
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "RE"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE RE_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " WHERE tblRE.Status LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblRE.DateRE BETWEEN  @DateFrom AND @DateTo ORDER BY DR_No "

                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, RE_No AS [RE No.], DateRE AS [Date], VCEName AS [Customer],  Model + ' Blk ' + Unit_Blk  + ' Lot ' + Unit_Lot + ' ' + Project AS Property, tblRE.Remarks, tblRE.Status  " &
                            " FROM     tblRE LEFT JOIN viewVCE_Master " &
                            " ON	   tblRE.VCECode = viewVCE_Master.VCECode " &
                            " LEFT JOIN tblSaleProperty " &
                            " ON	   tblRE.PropCode = tblSaleProperty.UnitCode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If



                Case "REP"
                    chkBatch.Visible = True
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND UnitCode LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND Project LIKE '%' + @Filter + '%'"
                            Case "Remarks"
                                filter = " "
                            Case "Status"
                                filter = " AND Status LIKE '%' + @Filter + '%'  "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   UnitCode, UnitCode as [Unit Code] ,Unit_No, Unit_Phase, Project, UnitType AS [Type], ContractPrice as [Contract Price], Status  " &
                            " FROM     tblSaleProperty " &
                            " WHERE    1=1 " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "RE-Reservation"
                    chkBatch.Visible = True
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblRE.RE_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND viewVCE_Master.VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND tblRE.Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblRE.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblRE.TransID, tblRE.RE_No AS [RE No.], DateRE AS [Date], VCEName AS [Lesee], TCPAmount AS [TCP Amount], " &
                            "          viewRE_Reservation.Balance AS  [Reservation Amount], Remarks,  tblRE.Status  " &
                            " FROM     tblRE INNER JOIN viewRE_Reservation " &
                            " ON	   tblRE.TransID = viewRE_Reservation.TransID " &
                            " LEFT JOIN viewVCE_Master " &
                            " ON	   tblRE.VCECode = viewVCE_Master.VCECode " &
                            " WHERE    viewRE_Reservation.Balance > 0  " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "RE-Collection"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " And '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND RE_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " "
                            Case "Status"
                                filter = " "
                        End Select
                    End If

                    'query = " 	 SELECT Top 100 ID, RE_No AS [RE No.], VCEName, Description, DueDate, Principal - AmountPaid  AS [Amount], Penalty, Project, BLK,  LOT  " & vbCrLf &
                    '        "  	FROM  " & vbCrLf &
                    '        "  	(  " & vbCrLf &
                    '        "  	SELECT ROW_NUMBER() OVER (ORDER BY tblRE.TransID, DueDate) AS ID, tblRE.TransID, RE_No, tblRE.VCECode, VCEName, Description, DueDate,  " & vbCrLf &
                    '        "  			Principal,   " & vbCrLf &
                    '        "  			CASE WHEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END))  > 7   " & vbCrLf &
                    '        "  				 THEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END)) * ((5/100.0)/30.0) * Principal  " & vbCrLf &
                    '        "  				 ELSE 0  " & vbCrLf &
                    '        "  			END AS Penalty,   " & vbCrLf &
                    '        "  			-- 5/100 (5%) Interest Rate Per Month  " & vbCrLf &
                    '        "  			-- 7 - Grace Period  " & vbCrLf &
                    '        "  			tblRE.VATable, tblRE.VATInc, Num AS PaymentNo,  " & vbCrLf &
                    '        "  			ISNULL(Collection.AmountPaid,0) AS AmountPaid, ISNULL(Collection.RefType + ':' + Collection.TransNo,'') AS PaymentReference,  " & vbCrLf &
                    '        "  			Collection.AppDate AS PaymentDate,  " & vbCrLf &
                    '        "  			SUM(Principal - ISNULL(AmountPaid, 0)) OVER(PARTITION BY tblRE.TransID, RE_No ORDER BY DueDate) AS Balance , Project, 'BLK' + Unit_Blk AS BLK, 'LOT' + Unit_Lot AS LOT " & vbCrLf &
                    '        "  	FROM	tblRE INNER JOIN (SELECT TransID, Principal, DueDate, Description, Num FROM viewRE_Schedule) AS viewRE_Schedule  " & vbCrLf &
                    '        "  	ON		tblRE.TransID = viewRE_Schedule.TransID  " & vbCrLf &
                    '        "  	INNER JOIN viewVCE_Master  " & vbCrLf &
                    '        "  	ON		tblRe.VCECode = viewVCE_Master.VCECode  " & vbCrLf &
                    '        "   INNER JOIN tblSaleProperty   ON " & vbCrLf &
                    '        "   tblSaleProperty.UnitCode = tblRE.PropCode " & vbCrLf &
                    '        "  	LEFT JOIN  " & vbCrLf &
                    '        "  	(  " & vbCrLf &
                    '        "  		SELECT AppDate, RefType, TransNo, SUM(Credit-Debit) AS AmountPaid,   " & vbCrLf &
                    '        "  						REPLACE(CAST(CASE WHEN RefNo LIKE '%-%' THEN  (LEFT(RefNo,CHARINDEX('-',RefNo,1)-1))  ELSE RefNo END AS nvarchar(50)),'RE:','') AS RefNo,   " & vbCrLf &
                    '        "  						CAST(CASE WHEN RefNo LIKE '%-%' THEN  (SUBSTRING(RefNo,CHARINDEX('-',RefNo,1)+1,LEN(RefNo)))  ELSE 1 END AS int) AS PaymentNo, tblRE.TransID AS RE_TransID " & vbCrLf &
                    '        "  		FROM view_GL  " & vbCrLf &
                    '        " 		INNER JOIN (SELECT RE_Equity FROM tblSystemSetup Union all select TAX_OV from tblSystemSetup) AS DEfaultAccount ON " & vbCrLf &
                    '        " 		DEfaultAccount.RE_Equity = view_GL.AccntCode " & vbCrLf &
                    '        " 		INNER JOIN tblRE ON " & vbCrLf &
                    '        " 		tblRE.RE_No = 	REPLACE(CAST(CASE WHEN RefNo LIKE '%-%' THEN  (LEFT(RefNo,CHARINDEX('-',RefNo,1)-1))  ELSE RefNo END AS nvarchar(50)),'RE:','') " & vbCrLf &
                    '        " 		WHERE RefNo LIKE 'RE:%'   AND view_GL.Status <> 'Cancelled'  " & vbCrLf &
                    '        "  		GROUP BY AppDate, RefType, TransNo, RefNo , tblRE.TransID " & vbCrLf &
                    '        "  	) AS Collection  " & vbCrLf &
                    '        "  	ON  CAST( tblRe.RE_No AS nvarchar(50)) = CAST(RefNo  AS nvarchar(50)) " & vbCrLf &
                    '        "  	AND viewRE_Schedule.Num   = Collection.PaymentNo  " & vbCrLf &
                    '        " 	AND Collection.RE_TransID = viewRE_Schedule.TransID  " & vbCrLf &
                    '        "  	WHERE Description <> 'Reservation' AND DueDate <= @AsOfDate  " & vbCrLf &
                    '        "  	) AS A  " & vbCrLf &
                    '        "  	WHERE (Principal) - AmountPaid > 0    " & filter



                    'query = "  	 SELECT Top 100 ID, RE_No AS [RE No.], VCEName, Description, DueDate, Principal - AmountPaid  AS [Amount], Penalty, Project, BLK,  LOT, RE_SchedNum   " & vbCrLf &
                    '"   	FROM   " & vbCrLf &
                    '"   	(   " & vbCrLf &
                    '"   	SELECT ROW_NUMBER() OVER (ORDER BY tblRE.TransID, DueDate) AS ID, tblRE.TransID, RE_No, tblRE.VCECode, VCEName, Description, DueDate,   " & vbCrLf &
                    '"   			Principal,    " & vbCrLf &
                    '"   			CASE WHEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END))  > 7    " & vbCrLf &
                    '"   				 THEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END)) * ((5/100.0)/30.0) * Principal   " & vbCrLf &
                    '"   				 ELSE 0   " & vbCrLf &
                    '"   			END AS Penalty,    " & vbCrLf &
                    '"   			-- 5/100 (5%) Interest Rate Per Month   " & vbCrLf &
                    '"   			-- 7 - Grace Period   " & vbCrLf &
                    '"   			tblRE.VATable, tblRE.VATInc, Num AS PaymentNo,   " & vbCrLf &
                    '"   			ISNULL(Collection.AmountPaid,0) AS AmountPaid, ISNULL(Collection.RefType + ':' + Collection.TransNo,'') AS PaymentReference,   " & vbCrLf &
                    '"   			Collection.AppDate AS PaymentDate,   " & vbCrLf &
                    '"   			SUM(Principal - ISNULL(AmountPaid, 0)) OVER(PARTITION BY tblRE.TransID, RE_No ORDER BY DueDate) AS Balance , Project, 'BLK' + Unit_Blk AS BLK, 'LOT' + Unit_Lot AS LOT, viewRE_Schedule.Num as RE_SchedNum  " & vbCrLf &
                    '"   	FROM	tblRE INNER JOIN (SELECT TransID, Principal, DueDate, Description, Num FROM viewRE_Schedule) AS viewRE_Schedule   " & vbCrLf &
                    '"   	ON		tblRE.TransID = viewRE_Schedule.TransID   " & vbCrLf &
                    '"   	INNER JOIN viewVCE_Master   " & vbCrLf &
                    '"   	ON		tblRe.VCECode = viewVCE_Master.VCECode   " & vbCrLf &
                    '"    INNER JOIN tblSaleProperty   ON  " & vbCrLf &
                    '"    tblSaleProperty.UnitCode = tblRE.PropCode  " & vbCrLf &
                    '"   	LEFT JOIN   " & vbCrLf &
                    '"   	(   " & vbCrLf &
                    '"   			SELECT  " & vbCrLf &
                    '"     MIN(AppDate) AS AppDate,  " & vbCrLf &
                    '"     RefType,  " & vbCrLf &
                    '"     MIN(TransNo) AS TransNo,  " & vbCrLf &
                    '"     SUM(Credit - Debit) AS AmountPaid,    " & vbCrLf &
                    '"     REPLACE(CAST(CASE  " & vbCrLf &
                    '"                  WHEN RefNo LIKE '%-%'  " & vbCrLf &
                    '"                  THEN (LEFT(RefNo, CHARINDEX('-', RefNo, 1) - 1))   " & vbCrLf &
                    '"                  ELSE RefNo  " & vbCrLf &
                    '"                  END AS nvarchar(50)), 'RE:', '') AS RefNo,    " & vbCrLf &
                    '"     CAST(CASE  " & vbCrLf &
                    '"          WHEN RefNo LIKE '%-%'  " & vbCrLf &
                    '"          THEN (SUBSTRING(RefNo, CHARINDEX('-', RefNo, 1) + 1, LEN(RefNo)))   " & vbCrLf &
                    '"          ELSE 1  " & vbCrLf &
                    '"          END AS int) AS PaymentNo,  " & vbCrLf &
                    '"     tblRE.TransID AS RE_TransID " & vbCrLf &
                    '" FROM view_GL   " & vbCrLf &
                    '" INNER JOIN (SELECT RE_Equity FROM tblSystemSetup UNION ALL SELECT TAX_OV FROM tblSystemSetup) AS DefaultAccount  " & vbCrLf &
                    '"     ON DefaultAccount.RE_Equity = view_GL.AccntCode  " & vbCrLf &
                    '" INNER JOIN tblRE  " & vbCrLf &
                    '"     ON tblRE.RE_No = REPLACE(CAST(CASE  " & vbCrLf &
                    '"                                   WHEN RefNo LIKE '%-%'  " & vbCrLf &
                    '"                                   THEN (LEFT(RefNo, CHARINDEX('-', RefNo, 1) - 1))   " & vbCrLf &
                    '"                                   ELSE RefNo  " & vbCrLf &
                    '"                                   END AS nvarchar(50)), 'RE:', '')  " & vbCrLf &
                    '" WHERE RefNo LIKE 'RE:%'   " & vbCrLf &
                    '"   AND view_GL.Status <> 'Cancelled' " & vbCrLf &
                    '" GROUP BY RefType, RefNo, tblRE.TransID " & vbCrLf &
                    '"   	) AS Collection   " & vbCrLf &
                    '"   	ON  CAST( tblRe.RE_No AS nvarchar(50)) = CAST(RefNo  AS nvarchar(50))  " & vbCrLf &
                    '"   	AND viewRE_Schedule.Num   = Collection.PaymentNo   " & vbCrLf &
                    '"  	AND Collection.RE_TransID = viewRE_Schedule.TransID   " & vbCrLf &
                    '"   	WHERE Description <> 'Reservation' AND DueDate <= @AsOfDate   " & vbCrLf &
                    '"   	) AS A   " & vbCrLf &
                    '"   	WHERE (Principal) - AmountPaid > 0     " & filter


                    'query = "  	 SELECT  ID, RE_No AS [RE No.], VCEName, Description, DueDate, Principal - AmountPaid  AS [Amount], Penalty, Project, BLK,  LOT, RE_SchedNum   " & vbCrLf &
                    '        "   	FROM   " & vbCrLf &
                    '        "   	(   " & vbCrLf &
                    '        "   	SELECT ROW_NUMBER() OVER (ORDER BY tblRE.TransID, DueDate) AS ID, tblRE.TransID, RE_No, tblRE.VCECode, VCEName, Description, DueDate,   " & vbCrLf &
                    '        "   			Principal,    " & vbCrLf &
                    '        "   			CASE WHEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END))  > 7    " & vbCrLf &
                    '        "   				 THEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END)) * ((5/100.0)/30.0) * Principal   " & vbCrLf &
                    '        "   				 ELSE 0   " & vbCrLf &
                    '        "   			END AS Penalty,    " & vbCrLf &
                    '        "   			-- 5/100 (5%) Interest Rate Per Month   " & vbCrLf &
                    '        "   			-- 7 - Grace Period   " & vbCrLf &
                    '        "   			tblRE.VATable, tblRE.VATInc, Num AS PaymentNo,   " & vbCrLf &
                    '        "   			ISNULL(Collection.AmountPaid,0) AS AmountPaid, ISNULL(Collection.RefType + ':' + Collection.TransNo,'') AS PaymentReference,   " & vbCrLf &
                    '        "   			Collection.AppDate AS PaymentDate,   " & vbCrLf &
                    '        "   			SUM(Principal - ISNULL(AmountPaid, 0)) OVER(PARTITION BY tblRE.TransID, RE_No ORDER BY DueDate) AS Balance , Project, 'BLK' + Unit_Blk AS BLK, 'LOT' + Unit_Lot AS LOT, viewRE_Schedule.Num as RE_SchedNum  " & vbCrLf &
                    '        "   	FROM	tblRE INNER JOIN (SELECT TransID, Principal, DueDate, Description, Num FROM viewRE_Schedule) AS viewRE_Schedule   " & vbCrLf &
                    '        "   	ON		tblRE.TransID = viewRE_Schedule.TransID   " & vbCrLf &
                    '        "   	INNER JOIN viewVCE_Master   " & vbCrLf &
                    '        "   	ON		tblRe.VCECode = viewVCE_Master.VCECode   " & vbCrLf &
                    '        "    INNER JOIN tblSaleProperty   ON  " & vbCrLf &
                    '        "    tblSaleProperty.UnitCode = tblRE.PropCode  " & vbCrLf &
                    '        "   	LEFT JOIN   " & vbCrLf &
                    '        "   	(   " & vbCrLf &
                    '        "   			SELECT  " & vbCrLf &
                    '        "     MIN(AppDate) AS AppDate,  " & vbCrLf &
                    '        "     RefType,  " & vbCrLf &
                    '        "     MIN(TransNo) AS TransNo,  " & vbCrLf &
                    '        "     SUM(Credit - Debit) AS AmountPaid,    " & vbCrLf &
                    '        "     REPLACE(CAST(CASE  " & vbCrLf &
                    '        "                  WHEN RefNo LIKE '%-%'  " & vbCrLf &
                    '        "                  THEN (LEFT(RefNo, CHARINDEX('-', RefNo, 1) - 1))   " & vbCrLf &
                    '        "                  ELSE RefNo  " & vbCrLf &
                    '        "                  END AS nvarchar(50)), 'RE:', '') AS RefNo,    " & vbCrLf &
                    '        "     CAST(CASE  " & vbCrLf &
                    '        "          WHEN RefNo LIKE '%-%'  " & vbCrLf &
                    '        "          THEN (SUBSTRING(RefNo, CHARINDEX('-', RefNo, 1) + 1, LEN(RefNo)))   " & vbCrLf &
                    '        "          ELSE 1  " & vbCrLf &
                    '        "          END AS int) AS PaymentNo,  " & vbCrLf &
                    '        "     tblRE.TransID AS RE_TransID " & vbCrLf &
                    '        " FROM view_GL   " & vbCrLf &
                    '        " INNER JOIN (SELECT RE_Equity FROM tblSystemSetup UNION ALL SELECT TAX_OV FROM tblSystemSetup) AS DefaultAccount  " & vbCrLf &
                    '        "     ON DefaultAccount.RE_Equity = view_GL.AccntCode  " & vbCrLf &
                    '        " INNER JOIN tblRE  " & vbCrLf &
                    '        "     ON tblRE.RE_No = REPLACE(CAST(CASE  " & vbCrLf &
                    '        "                                   WHEN RefNo LIKE '%-%'  " & vbCrLf &
                    '        "                                   THEN (LEFT(RefNo, CHARINDEX('-', RefNo, 1) - 1))   " & vbCrLf &
                    '        "                                   ELSE RefNo  " & vbCrLf &
                    '        "                                   END AS nvarchar(50)), 'RE:', '')  " & vbCrLf &
                    '        " WHERE RefNo LIKE 'RE:%'   " & vbCrLf &
                    '        "   AND view_GL.Status <> 'Cancelled' " & vbCrLf &
                    '        " GROUP BY RefType, RefNo, tblRE.TransID " & vbCrLf &
                    '        "   	) AS Collection   " & vbCrLf &
                    '        "   	ON  CAST( tblRe.RE_No AS nvarchar(50)) = CAST(RefNo  AS nvarchar(50))  " & vbCrLf &
                    '        "   	AND viewRE_Schedule.Num   = Collection.PaymentNo   " & vbCrLf &
                    '        "  	AND Collection.RE_TransID = viewRE_Schedule.TransID   " & vbCrLf &
                    '        "   	WHERE Description <> 'Reservation'  " & vbCrLf &
                    '        "   	) AS A   " & vbCrLf &
                    '        "   	WHERE (Principal) - AmountPaid > 0     " & filter

                    query = "   	   	 SELECT TOP 500  ID, RE_No AS [RE No.], VCEName, Description, DueDate, Principal - AmountPaid  AS [Amount], Penalty, Project, BLK,  LOT, RE_SchedNum    
    	                            FROM    
    	                            (    
    	                            SELECT ROW_NUMBER() OVER (ORDER BY tblRE.TransID, DueDate) AS ID, tblRE.TransID, RE_No, tblRE.VCECode, VCEName, Description, DueDate,    
    			                            Principal,     
    			                            CASE WHEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END))  > 7     
    				                             THEN DATEDIFF(Day,  DueDate, (CASE WHEN AppDate IS NULL THEN @AsOfDate ELSE AppDate END)) * ((5/100.0)/30.0) * Principal    
    				                             ELSE 0    
    			                            END AS Penalty,     
    			                            -- 5/100 (5%) Interest Rate Per Month    
    			                            -- 7 - Grace Period    
    			                            tblRE.VATable, tblRE.VATInc, Num AS PaymentNo,    
    			                            ISNULL(Collection.AmountPaid,0) AS AmountPaid, ISNULL(Collection.RefType + ':' + Collection.TransNo,'') AS PaymentReference,    
    			                            Collection.AppDate AS PaymentDate,    
    			                            SUM(Principal - ISNULL(AmountPaid, 0)) OVER(PARTITION BY tblRE.TransID, RE_No ORDER BY DueDate) AS Balance , Project, 'BLK' + Unit_Blk AS BLK, 'LOT' + Unit_Lot AS LOT, viewRE_Schedule.Num as RE_SchedNum   
    	                            FROM	tblRE INNER JOIN (SELECT TransID, Principal, DueDate, Description, Num FROM viewRE_Schedule) AS viewRE_Schedule    
    	                            ON		CASt(tblRE.TransID AS nvarchar(20)) = CAST(viewRE_Schedule.TransID AS nvarchar(20))    
    	                            INNER JOIN viewVCE_Master    
    	                            ON		CAST(tblRe.VCECode AS nvarchar(20)) = CAST(viewVCE_Master.VCECode AS nvarchar(50))    
                                 INNER JOIN tblSaleProperty   ON   
                                 CAST(tblSaleProperty.UnitCode AS nvarchar(20)) = CASt(tblRE.PropCode AS nvarchar(20))   
    	                            LEFT JOIN    
    	                            (    
    			                            SELECT   
                                  MIN(AppDate) AS AppDate,   
                                  RefType,   
                                  MIN(TransNo) AS TransNo,   
                                  SUM(Credit - Debit) AS AmountPaid,     
                                  REPLACE(CAST(CASE   
                                               WHEN RefNo LIKE '%-%'   
                                               THEN (LEFT(RefNo, CHARINDEX('-', RefNo, 1) - 1))    
                                               ELSE RefNo   
                                               END AS nvarchar(50)), 'RE:', '') AS RefNo,     
                                  CAST(CASE   
                                       WHEN RefNo LIKE '%-%'   
                                       THEN (SUBSTRING(RefNo, CHARINDEX('-', RefNo, 1) + 1, LEN(RefNo)))    
                                       ELSE 1   
                                       END AS int) AS PaymentNo,   
                                  tblRE.TransID AS RE_TransID  
                              FROM view_GL    
                              INNER JOIN (SELECT TOP 1 RE_Equity FROM tblSystemSetup UNION ALL SELECT TOP 1 TAX_OV FROM tblSystemSetup) AS DefaultAccount   
                                  ON CAST(DefaultAccount.RE_Equity AS nvarchar(50)) = CAST(view_GL.AccntCode AS nvarchar(50))   
                              INNER JOIN tblRE   
                                  ON CAST(tblRE.RE_No AS nvarchar(20)) = CAST(REPLACE(CAST(CASE   
                                                                WHEN RefNo LIKE '%-%'   
                                                                THEN (LEFT(RefNo, CHARINDEX('-', RefNo, 1) - 1))    
                                                                ELSE RefNo   
                                                                END AS nvarchar(50)), 'RE:', '') AS nvarchar(20))   
                              WHERE RefNo LIKE 'RE:%'    
                                AND view_GL.Status <> 'Cancelled'  
                              GROUP BY RefType, RefNo, tblRE.TransID  
    	                            ) AS Collection    
    	                            ON  CAST( tblRe.RE_No AS nvarchar(20)) = CAST(RefNo  AS nvarchar(20))   
    	                            AND CAST(viewRE_Schedule.Num AS nvarchar(20))   = CAST(Collection.PaymentNo AS nvarchar(50))   
   	                            AND CAST(Collection.RE_TransID AS nvarchar(50)) = CAST(viewRE_Schedule.TransID AS nvarchar(50))    
    	                            WHERE Description <> 'Reservation'   
    	                            ) AS A    
    	                            WHERE (Principal) - AmountPaid > 0  
                              " & filter


                    SQL.FlushParams()
                    If IsDate(p1) Then
                        SQL.AddParam("@AsOfDate", CDate(p1))
                    Else
                        SQL.AddParam("@AsOfDate", Date.Today)
                    End If
                    SQL.AddParam("@Filter", txtFilter.Text)


                Case "RE-Loan"
                    chkBatch.Visible = True
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND RE_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND tblRE.Status LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = "  SELECT   tblRE.TransID, tblRE.RE_No AS [RE No.], DateRE AS [Date], VCEName AS [Lesee], TCPAmount AS [TCP Amount],            " &
                            "  viewRE_LoanAmount.LoanAmount AS  [Loanable Amount], Remarks,  tblRE.Status    " &
                            "  FROM     tblRE INNER JOIN viewRE_LoanAmount  ON	   tblRE.TransID = viewRE_LoanAmount.TransID   " &
                            "  LEFT JOIN viewVCE_Master  ON	   tblRE.VCECode = viewVCE_Master.VCECode   " &
                            "  WHERE    viewRE_LoanAmount.LoanAmount > 0 " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "BM-CV"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND tblBM.BM_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND tblBM.Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND CASE WHEN viewBM_Balance.Ref_TransID IS NOT NULL THEN  'Active'  WHEN tblBM.Status ='Active' THEN 'Closed' ELSE tblBM.Status   END LIKE '%' + @Filter + '%' "
                            Case "Date Range"
                                filter = " AND tblBM.DateBM BETWEEN  @DateFrom AND @DateTo ORDER BY tblBM.BM_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT    TransID, tblBranch.Description as Branch, tblBM.BM_No AS [BM No.], DateBM AS [Date], VCEName AS [Supplier], tblBM.Remarks, " &
                            "           CASE WHEN viewBM_Balance.Ref_TransID IS NOT NULL THEN  'Active' " &
                            "                WHEN tblBM.Status ='Active' THEN 'Closed' " &
                            "                ELSE tblBM.Status " &
                            "           END AS Status" &
                            " FROM      tblBM LEFT JOIN viewVCE_Master " &
                            " ON	    tblBM .VCECode = viewVCE_Master.VCECode " &
                            " LEFT JOIN tblBranch  	           " &
                            " ON        tblBM.BranchCode = tblBranch.BranchCode      " &
                            " LEFT JOIN viewBM_Balance " &
                            " ON        tblBM.TransID = viewBM_Balance.Ref_TransID " &
                            " WHERE     1=1    " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If





                Case "BM"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = '' ORDER BY BM_No"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND BM_No LIKE '%' + @Filter + '%' ORDER BY BM_No"
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' ORDER BY BM_No "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' ORDER BY BM_No "
                            Case "Status"
                                filter = " AND tblBM.Status LIKE '%' + @Filter + '%' ORDER BY BM_No "
                            Case "Date Range"
                                filter = " AND tblBM.DateBM BETWEEN  @DateFrom AND @DateTo ORDER BY BM_No "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   TransID, tblBranch.Description as Branch, BM_No AS [BM No.], DateBM AS [Date], VCEName AS [Supplier], TotalAmount AS Amount, Remarks, tblBM.Status  " &
                            " FROM     tblBM LEFT JOIN viewVCE_Master " &
                            " ON	   tblBM .VCECode = viewVCE_Master.VCECode " &
                             " LEFT JOIN tblBranch  ON	           " &
                            " tblBM.BranchCode = tblBranch.BranchCode      " &
                            " WHERE          " &
                            " 	( tblBM.BranchCode IN  " &
                            " 	  (SELECT    DISTINCT tblBranch.BranchCode  " &
                            " 	  FROM      tblBranch   " &
                            " 	  INNER JOIN tblUser_Access    ON          " &
                            " 	  tblBranch.BranchCode = tblUser_Access.Code   " &
                            " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " &
                            " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " &
                            " 	   WHERE     UserID ='" & UserID & "'" &
                            " 	   ) " &
                            "     )   " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If



                Case "BM-BS"
                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " AND '' = ''"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " AND BM_No LIKE '%' + @Filter + '%' "
                            Case "VCE Name"
                                filter = " AND VCEName LIKE '%' + @Filter + '%' "
                            Case "Remarks"
                                filter = " AND Remarks LIKE '%' + @Filter + '%' "
                            Case "Status"
                                filter = " AND viewBM_Status.ClientStatus LIKE '%' + @Filter + '%' "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblBM.TransID, tblBM.BM_No AS [Booking No.], DateBM AS [Date], VCEName AS [Client], Location, TotalAmount AS [Amount], Remarks,  viewBM_Status.Status  " &
                            " FROM     tblBM LEFT JOIN viewVCE_Master  " &
                            " ON	   tblBM.ClientCode = viewVCE_Master.VCECode " &
                            " LEFT JOIN viewBM_Status  " &
                            " ON	   tblBM.TransID = viewBM_Status.TransID " &
                            " WHERE    1=1" & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "IPL"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = '' ORDER BY TransID DESC"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE IPL_No LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                            Case "VCE Name"
                                filter = " ORDER BY TransID DESC "
                            Case "Remarks"
                                filter = " WHERE Description LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                            Case "Status"
                                filter = " WHERE tblIPL.Status LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tblIPL.TransID, IPL_No AS [IPL No.], EffectivityDate AS [ Effectivity Date], Description, Type,  tblIPL.Status  " &
                            " FROM     tblIPL " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)
                Case "PDC"

                    ' CONDITION OF QUERY
                    If cbFilter.SelectedIndex = -1 Then
                        filter = " WHERE '' = '' ORDER BY TransID DESC"
                    Else
                        Select Case cbFilter.SelectedItem
                            Case "Transaction ID"
                                filter = " WHERE Batch_No LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                            Case "VCE Name"
                                filter = " WHERE VCEName LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                            Case "Remarks"
                                filter = " WHERE Remarks LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                            Case "Status"
                                filter = " WHERE tblPDC.Status LIKE '%' + @Filter + '%' ORDER BY TransID DESC "
                        End Select
                    End If

                    ' QUERY 
                    query = " SELECT   tbLPDC.TransID, Batch_No AS [Batch No.], VCEName AS [ Client], Bank, Remarks,  tblPDC.Status  " &
                            " FROM     tbLPDC INNER JOIN viewVCE_Master " &
                            " ON       tbLPDC.VCECode = viewVCE_Master.VCECode " & filter
                    SQL.FlushParams()
                    SQL.AddParam("@Filter", txtFilter.Text)

                Case "PAYROLL-TP"
                    ' CONDITION OF QUERY
                    SetPayrollDatabase()
                    ' QUERY 
                    query = " SELECT Emp_ID AS [Employee ID], EmployeeName AS Name, SeparationID, Separation_Type AS Type, Separation_Date AS [Separation Date], Separation_Reason AS Reason " &
                            " FROM viewSeparatedEmployee LEFT JOIN " & database & ".dbo.tblTP AS TP " &
                            " ON	 viewSeparatedEmployee.Emp_ID = TP.EmpID " &
                            " WHERE (YearID >= 2022 OR Emp_ID ='2351') AND TP.Status IS NULL "
                    SQL_RUBY.FlushParams()

                    SQL_RUBY.GetQuery(query)
                    If SQL_RUBY.SQLDS.Tables(0).Rows.Count > 0 Then
                        dgvList.DataSource = SQL_RUBY.SQLDS.Tables(0)
                        dgvList.Columns(0).Visible = True

                        If chkBatch.Visible = True Then
                            If chkBatch.Checked = True Then
                                Dim colX As New DataGridViewCheckBoxColumn
                                colX.HeaderText = "Include"
                                colX.Name = "dgcInc"
                                colX.Width = 50
                                colX.DefaultCellStyle.NullValue = False
                                dgvList.Columns.Add(colX)
                                colX.DisplayIndex = 1
                            End If
                        End If
                    End If
                    query = ""

                Case "TP"
                    ' CONDITION OF QUERY
                    SetPayrollDatabase()
                    ' QUERY 
                    query = " SELECT  TransID, TP_No AS [TP No.], Emp_ID AS [Employee ID], EmployeeName AS Name, SeparationID, Separation_Type AS Type, Separation_Date AS [Separation Date], Separation_Reason AS Reason " &
                            " FROM viewSeparatedEmployee INNER JOIN " & database & ".dbo.tblTP AS TP " &
                            " ON	 viewSeparatedEmployee.Emp_ID = TP.EmpID " &
                            " WHERE (YearID >= 2022 OR Emp_ID ='2351')  "
                    SQL_RUBY.FlushParams()

                    SQL_RUBY.GetQuery(query)
                    If SQL_RUBY.SQLDS.Tables(0).Rows.Count > 0 Then
                        dgvList.DataSource = SQL_RUBY.SQLDS.Tables(0)
                        dgvList.Columns(0).Visible = True

                        If chkBatch.Visible = True Then
                            If chkBatch.Checked = True Then
                                Dim colX As New DataGridViewCheckBoxColumn
                                colX.HeaderText = "Include"
                                colX.Name = "dgcInc"
                                colX.Width = 50
                                colX.DefaultCellStyle.NullValue = False
                                dgvList.Columns.Add(colX)
                                colX.DisplayIndex = 1
                            End If
                        End If
                    End If
                    query = ""
                Case "TP-CV"
                    ' CONDITION OF QUERY
                    SetPayrollDatabase()
                    ' QUERY 
                    query = " SELECT   TransID, TP_No AS [TP No.], Emp_ID AS [Employee ID], EmployeeName AS Name, SeparationID, Separation_Type AS Type, Separation_Date AS [Separation Date], Separation_Reason AS Reason " &
                            " FROM     viewSeparatedEmployee INNER JOIN " & database & ".dbo.tblTP AS TP " &
                            " ON	   viewSeparatedEmployee.Emp_ID = TP.EmpID " &
                            " WHERE    Status ='Cleared' AND TotalAmount > 0  "
                    SQL_RUBY.FlushParams()

                    SQL_RUBY.GetQuery(query)
                    If SQL_RUBY.SQLDS.Tables(0).Rows.Count > 0 Then
                        dgvList.DataSource = SQL_RUBY.SQLDS.Tables(0)
                        dgvList.Columns(0).Visible = True

                        If chkBatch.Visible = True Then
                            If chkBatch.Checked = True Then
                                Dim colX As New DataGridViewCheckBoxColumn
                                colX.HeaderText = "Include"
                                colX.Name = "dgcInc"
                                colX.Width = 50
                                colX.DefaultCellStyle.NullValue = False
                                dgvList.Columns.Add(colX)
                                colX.DisplayIndex = 1
                            End If
                        End If
                    End If
                    query = ""

                Case "RE-COMM"
                    ' CONDITION OF QUERY
                    'If cbFilter.SelectedIndex = -1 Then
                    '    filter = " WHERE '' = ''"
                    'Else
                    '    Select Case cbFilter.SelectedItem
                    '        Case "Transaction ID"
                    '            filter = " WHERE CA_No LIKE '%' + @Filter + '%' "
                    '        Case "VCE Name"
                    '            filter = " WHERE View_CA_Balance.VCEName LIKE '%' + @Filter + '%' "
                    '        Case "Remarks"
                    '            filter = " WHERE View_CA_Balance.Remarks LIKE '%' + @Filter + '%' "
                    '        Case "Status"
                    '            'filter = " WHERE tblCA.Status LIKE '%' + @Filter + '%' "
                    '            filter = " WHERE View_CA_Balance.Status LIKE '%' + @Filter + '%' "
                    '        Case "Date Range"
                    '            filter = " WHERE tblCA.DateCA BETWEEN  @DateFrom AND @DateTo ORDER BY CA_No "
                    '    End Select
                    'End If

                    ' QUERY 
                    'query = " SELECT   TransID, CA_No AS [CA No.], DateCA AS [Date], VCEName AS [Name], Remarks,  tblCA.Status  " & _
                    '        " FROM     tblCA LEFT JOIN viewVCE_Master " & _
                    '        " ON	   tblCA .VCECode = viewVCE_Master.VCECode " & filter
                    query = "   SELECT TransID, RE_No AS [RE No.], REPLACE(CAST(DateRE as DATE),' 12:00:00 AM','') as [Date], SalesCode AS Code , viewVCE_Master.VCEName AS Name, ISNULL(Remarks, '') AS Remarks, CommissionAmount AS TotalAmount, tblRE.Status  " &
                            "   FROM tblRE   " &
                            " 	LEFT JOIN viewVCE_Master ON tblRE.SalesCode = viewVCE_Master.VCECode  " &
                            "   WHERE tblRE.COM_Release = '0' AND tblRE.Status = 'Active' " &
                            "   ORDER BY TransID "
                    SQL.FlushParams()
                    'SQL.AddParam("@Filter", txtFilter.Text)
                    If cbFilter.SelectedItem = "Date Range" Then
                        SQL.AddParam("@DateFrom", dtpDateFrom.Value)
                        SQL.AddParam("@DateTo", dtpDateTo.Value)
                    End If

            End Select
            If query <> "" Then
                SQL.GetQuery(query)
                If SQL.SQLDS.Tables(0).Rows.Count > 0 Then
                    dgvList.DataSource = SQL.SQLDS.Tables(0)
                    dgvList.Columns(0).Visible = False

                    If moduleID = "CV" Or moduleID = "OR" Or moduleID = "AR" Or moduleID = "CR" Or moduleID = "PR" Then
                        dgvList.Columns(5).DefaultCellStyle.Format = "N2"
                        dgvList.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    ElseIf moduleID = "User_Level" Then
                        dgvList.Columns(1).Width = 300
                    End If
                    If chkBatch.Visible = True Then
                        If chkBatch.Checked = True Then
                            Dim colX As New DataGridViewCheckBoxColumn
                            colX.HeaderText = "Include"
                            colX.Name = "dgcInc"
                            colX.Width = 50
                            colX.DefaultCellStyle.NullValue = False
                            dgvList.Columns.Add(colX)
                            colX.DisplayIndex = 1
                        End If
                    End If
                Else
                    dgvList.DataSource = SQL.SQLDS.Tables(0)
                    dgvList.Columns(0).Visible = False
                End If
            End If

        Catch ex As Exception
            SaveError(ex.Message, ex.StackTrace, Me.Name.ToString, moduleID)
        Finally
            SQL.FlushParams()
        End Try
    End Sub

    Private Function GetQueryCollection(ByVal Type As String) As String
        ' CONDITION OF QUERY
        Dim filter As String = ""
        Dim temp As String = ""
        If cbFilter.SelectedIndex = -1 Then
            filter = " ORDER BY TransNo ASC"
        Else
            Select Case cbFilter.SelectedItem
                Case "Transaction ID"
                    filter = " AND TransNo LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "Remarks"
                    filter = " AND Remarks LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "Status"
                    filter = " AND tblCollection.Status LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "VCE Name"
                    filter = " AND VCEName LIKE '%' + @Filter + '%' ORDER BY TransNo ASC"
                Case "Date Range"
                    filter = " AND tblCollection.DateTrans BETWEEN  @DateFrom AND @DateTo ORDER BY TransNo "
            End Select
        End If

        ' QUERY 
        temp = " SELECT   TransID, tblBranch.Description as Branch, TransNo AS [TransNo.], DateTrans AS [Date], VCEName AS [VCEName], Amount AS [Amount], Remarks, tblCollection.Status  " & _
                " FROM     tblCollection LEFT JOIN viewVCE_Master " & _
                " ON       tblCollection.VCECode = viewVCE_Master.VCECode " & _
                 " LEFT JOIN tblBranch  ON	           " & _
                " tblCollection.BranchCode = tblBranch.BranchCode      " & _
                " WHERE          " & _
                " 	( tblCollection.BranchCode IN  " & _
                " 	  (SELECT    DISTINCT tblBranch.BranchCode  " & _
                " 	  FROM      tblBranch   " & _
                " 	  INNER JOIN tblUser_Access    ON          " & _
                " 	  tblBranch.BranchCode = tblUser_Access.Code   " & _
                " 	   AND       tblUser_Access.Status ='Active' AND tblBranch.Status ='Active'   " & _
                " 	   AND       tblUser_Access.Type = 'BranchCode' AND isAllowed = 1  " & _
                " 	   WHERE     UserID ='" & UserID & "'" & _
                " 	   ) " & _
                "     )   " & _
                " AND    tblCollection.TransType ='" & Type & "'  " & filter
        Return temp
    End Function

    Private Sub dgvList_CellBeginEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvList.CellBeginEdit

    End Sub

    Private Sub dgvList_DoubleClick(sender As System.Object, e As System.EventArgs) Handles dgvList.DoubleClick
        ChooseRecord()
    End Sub


    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        ChooseRecord()
    End Sub

    Private Sub ChooseRecord()
        If dgvList.SelectedRows.Count = 1 Then
            transID = dgvList.SelectedRows(0).Cells(0).Value.ToString
            itemCode = dgvList.SelectedRows(0).Cells(1).Value.ToString
            If moduleID = "RE-Collection" Then
                Re_schednum = dgvList.SelectedRows(0).Cells(10).Value.ToString
            End If
            batch = chkBatch.Checked
            If cbBranch.Visible = True Then
                BranchCode = Strings.Left(cbBranch.SelectedItem, cbBranch.SelectedItem.ToString.IndexOf(" - "))
            End If
            If moduleID = "PCVRR" Then
                frmCV.LoadPCV(transID)
                Me.Close()
            End If
            Me.Close()
        End If
    End Sub

    Private Sub dgvList_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles dgvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            ChooseRecord()
        ElseIf e.KeyCode = Keys.Space Then
            If chkBatch.Checked = True Then
                If dgvList.SelectedRows.Count > 0 Then
                    If IsNothing(dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value) OrElse dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value = False Then
                        dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value = True
                    Else
                        dgvList.SelectedRows(0).Cells(dgvList.Columns.Count - 1).Value = False
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        LoadData()
    End Sub

    Private Sub txtFilter_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txtFilter.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    LoadData()
        'End If
    End Sub

    Private Sub frmLoadTransactions_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'If e.KeyCode = Keys.Escape Then
        '    Me.Close()
        'ElseIf e.KeyCode = Keys.Down Or e.KeyCode = Keys.Up Then
        '    ' CHANGE FOCUS TO DATAGRID SELECTION ON WHEN KEY DOWN OR KEY UP IS PRESSED
        '    Dim RowIndex As Integer = 0
        '    If dgvList.Focused = False Then
        '        If dgvList.SelectedRows.Count = 0 Then ' IF THERE ARE NO ROWS SELECTED THEN SELECT A DEFAUL ROW IF THERE ARE EXISTING ROW
        '            If dgvList.Rows.Count > 0 Then
        '                dgvList.Rows(0).Selected = True
        '            End If
        '        Else
        '            If e.KeyCode = Keys.Down Then
        '                If dgvList.Rows.Count > dgvList.SelectedRows(0).Index + 1 Then
        '                    dgvList.Focus()
        '                    RowIndex = dgvList.SelectedRows(0).Index
        '                    dgvList.Rows(dgvList.SelectedRows(0).Index).Selected = False
        '                    dgvList.Rows(RowIndex + 1).Selected = True
        '                End If
        '            ElseIf e.KeyCode = Keys.Up Then
        '                If dgvList.SelectedRows(0).Index > 0 Then
        '                    dgvList.Rows(dgvList.SelectedRows(0).Index - 1).Selected = True
        '                End If
        '            End If
        '        End If
        '        dgvList.Focus()
        '    End If
        'ElseIf e.KeyValue >= 112 AndAlso e.KeyValue <= 113 Then
        '    ' If Keypressed is between F1 to F12
        '    Dim index As Integer = e.KeyValue - 112
        '    If cbFilter.Items.Count >= (index + 1) Then
        '        cbFilter.SelectedIndex = index
        '    End If
        'Else
        '    txtFilter.Focus()
        '    txtFilter.SelectionStart = txtFilter.TextLength
        'End If
    End Sub

    Private Sub chkAll_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkAll.CheckedChanged
        If disableEvent = False Then
            For Each row As DataGridViewRow In dgvList.Rows
                row.Cells(dgvList.Columns.Count - 1).Value = chkAll.Checked
            Next
        End If
    End Sub
    Private Sub txtFilter_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtFilter.TextChanged
        'LoadData()
    End Sub

    Private Sub chkBatch_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkBatch.CheckedChanged
        chkAll.Visible = chkBatch.Checked
        If dgvList.Rows.Count > 0 Then
            If chkBatch.Checked = True Then
                Dim colX As New DataGridViewCheckBoxColumn
                colX.HeaderText = "Include"
                colX.Name = "dgcInc"
                colX.Width = 50
                colX.DefaultCellStyle.NullValue = False
                dgvList.Columns.Add(colX)
                colX.DisplayIndex = 1
            Else
                dgvList.Columns.RemoveAt(dgvList.Columns("dgcInc").Index)
                disableEvent = True
                chkAll.Checked = False
                disableEvent = False
            End If
        End If
    End Sub

    Private Sub dgvList_CellClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick
        If e.ColumnIndex >= 0 AndAlso e.RowIndex >= 0 Then
            If chkBatch.Checked = True Then
                If IsNothing(dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value) OrElse dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = False Then
                    dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = True
                Else
                    dgvList.Item(dgvList.Columns.Count - 1, e.RowIndex).Value = False
                End If
            End If
        End If
    End Sub
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub cbFilter_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbFilter.SelectedIndexChanged
        If cbFilter.SelectedItem = "Date Range" Then
            dtpDateFrom.Visible = True
            dtpDateTo.Visible = True
            txtFilter.Visible = False
        Else
            txtFilter.Visible = True
            dtpDateFrom.Visible = False
            dtpDateTo.Visible = False
        End If
    End Sub
    Private Sub LoadCC()
        Dim selectQuery As String
        selectQuery = " SELECT Code FROM tblCC "
        SQL.ReadQuery(selectQuery)
        cbProject.Items.Clear()
        cbProject.Items.Add("")
        While SQL.SQLDR.Read
            cbProject.Items.Add(SQL.SQLDR("Code"))
        End While
    End Sub
End Class