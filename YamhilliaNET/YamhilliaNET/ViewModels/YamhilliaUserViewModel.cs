using System;
using YamhilliaNET.Models;

namespace YamhilliaNET.ViewModels
{
    public class YamhilliaUserViewModel
    {
        private readonly YamhilliaUser user;

        public YamhilliaUserViewModel(YamhilliaUser user)
        {
            this.user = user;
        }

        public string Email { get => user.Email; }
        public string Id { get => user.Id; }
        public string UserName { get => user.UserName; }

    }
}