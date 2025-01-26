using System.Net;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public sealed class PrudpListener
{
	public PrudpListener(int port)
	{
		LocalEndPoint = new IPEndPoint(IPAddress.Any, port);
	}

	public IPEndPoint LocalEndPoint { get; }

	public ValueTask<PrudpConnection> AcceptConnectionAsync(CancellationToken cancellationToken = default)
	{
		var remoteEndPoint = new IPEndPoint(IPAddress.Any, port: 0);
		var connection = new PrudpConnection(remoteEndPoint);
		return ValueTask.FromResult(connection);
	}
}
