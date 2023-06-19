using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider dirtSlider;
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider foodSlider;

    [Header("LobbyImages")]
    [SerializeField] private Image lampFilter;
    [SerializeField] private Image gameLobby;

    [Header("LobbyRecords")]
    [SerializeField] private Text jmRecordValue;
    [SerializeField] private Text fdRecordValue;
    [SerializeField] private Text hdRecordValue;

    public void SetDirt(int value) => dirtSlider.value = value;

    public void SetHappiness(int value) => happinessSlider.value = value;

    public void SetEnergy(int value) => energySlider.value = value;

    public void SetFood(int value) => foodSlider.value = value;

    public void SetRecords()
    {
        jmRecordValue.text = GameManager.Instance.infinityJumpRecord.ToString("N2");
        fdRecordValue.text = GameManager.Instance.foodDropRecord.ToString("N2");
        hdRecordValue.text = GameManager.Instance.hillDriveRecord.ToString("N2");
    }

    public void SetLampFilter()
    {
        if (lampFilter.isActiveAndEnabled) lampFilter.gameObject.SetActive(false);

        else lampFilter.gameObject.SetActive(true);
    }

    public void OffLampFilter()
    {
        if (lampFilter.isActiveAndEnabled) lampFilter.gameObject.SetActive(false);
    }

    public void SetGameLobby()
    {
        if (gameLobby.isActiveAndEnabled) gameLobby.gameObject.SetActive(false);

        else gameLobby.gameObject.SetActive(true);
    }

    public void OffGameLobby()
    {
        if (gameLobby.isActiveAndEnabled) gameLobby.gameObject.SetActive(false);
    }
}
