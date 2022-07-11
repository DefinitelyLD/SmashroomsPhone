using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public static event Action<SaveData> OnGameLoaded;
    private SaveData saveData;
    private InventorySave inventorySave;

    private void Awake() 
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
    private void Start() => Load();

    private void OnApplicationFocus(bool focusStatus) 
    {
        if(focusStatus == false) Save();
    }

    public void Save()
    { 
        saveData = new SaveData();
        saveData.soundOn = AudioManager.instance.soundOn;
        PlayerPrefs.SetString("save", SaveHelper.Serialize(saveData));

        inventorySave = InventoryManager.instance.GetInventorySave();
        PlayerPrefs.SetString("inventorySave", SaveHelper.Serialize(inventorySave));
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
        }
        else
        {
            Shelf.instance.SelectFighter(MushroomType.basicMushroom);
            AudioManager.instance.soundOn = true;
        }

        if(PlayerPrefs.HasKey("inventorySave"))
        {
            inventorySave = SaveHelper.Deserialize<InventorySave>(PlayerPrefs.GetString("inventorySave"));
            InventoryManager.instance.LoadInventory(inventorySave);
        }

        OnGameLoaded?.Invoke(saveData);
    }
}
