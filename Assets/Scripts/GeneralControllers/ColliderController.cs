using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public GameObject objectCollider;
    public Rigidbody2D playerRigidBody;

    private void Update()
    {
        if (playerRigidBody.velocity.y > 0)
        {
            objectCollider.SetActive(false);
        }
        else
        {
            objectCollider.SetActive(true);
        }
    }
}
