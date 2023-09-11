using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            // PayPal payment = new PayPal();
            // Payoneer payment = new Payoneer();
            IPayPalPayment payment = new PayoneerAdapter(new Payoneer());

            payment.PayPalPayment();
            payment.PayPalReceive();

            // payment.SendPayment();
            // payment.ReceivePayment();

            Console.ReadLine();
        }
    }
}
