using Microsoft.AspNetCore.Identity;

namespace SmileMate.Common.Entities
{
    public class User : IdentityUser<long>
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
    }
}
