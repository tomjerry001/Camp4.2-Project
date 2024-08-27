using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Repository
{
    public interface ILoginRepository
    {
        Task<int> GetRoleIdAsync(string userName, string password);
    }
}
