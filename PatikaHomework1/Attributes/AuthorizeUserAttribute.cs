using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PatikaHomework1.Interfaces;
using PatikaHomework1.Repositories;

namespace PatikaHomework1.Attributes
{

    public class AuthorizeUserAttribute : TypeFilterAttribute
    {
        public AuthorizeUserAttribute() : base(typeof(AuthorizeUserFilter)) {
            Arguments = new object[] { new UserRepository() };
        }
    }

    public class AuthorizeUserFilter : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;

        public AuthorizeUserFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string username = context.HttpContext.Request.Headers["Username"];
            string password = context.HttpContext.Request.Headers["Password"];


            if (!_userRepository.ValidateUser(username, password))
            {
                context.Result = new UnauthorizedResult();
            }
        }
       
    }

   
}
