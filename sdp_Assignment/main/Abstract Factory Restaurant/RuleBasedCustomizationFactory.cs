using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.AbstractFactory
{
    internal class RuleBasedCustomizationFactory
    {
        private CustomizationRule rule;

        public RuleBasedCustomizationFactory(CustomizationRule rule)
        {
            this.rule = rule;
        }

        public MenuItem Customize(MenuItem baseItem)
        {
            var customIngredients = new List<string>(baseItem.Ingredients);

            // Remove unwanted ingredients
            foreach (var ing in rule.RemoveIngredients)
                customIngredients.RemoveAll(i => i.Equals(ing, StringComparison.OrdinalIgnoreCase));

            // Add new ingredients
            customIngredients.AddRange(rule.AddIngredients);

            return new MenuItem(
                $"{baseItem.Name} ({rule.Name})",
                baseItem.Availability,
                baseItem.Price + rule.PriceAdjustment,
                customIngredients
            );
        }
    }
}
