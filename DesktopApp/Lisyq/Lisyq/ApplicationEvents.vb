Imports System.IO

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(
         sender As Object,
            e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs
        ) Handles Me.Startup
            ' Get the splash screen.
            Dim splash As SplashScreen_legacy = CType(My.Application.SplashScreen, SplashScreen_legacy)
            ' Display current status information.


            Dim args = e.CommandLine

            If args.Count() > 0 Then


                Dim filePath = args(0).ToString

                Try


                    Global.Lisyq.Main.queOpenFrom(filePath)


                Catch ex As Exception
                    ' Handle any potential errors gracefully.
                    Console.WriteLine("An error occurred while reading the file: " & ex.Message)
                End Try



            End If



        End Sub
        Private Sub MyApplication_StartupNextInstance(
    sender As Object,
    e As ApplicationServices.StartupNextInstanceEventArgs
) Handles Me.StartupNextInstance


            Dim args = e.CommandLine

            If args.Count() > 0 Then


                Dim filePath = args(0).ToString

                Try
                    Dim fileContents As String = File.ReadAllText(filePath)

                    Global.Lisyq.Main.Open_File_From(filePath)


                Catch ex As Exception
                    ' Handle any potential errors gracefully.
                    Console.WriteLine("An error occurred while reading the file: " & ex.Message)
                End Try



            End If


        End Sub



        Protected Overrides Function OnInitialize(
    commandLineArgs As System.Collections.
        ObjectModel.ReadOnlyCollection(Of String)
) As Boolean
            ' Set the display time to 5000 milliseconds (5 seconds). 
            Me.MinimumSplashScreenDisplayTime = 7000
            Return MyBase.OnInitialize(commandLineArgs)
        End Function

    End Class
End Namespace
