using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private SOVolumeSettings volumeSettings; // Referência ao ScriptableObject

    private const float MaxVolume = 1f;

    private void Start()
    {
        LoadVolumePreferences();

        // Registrar os métodos nos eventos onValueChanged dos Sliders
        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundEffectsSlider.onValueChanged.AddListener(OnSoundEffectsVolumeChanged);
    }

    private void OnDestroy()
    {
        musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        soundEffectsSlider.onValueChanged.RemoveListener(OnSoundEffectsVolumeChanged);
    }

    public void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        volumeSettings.musicVolume = value; // Salvar o valor no ScriptableObject
    }

    public void OnSoundEffectsVolumeChanged(float value)
    {
        AudioManager.Instance.SetSoundEffectsVolume(value);
        volumeSettings.soundEffectsVolume = value; // Salvar o valor no ScriptableObject
    }

    private void LoadVolumePreferences()
    {
        musicSlider.value = volumeSettings.musicVolume; // Carregar o valor do ScriptableObject
        soundEffectsSlider.value = volumeSettings.soundEffectsVolume; // Carregar o valor do ScriptableObject

        AudioManager.Instance.SetMusicVolume(volumeSettings.musicVolume);
        AudioManager.Instance.SetSoundEffectsVolume(volumeSettings.soundEffectsVolume);
    }

    public void SaveVolumePreferences()
    {
        GameManager.Instance.SaveGame();
    }
}