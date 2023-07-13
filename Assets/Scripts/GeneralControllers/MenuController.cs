using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private bool isPetMenu = false;
    [SerializeField] private Text rewardText;

    private void Start()
    {
        if(isPetMenu)
        {
            StartCoroutine(nameof(FrogSoundRoutine));
        }

        if(rewardText != null)
        {
            rewardText.text = "Congratulations! You have completed the task. Take your reward of " + GameManager.Instance.GameReward + " pepe coins!";
        }
    }


    public void PlayClickSound() => AudioManager.Instance.PlaySound("Select");

    public void PlayEating() => AudioManager.Instance.PlaySound("Eating");

    public void PlayShowering() => AudioManager.Instance.PlaySound("Showering");

    public void PlayFrogSound() => AudioManager.Instance.PlaySound("Frog");

    public IEnumerator FrogSoundRoutine()
    {
        yield return new WaitForSeconds(2);

        while(true)
        {
            PlayFrogSound();
            yield return new WaitForSeconds(0.5f);
            PlayFrogSound();
            yield return new WaitForSeconds(5);
        }
    }

    public void AddMoney()
    {
        GameManager.Instance.AddMoney(GameManager.Instance.GameReward);
    }
}
