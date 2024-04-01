using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Messages
{
    public static class MessageStatus
    {
        public const string ExceptionMessage = "Gözlənilməz xəta baş verdi!";
        public static string NotFoundMessage(string entityName) => $"Belə bir {entityName.ToLower()} yoxdur";
        public static string ExistMessage(string entityName) => $"Belə bir {entityName.ToLower()} mövcuddur";
        public static string NotNullMessage(string entityName) => $"{entityName.ToUpper()} null ola bilməz";
    }
}
