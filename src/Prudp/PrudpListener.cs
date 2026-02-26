using System.Net;

namespace Redplcs.GestapoOnline.Prudp;

public sealed class PrudpListener : IAsyncDisposable
{
    public IPEndPoint LocalEndPoint { get; }

    public ValueTask DisposeAsync()
    {
        return default;
    }

    public ValueTask<PrudpConnection> AcceptConnectionAsync(CancellationToken cancellationToken = default)
    {
        return default;
    }

    public static ValueTask<PrudpListener> ListenAsync(PrudpListenerOptions options, CancellationToken cancellationToken = default)
    {
        return default;
    }
}
