using DataL;
using System.Linq;

namespace BusinessL.Services
{
    class UserSer
    {
        public UserSer() { uctx = new UsersContext(); }
        public UsersContext uctx { get; set; }
        public bool CreateNewUser(string login, string passwd)
        {
            if (uctx.Users.FirstOrDefault(user => user.Login == login) == null)
            {
                uctx.Users.Add(new User { Login = login, Password = passwd });
                uctx.SaveChanges();
                return true;
            }
            else
                return true;
        }
        public bool RePasswUser(string login, string newPasswd)
        {
            User user = uctx.Users.FirstOrDefault(user => user.Login == login);
            if (user.Password != newPasswd)
            {
                user.Password = newPasswd;
                uctx.Users.Update(user);
                uctx.SaveChanges();
                return true;
            }
            else return false;
            
        }
        public bool RenameUser(string oldLogin, string newLogin)
        {
            User user = uctx.Users.FirstOrDefault(user => user.Login == oldLogin);
            if (uctx.Users.FirstOrDefault(user => user.Login == newLogin) == null)
            {
                user.Login = newLogin;
                uctx.Users.Update(user);
                uctx.SaveChanges();
                return true;
            }
            else return false;

        }
        public bool DeleteUser(string login)
        {
            User user_ = uctx.Users.FirstOrDefault(user => user.Login == login);
            uctx.Users.Remove(user_);
            uctx.SaveChanges();
            return false;
        }
    }
}
