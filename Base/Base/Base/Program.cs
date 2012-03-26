using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Base
{
    class Node
    {
        public float lat, lon;
        public int id,prior, zone;
    }

    class Address 
    {
        public int id, id_node, id_district, id_street, h_num;
        char corp_num;
        string corp_name;
    }

    class Dist 
    {
        public float dist,time;
        public int id,id_1,id_2;
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
        public string name,type;
        public int id;
    }
    
}