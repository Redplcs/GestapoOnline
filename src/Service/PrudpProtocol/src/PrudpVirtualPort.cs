namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public struct PrudpVirtualPort
{
	public byte StreamId { get; set; }
	public PrudpStreamType StreamType { get; set; }

	public static PrudpVirtualPort Read(byte value)
	{
		return new PrudpVirtualPort()
		{
			StreamId = (byte)(value & 0xF),
			StreamType = (PrudpStreamType)(value >> 0x4)
		};
	}
}
