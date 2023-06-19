using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region "Singleton"

    private static AudioManager instance;

    public static AudioManager Instance { get { return instance; } }

    #endregion

    [Header("Lobby")]
    public AudioClip select;
    public AudioClip showering;
    public AudioClip bgLobby;
    public AudioClip dancingSong;

    [Header("InfinityJump")]
    public AudioClip jumping;
    public AudioClip bgInfinityJump;

    [Header("FoodDrop")]
    public AudioClip eating;
    public AudioClip failure;
    public AudioClip bgFoodDropSound;

    [Header("HillDrive")]
    public AudioClip fuel;
    public AudioClip bgHillDrive;

    [Header("Generals")]
    public AudioClip coin;
    public AudioClip death;

    [Header("Dependencies")]
    [SerializeField] private AudioSource audioSource;

    //private
    private bool isDancing = false;

    private void Awake()
    {
        if(instance != null) Destroy(gameObject);

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    //main method
    private void PlaySound(AudioClip clip) => audioSource.PlayOneShot(clip);

    //main method for background sound
    private void SetBGSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    #region "Lobby Methods"

    public void PlaySelect() => PlaySound(select);

    public void PlayShowering() => PlaySound(showering);

    public void PlayBgLobby() => SetBGSound(bgLobby);

    public void PlayDacingSong()
    {
        if (isDancing == false)
        {
            SetBGSound(dancingSong);
            isDancing = true;
        }
        else
        {
            SetBGSound(bgLobby);
            isDancing = false;
        }
        
    }    

    #endregion

    #region "Infinity Jump Methods"

    public void PlayJump() => PlaySound(jumping);

    #endregion

    #region "Food Drop Methods"

    public void PlayEating() => PlaySound(eating);

    public void PlayFailure() => PlaySound(failure);

    public void PlayBgInfinityJump() => SetBGSound(bgInfinityJump);

    #endregion

    #region "Hill Drive Methods"

    public void PlayFuel() => PlaySound(fuel);

    public void PlayBgHillDrive() => SetBGSound(bgHillDrive);

    #endregion

    #region "Generals"

    public void PlayCoin() => PlaySound(coin);

    public void PlayDeath() => PlaySound(death);

    public void PlayBgFoodDrop() => SetBGSound(bgFoodDropSound);

    #endregion
}
