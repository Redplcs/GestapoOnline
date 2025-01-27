using Redplcs.GestapoOnline.Service.PrudpProtocol;
using System.Text;

const int listeningPort = 30065;
const string password = "7fas5";

//using var listener = new UdpClient(listeningPort);

var listener = new PrudpListener(listeningPort, Encoding.UTF8.GetBytes(password));

Console.WriteLine("Listening for incoming connection at port {0}", listener.LocalEndPoint.Port);

while (true)
{
	var connection = await listener.AcceptConnectionAsync().ConfigureAwait(false);

	Console.WriteLine("Established connection from {0}", connection.RemoteEndPoint);

	//var datagram = listener.ReceiveAsync().GetAwaiter().GetResult();

	//Console.WriteLine("Received packet with {0} bytes from '{1}'.", datagram.Buffer.Length, datagram.RemoteEndPoint);

	//var request = PrudpPacket.Deserialize(datagram.Buffer);

	//Console.WriteLine("StreamId: {0}\tStreamType: {1}\t// Source port", request.SourcePort.StreamId, request.SourcePort.StreamType);
	//Console.WriteLine("StreamId: {0}\tStreamType: {1}\t// Destination port", request.DestinationPort.StreamId, request.DestinationPort.StreamType);
	//Console.WriteLine("Type: {0}", request.Type);
	//Console.WriteLine("Flags: {0}", request.Flags);
	//Console.WriteLine("SessionId: {0}", request.SessionId);
	//Console.WriteLine("Signature: {0}", request.Signature);
	//Console.WriteLine("SequenceId: {0}", request.SequenceId);
	//Console.WriteLine("Checksum: {0}", request.Checksum);
	//Console.WriteLine("ConnectionSignature: {0}", request.ConnectionSignature);
	//Console.WriteLine("FragmentId: {0}", request.FragmentId);
	//Console.WriteLine("PayloadSize: {0}", request.PayloadSize);
	//Console.WriteLine("Raw: {0}", BitConverter.ToString(datagram.Buffer));

	//var reply = new PrudpPacket
	//{
	//	SourcePort = request.DestinationPort,
	//	DestinationPort = request.SourcePort,
	//	Type = request.Type,
	//	Flags = PrudpPacketFlags.Ack,
	//	Signature = 1,
	//	ConnectionSignature = 1,
	//};

	//listener.Send(PrudpPacket.Serialize(reply, Encoding.ASCII.GetBytes(password)), datagram.RemoteEndPoint);

	Console.WriteLine();
}
