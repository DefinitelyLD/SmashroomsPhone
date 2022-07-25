using System.Collections.Generic;
using UnityEngine;

public class BoostsUI : MonoBehaviour
{
    [SerializeField] GameObject agility, strength, intelligence, endurance;
    [SerializeField] List<RectTransform> rectTransforms;
    
    private const float CARD_WIDTH = 50; 

    public void ShowBoosts(bool showAgility, bool showStrength, bool showIntelligence, bool showEndurance)
    {
        agility.SetActive(showAgility);
        strength.SetActive(showStrength);
        intelligence.SetActive(showIntelligence);
        endurance.SetActive(showEndurance);

        UpdateUI();
    }
    
    private void UpdateUI()
    {
        int activeCounter = 0;
        foreach(RectTransform rectTransform in rectTransforms)
        {
            if(rectTransform.gameObject.activeSelf)
            {
                rectTransform.anchoredPosition = new Vector2(activeCounter * CARD_WIDTH, 0);
                activeCounter++;
            }
        }
    }
}
