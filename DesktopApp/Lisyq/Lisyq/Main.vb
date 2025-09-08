Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Web.WebView2.Core
Imports System.Text.RegularExpressions



Public Class Main

    Dim Point As New Point
    Dim SavePath As String = ""
    Dim dirPath As String = ""
    Dim SaveLivePlayerPath As String = ""
    Dim relativeLocation = My.Application.Info.DirectoryPath

    Dim locationData As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\LiSyQ"
    Dim settings As My.MySettings

    Dim OpenQuePath As String

    Dim FromOpenFile As Boolean = False


    Private Async Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim opts = New CoreWebView2EnvironmentOptions(additionalBrowserArguments:="--allow-file-access-from-files")
        Dim env = Await CoreWebView2Environment.CreateAsync(Nothing, locationData, opts)

        Await WebView21.EnsureCoreWebView2Async(env)

        WebView21.CoreWebView2.AddHostObjectToScript("NativeObject", New WebJsObject())
        WebView21.CoreWebView2.SetVirtualHostNameToFolderMapping("lisyq", "", CoreWebView2HostResourceAccessKind.Allow)

        'WebView21.CoreWebView2.Navigate("https://lisyq/main.html")
        WebView21.CoreWebView2.Navigate("file:///" & relativeLocation & "/main.html")

        WebView21.CoreWebView2.Settings.AreDefaultContextMenusEnabled = False
        WebView21.CoreWebView2.Settings.AreHostObjectsAllowed = True
        WebView21.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = True
        WebView21.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = True
        WebView21.CoreWebView2.Settings.IsPinchZoomEnabled = False
        WebView21.CoreWebView2.Settings.IsZoomControlEnabled = False





    End Sub

    Private Sub Main_Close(sender As Object, e As EventArgs) Handles MyBase.Closing
        Form1.Close()

    End Sub



    Public Sub Open_template_menu()
        template_menu.Show(Point.X, Point.Y)

    End Sub
    Public Sub Open_template_2_menu()
        template_menu_manual.Show(Point.X, Point.Y)
    End Sub

    Public Sub Open_content_menu()
        content_menu.Show(Point.X, Point.Y)
    End Sub

    Public Sub Open_track_menu()
        track_options.Show(Point.X, Point.Y)
    End Sub




    Private Sub WebView21_MouseUp(sender As Object, e As MouseEventArgs) Handles WebView21.MouseUp



        If e.Button = MouseButtons.Right Then

            Point.X = e.X
            Point.Y = e.Y



        End If


        MsgBox(Point.X)

    End Sub

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        'repaints the time ruler on the timeline on resize
        WebView21.ExecuteScriptAsync("gen_ruler()")

    End Sub


    Public data_string As String
    Public df As String

    Public Shared Function AutoEscapeString(filePath As String) As String
        ' Use Regex to escape special characters in the file path
        Return Regex.Escape(filePath)
    End Function


    <ClassInterface(ClassInterfaceType.None)>
    <ComVisible(True)>
    Public Class WebJsObject


        Dim phc As Integer

        Public Function Onready() As String

            'MsgBox("Start")

            If Main.OpenQuePath.Length > 0 Then
                Main.Open_File_From(Main.OpenQuePath)
                Main.OpenQuePath = ""

            End If


            Return Main.df

        End Function

        Public Function set_mouse_coords(x As Integer, y As Integer)

            Main.Point.X = x
            Main.Point.Y = y

        End Function




        ' *Legacy - Process output string to be sent to Ouput mediums
        Public Function outputs() As String

            outputs_v2() ' calls the new function, comment this function call to debug if needed
            Return True ' also comment this one

            Dim pchd As Integer = 0
            Dim arrayLists() As String = Main.df.Split("|")

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

        'New Implementation of output process that supports UDP packets and others
        Public Function outputs_v2()
            Dim pchd As Integer = 0
            Dim arrayLists() As String = Main.df.Split("|")

            For Each strg In arrayLists

                'For Comports goes here
                Try
                    If pchd >= 0 AndAlso pchd < Cports.Ports.Count Then
                        Dim port = Cports.Ports(pchd)

                        If port IsNot Nothing AndAlso port.IsOpen Then
                            port.WriteTimeout = 34
                            ' port.WriteLine(strg)

                            Dim Df = port.BytesToWrite

                            If Df <= 1 OrElse strg = "" OrElse strg = " " Then
                                Dim ParObject As Object = {pchd, strg}
                                Dim Th = New System.Threading.Thread(
                                     New Threading.ThreadStart(Sub() Cports.SendToCOM(ParObject))
                                 )
                                Th.Priority = Threading.ThreadPriority.Normal
                                Th.IsBackground = True
                                Th.Start()
                            End If
                        End If
                    End If


                Catch ex As Exception

                End Try

                'For UDP Channel Goes here

                Try
                    If pchd >= 0 AndAlso pchd < Cports.UdpChannels.Count Then
                        If Not String.IsNullOrWhiteSpace(strg) Then
                            Dim idx = pchd
                            Dim msg = strg
                            Dim Th = New System.Threading.Thread(
                                New Threading.ThreadStart(Sub() Cports.SendUdp(idx, msg)))
                            Th.Priority = Threading.ThreadPriority.Normal
                            Th.IsBackground = True
                            Th.Start()
                        End If
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

            phc = port
            Main.df = Data

        End Function

        Public Function get_values() As String

            Return Main.df


        End Function

        'Open the Port Configurator Panel
        Public Function OpenPorts() As String

            Form1.Show()

        End Function

        Public Function Show_template_menu()

            Main.Open_template_menu()

        End Function
        Public Function Show_manual_template_menu()

            Main.Open_template_2_menu()

        End Function


        Public Function Show_content_menu()

            Main.Open_content_menu()

        End Function


        Public Function Show_track_menu()

            Main.Open_track_menu()

        End Function

        Public Function put_data(data As String)

            Main.data_string = data


        End Function
        Public Function save_data()

            Main.Save_File()


        End Function
        Public Function Save_To_File()

            Main.Save_File()

        End Function
        Public Function Save_File_LVjs()

            Main.Save_File_LV()


        End Function
        Public Function Open_File()

            Main.Open_File()


        End Function
        Public Function Open_File_LVjs()

            Main.Open_File_LV()


        End Function

        'reset save Path for Live Player
        Public Function resetPathLV()

            Main.SaveLivePlayerPath = ""

        End Function

        ' Progress Window Logic Start

        Public Function set_progress(progress As Integer, info As String)

            ProgressBar.Progress = progress
            ProgressBar.ProgressText = info


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

        Public Function Open_FileDirectory()
            Main.Open_FileDirectory_Native()
            Return True
        End Function

        'Triggers New File Save intead of writing to an existing file
        Public Function AsNewTrigger()

            If Main.FromOpenFile Then
                Return False
            End If

            Main.asNew = True
            Main.SavePath = ""
            Return True
        End Function


        Public msgChache As String
        Public Function set_toastMessage(ms As String)
            msgChache = ms
        End Function
        Public Function show_toast()
            NotificationManager.Show(Main, msgChache, Color.Green, 2000)
            msgChache = ""
        End Function



        ' ================================================
        'New Implementation of PORT Channel managment ====
        ' ================================================



        Public Function add_comport(index As Integer, portname As String)
            AddComPortAt(index, portname, Main)
            Return True
        End Function


        ' UDP


        Public Function add_udpchannel(index As Integer, address As String, port As Integer)
            AddUdpChannelAt(index, address, port, Main)
            Return True
        End Function


        Public Function get_udplist_json() As String

            Return GetUdpListJson()
        End Function

        Public Function get_comlist() As String

            Return GetSystemComPortsJson()
        End Function

        Public Function get_comports() As String

            Return GetManagedComPortsJson()
        End Function




    End Class

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        WebView21.ExecuteScriptAsync("edit_template()")
        template_menu.Close()

    End Sub

    Private Sub CancelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelToolStripMenuItem.Click, ToolStripMenuItem18.Click
        template_menu.Close()
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click, ToolStripMenuItem17.Click
        Dim confirm_remove As DialogResult
        confirm_remove = MessageBox.Show("You are about to remove this template", "Remove Template ", MessageBoxButtons.YesNo
                                         )

        If confirm_remove = DialogResult.Yes Then
            WebView21.ExecuteScriptAsync("remove_template()")

        ElseIf confirm_remove = DialogResult.No Then
            'If you want to do something

        End If



    End Sub

    Private Sub ToolStripMenuItem4_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem4.Click
        WebView21.ExecuteScriptAsync("remove_content()")
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click

        WebView21.ExecuteScriptAsync("open_edit_for(timeline_data[selected_track_index].sub_tracks[selected_content.getAttribute('content_id')], 'edit')")
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        WebView21.ExecuteScriptAsync("copy_content()")
    End Sub

    Private Sub ToolStripMenuItem12_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem12.Click
        WebView21.ExecuteScriptAsync("paste_content()")
    End Sub


    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        Dim confirm_remove As DialogResult
        confirm_remove = MessageBox.Show("You are about to remove this track? All Contents of it would be removed as well", "Remove Track ", MessageBoxButtons.YesNo
                                         )

        If confirm_remove = DialogResult.Yes Then
            WebView21.ExecuteScriptAsync("remove_track()")

        ElseIf confirm_remove = DialogResult.No Then
            'If you want to do something

        End If
    End Sub


    Dim asNew = False

    Public Sub Save_File()

        If SavePath = "" Or asNew = True Then

            SaveFileDialog1.Filter = "LSYS Files (*.lsys*)|*.lsys"
            If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, data_string, False)
                SavePath = SaveFileDialog1.FileName
                asNew = False
            Else
                If SavePath = "" Then
                    asNew = True
                End If
            End If



        Else
            My.Computer.FileSystem.WriteAllText(SavePath, data_string, False)
            NotificationManager.Show(Me, "Saving File: " & SavePath, Color.Green, 2000)
            'MsgBox("File saved! to " + SavePath)
        End If

        FromOpenFile = False

    End Sub



    Public Sub Save_File_LV()

        If SaveLivePlayerPath = "" Or asNew = True Then
            SaveFileDialog2.Filter = "Live Player Templates file Files (*.lytemp*)|*.lytemp"
            If SaveFileDialog2.ShowDialog = Windows.Forms.DialogResult.OK Then
                My.Computer.FileSystem.WriteAllText(SaveFileDialog2.FileName, data_string, False)
                SaveLivePlayerPath = SaveFileDialog2.FileName
                asNew = False
            Else
                If SaveLivePlayerPath = "" Then
                    asNew = True
                End If
            End If



        Else
            My.Computer.FileSystem.WriteAllText(SaveLivePlayerPath, data_string, False)
            NotificationManager.Show(Me, "Saving File: " & SaveLivePlayerPath, Color.Green, 2000)
            'MsgBox("File saved! to " + SaveLivePlayerPath)
        End If



    End Sub

    Public Async Sub Open_FileDirectory_Native()
        ' directoryPicker.Filter = "LSYS Files (*.lsys*)|*.lsys"
        If directoryPicker.ShowDialog <> Windows.Forms.DialogResult.Cancel Then

            dirPath = AutoEscapeString(directoryPicker.FileName)
            Await WebView21.ExecuteScriptAsync("setMediaPath(""" & dirPath & """)")


            NotificationManager.Show(Me, "File: " & directoryPicker.FileName & " have been selected.", Color.Green, 2000)


        End If

    End Sub
    Public Sub Open_File()
        OpenFileDialog1.Filter = "LSYS Files (*.lsys*)|*.lsys"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)

            ProgressBar.Show()

            WebView21.ExecuteScriptAsync("load_from_file('" + fileReader + "')")

            SavePath = OpenFileDialog1.FileName

            NotificationManager.Show(Me, "File: " & OpenFileDialog1.FileName & "is Now Loading.", Color.Green, 2000)
            FromOpenFile = False
            asNew = False

        End If

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



        Dim fileReader As String = My.Computer.FileSystem.ReadAllText(filepath)
        ProgressBar.Show(Me)
        ProgressBar.BringToFront()
        WebView21.ExecuteScriptAsync("load_from_file('" + fileReader + "')")
        SavePath = filepath
        NotificationManager.Show(Me, "File: " & OpenFileDialog1.FileName & "is Now Loading.", Color.Green, 2000)
        FromOpenFile = False

    End Sub

    Public Sub queOpenFrom(filePath As String)
        FromOpenFile = True
        OpenQuePath = filePath


    End Sub



    Public Sub Open_File_LV()
        OpenFileDialog1.Filter = "LSYS temp Files (*.lytemp*)|*.lytemp"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)



            WebView21.ExecuteScriptAsync("load_LV_from_file('" + fileReader + "')")
            SaveLivePlayerPath = OpenFileDialog1.FileName
            NotificationManager.Show(Me, "File: " & OpenFileDialog1.FileName & "is Now Loading.", Color.Green, 2000)


        End If

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Open_File()
    End Sub

    Private Sub PortConfigurationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortConfigurationToolStripMenuItem.Click
        Form1.Show()
    End Sub

    Private Sub AddNewTrackToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddNewTrackToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("add_track()")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("save_to_file()")
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        asNew = True
        WebView21.ExecuteScriptAsync("save_to_file()")
    End Sub

    Private Sub PortChannelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortChannelToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("edit_track_option()")
    End Sub

    Private Sub content_menu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles content_menu.Opening

    End Sub

    Private Sub ToolStripMenuItem11_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ToolStripMenuItem11_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem11.Click

        Dim index_at As Integer
        Try
            index_at = Integer.Parse(index_position.Text)
        Catch ex As Exception
            MsgBox("Enter a Valid Positive Integer from 0 and up! ")

            Return
        End Try

        WebView21.ExecuteScriptAsync("duplicateTrack(" & index_at & ")")

    End Sub

    Private Sub AfterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AfterToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("duplicateTrack('after')")
    End Sub

    Private Sub BeforeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BeforeToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("duplicateTrack('before')")
    End Sub

    Private Sub StartOfTimelineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartOfTimelineToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("duplicateTrack(0)")
    End Sub

    Private Sub EndToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EndToolStripMenuItem.Click
        WebView21.ExecuteScriptAsync("duplicateTrack()")
    End Sub

    Private Sub SelectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectToolStripMenuItem.Click


        Dim index_at As Integer


        If insert_track_index.Text.Length <= 0 Then
            WebView21.ExecuteScriptAsync("add_track()")
            Return
        End If

        Try
            index_at = Integer.Parse(insert_track_index.Text)
        Catch ex As Exception
            MsgBox("Enter a Valid Positive Integer from 0 and up! or Leave Blank")

            Return
        End Try

        WebView21.ExecuteScriptAsync("add_track(" & index_at & ")")


    End Sub

    Private Sub ToolStripMenuItem13_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem13.Click

        WebView21.ExecuteScriptAsync("add_track()")


    End Sub

    Private Sub ToolStripMenuItem14_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem14.Click
        track_options.Show(Point.X, Point.Y)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles manual_.Click
        WebView21.ExecuteScriptAsync("modeSelect('manual')")
    End Sub

    Private Sub timeline__Click(sender As Object, e As EventArgs) Handles timeline_.Click
        WebView21.ExecuteScriptAsync("modeSelect('timeline')")
    End Sub

    Private Sub ToolStripMenuItem15_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem15.Click
        WebView21.ExecuteScriptAsync("createDialogue('settings','')")
    End Sub

    Private Sub sendToTemplates_Click(sender As Object, e As EventArgs) Handles sendToTemplates.Click
        WebView21.ExecuteScriptAsync("sendToTimelineTemplates()")
    End Sub

    Private Sub ToolStripMenuItem19_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem19.Click
        WebView21.ExecuteScriptAsync("sendToTimeTemplates()")
    End Sub

    Private Sub ToolStripMenuItem20_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem20.Click
        WebView21.ExecuteScriptAsync("sendToManualTemplates()")
    End Sub

    Private Sub ToolStripMenuItem16_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem16.Click
        WebView21.ExecuteScriptAsync("edit_template()")
        template_menu.Close()
    End Sub

    Private Sub LocalPathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LocalPathToolStripMenuItem.Click
        MsgBox(locationData)
    End Sub

    Private Sub ToolStripImportTemplate_Click(sender As Object, e As EventArgs) Handles ToolStripImportTemplate.Click
        WebView21.ExecuteScriptAsync("openImportTemplates()")
    End Sub

    Private Sub UseWebEngine1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseWebEngine1ToolStripMenuItem.Click

        Dim confirm_change As DialogResult
        confirm_change = MessageBox.Show("Switch to Webview Virtual Host (Some Features might not work properly), this will refresh the whole interface.", "Host Change", MessageBoxButtons.YesNo
                                         )

        If confirm_change = DialogResult.Yes Then
            WebView21.CoreWebView2.Navigate("https://lisyq/main.html")
            SavePath = ""
            SaveLivePlayerPath = ""


        ElseIf confirm_change = DialogResult.No Then
            'If you want to do something

        End If


    End Sub

    Private Sub UseWebEngine2ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseWebEngine2ToolStripMenuItem.Click

        Dim confirm_change As DialogResult
        confirm_change = MessageBox.Show("Switch to Webview Local File Based Host, this will refresh the whole interface.", "Host Change", MessageBoxButtons.YesNo
                                         )

        If confirm_change = DialogResult.Yes Then

            WebView21.CoreWebView2.Navigate("file:///" & relativeLocation & "/main.html")
            SavePath = ""
            SaveLivePlayerPath = ""

        ElseIf confirm_change = DialogResult.No Then
            'If you want to do something

        End If


    End Sub

    Private Sub ClearBuffersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearBuffersToolStripMenuItem.Click
        For xc = 0 To Cports.Ports.Count - 1
            Try
                Cports.Ports(xc).DiscardOutBuffer()

            Catch ex As Exception

            End Try


        Next

    End Sub

    Private Sub ToolStripMenuItem21_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem21.Click
        MsgBox("Coming Soon...")
    End Sub

    Private Sub ToolStripMenuItem22_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem22.Click
        MsgBox(relativeLocation)
    End Sub

    Private Sub ToolStripMenuItem23_Click(sender As Object, e As EventArgs)
        Dim args = My.Application.CommandLineArgs()

        If args.Count() > 0 Then


            Dim filePath = args(0).ToString

            Try
                Dim fileContents As String = File.ReadAllText(filePath)
                ' Now you have the contents of the file at your disposal.
                ' Proceed with your cosmic manipulations here.
                'MsgBox(fileContents)
                MsgBox(filePath)

            Catch ex As Exception
                ' Handle any potential errors gracefully.
                Console.WriteLine("An error occurred while reading the file: " & ex.Message)
            End Try



        End If





    End Sub

    Private Sub openNest_Click_1(sender As Object, e As EventArgs) Handles openNest.Click



        NestMain.Show()
        NestMain.WindowState = FormWindowState.Normal

        Me.WindowState = FormWindowState.Minimized


    End Sub

    Private Sub ToolStripMenuItem23_Click_1(sender As Object, e As EventArgs) Handles ToolStripMenuItem23.Click


        Dim confirm_newproject As DialogResult

        If SavePath.Length > 0 Then
            confirm_newproject = MessageBox.Show("Are you sure to start a new Project File? This will clear your current work so make sure to save it.", "New Project", MessageBoxButtons.YesNo)
        Else
            Return
        End If


        If confirm_newproject = DialogResult.Yes Then
            SavePath = ""
            asNew = True
            WebView21.ExecuteScriptAsync("new_refresh(true)")

        Else
            NotificationManager.Show(Me, "Action Canceled", Color.Yellow, 1500)

        End If



    End Sub

    Private Sub ToolStripMenuItem24_Click(sender As Object, e As EventArgs) Handles DMX_Patcher.Click
        WebView21.ExecuteScriptAsync("openDMXConfig()")
    End Sub
End Class