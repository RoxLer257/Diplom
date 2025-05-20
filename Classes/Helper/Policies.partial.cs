using Diplom.Classes.Helper;

namespace Diplom.Classes
{
    public partial class Policies
    {
        public string ClientFullName
        {
            get { return DisplayHelper.GetPolicyClientFullName(this); }
        }
    }
}