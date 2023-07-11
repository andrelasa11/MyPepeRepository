using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [SerializeField] private bool isPetMenu = false;

    private void Start()
    {
        if(isPetMenu)
        {
            StartCoroutine(nameof(FrogSoundRoutine));
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
}
