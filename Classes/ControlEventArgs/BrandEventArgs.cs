using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Classes.ControlEventArgs
{
    public class BrandEventArgs : EventArgs
    {
        public VehicleMakes Brand { get; set; }
        public BrandEventArgs(VehicleMakes brand) => Brand = brand;
    }
}
