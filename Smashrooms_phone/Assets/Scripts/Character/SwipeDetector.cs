using UnityEngine;
using System;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPostion;

    [SerializeField] private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField] float minDistanceForSwipe = 40f;

    public static event Action<float> OnSwipe = delegate {};

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.touches[0];

            if(touch.phase == TouchPhase.Began)
            {
                fingerUpPostion = touch.position;
                fingerDownPosition = touch.position;    
            }

            if(!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if(touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        } 
    }
    private void DetectSwipe()
    {
        if(SwipeDistanceCheckMet())
        {
            float direction = fingerDownPosition.x - fingerUpPostion.x > 0 ? 1 : -1;
            OnSwipe(direction);

            fingerUpPostion = fingerDownPosition;
        }
    }
    private bool SwipeDistanceCheckMet() => HorizontalMovementDistance() > minDistanceForSwipe;
    private float HorizontalMovementDistance() => Mathf.Abs(fingerDownPosition.x - fingerUpPostion.x);
}
