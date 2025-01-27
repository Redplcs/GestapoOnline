using System.Buffers.Binary;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

internal struct PrudpPacketHeader
{
	public PrudpVirtualPort SourcePort { get; set; }
	public PrudpVirtualPort DestinationPort { get; set; }
	public PrudpPacketType Type { get; set; }
	public PrudpPacketFlags Flags { get; set; }
	public byte SessionId { get; set; }
	public uint Signature { get; set; }
	public ushort SequenceId { get; set; }
	public uint? ConnectionSignature { get; set; }
	public byte? FragmentId { get; set; }
	public ushort? PayloadSize { get; set; }

	public readonly void Write(Span<byte> buffer)
	{
		SourcePort.Write(buffer[0..1]);
		DestinationPort.Write(buffer[1..2]);
		WriteTypeAndFlags(buffer[2..3]);
		buffer[3] = SessionId;
		BinaryPrimitives.WriteUInt32LittleEndian(buffer[4..8], Signature);
		BinaryPrimitives.WriteUInt16LittleEndian(buffer[8..10], SequenceId);

		switch (Type)
		{
			case PrudpPacketType.Syn:
			case PrudpPacketType.Connect:
				BinaryPrimitives.WriteUInt32LittleEndian(buffer[10..14], ConnectionSignature!.Value);
				break;

			case PrudpPacketType.Data:
				buffer[10] = FragmentId!.Value;
				break;
		}

		if (Flags.HasFlag(PrudpPacketFlags.HasSize))
		{
			var slice = Type switch
			{
				PrudpPacketType.Syn or PrudpPacketType.Connect => buffer[14..16],
				PrudpPacketType.Data => buffer[11..13],
				_ => buffer[10..12],
			};

			BinaryPrimitives.WriteUInt32LittleEndian(slice, PayloadSize!.Value);
		}
	}

	private readonly void WriteTypeAndFlags(Span<byte> buffer)
	{

	}

	public static PrudpPacketHeader Read(ReadOnlySpan<byte> buffer)
	{
		var header = new PrudpPacketHeader();
		header.SourcePort = PrudpVirtualPort.Read(buffer[0]);
		header.DestinationPort = PrudpVirtualPort.Read(buffer[1]);
		(header.Type, header.Flags) = PrudpHelper.ReadPacketTypeFlags(buffer[2]);
		header.SessionId = buffer[3];
		header.Signature = BinaryPrimitives.ReadUInt32LittleEndian(buffer[4..8]);
		header.SequenceId = BinaryPrimitives.ReadUInt16LittleEndian(buffer[8..10]);

		switch (header.Type)
		{
			case PrudpPacketType.Syn:
			case PrudpPacketType.Connect:
				header.ConnectionSignature = BinaryPrimitives.ReadUInt32LittleEndian(buffer[10..14]);
				break;

			case PrudpPacketType.Data:
				header.FragmentId = buffer[10];
				break;
		}

		if (header.Flags.HasFlag(PrudpPacketFlags.HasSize))
		{
			var slice = header.Type switch
			{
				PrudpPacketType.Syn or PrudpPacketType.Connect => buffer[14..16],
				PrudpPacketType.Data => buffer[11..13],
				_ => buffer[10..12],
			};

			header.PayloadSize = BinaryPrimitives.ReadUInt16LittleEndian(slice);
		}

		return header;
	}
}
