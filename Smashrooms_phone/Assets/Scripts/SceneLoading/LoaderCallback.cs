using System.Collections;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private const float LOADING_DELAY = 2f;

    private void Start() => StartCoroutine(DelayCallback());
    private IEnumerator DelayCallback()
    {
        yield return new WaitForSeconds(LOADING_DELAY);
        Loader.LoaderCallback();
    }
}
