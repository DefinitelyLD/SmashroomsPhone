using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage instance;
    private static readonly Dictionary<MushroomType, MushroomData> mushrooms = new Dictionary<MushroomType, MushroomData>();
    private static readonly Dictionary<ItemType, ItemData> items = new Dictionary<ItemType, ItemData>();
    private static readonly Dictionary<int, RankUpPressets> rankUpPresets = new Dictionary<int, RankUpPressets>();

    [SerializeField] AudioClip mainMenuTheme;
    [SerializeField] AudioClip fightTheme;

    [SerializeField] AudioClip winTheme;
    [SerializeField] AudioClip loseTheme;

    private void Awake() 
    {
        if(instance) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        InitializeStorage();
    }

    private void InitializeStorage()
    {
        DontDestroyOnLoad(gameObject);

        MushroomData[] loadedMushrooms = Resources.LoadAll<MushroomData>("Mushrooms");
        ItemData[] loadedItems = Resources.LoadAll<ItemData>("Items");
        RankUpPressets[] loadedPresets = Resources.LoadAll<RankUpPressets>("RankUpPresets");

        int length;
        length = loadedMushrooms.Length;

        for(int i = 0; i < length; i++) 
        {   
            if(mushrooms.ContainsKey(loadedMushrooms[i].type) == false)
            mushrooms.Add(loadedMushrooms[i].type, loadedMushrooms[i]);
        }

        length = loadedItems.Length;
        for(int i = 0; i < length; i++) 
        {   
            if(items.ContainsKey(loadedItems[i].type) == false)
            items.Add(loadedItems[i].type, loadedItems[i]);
        }

        length = loadedPresets.Length;
        for(int i = 0; i < length; i++) 
        {   
            if(rankUpPresets.ContainsKey(loadedPresets[i].rankId) == false)
            rankUpPresets.Add(loadedPresets[i].rankId, loadedPresets[i]);
        }
    }

    #region Getters
    public const int BASIC_DAMAGE = 50;
    public const int BASIC_HEALTH = 1000;
    public const int HEALTH_PER_STAT = 50;
    public const int DAMAGE_PER_STAT = 5;


    public static Sprite GetMushroomSprite(MushroomType type) => mushrooms[type].mushroomSprite;
    public static Sprite GetIslandSprite(MushroomType type) => mushrooms[type].islandSprite;
    public static float GetMushroomStrength(MushroomType type) => mushrooms[type].strength;
    public static float GetMushroomAgility(MushroomType type) => mushrooms[type].agility;
    public static float GetMushroomIntelligence(MushroomType type) => mushrooms[type].intelligence;
    public static float GetMushroomEndurance(MushroomType type) => mushrooms[type].endurance;

    public static Sprite GetItemIcon(ItemType type) => items[type].itemIcon;
    public static Sprite GetItemShopIcon(ItemType type) => items[type].shopIcon;
    public static string GetItemName(ItemType type) => items[type].itemName;
    public static int GetItemPrice(ItemType type) => items[type].price;

    public static List<ItemWithAmount> GetRankUpItems(int id) => rankUpPresets[id].itemsToRankUp;

    public AudioClip GetAudioTheme(MusicThemes theme)
    {
        if(theme == MusicThemes.mainMenuTheme) return mainMenuTheme;
        else if(theme == MusicThemes.fightTheme) return fightTheme;
        else if(theme == MusicThemes.loseTheme) return loseTheme;
        else return winTheme;
    }
    #endregion
}
