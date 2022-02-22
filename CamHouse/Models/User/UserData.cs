using Microsoft.AspNetCore.Identity;

namespace CamHouse.Models.User
{
    public class UserData : IdentityUser
    {
        [PersonalData]
        public string Liked { get; set; }
        [PersonalData]

        public string Favorites { get; set; }
    }
}
