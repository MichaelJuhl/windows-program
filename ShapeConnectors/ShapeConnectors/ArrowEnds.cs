using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeConnectors
{
    /// <summary>
    ///     Indicates which end of the line has an arrow.
    /// </summary>
    [Flags]
    public enum ArrowEnds
    {
        None = 0,
        Start = 1,
        End = 2,
        Both = 3
    }
}