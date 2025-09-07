


Module Cports
    Public Ports As New List(Of IO.Ports.SerialPort)

    Public Threads As New List(Of System.Threading.Thread)

    Public UdpChannels As New List(Of Net.Sockets.UdpClient)


    Public Sub SendToCOM(ByVal Params As Object)
        Try
            Cports.Ports(Params(0)).WriteTimeout = 30

            Cports.Ports(Params(0)).WriteLine(Params(1))


        Catch ex As Exception

        End Try

    End Sub



    'Directly Add a Comport at specified index and comprt name
    Public Sub AddComPortAt(index As Integer, portName As String, ByRef Parent As Control)
        Try
            ' Pad with Nothing up to index if needed
            While Cports.Ports.Count < index
                Cports.Ports.Add(Nothing)
            End While

            ' Close old port if exists
            If index < Cports.Ports.Count AndAlso Cports.Ports(index) IsNot Nothing Then
                If Cports.Ports(index).IsOpen Then
                    Cports.Ports(index).Close()
                End If
            End If

            ' Create new port
            Dim newPort As New IO.Ports.SerialPort With {
            .PortName = portName,
            .BaudRate = 115200,
            .WriteTimeout = 1500,
            .ReadTimeout = 1500
        }
            newPort.Open()

            ' Replace or add
            If index < Cports.Ports.Count Then
                Cports.Ports(index) = newPort
            Else
                Cports.Ports.Add(newPort)
            End If

            NotificationManager.Show(Parent, "Port " & portName & " Connected at index " & index, Color.Green, 800)

        Catch ex As Exception
            NotificationManager.Show(Parent, "Failed to Connect " & portName, Color.Red, 1000)
        End Try
    End Sub


    Public Sub AddUdpChannelAt(index As Integer, ip As String, port As Integer, parent As Control)
        Try
            ' Pad with Nothing if necessary
            While UdpChannels.Count < index
                UdpChannels.Add(Nothing)
            End While

            ' Close old UDP client if exists
            If index < UdpChannels.Count AndAlso UdpChannels(index) IsNot Nothing Then
                UdpChannels(index).Close()
            End If

            ' Create new UDP client
            Dim udp As New Net.Sockets.UdpClient()
            udp.Connect(ip, port)

            ' Replace or add
            If index < UdpChannels.Count Then
                UdpChannels(index) = udp
            Else
                UdpChannels.Add(udp)
            End If

            ' Optional: notification (requires passing Parent control if needed)
            NotificationManager.Show(parent, $"UDP Channel {ip}:{port} added at index {index}", Color.Green, 800)


        Catch ex As Exception
            ' Handle error
            NotificationManager.Show(parent, $"Failed to add UDP {ip}:{port}", Color.Red, 1000)
        End Try

    End Sub

    Public Function GetUdpListJson() As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append("[")

        For i As Integer = 0 To UdpChannels.Count - 1
            Dim udp = UdpChannels(i)
            Dim address As String = "null"
            Dim port As String = "null"

            If udp IsNot Nothing Then
                Dim endpoint = TryCast(udp.Client.RemoteEndPoint, Net.IPEndPoint)
                If endpoint IsNot Nothing Then
                    address = """" & endpoint.Address.ToString() & """"
                    port = endpoint.Port.ToString()
                End If
            End If

            sb.Append("{")
            sb.Append("""index"":" & i & ",")
            sb.Append("""address"":" & address & ",")
            sb.Append("""port"":" & port)
            sb.Append("}")

            If i < UdpChannels.Count - 1 Then
                sb.Append(",")
            End If
        Next

        sb.Append("]")
        Return sb.ToString()
    End Function


End Module
