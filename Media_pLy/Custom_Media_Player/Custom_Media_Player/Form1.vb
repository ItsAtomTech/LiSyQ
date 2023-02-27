Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Computer.FileSystem.CurrentDirectory = "D:\Music\あにめおんがくです。"
        ListBox1.DataSource = My.Computer.FileSystem.GetFiles(My.Computer.
        FileSystem.CurrentDirectory, FileIO.SearchOption.SearchTopLevelOnly,
        "*.mp3")




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If ListBox1.SelectedItem <> "" Then
            Me.AxWindowsMediaPlayer1.URL = ListBox1.SelectedItem.ToString
	        Else
            MsgBox("Please select a file", MsgBoxStyle.OkOnly, "Select a file")
        End If



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dx As String = Me.AxWindowsMediaPlayer1.currentMedia.getItemInfo("Title")
        Dim d As Double = Me.AxWindowsMediaPlayer1.Ctlcontrols.currentPosition


        MsgBox(dx & vbTab & Conversion.Int(d))

    End Sub
End Class
