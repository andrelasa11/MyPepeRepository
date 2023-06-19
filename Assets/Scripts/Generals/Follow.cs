using UnityEngine;

public class Follow : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private Transform target;

    //private
    private Vector3 offset;

    void Start() => offset = transform.position - target.position;

    void Update() => transform.position = target.position + offset;

}
