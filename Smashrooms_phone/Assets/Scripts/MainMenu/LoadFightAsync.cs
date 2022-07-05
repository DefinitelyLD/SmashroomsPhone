using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadFightAsync : MonoBehaviour
{
    private static bool changeScene;
    private void Start()
    {
        changeScene = false;
        StartCoroutine(LoadAsyncScene());
    }
    public static void AllowSceneTransition() => changeScene = true;
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("gameScene");
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && changeScene == false)
        {
            yield return new WaitForEndOfFrame();
        }
        
        asyncLoad.allowSceneActivation = true;
    }
}
