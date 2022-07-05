using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Shelf : MonoBehaviour
{
    public static Shelf instance;
    [SerializeField] Button fighter1;
    [SerializeField] Button fighter2;

    [SerializeField] TextMeshProUGUI strengthText;
    [SerializeField] TextMeshProUGUI agilityText;
    [SerializeField] TextMeshProUGUI intelligenceText;
    [SerializeField] TextMeshProUGUI enduranceText;

    [SerializeField] Slider strengthSlider;
    [SerializeField] Slider agilitySlider;
    [SerializeField] Slider intelligenceSlider;
    [SerializeField] Slider enduranceSlider;

    [SerializeField] Image mushroomImage;
    [SerializeField] Image islandImage;

    public MushroomType mushroomType;

    private void Awake() => instance = this;
    private void Start()
    {
        fighter1.onClick.AddListener(() => {SelectFighter(MushroomType.basicMushroom); ChangeCheckMark(1); });
        fighter2.onClick.AddListener(() => {SelectFighter(MushroomType.fireMushroom); ChangeCheckMark(2); }); 
    }

    public void SelectFighter(MushroomType type)
    {
        int strength = Storage.GetMushroomStrength(type);
        strengthText.text = strength.ToString();
        int agility = Storage.GetMushroomAgility(type);
        agilityText.text = agility.ToString();
        int intelligence = Storage.GetMushroomIntelligence(type);
        intelligenceText.text = intelligence.ToString();
        int endurance = Storage.GetMushroomEndurance(type);
        enduranceText.text = endurance.ToString();

        strengthSlider.value = strength;
        agilitySlider.value = agility;
        intelligenceSlider.value = intelligence;
        enduranceSlider.value = endurance;

        mushroomImage.sprite = Storage.GetMushroomSprite(type);
        islandImage.sprite = Storage.GetIslandSprite(type);

        mushroomType = type;

        mushroomImage.GetComponent<Animator>().enabled = type == MushroomType.basicMushroom? true : false;
    }

    public void ChangeCheckMark(int buttonID)
    {
        fighter1.transform.GetChild(0).gameObject.SetActive(false);
        fighter2.transform.GetChild(0).gameObject.SetActive(false);

        if(buttonID == 1) fighter1.transform.GetChild(0).gameObject.SetActive(true);
        else fighter2.transform.GetChild(0).gameObject.SetActive(true);   
    }
}
