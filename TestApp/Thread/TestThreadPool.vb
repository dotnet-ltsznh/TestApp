Imports System.Threading

Public Class TestThreadPool
    Private maxUsedThreads As Int16 = 0 '检测使用的最大线程数
    Private testThreadsSleep As Int16 = 1 '检测使用的最大线程数
    Private threadWaitTime As Int16 = 1000 '线程休眠时间

    ''' <summary>
    ''' 测试传参的线程池，根据线程数判断任务是否全部结束
    ''' </summary>
    Public Sub test()


        Try
#Region "多线程任务"
            Dim TPool As System.Threading.ThreadPool
            TPool.SetMaxThreads(2, 2)

            'Dim stateData1 As New StateData
            'stateData1.times = 3
            'stateData1.message = "第1个线程"

            'TPool.QueueUserWorkItem(New System.Threading.WaitCallback _
            '                        (AddressOf processMethod), stateData1)

            'Dim stateData2 As New StateData
            'stateData2.times = 6
            'stateData2.message = "第2个线程"

            'TPool.QueueUserWorkItem(New System.Threading.WaitCallback _
            '                        (AddressOf processMethod), stateData2)
            For i As Int16 = 1 To 100
                Dim stateData1 As New StateData
                stateData1.times = 30
                stateData1.message = "第" & i & "个线程"

                TPool.QueueUserWorkItem(New System.Threading.WaitCallback _
                                        (AddressOf processMethod), stateData1)

            Next
#End Region

#Region "判断线程池是否全部结束"
            '判断线程池是否全部结束
            Dim maxWorkerThreads, workerThreads, completionPortThreads As Int16
            Do
                If testThreadsSleep > 0 Then
                    Thread.Sleep(testThreadsSleep) '判断间隔时间
                Else
                    testThreadsSleep = 0
                End If

                ThreadPool.GetMaxThreads(maxWorkerThreads, completionPortThreads)
                ThreadPool.GetAvailableThreads(workerThreads, completionPortThreads)

                '获取最大使用的线程数
                If (maxWorkerThreads - workerThreads) > maxUsedThreads Then
                    maxUsedThreads = (maxWorkerThreads - workerThreads)
                End If

                '判断是否全部结束
                If (maxWorkerThreads - workerThreads) = 0 Then
                    '所有线程任务结束后执行代码段
                    Console.WriteLine(Date.Now() & vbTab & "所有线程运行结束")
                    Console.WriteLine(Date.Now() & vbTab & "检测时间间隔：" & vbTab & testThreadsSleep & "ms" & vbTab _
                        & "监测到使用最大线程数：" & maxUsedThreads)

                    '所有任务结束，退出
                    Exit Do
                End If
            Loop
#End Region

        Catch ex As Exception
            Console.WriteLine(Date.Now() & "异常消息：" & ex.Message)
            Console.Read()
            Throw ex
        End Try
    End Sub



    ''' <summary>
    ''' 根据ManulResetEvent判断任务是否全部结束
    ''' </summary>
    Public Sub testManulResetEvent()

        Dim maxUsedThreads As Int16 = 0 '检测使用的最大线程数
        Dim testThreadsSleep As Int16 = 10 '检测使用的最大线程数
        Try
#Region "多线程任务"
            Dim TPool As System.Threading.ThreadPool
            'ThreadPool.SetMaxThreads(proMaxVehicleCount, proMaxVehicleCount)


            'Dim manualWaitHandler1 As New ManualResetEvent(False)
            'Dim stateData1 As New StateData
            'stateData1.times = 3
            'stateData1.message = "第1个线程"
            'TPool.QueueUserWorkItem(New System.Threading.WaitCallback _
            '                        (AddressOf processMethod), stateData1)

            'Dim manualWaitHandler2 As New ManualResetEvent(False)
            'Dim stateData2 As New StateData
            'stateData2.times = 6
            'stateData2.message = "第2个线程"
            'TPool.QueueUserWorkItem(New System.Threading.WaitCallback _
            '                        (AddressOf processMethod), stateData2)

            Dim times As Int16 = 3
            Dim manualWaitHandler(times - 1) As ManualResetEvent

            For i As Int16 = 0 To times - 1
                manualWaitHandler(i) = New ManualResetEvent(False) 'true,一开始就可以执行(状态：终止/非终止（触发/非触发）)
                Dim stateData1 As New StateData
                stateData1.times = 3
                stateData1.message = "第" & i + 1 & "个线程"
                stateData1.manualWaitHandler = manualWaitHandler(i)
                TPool.QueueUserWorkItem(New WaitCallback _
                                        (AddressOf processMethodWithWaitHandler), stateData1)
            Next

            For i As Int16 = 0 To times - 1
                manualWaitHandler(i).WaitOne()
            Next
            'WaitHandle.WaitAll(manualWaitHandler)
#End Region

            '所有线程任务结束后执行代码段
            Console.WriteLine(Date.Now() & vbTab & "所有线程运行结束")
            'Console.WriteLine(Date.Now() & vbTab & "检测时间间隔：" & vbTab & testThreadsSleep & "ms" & vbTab _
            '            & "监测到使用最大线程数：" & maxUsedThreads)

        Catch ex As Exception
            Console.WriteLine(Date.Now() & "异常消息：" & ex.Message)
            Console.Read()
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' 任务处理传递参数
    ''' </summary>
    Public Class StateData
        Public times As Int16 = 10
        Public message As String = "默认消息"

        Public manualWaitHandler As ManualResetEvent

        Public outMessage As String = ""

    End Class

    ''' <summary>
    ''' 普通任务处理
    ''' </summary>
    ''' <param name="stateData"></param>
    Public Sub processMethod(ByVal stateData As Object)
        '处理任务
        Dim state As StateData = stateData
        Dim times As Int16 = state.times

        For i As Int16 = 1 To times
            Console.WriteLine(Date.Now() & vbTab & i & vbTab & "线程处理消息:" & vbTab & state.message)
            Thread.Sleep(threadWaitTime)
        Next
    End Sub

    ''' <summary>
    ''' 带WaitHandler(ManualResetEvent)的任务处理
    ''' </summary>
    ''' <param name="stateData"></param>
    Public Sub processMethodWithWaitHandler(ByVal stateData As Object)
        '处理任务
        Dim state As StateData = stateData
        Dim times As Int16 = state.times

        For i As Int16 = 1 To times
            Console.WriteLine(Date.Now() & vbTab & i & vbTab & "线程处理消息:" & vbTab & state.message)
            Thread.Sleep(threadWaitTime)
        Next

        '本任务结束
        state.manualWaitHandler.Set()
    End Sub

End Class
