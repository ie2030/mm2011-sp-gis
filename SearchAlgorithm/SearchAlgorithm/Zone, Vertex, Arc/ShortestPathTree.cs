using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    abstract class AbstractShortestPathTree
    {
        //public abstract AbstractShortestPathTree(Dictionary<int, Vertex> dictionaryOfVertexForZone);
        public abstract void Solve();
    }

    class ShortestPathTreeOfDistance : AbstractShortestPathTree
    {
        private Dictionary<int, Vertex> dictionaryOfVertexForZone;
        private Dictionary<double, BorderVertex> dictionaryOfBorderVertex;
        private Dictionary<int, Arc> dictionaryOfUsedVertex;


        public ShortestPathTreeOfDistance(Dictionary<int, Vertex> dictionaryOfVertexForZone)
        {
            this.dictionaryOfVertexForZone = dictionaryOfVertexForZone;
        }

        struct BorderVertex
        {
            public Arc ArcUsed;
            public int IdNext;
        }

        public override void Solve()
        {
            foreach (KeyValuePair<int, Vertex> kvp in dictionaryOfVertexForZone)
            {
                SolveOneId(kvp.Key);
            }

            return;
        }

        public void SolveVertexZone0(Dictionary<int, Dictionary<int, double>> oldVertexZone0)
        {
            foreach (KeyValuePair<int, Dictionary<int, double>> kvp in oldVertexZone0)
            {
                GetVertexZone0Helper(kvp.Key, kvp.Value);
            }
            return;
        }

        private void GetVertexZone0Helper(int id1, Dictionary<int, double> id2)
        {
            foreach (KeyValuePair<int, double> kvp in id2)
            {
                id2[kvp.Key] = SearchWightWay(id1, kvp.Key);
            }
        }

        private double SearchWightWay(int id1, int id2)
        {
            if (id1 == id2)
            {
                return 0;
            }

            foreach (Arc arc in dictionaryOfVertexForZone[id1].ArcThisVertex)
            {
                if (arc.IsBestWayForThisId(id2))
                {
                    return (arc.WightArc + SearchWightWay(arc.NextVertex.Id, id2));
                }
            }

            throw new Exception("Ужас, это не должно было произойти");
        }

        private void SolveOneId(int id)
        {
            dictionaryOfBorderVertex.Clear();
            dictionaryOfUsedVertex.Clear();

            dictionaryOfUsedVertex.Add(id, null);
            BorderVertex borderVertex;

            foreach (Arc arc in dictionaryOfVertexForZone[id].ArcThisVertex)
            {
                if (!dictionaryOfUsedVertex.ContainsKey(arc.NextVertex.Id))
                {

                    borderVertex = new BorderVertex();
                    borderVertex.ArcUsed = arc;
                    borderVertex.IdNext = arc.NextVertex.Id;
                    dictionaryOfBorderVertex.Add(arc.WightArc, borderVertex);
                }
            }

            double wight;

            while (dictionaryOfBorderVertex.Count() > 0)
            {
                id = dictionaryOfBorderVertex.Min().Value.IdNext;
                wight = dictionaryOfBorderVertex.Min().Key;
                dictionaryOfUsedVertex.Add(id, dictionaryOfBorderVertex.Min().Value.ArcUsed);
                // Удаляем врешину из словаря границ.
                dictionaryOfBorderVertex.Remove(wight);

                foreach (Arc arc in dictionaryOfVertexForZone[id].ArcThisVertex)
                {
                    if (!dictionaryOfUsedVertex.ContainsKey(arc.NextVertex.Id))
                    {
                        AddUsedVertex(arc);
                        borderVertex = new BorderVertex();
                        borderVertex.ArcUsed = arc;
                        borderVertex.IdNext = arc.NextVertex.Id;
                        dictionaryOfBorderVertex.Add(wight + arc.WightArc, borderVertex);
                    }
                }
            }

            // Нужно выставить все полученные значения в наш граф
            foreach (KeyValuePair<int, Arc> kvp in dictionaryOfUsedVertex)
            {
                if (kvp.Value != null)
                {
                    kvp.Value.AddIdInWay(id);
                }
            }

            dictionaryOfBorderVertex.Clear();
            dictionaryOfUsedVertex.Clear();
            return;
        }

        private void AddUsedVertex(Arc arc)
        {
            int[] arrayOfUsedId = arc.GetAllIdInWay();
            foreach (int id in arrayOfUsedId)
            {
                if (!dictionaryOfUsedVertex.ContainsKey(id))
                {
                    dictionaryOfUsedVertex.Add(id, null);
                }
            }
        }
    }
}
