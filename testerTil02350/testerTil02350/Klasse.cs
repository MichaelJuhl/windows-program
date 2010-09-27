using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testerTil02350
{
    public class Klasse:Objekt
    {
        public string[] Attrib;
        public string[] Method;
        public double Xkoor;
        public double Ykoor;
        
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
