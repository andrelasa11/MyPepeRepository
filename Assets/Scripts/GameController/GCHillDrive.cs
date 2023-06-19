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

    [Header("Exclusive UI")]
    public Image fuelUI;

    //hidden
    private Vector3 distanceReferencePoint;

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
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        distanceReferencePoint = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        scoreUI.SetScoreValueText(score);
        distanceUI.SetValueText(distance);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
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
        AudioManager.Instance.PlayFuel();

        if (fuel > 1)
        {
            fuel = 1;
        }
    }

    public override void OnGameOver()
    {
        audioSource.Stop();
        totalScore = score + distance;

        if (totalScore > GameManager.Instance.hillDriveRecord)
        {
            GameManager.Instance.SetHillDriveRecord(totalScore);
        }

        base.OnGameOver();
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

    #endregion
}
