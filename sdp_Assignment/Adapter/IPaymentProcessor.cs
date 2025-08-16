public interface IPaymentProcessor
{
    bool ProcessPayment(string customerName, double amount);
}