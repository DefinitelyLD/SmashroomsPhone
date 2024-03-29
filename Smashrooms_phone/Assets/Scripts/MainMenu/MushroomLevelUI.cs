using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MushroomLevelUI : MonoBehaviour
{
    [SerializeField] List<Slider> mushroomSliders = new List<Slider>();
    [SerializeField] List<TextMeshProUGUI> mushroomLevelsTexts = new List<TextMeshProUGUI>();
    [SerializeField] List<Image> rankImages = new List<Image>();

    [SerializeField] Color mainThemeColor;

    private void Awake()
    {
        PlayerData.OnMushroomXpChaged += UpdateUI;
        Mushroom.OnRankChanged += UpdateRankIcon;
    }

    private void UpdateUI(int id)
    {
        Mushroom availableMushrooms = PlayerData.instance.availableMushrooms[id];
        mushroomLevelsTexts[id].text = "lv." + availableMushrooms.level.ToString();
        mushroomSliders[id].maxValue = Utility.levelXp[availableMushrooms.level - 1];
        mushroomSliders[id].value = availableMushrooms.level == Mushroom.MAX_LEVEL ? mushroomSliders[id].maxValue : availableMushrooms.CurrentXP;
    }

    private void UpdateRankIcon(int mushroomID, int rank)
    {
        rankImages[mushroomID].sprite = Storage.GetRankImage(rank);
        mushroomLevelsTexts[mushroomID].color = rank == 10 ? mainThemeColor : Color.white;
    }

    private void OnDestroy() 
    {
        PlayerData.OnMushroomXpChaged -= UpdateUI;
        Mushroom.OnRankChanged -= UpdateRankIcon;
    } 
}
