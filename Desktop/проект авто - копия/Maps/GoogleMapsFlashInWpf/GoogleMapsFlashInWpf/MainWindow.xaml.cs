using System.Windows;
using System.Collections.Generic;
using System.Collections;
using Flash.External;
using System;
using System.ServiceModel;
using GoogleMapsFlashInWpf.Services;



namespace GoogleMapsFlashInWpf
{
    ///
    /// Interaction logic for Window1.xaml
    ///
    public partial class Window1 :Window
    {

        #region fields

        private ExternalInterfaceProxy proxy;
        private double stLat, stLng, fnLat, fnLng;
        private Services.AlgorithmServerClient algorithm;
        ///ArrayList of paths, counted by different algorithmes
        ArrayList[] paths;
        ///the amount of algorithmes, should be changed when new algorithmes will appear
        int k;
        

        #endregion

        #region constructor

        public Window1()
        {
            InitializeComponent();
            algorithm = new Services.AlgorithmServerClient();
            database = new Database.DBServerClient();
            streets = database.getStreets();
            k = 2;  
        }

        #endregion

        #region WindowLoaded

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            axFlash.FlashVars = "ABQIAAAA6Je9wEhpps-6h4SLEzfx0hQcnJAsAPU0edVn7hFTQC8ea3A_VBRkCS6mZzjo_25VBG1y_bIKsYiRMg";
            axFlash.Movie = System.Windows.Forms.Application.StartupPath + "\\GoogleMaps.swf";

            proxy = new ExternalInterfaceProxy(axFlash);
            proxy.ExternalInterfaceCall += new ExternalInterfaceCallEventHandler(proxy_ExternalInterfaceCall);

            stButton.IsEnabled = true;
            fnButton.IsEnabled = true;
        }

        #endregion

        
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
            ///Clear paths
            for (int i = 0; i < k; i++)
                paths[i] = null; 
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

        #region GetPath

        private void get_Path_Click(object sender, RoutedEventArgs e)
        {
            Services.Node start = new Services.Node();
            start.lat = stLat;
            start.lon = stLng;
            Services.Node finish = new Services.Node();
            finish.lat = fnLat;
            finish.lon = fnLng;
            
            try
            {
                Services.Node[] path = algorithm.getShortestPath(start, finish);
                separate_pathes(path);
                MessageBox.Show("Please, choose the algorithm to find the shortest path from list at the top panel");
                
            }
            catch (FaultException<ReadingFromConfigFault> ex)
            {
                separate_pathes(ex.Detail.RightPathes);
                MessageBoxResult result = MessageBox.Show(
                                  ex.Detail.Operation + " " +
                                    ex.Detail.Problem_message + " This name is: '"+ex.Detail.WrongName+"'.", 
                                  String.Empty, 
                                  MessageBoxButton.OK, 
                                  MessageBoxImage.Error);
                MessageBox.Show("Don't worry! Try to choose the algorithm, finding the shortest path, from the list 'Choose algorithm' at the top panel.");
            }
           
        }
        #endregion

        #region separate_pathes

        /// <summary>
        /// separating  paths in one list into  different lists
        /// </summary>
        void separate_pathes(Services.Node[] path)
        {
            int[] separate_dot_index = new int[k + 1];
            separate_dot_index[0] = -1;
            separate_dot_index[k] = int.MaxValue / 2;
            int i;

            ///Initialization of "paths"
            paths = new ArrayList[k];
            for (i = 0; i < k; i++)
                paths[i] = new ArrayList();

            ///searching of separating points
            i = 1;
            foreach (Services.Node point in path)
            {

                if (point.id == -1)
                {
                    separate_dot_index[i] = Array.IndexOf(path, point);
                    i++;
                }
            }


            i = 0;

            foreach (Services.Node point in path)
            {

                if ((separate_dot_index[i] < Array.IndexOf(path, point)) & (Array.IndexOf(path, point) <= separate_dot_index[i + 1]))
                {
                    if (Array.IndexOf(path, point) == separate_dot_index[i + 1])
                        i++;
                    else
                        paths[i].Add(new ArrayList(new double[2] { point.lat, point.lon }));
                }
            }

        }

        #endregion

        private void setStartMarker(double lat, double lng)
        {
            proxy.Call("SetStart", lat, lng);
        }

        private void setFinishMarker(double lat, double lng)
        {
            proxy.Call("SetFinish", lat, lng);
        }

        #region ComboBox

        private void comboBox1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string selectedItem = comboBox1.SelectedItem.ToString();
            if (selectedItem == "System.Windows.Controls.ComboBoxItem: Dijkstra")
              {
                  if (paths[0] != null)
                  {
                      ///Draws path, found by Dijkstra
                      drawLine(paths[0]);
                  }
                  else
                      MessageBox.Show("Push the 'get_path' button and then choose algorithm");
              }
            else if (selectedItem == "System.Windows.Controls.ComboBoxItem: Levit")
            {
                if (paths[1] != null)
                {
                    ///Draws path, found by Levit
                    drawLine(paths[1]);
                }
                else
                    MessageBox.Show("Push the 'get_path' button and then choose algorithm");
            }
        }
        #endregion
    }
}
