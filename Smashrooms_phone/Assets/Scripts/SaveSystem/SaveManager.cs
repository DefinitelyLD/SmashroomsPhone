using UnityEngine;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private PlayerDataSave playerDataSave;
    private SettingSave settingSave;
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
        playerDataSave = PlayerData.instance.GetPlayerDataSave();
        PlayerPrefs.SetString("player", SaveHelper.Serialize(playerDataSave));

        settingSave = new SettingSave();
        settingSave.soundOn = AudioManager.instance.soundOn;
        PlayerPrefs.SetString("settings", SaveHelper.Serialize(settingSave));

        inventorySave = InventoryManager.instance.GetInventorySave();
        PlayerPrefs.SetString("inventorySave", SaveHelper.Serialize(inventorySave));
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey("player"))
        {
            playerDataSave = SaveHelper.Deserialize<PlayerDataSave>(PlayerPrefs.GetString("player"));
            PlayerData.instance.LoadPlayerData(playerDataSave);
        }

        if(PlayerPrefs.HasKey("settings"))
        {
            settingSave = SaveHelper.Deserialize<SettingSave>(PlayerPrefs.GetString("settings"));
            AudioManager.instance.soundOn = settingSave.soundOn;
            AudioManager.instance.MuteSound(settingSave.soundOn);
        }

        if(PlayerPrefs.HasKey("inventorySave"))
        {
            inventorySave = SaveHelper.Deserialize<InventorySave>(PlayerPrefs.GetString("inventorySave"));
            InventoryManager.instance.LoadInventory(inventorySave);
        }
    }
}
