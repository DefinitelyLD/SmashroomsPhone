using UnityEngine;

public class MainMenuLoadingSceen : MonoBehaviour
{
    [SerializeField] RectTransform circle, triangle, hexagon;

    private void Start() 
    {
        AudioManager.instance.StopMusic();
        LeanTween.alpha(circle, 0, 0f);
        LeanTween.alpha(triangle, 0, 0f);
        LeanTween.alpha(hexagon, 0, 0f);
        AppearGameObjects();
    } 

    private void AppearGameObjects()
    {
        LeanTween.alpha(circle, 1, 0.6f).setDelay(0.1f);
        LeanTween.alpha(triangle, 1, 0.6f).setDelay(0.7f);
        LeanTween.alpha(hexagon, 1, 0.6f).setDelay(1.3f);
    }
}
