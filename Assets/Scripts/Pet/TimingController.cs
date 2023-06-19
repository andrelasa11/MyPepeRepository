using UnityEngine;

public class TimingController : MonoBehaviour
{
    [Header("Configuration")]
    public float gameHourTimer;
    public float hourLength;

    private void Update()
    {
        if(gameHourTimer <= 0)
        {
            gameHourTimer = hourLength;
        }
        else
        {
            gameHourTimer -= Time.deltaTime;
        }
    }
}
