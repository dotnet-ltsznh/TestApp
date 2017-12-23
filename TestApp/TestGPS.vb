Imports System.Threading
Imports CandyCore

Class TestGPS


    Public Sub printMapLongitudeLatitude(ByVal longitude As Double, ByVal latitude As Double)
        Try
            Dim test As New TestStopwatch
            test.startWatch(True)
            Dim gps As CandyCore.GPS = New CandyCore.GPS
            Thread.Sleep(1002)
            test.showWatch("1")
            Console.WriteLine(Date.Now() & vbTab & "GPS经度：" & longitude & vbTab & "GPS纬度：" & latitude)
            Thread.Sleep(2002)
            test.showWatch("2")
            Thread.Sleep(3002)
            test.stopWatch(False, "2")
            Console.WriteLine(Date.Now() & vbTab & "地图经度：" & gps.getMapLongitude(longitude, latitude) _
                & vbTab & "地图纬度：" & gps.GetMapLatitude(longitude, latitude))
            test.stopWatch(True, "3")
        Catch ex As Exception
            Console.WriteLine(Date.Now() & "异常消息：" & ex.Message)
            Console.Read()
        End Try

    End Sub
End Class
