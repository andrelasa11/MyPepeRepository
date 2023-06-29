using UnityEngine;
using UnityEngine.InputSystem;

public class PCRunner : PlayerController
{
    #region Jump Configuration

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDescentForce = 200f;
    [SerializeField] private int extraJumps = 1;
    private int jumpCount = 0;
    private bool isGrounded;
    private bool isJumping;
    private bool jumpTriggered;

    #endregion

    #region Input Handling

    public void OnJump(InputAction.CallbackContext value)
    {
        if (value.started)
            jumpTriggered = true;
    }

    #endregion

    #region MonoBehaviour Methods

    private void Update()
    {
        CheckJumpState();

        if (jumpTriggered && (isGrounded || jumpCount < extraJumps))
        {
            Jump();
            animator.SetTrigger("IsJumping");
            jumpTriggered = false;
        }

        animator.SetBool("NoChao", isGrounded);

        if (isGrounded && !isJumping)
        {
            animator.SetTrigger("TocouNoChao");
        }
    }

    private void FixedUpdate()
    {
        if (isJumping && rigidBody.velocity.y < 0)
        {
            ApplyJumpDescent();
        }
    }

    #endregion

    #region Player Actions

    private void CheckJumpState()
    {
        if (isGrounded)
        {
            isJumping = false;
            jumpCount = 0;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (jumpCount < extraJumps)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            jumpCount++;
            AudioManager.Instance.PlayJump();
        }
    }

    private void ApplyJumpDescent()
    {
        Vector2 velocity = rigidBody.velocity;
        velocity.y -= jumpDescentForce * Time.fixedDeltaTime;
        rigidBody.velocity = velocity;
    }

    #endregion

    #region Collision Handling

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    #endregion
}