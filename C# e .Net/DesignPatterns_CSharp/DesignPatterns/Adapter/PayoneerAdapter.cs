using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class PayoneerAdapter : IPayPalPayment
    {
        private Payoneer payoneer;
        public PayoneerAdapter(Payoneer payoneer)
        {
            this.payoneer = payoneer;
        }
        public Token AuthToken()
        {
            return this.payoneer.AuthToken();
        }

        public void PayPalPayment()
        {
            this.payoneer.SendPayment();
        }

        public void PayPalReceive()
        {
            this.payoneer.ReceivePayment();
        }
    }
}
