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
        buyButton.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked();});
        addButton.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); AddAmount();});
        removeButton.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); RemoveAmount();});
    }

    public void InitializeShopItem(ItemType item)
    {
        itemImage.sprite = Storage.GetItemShopIcon(item);
        price = Storage.GetItemPrice(item);
        Amount = 1;
    }

    private void AddAmount() => Amount++;
    private void RemoveAmount() => Amount--;
}
