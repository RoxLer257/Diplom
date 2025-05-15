using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Classes.ControlEventArgs
{
    public class ModelEventArgs : EventArgs
    {
        public VehicleModels Model { get; set; }
        public ModelEventArgs(VehicleModels model) => Model = model;
    }
}
