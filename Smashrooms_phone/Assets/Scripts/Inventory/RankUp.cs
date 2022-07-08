using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RankUp : MonoBehaviour
{
    [SerializeField] Button rankUpButton;
    [SerializeField] List<RectTransform> itemParents;
    [SerializeField] List<Image> itemIcons;
    [SerializeField] List<TextMeshProUGUI> itemTexts;

    private const int MAX_RANK = 3;
    private int currentRank = 1;
    private List<ItemWithAmount> itemsToRankUp;

    private void Awake()
    {
        rankUpButton.onClick.AddListener(() => TryRankUp());
    }

    private void Start() {
        UpdateUI();
    }

    public void UpdateUI()
    {
        DisableItems();
        Debug.Log(currentRank);
        itemsToRankUp = Storage.GetRankUpItems(currentRank);
        for(int i = 0; i < itemsToRankUp.Count; i++)
        {
            itemParents[i].gameObject.SetActive(true);
            itemIcons[i].sprite = Storage.GetItemIcon(itemsToRankUp[i].item);
            itemTexts[i].text = Storage.GetItemName(itemsToRankUp[i].item) + " " + InventoryManager.instance.GetItemAmount(itemsToRankUp[i].item) 
                                +" / " + itemsToRankUp[i].amount.ToString();
        }
        SetItemsPositions();
    }
    private void DisableItems()
    {
        foreach(RectTransform item in itemParents)
        item.gameObject.SetActive(false);
    }
    private void SetItemsPositions()
    {
        if(itemsToRankUp.Count == 1)
        {
            itemParents[0].anchoredPosition = new Vector2(360, 60); 
        }
        else if(itemsToRankUp.Count == 2)
        {
            itemParents[0].anchoredPosition = new Vector2(360, 150); 
            itemParents[1].anchoredPosition = new Vector2(360, -20);
        }
        else
        {
            itemParents[0].anchoredPosition = new Vector2(360, 230); 
            itemParents[1].anchoredPosition = new Vector2(360, 60);
            itemParents[2].anchoredPosition = new Vector2(360, -90);  
        }
    }

    private void TryRankUp()
    {
        if(InventoryManager.instance.TryBuyItem(itemsToRankUp) == false) return;

        if(currentRank < MAX_RANK) currentRank++;
        UpdateUI();
    }
}
