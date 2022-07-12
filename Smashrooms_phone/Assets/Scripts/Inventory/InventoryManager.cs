using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField] GameObject itemsPanel;
    [SerializeField] List<ItemSlot> slots = new List<ItemSlot>();
    private List<ItemType> items = new List<ItemType>();

    private void Awake() => instance = this;

    private void Start() 
    {
        AddItem(ItemType.gloomDust, 4);
        AddItem(ItemType.crystal_lvl_1, 4);
        AddItem(ItemType.goldClover, 4);
        UpdateUI();
    }
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
            slots[i].UpdateSlot(item);
        }
    }
    public void AddItem(ItemType itemType, int amount)
    {
        if(items.Contains(itemType) == false) items.Add(itemType);
        slots[items.IndexOf(itemType)].Quantity += amount;
        UpdateUI();
    }
    public void RemoveItem(ItemType itemType, int amount)
    {
        if(items.Contains(itemType) == false) return;
        if(slots[items.IndexOf(itemType)].Quantity < amount) return;

        slots[items.IndexOf(itemType)].Quantity -= amount;
        if(slots[items.IndexOf(itemType)].Quantity == 0) items.Remove(itemType);
        UpdateUI();
    }

    public int GetItemAmount(ItemType itemType)
    {
        if(items.Contains(itemType) == false) return 0;
        else return slots[items.IndexOf(itemType)].Quantity;
    }

    public bool TryBuyItem(List<ItemWithAmount> cost)
    {
        foreach (ItemWithAmount item in cost)
        {
            if(items.Contains(item.item) == false) return false;
            if(slots[items.IndexOf(item.item)].Quantity < item.amount) return false;
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
            save.quantities.Add(slots[items.IndexOf(items[i])].Quantity);
        }
        
        return save;
    }
}
