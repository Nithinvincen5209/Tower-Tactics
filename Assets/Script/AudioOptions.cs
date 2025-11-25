using UnityEngine;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    public Slider sfxScrollbar;
    public Slider  musicScrollbar;

    void Start()
    {
        // Load saved SFX
        float savedSfxValue = PlayerPrefs.GetFloat("SfxVolume", 1f);
        sfxScrollbar.value = savedSfxValue;
        AkSoundEngine.SetRTPCValue("SfxVolume", savedSfxValue * 100f);
        sfxScrollbar.onValueChanged.AddListener(SetSfxVolume);

        // Load saved Music
        float savedMusicValue = PlayerPrefs.GetFloat("MusicVolume", 1f);
        musicScrollbar.value = savedMusicValue;
        AkSoundEngine.SetRTPCValue("MusicVolume", savedMusicValue * 100f);
        musicScrollbar.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetSfxVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("SfxVolume", value * 100f);
        PlayerPrefs.SetFloat("SfxVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        AkSoundEngine.SetRTPCValue("MusicVolume", value * 100f);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
}
