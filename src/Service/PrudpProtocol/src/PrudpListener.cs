using Redplcs.GestapoOnline.Service.PrudpProtocol.Internal;
using System.Net;
using System.Net.Sockets;

namespace Redplcs.GestapoOnline.Service.PrudpProtocol;

public sealed class PrudpListener : IAsyncDisposable
{
	private readonly UdpClient _client;
	private readonly byte[] _accessKey;

	public PrudpListener(int port, byte[] accessKey)
	{
		_client = new UdpClient(port);
		_accessKey = accessKey;
	}

	public IPEndPoint LocalEndPoint => (IPEndPoint)_client.Client.LocalEndPoint!;

	public async ValueTask<PrudpConnection> AcceptConnectionAsync(CancellationToken cancellationToken = default)
	{
		UdpReceiveResult receiveResult = default;

		// Process 3-way handshake
		for (var i = 0; i < 3; i++)
		{
			receiveResult = await _client.ReceiveAsync(cancellationToken).ConfigureAwait(false);

			var buffer = receiveResult.Buffer.AsSpan();

			// Compare checksums
			{
				var expectedChecksum = buffer[^1];
				var actualChecksum = PrudpChecksum.Calculate(buffer[0..^1], _accessKey);

				if (expectedChecksum != actualChecksum)
				{
					throw new InvalidDataException("The package integrity is broken.");
				}
			}

			var header = PrudpPacketHeader.Read(buffer);
		}

		return new PrudpConnection(receiveResult.RemoteEndPoint);
	}

	public ValueTask DisposeAsync()
	{
		_client.Dispose();

		return ValueTask.CompletedTask;
	}
}
