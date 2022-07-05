using System.Collections;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    private const float DECISION_INTERVAL = 0.25f;
    private const float MIN_BLOCK_TIME = 1f;
    private const float MAX_BLOCK_TIME = 3f;

    [SerializeField] Transform player;
    [SerializeField] Stats stats;
    private FightActions fightActions;

    private void Start() {
        fightActions = GetComponent<FightActions>();
        fightActions.InitializaStats(true);

        StopAllCoroutines();
        StartCoroutine(MakeDecision());
    }
    private IEnumerator MakeDecision()
    {
        while(true)
        {
            yield return new WaitForSeconds(DECISION_INTERVAL);
            if(fightActions.isBlocking) continue;
            
            if(GameController.instance.gameOver) continue;

            float hpPercent = stats.CurrentHealth / (float) stats.maxHealth;
            float roll = Random.Range(0f, 1f);
            if(Vector3.Distance(player.position, transform.position) >= FightActions.MIN_DASH_DISTANCE)
            {
                float chanceToDash = 0.5f - ( hpPercent * 0.3f);
                if(roll <= chanceToDash) fightActions.Dash(-1);

                yield return new WaitForSeconds(0.25f);
            } 

            else if(Vector3.Distance(player.position, transform.position) < FightActions.MIN_DASH_DISTANCE && hpPercent > 0.7f)
            {
                if(roll > 0.25f) fightActions.Attack();
                else
                {
                    float blockTime = Random.Range(MIN_BLOCK_TIME, MAX_BLOCK_TIME); 
                    StartCoroutine(HoldBlock(blockTime));
                }
            }

            else if(Vector3.Distance(player.position, transform.position) < FightActions.MIN_DASH_DISTANCE && hpPercent <= 0.7f)
            {
                float chanceToBlock = 0.2f;
                float chanceToDash = 0.1f + (0.7f - hpPercent)/5f;
                float chanceToBlockOrDash = chanceToBlock + chanceToDash;

                if(roll > chanceToBlockOrDash) fightActions.Attack();
                else if(roll > chanceToDash && roll <= chanceToBlockOrDash)
                {
                    float blockTime = Random.Range(MIN_BLOCK_TIME, MAX_BLOCK_TIME);
                    StartCoroutine(HoldBlock(blockTime));
                }
                else if(roll <= chanceToDash) fightActions.Dash(1);
            }
        }
    }
    private IEnumerator HoldBlock(float blockTime)
    {
        fightActions.Block(true);
        yield return new WaitForSeconds(blockTime);
        fightActions.Block(false);
    }
}
