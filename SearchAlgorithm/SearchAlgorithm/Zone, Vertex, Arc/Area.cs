using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Area
    {
        private Dictionary<int, Zone> zones;
        private DataBase dataBase;
        private Dictionary<int, Vertex> DictionaryOfVertexZone0;

        public List<PointCoordinates> GetWay(PointCoordinates coor1, PointCoordinates coor2)
        {
            int id1 = dataBase.GetNearId(coor1);
            int id2 = dataBase.GetNearId(coor2);
            VertexInDateBase vertexDB1 = dataBase.GetVertex(id1);
            VertexInDateBase vertexDB2 = dataBase.GetVertex(id2);

            if (vertexDB1.NumberZone == vertexDB2.NumberZone)
            {
                return zones[vertexDB1.NumberZone].GetTrack(id1, id2);
            }
            else
            {
                // Запускается алгоритм A*
                throw new Exception("The method or operation is not implemented.");
            }
        }

    }

    class ArrayOfVertex // Массив возвращающий по idнику вершину
    {
        private int maxId;
        private List<Vertex> array;

        #region public
        public ArrayOfVertex(int maxId)
        {
            if (maxId <= 0)
            {
                throw new ArgumentException();
            }
            this.array = new List<Vertex>(maxId);
            this.maxId = maxId;
        }

        public void AddVertex(Vertex vertex) // Добавляет или перезаписывает вершину по id номеру
        {
            if (vertex == null)
            {
                throw new ArgumentNullException();
            }
            if (vertex.Id > maxId)
            {
                throw new Exception();
            }

            array.Insert(vertex.Id, vertex);
        }

        public Vertex ElementAt(int id)
        {
            return array.ElementAt(id);
        }
        #endregion
    }
}
