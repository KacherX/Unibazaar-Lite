using System.Collections.Generic;
using Final.Models;

namespace Final.Services
{
    public static class FakeUserStore
    {
        public static List<AppUser> Users = new()
        {
            new AppUser { Username = "alice", DisplayName = "Alice Yılmaz", Password = "alice123" },
            new AppUser { Username = "bob", DisplayName = "Bob Demir", Password = "bob123" },
            new AppUser { Username = "carol", DisplayName = "Carol Kaya", Password = "carol123" },
            new AppUser { Username = "dave", DisplayName = "Dave Çelik", Password = "dave123" },
            new AppUser { Username = "eve", DisplayName = "Eve Aksoy", Password = "eve123" },
            new AppUser { Username = "frank", DisplayName = "Frank Güneş", Password = "frank123" }
        };
    }
}