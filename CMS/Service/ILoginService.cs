using CMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public interface ILoginService
    {
        Task<int> AuthenticateUserAsync(string username, string password);

        
    }
}
