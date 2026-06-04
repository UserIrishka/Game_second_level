using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Zenject;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _masterSlider, _musicSlider, _effectSlider;
    [SerializeField] private Toggle _musicToggle;
    private MasterSave _masterSave;
    [Inject]
    private void Constract (MasterSave mastersave)
    {
        _masterSave = mastersave;
    }
    public void ChangeMusicVolume(float volume)
    {
        float mixerVolume = Mathf.Lerp(-80, 0, volume);
        _audioMixer.SetFloat("Music", mixerVolume);
        _masterSave.SaveData.MusicVolume = volume;
    }

    public void ChangeMasterVolume(float volume)
    {
        float mixerVolume = Mathf.Lerp(-80, 0, volume);
        _audioMixer.SetFloat("Master", mixerVolume);
        _masterSave.SaveData.MasterVolume = volume;
    }

    public void ChangeEffectsVolume(float volume)
    {
        float mixerVolume = Mathf.Lerp(-80, 0, volume);
        _audioMixer.SetFloat("Effects", mixerVolume);
        _masterSave.SaveData.EffectsVolume = volume;
    }

    public void ToggleMusic(bool value)
    {
        if (value == false)
        {
            _audioMixer.SetFloat("Master", -80);
            _masterSave.SaveData.MusicToggel = false;
        }
        else
        {
            _audioMixer.SetFloat("Master", 0);
            _masterSave.SaveData.MusicToggel = true;
        }
    }

    public void SaveSetting()
    {
        _masterSave.SaveAllData();
    }

    public void LoadSettings()
    {
        _masterSlider.value = _masterSave.SaveData.MasterVolume;
        _musicSlider.value = _masterSave.SaveData.MusicVolume;
        _effectSlider.value = _masterSave.SaveData.EffectsVolume;
        _musicToggle.isOn = _masterSave.SaveData.MusicToggel;
    }
}
