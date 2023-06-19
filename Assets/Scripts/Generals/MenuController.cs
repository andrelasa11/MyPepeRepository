using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PlayClickSound() => AudioManager.Instance.PlaySelect();

    public void PlayEating() => AudioManager.Instance.PlayEating();

    public void PlayShowering() => AudioManager.Instance.PlayShowering();
}
