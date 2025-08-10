public class Menu : IMenuCollection
{
    private List<MenuItem> _items;
    public Menu()
    {
        _items = new List<MenuItem>();
    }
    public void AddItem(MenuItem item)
    {
        _items.Add(item);
    }
    public IMenuIterator CreateIterator()
    {
        return new MenuIterator(_items);
    }
}