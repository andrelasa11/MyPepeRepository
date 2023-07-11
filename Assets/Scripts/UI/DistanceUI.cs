using UnityEngine;
using UnityEngine.UI;

public class DistanceUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Text valueText;

    public void SetValueText(float distance)
    {
        valueText.text = distance.ToString("N2");
    }
}
