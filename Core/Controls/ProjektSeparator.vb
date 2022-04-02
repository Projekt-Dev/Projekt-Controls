Imports System.ComponentModel
Imports System.Windows.Forms

Public Class ProjektSeparator
    Inherits Control

    Private _thickness As Integer = 1
    Private _isVertical As Boolean

    <Category("Projekt")>
    Public Property isVertical As Boolean
        Get
            Return _isVertical
        End Get
        Set(value As Boolean)
            Me._isVertical = value
            Me.Invalidate()
        End Set
    End Property
    <Category("Projekt")>
    Public Property Thickness As Integer
        Get
            Return _thickness
        End Get
        Set(value As Integer)
            Me._thickness = value

            If Me.Height < _thickness Then
                Height = _thickness
            Else
                Me.Invalidate()
            End If

        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dim sz = If(_isVertical, Me.Height / 2, Me.Width / 2)
        e.Graphics.TranslateTransform(Me.Width / 2.0F, Me.Height / 2.0F)

        Using pen As Pen = New Pen(ForeColor, _thickness)
            If Not _isVertical Then
                e.Graphics.DrawLine(pen, CSng(-sz + Padding.Left), 0, CSng(sz - Padding.Right), 0)

            Else
                e.Graphics.DrawLine(pen, 0, CSng(-sz + Padding.Top), 0, CSng(sz - Padding.Bottom))
            End If
        End Using

    End Sub

    Protected Overrides Sub OnPaddingChanged(e As EventArgs)
        MyBase.OnPaddingChanged(e)
        Me.Invalidate()

    End Sub




End Class
