﻿using Redplcs.GestapoOnline.Service.PrudpProtocol;
using System.Net.Sockets;

const int listeningPort = 30065;
var password = "7fas5"u8;

using var listener = new UdpClient(listeningPort);

while (true)
{
	var datagram = listener.ReceiveAsync().GetAwaiter().GetResult();

	Console.WriteLine("Received packet with {0} bytes from '{1}'.", datagram.Buffer.Length, datagram.RemoteEndPoint);

	var sourcePort = PrudpVirtualPort.Deserialize(datagram.Buffer[0]);
	var destinationPort = PrudpVirtualPort.Deserialize(datagram.Buffer[1]);
	var (type, flags) = PrudpPacketTypeAndFlags.Deserialize(datagram.Buffer[2]);
	var checksum = PrudpChecksum.Calculate(datagram.Buffer.AsSpan()[..^1], password);

	Console.WriteLine("StreamId: {0}\tStreamType: {1}\t// Source port", sourcePort.StreamId, sourcePort.StreamType);
	Console.WriteLine("StreamId: {0}\tStreamType: {1}\t// Destination port", destinationPort.StreamId, destinationPort.StreamType);
	Console.WriteLine("Type: {0}", type);
	Console.WriteLine("Flags: {0}", flags);
	Console.WriteLine("Received checksum: {0}\tCalculated checksum: {1}", datagram.Buffer[^1], checksum);
	Console.WriteLine("Raw: {0}", BitConverter.ToString(datagram.Buffer));

	Console.WriteLine();
}
