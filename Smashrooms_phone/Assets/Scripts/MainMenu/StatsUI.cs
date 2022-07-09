using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI strengthText, agilityText, intelligenceText, enduranceText;

    [SerializeField] Slider strengthSlider, agilitySlider, intelligenceSlider, enduranceSlider;

    private void Awake()
    {
        PlayerData.OnMushroomSelected += UpdateUI;
        Mushroom.OnStatsChaged += UpdateUI;
    }

    private void UpdateUI(int id)
    {
        float strength = PlayerData.instance.availableMushrooms[id].strength;
        strengthText.text = strength.ToString();
        float agility = PlayerData.instance.availableMushrooms[id].agility;
        agilityText.text = agility.ToString();
        float intelligence = PlayerData.instance.availableMushrooms[id].intelligence;
        intelligenceText.text = intelligence.ToString();
        float endurance = PlayerData.instance.availableMushrooms[id].endurance;
        enduranceText.text = endurance.ToString();

        strengthSlider.value = strength;
        agilitySlider.value = agility;
        intelligenceSlider.value = intelligence;
        enduranceSlider.value = endurance;
    }

    private void OnDestroy() 
    {
        PlayerData.OnMushroomSelected -= UpdateUI;
        Mushroom.OnStatsChaged -= UpdateUI;
    }
}
