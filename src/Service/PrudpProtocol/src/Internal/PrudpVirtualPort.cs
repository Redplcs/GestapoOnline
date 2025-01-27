using System.Diagnostics;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

[DebuggerDisplay("StreamId = {StreamId}, StreamType = {StreamType}")]
internal struct PrudpVirtualPort
{
	public byte StreamId { get; set; }
	public PrudpStreamType StreamType { get; set; }

	public static PrudpVirtualPort Read(byte value)
	{
		return new PrudpVirtualPort()
		{
			StreamId = (byte)(value & 0xF),
			StreamType = (PrudpStreamType)(value >> 4),
		};
	}
}
