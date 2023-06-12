


Module Cports
    Public Ports As New List(Of IO.Ports.SerialPort)

    Public Threads As New List(Of System.Threading.Thread)




    Public Sub SendToCOM(ByVal Params As Object)
        Try
            Cports.Ports(Params(0)).WriteTimeout = 30



            Cports.Ports(Params(0)).WriteLine(Params(1))




        Catch ex As Exception

        End Try



    End Sub




End Module
