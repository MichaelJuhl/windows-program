﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testerTil02350
{
    public class Class:Object
    {
        public string[] Attrib;
        public string[] Method;
        public int Xkoor;
        public int Ykoor;
        
        public Class(string name,string[] attrib, string[] method, int xkoor, int ykoor)
            :base(name)
        {
            Attrib = attrib;
            Method = method;
            Xkoor = xkoor;
            Ykoor = ykoor;
        }
    }
}
