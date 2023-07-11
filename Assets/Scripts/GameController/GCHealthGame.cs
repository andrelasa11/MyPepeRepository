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
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject continueCanvas;

    private bool firstRun = true;

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

        AudioManager.Instance.SetBackgroundSound("BgHealthGame");

        Screen.orientation = ScreenOrientation.Landscape;
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
        healthBar.value = GameManager.Instance.Health;

        if (GameManager.Instance.Health >= 100 && firstRun)
        {
            OnContinueAsk();
        }
    }
    private void OnContinueAsk()
    {
        Time.timeScale = 0;
        continueCanvas.SetActive(true);
    }

    public void WannaContinue()
    {
        Time.timeScale = 1f;
        firstRun = false;
        continueCanvas.SetActive(false);
    }

    public void DontWannaContinue()
    {
        Time.timeScale = 1f;
        OnGameOver();

    }
}
