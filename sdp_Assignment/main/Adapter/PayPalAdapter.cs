namespace sdp_Assignment.main.Adapter

{
    public class PayPalAdapter : IPaymentProcessor
    {
        private PayPalService _payPalService;
        private string _customerEmail;

        public PayPalAdapter(PayPalService payPalService, string customerEmail)
        {
            _payPalService = payPalService;
            _customerEmail = customerEmail;
        }

        public bool ProcessPayment(string customerName, double amount)
        {
            Console.WriteLine($"[Adapter] Mapping customer '{customerName}' to email '{_customerEmail}'...");
            return _payPalService.MakeTransaction(_customerEmail, amount);
        }
    }
}