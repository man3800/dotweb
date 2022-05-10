System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("en-US");
IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 80);
Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
var sendData = @"<html><head>
                    <link rel='icon' type='image/png' href='data:image/png;base64,iVBORw0KGgo='>
                    </head><body>hello world</body></html>";
server.Bind(ipep);
server.Listen(100);

while (true)
{
    StringBuilder sb = new StringBuilder(100);
    Socket client = server.Accept();
    try
    {
        sb.AppendLine("HTTP/1.1 200 ok");
        sb.AppendLine("date: " + DateTime.UtcNow.ToString("ddd, dd MMM yyy HH':'mm':'ss 'GMT'", culInfo));
        sb.AppendLine("server: test server");
        sb.AppendLine("Content-Length: " + sendData.Length);
        sb.AppendLine("content-type:text/html; charset=UTF-8");
        sb.AppendLine();
        sb.AppendLine(sendData);
        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        client.Send(bytes);
        Thread.Sleep(10);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
    finally
    {
        client.Close();
    }
}
