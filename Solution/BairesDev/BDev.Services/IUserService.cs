using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDev.Services
{
    public interface IUserService
    {
        bool ValidateUserPassword(string username, string password);
    }
}
