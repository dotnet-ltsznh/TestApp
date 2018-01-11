Imports System.IO

Module Test


    Public Sub main()
        Try
            Console.WriteLine(Date.Now() & vbTab & "程序启动！")

            'Dim test As New TestThreadPool
            'test.test()
            'test.testManulResetEvent()

            'Dim test As New TestGPS
            'test.printMapLongitudeLatitude(121.123, 31.123)

            Dim test As New CandyCore.CandyPrinter
            Dim path As String = "d:\1.txt"
            'test.PrintByPage(path)
            test.PrintFileByFilePath(path)
            'test.Print(File.ReadAllText(path))
            'test.Print("tes\nt" + vbCrLf + "t\r\n\ttt")


            Console.WriteLine(Date.Now() & vbTab & "程序运行结束！" & vbCrLf & "按回车键退出...")
            Console.Read()
        Catch ex As Exception
            Console.WriteLine(Date.Now() & vbTab & "异常消息：" & vbTab & ex.Message)
            Console.Read()
        End Try

    End Sub
End Module
