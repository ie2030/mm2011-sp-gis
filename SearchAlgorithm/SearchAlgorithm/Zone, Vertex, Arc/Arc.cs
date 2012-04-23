using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Arc
    {
        public readonly Vertex NextVertex;
        public readonly double WightArc;
        public readonly List<PointCoordinates> Track;
        private List<int> IdInWay;

        public Arc(Vertex nextVertex, double wightArc, List<PointCoordinates> track)
        {
            if (nextVertex == null ||
               track == null)
            {
                throw new ArgumentNullException();
            }
            this.NextVertex = nextVertex;
            this.WightArc = wightArc;
            this.Track = track;
        }

        public void AddIdInWay(int id)
        {
            IdInWay.Add(id);
        }

        public bool IsBestWayForThisId(int id)
        {
            return (IdInWay.IndexOf(id) != -1);
        }

        public int[] GetAllIdInWay ()
        {
            return IdInWay.ToArray();
        }

    }
}
