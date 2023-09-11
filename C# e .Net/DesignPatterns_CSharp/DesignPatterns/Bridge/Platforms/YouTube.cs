using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Platforms
{
    class YouTube : IPlatform
    {
        public YouTube()
        {
            ConfigureRMTP();
            Console.WriteLine("YouTube: Transmissão iniciada!");
        }
        public void AuthToken()
        {
            Console.WriteLine("YouTube: Autenticando aplicação.");
        }

        public void ConfigureRMTP()
        {
            AuthToken();
            Console.WriteLine("YouTube: Configurando servidor RMTP.");
        }
    }
}
