namespace sdp_Assignment.main.Adapter
{public class PayPalService
    {
        public bool MakeTransaction(string email, double totalAmount)
        {
            Console.WriteLine($"[PayPal] Charging {email} ${totalAmount}...");
            return true;
        }
    }
}