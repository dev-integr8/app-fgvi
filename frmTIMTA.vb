Imports Microsoft.Office.Interop

Public Class frmTIMTA
    Dim templateName As String = "TIMTA"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fileName As String = "TIMTA.xlsx"
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlApp = New Excel.Application
        Dim firstRow As Integer = 10
        Dim App_Path As String
        Dim ctr As Integer = 0
        Dim SQL As New SQLControl
        Dim query As String
        query = "spTIMTA"
        SQL.FlushParams()
        SQL.AddParam("@Year", NumericUpDown1.Value)
        SQL.GetQuery(query)
        Dim dt As DataTable = SQL.SQLDS.Tables(0)
        If dt.Rows.Count > 0 Then
            App_Path = New System.IO.FileInfo(Application.ExecutablePath).DirectoryName & "\Templates"
            If My.Computer.FileSystem.FileExists(App_Path + "\" & templateName & ".xlsx") Then
                xlWorkBook = xlApp.Workbooks.Open(App_Path + "\" & templateName & ".xlsx")
                xlWorkSheet = xlWorkBook.Worksheets("Template")
                For i As Integer = 0 To dt.Rows.Count - 1
                    For j As Integer = 0 To dt.Columns.Count - 1
                        xlWorkSheet.Cells(i + firstRow, j + 1) = dt.Rows(i).Item(j)
                    Next
                    ctr += 1
                    xlWorkSheet.Rows(firstRow + 1 + ctr).Insert
                Next

                xlWorkBook.SaveAs(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
                xlWorkBook.Close(Type.Missing, Type.Missing, Type.Missing)
                xlApp.Quit()

                ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet) : xlWorkSheet = Nothing
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook) : xlWorkBook = Nothing
            Else
                MsgBox("No Template found!" & vbNewLine & "Please contact your systems administrator", MsgBoxStyle.Exclamation)
            End If
        End If


        ' CLEAN UP. (CLOSE INSTANCES OF EXCEL OBJECTS.)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp) : xlApp = Nothing
        If My.Computer.FileSystem.FileExists(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName) Then
            Dim xls As Excel.Application
            Dim workbook As Excel.Workbook
            xls = New Excel.Application
            xls.Visible = True
            workbook = xls.Workbooks.Open(My.Computer.FileSystem.SpecialDirectories.Desktop & "\" & fileName)
        End If
    End Sub
End Class