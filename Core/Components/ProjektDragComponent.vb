Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class ProjektDragComponent
    Inherits Component

    Private handleControl As Control

    Public Property SelectedControl As Control
        Get
            Return Me.handleControl
        End Get
        Set(value As Control)
            Me.handleControl = value
            AddHandler Me.handleControl.MouseDown, New MouseEventHandler(AddressOf Me.Mouse_Down)
        End Set
    End Property
    <DllImport("user32.dll")>
    Public Shared Function SendMessage(a As IntPtr, msg As Integer, wParam As Integer, lParam As Integer) As Integer

    End Function
    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean

    End Function
    Private Sub Mouse_Down(sender As Object, e As MouseEventArgs)
        Dim flag As Boolean = e.Button = MouseButtons.Left
        If flag Then
            ProjektDragComponent.ReleaseCapture()
            ProjektDragComponent.SendMessage(Me.SelectedControl.FindForm().Handle, 161, 2, 0)
        End If
    End Sub

End Class
