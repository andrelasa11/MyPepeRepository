using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameButtonController : MonoBehaviour
{
    [SerializeField] private GameType gameType;
    [SerializeField] private LoadScene loadScene;
    [SerializeField] private GameObject warningBar;
    [SerializeField] private Text warningTxt;
    
    public void DoLoad()
    {
        OnLoadGame(gameType, GameManager.Instance.GameTax);
    }

    private void OnLoadGame(GameType gameType, float gameTax)
    {
        switch (gameType)
        {
            case GameType.HealthGame:
                if (GameManager.Instance.Health <= 5 && GameManager.Instance.Money >= gameTax)
                {
                    loadScene.DoLoadScene("HealthGame");
                    SetMoney(-gameTax);
                }
                else
                {
                    if (GameManager.Instance.Money < gameTax)
                    {
                        warningTxt.text = "You don't have money enough to play!";
                    }
                    else
                    {                     
                        warningTxt.text = "Your status isn't low enough to play!";
                    }
                    StartCoroutine(nameof(WarningBarRoutine));
                }
                break;
            case GameType.FoodDrop:
                if (GameManager.Instance.Hunger <= 5 && GameManager.Instance.Money >= gameTax)
                {
                    SetMoney(-gameTax);
                    loadScene.DoLoadScene(gameType.ToString());
                }
                else
                {
                    if (GameManager.Instance.Money < gameTax)
                    {
                        warningTxt.text = "You don't have money enough to play!";
                    }
                    else
                    {
                        warningTxt.text = "Your status isn't low enough to play!";
                    }
                    StartCoroutine(nameof(WarningBarRoutine));
                }
                break;
            case GameType.HillDrive:
                if (GameManager.Instance.Happiness <= 5 && GameManager.Instance.Money >= gameTax)
                {
                    SetMoney(-gameTax);
                    loadScene.DoLoadScene(gameType.ToString());
                }
                else
                {
                    if (GameManager.Instance.Money < gameTax)
                    {
                        warningTxt.text = "You don't have money enough to play!";
                    }
                    else
                    {
                        warningTxt.text = "Your status isn't low enough to play!";
                    }
                    StartCoroutine(nameof(WarningBarRoutine));
                }
                break;
            case GameType.Runner:
                if (GameManager.Instance.Energy <= 5 && GameManager.Instance.Money >= gameTax)
                {
                    SetMoney(-gameTax);
                    loadScene.DoLoadScene(gameType.ToString());
                }
                else
                {
                    if (GameManager.Instance.Money < gameTax)
                    {
                        warningTxt.text = "You don't have money enough to play!";
                    }
                    else
                    {
                        warningTxt.text = "Your status isn't low enough to play!";
                    }
                    StartCoroutine(nameof(WarningBarRoutine));
                }
                break;
        }
    }

    public void SetMoney(float value)
    {
        GameManager.Instance.AddMoney(value);
    }

    private IEnumerator WarningBarRoutine()
    {
        warningBar.SetActive(true);

        yield return new WaitForSeconds(5);

        warningBar.SetActive(false);
    }
}
