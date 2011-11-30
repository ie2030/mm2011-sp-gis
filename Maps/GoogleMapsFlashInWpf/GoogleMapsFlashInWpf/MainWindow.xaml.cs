using System;
using System.Linq;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections;
using Flash.External;

namespace GoogleMapsFlashInWpf
{
    ///
    /// Interaction logic for Window1.xaml
    ///
    public partial class Window1 : Window
    {

        private ExternalInterfaceProxy proxy;
        
        public Window1()
        {
            InitializeComponent();
        }
        
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            axFlash.FlashVars = "ABQIAAAA6Je9wEhpps-6h4SLEzfx0hQcnJAsAPU0edVn7hFTQC8ea3A_VBRkCS6mZzjo_25VBG1y_bIKsYiRMg";
            axFlash.Movie = System.Windows.Forms.Application.StartupPath + "\\GoogleMaps.swf";

            proxy = new ExternalInterfaceProxy(axFlash);
            proxy.ExternalInterfaceCall += new ExternalInterfaceCallEventHandler(proxy_ExternalInterfaceCall);
        }
        
        /// <summary>
        /// Parse calls from flash application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private object proxy_ExternalInterfaceCall(object sender, ExternalInterfaceCallEventArgs e) {
          switch (e.FunctionCall.FunctionName) {
            case "setPosition":
              setPosition(e.FunctionCall.Arguments[0].ToString(), e.FunctionCall.Arguments[1].ToString());
              return null;
          }
          return null;
        }

        private void setPosition(string newLat, string newLng) 
        {
          lat.Text = newLat;
          lng.Text = newLng;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {   
            if (e.Key == Key.Enter)
                Search_Click(sender, null);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (address.Text != "")
              proxy.Call("Search", address.Text);
        }
        /// <summary>
        /// Draw polyline on Map
        /// </summary>
        /// <param name="points">ArrayList of points. Each elem is ArrayList with lat and lng</param>
        private void drawLine(ArrayList points) {
            proxy.Call("Drawline", points);
        }
        
        /// <summary>
        /// Delete polyline from Map
        /// </summary>
        private void clearLine() {
            proxy.Call("Clearline");
        }
        
        // Example of using drawLine
        private void paintSomeLine(object sender, RoutedEventArgs e) {
          ArrayList points = new ArrayList();
          double x = Convert.ToDouble(lat.Text); //Current marker coordinats
          double y = Convert.ToDouble(lng.Text);
          points.Add(new ArrayList(new double[2]{x, y}));
          x -= 0.001;
          y += 0.001;
          points.Add(new ArrayList(new double[2]{x, y }));
          x += 0.0003;
          y -= 0.0015;
          points.Add(new ArrayList(new double[2]{x, y }));
          drawLine(points);
        }

        private void Clear_Click(object sender, RoutedEventArgs e) {
          clearLine();
        }
    }
}
