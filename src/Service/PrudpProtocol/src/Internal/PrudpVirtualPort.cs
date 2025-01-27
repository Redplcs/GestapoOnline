using System.Diagnostics;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

[DebuggerDisplay("StreamId = {StreamId}, StreamType = {StreamType}")]
internal struct PrudpVirtualPort
{
	private const int StreamIdMask = 0xF;
	private const int StreamTypeShiftBy = 4;

	public byte StreamId { get; set; }
	public PrudpStreamType StreamType { get; set; }

	public readonly void Write(Span<byte> buffer)
	{
		var maskedStreamId = StreamId & StreamIdMask;
		var maskedStreamType = (int)StreamType << StreamTypeShiftBy;
		var maskedPort = (byte)(maskedStreamType | maskedStreamId);

		buffer[0] = maskedPort;
	}

	public static PrudpVirtualPort Read(byte value)
	{
		return new PrudpVirtualPort()
		{
			StreamId = (byte)(value & StreamIdMask),
			StreamType = (PrudpStreamType)(value >> StreamTypeShiftBy),
		};
	}
}
