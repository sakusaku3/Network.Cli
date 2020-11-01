using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network
{
    public class Graph<TVertex, TEdge> where TEdge : IEdge<TVertex>
    {
        public IReadOnlyList<TVertex> Vertices => this._vertices;
        private readonly List<TVertex> _vertices = 
            new List<TVertex>();

        public IReadOnlyList<TEdge> Edges => this._edges;
        private readonly List<TEdge> _edges = 
            new List<TEdge>();

        private readonly Dictionary<TVertex, List<TEdge>> vertexOutEdges = 
            new Dictionary<TVertex, List<TEdge>>();

        private readonly Dictionary<TVertex, List<TEdge>> vertexInEdges = 
            new Dictionary<TVertex, List<TEdge>>();

        public Graph() : this(Enumerable.Empty<TEdge>()) { }

        public Graph(IEnumerable<TEdge> edges)
        {
            this.AddEdgeRange(edges);
        }

        public void AddVertex(TVertex vertex)
        {
            if (this._vertices.Contains(vertex)) return;
            this._vertices.Add(vertex);
            this.vertexOutEdges.Add(vertex, new List<TEdge>());
            this.vertexInEdges.Add(vertex, new List<TEdge>());
        }

        public void AddEdgeRange(IEnumerable<TEdge> edges)
        {
            foreach (var edge in edges)
                this.AddEdge(edge);
        }

        public void AddEdge(TEdge edge)
        {
            if (this._edges.Contains(edge)) return;
            this._edges.Add(edge);
            this.AddVertex(edge.Source);
            this.AddVertex(edge.Target);
            this.vertexOutEdges[edge.Source].Add(edge);
            this.vertexInEdges[edge.Target].Add(edge);
        }
    }
}
