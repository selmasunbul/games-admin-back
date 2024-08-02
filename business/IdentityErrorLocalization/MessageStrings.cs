using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business.IdentityErrorLocalization
{
    public static class ValidationMessages
    {
        public const string NotEmptyMessage = "Bu alan boş olamaz.";
        public const string NotEmpty = "Bu alan boş olamaz.";
        public const string NotNull = "Bu alan boş olamaz.";
        public const string NotValid = "Bu alan geçerli değil.";

        public const string EmailNotValid = "E-Posta geçerli değil.";
        public const string PasswordNotValid = "Şifre geçerli değil.";
        public const string EmailAlreadyInTheSystem = "E-Posta adresi sistemde zaten kayıtlı.";
        public const string UserNameOrPasswordIncorrect = "Kullanıcı adı veya şifre yanlış.";
        public const string PasswordRulesNotMatching = "Şifre büyük harf, küçük harf, rakam ve özel karakter(!*?-) içermelidir.";
    }
    public static class ResultMessages
    {
        public const string Successfull = "Başarılı";
        public const string Error = "Hata";

        public const string DataSuccessfullySaved = "Verileriniz başarıyla kaydedildi.";
        public const string DataSaveError = "Verileriniz kaydedilirken hata oluştu.";

        public const string EmailSendingError = "E-Posta gönderilirken hata oluştu.";
        public const string EmailSuccessfullySended = "E-Posta gönderildi.";

        public const string UserNotFound = "Kullanıcı sistemde kayıtlı değil.";


        public const string UserPasswordNotChanged = "Şifre değiştirirken hata oluştu.";
        public const string UserCreationError = "Kullanıcı oluşturulurken hata oluştu.";

    }
    public static class IdentityErrorMessages
    {
        public const string DuplicateEmail = "E-posta adresi '{0}' sistemde mevcut.";
        public const string DuplicateUserName = "Kullanıcı adı '{0}' sistemde mevcut.";
        public const string InvalidEmail = "E-posta adresi '{0}' geçerli değil.";
        public const string DuplicateRoleName = "Rol adı '{0}' sistemde mevcut.";
        public const string InvalidRoleName = "Rol adı '{0}' geçerli değil.";
        public const string InvalidToken = "Doğrulama kodu geçerli değil.";
        public const string InvalidUserName = "Kullanıcı adı '{0}' geçerli değil.";
        public const string LoginAlreadyAssociated = "Bir harici giriş zaten bu hesaba bağlı.";
        public const string PasswordMismatch = "Şifreler eşleşmiyor.";
        public const string PasswordRequiresDigit = "Şifre rakam içermelidir.(0-9)";
        public const string PasswordRequiresLower = "Şifre küçük harf içermelidir.(a-z)";
        public const string PasswordRequiresNonAlphanumeric = "Şifre harf-rakam olmayan karakter içermelidir.(!+,-?)";
        public const string PasswordRequiresUniqueChars = "Şifre en az {0} benzersiz karakter içermelidir.";
        public const string PasswordRequiresUpper = "Şifre büyük harf içermelidir.(A-Z)";
        public const string PasswordTooShort = "Şifre en az {0} karakter uzunluğunda olmalıdır.";
        public const string UserAlreadyHasPassword = "Kullanıcının bir şifresi zaten var.";
        public const string UserAlreadyInRole = "Kullanıcının zaten '{0}' rolü var.";
        public const string UserNotInRole = "Kullanıcının '{0}' rolü yok.";
        public const string UserLockoutNotEnabled = "Kullanıcı kilidi aktif değil.";
        public const string RecoveryCodeRedemptionFailed = "Kurtarma kodu kullanılamadı.";
        public const string ConcurrencyFailure = "Eş zamanlama hatası.";
        public const string DefaultError = "Yönetim sistemi hatası.";
    }
}
