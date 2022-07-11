using System.Collections.Generic;

[System.Serializable]
public class InventorySave
{
    public List<ItemType> items = new List<ItemType>();
    public List<int> quantities = new List<int>();
}
