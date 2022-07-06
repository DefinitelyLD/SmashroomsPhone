using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GameObject itemsPanel;
    [SerializeField] List<ItemSlot> slots = new List<ItemSlot>();
    private List<ItemType> items = new List<ItemType>();

    private void Start() 
    {
        AddItem(ItemType.gloomDust);
        AddItem(ItemType.gloomDust);
        AddItem(ItemType.gloomDust);
        AddItem(ItemType.gloomDust);
        AddItem(ItemType.crystal);
        UpdateUI();
    }

    private void UpdateUI()
    {
        for(int i = 0; i < slots.Count; i++)
        {
            ItemType item = i < items.Count ? items[i] : ItemType.NONE;
            slots[i].UpdateSlot(item);
        }
    }
    private void AddItem(ItemType itemType)
    {
        if(items.Contains(itemType) == false) items.Add(itemType);
        slots[items.IndexOf(itemType)].Quantity ++;
        UpdateUI();
    }
    private void RemoveItem(ItemType itemType)
    {
        if(items.Contains(itemType) == false) return;
        slots[items.IndexOf(itemType)].Quantity --;
        if(slots[items.IndexOf(itemType)].Quantity == 0) items.Remove(itemType);
        UpdateUI();
    }
}
