using System.Net;

namespace Redplcs.GestapoOnline.Prudp;

public sealed class PrudpConnection : IAsyncDisposable
{
    public IPEndPoint LocalEndPoint { get; }
    public IPEndPoint RemoteEndPoint { get; }

    public ValueTask CloseAsync(CancellationToken cancellationToken = default)
    {
        return default;
    }

    public ValueTask DisposeAsync()
    {
        return default;
    }

    public ValueTask<ReadOnlyMemory<byte>> ReceiveAsync(CancellationToken cancellationToken = default)
    {
        return default;
    }

    public ValueTask SendAsync(ReadOnlyMemory<byte> bytes, CancellationToken cancellationToken = default)
    {
        return default;
    }

    public static ValueTask<PrudpConnection> ConnectAsync(PrudpClientConnectionOptions options, CancellationToken cancellationToken = default)
    {
        return default;
    }
}
