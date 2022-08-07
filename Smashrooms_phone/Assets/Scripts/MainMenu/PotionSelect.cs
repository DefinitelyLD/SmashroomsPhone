using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class PotionSelect : MonoBehaviour
{
    [SerializeField] Button intelligence, agility, strength, endurance;
    [SerializeField] Button play, continueButton, exit;
    [SerializeField] GameObject parent;
    [SerializeField] Sprite selected, normal;
    [SerializeField] TextMeshProUGUI intelligenceAmount, agilityAmount, strenghtAmount, enduranceAmount;

    private void Awake() 
    {
        //continueButton.onClick.AddListener(() => {UsePotions(); LoadFightAsync.AllowSceneTransition(); SaveManager.instance.Save(); 
        //AudioManager.instance.PlaySoundOnButtonClicked();});
        play.onClick.AddListener(() => ToggleUI());
        exit.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); ToggleUI();});

        intelligence.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); SelectPotion(SelectedPotion.Intelligence);});
        agility.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); SelectPotion(SelectedPotion.Agility);});
        strength.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); SelectPotion(SelectedPotion.Strength);});
        endurance.onClick.AddListener(() => {AudioManager.instance.PlaySoundOnButtonClicked(); SelectPotion(SelectedPotion.Endurance);});
    }

    private void ToggleUI()
    {
        parent.SetActive(!parent.activeSelf);

        if(parent.activeSelf)
        {
            int intAmount = InventoryManager.instance.GetItemAmount(ItemType.intelligencePotion);
            intelligenceAmount.text = intAmount <= 9? intAmount.ToString() : "9+";

            int agAmount = InventoryManager.instance.GetItemAmount(ItemType.agilityPotion);
            agilityAmount.text = agAmount <= 9? agAmount.ToString() : "9+";

            int stAmount = InventoryManager.instance.GetItemAmount(ItemType.strengthPotion);
            strenghtAmount.text = stAmount <= 9? stAmount.ToString() : "9+";

            int endAmount = InventoryManager.instance.GetItemAmount(ItemType.endurancePotion);
            enduranceAmount.text = endAmount <= 9? endAmount.ToString() : "9+";
        }
    }

    private void SelectPotion(SelectedPotion potion)
    {
        if(potion == SelectedPotion.Intelligence)
        {
            if(InventoryManager.instance.GetItemAmount(ItemType.intelligencePotion) == 0) return;
            bool isSelected = intelligence.image.sprite == normal;
            Debug.Log(isSelected);
            intelligence.image.sprite = isSelected? selected : normal;
            PlayerData.instance.intelligencePotionUsed = isSelected;
        }
        else if(potion == SelectedPotion.Agility)
        {
            if(InventoryManager.instance.GetItemAmount(ItemType.agilityPotion) == 0) return;
            bool isSelected = agility.image.sprite == normal;
            agility.image.sprite = isSelected? selected : normal;
            PlayerData.instance.agilityPotionUsed = isSelected;
        }
        else if(potion == SelectedPotion.Strength)
        {
            if(InventoryManager.instance.GetItemAmount(ItemType.strengthPotion) == 0) return;
            bool isSelected = strength.image.sprite == normal;
            strength.image.sprite = isSelected? selected : normal;
            PlayerData.instance.strengthPotionUsed = isSelected;
        }
        else if(potion == SelectedPotion.Endurance)
        {
            if(InventoryManager.instance.GetItemAmount(ItemType.endurancePotion) == 0) return;
            bool isSelected = endurance.image.sprite == normal;
            endurance.image.sprite = isSelected? selected : normal;
            PlayerData.instance.endurancePotionUsed = isSelected;
        }
    }

    private void UsePotions()
    {
        if(PlayerData.instance.intelligencePotionUsed)
        InventoryManager.instance.RemoveItem(ItemType.intelligencePotion, 1);

        if(PlayerData.instance.strengthPotionUsed)
        InventoryManager.instance.RemoveItem(ItemType.strengthPotion, 1);

        if(PlayerData.instance.agilityPotionUsed)
        InventoryManager.instance.RemoveItem(ItemType.agilityPotion, 1);

        if(PlayerData.instance.endurancePotionUsed)
        InventoryManager.instance.RemoveItem(ItemType.endurancePotion, 1);
    }

    public enum SelectedPotion
    {
        Intelligence,
        Agility,
        Strength,
        Endurance
    }
}
