using Microsoft.AspNetCore.Identity;
using System.Runtime.Serialization;

namespace DAL.Entities
{
    public class User : IdentityUser
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        public string Role { get; set; }
    }
}
