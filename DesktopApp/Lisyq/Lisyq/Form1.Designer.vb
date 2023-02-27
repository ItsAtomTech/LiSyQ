<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ports_display = New System.Windows.Forms.ComboBox()
        Me.ScanPorts = New System.Windows.Forms.Button()
        Me.Set_button = New System.Windows.Forms.Button()
        Me.discon = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel_1 = New System.Windows.Forms.Panel()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.com_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.p_num_ch = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.re_connect = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel_1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ports_display
        '
        Me.ports_display.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.ports_display.FormattingEnabled = True
        Me.ports_display.Location = New System.Drawing.Point(13, 22)
        Me.ports_display.Name = "ports_display"
        Me.ports_display.Size = New System.Drawing.Size(121, 21)
        Me.ports_display.TabIndex = 0
        '
        'ScanPorts
        '
        Me.ScanPorts.Location = New System.Drawing.Point(141, 19)
        Me.ScanPorts.Name = "ScanPorts"
        Me.ScanPorts.Size = New System.Drawing.Size(75, 23)
        Me.ScanPorts.TabIndex = 1
        Me.ScanPorts.Text = "Scan Ports"
        Me.ScanPorts.UseVisualStyleBackColor = True
        '
        'Set_button
        '
        Me.Set_button.Location = New System.Drawing.Point(141, 49)
        Me.Set_button.Name = "Set_button"
        Me.Set_button.Size = New System.Drawing.Size(75, 23)
        Me.Set_button.TabIndex = 2
        Me.Set_button.Text = "Connect"
        Me.Set_button.UseVisualStyleBackColor = True
        '
        'discon
        '
        Me.discon.Location = New System.Drawing.Point(321, 162)
        Me.discon.Name = "discon"
        Me.discon.Size = New System.Drawing.Size(75, 23)
        Me.discon.TabIndex = 3
        Me.discon.Text = "Disconnect"
        Me.discon.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Help
        Me.Label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Label1.Location = New System.Drawing.Point(222, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 67)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Scan Ports to show connected" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Devices." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Select the COM port then " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "click Connec" &
    "t."
        '
        'Panel_1
        '
        Me.Panel_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel_1.Controls.Add(Me.DataGridView1)
        Me.Panel_1.Location = New System.Drawing.Point(4, 162)
        Me.Panel_1.Name = "Panel_1"
        Me.Panel_1.Size = New System.Drawing.Size(311, 108)
        Me.Panel_1.TabIndex = 6
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.com_name, Me.p_num_ch})
        Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(307, 104)
        Me.DataGridView1.TabIndex = 0
        '
        'com_name
        '
        Me.com_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.com_name.HeaderText = "COM NAME"
        Me.com_name.Name = "com_name"
        Me.com_name.ReadOnly = True
        '
        'p_num_ch
        '
        Me.p_num_ch.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.p_num_ch.HeaderText = "PORT NUM Ch."
        Me.p_num_ch.Name = "p_num_ch"
        Me.p_num_ch.ReadOnly = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(13, 105)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 7
        Me.Button1.Text = "Test"
        Me.Button1.UseVisualStyleBackColor = True
        '
        're_connect
        '
        Me.re_connect.Location = New System.Drawing.Point(321, 192)
        Me.re_connect.Name = "re_connect"
        Me.re_connect.Size = New System.Drawing.Size(75, 23)
        Me.re_connect.TabIndex = 8
        Me.re_connect.Text = "Re-Connect"
        Me.re_connect.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.re_connect.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(321, 221)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Refresh"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 276)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.re_connect)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel_1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.discon)
        Me.Controls.Add(Me.Set_button)
        Me.Controls.Add(Me.ScanPorts)
        Me.Controls.Add(Me.ports_display)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(414, 314)
        Me.MinimumSize = New System.Drawing.Size(414, 314)
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Port Config"
        Me.Panel_1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ports_display As ComboBox
    Friend WithEvents ScanPorts As Button
    Friend WithEvents Set_button As Button
    Friend WithEvents discon As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel_1 As Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents com_name As DataGridViewTextBoxColumn
    Friend WithEvents p_num_ch As DataGridViewTextBoxColumn
    Friend WithEvents Button1 As Button
    Friend WithEvents re_connect As Button
    Friend WithEvents Button2 As Button
End Class
