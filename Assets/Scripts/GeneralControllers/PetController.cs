using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PetController : MonoBehaviour
{
    [Header("Sliders")]
    public Slider hungerSlider;
    public Slider energySlider;
    public Slider healthSlider;
    public Slider happinessSlider;

    [Header("RecordsTxT")]
    public Text foodDropRecordText;
    public Text healthGameRecordText;
    public Text hillDriveRecordText;
    public Text runnerRecordText;

    [Header("MoneyTxt")]
    public Text moneyText;

    [Header("Dependencies")]
    [SerializeField] private GameObject statusSliders;
    [SerializeField] private GameObject petSprite;
    [SerializeField] private GameObject eggSprite;
    [SerializeField] private GameObject interactiveButtons;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject congratsWindow;
    [SerializeField] private GameObject buyPepeButton;
    [SerializeField] private Text congratsWindowTxt;
    [SerializeField] private Animator eggAnimator;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        UpdateRecordTexts();
        UpdateMoneyText();
        Screen.orientation = ScreenOrientation.Portrait;
        AudioManager.Instance.SetBackgroundSound("BgLobby");

        if(gameManager.NumOfPets < 1)
        {
            ShowEggCanvas();
        }
        else
        {
            ShowPetCanvas();
        }
    }

    private void Update()
    {
        UpdateSliders();
    }

    private void UpdateSliders()
    {
        hungerSlider.value = gameManager.Hunger;
        energySlider.value = gameManager.Energy;
        healthSlider.value = gameManager.Health;
        happinessSlider.value = gameManager.Happiness;
    }

    public void UpdateRecordTexts()
    {        
        foodDropRecordText.text = gameManager.FoodDropRecord.ToString("N0");
        healthGameRecordText.text = gameManager.HealthGameRecord.ToString("N0");
        hillDriveRecordText.text = gameManager.HillDriveRecord.ToString("N0");
        runnerRecordText.text = gameManager.RunnerRecord.ToString("N0");
    }

    public void UpdateMoneyText()
    {
        moneyText.text = gameManager.Money.ToString("N2");
    }

    private void ShowEggCanvas()
    {
        statusSliders.SetActive(false);
        petSprite.SetActive(false);
        eggSprite.SetActive(true);
        playButton.SetActive(false); 
        buyPepeButton.SetActive(true);
    }

    public void ShowPetCanvas()
    {
        if(gameManager.NumOfPets < 1)
        {
            return;
        }

        statusSliders.SetActive(true);
        playButton.SetActive(true);
        petSprite.SetActive(true);
        eggSprite.SetActive(false);
        interactiveButtons.SetActive(true);
        buyPepeButton.SetActive(false);
    }

    public void DoBuyTheFrog()
    {
        if(gameManager.Money >= gameManager.EggPrice)
        {
            congratsWindowTxt.text = "Congratulations! You've purchased your pepe for " + gameManager.EggPrice + " pepecoins! Take good care of it!";
            StartCoroutine(nameof(BuyRoutine));
        }
        else
        {
            congratsWindowTxt.text = "You don't have enough pepecoins! Please go to the store to acquire them!";
            congratsWindow.SetActive(true);
        }
        
    }

    public IEnumerator BuyRoutine()
    {
        buyPepeButton.SetActive(false);
        gameManager.AddMoney(-gameManager.EggPrice);
        gameManager.AddPet(1);
        interactiveButtons.SetActive(false);
        eggAnimator.SetBool("buy", true);
        AudioManager.Instance.PlaySound("Born");
        yield return new WaitForSeconds(3);
        AudioManager.Instance.PlaySound("Frog");
        congratsWindow.SetActive(true);

    }

}