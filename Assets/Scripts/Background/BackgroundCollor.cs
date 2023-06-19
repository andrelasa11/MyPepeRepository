using UnityEngine;

public class BackgroundCollor : MonoBehaviour
{

    [SerializeField] private Camera cameraUI;
    [SerializeField] private Color color;
  
    public void ChangeBackgroundCollor(string hexadecimalColorCode)
    {
        ColorUtility.TryParseHtmlString(hexadecimalColorCode, out color);

        cameraUI.backgroundColor = color;
    }

    
}
