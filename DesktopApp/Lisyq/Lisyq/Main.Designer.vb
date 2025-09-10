<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.WebView21 = New Microsoft.Web.WebView2.WinForms.WebView2()
        Me.template_menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem20 = New System.Windows.Forms.ToolStripMenuItem()
        Me.content_menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem14 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CancelToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.sendToTemplates = New System.Windows.Forms.ToolStripMenuItem()
        Me.track_options = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem13 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem()
        Me.insert_track_index = New System.Windows.Forms.ToolStripTextBox()
        Me.SelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem8 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AfterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeforeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AtPositionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.index_position = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripMenuItem11 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartOfTimelineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EndToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PortChannelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripMenuItem12 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem9 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem23 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PortConfigurationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DMX_Patcher = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClearBuffersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripImportTemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem21 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem22 = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocalPathToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseWebEngine1ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseWebEngine2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimelineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddNewTrackToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.openNest = New System.Windows.Forms.Button()
        Me.timeline_ = New System.Windows.Forms.Button()
        Me.manual_ = New System.Windows.Forms.Button()
        Me.template_menu_manual = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem16 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem17 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem18 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem19 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileDialog2 = New System.Windows.Forms.SaveFileDialog()
        Me.directoryPicker = New System.Windows.Forms.OpenFileDialog()
        Me.ToolStripMenuItem24 = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.template_menu.SuspendLayout()
        Me.content_menu.SuspendLayout()
        Me.track_options.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.template_menu_manual.SuspendLayout()
        Me.SuspendLayout()
        '
        'WebView21
        '
        Me.WebView21.AllowExternalDrop = True
        Me.WebView21.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebView21.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.WebView21.CreationProperties = Nothing
        Me.WebView21.DefaultBackgroundColor = System.Drawing.Color.DimGray
        Me.WebView21.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.WebView21.Location = New System.Drawing.Point(0, 27)
        Me.WebView21.Name = "WebView21"
        Me.WebView21.Padding = New System.Windows.Forms.Padding(5, 0, 0, 0)
        Me.WebView21.Size = New System.Drawing.Size(800, 423)
        Me.WebView21.TabIndex = 0
        Me.WebView21.ZoomFactor = 1.0R
        '
        'template_menu
        '
        Me.template_menu.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.template_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.CancelToolStripMenuItem, Me.ToolStripMenuItem20})
        Me.template_menu.Name = "template_menu"
        Me.template_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.template_menu.Size = New System.Drawing.Size(198, 92)
        Me.template_menu.Text = "Template Options"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem1.Image = Global.Lisyq.My.Resources.Resources.edit1
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem1.Text = "Edit Template"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem2.Text = "Remove"
        '
        'CancelToolStripMenuItem
        '
        Me.CancelToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem"
        Me.CancelToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.CancelToolStripMenuItem.Text = "Cancel"
        '
        'ToolStripMenuItem20
        '
        Me.ToolStripMenuItem20.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem20.Name = "ToolStripMenuItem20"
        Me.ToolStripMenuItem20.Size = New System.Drawing.Size(197, 22)
        Me.ToolStripMenuItem20.Text = "Send to Manual Templ."
        '
        'content_menu
        '
        Me.content_menu.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.content_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem3, Me.ToolStripMenuItem4, Me.ToolStripMenuItem5, Me.ToolStripMenuItem14, Me.CancelToolStripMenuItem1, Me.sendToTemplates})
        Me.content_menu.Name = "template_menu"
        Me.content_menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.content_menu.Size = New System.Drawing.Size(169, 136)
        Me.content_menu.Text = "Content Options"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItem3.Text = "Edit"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItem4.Text = "Remove"
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItem5.Text = "Copy"
        '
        'ToolStripMenuItem14
        '
        Me.ToolStripMenuItem14.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem14.Name = "ToolStripMenuItem14"
        Me.ToolStripMenuItem14.Size = New System.Drawing.Size(168, 22)
        Me.ToolStripMenuItem14.Text = "Track Options"
        '
        'CancelToolStripMenuItem1
        '
        Me.CancelToolStripMenuItem1.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.CancelToolStripMenuItem1.Name = "CancelToolStripMenuItem1"
        Me.CancelToolStripMenuItem1.Size = New System.Drawing.Size(168, 22)
        Me.CancelToolStripMenuItem1.Text = "Cancel"
        '
        'sendToTemplates
        '
        Me.sendToTemplates.BackColor = System.Drawing.Color.Transparent
        Me.sendToTemplates.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.sendToTemplates.Name = "sendToTemplates"
        Me.sendToTemplates.Size = New System.Drawing.Size(168, 22)
        Me.sendToTemplates.Text = "Add to Templates"
        '
        'track_options
        '
        Me.track_options.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.track_options.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem13, Me.ToolStripMenuItem10, Me.ToolStripMenuItem8, Me.ToolStripMenuItem6, Me.ToolStripMenuItem7, Me.ToolStripSeparator1, Me.ToolStripMenuItem12, Me.ToolStripMenuItem9})
        Me.track_options.Name = "template_menu"
        Me.track_options.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.track_options.Size = New System.Drawing.Size(177, 164)
        Me.track_options.Text = "Track Options"
        '
        'ToolStripMenuItem13
        '
        Me.ToolStripMenuItem13.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem13.Name = "ToolStripMenuItem13"
        Me.ToolStripMenuItem13.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem13.Text = "Add New Track"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.insert_track_index, Me.SelectToolStripMenuItem})
        Me.ToolStripMenuItem10.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem10.Text = "Add New Track At: "
        '
        'insert_track_index
        '
        Me.insert_track_index.Name = "insert_track_index"
        Me.insert_track_index.Size = New System.Drawing.Size(100, 23)
        '
        'SelectToolStripMenuItem
        '
        Me.SelectToolStripMenuItem.Name = "SelectToolStripMenuItem"
        Me.SelectToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.SelectToolStripMenuItem.Text = "Select"
        '
        'ToolStripMenuItem8
        '
        Me.ToolStripMenuItem8.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AfterToolStripMenuItem, Me.BeforeToolStripMenuItem, Me.AtPositionToolStripMenuItem, Me.StartOfTimelineToolStripMenuItem, Me.EndToolStripMenuItem})
        Me.ToolStripMenuItem8.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem8.Name = "ToolStripMenuItem8"
        Me.ToolStripMenuItem8.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem8.Text = "Duplicate"
        '
        'AfterToolStripMenuItem
        '
        Me.AfterToolStripMenuItem.Name = "AfterToolStripMenuItem"
        Me.AfterToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.AfterToolStripMenuItem.Text = "After"
        '
        'BeforeToolStripMenuItem
        '
        Me.BeforeToolStripMenuItem.Name = "BeforeToolStripMenuItem"
        Me.BeforeToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.BeforeToolStripMenuItem.Text = "Before"
        '
        'AtPositionToolStripMenuItem
        '
        Me.AtPositionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.index_position, Me.ToolStripMenuItem11})
        Me.AtPositionToolStripMenuItem.Name = "AtPositionToolStripMenuItem"
        Me.AtPositionToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.AtPositionToolStripMenuItem.Text = "At Position"
        '
        'index_position
        '
        Me.index_position.Name = "index_position"
        Me.index_position.Size = New System.Drawing.Size(100, 23)
        Me.index_position.ToolTipText = "Insert number From 0 and up"
        '
        'ToolStripMenuItem11
        '
        Me.ToolStripMenuItem11.Name = "ToolStripMenuItem11"
        Me.ToolStripMenuItem11.Size = New System.Drawing.Size(160, 22)
        Me.ToolStripMenuItem11.Text = "Select"
        '
        'StartOfTimelineToolStripMenuItem
        '
        Me.StartOfTimelineToolStripMenuItem.Name = "StartOfTimelineToolStripMenuItem"
        Me.StartOfTimelineToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.StartOfTimelineToolStripMenuItem.Text = "Start of Timeline"
        '
        'EndToolStripMenuItem
        '
        Me.EndToolStripMenuItem.Name = "EndToolStripMenuItem"
        Me.EndToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.EndToolStripMenuItem.Text = "End"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PortChannelToolStripMenuItem})
        Me.ToolStripMenuItem6.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem6.Text = "Edit Track"
        '
        'PortChannelToolStripMenuItem
        '
        Me.PortChannelToolStripMenuItem.BackColor = System.Drawing.SystemColors.Menu
        Me.PortChannelToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.PortChannelToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PortChannelToolStripMenuItem.Name = "PortChannelToolStripMenuItem"
        Me.PortChannelToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.PortChannelToolStripMenuItem.Text = "Port and Channel"
        '
        'ToolStripMenuItem7
        '
        Me.ToolStripMenuItem7.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem7.Name = "ToolStripMenuItem7"
        Me.ToolStripMenuItem7.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem7.Text = "Remove"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(173, 6)
        '
        'ToolStripMenuItem12
        '
        Me.ToolStripMenuItem12.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem12.Name = "ToolStripMenuItem12"
        Me.ToolStripMenuItem12.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem12.Text = "Paste(Content)"
        '
        'ToolStripMenuItem9
        '
        Me.ToolStripMenuItem9.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem9.Name = "ToolStripMenuItem9"
        Me.ToolStripMenuItem9.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem9.Text = "Cancel"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "lsys"
        Me.SaveFileDialog1.FileName = "Light Sequence"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "lsys"
        Me.OpenFileDialog1.Filter = "LyqFiles | *lsys"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.MenuStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ToolsToolStripMenuItem, Me.TimelineToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.MinimumSize = New System.Drawing.Size(800, 27)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 27)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.TabStop = True
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem23, Me.OpenToolStripMenuItem, Me.ToolStripMenuItem15, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem})
        Me.FileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 23)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ToolStripMenuItem23
        '
        Me.ToolStripMenuItem23.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripMenuItem23.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripMenuItem23.Name = "ToolStripMenuItem23"
        Me.ToolStripMenuItem23.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem23.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.OpenToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripMenuItem15.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(184, 22)
        Me.ToolStripMenuItem15.Text = "Settings and Options"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.SaveToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.SaveAsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As"
        '
        'ToolsToolStripMenuItem
        '
        Me.ToolsToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.ToolsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem24, Me.PortConfigurationToolStripMenuItem, Me.DMX_Patcher, Me.ClearBuffersToolStripMenuItem, Me.ToolStripImportTemplate, Me.ToolStripMenuItem21, Me.ToolStripMenuItem22, Me.LocalPathToolStripMenuItem, Me.UseWebEngine1ToolStripMenuItem, Me.UseWebEngine2ToolStripMenuItem})
        Me.ToolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem"
        Me.ToolsToolStripMenuItem.Size = New System.Drawing.Size(48, 23)
        Me.ToolsToolStripMenuItem.Text = "Tools"
        '
        'PortConfigurationToolStripMenuItem
        '
        Me.PortConfigurationToolStripMenuItem.BackColor = System.Drawing.Color.DimGray
        Me.PortConfigurationToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.PortConfigurationToolStripMenuItem.Name = "PortConfigurationToolStripMenuItem"
        Me.PortConfigurationToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.PortConfigurationToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.PortConfigurationToolStripMenuItem.Text = "Port Configuration (Legacy)"
        '
        'DMX_Patcher
        '
        Me.DMX_Patcher.BackColor = System.Drawing.Color.DimGray
        Me.DMX_Patcher.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.DMX_Patcher.Name = "DMX_Patcher"
        Me.DMX_Patcher.Size = New System.Drawing.Size(296, 22)
        Me.DMX_Patcher.Text = "DMX Config Patcher"
        '
        'ClearBuffersToolStripMenuItem
        '
        Me.ClearBuffersToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ClearBuffersToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClearBuffersToolStripMenuItem.Name = "ClearBuffersToolStripMenuItem"
        Me.ClearBuffersToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.ClearBuffersToolStripMenuItem.Text = "Clear Buffers"
        '
        'ToolStripImportTemplate
        '
        Me.ToolStripImportTemplate.AutoToolTip = True
        Me.ToolStripImportTemplate.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripImportTemplate.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripImportTemplate.Name = "ToolStripImportTemplate"
        Me.ToolStripImportTemplate.Size = New System.Drawing.Size(296, 22)
        Me.ToolStripImportTemplate.Text = "Import Templates"
        Me.ToolStripImportTemplate.ToolTipText = "Import Templates from a file selected"
        '
        'ToolStripMenuItem21
        '
        Me.ToolStripMenuItem21.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripMenuItem21.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripMenuItem21.Name = "ToolStripMenuItem21"
        Me.ToolStripMenuItem21.Size = New System.Drawing.Size(296, 22)
        Me.ToolStripMenuItem21.Text = "Plugin Manager"
        Me.ToolStripMenuItem21.ToolTipText = "Install and manage Plugins"
        '
        'ToolStripMenuItem22
        '
        Me.ToolStripMenuItem22.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.ToolStripMenuItem22.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem22.Name = "ToolStripMenuItem22"
        Me.ToolStripMenuItem22.Size = New System.Drawing.Size(296, 22)
        Me.ToolStripMenuItem22.Text = "Local Path"
        '
        'LocalPathToolStripMenuItem
        '
        Me.LocalPathToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LocalPathToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.LocalPathToolStripMenuItem.Name = "LocalPathToolStripMenuItem"
        Me.LocalPathToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.LocalPathToolStripMenuItem.Text = "Data Path"
        '
        'UseWebEngine1ToolStripMenuItem
        '
        Me.UseWebEngine1ToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.UseWebEngine1ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.UseWebEngine1ToolStripMenuItem.Name = "UseWebEngine1ToolStripMenuItem"
        Me.UseWebEngine1ToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.UseWebEngine1ToolStripMenuItem.Text = "Use Web Engine 1 "
        '
        'UseWebEngine2ToolStripMenuItem
        '
        Me.UseWebEngine2ToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.UseWebEngine2ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.UseWebEngine2ToolStripMenuItem.Name = "UseWebEngine2ToolStripMenuItem"
        Me.UseWebEngine2ToolStripMenuItem.Size = New System.Drawing.Size(296, 22)
        Me.UseWebEngine2ToolStripMenuItem.Text = "Use Web Engine 2"
        '
        'TimelineToolStripMenuItem
        '
        Me.TimelineToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TimelineToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewTrackToolStripMenuItem})
        Me.TimelineToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.TimelineToolStripMenuItem.Name = "TimelineToolStripMenuItem"
        Me.TimelineToolStripMenuItem.Size = New System.Drawing.Size(65, 23)
        Me.TimelineToolStripMenuItem.Text = "Timeline"
        '
        'AddNewTrackToolStripMenuItem
        '
        Me.AddNewTrackToolStripMenuItem.BackColor = System.Drawing.Color.DimGray
        Me.AddNewTrackToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.AddNewTrackToolStripMenuItem.Name = "AddNewTrackToolStripMenuItem"
        Me.AddNewTrackToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.AddNewTrackToolStripMenuItem.Text = "Add New Track"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Controls.Add(Me.openNest)
        Me.Panel1.Controls.Add(Me.timeline_)
        Me.Panel1.Controls.Add(Me.manual_)
        Me.Panel1.Controls.Add(Me.MenuStrip1)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 27)
        Me.Panel1.TabIndex = 4
        '
        'openNest
        '
        Me.openNest.BackColor = System.Drawing.Color.DimGray
        Me.openNest.Cursor = System.Windows.Forms.Cursors.Hand
        Me.openNest.Dock = System.Windows.Forms.DockStyle.Right
        Me.openNest.FlatAppearance.BorderSize = 0
        Me.openNest.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.openNest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.openNest.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.openNest.Location = New System.Drawing.Point(523, 0)
        Me.openNest.Name = "openNest"
        Me.openNest.Size = New System.Drawing.Size(92, 27)
        Me.openNest.TabIndex = 7
        Me.openNest.Text = "Nest.in Player"
        Me.openNest.UseVisualStyleBackColor = False
        '
        'timeline_
        '
        Me.timeline_.BackColor = System.Drawing.Color.DimGray
        Me.timeline_.Cursor = System.Windows.Forms.Cursors.Hand
        Me.timeline_.Dock = System.Windows.Forms.DockStyle.Right
        Me.timeline_.FlatAppearance.BorderSize = 0
        Me.timeline_.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.timeline_.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.timeline_.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.timeline_.Location = New System.Drawing.Point(615, 0)
        Me.timeline_.Name = "timeline_"
        Me.timeline_.Size = New System.Drawing.Size(92, 27)
        Me.timeline_.TabIndex = 5
        Me.timeline_.Text = "Timeline Panel"
        Me.timeline_.UseVisualStyleBackColor = False
        '
        'manual_
        '
        Me.manual_.BackColor = System.Drawing.Color.DimGray
        Me.manual_.Cursor = System.Windows.Forms.Cursors.Hand
        Me.manual_.Dock = System.Windows.Forms.DockStyle.Right
        Me.manual_.FlatAppearance.BorderSize = 0
        Me.manual_.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.manual_.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.manual_.ForeColor = System.Drawing.SystemColors.ButtonFace
        Me.manual_.Location = New System.Drawing.Point(707, 0)
        Me.manual_.Name = "manual_"
        Me.manual_.Size = New System.Drawing.Size(93, 27)
        Me.manual_.TabIndex = 4
        Me.manual_.Text = "Manual Panel"
        Me.manual_.UseVisualStyleBackColor = False
        '
        'template_menu_manual
        '
        Me.template_menu_manual.BackColor = System.Drawing.SystemColors.WindowFrame
        Me.template_menu_manual.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem16, Me.ToolStripMenuItem17, Me.ToolStripMenuItem18, Me.ToolStripMenuItem19})
        Me.template_menu_manual.Name = "template_menu"
        Me.template_menu_manual.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.template_menu_manual.Size = New System.Drawing.Size(203, 92)
        Me.template_menu_manual.Text = "Template Options"
        '
        'ToolStripMenuItem16
        '
        Me.ToolStripMenuItem16.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem16.Image = Global.Lisyq.My.Resources.Resources.edit1
        Me.ToolStripMenuItem16.Name = "ToolStripMenuItem16"
        Me.ToolStripMenuItem16.Size = New System.Drawing.Size(202, 22)
        Me.ToolStripMenuItem16.Text = "Edit Template"
        '
        'ToolStripMenuItem17
        '
        Me.ToolStripMenuItem17.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem17.Name = "ToolStripMenuItem17"
        Me.ToolStripMenuItem17.Size = New System.Drawing.Size(202, 22)
        Me.ToolStripMenuItem17.Text = "Remove"
        '
        'ToolStripMenuItem18
        '
        Me.ToolStripMenuItem18.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem18.Name = "ToolStripMenuItem18"
        Me.ToolStripMenuItem18.Size = New System.Drawing.Size(202, 22)
        Me.ToolStripMenuItem18.Text = "Cancel"
        '
        'ToolStripMenuItem19
        '
        Me.ToolStripMenuItem19.ForeColor = System.Drawing.SystemColors.ControlLight
        Me.ToolStripMenuItem19.Name = "ToolStripMenuItem19"
        Me.ToolStripMenuItem19.Size = New System.Drawing.Size(202, 22)
        Me.ToolStripMenuItem19.Text = "Add To Timeline Templ."
        '
        'SaveFileDialog2
        '
        Me.SaveFileDialog2.DefaultExt = "lytemp"
        Me.SaveFileDialog2.FileName = "Live Template File"
        '
        'directoryPicker
        '
        Me.directoryPicker.Title = "Directory Picker"
        '
        'ToolStripMenuItem24
        '
        Me.ToolStripMenuItem24.BackColor = System.Drawing.Color.DimGray
        Me.ToolStripMenuItem24.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.ToolStripMenuItem24.Name = "ToolStripMenuItem24"
        Me.ToolStripMenuItem24.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.ToolStripMenuItem24.Size = New System.Drawing.Size(296, 22)
        Me.ToolStripMenuItem24.Text = "Port Configuration"
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.WebView21)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(300, 488)
        Me.Name = "Main"
        Me.Text = "LiSyQ Studio v1.2"
        CType(Me.WebView21, System.ComponentModel.ISupportInitialize).EndInit()
        Me.template_menu.ResumeLayout(False)
        Me.content_menu.ResumeLayout(False)
        Me.track_options.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.template_menu_manual.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents WebView21 As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents template_menu As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
    Friend WithEvents CancelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents content_menu As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem3 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripMenuItem
    Friend WithEvents CancelToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents track_options As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem10 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem7 As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem12 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem9 As ToolStripMenuItem
    Private WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ToolsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TimelineToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PortConfigurationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AddNewTrackToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PortChannelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem8 As ToolStripMenuItem
    Friend WithEvents AfterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BeforeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AtPositionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents index_position As ToolStripTextBox
    Friend WithEvents StartOfTimelineToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EndToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem11 As ToolStripMenuItem
    Friend WithEvents insert_track_index As ToolStripTextBox
    Friend WithEvents SelectToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem13 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem14 As ToolStripMenuItem
    Friend WithEvents timeline_ As Button
    Friend WithEvents manual_ As Button
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem15 As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents sendToTemplates As ToolStripMenuItem
    Friend WithEvents template_menu_manual As ContextMenuStrip
    Friend WithEvents ToolStripMenuItem16 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem17 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem18 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem19 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem20 As ToolStripMenuItem
    Friend WithEvents LocalPathToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UseWebEngine1ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UseWebEngine2ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClearBuffersToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripImportTemplate As ToolStripMenuItem
    Private WithEvents SaveFileDialog2 As SaveFileDialog
    Friend WithEvents ToolStripMenuItem21 As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem22 As ToolStripMenuItem
    Friend WithEvents directoryPicker As OpenFileDialog
    Friend WithEvents openNest As Button
    Friend WithEvents ToolStripMenuItem23 As ToolStripMenuItem
    Friend WithEvents DMX_Patcher As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem24 As ToolStripMenuItem
End Class
