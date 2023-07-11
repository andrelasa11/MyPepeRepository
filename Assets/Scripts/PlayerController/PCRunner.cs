using UnityEngine;
using UnityEngine.InputSystem;

public class PCRunner : PlayerController
{
    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float jumpDescentForce = 200f;
    [SerializeField] private int extraJumps = 1;

    private int jumpCount = 0;
    private bool isGrounded;
    //private bool isJumping;

    #region Input Handling
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && (isGrounded || jumpCount < extraJumps))
        {
            Jump();
        }
    }
    #endregion

    #region MonoBehaviour Methods
    private void Update()
    {
        CheckJumpState();
        //UpdateAnimator();
    }

    private void FixedUpdate()
    {
        ApplyJumpDescent();
        animator.SetFloat("Height", rigidBody.velocity.y);
    }
    #endregion

    #region Main Methods
    private void Jump()
    {
        if (jumpCount >= extraJumps)
        {
            return;
        }

        animator.SetBool("isGround", false);
        AudioManager.Instance.PlaySound("Jump");
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
        rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //isJumping = true;
        jumpCount++;
    }
   
    private void CheckJumpState()
    {
        if (isGrounded)
        {
            //isJumping = false;
            jumpCount = 0;
        }
    }    

    private void ApplyJumpDescent()
    {
        if (rigidBody.velocity.y < 0)
        {
            Vector2 velocity = rigidBody.velocity;
            velocity.y -= jumpDescentForce * Time.fixedDeltaTime;
            rigidBody.velocity = velocity;
        }
    }

    /*private void UpdateAnimator()
    {
        if (isGrounded && !isJumping)
        {
            animator.SetTrigger("TocouNoChao");
        }
    }*/
    #endregion

    #region Collision Handling
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isGrounded)
        {
            isGrounded = true;
            jumpCount = 0;
            animator.SetBool("isGround", true);
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