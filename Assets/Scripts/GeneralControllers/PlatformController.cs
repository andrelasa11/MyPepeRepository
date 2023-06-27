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

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float currentSpeed = GCRunner.Instance.Speed;

        currentSpeed += GCRunner.Instance.AccelerationRate * Time.fixedDeltaTime;
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, GCRunner.Instance.MaxSpeed);

        float realVelocity = currentSpeed / depth;
        Vector2 pos = transform.position;

        pos.x -= realVelocity * Time.fixedDeltaTime;

        if (pos.x <= -20)
        {
            pos.x = 20;
            pos.y = Random.Range(minY, maxY);
            SpawnObstacles();
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