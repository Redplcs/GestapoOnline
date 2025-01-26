using System.Net;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public sealed class PrudpConnection
{
	internal PrudpConnection(IPEndPoint remoteEndPoint)
	{
		RemoteEndPoint = remoteEndPoint;
	}

	public IPEndPoint RemoteEndPoint { get; }
}
