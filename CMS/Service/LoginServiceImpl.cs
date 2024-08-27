using CMS.Model;
using CMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public class LoginServiceImpl : ILoginService
    {
       
        private ILoginRepository _loginRepository;


        
        public LoginServiceImpl(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }



        public async Task<int> AuthenticateUserAsync(string username, string password)
        {
            return await _loginRepository.GetRoleIdAsync(username, password);
        }
       
    }
}
