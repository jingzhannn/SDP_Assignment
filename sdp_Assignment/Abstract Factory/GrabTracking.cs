public class GrabTracking : ITrackingSystem
    {
        public void TrackOrder(int orderId)
        {
            Console.WriteLine($"[Grab Tracking] Order {orderId} is on the way!");
        }
    }