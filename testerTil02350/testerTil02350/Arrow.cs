using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace testerTil02350
{
    class Arrow : Objekt
    {
        int XBegin, YBegin, XEnd, YEnd;
        public Arrow(string name,int xBegin,int xEnd,int yBegin,int yEnd)
            : base(name)
        {
            XBegin = xBegin;
            YBegin = xBegin;
            XEnd = xEnd;
            YEnd = yEnd;
        }
    }
}
