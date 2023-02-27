Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load




        scan_ports()
        Update_table()




        ' Ports(0).WriteLine("")


    End Sub


    Private Sub scan_ports()
        Dim ports_avialable As String() = IO.Ports.SerialPort.GetPortNames()


        ports_display.Items.Clear()

        For Each po In ports_avialable

            ports_display.Items.Add(po)



        Next

        Try
            ports_display.SelectedIndex = 0
        Catch ex As Exception

        End Try


        'MsgBox(ports_avialable.Length)
    End Sub

    Private Sub ScanPorts_Click(sender As Object, e As EventArgs) Handles ScanPorts.Click
        scan_ports()

    End Sub

    Private Sub Set_button_Click(sender As Object, e As EventArgs) Handles Set_button.Click


        Try


            Try


                If Cports.Ports.Contains(Cports.Ports.Find(Function(p) p.PortName = ports_display.SelectedItem().ToString())) Then
                    MsgBox(ports_display.SelectedItem().ToString() & " Is already Added to Channels!")
                    Return
                End If


            Catch ex As Exception

            End Try


            If ports_display.SelectedIndex < 0 Then

                MsgBox("Select Port First!")
                Return

            End If


            Cports.Ports.Add(New IO.Ports.SerialPort)
            Cports.Ports(Cports.Ports.Count - 1).BaudRate = 115200

            Cports.Ports(Cports.Ports.Count - 1).PortName = ports_display.SelectedItem().ToString
            Cports.Ports(Cports.Ports.Count - 1).WriteTimeout = 1500
            Cports.Ports(Cports.Ports.Count - 1).ReadTimeout = 1500
            Cports.Ports(Cports.Ports.Count - 1).Open()

            'Cports.Threads.Add(New System.Threading.Thread(AddressOf Cports.SendToCOM, 0, 10)

            'Cports.Threads.Add(New System.Threading.Thread(New Threading.ThreadStart(Sub() Cports.SendToCOM())))


            NotificationManager.Show(Me, "Port Connected", Color.Green, 800)


        Catch ex As Exception
            NotificationManager.Show(Me, "Failed to Connect to Port", Color.Red, 1000)
        End Try
        ' MsgBox("Confiured Ports Count :" & Ports.Count)
        Update_table()


    End Sub


    Private Sub Update_table()

        DataGridView1.Rows.Clear()

        For xc = 0 To Cports.Ports.Count - 1

            Dim Port_name As String

            If Cports.Ports(xc).IsOpen Then
                Port_name = Cports.Ports(xc).PortName
            Else
                Port_name = Cports.Ports(xc).PortName & " (Not Connected)"
            End If

            Dim Data = {Port_name, xc}

            DataGridView1.Rows.Add(Data)


        Next


    End Sub


    Private Sub discon_Click(sender As Object, e As EventArgs) Handles discon.Click

        If DataGridView1.SelectedRows.Count() <= 0 Then

            NotificationManager.Show(Me, "Select Channel First!", Color.Red, 1000)


            Return
        End If

        Dim idx As Integer = DataGridView1.CurrentRow.Index
        Dim port_h As Integer = DataGridView1.Item(1, idx).Value


        Cports.Ports(port_h).Close()
        Cports.Ports.RemoveAt(port_h)
        Update_table()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Ports(0).WriteLine("ffffffff")
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        If DataGridView1.SelectedRows.Count() <= 0 Then

            'MsgBox("Select Channel First!")
            NotificationManager.Show(Me, "Select Channel First!", Color.Red, 1000)


            Return
        End If

        Try
            Dim idx As Integer = DataGridView1.CurrentRow.Index
            Cports.Ports(DataGridView1.Item(1, idx).Value).WriteLine("ffffff")

            NotificationManager.Show(Me, "Test Data Sent!", Color.Black, 800)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub re_connect_Click(sender As Object, e As EventArgs) Handles re_connect.Click
        If DataGridView1.SelectedRows.Count() <= 0 Then

            NotificationManager.Show(Me, "Select Channel First!", Color.Red, 1000)


            Return
        End If


        Dim idx As Integer = DataGridView1.CurrentRow.Index
        Dim port_h As Integer = DataGridView1.Item(1, idx).Value

        Try
            Cports.Ports(port_h).Open()


        Catch ex As Exception

        End Try

        Update_table()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Update_table()
    End Sub
End Class
