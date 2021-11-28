Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class ProjektButton
    Inherits Button

    'Fields
    Private bSize As Integer = 0
    Private bRadius As Integer = 40
    Private bColor As Color = Color.LightSeaGreen

    <Category("Projekt")>
    Public Property BorderSize As Integer
        Get
            Return bSize
        End Get
        Set(value As Integer)
            Me.bSize = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderRadius As Integer
        Get
            Return bRadius
        End Get
        Set(value As Integer)
            If value <= Me.Height Then
                bRadius = value
            Else
                value = Me.Height
            End If
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderColor As Color
        Get
            Return bColor
        End Get
        Set(value As Color)
            bColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property BackgroundColor As Color
        Get
            Return Me.BackColor
        End Get
        Set(value As Color)
            Me.BackColor = value
        End Set
    End Property
    <Category("Projekt")>
    Public Property TextColor As Color
        Get
            Return Me.ForeColor
        End Get
        Set(value As Color)
            Me.ForeColor = value
        End Set
    End Property

    'Constructor
    Public Sub New()
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.Size = New Size(150, 40)
        Me.BackColor = Color.Crimson
        Me.ForeColor = Color.White
        AddHandler Me.Resize, New EventHandler(AddressOf Button_Resize)
    End Sub

    'Methods
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

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)
        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias
        pevent.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality
        pevent.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic

        Dim rectSurface As RectangleF = New RectangleF(0, 0, Me.Width, Me.Height)
        Dim rectBorder As RectangleF = New RectangleF(1, 1, Me.Width - 0.8F, Me.Height - 1)

        If bRadius > 2 Then 'Rounded button

            Using pathSurface As GraphicsPath = GetFigurePath(rectSurface, bRadius)
                Using pathBorder As GraphicsPath = GetFigurePath(rectBorder, bRadius - 1.0F)
                    Using penSurface As New Pen(Me.Parent.BackColor, 2)
                        Using penBorder As New Pen(bColor, bSize)

                            penBorder.Alignment = PenAlignment.Inset
                            'Button surface
                            Me.Region = New Region(pathSurface)
                            'Draw surface border for HD result
                            pevent.Graphics.DrawPath(penSurface, pathSurface)
                            'Button border
                            If bSize >= 1 Then
                                pevent.Graphics.DrawPath(penBorder, pathBorder)
                            End If

                        End Using
                    End Using
                End Using
            End Using

        Else 'Normal Button

            Me.Region = New Region(rectSurface)
            If bSize >= 1 Then
                Using penBorder As New Pen(bColor, bSize)
                    penBorder.Alignment = PenAlignment.Inset
                    pevent.Graphics.DrawRectangle(penBorder, 0, 0, Me.Width - 1, Me.Height - 1)
                End Using
            End If

        End If

    End Sub

    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        AddHandler Me.Parent.BackColorChanged, New EventHandler(AddressOf Container_BackColorChanged)
    End Sub

    Private Sub Container_BackColorChanged(sender As Object, e As EventArgs)
        If Me.DesignMode Then
            Me.Invalidate()
        End If

    End Sub
    Private Sub Button_Resize(sender As Object, e As EventArgs)
        If bRadius > Me.Height Then
            bRadius = Me.Height
        End If
    End Sub


End Class