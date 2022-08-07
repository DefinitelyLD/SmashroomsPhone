using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField] GameObject itemsPanel;
    [SerializeField] List<ItemSlot> slots = new List<ItemSlot>();
    private List<ItemType> items = new List<ItemType>();
    private List<int> quantities = new List<int>();

    private void Awake() => instance = this;

    private void Start() => UpdateUI();

    public void LoadInventory(InventorySave save)
    {
        for(int i = 0; i < save.items.Count; i++)
        AddItem(save.items[i], save.quantities[i]);
    }

    private void UpdateUI()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            ItemType item = i < items.Count ? items[i] : ItemType.NONE;
            slots[i].Quantity = i < items.Count ? quantities[i] : 0;
            slots[i].UpdateSlot(item);
        }
    }
    public void AddItem(ItemType itemType, int amount)
    {
        if(items.Contains(itemType) == false) 
        {
            items.Add(itemType);
            quantities.Add(0);
        }
        int indexOff =  items.IndexOf(itemType);
        quantities[indexOff] += amount;
        slots[indexOff].Quantity = quantities[indexOff];
        UpdateUI();
    }
    public void RemoveItem(ItemType itemType, int amount)
    {
        if(items.Contains(itemType) == false) return;

        int indexOff =  items.IndexOf(itemType);
        if(quantities[indexOff] < amount) return;
        quantities[indexOff] -= amount;
        slots[items.IndexOf(itemType)].Quantity = quantities[indexOff];
        if(quantities[items.IndexOf(itemType)] == 0) 
        {
            items.Remove(itemType);
            quantities.RemoveAt(indexOff);
        }
        UpdateUI();
    }

    public int GetItemAmount(ItemType itemType)
    {
        if(items.Contains(itemType) == false) return 0;
        else return quantities[items.IndexOf(itemType)];
    }

    public bool TryBuyItem(List<ItemWithAmount> cost)
    {
        foreach (ItemWithAmount item in cost)
        {
            if(items.Contains(item.item) == false) return false;
            if(quantities[items.IndexOf(item.item)] < item.amount) return false;
        }

        foreach (ItemWithAmount item in cost) RemoveItem(item.item, item.amount);
        return true;
    }

    public InventorySave GetInventorySave()
    {
        InventorySave save = new InventorySave();
        for(int i = 0; i < items.Count; i++)
        {
            save.items.Add(items[i]);
            save.quantities.Add(quantities[items.IndexOf(items[i])]);
        }
        
        return save;
    }
}
