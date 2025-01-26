namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

internal struct PrudpVirtualPort
{
	public byte StreamId { get; set; }
	public PrudpStreamType StreamType { get; set; }
}
