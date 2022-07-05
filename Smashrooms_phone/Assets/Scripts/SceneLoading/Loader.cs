using UnityEngine.SceneManagement;
using System;

public static class Loader 
{
    private static Action onLoaderCallback;
    public static void Load(Scene scene, bool useLoadingScreen)
    {
        if(useLoadingScreen)
        {
            onLoaderCallback = () => {SceneManager.LoadScene(scene.ToString());};
            SceneManager.LoadScene(Scene.loadingScene.ToString());
        }
        else SceneManager.LoadScene(scene.ToString());
    }
    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    } 

    public enum Scene
    {
        mainMenu,
        loadingScene,
        gameScene,
    }
}
