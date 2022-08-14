using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    //TODO : ADD ACTION + REMOVE INSTANCE
    public static GameOverUI instance;
    [SerializeField] GameObject ggScreen;
    [SerializeField] Image ggScreenBg;
    [SerializeField] Sprite winBg, loseBg;

    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] TextMeshProUGUI hitsDoneText, hitsRecievedText;
    [SerializeField] TextMeshProUGUI mTrophyText, xpText, mTokenText;

    [SerializeField] Button playAgainButton, exitButton;

    [SerializeField] GameOverItems gameOverItems;

    private void Awake() => instance = this;

    public int hitsDone;
    public int hitsRecieved;

    private void Start() 
    {
        playAgainButton.onClick.AddListener(() => { PlayAgain(); AudioManager.instance.PlaySoundOnButtonClicked(); });
        exitButton.onClick.AddListener(() => { Exit(); AudioManager.instance.PlaySoundOnButtonClicked(); });   

        hitsDone = 0;
        hitsRecieved = 0; 
    }

    public void ShowGGScreen(bool win, int mTrophy, int xp, int mToken)
    {
        if(GameController.instance.gameOver) return;

        GameController.instance.gameOver = true;
        ggScreen.gameObject.SetActive(true);

        ggScreenBg.sprite = win ? winBg : loseBg;
        resultText.text = win ? "VICTORY" : "DEFEAT";

        hitsDoneText.text = hitsDone.ToString();
        hitsRecievedText.text = hitsRecieved.ToString();

        mTrophyText.text = "+" + mTrophy.ToString();
        xpText.text = "+" + xp.ToString();
        mTokenText.text = "+" + mToken.ToString();

        PlayerDataSave save = SaveHelper.Deserialize<PlayerDataSave>(PlayerPrefs.GetString("player"));

        if(win)
        {
            List<ItemType> itemsToAdd = gameOverItems.DropItems();
            for(int i = 0; i < itemsToAdd.Count; i++)
            {
                if(save.droppedItems.Contains(itemsToAdd[i]))
                {
                    save.droppedItemsAmount[save.droppedItems.IndexOf(itemsToAdd[i])] ++;
                }
                else
                {
                    save.droppedItems.Add(itemsToAdd[i]);
                    save.droppedItemsAmount.Add(1);
                }
            }
        }

        save.trophies += mTrophy;
        save.xpFromFight = xp;
        save.tokens += mToken;
        save.mushroomCurrentXps[(int)save.selectedMushroom] += xp;

        save.agilityPotionUsed = false;
        save.endurancePotionUsed = false;
        save.intelligencePotionUsed = false;
        save.strengthPotionUsed = false;
            
        PlayerPrefs.SetString("player", SaveHelper.Serialize(save));

        MusicThemes theme = win ? MusicThemes.winTheme : MusicThemes.loseTheme; 
        AudioManager.instance.ChangeMainTheme(theme);
    }

    public void PlayAgain() => Loader.Load(Loader.Scene.gameScene, false);
    public void Exit() => Loader.Load(Loader.Scene.mainMenu, true);
}
