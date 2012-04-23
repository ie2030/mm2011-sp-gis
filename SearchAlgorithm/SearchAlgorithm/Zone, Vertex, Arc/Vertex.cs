using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public enum PriorityVertex
    {
        Object, // Строение или любой объект вне дороги
        Road // Дорога
    } 

    class Vertex
    {
        public readonly int Id;
        public readonly PointCoordinates Coordinates;
        public readonly PriorityVertex Priority;
        public int Zone; // Номер зоны в котором лежит Вершина, если зона 0, толежит в нескольких зонах
        public int[] Zone0; // только если зона 0, массив зон 
        public Arc[] ArcThisVertex;

        public Vertex(int id, PointCoordinates coordinates, PriorityVertex priority, int zone)
        {
            if (coordinates == null)
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

        public void AddArc(Arc arc)
        {
            if (ArcThisVertex == null)
            {
                ArcThisVertex = new Arc[] { arc };
            }
            else
            {
                Arc[] temp = new Arc[ArcThisVertex.Length + 1];
                Array.Copy(ArcThisVertex, 0, temp, 0, ArcThisVertex.Length);
                temp[ArcThisVertex.Length] = arc;
                ArcThisVertex = temp;
            }
            return;
        }

        public void AddArc(Arc[] arc)
        {
            if (ArcThisVertex == null)
            {
                ArcThisVertex = arc;
            }
            else
            {
                Arc[] temp = new Arc[ArcThisVertex.Length + arc.Length];
                Array.Copy(ArcThisVertex, 0, temp, 0, ArcThisVertex.Length);
                Array.Copy(arc, 0, temp, ArcThisVertex.Length - 1, arc.Length);
                ArcThisVertex = temp;
            }
            return;
        }

        public void AddZone(int zone)
        {
            if (zone != 0)
            {
                Zone0 = new int[] { zone };
                zone = 0;
            }
            else
            {
                int[] temp = new int[Zone0.Length + 1];
                Array.Copy(Zone0, 0, temp, 0, Zone0.Length);
                temp[Zone0.Length] = zone;
                Zone0 = temp;
            }
        }
    }
}
