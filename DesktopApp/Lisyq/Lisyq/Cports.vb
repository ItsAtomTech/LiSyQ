


Module Cports
    Public Ports As New List(Of IO.Ports.SerialPort)

    Public Threads As New List(Of System.Threading.Thread)

    Public UdpChannels As New List(Of Net.Sockets.UdpClient)

    Public UdpLocks As New List(Of Object)

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

    Public Function GetSystemComPortsJson() As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append("[")

        Dim ports = IO.Ports.SerialPort.GetPortNames()

        For i As Integer = 0 To ports.Length - 1
            sb.Append("{")
            sb.Append("""index"":" & i & ",")
            sb.Append("""portName"":""" & ports(i) & """")
            sb.Append("}")

            If i < ports.Length - 1 Then
                sb.Append(",")
            End If
        Next

        sb.Append("]")
        Return sb.ToString()
    End Function

    Public Function GetManagedComPortsJson() As String
        Dim sb As New System.Text.StringBuilder()
        sb.Append("[")

        For i As Integer = 0 To Ports.Count - 1
            Dim port = Ports(i)
            Dim pname As String = "null"
            Dim openState As String = "false"

            If port IsNot Nothing Then
                pname = """" & port.PortName & """"
                openState = If(port.IsOpen, "true", "false")
            End If

            sb.Append("{")
            sb.Append("""index"":" & i & ",")
            sb.Append("""portName"":" & pname & ",")
            sb.Append("""isOpen"":" & openState)
            sb.Append("}")

            If i < Ports.Count - 1 Then
                sb.Append(",")
            End If
        Next

        sb.Append("]")
        Return sb.ToString()
    End Function


    'Remove the Comport at specified index
    Public Sub DisconnectComPortAt(index As Integer)
        If index >= 0 AndAlso index < Ports.Count Then
            Dim sp = Ports(index)

            ' Close if valid and open
            If sp IsNot Nothing AndAlso sp.IsOpen Then
                sp.Close()
            End If

            ' Remove slot entirely
            Ports.RemoveAt(index)
        End If
    End Sub

    ' For UDP Channel Managements =======================================
    Public Sub AddUdpChannelAt(index As Integer, address As String, port As Integer, ByRef Parent As Control)
        Try
            ' Pad with Nothing if needed
            While Cports.UdpChannels.Count <= index
                Cports.UdpChannels.Add(Nothing)
                Cports.UdpLocks.Add(New Object())
            End While

            ' Close and overwrite if already exists
            If Cports.UdpChannels(index) IsNot Nothing Then
                Cports.UdpChannels(index).Close()
                Cports.UdpChannels(index) = Nothing
                Cports.UdpLocks(index) = New Object()
            End If

            ' Create and bind new UDP client
            Dim udp = New Net.Sockets.UdpClient()
            udp.Connect(address, port)

            ' Replace or insert
            If index = Cports.UdpChannels.Count Then
                Cports.UdpChannels.Add(udp)
                Cports.UdpLocks.Add(New Object())
            Else
                Cports.UdpChannels(index) = udp
                Cports.UdpLocks(index) = New Object()
            End If

            NotificationManager.Show(Parent, "UDP channel " & address & ":" & port & " at index " & index, Color.Green, 800)

        Catch ex As Exception
            NotificationManager.Show(Parent, "Failed to create UDP channel at index " & index, Color.Red, 1000)
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


    ' send a string via a UDP channel
    Public Sub SendUdp(index As Integer, message As String)
        If index >= 0 AndAlso index < UdpChannels.Count Then
            Dim udp = UdpChannels(index)

            If udp IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(message) Then
                SyncLock UdpLocks(index)
                    Dim data = System.Text.Encoding.UTF8.GetBytes(message)
                    udp.Send(data, data.Length)
                End SyncLock
            End If
        End If
    End Sub

    ' remove UDP channel
    Public Sub RemoveUdpChannelAt(index As Integer, ByRef Parent As Control)
        Try
            If index >= 0 AndAlso index < Cports.UdpChannels.Count Then
                Dim udp = Cports.UdpChannels(index)

                ' Close if active
                If udp IsNot Nothing Then
                    udp.Close()
                    Cports.UdpChannels(index) = Nothing
                End If

                ' Reset lock at this index
                If index < Cports.UdpLocks.Count Then
                    Cports.UdpLocks(index) = New Object()
                End If

                NotificationManager.Show(Parent, "UDP channel at index " & index & " removed", Color.Orange, 800)
            End If

        Catch ex As Exception
            NotificationManager.Show(Parent, "Failed to remove UDP channel at index " & index, Color.Red, 1000)
        End Try
    End Sub


End Module
