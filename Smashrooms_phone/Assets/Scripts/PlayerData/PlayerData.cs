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
            mushroomTokensText.text = mushroomTokens <= 999 ? mushroomTokens.ToString() : "+999"; 
        }
    }

    [HideInInspector] public List<int> levelXp = new List<int> {50, 100, 250, 500, 750, 1000, 1500, 2500, 5000, 10000}; //TODO: UTILITY 
    private int currentLvl = 1;
    private int currentXP;

    public int CurrentXP
    {
        get => currentXP;
        set 
        {
            currentXP = value;
            if(currentXP >= levelXp[currentLvl - 1] && currentLvl < 10)
            {
                currentXP = currentXP - levelXp[currentLvl - 1];
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
            xpSlider.maxValue = levelXp[currentLvl - 1];
        }
    }

    private void Awake() => instance = this;
    private void Start()
    {
        availableMushrooms.Add(new Mushroom(MushroomType.basicMushroom, levelXp));
        availableMushrooms.Add(new Mushroom(MushroomType.fireMushroom, levelXp));
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) CurrentXP += 50;
        if(Input.GetKeyDown(KeyCode.Alpha2)) AddMushroomXp(0, 50);
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
