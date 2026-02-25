using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;


    const string MasterKey = "MasterVolume";
    const string MusicKey = "MusicVolume";
    const string SfxKey = "SfxVolume";

    void Start()
    {
        float master = PlayerPrefs.GetFloat(MasterKey, 0.8f);
        float music = PlayerPrefs.GetFloat(MusicKey, 0.8f);
        float sfx = PlayerPrefs.GetFloat(SfxKey, 0.8f);

        if (masterSlider != null) masterSlider.value = master;
        if (musicSlider != null) musicSlider.value = music;
        if (sfxSlider != null) sfxSlider.value = sfx;

        mixer.SetFloat("MasterVolume", LinearToDB(master));
        mixer.SetFloat("MusicVolume", LinearToDB(music));
        mixer.SetFloat("SfxVolume", LinearToDB(sfx));

        if (masterSlider != null)
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
        
        if (musicSlider != null)
            musicSlider.onValueChanged.AddListener(SetMusicVolume);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    void SetMasterVolume(float v)
    {
        mixer.SetFloat("MasterVolume", LinearToDB(v));
        PlayerPrefs.SetFloat(MasterKey, v);
    }

    void SetMusicVolume(float v)
    {
        mixer.SetFloat("MusicVolume", LinearToDB(v));
        PlayerPrefs.SetFloat(MusicKey, v);
    }

    void SetSfxVolume(float v)
    {
        mixer.SetFloat("SfxVolume", LinearToDB(v));
        PlayerPrefs.SetFloat(SfxKey, v);
    }

    float LinearToDB(float value)
    {
        return Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
    }
}