using System;

namespace Network
{
    public interface IEdge<TVertex>
    {
        TVertex Source { get; }
        TVertex Target { get; }
        int Cost { get; }
    }
}
