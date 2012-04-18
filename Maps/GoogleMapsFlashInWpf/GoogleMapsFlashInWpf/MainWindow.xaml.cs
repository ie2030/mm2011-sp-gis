using System.Windows;
using System.Collections.Generic;
using System.Collections;
using Flash.External;

namespace GoogleMapsFlashInWpf
{
    ///
    /// Interaction logic for Window1.xaml
    ///
    public partial class Window1 :Window
    {

        private ExternalInterfaceProxy proxy;
        private double stLat, stLng, fnLat, fnLng;
        private Services.AlgorithmServerClient algorithm;

        public Window1()
        {
            InitializeComponent();
            algorithm = new Services.AlgorithmServerClient();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            axFlash.FlashVars = "ABQIAAAA6Je9wEhpps-6h4SLEzfx0hQcnJAsAPU0edVn7hFTQC8ea3A_VBRkCS6mZzjo_25VBG1y_bIKsYiRMg";
            axFlash.Movie = System.Windows.Forms.Application.StartupPath + "\\GoogleMaps.swf";

            proxy = new ExternalInterfaceProxy(axFlash);
            proxy.ExternalInterfaceCall += new ExternalInterfaceCallEventHandler(proxy_ExternalInterfaceCall);

            stButton.IsEnabled = true;
            fnButton.IsEnabled = true;
        }

        /// <summary>
        /// Parse calls from flash application
        /// </summary>
        private object proxy_ExternalInterfaceCall(object sender, ExternalInterfaceCallEventArgs e)
        {
            switch (e.FunctionCall.FunctionName)
            {
                case "setStPosition":
                    setStPosition((double)e.FunctionCall.Arguments[0], (double)e.FunctionCall.Arguments[1]);
                    return null;
                case "setFnPosition":
                    setFnPosition((double)e.FunctionCall.Arguments[0], (double)e.FunctionCall.Arguments[1]);
                    return null;
            }
            return null;
        }

        private void setStPosition(double lat, double lng)
        {
            stLat = lat;
            stLng = lng;
        }

        private void setFnPosition(double lat, double lng)
        {
            fnLat = lat;
            fnLng = lng;
        }

        /// <summary>
        /// Draw polyline on Map
        /// </summary>
        /// <param name="points">ArrayList of points. Each elem is ArrayList with lat and lng</param>
        private void drawLine(ArrayList points)
        {
            if (proxy != null)
                proxy.Call("Drawline", points);
        }

        private void clear()
        {
            if (proxy != null)
                proxy.Call("Clear");
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

        private void stButton_Checked(object sender, RoutedEventArgs e)
        {
            if (proxy != null)
                proxy.Call("SelectMarker", 0);
        }

        private void fnButton_Checked(object sender, RoutedEventArgs e)
        {
            if (proxy != null) 
                proxy.Call("SelectMarker", 1);
        }


        private void get_Path_Click(object sender, RoutedEventArgs e)
        {
            Services.Node start = new Services.Node();
            start.lat = stLat;
            start.lon = stLng;
            Services.Node finish = new Services.Node();
            finish.lat = fnLat;
            finish.lon = fnLng;
            Services.Node[] path = algorithm.getShortestPath(start, finish);
            Title = path.Length.ToString();
            ArrayList points = new ArrayList();
            foreach (Services.Node point in path){
                points.Add(new ArrayList(new double[2] {point.lat, point.lon}));
            }
            drawLine(points);
        }

    }
}
