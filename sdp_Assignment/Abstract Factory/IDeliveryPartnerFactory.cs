public interface IDeliveryPartnerFactory
    {
        IDeliveryRider CreateRider();
        ITrackingSystem CreateTrackingSystem();
    }