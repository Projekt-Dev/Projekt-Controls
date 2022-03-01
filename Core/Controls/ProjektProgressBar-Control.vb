Imports System.ComponentModel

Public Enum TextPosition
    Left
    Right
    Center
    Sliding
    None
End Enum

Public Class ProjektProgressBar
    Inherits ProgressBar

    'Fields
    Private _channelColor As Color = Color.Crimson
    Private _sliderColor As Color = Color.Gray
    Private _foreBackColor As Color = Color.Gray
    Private _channelHeight As Integer = 6
    Private _sliderHeight As Integer = 6
    Private _showValue As TextPosition = TextPosition.Right

    'Other
    Private _paintedBack As Boolean = False
    Private _stopPainting As Boolean = False

    Private Sub New()
        Me.SetStyle(ControlStyles.UserPaint, True)
        Me.ForeColor = Color.White
    End Sub

    <Category("Projekt")>
    Public Property ChannelColor As Color
        Get
            Return _channelColor
        End Get
        Set(value As Color)
            _channelColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property SliderColor As Color
        Get
            Return _sliderColor
        End Get
        Set(value As Color)
            _sliderColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property ForeBackColor As Color
        Get
            Return _foreBackColor
        End Get
        Set(value As Color)
            _foreBackColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property ChannelHeight As Integer
        Get
            Return _channelHeight
        End Get
        Set(value As Integer)
            _channelHeight = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property ShowValue As TextPosition
        Get
            Return _showValue
        End Get
        Set(value As TextPosition)
            _showValue = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    <Browsable(True)>
    <EditorBrowsable(EditorBrowsableState.Always)>
    Public Overrides Property Font As Font
        Get
            Return Me.Font
        End Get
        Set(value As Font)
            Me.Font = value
        End Set
    End Property
    <Category("Projekt")>
    Public Overrides Property ForeColor As Color
        Get
            Return Me.ForeColor
        End Get
        Set(value As Color)
            Me.ForeColor = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property SliderHeight As Integer
        Get
            Return _sliderHeight
        End Get
        Set(value As Integer)
            _sliderHeight = value
        End Set
    End Property

    'Paint background and channel
    Protected Overrides Sub OnPaintBackground(pevent As PaintEventArgs)
        If _stopPainting = False Then
            If _paintedBack = False Then
                'Fields
                Dim g As Graphics = pevent.Graphics
                Dim rect As New Rectangle(0, 0, Me.Width, _channelHeight)

                Using brushChannel As New SolidBrush(_channelColor)
                    If _channelHeight >= _sliderHeight Then
                        rect.Y = Me.Height - _channelHeight
                    Else
                        rect.Y = Me.Height - ((_channelHeight + _sliderHeight) / 2)
                    End If
                    g.Clear(Me.Parent.BackColor)
                    g.FillRectangle(brushChannel, rect)

                    If Me.DesignMode = False Then
                        _paintedBack = True
                    End If
                End Using
                'reset painting the background and channel
                If Me.Value = Me.Maximum Or Me.Value = Me.Minimum Then
                    _paintedBack = False
                End If
            End If
        End If
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        If _stopPainting = False Then
            Dim g As Graphics = e.Graphics
            Dim scaleFactor As Double = ((Me.Value - Me.Minimum) / (Me.Maximum - Me.Minimum))
            Dim sliderWidth = CType(Me.Width * scaleFactor, Integer)
            Dim rectSlider As New Rectangle(0, 0, sliderWidth, _sliderHeight)
            Using brushSlider As New SolidBrush(_sliderColor)
                If _sliderHeight >= _channelHeight Then
                    rectSlider.Y = Me.Height - _sliderHeight
                Else
                    rectSlider.Y = Me.Height - ((_sliderHeight + _channelHeight) / 2)
                End If

                'Painting slider
                If sliderWidth > 1 Then
                    g.FillRectangle(brushSlider, rectSlider)
                End If
                If Not ShowValue = TextPosition.None Then
                    DrawTextValue(g, sliderWidth, rectSlider)
                End If
            End Using

        End If
    End Sub

    Private Sub DrawTextValue(g As Graphics, sliderWidth As Integer, rectSlider As Rectangle)
        'Fields
        Dim text As String = Me.Value.ToString() + "%"
        Dim textSize = TextRenderer.MeasureText(text, Me.Font)
        Dim rectText = New Rectangle(0, 0, textSize.Width, textSize.Height + 2)
        Using brushText As New SolidBrush(Me.ForeColor)
            Using brushTextBack As New SolidBrush(ForeBackColor)
                Using textFormat As New StringFormat
                    Select Case ShowValue
                        Case TextPosition.Left
                            rectText.X = 0
                            textFormat.Alignment = StringAlignment.Near
                        Case TextPosition.Right
                            rectText.X = Me.Width - textSize.Width
                            textFormat.Alignment = StringAlignment.Far
                        Case TextPosition.Center
                            rectText.X = (Me.Width - textSize.Width) / 2
                            textFormat.Alignment = StringAlignment.Center
                        Case TextPosition.Sliding
                            rectText.X = sliderWidth - textSize.Width
                            textFormat.Alignment = StringAlignment.Center
                            'Clean previous text surface
                            Using brushClear As New SolidBrush(Me.Parent.BackColor)
                                Dim rect = rectSlider
                                rect.Y = rectText.Y
                                rect.Height = rect.Height
                                g.FillRectangle(brushClear, rect)
                            End Using
                    End Select

                    'Painting
                    g.FillRectangle(brushTextBack, rectText)
                    g.DrawString(text, Me.Font, brushText, rectText, textFormat)

                End Using
            End Using
        End Using

    End Sub
End Class