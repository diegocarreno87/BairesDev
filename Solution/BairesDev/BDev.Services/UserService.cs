using BDev.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.Services
{
    public class UserService : IUserService
    {
        private UserBLL BLL;
        public UserService()
        {
            this.BLL = BLL = new UserBLL();
        }

        public bool ValidateUserPassword(string username, string password)
        {
            Common.User user = 
                new Common.User()
            {
                    username = username,
                    password = password
            };
            return this.BLL.ValidateUserPassword(user);
        }
    }
}
