using Redplcs.GestapoOnline.Service.PrudpProtocol;
using System.Net.Sockets;

const int listeningPort = 30065;

using var listener = new UdpClient(listeningPort);

while (true)
{
	var receiveResult = await listener.ReceiveAsync();

	LogReceivedDatagram(receiveResult);

	var sourcePort = PrudpVirtualPort.Read(receiveResult.Buffer[0]);
	var destinationPort = PrudpVirtualPort.Read(receiveResult.Buffer[1]);
	var (type, flags) = PrudpPacketTypeAndFlags.Deserialize(receiveResult.Buffer[2]);

	LogVirtualPort(sourcePort, nameof(sourcePort));
	LogVirtualPort(destinationPort, nameof(destinationPort));

	Console.WriteLine();
}

static void LogReceivedDatagram(UdpReceiveResult result)
{
	Console.WriteLine("Received {0} bytes from {1}", result.Buffer.Length, result.RemoteEndPoint);
	Console.WriteLine(BitConverter.ToString(result.Buffer).Replace("-", ", "));
}

static void LogVirtualPort(PrudpVirtualPort port, string header)
{
	Console.WriteLine("{0}\t|\tStreamId = {1}\tStreamType = {2}", header, port.StreamId, port.StreamType);
}
