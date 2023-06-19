using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject lobby3;
    [SerializeField] private GameObject lobby1;
    
    //private
    private int countdown = 6;
    private bool countdownOn = true;

    public void StartCountdown()
    {        
        countdownOn = true;
        StartCoroutine(CountdownCoroutine());
    }

    public void ExitCountdown()
    {
        countdownOn = false;
        countdown = 6;
        lobby3.SetActive(false);
        lobby1.SetActive(true);
    }

    public void PlayClickSound() => AudioManager.Instance.PlaySelect();

    public IEnumerator CountdownCoroutine()
    {
        while (countdownOn)
        {
            countdown--;

            countdownText.text = countdown.ToString();                        

            if (countdown <= 0)
            {
                ExitCountdown();
            }

            yield return new WaitForSeconds(1);
        }
    }
}
