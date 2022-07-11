using UnityEngine.UI;
using UnityEngine;

public class MenuAdd : MonoBehaviour
{
    [SerializeField] Sprite soundOnIcon, soundOffIcon;

    [SerializeField] Button soundButton;

    private void Start() => ChangeSoundButtonIcon();

    public void CloseGame() => Application.Quit();

    public void MuteSound() => AudioManager.instance.ChangeMuteState();

    public void ChangeSoundButtonIcon() => soundButton.GetComponent<Image>().sprite = AudioListener.volume == 1 ? soundOnIcon : soundOffIcon;
}
