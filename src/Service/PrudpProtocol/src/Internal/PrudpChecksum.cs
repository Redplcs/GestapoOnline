using System.Numerics.Tensors;
using System.Runtime.InteropServices;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;

internal static class PrudpChecksum
{
	public static byte Calculate(ReadOnlySpan<byte> data, ReadOnlySpan<byte> password)
	{
		var words = MemoryMarshal.Cast<byte, uint>(data);
		var wordsSum = TensorPrimitives.Sum(words);
		var main = MemoryMarshal.AsBytes(MemoryMarshal.CreateReadOnlySpan(ref wordsSum, length: 1));
		var remaining = data[^words.Length..];

		var checksum = TensorPrimitives.Sum(password);
		checksum += TensorPrimitives.Sum(remaining);
		checksum += TensorPrimitives.Sum(main);

		return checksum;
	}
}
