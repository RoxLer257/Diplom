using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Classes.ControlEventArgs
{

    public class PropertyTypeEventArgs : EventArgs
    {
        public PropertyTypes PropertyType { get; set; }
        public PropertyTypeEventArgs(PropertyTypes propertyType) => PropertyType = propertyType;
    }
}
