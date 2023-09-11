using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Payoneer : IPayoneerPayment
    {
        private Token token;
        public Token AuthToken()
        {
            return new Token();
        }
        public void SendPayment()
        {
            token = AuthToken();
            Console.WriteLine("Enviando pagamentos com Payoneer!");
        }

        public void ReceivePayment()
        {
            token = AuthToken();
            Console.WriteLine("Recebendo pagamentos com Payoneer!");
        }
    }
}
