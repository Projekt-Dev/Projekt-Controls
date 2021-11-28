Public Class MainForm
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MainRound.SelectedForm = Me
    End Sub

    Private Sub ProjektButton1_Click(sender As Object, e As EventArgs) Handles ProjektButton1.Click
        MessageBox.Show(ProjektTextBox1._Text)
    End Sub

End Class
