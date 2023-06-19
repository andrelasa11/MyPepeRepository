using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    [Header("Configuration")]
    [Range(0, 1)] public float pickupChance;

    [Header("Dependencies")]
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private GameObject coinPrefab;

    //private
    private float randomPickUp;
    private Quaternion spawnRotation = new Quaternion(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start() => CoinDice();

    public void CoinDice()
    {
        foreach (Transform coin in spawnPositions)
        {
            randomPickUp = Random.Range(0, 1f);

            if (randomPickUp <= pickupChance)
            {
                Instantiate(coinPrefab, coin.transform.position, spawnRotation);
            }
        }
    }
}
