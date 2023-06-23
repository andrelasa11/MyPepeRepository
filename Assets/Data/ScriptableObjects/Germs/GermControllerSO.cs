using UnityEngine;

[CreateAssetMenu(fileName = "GermController", menuName = "ScriptableObjects/GermController")]
public class GermControllerSO : ScriptableObject
{
    public Sprite sprite;
    public float speed = 5.0f;
    public RuntimeAnimatorController animatorController;
    public float healthPoints;
    public int score = 10;
}
