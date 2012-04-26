using System.Windows;
using System.Collections.Generic;
using System.Collections;
using Flash.External;
using System;
using System.IO;

namespace GoogleMapsFlashInWpf
{
    public partial class Window1 : Window
    {
        Database.Street[] streets;
        Database.DBServerClient database;

        int getNumber(int i, string text) { 
            string a="";
            while (text[i] >= '0' && text[i] <= '9') a += text[i];
            if (a != "")
            {
                return Convert.ToInt32(a);
            }
            else return -1;

        }
        
        int findHouse(string Adress, string StreetName){
            string[] parts = Adress.Split(new string[] {" " , StreetName},StringSplitOptions.RemoveEmptyEntries);
            string [] adParts = StreetName.Split(' ');
            int n = parts.Length;
            for (int i = 0; i < n; i++)
            {
                int b = 0;
                for (int j = 0; j < parts[i].Length; j++)
                    if (parts[i][j] >= '0' && parts[i][j] <= '9') b++;
                if (b == parts[i].Length) return Convert.ToInt32(parts[i]);
            }
            return - 1;
        }
        
        bool gap(string text)
        {
            int n = text.Length;
            for (int i = 0; i < n; i++)
            {
                if (text[i] == ' ') return true;
            }
            return false;
        }
        
        bool IfEqual(string text)
        {
            foreach (Database.Street curr in streets)
                if (curr.name.Equals(text))
                    return true;
            return false;
        }
        
        string RightDevide(string text)
        {
            int n = text.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                if (text[i] == ' ')
                {
                    string a = text.Substring(0, i);
                    return a;
                }
            }
            return text; ;
        }
        
        string LeftDevide(string text)
        {
            int n = text.Length;
            for (int i = 0; i < n; i++)
            {
                if (text[i] == ' ')
                {
                    string b = text.Substring(i + 1);
                    return b;
                }
            }
            return text; ;
        }
        
        string FindStreet( string Adress)
        {
            if (IfEqual(Adress)) return Adress;
            if (gap(Adress) == false) return "ERROR!!!";
            string res = FindStreet(LeftDevide(Adress));
            if (res != "ERROR!!!") return res;
            else
            {
                return FindStreet(RightDevide(Adress));
            }

        }

        private int getStreetID(string name)
        {
            foreach (Database.Street curr in streets)
                if (curr.name.Equals(name))
                    return curr.id;
            return -1;
        }

        private void parseAddress(object sender, RoutedEventArgs e)
        {
            string addrStr = adress.Text;
            Database.Address addr = new Database.Address();
            string streetName = FindStreet(addrStr);
            if (streetName.Equals("ERROR!!!"))
                return;
            //Title = streetName;
            addr.id_street = getStreetID(streetName);
            addr.h_num = findHouse(addrStr, streetName);
            Database.Node node = database.getNodeByAdress(addr);
            if (node != null)
            {
                //Title = node.lat.ToString() + " " + node.lon + "( " + addr.id_street + " " + addr.h_num + ")";
                if ((bool) stButton.IsChecked)
                {
                    setStPosition(node.lat, node.lon);
                    setStartMarker(node.lat, node.lon);
                }
                else
                {
                    setFnPosition(node.lat, node.lon);
                    setFinishMarker(node.lat, node.lon);
                }
                return;
            }
                /// FUCK FUCK FUCK FUCK!!!!!!fffff
        }
    }
}
