using sdp_Assignment.main.Composite;

namespace sdp_Assignment.main.AbstractFactory
{
    internal class CustomizationEngine
    {
        private List<CustomizationRule> rules = new List<CustomizationRule>();

        public void AddRule(CustomizationRule rule)
        {
            rules.Add(rule);
        }

        public MenuItem Apply(MenuItem baseItem, List<string> selectedCustomizations)
        {
            MenuItem current = baseItem;

            foreach (var selected in selectedCustomizations)
            {
                var rule = rules.FirstOrDefault(r => r.Name == selected);
                if (rule != null)
                {
                    var factory = new RuleBasedCustomizationFactory(rule);
                    current = factory.Customize(current);
                }
            }

            return current;
        }
    }
}
