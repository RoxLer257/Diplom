using System.Linq;

namespace Diplom.Classes.Helper
{
    public static class DisplayHelper
    {
        public static string GetClientDisplayName(Clients client)
        {
            if (client == null) return "Нет данных";

            if (client.ClientTypeID == 1) 
            {
                return $"{client.LastName} {client.FirstName} {client.MiddleName}".Trim();
            }
            else if (client.ClientTypeID == 2) 
            {
                return client.CompanyName ?? "Не указано";
            }
            return "Неизвестный тип";
        }

        public static string GetFullName(string lastName, string firstName, string middleName)
        {
            return $"{lastName} {firstName} {middleName}".Trim();
        }

        public static string GetPolicyClientFullName(Policies policy)
        {
            var client = policy.Clients.FirstOrDefault();
            return client != null ? GetClientDisplayName(client) : "Нет данных";
        }
    }
}