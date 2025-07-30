Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Namespace FlexibleLabel
    Public Class FlexibleLabelControl
        Inherits Label

        Private _isSelected As Boolean = False
        Property isSelected As Boolean
            Get
                Return _isSelected
            End Get
            Set(value As Boolean)
                If value = True Then
                    Me.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
                    ShowResizeHandles()
                    Me.BringToFront()
                Else
                    Me.BorderStyle = Windows.Forms.BorderStyle.None
                    HideResizeHandles()
                End If
                _isSelected = value
            End Set
        End Property

        Public Overrides Property Text As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
                Dim size As Size = Me.Size
                Me.AutoSize = True
                Dim newWidth As Integer = Me.Width
                Me.AutoSize = False
                Me.Size = New Size(newWidth, size.Height)
            End Set
        End Property


        ''' <summary> Constuctor
        ''' </summary>
        Public Sub New()

            '' Initialize
            Me.AutoSize = False
            Me.BorderStyle = BorderStyle.None
            Me.Margin = New Padding(5, 5, 5, 5)
            Me.BackColor = Color.Transparent
            '' Add resize handles
            InitResizeHandles()

            '' Set drag move function
            SetMoveFunction()
        End Sub



        ''' <summary> Toggle IsSelected on MouseDown
        ''' </summary>
        ''' <param name="e"> Event Info </param>
        Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
            MyBase.OnMouseDown(e)
            ShowResizeHandles()
        End Sub

        ''' <summary> Text Paint event
        ''' </summary>
        ''' <param name="e"> Event Info </param>
        Protected Overrides Sub OnPaint(e As PaintEventArgs)
            Dim label As Label = CType(Me, Label)
            If (label.BorderStyle = BorderStyle.FixedSingle) Then
                Me.DrawBorder(e.Graphics)
            End If
            MyBase.OnPaint(e)
        End Sub

        ''' <summary> Background Paint event
        ''' </summary>
        ''' <param name="e"> Event Info </param>
        Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
            MyBase.OnPaintBackground(e)
        End Sub
        Protected Function BorderPen() As Pen
            Dim color As Color = IIf(Me.BackColor.GetBrightness() < 0.5, ControlPaint.Light(Me.BackColor), ControlPaint.Dark(Me.BackColor))
            Dim pen1 As New Pen(color)
            pen1.DashStyle = Drawing2D.DashStyle.Dot
            Return pen1
        End Function

        Protected Sub DrawBorder(graphics As Graphics)
            Dim label As Label = CType(Me, Label)
            If Not IsNothing(label) AndAlso label.Visible Then
                Dim borderPen As Pen = Me.BorderPen
                Dim clientRectangle As Rectangle = Me.ClientRectangle
                Dim num As Integer = clientRectangle.Width
                clientRectangle.Width = num - 1
                num = clientRectangle.Height
                clientRectangle.Height = num - 1
                graphics.DrawRectangle(borderPen, clientRectangle)
                borderPen.Dispose()
            End If
        End Sub


        ''' <summary> Allow Control to moved by mouse drag
        ''' </summary>
        Public isDragging As Boolean = False
        Public StartPoint As Point = Point.Empty
        Public Sub SetMoveFunction()
            AddHandler Me.MouseDown, AddressOf LabelMouseDown
            AddHandler Me.MouseUp, AddressOf LabelMouseUp
            AddHandler Me.MouseMove, AddressOf LabelMouseMove
        End Sub

        Private Sub LabelMouseDown(sender As Object, e As MouseEventArgs)
            If (e.Button = MouseButtons.Left) Then
                Me.Cursor = Cursors.SizeAll
                isDragging = True
                StartPoint = New Point(e.X, e.Y)
                Me.Capture = True
                isSelected = True
            End If
        End Sub

        Private Sub LabelMouseUp(sender As Object, e As MouseEventArgs)
            If (e.Button = MouseButtons.Left) Then
                Me.Cursor = Cursors.SizeAll
                isDragging = False
                Me.Capture = False
            End If
        End Sub

        Private Sub LabelMouseMove(sender As Object, e As MouseEventArgs)
            If (isDragging) Then
                Me.Cursor = Cursors.Arrow
                Me.Left = Math.Max(0, e.X + Me.Left - StartPoint.X)
                Me.Top = Math.Max(0, e.Y + Me.Top - StartPoint.Y)
            End If
        End Sub

#Region "Resize Methods"

        Protected Overrides Sub OnSizeChanged(e As EventArgs)
            MyBase.OnSizeChanged(e)

            lbNW.Location = New Point(0, 0)
            lbN.Location = New Point(Width / 2 - lbN.Width / 2 + 1, 0)
            lbNE.Location = New Point(Width - lbNE.Width, 0)
            lbE.Location = New Point(Width - lbE.Width, Height / 2 - lbE.Height / 2 + 1)
            lbSE.Location = New Point(Width - lbNE.Width, Height - lbSE.Height)
            lbS.Location = New Point(Width / 2 - lbN.Width / 2 + 1, Height - lbS.Height)
            lbSW.Location = New Point(0, Height - lbSW.Height)
            lbW.Location = New Point(0, Height / 2 - lbE.Height / 2 + 1)
        End Sub

        Protected Overrides Sub OnMouseEnter(e As EventArgs)
            MyBase.OnMouseEnter(e)
            Me.Cursor = Cursors.SizeAll
        End Sub

        Private lbNW As Label = New Label()
        Private lbN As Label = New Label()
        Private lbNE As Label = New Label()
        Private lbE As Label = New Label()
        Private lbSE As Label = New Label()
        Private lbS As Label = New Label()
        Private lbSW As Label = New Label()
        Private lbW As Label = New Label()

        Private isOnResize As Boolean = False
        Private labelName As String = String.Empty
        Private pointStart As Point = Point.Empty
        Private oriLabelLocation As Point = Point.Empty
        Private oriSize As Size = New Size()

        Private Sub InitResizeHandles()
            InitHandles(lbNW)
            lbNW.Name = "lbNW"

            InitHandles(lbN)
            lbN.Name = "lbN"

            InitHandles(lbNE)
            lbNE.Name = "lbNE"

            InitHandles(lbE)
            lbE.Name = "lbE"

            InitHandles(lbSE)
            lbSE.Name = "lbSE"

            InitHandles(lbS)
            lbS.Name = "lbS"

            InitHandles(lbSW)
            lbSW.Name = "lbSW"

            InitHandles(lbW)
            lbW.Name = "lbW"

            lbNW.Location = New Point(0, 0)
            lbN.Location = New Point(Width / 2 - lbN.Width / 2 + 1, 0)
            lbNE.Location = New Point(Width - lbNE.Width, 0)
            lbE.Location = New Point(Width - lbE.Width, Height / 2 - lbE.Height / 2 + 1)
            lbSE.Location = New Point(Width - lbNE.Width, Height - lbSE.Height)
            lbS.Location = New Point(Width / 2 - lbN.Width / 2 + 1, Height - lbS.Height)
            lbSW.Location = New Point(0, Height - lbSW.Height)
            lbW.Location = New Point(0, Height / 2 - lbE.Height / 2 + 1)
        End Sub

        Private Sub InitHandles(label As Label)
            label.Size = New Size(6, 6)
            label.BackColor = Color.Black
            label.Visible = False
            AddHandler label.MouseEnter, AddressOf label_MouseEnter
            AddHandler label.MouseDown, AddressOf label_MouseDown
            AddHandler label.MouseUp, AddressOf label_MouseUp
            AddHandler label.MouseMove, AddressOf label_MouseMove
            Me.Controls.Add(label)
        End Sub

        Private Sub label_MouseEnter(sender As Object, e As EventArgs)
            Dim name As String = (CType(sender, Label)).Name
            Select Case name
                Case "lbNW"
                    Me.Cursor = Cursors.SizeNWSE
                Case "lbN"
                    Me.Cursor = Cursors.SizeNS
                Case "lbNE"
                    Me.Cursor = Cursors.SizeNESW
                Case "lbE"
                    Me.Cursor = Cursors.SizeWE
                Case "lbSE"
                    Me.Cursor = Cursors.SizeNWSE
                Case "lbS"
                    Me.Cursor = Cursors.SizeNS
                Case "lbSW"
                    Me.Cursor = Cursors.SizeNESW
                Case "lbW"
                    Me.Cursor = Cursors.SizeWE
            End Select
        End Sub

        Private Sub label_MouseUp(sender As Object, e As MouseEventArgs)
            isOnResize = False
            CType(sender, Label).Capture = False
        End Sub

        Private Sub label_MouseDown(sender As Object, e As MouseEventArgs)
            If (e.Button = System.Windows.Forms.MouseButtons.Left) Then
                ShowResizeHandles()
                isOnResize = True
                pointStart = New Point(e.X, e.Y)
                oriLabelLocation = Parent.PointToClient(PointToScreen((CType(sender, Label)).Location))
                oriSize = Me.Size
                labelName = (CType(sender, Label)).Name
                CType(sender, Label).Capture = True
            End If
        End Sub

        Sub label_MouseMove(sender As Object, e As MouseEventArgs)
            If (isOnResize) Then
                Dim Size As Size = Me.Size
                Dim Location As Point = Point.Empty
                Dim Left As Integer = Me.Left
                Dim Top As Integer = Me.Top

                Dim X As Integer = e.X
                Dim Y As Integer = e.Y

                Select Case labelName
                    Case "lbNW"
                        Left = Math.Max(0, X + Me.Left - pointStart.X)
                        Top = Math.Max(0, Y + Me.Top - pointStart.Y)
                        Size = New Size(oriSize.Width - (Left - oriLabelLocation.X), oriSize.Height - (Top - oriLabelLocation.Y))
                    Case "lbSE"
                        Size = New Size(Me.Width + X, Me.Height + Y)
                    Case "lbN"
                        Top = Math.Max(0, Y + Me.Top - pointStart.Y)
                        Size = New Size(Me.Width, oriSize.Height - (Top - oriLabelLocation.Y))
                    Case "lbS"
                        Size = New Size(Me.Width, Me.Height + Y)
                    Case "lbNE"
                        Top = Math.Max(0, Y + Me.Top - pointStart.Y)
                        Size = New Size(Me.Width + X, oriSize.Height - (Top - oriLabelLocation.Y))
                    Case "lbSW"
                        Left = Math.Max(0, X + Me.Left - pointStart.X)
                        Size = New Size(oriSize.Width - (Left - oriLabelLocation.X), Me.Height + Y)
                    Case "lbE"
                        Size = New Size(Me.Width + X, Me.Height)
                    Case "lbW"
                        Left = Math.Max(0, X + Me.Left - pointStart.X)
                        Size = New Size(oriSize.Width - (Left - oriLabelLocation.X), Me.Height)
                End Select
                If (Size.Width < 17) Then
                    Left = Me.Left
                    Size = New Size(17, Size.Height)
                End If

                If (Size.Height < 17) Then
                    Top = Me.Top
                    Size = New Size(Size.Width, 17)
                End If

                If (Size.Width + Left > Parent.Width - 20) Then
                    Size = New Size(Parent.Width - Left - 20, Size.Height)

                End If

                If (Size.Height + Top > Parent.Height - 20) Then
                    Size = New Size(Size.Width, Parent.Height - Top - 20)
                End If

                Me.Left = Left
                Me.Top = Top

                Me.MaximumSize = Size
                Me.MinimumSize = Size
                Me.Size = Size
            End If
        End Sub

        Public Sub ShowResizeHandles()
            lbNW.Visible = True
            lbN.Visible = True
            lbNE.Visible = True
            lbE.Visible = True
            lbSE.Visible = True
            lbS.Visible = True
            lbSW.Visible = True
            lbW.Visible = True
        End Sub

        Public Sub HideResizeHandles()
            lbNW.Visible = False
            lbN.Visible = False
            lbNE.Visible = False
            lbE.Visible = False
            lbSE.Visible = False
            lbS.Visible = False
            lbSW.Visible = False
            lbW.Visible = False
        End Sub
#End Region

    End Class
End Namespace