using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public static event Action<SaveData> OnGameLoaded;
    private SaveData saveData;

    private void Awake() 
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
    private void Start() => Load();
    private void OnApplicationQuit() => Save();

    public void Save()
    {   
        saveData = new SaveData();
        saveData.soundOn = AudioManager.instance.soundOn;
        /*saveData.selectedMushroom = Shelf.instance.mushroomType;
        saveData.mushroomTrophies = ProfileController.instance.Trophies;
        saveData.mushroomTokens = ProfileController.instance.MushroomTokens;
        saveData.currentLvl = ProfileController.instance.CurrentLvl;
        saveData.currentLvlXp = ProfileController.instance.CurrentXP;
        saveData.mushroomLvls.AddRange(ProfileController.instance.mushroomLevels);
        saveData.mushroomXps.AddRange(ProfileController.instance.mushroomXps);*/

        PlayerPrefs.SetString("save", SaveHelper.Serialize(saveData));
    }
    public void RewriteSave(SaveData save) => PlayerPrefs.SetString("save", SaveHelper.Serialize(saveData));

    public void Load()
    {
        if(PlayerPrefs.HasKey("save"))
        {
            saveData = SaveHelper.Deserialize<SaveData>(PlayerPrefs.GetString("save"));

            AudioManager.instance.soundOn = saveData.soundOn;
            AudioManager.instance.MuteSound(saveData.soundOn);

            Shelf.instance.SelectFighter(saveData.selectedMushroom);
            Shelf.instance.ChangeCheckMark((int)saveData.selectedMushroom + 1);

            /*ProfileController.instance.Trophies = saveData.mushroomTrophies;
            ProfileController.instance.MushroomTokens = saveData.mushroomTokens;
            ProfileController.instance.CurrentLvl = saveData.currentLvl;
            ProfileController.instance.CurrentXP = saveData.currentLvlXp;
            ProfileController.instance.SetMushroomXPs(saveData.mushroomLvls, saveData.mushroomXps);*/
        }
        else
        {
            Shelf.instance.SelectFighter(MushroomType.basicMushroom);
            AudioManager.instance.soundOn = true;
        }
        OnGameLoaded?.Invoke(saveData);
    }
}
