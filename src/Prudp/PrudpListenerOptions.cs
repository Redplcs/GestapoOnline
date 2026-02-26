using System.Net;

namespace Redplcs.GestapoOnline.Prudp;

public sealed class PrudpListenerOptions
{
    public string AccessKey { get; set; } = null!;
    public IPEndPoint ListenEndPoint { get; set; } = null!;
}
