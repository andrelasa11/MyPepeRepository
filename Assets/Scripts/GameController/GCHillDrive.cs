using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GCHillDrive : GameController
{
    #region "Singleton"

    private static GCHillDrive instance;

    public static GCHillDrive Instance { get { return instance; } }

    #endregion

    [Header("Fuel Configuration")]
    public float fuel;
    public float fuelConsumption;

    [Header("Exclusive Dependencies")]
    [SerializeField] private PCHillDrive player;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Slider happinessBar;
    [SerializeField] private GameObject continueCanvas;

    [Header("Exclusive UI")]
    public Image fuelUI;

    //hidden
    private Vector3 distanceReferencePoint;
    private bool firstRun = true;

    #region "Awake/Start/Update"

    private void Awake()
    {
        instance = this;
        score = 0;
        distance = 0;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        AudioManager.Instance.SetBackgroundSound("BgHillDrive");
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        distanceReferencePoint = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        scoreUI.SetScoreValueText(score);
        distanceUI.SetValueText(distance);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        happinessBar.value = GameManager.Instance.Happiness;
    }

    private void Update()
    {
        distance = Vector3.Distance(distanceReferencePoint, player.transform.position);
        distanceUI.SetValueText(distance);
    }

    #endregion

    #region "My Methods"

    public void SetFuel(float value)
    {
        fuel += value;
        AudioManager.Instance.PlaySound("Fuel");

        if (fuel > 1)
        {
            fuel = 1;
        }
    }

    public override void OnGameOver()
    {
        audioSource.Stop();
        totalScore = score + distance;

        if (totalScore > GameManager.Instance.HillDriveRecord)
        {
            GameManager.Instance.SetHillDriveRecord(totalScore);
        }

        base.OnGameOver();
    }

    public override void SetScore(int scorePoints)
    {
        AudioManager.Instance.PlaySound("Frog");
        base.SetScore(scorePoints);
    }

    public void NoFuelGameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    #endregion

    #region "Coroutines"

    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(2);
        OnGameOver();
    }

    public void AddHappiness(float value)
    {
        GameManager.Instance.PlayWithPet(value);
        happinessBar.value = GameManager.Instance.Happiness;

        if (GameManager.Instance.Happiness >= 100 && firstRun)
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

    #endregion
}
