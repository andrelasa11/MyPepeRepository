using System.Collections.Generic;
using UnityEngine;

public class GroundSpawnerController : MonoBehaviour
{
    #region "Singleton"

    private static GroundSpawnerController instance;

    public static GroundSpawnerController Instance { get { return instance; } }

    #endregion

    [Header("Configuration")]
    [SerializeField] private Vector3 startGenerationPosition; //x: 0, y: -7.5f, z: 0

    [Header("Grounds to Spawn")]
    [SerializeField] private List<GameObject> startGrounds;
    [SerializeField] private List<GameObject> grounds;    

    //hidden
    private int groundIndex;
    [HideInInspector] public List<GameObject> groundsToSpawn;

    private void Awake() => instance = this;

    private void Start()
    {
        foreach (var ground in grounds)
        {
            groundsToSpawn.Add(ground);
        }

        GenerateStartGround();
    }
    

    public void GenerateGround(Vector3 generationPosition)
    {
        groundIndex = Random.Range(0, groundsToSpawn.Count);

        Instantiate(groundsToSpawn[groundIndex], generationPosition, Quaternion.identity);

        groundsToSpawn.RemoveAt(groundIndex);

        if(groundsToSpawn.Count <= 0)
        {
            foreach (var ground in grounds)
            {
                groundsToSpawn.Add(ground);
            }
        }
    }

    public void GenerateStartGround()
    {
        int startGroundIndex = UnityEngine.Random.Range(0, startGrounds.Count);
        Instantiate(startGrounds[startGroundIndex], startGenerationPosition, Quaternion.identity);
    }
}
