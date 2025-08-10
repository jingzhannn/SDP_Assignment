public class MenuIterator : IMenuIterator
{
    private List<MenuItem> _items;
    private int _position = 0;
    public MenuIterator(List<MenuItem> items)
    {
        _items = items;
    }
    public bool HasNext()
    {
        return _position < _items.Count;
    }
    public MenuItem Next()
    {
        if (HasNext())
        {
            return _items[_position++];
        }
        return null;
    }
}