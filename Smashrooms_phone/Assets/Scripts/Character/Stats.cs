using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    //TODO: CLASS FOR STATS UI + ACTIONS
    public float maxHealth;
    private float currentHealth; 
    public int damage;

    [SerializeField] BoostsUI boostsUI;

    [SerializeField] Slider hpSlider;
    [SerializeField] TextMeshProUGUI hpText; 

    [SerializeField] Material whiteFlashMat;
    [SerializeField] SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Renderer rend;
    public bool isEnemy;

    private Animator anim;

    public float damageBlock = 0f;

    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            if(currentHealth <= 0) currentHealth = 0;

            hpSlider.value = currentHealth;
            hpText.text = currentHealth.ToString();
        }
    }

    public void InitializaStats(bool _isEnemy)
    {
        isEnemy = _isEnemy;

        PlayerDataSave save = SaveHelper.Deserialize<PlayerDataSave>(PlayerPrefs.GetString("player"));
        MushroomType selectedMushroom =  isEnemy? MushroomType.basicMushroom : save.selectedMushroom;

        float addingEndurance = save.endurancePotionUsed ? 2 : 0;
        float enduranceStatAmount = save.mushroomEndurance[(int)selectedMushroom] + addingEndurance;

        maxHealth = Storage.BASIC_HEALTH + Storage.HEALTH_PER_STAT * enduranceStatAmount * 
                    (1 + save.mushroomLvls[(int) selectedMushroom] / 10f);

        maxHealth = 20;

        hpSlider.maxValue = maxHealth;
        CurrentHealth = maxHealth;
        
        float addingStrength = save.strengthPotionUsed ? 2 : 0;
        float strengthStatAmount = save.mushroomEndurance[(int)selectedMushroom] + addingEndurance;

        damage = (int)((Storage.BASIC_DAMAGE + Storage.DAMAGE_PER_STAT * strengthStatAmount * 
                    (1 + save.mushroomLvls[(int) selectedMushroom] / 10f)));

        rend = GetComponent<Renderer>();
        originalMaterial = rend.material;

        anim = GetComponent<Animator>();

        if(!isEnemy)
            boostsUI.ShowBoosts(save.agilityPotionUsed, save.strengthPotionUsed, save.intelligencePotionUsed, save.endurancePotionUsed);
    }

    public void TakeDamage(int damage, int attackIndex)
    {
        if(damageBlock == 0)
        {
            anim.enabled = false;
            anim.enabled = true;
            if(attackIndex == 1 || attackIndex == 0) anim.Play("damage1");
            if(attackIndex == 2) anim.Play("damage3");
            if(attackIndex == 3) anim.Play("damage2");
        }

        int incomingDamage = (int)(damage * (1 - damageBlock));
        CurrentHealth -= incomingDamage;
        StartCoroutine(WhiteFlash());

        if(isEnemy) GameOverUI.instance.hitsDone += 1;
        else GameOverUI.instance.hitsRecieved += 1;

        if(currentHealth <= 0) GameOver();

        AudioManager.instance.PlayPunchSound(!isEnemy);
    }
    private IEnumerator WhiteFlash()
    {
        spriteRenderer.sortingOrder = 19;
        rend.material = whiteFlashMat;
        yield return new WaitForSeconds(0.125f);
        rend.material = originalMaterial;
    }
    private void GameOver()
    {
        anim.Play("death");
        int awardsAmount = isEnemy ? Random.Range(90, 130 + 1) : Random.Range(25, 30 + 1); //TODO: CONSTANTS ??? 
        GameOverUI.instance.ShowGGScreen(isEnemy, awardsAmount, awardsAmount, awardsAmount); 
    }
    private void StopAnimator() => anim.enabled = false;
}
