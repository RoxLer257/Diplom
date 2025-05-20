using Diplom.Classes.Helper;

namespace Diplom.Classes
{
    public partial class Employees
    {
        public string FullName
        {
            get { return DisplayHelper.GetFullName(LastName, FirstName, MiddleName); }
        }
    }
}