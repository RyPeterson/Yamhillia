using YamhilliaNET.Models.Entities;

namespace YamhilliaNET.ViewModels
{
    public class UserViewModel
    {
        public long Id { set; get; }
        public string Username { set; get; }
        
        public UserViewModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
        }
    }
}