using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuAdd;
    [SerializeField] GameObject character;

    [SerializeField] GameObject menuButtons, sideButtons, playButtonParent, backButtonParent; 
    [SerializeField] GameObject mTokens, sTokens;
    [SerializeField] GameObject profileBar, fractionEmblem;
    [SerializeField] GameObject shelfPage, invetoryRankPage, shopPage;

    [SerializeField] Button shelfButton, inventoryButton, backButton, shopButton;

    private GameObject currentPage;

    private bool isMenuOpened = false; 
    public static bool isPageOpened = false;

    private void Awake() 
    {
        shelfButton.onClick.AddListener(() => ChangePageState(shelfPage));
        inventoryButton.onClick.AddListener(() => ChangePageState(invetoryRankPage));
        backButton.onClick.AddListener(() => ClosePage());
        shopButton.onClick.AddListener(() => ChangePageState(shopPage, true));
    }
    public void ChangeSideMenuActiveState() 
    {
        float posX = isMenuOpened? menuAdd.transform.localPosition.x - 345 : menuAdd.transform.localPosition.x + 345 ;
        LeanTween.moveLocalX(menuAdd, posX, 0.1f);

        isMenuOpened = !isMenuOpened;
    }

    public void ChangePageState(GameObject pageToOpen, bool hideCharacter = false)
    {
        currentPage = pageToOpen;
        float posY = isPageOpened ? -300 : 300;
        float posX = isPageOpened ? -450 : 450;

        float charPosX = isPageOpened ? -600 : 600;
        float pagePosY = isPageOpened ? -1550 : 1550;

        float profilePosY = isPageOpened ? -600 : 600;

        if(isMenuOpened) ChangeSideMenuActiveState();

        LeanTween.moveLocalY(menuButtons, menuButtons.transform.localPosition.y - posY, 0.1f);
        LeanTween.moveLocalY(backButtonParent, backButtonParent.transform.localPosition.y + posY, 0.1f);
        
        if(currentPage != shopPage)
        {
            LeanTween.moveLocalY(mTokens, mTokens.transform.localPosition.y + posY, 0.1f);
            LeanTween.moveLocalY(sTokens, sTokens.transform.localPosition.y + posY, 0.1f);
        }
        LeanTween.moveLocalY(profileBar, profileBar.transform.localPosition.y + posY, 0.1f);
        LeanTween.moveLocalY(fractionEmblem, fractionEmblem.transform.localPosition.y + pagePosY, 0.1f);

        LeanTween.moveLocalX(sideButtons, sideButtons.transform.localPosition.x + posX, 0.1f);
        LeanTween.moveLocalX(playButtonParent, playButtonParent.transform.localPosition.x + posX, 0.1f);
        
        if(hideCharacter)
        {
            character.SetActive(!character.activeSelf);
            ChangeShelfButtonState(!isPageOpened);
        }
        else
        {
            character.SetActive(true);
            if(currentPage != shopPage)
            {
                float delayChar = isPageOpened ? 0.1f : 0f;
                int id = LeanTween.moveLocalX(character, character.transform.localPosition.x - charPosX , 0.1f).setDelay(delayChar).id;
                LTDescr d = LeanTween.descr(id);

                if(isPageOpened) d.setOnComplete(() => {ChangeShelfButtonState(!isPageOpened); });
                else d.setOnStart(() => {ChangeShelfButtonState(!isPageOpened); });
            }
        }

        float delay = isPageOpened ? 0f : 0.1f;
        LeanTween.moveLocalY(pageToOpen, pageToOpen.transform.localPosition.y + pagePosY , 0.1f).setDelay(delay);

        isPageOpened = !isPageOpened;
    }
    public void ClosePage()
    {
        if(currentPage != null && isPageOpened) ChangePageState(currentPage);
    }
    public void ChangeShelfButtonState(bool isActive) => shelfButton.gameObject.SetActive(isActive);
}
