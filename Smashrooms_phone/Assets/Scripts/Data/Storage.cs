using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage instance;
    private static readonly Dictionary<MushroomType, MushroomData> mushrooms = new Dictionary<MushroomType, MushroomData>();

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
    }
    private void Start() => InitializeStorage();

    private void InitializeStorage()
    {
        DontDestroyOnLoad(gameObject);

        MushroomData[] loadedMushrooms = Resources.LoadAll<MushroomData>("Mushrooms");

        int length;
        length = loadedMushrooms.Length;

        for(int i = 0; i < length; i++) 
        {   
            if(mushrooms.ContainsKey(loadedMushrooms[i].type) == false)
            mushrooms.Add(loadedMushrooms[i].type, loadedMushrooms[i]);
        }
    }

    #region Getters
    public const int BASIC_DAMAGE = 50;
    public const int BASIC_HEALTH = 1000;
    public const int HEALTH_PER_STAT = 50;
    public const int DAMAGE_PER_STAT = 5;


    public static Sprite GetMushroomSprite(MushroomType type) => mushrooms[type].mushroomSprite;
    public static Sprite GetIslandSprite(MushroomType type) => mushrooms[type].islandSprite;
    public static int GetMushroomStrength(MushroomType type) => mushrooms[type].strength;
    public static int GetMushroomAgility(MushroomType type) => mushrooms[type].agility;
    public static int GetMushroomIntelligence(MushroomType type) => mushrooms[type].intelligence;
    public static int GetMushroomEndurance(MushroomType type) => mushrooms[type].endurance;

    public AudioClip GetAudioTheme(MusicThemes theme)
    {
        if(theme == MusicThemes.mainMenuTheme) return mainMenuTheme;
        else if(theme == MusicThemes.fightTheme) return fightTheme;
        else if(theme == MusicThemes.loseTheme) return loseTheme;
        else return winTheme;
    }
    #endregion
}
