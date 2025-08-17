namespace sdp_Assignment.main.Adapter
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(string customerName, double amount);
    }
}