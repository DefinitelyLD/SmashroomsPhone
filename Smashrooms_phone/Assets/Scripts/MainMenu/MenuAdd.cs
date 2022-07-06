using UnityEngine.UI;
using UnityEngine;

public class MenuAdd : MonoBehaviour
{
    [SerializeField] Sprite soundOnIcon, soundOffIcon;
    [SerializeField] Button soundButton;

    private void Awake() => SaveManager.OnGameLoaded += ChangeSoundButtonIcon;

    public void CloseGame() => Application.Quit();

    public void MuteSound() => AudioManager.instance.ChangeMuteState();

    public void ChangeSoundButtonIcon(SaveData saveData) => soundButton.GetComponent<Image>().sprite = AudioListener.volume == 1 ? soundOnIcon : soundOffIcon;
    private void OnDestroy() => SaveManager.OnGameLoaded -= ChangeSoundButtonIcon;
}
