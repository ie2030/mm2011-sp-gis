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

    class Address
    {
        public int id, id_node, id_district, id_street, h_num;
        char corp_num;
        string corp_name;
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

    class Street
    {
        public string name, type;
        public int id;
    }
    
}
