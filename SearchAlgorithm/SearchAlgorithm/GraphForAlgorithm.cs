using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{

    public class ArrayOfVertex // Массив возвращающий по idнику вершину
    {
        public ArrayOfVertex (int maxId)
        {
            if (maxId == null)
            {
                throw new ArgumentNullException();
            }
            if (maxId <= 0)
            {
                throw new ArgumentException();
            }
            this.array = new List<Vertex>(maxId);
            this.maxId = maxId;
        }

        private int maxId;
        private List<Vertex> array;

        public void AddVertex (Vertex vertex) // Добавляет или перезаписывает вершину по id номеру
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
    }

    public class Graph // Весь граф для алгоритма
    {
        int maxId = OnDataBase.GetMaxId();
        ArrayOfVertex array;

        public void Create()
        {
            array = new ArrayOfVertex(maxId);

            Vertex firstVertex = CreateHelper(0);          
        }

        private Vertex CreateHelper(int id)
        {
            VertexInDateBase vertexInDateBase = OnDataBase.GetVertex(id);
            Vertex vertex = ConvertVertix(vertexInDateBase);
            array.AddVertex(vertex);
            Vertex temp;

            for (int i = 0; i < vertexInDateBase.Arcs.Length; i++)
            {
                temp = array.ElementAt(vertexInDateBase.Arcs[i].Id);
                if (temp == null)
                {
                    // Добавляем новую вершину и потом дугу к ней
                    vertex.AddArc(new Arc(CreateHelper(vertexInDateBase.Arcs[i].Id), vertexInDateBase.Arcs[i].WightArc, vertexInDateBase.Arcs[i].Track));
                }
                else
                {
                    // Добавляем только дугу
                    vertex.AddArc(new Arc (temp, vertexInDateBase.Arcs[i].WightArc, vertexInDateBase.Arcs[i].Track));
                }
            }

            return vertex;
        }

        private Vertex ConvertVertix(VertexInDateBase vertexInDateBase)
        {
            return new Vertex(vertexInDateBase.Id, vertexInDateBase.Coordinates, vertexInDateBase.Priority, 1); // Зону нужно исправить
        }

    }

    public class GraphZone // Граф одной зоны. Реализация его будет выложена позже
    {

    }

    public class ZoneBorder // Границы одной зоны
    {
        public readonly PointCoordinates[] Vertex; // Вершины многоуголника соединяющиеся последовательно и первая с последней
        public ZoneBorder (PointCoordinates[] vertex)
        {
            if (vertex.Length < 3)
            {
                throw new ArgumentException();
            }

            this.Vertex = vertex;
        }
    }

    public class Zone
    {
        public readonly ZoneBorder Border;
        public readonly string Name;
        public Zone (ZoneBorder border, string name)
        {
            if (border == null || string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException();
            }
            this.Border = border;
            this.Name = name;
        }
    }

    public class Vertex
    {
        public readonly int Id;
        public readonly PointCoordinates Coordinates;
        public readonly PriorityVertex Priority;
        public int Zone; // Номер зоны в котором лежит Вершина, если зона 0, толежит в нескольких зонах
        public int[] Zone0; // только если зона 0, массив зон 
        public Arc[] ArcThisVertex;
        
        public Vertex (int id, PointCoordinates coordinates, PriorityVertex priority, int zone)
        {
            if (id == null ||
                zone == null ||
                coordinates == null)
            {
                throw new ArgumentNullException();
            }

            if (id == 0)
            {

                throw new ArgumentException();
            }

            this.Id = id;
            this.Coordinates = coordinates;
            this.Priority = priority;
            this.Zone = zone;
            this.Zone0 = null;
        }

        public void AddArc (Arc arc)
        {
            if (ArcThisVertex == null)
            {
                ArcThisVertex = new Arc[]{ arc };
            }
            else
            {
                Arc[] temp = new Arc[ArcThisVertex.Length + 1];
                Array.Copy(ArcThisVertex, 0, temp, 0, ArcThisVertex.Length);
                temp[ArcThisVertex.Length]=arc;
                ArcThisVertex = temp;
            }
            return;
        }
        
        public void AddArc (Arc[] arc)
        {
            if (ArcThisVertex == null)
            {
                ArcThisVertex = arc;
            }
            else
            {
                Arc[] temp = new Arc[ArcThisVertex.Length + arc.Length];
                Array.Copy(ArcThisVertex, 0, temp, 0, ArcThisVertex.Length);
                Array.Copy(arc, 0, temp, ArcThisVertex.Length-1, arc.Length);
                ArcThisVertex = temp;
            }
            return;
        }

        public void AddZone (int zone)
        {
            if(zone != 0)
            {
                Zone0 = new int[]{zone};
                zone = 0;
            }
            else
            {
                int[] temp = new int[Zone0.Length + 1];
                Array.Copy(Zone0, 0, temp, 0, Zone0.Length);
                temp[Zone0.Length]=zone;
                Zone0 = temp;
            }
        }
    }

    public class Arc
    {
        public readonly Vertex NextVertex;
        public readonly TimeSpan WightArc;
        public readonly PointCoordinates[] Track;
        public Arc (Vertex nextVertex, TimeSpan wightArc, PointCoordinates[] track)
        {
             if (nextVertex == null ||
                wightArc == null ||
                track == null)
            {
                throw new ArgumentNullException();
            }
            this.NextVertex = nextVertex;
            this.WightArc = wightArc;
            this.Track = track;
        }
    }

    public enum PriorityVertex
    {
        Object, // Строение или любой объект вне дороги
        Road // Дорога
    } 
}

