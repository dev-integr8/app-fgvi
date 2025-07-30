Imports jade.FlexibleLabel
Imports CrystalDecisions.CrystalReports.Engine
Public Class frmCheck_Designer
    Public SelectedLabel As FlexibleLabelControl
    Dim templateID As Integer = 0
    Dim CR As New ReportDocument()

    Dim CheckName As ReportObject
    Dim InWords As ReportObject
    Dim CheckDate As ReportObject
    Dim Amount As ReportObject

    Dim lblName As New FlexibleLabelControl
    Dim lblInWords As New FlexibleLabelControl
    Dim lblDate As New FlexibleLabelControl
    Dim lblAmount As New FlexibleLabelControl

    Public Overloads Function ShowDialog(ByVal DocNumber As String) As Boolean
        templateID = DocNumber
        MyBase.ShowDialog()
        Return True
    End Function
    Private Sub frmCheckWriter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddFlexiLabels()

        If templateID <> 0 Then
            DisplayCheck(templateID)
        Else
            displayDefault()
        End If
    End Sub
    Private Sub AddFlexiLabels()
        lblName.Text = "JUAN DELA CRUZ"
        lblAmount.Text = "30,000.00"
        lblInWords.Text = "THIRTY THOUSAND PESOS ONLY"
        lblDate.Text = "DECEMBER 25, 2020"

        Panel1.Controls.Add(lblName)
        Panel1.Controls.Add(lblInWords)
        Panel1.Controls.Add(lblDate)
        Panel1.Controls.Add(lblAmount)

        AddClickEvent(Controls)
        AddHandler Me.MouseDown, AddressOf Everywhere_Click
        AddHandler lblAmount.MouseDown, AddressOf SelectLabel
        AddHandler lblDate.MouseDown, AddressOf SelectLabel
        AddHandler lblInWords.MouseDown, AddressOf SelectLabel
        AddHandler lblName.MouseDown, AddressOf SelectLabel
        AddHandler lblAmount.MouseMove, AddressOf ShowLocationProperty
        AddHandler lblDate.MouseMove, AddressOf ShowLocationProperty
        AddHandler lblInWords.MouseMove, AddressOf ShowLocationProperty
        AddHandler lblName.MouseMove, AddressOf ShowLocationProperty
        AddHandler lblAmount.SizeChanged, AddressOf ShowSizeProperty
        AddHandler lblDate.SizeChanged, AddressOf ShowSizeProperty
        AddHandler lblInWords.SizeChanged, AddressOf ShowSizeProperty
        AddHandler lblName.SizeChanged, AddressOf ShowSizeProperty
    End Sub
    Private Sub DisplayCheck(ByVal TemplateID As Integer)
        Dim query As String
        query = " SELECT    ID, TemplateName, Name_X, Name_Y, Name_W, Name_H, " & _
                "           InWords_X, InWords_Y, InWords_W, InWords_H, " & _
                "           Amount_X, Amount_Y, Amount_W, Amount_H, " & _
                "           Date_X, Date_Y, Date_W, Date_H " & _
                " FROM tblCheck_Template " & _
                " WHERE ID = @ID "
        SQL.FlushParams()
        SQL.AddParam("@ID", TemplateID)
        SQL.ReadQuery(query)
        If SQL.SQLDR.Read Then
            txtTemplateName.Text = SQL.SQLDR("TemplateName").ToString
            lblName.Left = SQL.SQLDR("Name_X")
            lblName.Top = SQL.SQLDR("Name_Y")
            lblName.Width = SQL.SQLDR("Name_W")
            lblName.Height = SQL.SQLDR("Name_H")
            lblInWords.Left = SQL.SQLDR("InWords_X")
            lblInWords.Top = SQL.SQLDR("InWords_Y")
            lblInWords.Width = SQL.SQLDR("InWords_W")
            lblInWords.Height = SQL.SQLDR("InWords_H")
            lblAmount.Left = SQL.SQLDR("Amount_X")
            lblAmount.Top = SQL.SQLDR("Amount_Y")
            lblAmount.Width = SQL.SQLDR("Amount_W")
            lblAmount.Height = SQL.SQLDR("Amount_H")
            lblDate.Left = SQL.SQLDR("Date_X")
            lblDate.Top = SQL.SQLDR("Date_Y")
            lblDate.Width = SQL.SQLDR("Date_W")
            lblDate.Height = SQL.SQLDR("Date_H")
        End If
    End Sub
    Public Sub displayDefault()
        Try
            Dim reportPath As String = App_Path & "\CR_Reports\" & database & "\Check_X.rpt"
            CR.Load(reportPath)
            CheckName = CR.ReportDefinition.ReportObjects("BPName1")
            InWords = CR.ReportDefinition.ReportObjects("Inwords1")
            CheckDate = CR.ReportDefinition.ReportObjects("CheckDate1")
            Amount = CR.ReportDefinition.ReportObjects("Amount1")

            ' NAME
            lblName.Left = CheckName.Left / CR_Factor
            lblName.Top = CheckName.Top / CR_Factor
            lblName.Width = CheckName.Width / CR_Factor

            ' AMOUNT IN WORDS
            lblInWords.Left = InWords.Left / CR_Factor
            lblInWords.Top = InWords.Top / CR_Factor
            lblInWords.Width = InWords.Width / CR_Factor

            ' DATE
            lblDate.Left = CheckDate.Left / CR_Factor
            lblDate.Top = CheckDate.Top / CR_Factor
            lblDate.Width = CheckDate.Width / CR_Factor

            ' AMOUNT
            lblAmount.Left = Amount.Left / CR_Factor
            lblAmount.Top = Amount.Top / CR_Factor
            lblAmount.Width = Amount.Width / CR_Factor


        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub SelectLabel(sender As Object, e As MouseEventArgs)
        If sender.isSelected Then
            SelectedLabel = sender
            txtX.Text = sender.Location.X
            txtY.Text = sender.Location.Y
            txtHeight.Text = sender.Height
            txtWidth.Text = sender.Width
        End If
    End Sub


    Private Sub ShowLocationProperty(sender As Object, e As MouseEventArgs)
        If sender.isSelected Then
            txtX.Text = sender.Location.X
            txtY.Text = sender.Location.Y
        End If
    End Sub

    Private Sub ShowSizeProperty(sender As Object, e As EventArgs)
        If sender Is SelectedLabel Then
            txtHeight.Text = sender.Height
            txtWidth.Text = sender.Width
        End If
    End Sub


    Private Sub AddClickEvent(Controls As Object)
        For Each c As Control In Controls
            If c.Controls.Count > 0 Then
                Try
                    If CType(c, FlexibleLabelControl).isSelected = CType(c, FlexibleLabelControl).isSelected Then

                    End If
                Catch ex As Exception
                    AddClickEvent(c.Controls)
                End Try
            End If
            If Not TypeOf c Is TextBox Then
                AddHandler c.MouseDown, AddressOf Everywhere_Click
            End If
        Next
    End Sub

    Private Sub Everywhere_Click(sender As Object, e As MouseEventArgs)
        SelectedLabel = Nothing
        txtX.Clear()
        txtY.Clear()
        txtHeight.Clear()
        txtWidth.Clear()
        For Each ctl As Control In Panel1.Controls
            Try
                If ctl.Text <> sender.Text Then
                    If CType(ctl, FlexibleLabelControl).isSelected Then
                        CType(ctl, FlexibleLabelControl).isSelected = False
                        CType(ctl, FlexibleLabelControl).isDragging = False
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub

    Private Sub txtX_KeyDown(sender As Object, e As KeyEventArgs) Handles txtX.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtX.Text) Then
                SelectedLabel.Left = txtX.Text
            Else
                txtX.Text = SelectedLabel.Left
            End If
        End If
    End Sub

    Private Sub txtY_KeyDown(sender As Object, e As KeyEventArgs) Handles txtY.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtY.Text) Then
                SelectedLabel.Top = txtY.Text
            Else
                txtY.Text = SelectedLabel.Top
            End If
        End If
    End Sub

    Private Sub txtWidth_KeyDown(sender As Object, e As KeyEventArgs) Handles txtWidth.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtWidth.Text) Then
                SelectedLabel.Width = txtWidth.Text
            Else
                txtWidth.Text = SelectedLabel.Width
            End If
        End If
    End Sub

    Private Sub txtHeight_KeyDown(sender As Object, e As KeyEventArgs) Handles txtHeight.KeyDown
        If e.KeyCode = Keys.Enter Then
            If IsNumeric(txtHeight.Text) Then
                SelectedLabel.Height = txtHeight.Text
            Else
                txtHeight.Text = SelectedLabel.Height
            End If
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim b As New Bitmap(Panel1.Width, Panel1.Height)
        Panel1.DrawToBitmap(b, Panel1.ClientRectangle)

        e.Graphics.DrawImage(b, New Point(0, 0))
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtTemplateName.Text = "" Then
            MsgBox("Please input name for this template", MsgBoxStyle.Exclamation)
        Else
            If templateID = 0 Then
                templateID = GenerateTransID("ID", "tblCheck_Template")
                Dim insertSQL As String
                insertSQL = " INSERT INTO  " & _
                            " tblCheck_Template(ID, TemplateName, Name_X, Name_Y, Name_W, Name_H, InWords_X, InWords_Y, InWords_W, InWords_H, Amount_X, Amount_Y, Amount_W, Amount_H, Date_X, Date_Y, Date_W, Date_H, Orientation, Status) " & _
                            " VALUES(@ID, @TemplateName, @Name_X, @Name_Y, @Name_W, @Name_H, @InWords_X, @InWords_Y, @InWords_W, @InWords_H, @Amount_X, @Amount_Y, @Amount_W, @Amount_H, @Date_X, @Date_Y, @Date_W, @Date_H, @Orientation,, @Status) "
                SQL.FlushParams()
                SQL.AddParam("@ID", templateID)
                SQL.AddParam("@TemplateName", txtTemplateName.Text)
                SQL.AddParam("@Name_X", lblName.Left)
                SQL.AddParam("@Name_Y", lblName.Top)
                SQL.AddParam("@Name_W", lblName.Width)
                SQL.AddParam("@Name_H", lblName.Height)
                SQL.AddParam("@InWords_X", lblInWords.Left)
                SQL.AddParam("@InWords_Y", lblInWords.Top)
                SQL.AddParam("@InWords_W", lblInWords.Width)
                SQL.AddParam("@InWords_H", lblInWords.Height)
                SQL.AddParam("@Amount_X", lblAmount.Left)
                SQL.AddParam("@Amount_Y", lblAmount.Top)
                SQL.AddParam("@Amount_W", lblAmount.Width)
                SQL.AddParam("@Amount_H", lblAmount.Height)
                SQL.AddParam("@Date_X", lblDate.Left)
                SQL.AddParam("@Date_Y", lblDate.Top)
                SQL.AddParam("@Date_W", lblDate.Width)
                SQL.AddParam("@Date_H", lblDate.Height)
                SQL.AddParam("@Status", "Active")
                SQL.ExecNonQuery(insertSQL)
            Else
                Dim updateSQL As String
                updateSQL = " UPDATE tblCheck_Template " & _
                            " SET    TemplateName = @TemplateName, " & _
                            "        Name_X = @Name_X, Name_Y = @Name_Y, Name_W =@Name_W, Name_H = @Name_H, " & _
                            "        InWords_X = @InWords_X, InWords_Y = @InWords_Y, InWords_W = @InWords_W, InWords_H = @InWords_H, " & _
                            "        Amount_X = @Amount_X, Amount_Y = @Amount_Y, Amount_W = @Amount_W, Amount_H = @Amount_H, " & _
                            "        Date_X = @Date_X, Date_Y = @Date_Y, Date_W = @Date_W, Date_H = @Date_H " & _
                            " WHERE ID = @ID  "
                SQL.FlushParams()
                SQL.AddParam("@ID", templateID)
                SQL.AddParam("@TemplateName", txtTemplateName.Text)
                SQL.AddParam("@Name_X", lblName.Left)
                SQL.AddParam("@Name_Y", lblName.Top)
                SQL.AddParam("@Name_W", lblName.Width)
                SQL.AddParam("@Name_H", lblName.Height)
                SQL.AddParam("@InWords_X", lblInWords.Left)
                SQL.AddParam("@InWords_Y", lblInWords.Top)
                SQL.AddParam("@InWords_W", lblInWords.Width)
                SQL.AddParam("@InWords_H", lblInWords.Height)
                SQL.AddParam("@Amount_X", lblAmount.Left)
                SQL.AddParam("@Amount_Y", lblAmount.Top)
                SQL.AddParam("@Amount_W", lblAmount.Width)
                SQL.AddParam("@Amount_H", lblAmount.Height)
                SQL.AddParam("@Date_X", lblDate.Left)
                SQL.AddParam("@Date_Y", lblDate.Top)
                SQL.AddParam("@Date_W", lblDate.Width)
                SQL.AddParam("@Date_H", lblDate.Height)
                SQL.ExecNonQuery(updateSQL)
            End If
            MsgBox("Changes Saved Successfully!", MsgBoxStyle.Information)
            Me.Close()
        End If
    End Sub
End Class