using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public interface IGraphForAlgorithm
    {
        OnDataBase[] GiveMeIdPriorCrossroads(int id);
        OnDataBase[] GiveMeId(int id);
    }



    public enum PriorityVertex
    {
        Crossroads,
        Home
    }

    class OnDataBase
    {
        int id1;
        int id2;
        PointCoordinates id2Coordinates;
        PriorityVertex id2Priority; 
        TimeSpan time;
    }

    class GraphForAlgorithm : IGraphForAlgorithm
    {

    }
}


// Id1 Id2 time track prior koordinatid1 