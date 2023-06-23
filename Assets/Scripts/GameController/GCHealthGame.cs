using UnityEngine.UI;
using UnityEngine;

public class GCHealthGame : GameController
{

    #region "Singleton"

    private static GCHealthGame instance;

    public static GCHealthGame Instance { get { return instance; } }

    #endregion

    [Header("Germs Configuration")]
    [SerializeField] private float cadenceToDecrease;

    [Header("Exclusive Dependencies")]
    public Slider healthBar;
    public AudioClip healthGameMusic;

    private void Awake()
    {
        instance = this;
        score = 0;
        totalScore = 0;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        scoreUI.SetScoreValueText(score);

        lifeUI.SetValueText(playerLife);

        healthBar.value = GameManager.Instance.Health;

        AudioManager.Instance.PlayBgFoodDrop();

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public override void OnGameOver()
    {
        totalScore = score + distance;

        if (totalScore > GameManager.Instance.HealthGameRecord)
        {
            GameManager.Instance.SetHealthGameRecord(totalScore);
        }

        base.OnGameOver();
    }

    public void AddHealth (float value)
    {
        GameManager.Instance.HealPet(value);
        healthBar.value += value;

        if (healthBar.value >= 100)
        {
            OnGameOver();
        }
    }
}
