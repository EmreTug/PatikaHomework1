using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PatikaHomework1.Interfaces;
using PatikaHomework1.Repositories;

namespace PatikaHomework1.Attributes
{
    // Kullanıcı yetkilendirme için oluşturulan Attribute sınıfı
    public class AuthorizeUserAttribute : TypeFilterAttribute
    {
        // Attribute için bağımlılık oluşturulan constructor

        public AuthorizeUserAttribute() : base(typeof(AuthorizeUserFilter))
        {
            Arguments = new object[] { new UserRepository() };
        }
    }
    // Yetkilendirme işleminin yapılacağı filtre sınıfı
    public class AuthorizeUserFilter : ActionFilterAttribute
    {
        // Kullanıcıların saklandığı repository nesnesi
        private readonly IUserRepository _userRepository;

        // Constructor ile bağımlılık yönetimi yapılıyor.
        public AuthorizeUserFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Action çalıştırılmadan önce yetkilendirme işlemi yapılıyor.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // İstek header bilgilerinden kullanıcı adı ve şifre çekiliyor.
            string username = context.HttpContext.Request.Headers["Username"];
            string password = context.HttpContext.Request.Headers["Password"];

            // Kullanıcı adı ve şifre repository sınıfında doğrulanıyor.
            if (!_userRepository.ValidateUser(username, password))
            {
                // Yetkilendirme başarısız olduğunda unauthorized result döndürülüyor.
                context.Result = new UnauthorizedResult();
            }
        }
    }

}