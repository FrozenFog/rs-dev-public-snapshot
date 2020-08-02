using RelertSharp.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// TODO

namespace RelertSharp.DataStructures
{
    class Graph<T> where T : IComparable
    {
        public Graph()
        {
            AdjacencyList = new Dictionary<GraphNode<T>, HashSet<GraphEdge<T>>>();
        }

        #region Public Methods - Graph
        #region Add
        public bool Add(GraphNode<T> node)
        {
            if (AdjacencyList.ContainsKey(node))
                return false;
            AdjacencyList[node] = new HashSet<GraphEdge<T>>();
            return true;
        }
        public void Add(GraphNode<T> node, bool bOverride)
        {
            if (!bOverride && AdjacencyList.ContainsKey(node))
                return;
            AdjacencyList[node] = new HashSet<GraphEdge<T>>();
            return;
        }
        public bool Add(GraphNode<T> node, HashSet<GraphEdge<T>> adj)
        {
            if (AdjacencyList.ContainsKey(node))
                return false;

            try
            {
                AdjacencyList[node] = Misc.MemCpy(adj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Add(GraphNode<T> node, HashSet<GraphEdge<T>> adj, bool bOverride)
        {
            if (!bOverride && AdjacencyList.ContainsKey(node))
                return false;
            try
            {
                AdjacencyList[node] = Misc.MemCpy(adj);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region Remove
        public bool Remove(GraphNode<T> node)
        {
            return AdjacencyList.Remove(node);
        }
        #endregion
        #region Contains
        public bool Contains(GraphNode<T> node)
        {
            return AdjacencyList.ContainsKey(node);
        }
        #endregion
        #region Connect
        public bool Connect(GraphNode<T> prev, GraphNode<T> next, T weigh, bool bOverride = false, bool bDoubleEdge = false)
        {
            if (!AdjacencyList.ContainsKey(prev) || !AdjacencyList.ContainsKey(next))
                return false;



            return true;
        }
        #endregion
        #endregion



        public Dictionary<GraphNode<T>, HashSet<GraphEdge<T>>> AdjacencyList { get; private set; }
    }

    public class GraphNode<T>
    {
        object Tag;

    }

    public class GraphEdge<T>
    {
        public GraphEdge(object tag, GraphNode<T> from, GraphNode<T> to)
        {
            Tag = tag;
            From = from;
            To = to;

            
        }

        public object Tag { get; set; }
        public GraphNode<T> From { get; set; }
        public GraphNode<T> To { get; set; }


    }
}
