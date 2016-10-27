using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiDispatcher.Core
{
    public interface ILoginService
    {

        bool Login(string name, string password);

        bool Logout();

        bool Relogin(string name, string password);

    }
}
