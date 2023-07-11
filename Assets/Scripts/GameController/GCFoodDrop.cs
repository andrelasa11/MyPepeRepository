using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GCFoodDrop : GameController
{

    #region "Singleton"

    private static GCFoodDrop instance;

    public static GCFoodDrop Instance { get { return instance; } }

    #endregion


    [Header("Food Speed Configuration")]
    [SerializeField] private float initialFoodSpeed;
    [SerializeField] private float secondStageSpeed;
    [SerializeField] private float thirdStageSpeed;
    [SerializeField] private float timeToSecondStage;
    [SerializeField] private float timeToThirdStage;

    [Header("Cadence Configuration")]
    [SerializeField] private float cadenceToDecrease;
    [SerializeField] private float onFireWaitTime;
    private float onFireCountdown;

    [Header("Exclusive Dependencies")]
    [SerializeField] private SpawnerController spawnerController;
    [SerializeField] private PepperUI pepperUI;
    [SerializeField] private GameObject pepperGO;
    [SerializeField] private Slider hungerhBar;
    [SerializeField] private GameObject continueCanvas;

    // Food Properties
    [HideInInspector] public float foodSpeed;
    private bool isOnFire = false;
    private bool firstRun = true;

    private void Awake()
    {
        instance = this;
        score = 0;
        totalScore = 0;
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {            
        StartCoroutine(StageCoroutine());

        scoreUI.SetScoreValueText(score);

        lifeUI.SetValueText(playerLife);

        AudioManager.Instance.SetBackgroundSound("BgFoodDrop");

        onFireCountdown = onFireWaitTime;

        pepperUI.SetValueText(onFireCountdown);

        hungerhBar.value = GameManager.Instance.Hunger;

        continueCanvas.SetActive(false);
    }

    #region "My Methods"

    public override void SetScore(int scorePoints)
    {
        if(isOnFire == false)
        {
            score += scorePoints;
            scoreUI.SetScoreValueText(score);
        }
        if(isOnFire == true)
        {
            score += scorePoints * 2;
            scoreUI.SetScoreValueText(score);
        }      

        if(scorePoints > 0)
        {
            AudioManager.Instance.PlaySound("Eating");
        }

        AddFood(scorePoints);
    }

    public override void OnGameOver()
    {
        totalScore = score;

        if (totalScore > GameManager.Instance.FoodDropRecord)
        {
            GameManager.Instance.SetFoodDropRecord(totalScore);
        }

        base.OnGameOver();        
    }

    public void SetOnFire()
    {
        if(isOnFire == false)
        {
            onFireCountdown = onFireWaitTime;
            pepperUI.SetValueText(onFireCountdown);
            StartCoroutine(nameof(OnFireCoroutine));
        }
        else
        {
            onFireCountdown = onFireWaitTime;
            pepperUI.SetValueText(onFireCountdown);
        }

    }

    private void AddFood(int value)
    {
        float valueToAdd;

        valueToAdd = value / 3;

        GameManager.Instance.FeedPet(valueToAdd);
        hungerhBar.value = GameManager.Instance.Hunger;

        if (GameManager.Instance.Hunger >= 100 && firstRun)
        {
            OnContinueAsk();
        }

        Debug.Log("Value To Add: " + valueToAdd);
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

    #region "Coroutines"

    private IEnumerator StageCoroutine()
    {
        foodSpeed = initialFoodSpeed;

        yield return new WaitForSeconds(timeToSecondStage);

        foodSpeed = secondStageSpeed;
        spawnerController.cadence -= cadenceToDecrease;

        yield return new WaitForSeconds(timeToThirdStage);

        foodSpeed = thirdStageSpeed;
        spawnerController.cadence -= cadenceToDecrease;
    }

    private IEnumerator OnFireCoroutine()
    {
        isOnFire = true;
        pepperGO.SetActive(true);

        while (onFireCountdown > 0)
        {
            onFireCountdown -= 0.5f;
            Debug.Log(onFireCountdown);
            yield return new WaitForSeconds(0.5f);
            pepperUI.SetValueText(onFireCountdown);
        }

        isOnFire = false;
        pepperGO.SetActive(false);
    }

    #endregion

}
