using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class DataBase
    {/* Основные понятия:
         * 
         * id, номер вершины (точки) - Переменная типа int.
         *                                  В графе: является уникальным номером для вершины графа. Все вершины графа пронумерованы с 0 до maxId включительно.
         *                                  На карте: должна являться уникальным номером точки, которая указывает на объект: дом, перекресток, разворот и т.д.
         *                                  
         * Ближайшая вершина - В графе: Точка из которой или в которую будет следовать путь. Является ближайшей для расчетов.
         *                     На карте: Ближайшая точка, имеющаяся в базе, к точке заданной пользователем.
         *                     
         * maxId, максимальный номер вершины(точки) - Переменная типа int. Число указывающая максимально возможный Id. (см. определение id)
         * 
         * Arc, Дуга - направленное ребро графа. Имеет данные о: времени проезда по данной дуге, длине в метрах и координатах для отрисовки в клиенте. Это все в дополнение к направлению дуги.
         */

        // Основные функции:
        public VertexInDateBase GetVertex(int id) // Возвращает вершину графа с номером id. А именно заполненный экземпляр класса VertexInDateBase у которого в свою очередь нужно будет заполнить массив исходящих дуг, каждая из которых представляет экземпляр класса ArcInDateBase.
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int GetNearId(PointCoordinates coordinates) // Возвращает номер ближайшей вершины к точке с координатами coordinates.
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int GetMaxId() // Возвращается максимально возможный номер вершины.
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int[] GiveReverseWay(int id) // Возвращает массив idшников, которые являются теми вершинами из которых дуги указывают на вершину с данным номером id.
        {
            throw new Exception("The method or operation is not implemented.");
        }

        // Функции необходимые в будущем:

        public TimeSpan LastUpdate() // Возвращает дату последнего изменения базы данных. Требуется для того чтобы перезагрузить граф в алгоритм с последними изменениями.
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int[] UpdateZone() /* Возвращает данные о том какие зоны были изменены между последним и предпоследним вызовом LastUpdate.
                                   * Так же замечу, что этот экземпляр базы данных не должен ни в коем случае изменяться, пока ей пользуется алгоритм.
                                   * Нужно для дальнейшей непрерывной работы сервера. 
                                   */
        {
            throw new Exception("The method or operation is not implemented.");
        }

    }

    class VertexInDateBase
    {
        public readonly int Id; // Номер этой вершины
        public readonly PointCoordinates Coordinates; // Координаты этой вершины
        public readonly PriorityVertex Priority; // Приоритет этой вершины
        public ArcInDateBase[] Arcs; // Дуги этой вершины
        public readonly int NumberZone;

        public VertexInDateBase(int id, PointCoordinates coordinates, PriorityVertex priorityVertex, ArcInDateBase[] arcs, int numberZone)
        {
            if (coordinates == null ||
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
        public readonly TimeSpan Time; // вес этой дуги
        public readonly int Distance;
        public readonly PointCoordinates[] Track; // Координаты отображения дуги

        public ArcInDateBase(int id, TimeSpan time, int distance, PointCoordinates[] track)
        {
            if (time == null ||
                track == null)
            {
                throw new ArgumentNullException();
            }
            if (track.Length < 2)
            {
                throw new ArgumentException();
            }

            this.Distance = distance;
            this.Id = id;
            this.Time = time;
            this.Track = track;
        }
    }
}
