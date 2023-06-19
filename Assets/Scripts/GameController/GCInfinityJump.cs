using System.Collections;
using UnityEngine;

public class GCInfinityJump : GameController
{

    #region "Singleton"

    private static GCInfinityJump instance;

    public static GCInfinityJump Instance { get { return instance; } }

    #endregion

    [Header("Configuration")]
    [SerializeField] private float initialPlatformSpeed;
    [SerializeField] private float secondStageSpeed;
    [SerializeField] private float thirdStageSpeed;
    [SerializeField] private float timeToSecondStage;
    [SerializeField] private float timeToThirdStage;
    [SerializeField] private float respawnTime;
    [SerializeField] private float distancePerSecond;
    [SerializeField] private float playerGravity;

    [Header("Cadence Configuration")]
    [SerializeField] private float cadenceToDecrease;


    [Header("Exclusive Dependencies")]
    [SerializeField] private AutoScroller autoScroller;
    [SerializeField] private GameObject player;
    [SerializeField] private SpawnerController spawnerController;
  

    // Platform Properties
    [HideInInspector] public float platformSpeed;
    
    //private
    private Vector3 startPosition;
    private bool isDead = false;
    private SpriteRenderer[] spritesToTint;

    #region "Awake/Start/Update"

    private void Awake()
    {
        instance = this;
        score = 0;
        distance = 0;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        AudioManager.Instance.PlayBgInfinityJump();
        mainCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        isDead = false;
        player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;
        StartCoroutine(StageCoroutine());
        startPosition = player.transform.position;
        StartCoroutine(DistanceCoroutine());
        StartCoroutine(RespawnPlayer());
        lifeUI.SetValueText(playerLife);
        scoreUI.SetScoreValueText(score);
        distanceUI.SetValueText(distance);
    }

    #endregion

    #region "My Methods"

    public override void SetPlayerLives(int value)
    {
        playerLife += value;
        lifeUI.SetValueText(playerLife);

        if (playerLife <= 0)
        {
            StopCoroutine(DistanceCoroutine());
            isDead = true;
            OnGameOver();
        }
        else
        {
            StartCoroutine(RespawnPlayer());
        }
    }

    public override void OnGameOver()
    {
        totalScore = score + distance;

        if (totalScore > GameManager.Instance.infinityJumpRecord)
        {
            GameManager.Instance.SetInfinityJumpRecord(totalScore);
        }

        base.OnGameOver();
    }

    #endregion

    #region "Coroutines"

    private IEnumerator StageCoroutine()
    {
        platformSpeed = initialPlatformSpeed;

        yield return new WaitForSeconds(timeToSecondStage);

        autoScroller.scrollSpeed *= 2;
        platformSpeed = secondStageSpeed;
        distancePerSecond += 1.5f;
        spawnerController.cadence -= cadenceToDecrease;

        yield return new WaitForSeconds(timeToThirdStage);

        autoScroller.scrollSpeed *= 1.5f;
        platformSpeed = thirdStageSpeed;
        distancePerSecond += 2.5f;
        spawnerController.cadence -= cadenceToDecrease;
    }

    private IEnumerator RespawnPlayer()
    {        
        spritesToTint = player.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spritesToTint)
        {
            spriteRenderer.enabled = false;
        }


        player.layer = 3;

        yield return new WaitForSeconds(respawnTime);

        player.transform.position = startPosition;
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;


        for (float i = 0; i < 3; i += 0.1f)
        {
            foreach(SpriteRenderer spriteRenderer in spritesToTint)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
            
            yield return new WaitForSeconds(0.1f);            
        }

        player.layer = 7;

        player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;
        
        foreach (SpriteRenderer spriteRenderer in spritesToTint)
        {
            spriteRenderer.enabled = true;
        }
    }

    private IEnumerator DistanceCoroutine()
    {
        while(isDead == false)
        {
            distance += (distancePerSecond/2);

            distanceUI.SetValueText(distance);

            yield return new WaitForSeconds(0.5f);
        }
    }

    #endregion

}