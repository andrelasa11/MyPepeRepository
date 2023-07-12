using UnityEngine;

[CreateAssetMenu(fileName = "VolumeSettings", menuName = "ScriptableObjects/VolumeSettings")]
public class SOVolumeSettings : ScriptableObject
{
    public float musicVolume = 1f;
    public float soundEffectsVolume = 1f;
}
