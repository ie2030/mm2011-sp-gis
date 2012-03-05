using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DBComponent {
    [DataContract]
    public class Point {
        public Point(double lat, double lng) {
            this.lat = lat;
            this.lng = lng;
        }
        public Point(double lat, double lng, string name) {
            this.lat = lat;
            this.lng = lng;
            this.name = name;
        }
        [DataMember]
        public double lat, lng;
        [DataMember]
        public string name;
        
    }
}
