using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Repository.Services.Username_Hashing
{
    public interface IUsernameHasher
    {
        public string HashUserName(string userName);
        public bool VerifyUserName(string userName, string hashedUserName);
    }
}
