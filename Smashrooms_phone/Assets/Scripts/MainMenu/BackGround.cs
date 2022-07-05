using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float partOneSpeed = 5f;
    public float partTwoSpeed = 10f;
    
    [SerializeField] GameObject movingPartOne;
    [SerializeField] RectTransform partOneTransform;

    [SerializeField] GameObject movingPartTwo;
    [SerializeField] RectTransform partTwoTransform;
    
    private int partOneDirection = 1;
    private int partTwoDirection = -1;

    public float partSize = 2048;

    public bool useOnlyOne = false;

    private void Update() 
    {
        movingPartOne.transform.position = Vector3.MoveTowards( movingPartOne.transform.position,  movingPartOne.transform.position + partOneDirection * new Vector3(partSize, 0, 0), partOneSpeed * Time.deltaTime);
        if(Mathf.Abs(partOneTransform.anchoredPosition.x) >= partSize) partOneDirection *= -1;
        
        if(useOnlyOne == false)
        {
            movingPartTwo.transform.position = Vector3.MoveTowards( movingPartTwo.transform.position,  movingPartTwo.transform.position + partTwoDirection * new Vector3(partSize, 0, 0), partTwoSpeed * Time.deltaTime);
            if(Mathf.Abs(partTwoTransform.anchoredPosition.x) >= partSize) partTwoDirection *= -1;
        }
    }
}
