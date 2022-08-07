using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private FightActions fightActions;

    private void Awake() => SwipeDetector.OnSwipe += UseDash;

    private void Start() 
    {
        fightActions = GetComponent<FightActions>();
        fightActions.InitializaStats(false);
    }
    
    private void Update()
    {
        if( GameController.instance.gameOver) return;

        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if(touch.position.x > Screen.width / 2) fightActions.Attack();
            if(touch.position.x < Screen.width / 2)
            {
                if(touch.phase == TouchPhase.Began) fightActions.Block(true);
                if(touch.phase == TouchPhase.Ended) fightActions.Block(false);
            }
        }
        /*if(Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1)) fightActions.Attack();
        if(Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0)) fightActions.Block(true);
        if(Input.GetMouseButtonUp(1)) fightActions.Block(false);
        if(Input.GetKeyDown(KeyCode.A)) fightActions.Dash(-1);
        if(Input.GetKeyDown(KeyCode.D)) fightActions.Dash(1);*/
    }

    private void UseDash(float direction) 
    { 
        if( GameController.instance.gameOver) return;
        fightActions.Dash(direction);
    } 

    private void OnDestroy() => SwipeDetector.OnSwipe -= UseDash;
}
