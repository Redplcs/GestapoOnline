﻿namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public struct PrudpVirtualPort
{
	private const int StreamIdMask = 0xF;
	private const int StreamTypeShiftBy = 0x4;

	public byte StreamId { get; set; }
	public PrudpStreamType StreamType { get; set; }

	public static PrudpVirtualPort Deserialize(byte data)
	{
		return new PrudpVirtualPort()
		{
			StreamId = (byte)(data & StreamIdMask),
			StreamType = (PrudpStreamType)(data >> StreamTypeShiftBy)
		};
	}
}
