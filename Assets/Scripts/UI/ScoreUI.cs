using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Text scoreValueText;
    [SerializeField] private Text finalScoreValueText;
    [SerializeField] private Text totalValueText;
    [SerializeField] private Text recordValueText;
    [SerializeField] private GameType gameType;

    public void SetScoreValueText(int scoreValue) => scoreValueText.text = scoreValue.ToString("N0");

    public void SetTotalValueText(int scoreValue, float totalScoreValue)
    {
        finalScoreValueText.text = scoreValue.ToString("N0");
        totalValueText.text = totalScoreValue.ToString("N2");

        switch(gameType)
        {
            case GameType.InfinityJump:
                recordValueText.text = GameManager.Instance.infinityJumpRecord.ToString("N2");
                break;

            case GameType.FoodDrop:
                recordValueText.text = GameManager.Instance.foodDropRecord.ToString("N2");
                break;

            case GameType.HillDrive:
                recordValueText.text = GameManager.Instance.hillDriveRecord.ToString("N2");
                break;
        }
    }
}
