using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApi.Alpari.Models.Entities
{
    public class User
    {
        public Guid Id{ get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; } =true;
        public ICollection<UserToken> userToken { get; set; }
    }

    public class UserToken
    {
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExpired { get; set; }

        public string VersionOS { get; set; }

        public User user { get; set; }
        public Guid UserId { get; set; }

        public string  RefreshToken { get; set; }
        public DateTime RefreshTokenExpir { get; set; }
    }
}
