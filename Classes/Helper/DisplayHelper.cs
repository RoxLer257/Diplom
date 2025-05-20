using System.Linq;

namespace Diplom.Classes.Helper
{
    public static class DisplayHelper
    {
        // Формирование строки для клиента
        public static string GetClientDisplayName(Clients client)
        {
            if (client == null) return "Нет данных";

            // Предположим, что ID типа клиента 1 - это физическое лицо, а 2 - юрлицо.
            if (client.ClientTypeID == 1) // Физическое лицо
            {
                return $"{client.LastName} {client.FirstName} {client.MiddleName}".Trim();
            }
            else if (client.ClientTypeID == 2) // Юридическое лицо
            {
                return client.CompanyName ?? "Не указано";
            }
            return "Неизвестный тип";
        }

        // Формирование строки для водителей и сотрудников
        public static string GetFullName(string lastName, string firstName, string middleName)
        {
            return $"{lastName} {firstName} {middleName}".Trim();
        }

        // Формирование строки для полисов
        public static string GetPolicyClientFullName(Policies policy)
        {
            var client = policy.Clients.FirstOrDefault();
            return client != null ? GetClientDisplayName(client) : "Нет данных";
        }
    }
}