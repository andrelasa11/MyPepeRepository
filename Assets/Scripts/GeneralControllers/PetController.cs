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

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;

        UpdateRecordTexts();
        UpdateMoneyText();
        Screen.orientation = ScreenOrientation.Portrait;
        AudioManager.Instance.SetBackgroundSound("BgLobby");
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
        Debug.Log("UpdateRecords Chamado! " + gameManager.FoodDropRecord);
        foodDropRecordText.text = gameManager.FoodDropRecord.ToString("N0");
        healthGameRecordText.text = gameManager.HealthGameRecord.ToString("N0");
        hillDriveRecordText.text = gameManager.HillDriveRecord.ToString("N0");
        runnerRecordText.text = gameManager.RunnerRecord.ToString("N0");
    }

    public void UpdateMoneyText()
    {
        moneyText.text = gameManager.Money.ToString("N2");
    }

}