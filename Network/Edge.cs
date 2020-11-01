using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
    public class Edge<TVertex> : IEdge<TVertex>
    {
        public TVertex Source { get; }
        public TVertex Target { get; }
        public int Cost { get; }

        public Edge(
            TVertex source, 
            TVertex target) : this(source, target, 0) { }

        public Edge(
            TVertex source, 
            TVertex target,
            int cost)
        {
            this.Source = source;
            this.Target = target;
            this.Cost = cost;
        }

        public override string ToString()
        {
            return $"{this.Source}->{this.Target}";
        }
    }
}
