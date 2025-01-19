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

	public static byte[] Serialize(PrudpPacket packet, byte[] password)
	{
		var buffer = new byte[15];

		buffer[0] = PrudpVirtualPort.Serialize(packet.SourcePort);
		buffer[1] = PrudpVirtualPort.Serialize(packet.DestinationPort);
		buffer[2] = PrudpPacketTypeAndFlags.Serialize(packet.Type, packet.Flags);
		buffer[3] = packet.SessionId;

		BinaryPrimitives.WriteUInt32LittleEndian(buffer[4..8], packet.Signature);
		BinaryPrimitives.WriteUInt16LittleEndian(buffer[8..10], packet.SequenceId);
		BinaryPrimitives.WriteUInt32LittleEndian(buffer[10..14], packet.ConnectionSignature!.Value);

		buffer[^1] = PrudpChecksum.Calculate(buffer[..^1], password);

		return buffer;
	}

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
