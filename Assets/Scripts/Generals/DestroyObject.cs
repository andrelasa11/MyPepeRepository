using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    [Header("Configuration")]
    [SerializeField] private float destroyTime;

    public void DestroyThis() => Destroy(gameObject, destroyTime);
}