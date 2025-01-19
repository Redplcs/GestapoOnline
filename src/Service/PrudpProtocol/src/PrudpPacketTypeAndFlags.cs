namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public static class PrudpPacketTypeAndFlags
{
	private const int TypeMask = 0b111;
	private const int FlagsShiftBy = 3;

	public static (PrudpPacketType type, PrudpPacketFlags flags) Deserialize(byte data)
	{
		var type = (PrudpPacketType)(data & TypeMask);
		var flags = (PrudpPacketFlags)(data >> FlagsShiftBy);

		return (type, flags);
	}
}
