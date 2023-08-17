Imports System.Runtime.InteropServices
Imports Microsoft.Web.WebView2.Core

Public Class Main

    Dim Point As New Point
    Dim SavePath As String = ""
    Dim SaveLivePlayerPath As String = ""
    Dim relativeLocation = Environment.CurrentDirectory

    Private Async Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Await WebView21.EnsureCoreWebView2Async
        WebView21.CoreWebView2.AddHostObjectToScript("NativeObject", New WebJsObject())
        WebView21.CoreWebView2.SetVirtualHostNameToFolderMapping("lisyq", "", CoreWebView2HostResourceAccessKind.Allow)
        WebView21.CoreWebView2.Navigate("https://lisyq/main.html")
        'WebView21.CoreWebView2.Navigate("file:///" & relativeLocation & "/main.html")


        WebView21.CoreWebView2.Settings.AreDefaultContextMenusEnabled = False
        WebView21.CoreWebView2.Settings.AreHostObjectsAllowed = True
        WebView21.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = True
        WebView21.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = True
        WebView21.CoreWebView2.Settings.IsPinchZoomEnabled = False
        WebView21.CoreWebView2.Settings.IsZoomControlEnabled = False


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
    Dim df As String




    <ClassInterface(ClassInterfaceType.None)>
    <ComVisible(True)>
    Public Class WebJsObject


        Dim phc As Integer

        Public Function Onready() As String



            Return Main.df

        End Function

        Public Function set_mouse_coords(x As Integer, y As Integer)

            Main.Point.X = x
            Main.Point.Y = y

        End Function




        ' Process output string to be sent to Ouput mediums
        Public Function outputs() As String
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
                ProgressBar.ShowDialog()

            End If



        End Function

        Public Function close_progress()
            ProgressBar.Close()
        End Function

        ' Progress Window End


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

    Public Sub Open_File()
        OpenFileDialog1.Filter = "LSYS Files (*.lsys*)|*.lsys"
        If OpenFileDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
            Dim fileReader As String = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName)

            ProgressBar.Show()

            WebView21.ExecuteScriptAsync("load_from_file('" + fileReader + "')")

            SavePath = OpenFileDialog1.FileName

            NotificationManager.Show(Me, "File: " & OpenFileDialog1.FileName & "is Now Loading.", Color.Green, 2000)


        End If

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
        MsgBox(relativeLocation)
    End Sub

    Private Sub ToolStripImportTemplate_Click(sender As Object, e As EventArgs) Handles ToolStripImportTemplate.Click
        WebView21.ExecuteScriptAsync("openImportTemplates()")
    End Sub

    Private Sub UseWebEngine1ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UseWebEngine1ToolStripMenuItem.Click

        Dim confirm_change As DialogResult
        confirm_change = MessageBox.Show("Switch to Webview Virtual Host (Default), this will refresh the whole interface.", "Host Change", MessageBoxButtons.YesNo
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


End Class