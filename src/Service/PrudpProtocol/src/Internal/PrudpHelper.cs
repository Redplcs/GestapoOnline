namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

internal static class PrudpHelper
{
	public static (PrudpPacketType, PrudpPacketFlags) ReadPacketTypeFlags(byte value)
	{
		var type = (PrudpPacketType)(value & 0b111);
		var flags = (PrudpPacketFlags)(value >> 3);

		return (type, flags);
	}
}
