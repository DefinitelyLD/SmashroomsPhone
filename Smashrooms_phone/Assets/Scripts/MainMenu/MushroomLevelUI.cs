using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MushroomLevelUI : MonoBehaviour
{
    [SerializeField] List<Slider> mushroomSliders = new List<Slider>();
    [SerializeField] List<TextMeshProUGUI> mushroomLevelsTexts = new List<TextMeshProUGUI>();

    private void Awake() => PlayerData.OnMushroomXpChaged += UpdateUI;

    private void UpdateUI(int id)
    {
        Mushroom availableMushrooms = PlayerData.instance.availableMushrooms[id];
        mushroomLevelsTexts[id].text = "lv." + availableMushrooms.level.ToString();
        mushroomSliders[id].maxValue = PlayerData.instance.levelXp[availableMushrooms.level - 1];
        mushroomSliders[id].value = availableMushrooms.level == Mushroom.MAX_LEVEL ? mushroomSliders[id].maxValue : availableMushrooms.currentXP;
    }

    private void OnDestroy() => PlayerData.OnMushroomXpChaged -= UpdateUI;
}
