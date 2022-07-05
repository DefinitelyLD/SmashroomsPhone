using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    [SerializeField] GameObject menuAdd;
    [SerializeField] GameObject character;

    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject sideButtons;
    [SerializeField] GameObject playButton;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject shelfButton; 
    [SerializeField] GameObject profileBar;
    [SerializeField] GameObject mTokens;
    [SerializeField] GameObject sTokens;

    [SerializeField] GameObject shelf;

    [SerializeField] Sprite soundOnIcon;
    [SerializeField] Sprite soundOffIcon;

    [SerializeField] Button soundButton;
    [SerializeField] Button play;

    [SerializeField] GameObject fractionEmblem;
 
    bool isMenuOpened = false; 
    public static bool isShelfOpened = false;

    private void Awake() => instance = this;
    private void Start() 
    {
        play.onClick.AddListener(() => {LoadFightAsync.AllowSceneTransition(); SaveManager.instance.Save(); });
        MoveCharacterUp();
        AudioManager.instance.ChangeMainTheme(MusicThemes.mainMenuTheme);
    }
    public void ChangeSideMenuActiveState() 
    {
        float posX = isMenuOpened? menuAdd.transform.localPosition.x - 345 : menuAdd.transform.localPosition.x + 345 ;
        LeanTween.moveLocalX(menuAdd, posX, 0.1f);

        isMenuOpened = !isMenuOpened;
    }

    public void ChangeShelfState()
    {
        float posY = isShelfOpened ? -300 : 300;
        float posX = isShelfOpened ? -450 : 450;

        float charPosX = isShelfOpened ? -600 : 600;
        float shelfPosY = isShelfOpened ? -1550 : 1550;

        float profilePosY = isShelfOpened ? -600 : 600;

        if(isMenuOpened) ChangeSideMenuActiveState();

        LeanTween.moveLocalY(menuButtons, menuButtons.transform.localPosition.y - posY, 0.1f);
        LeanTween.moveLocalY(backButton, backButton.transform.localPosition.y + posY, 0.1f);

        LeanTween.moveLocalY(mTokens, mTokens.transform.localPosition.y + posY, 0.1f);
        LeanTween.moveLocalY(sTokens, sTokens.transform.localPosition.y + posY, 0.1f);
        LeanTween.moveLocalY(profileBar, profileBar.transform.localPosition.y + posY, 0.1f);
        LeanTween.moveLocalY(fractionEmblem, fractionEmblem.transform.localPosition.y + shelfPosY, 0.1f);

        LeanTween.moveLocalX(sideButtons, sideButtons.transform.localPosition.x + posX, 0.1f);
        LeanTween.moveLocalX(playButton, playButton.transform.localPosition.x + posX, 0.1f);

        float delayChar = isShelfOpened ? 0.1f : 0f;
        int id = LeanTween.moveLocalX(character, character.transform.localPosition.x - charPosX , 0.1f).setDelay(delayChar).id;
        LTDescr d = LeanTween.descr(id);

        if(isShelfOpened) d.setOnComplete(() => {ChangeShelfButtonState(!isShelfOpened); });
        else d.setOnStart(() => {ChangeShelfButtonState(!isShelfOpened); });

        float delayShelf = isShelfOpened ? 0f : 0.1f;
        LeanTween.moveLocalY(shelf, shelf.transform.localPosition.y + shelfPosY , 0.1f).setDelay(delayShelf);

        isShelfOpened = !isShelfOpened;
    }
    public void ChangeShelfButtonState(bool isActive)
    {
        shelfButton.gameObject.SetActive(isActive);
    }

    public void CloseGame() => Application.Quit();

    public void MuteSound() => AudioManager.instance.ChangeMuteState();

    public void ChangeSoundButtonIcon() => soundButton.GetComponent<Image>().sprite = AudioListener.volume == 1 ? soundOnIcon : soundOffIcon;

    #region Character
    private void MoveCharacterUp()
    {
        int id = LeanTween.moveLocalY(character, character.transform.localPosition.y + 25, 10f).id;
        LTDescr d = LeanTween.descr(id);

        d.setOnComplete(MoveCharacterDown);
    }
    private void MoveCharacterDown()
    {
        int id = LeanTween.moveLocalY(character, character.transform.localPosition.y -25, 10f).id;
        LTDescr d = LeanTween.descr(id);

        d.setOnComplete(MoveCharacterUp);
    }
    #endregion
}
