Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D

<DefaultEvent("_TextChanged")>
Public Class ProjektTextBox 'ADD ALL RELEVANT EVENTS
    Inherits UserControl

    'Fields
    Private _borderColor As Color = Color.Crimson
    Private _borderFocusColor As Color = Color.DarkRed
    Private _borderSize As Integer = 2
    Private _borderRadius As Integer = 0
    Private _underlineStyle As Boolean = False
    Private _isFocused As Boolean = False
    Private _placeHolderColor As Color = Color.DarkSlateGray
    Private _placeHolderText As String = "PROJEKT"
    Private _isPlaceHolder As Boolean = False
    Private _isPasswordChar As Boolean = False

    'Events
    Public Event _TextChanged As EventHandler
    Friend WithEvents TextBox1 As TextBox

    <Category("Projekt")>
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(value As Integer)
            _borderSize = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property UnderlineStyle As Boolean
        Get
            Return _underlineStyle
        End Get
        Set(value As Boolean)
            _underlineStyle = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property PasswordChar As Boolean
        Get
            Return _isPasswordChar
        End Get
        Set(value As Boolean)
            IsPasswordChar = value
            TextBox1.UseSystemPasswordChar = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property MultiLine As Boolean
        Get
            Return TextBox1.Multiline
        End Get
        Set(value As Boolean)
            TextBox1.Multiline = value
        End Set
    End Property
    <Category("Projekt")>
    Public Overrides Property BackColor() As Color
        Get
            Return MyBase.BackColor
        End Get
        Set(ByVal value As Color)
            MyBase.BackColor = value
            TextBox1.BackColor = value
        End Set
    End Property
    <Category("Projekt")>
    Public Overrides Property ForeColor() As Color
        Get
            Return MyBase.ForeColor
        End Get
        Set(ByVal value As Color)
            MyBase.ForeColor = value
            TextBox1.ForeColor = value
        End Set
    End Property
    <Category("Projekt")>
    Public Overrides Property Font() As Font
        Get
            Return MyBase.Font
        End Get
        Set(ByVal value As Font)
            MyBase.Font = value
            TextBox1.Font = value
            If Me.DesignMode Then
                updateControlHeight()
            End If
        End Set
    End Property
    <Category("Projekt")>
    Public Property _Text() As String
        Get
            If IsPlaceHolder Then
                Return String.Empty
            Else
                Return TextBox1.Text
            End If
        End Get
        Set(ByVal value As String)
            TextBox1.Text = value
            SetPlaceHolder()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderFocusColor As Color
        Get
            Return _borderFocusColor
        End Get
        Set(value As Color)
            _borderFocusColor = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderRadius As Integer
        Get
            Return _borderRadius
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _borderRadius = value
                Me.Invalidate()
            End If

        End Set
    End Property
    <Category("Projekt")>
    Public Property PlaceHolderColor As Color
        Get
            Return _placeHolderColor
        End Get
        Set(value As Color)
            _placeHolderColor = value
            If IsPasswordChar Then
                TextBox1.ForeColor = value
            End If
        End Set
    End Property
    <Category("Projekt")>
    Public Property PlaceHolderText As String
        Get
            Return _placeHolderText
        End Get
        Set(value As String)
            _placeHolderText = value
            TextBox1.Text = ""
            SetPlaceHolder()
        End Set
    End Property
    <Category("Projekt")>
    Public Property IsPlaceHolder As Boolean
        Get
            Return _isPlaceHolder
        End Get
        Set(value As Boolean)
            _isPlaceHolder = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property IsPasswordChar As Boolean
        Get
            Return _isPasswordChar
        End Get
        Set(value As Boolean)
            _isPasswordChar = value
        End Set
    End Property


    'Methods
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics

        If _borderRadius > 1 Then 'Rounded TextBox

            Dim rectBorderSmooth = Me.ClientRectangle
            Dim rectBorder = Rectangle.Inflate(rectBorderSmooth, -BorderSize, -BorderSize)
            Dim smoothSize As Integer = If(BorderSize > 0, BorderSize, 1)

            If BorderRadius <= BorderSize Then
                BorderRadius *= 2
            End If

            Using pathBorderSmooth As GraphicsPath = GetFigurePath(rectBorderSmooth, BorderRadius)
                Using pathBorder As GraphicsPath = GetFigurePath(rectBorder, BorderRadius - BorderSize)
                    Using penBorderSmooth As New Pen(Me.Parent.BackColor, smoothSize)
                        Using penBorder As New Pen(BorderColor, BorderSize)
                            Me.Region = New Region(pathBorderSmooth)
                            g.SmoothingMode = SmoothingMode.AntiAlias
                            penBorder.Alignment = PenAlignment.Center
                            If BorderRadius > 15 Then
                                SetTextBoxRoundedRegion() 'Set the rounded region of textbox
                            End If
                            If _isFocused Then
                                penBorder.Color = _borderFocusColor
                            End If

                            If _underlineStyle Then 'Line style
                                g.DrawPath(penBorderSmooth, pathBorderSmooth)
                                g.SmoothingMode = SmoothingMode.None
                                g.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                            Else 'Normal style
                                g.DrawPath(penBorderSmooth, pathBorderSmooth)
                                g.DrawPath(penBorder, pathBorder)
                            End If


                        End Using
                    End Using
                End Using
            End Using

        Else 'Normal TextBox

            Using penBorder As New Pen(BorderColor, BorderSize)
                penBorder.Alignment = PenAlignment.Center
                Me.Region = New Region(Me.ClientRectangle)

                If Not _isFocused Then
                    If _underlineStyle Then 'Line style
                        g.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                    Else 'Normal style
                        g.DrawRectangle(penBorder, 0, 0, Me.Width - 0.5F, Me.Height - 0.5F)
                    End If
                Else
                    penBorder.Color = _borderFocusColor
                    If _underlineStyle Then 'Line style
                        g.DrawLine(penBorder, 0, Me.Height - 1, Me.Width, Me.Height - 1)
                    Else 'Normal style
                        g.DrawRectangle(penBorder, 0, 0, Me.Width - 0.5F, Me.Height - 0.5F)
                    End If

                End If

            End Using

        End If

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        If (Me.DesignMode) Then
            updateControlHeight()
        End If
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        updateControlHeight()

    End Sub

    'Private Methods
    Private Sub updateControlHeight()
        If TextBox1.Multiline = False Then
            Dim txtHeight As Integer = TextRenderer.MeasureText("Text", Me.Font).Height + 1
            TextBox1.Multiline = True
            TextBox1.MinimumSize = New Size(0, txtHeight)
            TextBox1.Multiline = False

            Me.Height = TextBox1.Height + Me.Padding.Top + Me.Padding.Bottom
        End If
    End Sub

    Private Function GetFigurePath(rect As RectangleF, radius As Single) As GraphicsPath
        Dim path As GraphicsPath = New GraphicsPath
        path.StartFigure()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90)
        path.CloseFigure()

        Return path
    End Function

    Private Sub SetTextBoxRoundedRegion()

        Dim pathTxt As GraphicsPath
        If Me.MultiLine Then
            pathTxt = GetFigurePath(TextBox1.ClientRectangle, BorderRadius - BorderSize)
            TextBox1.Region = New Region(pathTxt)

        Else
            pathTxt = GetFigurePath(TextBox1.ClientRectangle, BorderRadius * 2)
            TextBox1.Region = New Region(pathTxt)

        End If

    End Sub

    Private Sub SetPlaceHolder()
        If String.IsNullOrWhiteSpace(TextBox1.Text) And PlaceHolderText IsNot Nothing Then
            IsPlaceHolder = True
            TextBox1.Text = PlaceHolderText
            TextBox1.ForeColor = PlaceHolderColor
            If IsPasswordChar Then
                TextBox1.UseSystemPasswordChar = False
            End If
        End If
    End Sub

    Private Sub RemovePlaceHolder()
        If IsPlaceHolder And PlaceHolderText IsNot Nothing Then
            IsPlaceHolder = False
            TextBox1.Text = String.Empty
            TextBox1.ForeColor = Me.ForeColor
            If IsPasswordChar Then
                TextBox1.UseSystemPasswordChar = True
            End If
        End If
    End Sub

#Region "Events"
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        RaiseEvent _TextChanged(sender, e)
    End Sub

    Private Sub TextBox1_Click(sender As Object, e As EventArgs) Handles TextBox1.Click
        Me.OnClick(e)
    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As EventArgs) Handles TextBox1.Enter
        _isFocused = True
        Me.Invalidate()
        RemovePlaceHolder()

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        _isFocused = False
        Me.Invalidate()
        SetPlaceHolder()

    End Sub
#End Region

End Class
