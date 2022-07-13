using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] Button buyButton, addButton, removeButton;
    [SerializeField] TextMeshProUGUI amountText, priceText;

    private const int MAX_ITEMS = 10;
    private const int MIN_ITEMS = 1;

    private ItemType itemType;

    private int price;
    private int amount;
    private int Amount
    {
        get => amount;
        set 
        {
            amount = value;
            amount = Mathf.Clamp(amount, MIN_ITEMS, MAX_ITEMS);
            amountText.text = amount.ToString();
            priceText.text = (price * amount).ToString();
        }
    }

    private void Awake()
    {
        buyButton.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); TryBuyItem();});
        addButton.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); AddAmount();});
        removeButton.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); RemoveAmount();});
    }

    public void InitializeShopItem(ItemType _itemType)
    {
        itemImage.sprite = Storage.GetItemShopIcon(_itemType);
        price = Storage.GetItemPrice(_itemType);
        Amount = 1;
        itemType = _itemType;
    }

    private void AddAmount() => Amount++;
    private void RemoveAmount() => Amount--;

    private void TryBuyItem()
    {
        if(PlayerData.instance.MushroomTokens >= price * amount)
        {
            PlayerData.instance.MushroomTokens -= price * amount;
            InventoryManager.instance.AddItem(itemType, amount);
        }
    }
}
