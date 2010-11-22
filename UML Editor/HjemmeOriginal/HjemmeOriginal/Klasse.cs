using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;


namespace HjemmeOriginal
{
    public class Klasse:Objekt
    {
        public string[] Attrib;
        public string[] Method;
        public double Xkoor;
        public double Ykoor;
        
        public List<LineGeometry> EndLines { get; private set; }
        public List<LineGeometry> StartLines { get; private set; } 

        public Klasse(string name,string[] attrib, string[] method, double xkoor, double ykoor)
            :base(name)
        {
            Attrib = attrib;
            Method = method;
            Xkoor = xkoor;
            Ykoor = ykoor;
        }
    }
}
