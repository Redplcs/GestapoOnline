using System.Net;
using System.Net.Sockets;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public sealed class PrudpListener : IAsyncDisposable
{
	private readonly UdpClient _client;

	public PrudpListener(int port)
	{
		_client = new UdpClient(port);
	}

	public IPEndPoint LocalEndPoint => (IPEndPoint)_client.Client.LocalEndPoint!;

	public async ValueTask<PrudpConnection> AcceptConnectionAsync(CancellationToken cancellationToken = default)
	{
		var receiveResult = await _client.ReceiveAsync(cancellationToken).ConfigureAwait(false);

		return new PrudpConnection(receiveResult.RemoteEndPoint);
	}

	public ValueTask DisposeAsync()
	{
		_client.Dispose();

		return ValueTask.CompletedTask;
	}
}
