using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerController : MonoBehaviour
{
    [Header("Configuration")]
    public float speed;
   
    [Header("Dependencies")]
    public Rigidbody2D rigidBody;
    public Animator animator;

    // Private
    protected bool isFacingRight = true;
    protected float horizontalMove;

    public virtual void OnMovement(InputAction.CallbackContext value) => horizontalMove = value.ReadValue<Vector2>().x;

    public virtual void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }
}
