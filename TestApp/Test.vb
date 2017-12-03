Module Test


    Public Sub main()
        Try
            Console.WriteLine(Date.Now() & vbTab & "程序启动！")

            Dim test As New TestThreadPool
            'test.test()
            test.testManulResetEvent()

            Console.WriteLine(Date.Now() & "程序运行结束！" & vbCrLf & "按回车键退出...")
            Console.Read()
        Catch ex As Exception
            Console.WriteLine(Date.Now() & "异常消息：" & ex.Message)
            Console.Read()
        End Try

    End Sub
End Module
