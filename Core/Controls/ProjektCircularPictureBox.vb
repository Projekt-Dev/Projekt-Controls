Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class ProjektCircularPictureBox
    Inherits PictureBox

    'Fields
    Private _borderSize As Integer = 2
    Private _borderColor As Color = Color.Crimson
    Private _borderColor2 As Color = Color.DarkRed
    Private _borderLineStyle As DashStyle = DashStyle.Solid
    Private _borderCapStyle As DashCap = DashCap.Flat
    Private _gradientAngle As Double = 50.0F

    Public Sub New()
        Me.Size = New Size(100, 100)
        Me.SizeMode = PictureBoxSizeMode.StretchImage
        Me.BackgroundImageLayout = ImageLayout.Stretch
    End Sub

    'Properties
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
    Public Property BorderColor2 As Color
        Get
            Return _borderColor2
        End Get
        Set(value As Color)
            _borderColor2 = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderLineStyle As DashStyle
        Get
            Return _borderLineStyle
        End Get
        Set(value As DashStyle)
            _borderLineStyle = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderCapStyle As DashCap
        Get
            Return _borderCapStyle
        End Get
        Set(value As DashCap)
            _borderCapStyle = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property GradientAngle As Double
        Get
            Return _gradientAngle
        End Get
        Set(value As Double)
            _gradientAngle = value
            Me.Invalidate()
        End Set
    End Property

    'Overridden methods
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Size = New Size(Me.Width, Me.Width)
    End Sub
    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        MyBase.OnPaint(pe)
        'Fields
        Dim g = pe.Graphics
        Dim rectContourSmooth = Rectangle.Inflate(Me.ClientRectangle, -1, -1)
        Dim rectBorder = Rectangle.Inflate(rectContourSmooth, -_borderSize, -_borderSize)
        Dim smoothSize = If(_borderSize > 0, _borderSize * 3, 1)

        Using borderGColor = New LinearGradientBrush(rectBorder, _borderColor, _borderColor2, CType(_gradientAngle, Single))
            Using pathRegion = New GraphicsPath
                Using penSmooth = New Pen(Me.Parent.BackColor, smoothSize)
                    Using penBorder = New Pen(borderGColor, _borderSize)
                        g.SmoothingMode = SmoothingMode.AntiAlias
                        penBorder.DashStyle = _borderLineStyle
                        penBorder.DashCap = _borderCapStyle

                        pathRegion.AddEllipse(rectContourSmooth)
                        Me.Region = New Region(pathRegion)

                        'Drawing
                        g.DrawEllipse(penSmooth, rectContourSmooth)
                        If _borderSize > 0 Then
                            g.DrawEllipse(penBorder, rectBorder)
                        End If

                    End Using
                End Using
            End Using
        End Using

    End Sub

End Class
