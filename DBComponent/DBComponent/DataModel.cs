using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DBComponent
{   
    /// <summary>
    /// Point on map
    /// </summary>
    [DataContract]
    public class Node
    {
        public Node(double _lat, double _lon)
        {
            lat = _lat;
            lon = _lon;
        }
        [DataMember]
        public double lat, lon;
        [DataMember]
        public int id, prior, zone;
    }
    [DataContract]
    public class Address
    {
        [DataMember]
        public int id, id_node, id_district, id_street, h_num;
        [DataMember]
        public char corp_num;
        [DataMember]
        public string corp_name;
    }

    public class Dist
    {
        public double dist, time;
        public int id, id_1, id_2;
    }

    class District
    {
        public string name;
        public int id;
    }

    class DSConnection
    {
        public int id, id_street, id_district;
    }

    class SName
    {
        int id, id_node;
        string name;
    }

    [DataContract]
    public class Street
    {
        [DataMember]
        public string name, type;
        [DataMember]
        public int id;
    }
    
}
