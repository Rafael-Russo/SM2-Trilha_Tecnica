using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Platforms
{
    class TwitchTV : IPlatform
    {
        public TwitchTV()
        {
            ConfigureRMTP();
            Console.WriteLine("TwitchTV: Transmissão iniciada!");
        }
        public void AuthToken()
        {
            Console.WriteLine("TwitchTV: Autenticando aplicação.");
        }

        public void ConfigureRMTP()
        {
            AuthToken();
            Console.WriteLine("TwitchTV: Configurando servidor RMTP.");
        }
    }
}
