using System;
using System.Linq;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Input;

namespace GoogleMapsFlashInWpf
{
    ///
    /// Interaction logic for Window1.xaml
    ///
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            AxShockwaveFlashObjects.AxShockwaveFlash axFlash = wfh.Child as AxShockwaveFlashObjects.AxShockwaveFlash;
            axFlash.FlashVars = "ABQIAAAA6Je9wEhpps-6h4SLEzfx0hQcnJAsAPU0edVn7hFTQC8ea3A_VBRkCS6mZzjo_25VBG1y_bIKsYiRMg";
            axFlash.Movie = System.Windows.Forms.Application.StartupPath + "\\GoogleMaps.swf";

            axFlash.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(axFlash_FlashCall);
        }

        void axFlash_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
        {
            XDocument call = XDocument.Parse(e.request);

            var q = from c in call.Elements("invoke")
                    select new
                    {
                        Name = c.Attribute("name").Value,
                        Arguments = c.Element("arguments").Descendants()
                    };
            foreach (var i in q)
            {
                if (i.Name == "setPosition")
                {
                    lat.Text = i.Arguments.ElementAt(0).Value;
                    lng.Text = i.Arguments.ElementAt(1).Value;
                }
            }

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Search_Click(sender, null);
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            AxShockwaveFlashObjects.AxShockwaveFlash axFlash = wfh.Child as AxShockwaveFlashObjects.AxShockwaveFlash;

            XElement call = new XElement("invoke",
                    new XAttribute("name", "Search"),
                    new XAttribute("returntype", "xml"),
                    new XElement("arguments",
                        new XElement("string", address.Text)
                    )
            );
            axFlash.CallFunction(call.ToString(SaveOptions.DisableFormatting));
        }
    }
}