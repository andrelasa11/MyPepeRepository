using UnityEngine;
using UnityEngine.UI;

public class PepperUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Text valueText;

    public void SetValueText(float countdown) => valueText.text = countdown.ToString();
}
