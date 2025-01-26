namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

internal enum PrudpPacketType
{
	Syn = 0,
	Connect = 1,
	Data = 2,
	Disconnect = 3,
	Ping = 4,
}
