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
    Private _underlineStyle As Boolean = False
    Private _isFocused As Boolean = False

    Friend WithEvents TextBox1 As TextBox

    'Events
    Public Event _TextChanged As EventHandler

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
            Return TextBox1.UseSystemPasswordChar
        End Get
        Set(value As Boolean)
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
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            TextBox1.Text = value
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


    'Methods
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim g As Graphics = e.Graphics

        Using penBorder As New Pen(BorderColor, BorderSize)
            penBorder.Alignment = PenAlignment.Center

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

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        _isFocused = False
        Me.Invalidate()

    End Sub
#End Region

End Class
