Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class ProjektDatePicker
    Inherits DateTimePicker

    'Main Fields
    Private _skinColor As Color = Color.Crimson
    Private _textColor As Color = Color.White
    Private _borderColor As Color = Color.PaleVioletRed
    Private _borderSize As Integer = 0

    'Misc Fields
    Private _droppedDown As Boolean = False
    Private _calendarIcon As Image = My.Resources.calendar_Light 'Increase default size
    Private _iconButtonArea As RectangleF
    Private ReadOnly _calendarIconWidth = 34
    Private ReadOnly _arraowIconWidth = 17

    <Category("Projekt")>
    Public Property SkinColor As Color
        Get
            Return _skinColor
        End Get
        Set(value As Color)
            _skinColor = value
            If _skinColor.GetBrightness >= 0.8F Then
                _calendarIcon = My.Resources.calendar_Dark
            Else
                _calendarIcon = My.Resources.calendar_Light
            End If
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property TextColor As Color
        Get
            Return _textColor
        End Get
        Set(value As Color)
            _textColor = value
            Me.Invalidate()
        End Set
    End Property
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

    'Contructor
    Public Sub New()
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.MinimumSize = New Size(0, 35)
        Me.Font = New Font(Me.Font.Name, 9.5F)
    End Sub

    'Methods
    Protected Overrides Sub OnDropDown(eventargs As EventArgs)
        MyBase.OnDropDown(eventargs)
        _droppedDown = True
    End Sub

    Protected Overrides Sub OnCloseUp(eventargs As EventArgs)
        MyBase.OnCloseUp(eventargs)
        _droppedDown = False
    End Sub

    Protected Overrides Sub OnKeyPress(e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        e.Handled = True
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Using g As Graphics = Me.CreateGraphics
            Using penBorder As New Pen(BorderColor, BorderSize)
                Using skinBrush As New SolidBrush(SkinColor)
                    Using openIconBrush As New SolidBrush(Color.FromArgb(50, 64, 64, 64))
                        Using textBrush As New SolidBrush(TextColor)
                            Using textFormat As New StringFormat
                                Dim clientArea As New RectangleF(0, 0, Me.Width - 0.5F, Me.Height - 0.5F)
                                Dim iconArea As New RectangleF(clientArea.Width - _calendarIconWidth, 0, _calendarIconWidth, clientArea.Height)
                                penBorder.Alignment = PenAlignment.Inset
                                textFormat.LineAlignment = StringAlignment.Center

                                'Draw Surface
                                g.FillRectangle(skinBrush, clientArea)
                                'Draw Text
                                g.DrawString(" " + Me.Text, Me.Font, textBrush, clientArea, textFormat)
                                'Draw open calendar icon highlight
                                If _droppedDown = True Then
                                    g.FillRectangle(openIconBrush, iconArea)
                                End If
                                'Draw border
                                If BorderSize >= 1 Then
                                    g.DrawRectangle(penBorder, clientArea.X, clientArea.Y, clientArea.Width, clientArea.Height)
                                End If
                                'Draw icon
                                g.DrawImage(_calendarIcon, Me.Width - _calendarIcon.Width - 9, CSng((Me.Height - _calendarIcon.Height) / 2))

                            End Using
                        End Using
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Dim iconWidth = GetIconButtonWidth()
        _iconButtonArea = New RectangleF(Me.Width - iconWidth, 0, iconWidth, Me.Height)
    End Sub

    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        If _iconButtonArea.Contains(e.Location) Then
            Me.Cursor = Cursors.Hand
        Else
            Me.Cursor = Cursors.Default
        End If
    End Sub

    'Private Methods
    Private Function GetIconButtonWidth() As Integer
        Dim textWidth = TextRenderer.MeasureText(Me.Text, Me.Font).Width
        If textWidth <= Me.Width - (_calendarIconWidth + 20) Then
            Return _calendarIconWidth
        Else
            Return _arraowIconWidth
        End If
    End Function


End Class
