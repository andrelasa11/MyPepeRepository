using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void PlayClickSound() => AudioManager.Instance.PlaySound("Select");

    public void PlayEating() => AudioManager.Instance.PlaySound("Eating");

    public void PlayShowering() => AudioManager.Instance.PlaySound("Showering");
}
