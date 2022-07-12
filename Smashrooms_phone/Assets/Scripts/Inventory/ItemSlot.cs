using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] GameObject amount;
    [SerializeField] TextMeshProUGUI itemAmountText;

    private ItemType currentItem;
    private int quantity;
    public int Quantity
    {
        get => quantity;
        set
        {
            quantity = value;
            itemAmountText.text = quantity < 10 ? quantity.ToString() : "9+";
        }
    }

    public void UpdateSlot(ItemType _item)
    {
        itemIcon.enabled = _item != ItemType.NONE;
        amount.SetActive(_item != ItemType.NONE);
        if(_item == ItemType.NONE) return;

        currentItem = _item;
        itemIcon.sprite = Storage.GetItemIcon(currentItem);
    }
}
