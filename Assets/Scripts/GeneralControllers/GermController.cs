using UnityEngine;

public class GermController : MonoBehaviour
{
    [Header("Germ Controller Settings")]
    [SerializeField] private GermControllerSO[] germControllerSOs;

    private GermControllerSO currentGermControllerSO;
    private SpriteRenderer spriteRenderer;
    private float speed;
    private int scorePoints;
    private float healthPoints;

    private void Start() => InitializeGermController();

    private void InitializeGermController()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SelectRandomGermControllerSO();
        SetVariablesFromGermControllerSO();
    }

    private void SelectRandomGermControllerSO()
    {
        int index = Random.Range(0, germControllerSOs.Length);
        currentGermControllerSO = germControllerSOs[index];
    }

    private void SetVariablesFromGermControllerSO()
    {
        spriteRenderer.sprite = currentGermControllerSO.sprite;
        speed = currentGermControllerSO.speed;
        scorePoints = currentGermControllerSO.score;
        healthPoints = currentGermControllerSO.healthPoints;
    }

    private void Update() => Move();

    private void Move() => transform.Translate(Vector2.left * speed * Time.deltaTime);

    public void AddScoreAndHealth() => UpdateScoreAndHealth(scorePoints, healthPoints);

    public void DecreaseScoreAndHealth() => UpdateScoreAndHealth(-scorePoints, -healthPoints);

    public void DecreaseLife() => GCHealthGame.Instance.SetPlayerLives(-1);

    private void UpdateScoreAndHealth(int scoreDelta, float healthDelta)
    {
        GCHealthGame.Instance.SetScore(scoreDelta);
        GCHealthGame.Instance.AddHealth(healthDelta);
    }
}