using UnityEngine;

public class Rotator : MonoBehaviour
{

    [Header("Configuration")]
    public float speedRotation;

    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, speedRotation) * Time.deltaTime);
    }
}
