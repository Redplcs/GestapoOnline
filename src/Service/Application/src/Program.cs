using System.Net.Sockets;

const int listeningPort = 30065;

using var listener = new UdpClient(listeningPort);

while (true)
{
	var receiveResult = await listener.ReceiveAsync();

	LogReceivedDatagram(receiveResult);

	Console.WriteLine();
}

static void LogReceivedDatagram(UdpReceiveResult result)
{
	Console.WriteLine("Received {0} bytes from {1}", result.Buffer.Length, result.RemoteEndPoint);
	Console.WriteLine(BitConverter.ToString(result.Buffer).Replace("-", ", "));
}
