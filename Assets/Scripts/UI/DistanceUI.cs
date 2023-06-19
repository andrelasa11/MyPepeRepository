using UnityEngine;
using UnityEngine.UI;

public class DistanceUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Text valueText;
    [SerializeField] private Text finalText;

    public void SetValueText(float distance)
    {
        valueText.text = distance.ToString("N2");
        finalText.text = distance.ToString("N2");
    }
}
