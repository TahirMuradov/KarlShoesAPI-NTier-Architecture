using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Messages
{
    public static class AuthStatus
    {
        public const string EmailExists = "Email mövcuddur!";
        public const string UserNotFound = "İstifadəçi tapılmadı!";
        public const string RoleNotFound = "Rol tapılmadı!";
        public const string UsernameExists = "İstifadəçi mövcuddur!";
        public const string RoleRemovedSuccessfully = "Rol uğurlu silindi!";
        public const string RoleUpdatedSuccessfully = "Rol uğurlu yeniləndi!";
        public const string RoleAddedSuccessfully = "Rol uğurlu əlavə olundu!";
        public const string RegisteridSuccessfully = "Uğurla qeydiyyatdan keçdiniz!";
        public const string EmailOrPasswordWrong = "İstifadəçi adınız və ya Şifrəniz yanlışdır!";
        public const string LogoutSuccessfully = "Çıxış olundu!";
    }
}
