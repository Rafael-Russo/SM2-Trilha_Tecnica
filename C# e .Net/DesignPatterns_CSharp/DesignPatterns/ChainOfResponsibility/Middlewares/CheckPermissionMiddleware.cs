using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainOfResponsibility.Middlewares
{
    class CheckPermissionMiddleware : Middleware
    {
        public override bool Check(string email, string password)
        {
            if(email == "master@hcode.com.br")
            {
                Console.WriteLine("Seja Bem-Vindo Administrador!");
                return true;
            }

            Console.WriteLine("Seja Bem-Vindo!");
            return CheckNext(email, password);
        }
    }
}
