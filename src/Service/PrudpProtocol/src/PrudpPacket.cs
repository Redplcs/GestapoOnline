using System.Buffers.Binary;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public struct PrudpPacket
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
	public Memory<byte> Payload { get; set; }
	public byte Checksum { get; set; }

	public static PrudpPacket Deserialize(byte[] data)
	{
		return Deserialize(data.AsSpan());
	}

	public static PrudpPacket Deserialize(ReadOnlySpan<byte> data)
	{
		var packet = new PrudpPacket
		{
			SourcePort = PrudpVirtualPort.Deserialize(data[0]),
			DestinationPort = PrudpVirtualPort.Deserialize(data[1]),
			SessionId = data[3],
			Signature = BinaryPrimitives.ReadUInt32LittleEndian(data[4..8]),
			SequenceId = BinaryPrimitives.ReadUInt16LittleEndian(data[8..10]),
			Checksum = data[^1]
		};

		(packet.Type, packet.Flags) = PrudpPacketTypeAndFlags.Deserialize(data[2]);

		if (packet.Type is PrudpPacketType.Syn or PrudpPacketType.Connect)
		{
			packet.ConnectionSignature = BinaryPrimitives.ReadUInt32LittleEndian(data[10..14]);

			if (packet.Flags.HasFlag(PrudpPacketFlags.HasSize))
			{
				packet.PayloadSize = BinaryPrimitives.ReadUInt16LittleEndian(data[14..16]);
			}
		}
		else if (packet.Type is PrudpPacketType.Data)
		{
			packet.FragmentId = data[10];

			if (packet.Flags.HasFlag(PrudpPacketFlags.HasSize))
			{
				packet.PayloadSize = BinaryPrimitives.ReadUInt16LittleEndian(data[11..13]);
			}
		}

		return packet;
	}
}
