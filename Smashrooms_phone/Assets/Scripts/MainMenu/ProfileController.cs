using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ProfileController : MonoBehaviour
{
    //TODO: REFACTOR ????
    public static ProfileController instance;
    [SerializeField] TextMeshProUGUI trophiesText, mushroomTokensText;

    [SerializeField] TextMeshProUGUI lvlText;
    [SerializeField] Slider xpSlider;

    [SerializeField] List<Slider> mushroomSliders = new List<Slider>();
    [SerializeField] List<TextMeshProUGUI> mushroomLevelsTexts = new List<TextMeshProUGUI>();
    [HideInInspector] public List<int> mushroomLevels = new List<int>(){1, 1};
    [HideInInspector] public List<int> mushroomXps = new List<int>(){0, 0};

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

    private List<int> levelXp = new List<int> {50, 100, 250, 500, 750, 1000, 1500, 2500, 5000, 10000};
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

    public void AddMushroomXp(MushroomType selectedMushroom, int amount)
    {
        int id = (int) selectedMushroom;
        mushroomXps[id] += amount;
        if(mushroomXps[id] >= levelXp[mushroomLevels[id] - 1] && mushroomLevels[id] < 10)
        {
            mushroomXps[id] = mushroomXps[id] - levelXp[mushroomLevels[id] - 1];
            mushroomLevels[id] += 1;
            mushroomLevelsTexts[id].text = "lv." + mushroomLevels[id].ToString();
            mushroomSliders[id].maxValue = levelXp[mushroomLevels[id] - 1];
        }
        mushroomSliders[id].value = mushroomLevels[id] == 10 ? mushroomSliders[id].maxValue : mushroomXps[id];
    }

    public void SetMushroomXPs(List<int> levels, List<int> xps)
    {
        for(int i = 0; i < levels.Count; i++)
        {
            mushroomLevels[i] = levels[i];
            mushroomSliders[i].maxValue = levelXp[levels[i] - 1];
            AddMushroomXp((MushroomType)i, xps[i]);
            mushroomLevelsTexts[i].text = "lv." + mushroomLevels[i];
        }
    }
}
