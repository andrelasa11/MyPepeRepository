using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GCRunner : GameController
{
    #region "Singleton"

    private static GCRunner instance;

    public static GCRunner Instance { get { return instance; } }

    #endregion

    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float accelerationRate = 2f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Exclusive Dependencies")]
    [SerializeField] private Slider energyBar;

    public float Speed => speed;
    public float AccelerationRate => accelerationRate;
    public float MaxSpeed => maxSpeed;

    private void Awake()
    {
        instance = this;
        score = 0;
        totalScore = 0;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        AudioManager.Instance.SetBackgroundSound("BgRunner");
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        scoreUI.SetScoreValueText(score);
        lifeUI.SetValueText(playerLife);
        distanceUI.SetValueText(distance);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(nameof(DistanceRoutine));
        energyBar.value = GameManager.Instance.Energy;
    }

    private void FixedUpdate()
    {
        SetSpeed();

        distanceUI.SetValueText(distance);
    }

    private IEnumerator DistanceRoutine()
    {
        while (true)
        {
            distance += speed / 6;

            yield return new WaitForSeconds(0.5f);
        }

    }

    private void SetSpeed()
    {
        speed += accelerationRate * Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
    }

    public override void OnGameOver()
    {
        totalScore = score + distance;

        if (totalScore > GameManager.Instance.RunnerRecord)
        {
            GameManager.Instance.SetRunnerRecord(totalScore);
        }

        base.OnGameOver();
    }

    public void AddEnergy(float value)
    {
        GameManager.Instance.PutToBed(value);
        energyBar.value = GameManager.Instance.Energy;

        if (GameManager.Instance.Energy >= 100)
        {
            DoTheReward();
        }
    }

    private void DoTheReward()
    {
        GameManager.Instance.AddMoney(GameManager.Instance.GameReward);
        Time.timeScale = 0;
        rewardCanvas.SetActive(true);
    }

    public void DoBackToLobby()
    {
        Time.timeScale = 1f;
        OnGameOver();

    }
}
