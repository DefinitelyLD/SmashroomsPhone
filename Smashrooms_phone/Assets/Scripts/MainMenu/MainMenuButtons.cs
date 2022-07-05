using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] List<Button> mainMenuButtons;

    private void Awake()
    {
        foreach(Button button in mainMenuButtons)
        button.onClick.AddListener(() => AudioManager.instance.PlaySoundOnButtonClicked());
    }
}
