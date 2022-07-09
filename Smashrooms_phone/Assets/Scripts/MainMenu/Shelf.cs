using UnityEngine.UI;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public static Shelf instance;
    [SerializeField] Button fighter1, fighter2;

    [SerializeField] Image mushroomImage, islandImage;

    public MushroomType mushroomType;

    private void Awake() => instance = this;
    private void Start()
    {
        fighter1.onClick.AddListener(() => {SelectFighter(MushroomType.basicMushroom); ChangeCheckMark(1); });
        fighter2.onClick.AddListener(() => {SelectFighter(MushroomType.fireMushroom); ChangeCheckMark(2); }); 
    }

    public void SelectFighter(MushroomType type)
    {
        mushroomImage.sprite = Storage.GetMushroomSprite(type);
        islandImage.sprite = Storage.GetIslandSprite(type);

        mushroomType = type;

        mushroomImage.GetComponent<Animator>().enabled = type == MushroomType.basicMushroom? true : false;

        PlayerData.instance.SelectMushroom((int) mushroomType);
    }

    public void ChangeCheckMark(int buttonID)
    {
        fighter1.transform.GetChild(0).gameObject.SetActive(false);
        fighter2.transform.GetChild(0).gameObject.SetActive(false);

        if(buttonID == 1) fighter1.transform.GetChild(0).gameObject.SetActive(true);
        else fighter2.transform.GetChild(0).gameObject.SetActive(true);   
    }
}
