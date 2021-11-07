Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class ProjektRoundedForm_Component
    Inherits Component

    Private _selectedForm As Form
    Private _borderSize As Integer = 20

    <Browsable(False)>
    Public Property SelectedForm As Form
        Get
            Return _selectedForm
        End Get
        Set(value As Form)
            If value IsNot Nothing Then
                _selectedForm = value
                ' _selectedForm.Region = Region.FromHrgn(ProjektRoundedForm.CreateRoundRectRgn(0, 0, Me._selectedForm.Width, Me._selectedForm.Height, _borderSize, _borderSize))

                ' Create a GraphicsPath that represents a rectangle with rounded corners
                Dim path As New GraphicsPath()
                Dim r As New Rectangle(0, 0, Me._selectedForm.Width, Me._selectedForm.Height)

                ' the following code lifted from https://stackoverflow.com/questions/1049328/how-to-create-a-rounded-rectangle-at-runtime-in-windows-forms-with-vb-net-c

                Dim d As Integer = _borderSize * 2   ' twiddle with this if the corners are too round; e.g. change it to just d = _borderSize
                path.StartFigure()
                path.AddLine(r.Left + d, r.Top, r.Right - d, r.Top)
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Top, r.Right, r.Top + d), -90, 90)
                path.AddLine(r.Right, r.Top + d, r.Right, r.Bottom - d)
                path.AddArc(Rectangle.FromLTRB(r.Right - d, r.Bottom - d, r.Right, r.Bottom), 0, 90)
                path.AddLine(r.Right - d, r.Bottom, r.Left + d, r.Bottom)
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Bottom - d, r.Left + d, r.Bottom), 90, 90)
                path.AddLine(r.Left, r.Bottom - d, r.Left, r.Top + d)
                path.AddArc(Rectangle.FromLTRB(r.Left, r.Top, r.Left + d, r.Top + d), 180, 90)
                path.CloseFigure()

                _selectedForm.Region = New Region(path)

                _selectedForm.Invalidate()
            End If
        End Set
    End Property
    <Category("Projekt")>
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(value As Integer)
            _borderSize = value
        End Set
    End Property


    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>'Creates rounded form
    Private Shared Function CreateRoundRectRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer, ByVal nWidthEllipse As Integer, ByVal nHeightEllipse As Integer) As IntPtr
    End Function

End Class
