using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Zone
    {
        public readonly int NumberThisZone;
        private GraphForZone graph;
        public Dictionary<int, Dictionary<int, double>> VertexZone0;
        private Dictionary<int, Vertex> dictionaryOfVertexForZone;

        private ShortestPathTreeOfDistance shortestPathTreeOfDistance;

        public Zone(DataBase dataBase, int numberZone)
        {
            this.NumberThisZone = numberZone;
            this.graph = new GraphForZone(dataBase, numberZone);
            shortestPathTreeOfDistance = new ShortestPathTreeOfDistance(dictionaryOfVertexForZone);
        }

        public List<PointCoordinates> GetTrack(int Id1, int Id2)
        {
            if (Id1 == Id2)
            {
                return new List<PointCoordinates>();
            }

            foreach (Arc arc in dictionaryOfVertexForZone[Id1].ArcThisVertex)
            {
                if (arc.IsBestWayForThisId(Id2))
                {
                    List<PointCoordinates> temp = GetTrack(arc.NextVertex.Id, Id2);
                    temp.AddRange(arc.Track);
                    return temp;
                }
            }

            throw new Exception("Ужас, это не должно было произойти");
        }

        private double GetWight(int id1, int id2)
        {
            if (id1 == id2)
            {
                return 0;
            }

            foreach (Arc arc in dictionaryOfVertexForZone[id1].ArcThisVertex)
            {
                if (arc.IsBestWayForThisId(id2))
                {
                    return (arc.WightArc + GetWight(arc.NextVertex.Id, id2));
                }
            }

            throw new Exception("Ужас, это не должно было произойти");
        }

        public void UpdateZone()
        {
            shortestPathTreeOfDistance.Solve();
            shortestPathTreeOfDistance.SolveVertexZone0(VertexZone0);
        }
    }

    class GraphForZone
    {
        private Dictionary<int, Vertex> dictionaryOfVertex;
        private DataBase dataBase;
        private int numberZone;

        public GraphForZone(DataBase dataBase, int numberZone)
        {
            this.dictionaryOfVertex = new Dictionary<int, Vertex>();
            //CreateGraphForZone(idFirstVertix);
        }

        private void CreateGraphForZone(int idVertix)
        {
            VertexInDateBase vertexInDateBase = dataBase.GetVertex(idVertix); // Исправить
            Vertex vertex = ConvertVertix(vertexInDateBase);

            dictionaryOfVertex.Add(vertex.Id, vertex);

            Vertex temp;

            for (int i = 0; i < vertexInDateBase.Arcs.Length; i++)
            {
                temp = dictionaryOfVertex[vertexInDateBase.Arcs[i].Id];

                //Проверить на то что мы в нужной зоне

                if (temp == null)
                {
                    // Добавляем новую вершину и потом дугу к ней
                    //vertex.AddArc(new Arc(CreateGraphForZone(vertexInDateBase.Arcs[i].Id), vertexInDateBase.Arcs[i].Time, vertexInDateBase.Arcs[i].Track));
                }
                else
                {
                    // Добавляем только дугу
                    //vertex.AddArc(new Arc(temp, vertexInDateBase.Arcs[i].Time, vertexInDateBase.Arcs[i].Track));
                }
            }


            return;
        }

        private Vertex ConvertVertix(VertexInDateBase vertexInDateBase)
        {
            return new Vertex(vertexInDateBase.Id, vertexInDateBase.Coordinates, vertexInDateBase.Priority, vertexInDateBase.NumberZone); // Зону нужно исправить
        }


    }
}
