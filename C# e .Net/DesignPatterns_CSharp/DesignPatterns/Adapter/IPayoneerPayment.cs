namespace Adapter
{
    interface IPayoneerPayment
    {
        Token AuthToken();
        void SendPayment();
        void ReceivePayment();
    }
}
