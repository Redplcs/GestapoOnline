namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

[Flags]
public enum PrudpPacketFlags
{
	None = 0,
	Ack = 0x001,
	Reliable = 0x002,
	NeedAck = 0x004,
	HasSize = 0x008,
	MultiAck = 0x200,
}
