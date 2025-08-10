public class MenuItem
{
    private string name;
    private double price;
    private string description;
    public string Name { get { return name; } }
    public double Price { get { return price; } }
    public string Description { get { return description; } }
    public MenuItem(string name, double price, string desc)
    {
        this.name = name;
        this.price = price;
        this.description = desc;
    }
}