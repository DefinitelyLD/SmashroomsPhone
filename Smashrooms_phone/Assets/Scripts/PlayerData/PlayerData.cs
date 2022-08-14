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
            if(currentXP >= Utility.levelXp[currentLvl - 1] && currentLvl < 100)
            {
                currentXP = currentXP - Utility.levelXp[currentLvl - 1];
                CurrentLvl += 1;
            }
            xpSlider.value = currentLvl == 100 ? xpSlider.maxValue : currentXP;
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

    public void LoadPlayerData(PlayerDataSave save)
    {
        int id = (int)save.selectedMushroom;
        SelectMushroom(id);

        intelligencePotionUsed = false;
        agilityPotionUsed = false;
        strengthPotionUsed = false;
        endurancePotionUsed = false;

        CurrentLvl = save.currentLvl;
        CurrentXP = save.currentXP;
        CurrentXP += save.xpFromFight;
        Trophies = save.trophies;
        MushroomTokens = save.tokens;

        for(int i = 0; i < availableMushrooms.Count; i++)
            availableMushrooms[i].level = save.mushroomLvls[i];

        for(int i = 0; i < availableMushrooms.Count; i++)
            availableMushrooms[i].Rank = save.mushroomRanks[i];

        for(int i = 0; i < availableMushrooms.Count; i++)
            availableMushrooms[i].CurrentXP = save.mushroomCurrentXps[i];

        OnMushroomXpChaged?.Invoke(id);

        for(int i = 0; i < save.droppedItems.Count; i++)
            InventoryManager.instance.AddItem(save.droppedItems[i], save.droppedItemsAmount[i]);
        
        save.droppedItems.Clear();
        save.droppedItemsAmount.Clear();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) CurrentXP += 100;
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            availableMushrooms[0].CurrentXP += 100;
            OnMushroomXpChaged?.Invoke(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) 
        {
            availableMushrooms[1].CurrentXP += 100;
            OnMushroomXpChaged?.Invoke(1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)) MushroomTokens += 100;
    }

    public void SelectMushroom(int id)
    {
        selectedMushroomId = id;
        OnMushroomSelected.Invoke(selectedMushroomId);
    }

    public bool CanRankUpMushroom() => availableMushrooms[selectedMushroomId].CanRankUp();
    public void RankUPMushroom() => availableMushrooms[selectedMushroomId].RankUP();
    public int GetMushroomRank() => availableMushrooms[selectedMushroomId].Rank;

    public PlayerDataSave GetPlayerDataSave()
    {
        PlayerDataSave save = new PlayerDataSave();

        save.selectedMushroom = (MushroomType) selectedMushroomId;

        save.intelligencePotionUsed = intelligencePotionUsed;
        save.agilityPotionUsed = agilityPotionUsed;
        save.strengthPotionUsed = strengthPotionUsed;
        save.endurancePotionUsed = endurancePotionUsed;

        save.currentXP = CurrentXP;
        save.currentLvl = currentLvl;
        save.trophies = Trophies;
        save.tokens = MushroomTokens;

        foreach(Mushroom mushroom in availableMushrooms)
        {
            save.mushroomRanks.Add(mushroom.Rank);
            save.mushroomLvls.Add(mushroom.level);
            save.mushroomCurrentXps.Add(mushroom.CurrentXP);

            save.mushroomStrength.Add(mushroom.strength);
            save.mushroomAgility.Add(mushroom.agility);
            save.mushroomIntelligence.Add(mushroom.intelligence);
            save.mushroomEndurance.Add(mushroom.endurance);
        }

        return save;
    }
}
