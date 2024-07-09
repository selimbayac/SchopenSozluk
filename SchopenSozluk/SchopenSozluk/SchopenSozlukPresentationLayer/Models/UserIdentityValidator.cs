using Microsoft.AspNetCore.Identity;
namespace SchopenSozlukPresentationLayer.Models
{
    public class UserIdentityValidator : IdentityErrorDescriber
    {
       public override IdentityError PasswordTooShort(int lengt)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description=$"Parola en az {lengt} karakter olmalıdır"
            };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresUpper",
                Description = "Lütfen en az 1 büyük harf giriniz"
            }; // devamı gelir
        }
    }
}
