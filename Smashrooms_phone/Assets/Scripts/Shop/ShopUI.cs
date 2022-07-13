using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] Sprite potionsActive, potionsInactive, reagentsActive, reagentsInactive, crystalsActive, crystalsInactive;
    [SerializeField] Button potions, reagents, crystals, scrollLeft, scrollRight;
    [SerializeField] Sprite arrowActive, arrowDisabled;
    [SerializeField] List<ShopItem> shopItems;
    [SerializeField] RectTransform shopItemsParent;

    private const float CARD_SIZE = 375;
    private const int CARD_PER_PAGE = 3;
    private SelectedTab selectedTab;
    private float minScrollPos;

    private Dictionary<SelectedTab, List<ItemType>> pageItems = new Dictionary<SelectedTab, List<ItemType>>()
    {
        { SelectedTab.potions, new List<ItemType>(){ ItemType.intelligencePotion, ItemType.agilityPotion, ItemType.strengthPotion, ItemType.endurancePotion }},
        { SelectedTab.reagents, new List<ItemType>(){ ItemType.goldClover, ItemType.gloomDust, ItemType.fireEssence, ItemType.airEssence, ItemType.relic }},
        { SelectedTab.crystals, new List<ItemType>(){ ItemType.crystal_lvl_1, ItemType.crystal_lvl_2, ItemType.crystal_lvl_3, ItemType.crystal_lvl_4, ItemType.crystal_lvl_5 }},
    };

    private void Awake()
    {
        potions.onClick.AddListener(() => SelectTab(SelectedTab.potions));
        reagents.onClick.AddListener(() => SelectTab(SelectedTab.reagents));
        crystals.onClick.AddListener(() => SelectTab(SelectedTab.crystals));
        scrollLeft.onClick.AddListener(() => ScrollItems(1));
        scrollRight.onClick.AddListener(() => ScrollItems(-1));
    }  

    private void Start() => SelectTab(SelectedTab.potions);

    public void SelectTab(SelectedTab tab)
    {
        potions.image.sprite = tab == SelectedTab.potions ? potionsActive : potionsInactive;
        reagents.image.sprite = tab == SelectedTab.reagents ? reagentsActive : reagentsInactive;
        crystals.image.sprite = tab == SelectedTab.crystals ? crystalsActive : crystalsInactive;

        for(int i = 0; i < shopItems.Count; i++)
        {
            if(i < pageItems[tab].Count) 
            {
                shopItems[i].gameObject.SetActive(true);
                shopItems[i].InitializeShopItem(pageItems[tab][i]);
            }
            else shopItems[i].gameObject.SetActive(false);
        }
        shopItemsParent.anchoredPosition = new Vector2(0, shopItemsParent.anchoredPosition.y);
        selectedTab = tab;

        minScrollPos = -CARD_SIZE * (pageItems[selectedTab].Count - CARD_PER_PAGE);
        ChangeScrollButtons();
    }

    private void ScrollItems(int scrollDirection)
    {
        float x = shopItemsParent.anchoredPosition.x;
        x += CARD_SIZE * scrollDirection;
        x = Mathf.Clamp(x, minScrollPos, 0);
        shopItemsParent.anchoredPosition = new Vector2(x, shopItemsParent.anchoredPosition.y);

        ChangeScrollButtons();
    }
    private void ChangeScrollButtons()
    {
        scrollRight.image.sprite = shopItemsParent.anchoredPosition.x == minScrollPos ? arrowDisabled : arrowActive;
        scrollLeft.image.sprite = shopItemsParent.anchoredPosition.x == 0 ? arrowDisabled : arrowActive;
    }

    public enum SelectedTab
    {
        potions,
        reagents,
        crystals,
    }
}
