Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel

Public Class ProjektRadioButton
    Inherits RadioButton

    'Fields
    Private _checkedColor As Color = Color.Crimson
    Private _uncheckedColor As Color = Color.Gray

    <Category("Projekt")>
    Public Property CheckedColor As Color
        Get
            Return _checkedColor
        End Get
        Set(value As Color)
            _checkedColor = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property UncheckedColor As Color
        Get
            Return _uncheckedColor
        End Get
        Set(value As Color)
            _uncheckedColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Sub New()
        Me.MinimumSize = New Size(0, 21)
    End Sub

    'Methods
    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        MyBase.OnPaint(pevent)
        'Fields
        Dim g As Graphics = pevent.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim rbBorderSize As Single = 18.0F
        Dim rbCheckdSize As Single = 12.0F
        Dim rectRbBorder As RectangleF = New RectangleF With {
            .X = 0.5F,
            .Y = (Me.Height - rbBorderSize) / 2,
            .Width = rbBorderSize,
            .Height = rbBorderSize
        }
        Dim rectRbCheck As RectangleF = New RectangleF With {
            .X = rectRbBorder.X + (rectRbBorder.Width - rbCheckdSize) / 2,
            .Y = (Me.Height - rbCheckdSize) / 2,
            .Width = rbCheckdSize,
            .Height = rbCheckdSize
        }

        'Drawing
        Using penBorder As New Pen(_checkedColor, 1.6F)
            Using brushRbCheck As New SolidBrush(_checkedColor)
                Using brushText As New SolidBrush(Me.ForeColor)
                    'Draw Surface
                    g.Clear(Me.BackColor)
                    'Draw Radio Button
                    If Me.Checked Then
                        g.DrawEllipse(penBorder, rectRbBorder)
                        g.FillEllipse(brushRbCheck, rectRbCheck)
                    Else
                        penBorder.Color = _uncheckedColor
                        g.DrawEllipse(penBorder, rectRbBorder)
                    End If

                    g.DrawString(Me.Text, Me.Font, brushText,
                                rbBorderSize + 8, (Me.Height - TextRenderer.MeasureText(Me.Text, Me.Font).Height) / 2)


                End Using
            End Using
        End Using

    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Me.Width = TextRenderer.MeasureText(Me.Text, Me.Font).Width + 30
    End Sub


End Class
