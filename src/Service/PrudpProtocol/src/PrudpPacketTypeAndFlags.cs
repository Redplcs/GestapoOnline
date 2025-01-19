namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public static class PrudpPacketTypeAndFlags
{
	private const int TypeMask = 0b111;
	private const int FlagsShiftBy = 3;

	public static byte Serialize(PrudpPacketType type, PrudpPacketFlags flags)
	{
		var typeMasked = (int)type & TypeMask;
		var flagsInt = (int)flags;

		return (byte)(flagsInt << FlagsShiftBy | typeMasked);
	}

	public static (PrudpPacketType type, PrudpPacketFlags flags) Deserialize(byte data)
	{
		var type = (PrudpPacketType)(data & TypeMask);
		var flags = (PrudpPacketFlags)(data >> FlagsShiftBy);

		return (type, flags);
	}
}
