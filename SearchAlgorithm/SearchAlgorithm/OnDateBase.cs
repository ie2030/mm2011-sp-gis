using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class OnDataBase // Вытаскиваем данные из базы данных
    {
        public GiveMeVertex (int id)
        {


            return VertexInDateBase(id, );
        }

        /* так же здесь будут функции:
         * GiveMeZone ()              - Дайте пожалуйста разметки зоны
         * GiveMeVertex (id)          - Дайте пожалуйста Вершину с id таким то 
         * GiveMeNearId (coordinates) - Дайте пожалуйста ближайшую вершину, а точнее её номер
         * GiveMeMaxId ()             - Дайте пожалуйста максимальный номер для вершин
         * GiveMeSoapAndRope ()       - Дайте пожалуйста мыло и веревку
        */
    }

    class VertexInDateBase
    {
        public readonly int Id; // Номер этой вершины
        public readonly PointCoordinates Coordinates; // Координаты этой вершины
        public readonly PriorityVertex Priority; // Приоритет этой вершины
        public ArcInDateBase[] Arcs; // Дуги этой вершины

        public VertexInDateBase(int id, PointCoordinates coordinates, PriorityVertex priorityVertex, ArcInDateBase[] arcs)
        {
            if (id == null ||
               coordinates == null ||
               arcs == null)
            {
                throw new ArgumentNullException();
            }
            if (arcs.Length == 0)
            {
                throw new ArgumentException();
            }

        }
    }

    class ArcInDateBase
    {
        public readonly int Id; // куда приходит дуга
        public readonly TimeSpan WightArc; // вес этой дуги
        public readonly PointCoordinates[] Track; // Координаты отображения дуги

        public ArcInDateBase(int id, TimeSpan wightArc, PointCoordinates[] track)
        {
            if (id == null ||
                wightArc == null ||
                track == null)
            {
                throw new ArgumentNullException();
            }
            if (track.Length < 2)
            {
                throw new ArgumentException();
            }

            this.Id = id;
            this.WightArc = wightArc;
            this.Track = track
        }
    }
}
