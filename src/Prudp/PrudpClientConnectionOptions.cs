using System.Net;

namespace Redplcs.GestapoOnline.Prudp;

public sealed class PrudpClientConnectionOptions
{
    public string AccessKey { get; set; } = null!;
    public EndPoint RemoteEndPoint { get; set; } = null!;
    public IPEndPoint? LocalEndPoint { get; set; }
}
