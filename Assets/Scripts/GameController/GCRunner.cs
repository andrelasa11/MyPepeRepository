using UnityEngine;

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
        AudioManager.Instance.PlayBgRunner();
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        scoreUI.SetScoreValueText(score);
        lifeUI.SetValueText(playerLife);
        distanceUI.SetValueText(distance);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
