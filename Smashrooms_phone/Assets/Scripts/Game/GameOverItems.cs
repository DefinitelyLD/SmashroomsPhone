using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverItems : MonoBehaviour
{
    [SerializeField] List<RectTransform> items;
    [SerializeField] List<Image> images;

    private const int MAX_ITEMS = 3;
    private const float DROP_CHANCE = 0.25f;
    private const float TWEEN_TIME = 0.75f;

    public List<ItemType> DropItems()
    {
        for(int i = 0; i < images.Count; i++)
            images[i].color = new Color(255,255,255, 0);
    
        List<ItemType> droppedItems = new List<ItemType>();
        int itemsAmount = Enum.GetNames(typeof(ItemType)).Length;
        int successfulDrops = 0;
        for(int i = 1; i < itemsAmount; i++)
        {
            if(successfulDrops >= MAX_ITEMS) 
            {
                ShowItems(droppedItems);
                return droppedItems;
            }

            float roll = UnityEngine.Random.Range(0, 100f);
            if(roll <= DROP_CHANCE)
            {
                successfulDrops++;
                droppedItems.Add((ItemType)i);
            } 
        }

        ShowItems(droppedItems);
        return droppedItems;
    }

    private void ShowItems(List<ItemType> droppedItems)
    {
        for(int i = 0; i < droppedItems.Count; i++)
        {
            LeanTween.alpha(items[i], 1, TWEEN_TIME).setDelay(TWEEN_TIME * i);
            images[i].sprite = Storage.GetItemIcon(droppedItems[i]);
        }
    }
}
