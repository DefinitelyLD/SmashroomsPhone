using UnityEngine;

public class FightActions : MonoBehaviour
{
    //TODO: SPLIT INTO CLASSES
    [SerializeField] Transform attackPoint;
    [SerializeField] Transform opponent;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Stats stats;

    private Animator anim;
    private float animSpeed;
    
    [Header("Attacking")]
    public float attackRange = 1f;
    public LayerMask characterLayer;
    public float attackInterval = 0.5f;
    private float nextAttackTime = 0f;
    private int attackIndex = 0;
    public float maxComboDelay = 1f;
    public float delayAfterCombo = 1f; 
    private float stunGetOutTime = 0f;
    public float stunTime = 0.75f; 

    [HideInInspector] public bool isBlocking;

    [Header("Dashing")]
    private Rigidbody2D rb;
    public const float MIN_DASH_DISTANCE = 20f;
    public const float DASH_BACKWARD_FORCE = 10000f;
    public float dashSpeed = 100f;
    public float dashBackwardDistance = 30f;
    public float dashStopDistance = 15f;
    public float dashInterval = 0.5f;
    private float nextDashTime = 0f;
    private bool isDashing = false;
    private bool dashingOnEnemy; 
    private Vector2 dashBackwardPostion;

    [Header("KnockBack")]
    public float knockBackForce = 100;  

    private bool isEnemy;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        animSpeed = anim.speed;
    }
    
    public void InitializaStats(bool _isEnemy) 
    {
        stats.InitializaStats(_isEnemy);
        isEnemy = _isEnemy;
    } 

    private void Update() 
    {
        if(isDashing && dashingOnEnemy) 
        {
            transform.position = Vector3.MoveTowards(transform.position, opponent.position, dashSpeed * Time.deltaTime);
            if(Mathf.Abs(transform.position.x - opponent.position.x) < dashStopDistance) DashAttack();
        }
        if(isDashing && dashingOnEnemy == false) 
        {
            rb.AddForce(dashBackwardPostion);
            dashingOnEnemy = false;
            isDashing = false;
        }
    }
    public void Attack(bool dashAttack = false)
    {
        if(Time.time < stunGetOutTime) return;

        if((Time.time >= nextAttackTime && isDashing == false && isBlocking == false) || dashAttack)
        {
            if(Time.time >= nextAttackTime + maxComboDelay) attackIndex = 0;
            
            if(dashAttack == false)
            {
                attackIndex++;
                anim.Play("hit" + attackIndex.ToString()); 
            }
            
            Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, characterLayer);
            
            foreach(Collider2D col in hitEnemies)
            if(col.gameObject != this.gameObject)
            {
                if(col.TryGetComponent(out Stats targetStats)) targetStats.TakeDamage(stats.damage, attackIndex);
                if(col.TryGetComponent(out FightActions fightActions)) 
                {
                    fightActions.KnockBack(targetStats.isEnemy);
                    if(targetStats.damageBlock == 0)
                    fightActions.stunGetOutTime = Time.time + stunTime;
                    spriteRenderer.sortingOrder = isEnemy ? 20 : 21;
                }
            }
            KnockBack(!isEnemy);
            nextAttackTime = Time.time + attackInterval;

            if(attackIndex == 3) 
            {
                attackIndex = 0;
                nextAttackTime += delayAfterCombo;
            }
        }
    }
    public void Dash(float direction)
    {
        if(isDashing) return;
        if(Time.time < stunGetOutTime) return;
        if(direction == 0) return;

        if(Time.time >= nextDashTime)
        {
            if(isEnemy)
            {
                if(direction > 0) anim.Play("dashBackward");
                else
                {
                    if(Vector3.Distance(transform.position, opponent.transform.position) < MIN_DASH_DISTANCE) return;
                    anim.Play("dashForward");
                    dashingOnEnemy = true;
                } 
            }
            else
            {
                if(direction > 0)
                {
                    if(Vector3.Distance(transform.position, opponent.transform.position) < MIN_DASH_DISTANCE) return;
                    anim.Play("dashForward");
                    dashingOnEnemy = true;
                } 
                else anim.Play("dashBackward");
            }

            dashBackwardPostion = new Vector2(DASH_BACKWARD_FORCE, 0) * direction;
            isDashing = true;

            nextDashTime = Time.time + dashInterval;
        }

    }
    private void DashAttack()
    {
        dashingOnEnemy = false;
        isDashing = false;
        HoldDashAnimation();
        Attack(true);
    }
    public void Block(bool _isBlocking)
    {
        if(Time.time < stunGetOutTime) return;
        
        isBlocking = _isBlocking;
        if(isBlocking) anim.Play("block");
        if(isBlocking == false) HoldBlockAnimation();

        stats.damageBlock = isBlocking ? 0.8f : 0f;
    }

    public void HoldDashAnimation()
    {
        if(!isDashing) anim.speed = animSpeed;
        else anim.speed = 0;
    }
    public void HoldBlockAnimation()
    {
        if(!isBlocking) anim.speed = animSpeed;
        else anim.speed = 0;
    }

    private void KnockBack(bool isEnemy)
    {
        if(isBlocking) return;
        
        float direction = isEnemy ? 1 : -1;
        rb.AddForce(new Vector2(knockBackForce * direction, 0));
    }
}
