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
}
