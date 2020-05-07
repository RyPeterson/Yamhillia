using YamhillaNET.Models;

namespace YamhillaNET.ViewModels
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