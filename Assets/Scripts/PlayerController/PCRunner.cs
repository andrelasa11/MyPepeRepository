using UnityEngine;
using UnityEngine.InputSystem;

public class PCRunner : PlayerController
{
    #region Jump Configuration

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private int maxJumpCount = 2;
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

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Obter referência ao componente Animator
    }

    private void Update()
    {
        if (jumpCount >= maxJumpCount)
        {
            isJumping = false;
        }
        else if (isJumping && rigidBody.velocity.y <= 0)
        {
            isJumping = false;
            jumpCount++;
        }

        // Atualizar o parâmetro "IsJumping" da animação
        if (jumpTriggered && (isGrounded || jumpCount < maxJumpCount))
        {
            Jump();
            animator.SetTrigger("IsJumping");
            jumpTriggered = false;
        }

        // Atualizar o parâmetro "NoChao" da animação
        animator.SetBool("NoChao", isGrounded);

        // Verificar se tocou no chão para retornar para a animação de idle
        if (isGrounded && !isJumping)
        {
            animator.SetTrigger("TocouNoChao");
        }
    }

    private void FixedUpdate()
    {
        if (!isJumping)
            ApplyGravity();
    }

    #endregion

    #region Player Actions

    private void Jump()
    {
        if (isGrounded || jumpCount < maxJumpCount)
        {
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            jumpCount++;
        }
    }

    private void ApplyGravity()
    {
        Vector2 velocity = rigidBody.velocity;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rigidBody.velocity = velocity;
    }

    #endregion

    #region Collision Handling

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;
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