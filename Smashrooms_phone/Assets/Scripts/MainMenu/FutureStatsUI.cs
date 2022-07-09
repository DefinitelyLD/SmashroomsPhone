using UnityEngine.UI;
using UnityEngine;

public class FutureStatsUI : MonoBehaviour
{
    [SerializeField] Slider strengthSlider, agilitySlider, intelligenceSlider, enduranceSlider;
    [SerializeField] Image strengthSliderFill, agilitySliderFill, intelligenceSliderFill, enduranceSliderFill;

    private void Awake()
    {
        PlayerData.OnMushroomSelected += UpdateUI;
        Mushroom.OnStatsChaged += UpdateUI;
    }

    public void UpdateUI(int id)
    {
        if(PlayerData.instance.GetMushroomRank() == Mushroom.MAX_RANK) return;

        float strength = PlayerData.instance.availableMushrooms[id].strength + Mushroom.STATS_ON_RANK_UP;
        float agility = PlayerData.instance.availableMushrooms[id].agility + Mushroom.STATS_ON_RANK_UP;
        float intelligence = PlayerData.instance.availableMushrooms[id].intelligence + Mushroom.STATS_ON_RANK_UP;
        float endurance = PlayerData.instance.availableMushrooms[id].endurance + Mushroom.STATS_ON_RANK_UP;

        strengthSlider.value = strength;
        agilitySlider.value = agility;
        intelligenceSlider.value = intelligence;
        enduranceSlider.value = endurance;
    }

    public void SetFutureStatsActive(bool isActive)
    {
        strengthSliderFill.enabled = isActive;
        agilitySliderFill.enabled = isActive;
        intelligenceSliderFill.enabled = isActive;
        enduranceSliderFill.enabled = isActive;
    }

    private void OnDestroy() 
    {
        PlayerData.OnMushroomSelected -= UpdateUI;
        Mushroom.OnStatsChaged -= UpdateUI;
    }
}
