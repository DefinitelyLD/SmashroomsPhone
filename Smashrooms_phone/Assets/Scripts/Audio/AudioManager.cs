using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private const float MIN_SOUND_VALUE = -80f;
    public bool soundOn = true;

    [SerializeField] AudioSource mainThemePlayer, buttonsPlayer, punchPlayer, enemyPunchPlayer;

    private void Awake() 
    {   
        if(instance) Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void ChangeMainTheme(MusicThemes theme)
    {
        mainThemePlayer.Stop();
        AudioClip clipToPlay = Storage.instance.GetAudioTheme(theme);
        mainThemePlayer.clip = clipToPlay;
        
        mainThemePlayer.Play();
    }
    public void StopMusic() => mainThemePlayer.Stop();
    public void PlaySoundOnButtonClicked()
    {
        buttonsPlayer.volume = Random.Range(0.9f, 1f);
        buttonsPlayer.pitch = Random.Range(0.95f, 1.05f);
        buttonsPlayer.Play();
    }

    public void ChangeMuteState()
    {
        soundOn = !soundOn;
        MuteSound(soundOn);
    }

    public void PlayPunchSound(bool isEnemy)
    {
        AudioSource player = isEnemy? enemyPunchPlayer : punchPlayer;

        player.Stop();
        player.volume = Random.Range(0.5f, 0.6f);
        player.pitch = Random.Range(0.95f, 1.05f);
        player.Play();
    }
    public void MuteSound(bool mute) => AudioListener.volume = mute ? 1 : 0;
}
public enum MusicThemes
{
    mainMenuTheme,
    fightTheme,
    winTheme,
    loseTheme,
}