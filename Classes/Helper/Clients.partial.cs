using Diplom.Classes.Helper;

namespace Diplom.Classes
{
    public partial class Clients
    {
        public string DisplayName
        {
            get { return DisplayHelper.GetClientDisplayName(this); }
        }
    }
}