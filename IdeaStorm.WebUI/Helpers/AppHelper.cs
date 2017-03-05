using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdeaStorm.Domain.Entities;

namespace IdeaStorm.WebUI.Helpers
{
    public static class AppHelper
    {
        public static User GetCurrentUser()
        {
            return new User() {UserName = "undef_user"};
        }
    }
}