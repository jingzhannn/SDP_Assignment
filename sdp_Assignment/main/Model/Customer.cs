namespace sdp_Assignment.main.Model
{
    internal class Customer : User
    {
        // Add this property to allow selection of a restaurant
        public Restaurant? SelectedRestaurant { get; set; }
    }
}
