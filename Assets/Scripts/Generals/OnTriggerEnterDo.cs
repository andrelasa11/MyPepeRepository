using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterDo : MonoBehaviour
{
    [SerializeField] private UnityEvent playerActions;
    [SerializeField] private UnityEvent playerHeadActions;
    [SerializeField] private UnityEvent dangerActions;
    [SerializeField] private UnityEvent platformActions;
    [SerializeField] private UnityEvent petFoodActions;
    [SerializeField] private UnityEvent coinActions;
    [SerializeField] private UnityEvent limitsActions;
    [SerializeField] private UnityEvent generalActions;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActions.Invoke();
        }
        /*else if (collision.CompareTag("PlayerHead"))
        {
            playerHeadActions.Invoke();
        }*/
        else if (collision.CompareTag("Danger"))
        {
            dangerActions.Invoke();
        }
        /*else if (collision.CompareTag("Platform"))
        {
            platformActions.Invoke();
        }*/
        else if (collision.CompareTag("PetFood"))
        {
            petFoodActions.Invoke();
        }
        /*else if (collision.CompareTag("Coin"))
        {
            coinActions.Invoke();
        }*/
        else if (collision.CompareTag("Limits"))
        {
            limitsActions.Invoke();
        }
        else
        {
            generalActions.Invoke();
        }
    }
}
