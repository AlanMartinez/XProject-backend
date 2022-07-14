using System.ComponentModel.DataAnnotations;
using XProject.Core.Enumerations;

namespace XProject.Core.Entities
{
    public class Security : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType Role { get; set; }
        public User? User { get; set; }

        private readonly EmailAddressAttribute Validator;

        public Security() => Validator = new EmailAddressAttribute();

        public void CreateUser(string email)
        {
            if(!Validator.IsValid(email))
                throw new ValidationException("Username format invalid");

            User = new User() { Email = email, Phone = "" };
        }
    }
}