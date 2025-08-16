namespace sdp_Assignment.main.AbstractFactory
{
    internal class CustomizationRule
    {
        public string Name { get; set; }              // e.g. "No Lettuce"
        public List<string> RemoveIngredients { get; set; } = new();
        public List<string> AddIngredients { get; set; } = new();
        public double PriceAdjustment { get; set; }   // e.g. +2.0 for extra cheese
    }
}
