using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] SpriteRenderer character;

    [SerializeField] List<Arena> arenas = new List<Arena>();

    [SerializeField] RectTransform loadingScreen;

    public bool gameOver = true;

    private void Awake() => instance = this;
    private void Start() 
    {
        SelectCharacter(SaveHelper.Deserialize<SaveData>(PlayerPrefs.GetString("save")).selectedMushroom);
        AudioManager.instance.StopMusic();

        int id = LeanTween.alpha(loadingScreen, 0f, 0.5f).setDelay(2f).id;
        LTDescr d = LeanTween.descr(id);
        d.setOnComplete(() => { SetGGState(false); AudioManager.instance.ChangeMainTheme(MusicThemes.fightTheme); }); 

        int randomArenaID = Random.Range(0, arenas.Count);

        foreach(Arena arena in arenas)
        {
            arena.bg.SetActive(false);
            arena.land.SetActive(false);      
        } 
        arenas[randomArenaID].bg.SetActive(true);
        arenas[randomArenaID].land.SetActive(true);
    }
    
    public void SelectCharacter(MushroomType type) => character.sprite = Storage.GetMushroomSprite(MushroomType.basicMushroom); //TODO: Return to type
 
    public void SetGGState(bool _gameOver) => gameOver = _gameOver;
}

[System.Serializable]
public struct Arena
{
    public GameObject bg;
    public GameObject land;
}
