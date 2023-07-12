using UnityEngine;

[CreateAssetMenu(fileName = "GermController", menuName = "ScriptableObjects/GermController")]
public class SOGermController : ScriptableObject
{
    [Header("Organization")]
    public string germName;
    public string level;

    [Header("Configuration")]
    public Sprite sprite;
    public float speed = 5.0f;
    public string animatorController;
    public float healthPoints;
    public int score = 10;
}
