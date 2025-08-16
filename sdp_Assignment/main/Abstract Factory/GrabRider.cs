public class GrabRider : IDeliveryRider
    {
        public void DeliverOrder(string orderDetails)
        {
            Console.WriteLine($"[Grab Rider] Delivering order: {orderDetails}");
        }
    }