using System.Collections;
using UnityEngine;

public class SpawnerCannonController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject germPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float initialSpawnDelay = 1.0f;
    [SerializeField] private float spawnInterval = 2.0f;

    private Coroutine spawnCoroutine;

    private void Start() => StartSpawning();

    private void StartSpawning()
    {
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnGerm());
    }

    private IEnumerator SpawnGerm()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Spawn() => Instantiate(germPrefab, transform.position, Quaternion.identity);
}