using UnityEngine;
using UnityEngine.UI;

public class PetStatusUI : MonoBehaviour
{
    [Header("Sliders")]
    public Slider hungerSlider;
    public Slider energySlider;
    public Slider healthSlider;
    public Slider happinessSlider;

    [Header("Records")]
    public Text foodDropRecordText;
    public Text healthGameRecordText;
    public Text hillDriveRecordText;
    public Text runnerRecordText;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        UpdateSliders();
        UpdateRecordTexts();
    }

    private void UpdateSliders()
    {
        hungerSlider.value = gameManager.Hunger;
        energySlider.value = gameManager.Energy;
        healthSlider.value = gameManager.Health;
        happinessSlider.value = gameManager.Happiness;
    }

    private void UpdateRecordTexts()
    {
        foodDropRecordText.text = "Food Drop Record: " + gameManager.FoodDropRecord;
        healthGameRecordText.text = "Health Game Record: " + gameManager.HealthGameRecord;
        hillDriveRecordText.text = "Hill Drive Record: " + gameManager.HillDriveRecord;
        runnerRecordText.text = "Runner Record: " + gameManager.RunnerRecord;
    }
}