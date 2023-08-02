Public Class ProgressBar

    'The Progress of loading 0 - 100
    Public Progress As Integer = 0
    'The text to show above the progress bar while loading etc
    Public ProgressText As String = ""

    Private Sub ProgressBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ProgressBar_Close(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        ProgressBar1.Value = 0

    End Sub


    Public Sub ShowProgress()

        ProgressBar1.Value = Convert.ToInt32(Progress)

        If ProgressText = "" Then
            ProgressText = "Loading..."

        End If

        InfoText.Text = ProgressText





    End Sub


End Class