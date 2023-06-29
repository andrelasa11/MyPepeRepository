using UnityEngine;

public class PlatformController : MonoBehaviour
{
    #region Parallax Effect

    [Header("Parallax Effect")]
    [SerializeField] private float depth = 1f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    #endregion

    #region Obstacle

    [Header("Obstacle")]
    [SerializeField] private Transform[] obstacleSpawnPoints;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField, Range(0f, 1f)] private float obstacleSpawnChance = 0.5f;
    [SerializeField] private CollisionTag obstacleTag;

    #endregion

    [Header("Reposition Settings")]
    [SerializeField] private float repositionX = 20f;
    [SerializeField] private float repositionThreshold = -20f;

    [SerializeField] private float currentSpeed; // Armazena a velocidade atual

    private void Start()
    {
        currentSpeed = GCRunner.Instance.Speed; // Inicializa a velocidade com a velocidade inicial do GCRunner
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        currentSpeed += GCRunner.Instance.AccelerationRate * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, GCRunner.Instance.MaxSpeed);

        float realVelocity = currentSpeed / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= repositionThreshold)
        {
            pos.x = repositionX;
            pos.y = Random.Range(minY, maxY);
            SpawnObstacles();
            currentSpeed += GCRunner.Instance.AccelerationRate * Time.fixedDeltaTime; // Mantém a velocidade atualizada após o reposicionamento
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, GCRunner.Instance.MaxSpeed);
        }

        transform.position = pos;
    }

    private void SpawnObstacles()
    {
        DestroyPreviousObstacles();
        InstantiateNewObstacles();
    }

    private void DestroyPreviousObstacles()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(obstacleTag.ToString()))
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void InstantiateNewObstacles()
    {
        foreach (Transform spawnPoint in obstacleSpawnPoints)
        {
            if (Random.value <= obstacleSpawnChance)
            {
                GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity);
                obstacle.tag = obstacleTag.ToString();
                obstacle.transform.parent = transform;
            }
        }
    }
}