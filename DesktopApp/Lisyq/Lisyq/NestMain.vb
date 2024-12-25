Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Web.WebView2.Core
Imports System.Text.RegularExpressions
Public Class NestMain

    Dim Point As New Point
    Dim SavePath As String = ""
    Dim dirPath As String = ""
    Dim SavePlayListPath As String = ""
    Dim SaveNestedTLPath As String = ""
    Dim relativeLocation = My.Application.Info.DirectoryPath

    Dim locationData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LiSyQ"
    Dim settings As My.MySettings
    Dim Asnew
    Dim NTAsnew

    Dim df As String
    Dim phc

    Dim OpenQuePath As String

    Private Async Sub NestMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim opts = New CoreWebView2EnvironmentOptions(additionalBrowserArguments:="--allow-file-access-from-files")
        Dim env = Await CoreWebView2Environment.CreateAsync(Nothing, locationData, opts)

        Await WebView21.EnsureCoreWebView2Async(env)

        WebView21.CoreWebView2.AddHostObjectToScript("NativeObject", New WebJsObject())
        WebView21.CoreWebView2.SetVirtualHostNameToFolderMapping("lisyq", "", CoreWebView2HostResourceAccessKind.Allow)

        'WebView21.CoreWebView2.Navigate("https://lisyq/main.html")
        WebView21.CoreWebView2.Navigate("file:///" & relativeLocation & "/views/nest_main.html")

        WebView21.CoreWebView2.Settings.AreDefaultContextMenusEnabled = False
        WebView21.CoreWebView2.Settings.AreHostObjectsAllowed = True
        WebView21.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = True
        WebView21.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = True
        WebView21.CoreWebView2.Settings.IsPinchZoomEnabled = False
        WebView21.CoreWebView2.Settings.IsZoomControlEnabled = False



    End Sub

    <ClassInterface(ClassInterfaceType.None)>
    <ComVisible(True)>
    Public Class WebJsObject

        Public Function get_values() As String

            Return Main.df

        End Function

        Public Function Open_File()

            NestMain.Open_File()


        End Function


        'Opening and saving Playlist
        Public Function Save_File_PL()
            NestMain.Save_File_PL()
        End Function

        Public Function put_pl_data(data As String)
            NestMain.data_string_pl = data
        End Function


        Public Function Open_File_PL()
            NestMain.Open_File_PL()

        End Function
        Public Function Open_File_NT()
            NestMain.Open_File_NT()

        End Function

        Public Function open_filePath(filePath) As String
            Dim fileReader As String
            Try
                fileReader = My.Computer.FileSystem.ReadAllText(filePath)
            Catch ex As Exception
                MessageBox.Show("Failed Loading: " & filePath)
                fileReader = ""
            End Try

            Return fileReader

        End Function


        'Opening and saving Nested Playlist
        Public Function Save_File_NT()
            NestMain.Save_File_NT()
        End Function

        Public Function put_data_nt(data As String)
            NestMain.data_string_nt = data
        End Function

        ' Progress Window Logic Start

        Public Function set_progress(progress As Integer, info As String)

            ProgressBar.Progress = progress
            ProgressBar.ProgressText = info


        End Function

        Public Function set_mouse_coords(x As Integer, y As Integer)

            NestMain.Point.X = x
            NestMain.Point.Y = y

        End Function

        Public Function Show_template_scriptmenu()

            NestMain.Show_template_scriptmenu()

        End Function

        Public Function Show_content_menu()

            NestMain.Show_content_menu()

        End Function

        Public Function show_progress()

            ProgressBar.ShowProgress()

            If ProgressBar.Visible = False Then
                ProgressBar.Show(Me)
                ProgressBar.BringToFront()

            End If

        End Function

        Public Function close_progress()
            ProgressBar.Close()
        End Function

        ' Progress Window End



        ' Sending to Port start
        ' Process output string to be sent to Ouput mediums
        Public Function outputs() As String
            Dim pchd As Integer = 0
            Dim arrayLists() As String = NestMain.df.Split("|")

            For Each strg In arrayLists

                Try
                    Cports.Ports(pchd).WriteTimeout = 34
                    'Cports.Ports(pchd).WriteLine(strg)

                    Dim Df = Cports.Ports(pchd).BytesToWrite

                    If Df <= 1 Or strg = "" Or strg = " " Then
                        Dim ParObject As Object = {pchd, strg}
                        Dim Th = New System.Threading.Thread(New Threading.ThreadStart(Sub() Cports.SendToCOM(ParObject)))
                        Th.Priority = Threading.ThreadPriority.Normal

                        Th.IsBackground = True
                        Th.Start()
                    End If


                Catch ex As Exception

                End Try
                pchd += 1
            Next




        End Function




        Public Function clearAllBuffer() As String

            For xc = 0 To Cports.Ports.Count - 1
                Try
                    Cports.Ports(xc).DiscardOutBuffer()

                Catch ex As Exception

                End Try


            Next

        End Function

        ' This sets the value To be sent To outputs from the js object
        Public Function set_values(port As Integer, Data As String)

            NestMain.phc = port
            NestMain.df = Data

        End Function

        ' Sending to Port start End


    End Class


    Public Sub Show_template_scriptmenu()
        template_menu.Show(Point.X, Point.Y)

    End Sub
    Public Sub Show_content_menu()
        content_menu.Show(Point.X, Point.Y)

    End Sub

    Public Sub Open_File_From(filepath As String)
        OpenFileDialog1.Filter = "LSYS Files (*.lsys*)|*.lsys"


        If SavePath.Length > 0 Then
            Dim confirm_loadnew As DialogResult

            confirm_loadnew = MessageBox.Show("You are about to load this file and close currently open file so save changes first, Continue to Load?", "Open New", MessageBoxButtons.YesNo
                                         )
            If confirm_loadnew = DialogResult.Yes Then
                ProgressBar.Show(Me)
            Else
                Return
            End If

        End If




        SavePath = filepath
        NotificationManager.Show(Me, "File: " & OpenFileDialog1.FileName & "is Now Loading.", Color.Green, 2000)


    End Sub

    Private Sub timeline__Click(sender As Object, e As EventArgs)

    End Sub


    Public Sub Open_template_scriptmenu()
        template_menu.Show(Point.X, Point.Y)

    End Sub

    Private Sub manual__Click(sender As Object, e As EventArgs) Handles manual_.Click
        Main.Show()
        Main.WindowState = FormWindowState.Normal
        Me.WindowState = FormWindowState.Minimized


    End Sub

    Private Sub WebView21_Click(sender As Object, e As EventArgs) Handles WebView21.Click

    End Sub

    Public Sub Open_File()
        OpenFileDialog1.Filter = "LSYS Files (*.lsys*)|*.lsys"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)

            ' ProgressBar.Show()

            Dim filePath = Main.AutoEscapeString(OpenFileDialog1.FileName)

            WebView21.ExecuteScriptAsync("load_from_file('" & fileReader & "',""" & filePath & """)")
            NotificationManager.Show(Me, "File: " & OpenFileDialog1.FileName & "is Now Loading.", Color.Green, 2000)


        End If

    End Sub


    Dim data_string_pl
    Public Sub Save_File_PL()

        If SavePlayListPath = "" Or Asnew = True Then
            SaveFileDialog2.Filter = "Lisyq Playlist Files (*.lips*)|*.lips"
            If SaveFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog2.FileName, data_string_pl, False)
                SavePlayListPath = SaveFileDialog2.FileName
                Asnew = False
            Else
                If SavePlayListPath = "" Then
                    Asnew = True
                End If
            End If

        Else
            My.Computer.FileSystem.WriteAllText(SavePlayListPath, data_string_pl, False)
            NotificationManager.Show(Me, "Saving File: " & SavePlayListPath, Color.Green, 2000)
            'MsgBox("File saved! to " + SaveLivePlayerPath)
        End If



    End Sub

    Dim data_string_nt
    Public Sub Save_File_NT()

        If SaveNestedTLPath = "" Or NTAsnew = True Then
            SaveFileDialog2.Filter = "Lisyq Nested Project Timeline Files (*.ntlis*)|*.ntlis"
            If SaveFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog2.FileName, data_string_nt, False)
                SaveNestedTLPath = SaveFileDialog2.FileName
                NTAsnew = False
            Else
                If SaveNestedTLPath = "" Then
                    NTAsnew = True
                End If
            End If


        Else
            My.Computer.FileSystem.WriteAllText(SaveNestedTLPath, data_string_nt, False)
            NotificationManager.Show(Me, "Saving File: " & SaveNestedTLPath, Color.Green, 2000)
            'MsgBox("File saved! to " + SaveLivePlayerPath)
        End If



    End Sub


    Public Sub Open_File_PL()
        OpenFileDialog2.Filter = "Lisyq playlist Files (*.lips*)|*.lips"


        If SavePlayListPath.Length > 0 Then
            Dim confirm_loadnew As DialogResult

            confirm_loadnew = MessageBox.Show("You are about to load a file and close currently open file so save changes first, Continue to Load?", "Open New", MessageBoxButtons.YesNo
                                         )
            If confirm_loadnew = DialogResult.Yes Then
                ProgressBar.Show(Me)
            Else
                Return
            End If

        End If

        If OpenFileDialog2.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog2.FileName)

            WebView21.ExecuteScriptAsync("load_pl_from_file('" & fileReader & "')")

            SavePlayListPath = OpenFileDialog2.FileName

            NotificationManager.Show(Me, "File: " & OpenFileDialog2.FileName & "is Now Loading.", Color.Green, 2000)

        End If

        ' ProgressBar.Show(Me)
        ' ProgressBar.BringToFront()




    End Sub
    Public Sub Open_File_NT()
        OpenFileDialog2.Filter = "Lisyq Nested Project Timeline Files (*.ntlis*)|*.ntlis"


        If SaveNestedTLPath.Length > 0 Then
            Dim confirm_loadnew As DialogResult

            confirm_loadnew = MessageBox.Show("You are about to load a file and close currently open file so save changes first, Continue to Load?", "Open New", MessageBoxButtons.YesNo
                                         )
            If confirm_loadnew = DialogResult.Yes Then
                ProgressBar.Show(Me)
            Else
                Return
            End If

        End If

        If OpenFileDialog2.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog2.FileName)

            WebView21.ExecuteScriptAsync("load_from_file_nt('" & fileReader & "')")

            SaveNestedTLPath = OpenFileDialog2.FileName

            NotificationManager.Show(Me, "File: " & OpenFileDialog2.FileName & "is Now Loading.", Color.Green, 2000)

        End If

        ' ProgressBar.Show(Me)
        ' ProgressBar.BringToFront()




    End Sub

    Private Sub PortConfigurationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortConfigurationToolStripMenuItem.Click
        Form1.Show()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("save_to_file_nt()")
    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Open_File_NT()
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        NTAsnew = True
        WebView21.ExecuteScriptAsync("save_to_file_nt()")
    End Sub

    Private Sub CancelToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem1.Click

    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        WebView21.ExecuteScriptAsync("remove_subtrack()")
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        WebView21.ExecuteScriptAsync("remove_scriptstub()")
    End Sub


    Public Sub ComingSoon()
        NotificationManager.Show(Me, "This Feature is coming soon...", Color.Gold, 2000)
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        ComingSoon()
    End Sub

    Private Sub ToolStripMenuItem14_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem14.Click
        ComingSoon()
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        ComingSoon()
    End Sub
End Class