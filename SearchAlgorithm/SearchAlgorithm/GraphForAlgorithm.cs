using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    // Мной написанный код для создания общего виртуального графа. Устарел и умер.

    /*
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
                    vertex.AddArc(new Arc(CreateHelper(vertexInDateBase.Arcs[i].Id), vertexInDateBase.Arcs[i].Time, vertexInDateBase.Arcs[i].Track));
                }
                else
                {
                    // Добавляем только дугу
                    vertex.AddArc(new Arc (temp, vertexInDateBase.Arcs[i].Time, vertexInDateBase.Arcs[i].Track));
                }
            }

            return vertex;
        }



    }
    */
}

