using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public static event Action<int> OnMushroomSelected;
    public static event Action<int> OnMushroomXpChaged;

    [SerializeField] TextMeshProUGUI trophiesText, mushroomTokensText;

    [SerializeField] TextMeshProUGUI lvlText;
    [SerializeField] Slider xpSlider;

    [HideInInspector] public List<Mushroom> availableMushrooms = new List<Mushroom>();
    private int selectedMushroomId = 0;

    public bool intelligencePotionUsed, agilityPotionUsed, strengthPotionUsed, endurancePotionUsed;

    private int trophies;
    public int Trophies
    {
        get => trophies; 
        set 
        {
            trophies = value;
            trophiesText.text = trophies <= 999 ? trophies.ToString() : "+999"; 
        }
    }

    private int mushroomTokens;
    public int MushroomTokens
    {
        get => mushroomTokens; 
        set 
        {
            mushroomTokens = value;
            mushroomTokensText.text = mushroomTokens <= 9999 ? mushroomTokens.ToString() : "+9999"; 
        }
    }

    private int currentLvl = 1;
    private int currentXP;

    public int CurrentXP
    {
        get => currentXP;
        set 
        {
            currentXP = value;
            if(currentXP >= Utility.levelXp[currentLvl - 1] && currentLvl < 10)
            {
                currentXP = currentXP - Utility.levelXp[currentLvl - 1];
                CurrentLvl += 1;
            }
            xpSlider.value = currentLvl == 10 ? xpSlider.maxValue : currentXP;
        }
    }

    public int CurrentLvl
    {
        get => currentLvl;
        set
        {
            currentLvl = value;
            lvlText.text = "lv." + currentLvl.ToString();
            xpSlider.maxValue = Utility.levelXp[currentLvl - 1];
        }
    }

    private void Awake()
    {
        instance = this;
        availableMushrooms.Add(new Mushroom(MushroomType.basicMushroom));
        availableMushrooms.Add(new Mushroom(MushroomType.fireMushroom));
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) MushroomTokens += 100;
    }

    public void LoadPlayerData()
    {

    }

    public void SelectMushroom(int id)
    {
        selectedMushroomId = id;
        OnMushroomSelected.Invoke(selectedMushroomId);
    }

    public void AddMushroomXp(int selectedMushroom, int amount)
    {
        int id = selectedMushroom;
        availableMushrooms[id].AddXp(amount);
        OnMushroomXpChaged?.Invoke(selectedMushroom);
    }
    public bool CanRankUpMushroom() => availableMushrooms[selectedMushroomId].CanRankUp();
    public void RankUPMushroom() => availableMushrooms[selectedMushroomId].RankUP();
    public int GetMushroomRank() => availableMushrooms[selectedMushroomId].rank;
}
