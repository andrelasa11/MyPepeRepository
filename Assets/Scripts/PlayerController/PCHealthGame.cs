using UnityEngine;
using UnityEngine.InputSystem;

public class PCHealthGame : PlayerController
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    //private Rigidbody2D rb;
    private bool isJumping = false;
    private bool isJumpBuffered = false;
    private float coyoteTimeRemaining = 0f;
    private float jumpBufferTimeRemaining = 0f;

    private void Update()
    {
        // Jump buffering
        if (isJumpBuffered && jumpBufferTimeRemaining > 0f)
        {
            Jump();
            jumpBufferTimeRemaining = 0f;
            isJumpBuffered = false;
        }
        jumpBufferTimeRemaining -= Time.deltaTime;

        // Coyote time
        if (coyoteTimeRemaining > 0f)
        {
            coyoteTimeRemaining -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);

        if (horizontalMove != 0)
        {
            animator.SetBool("Run", true);
        }
        else animator.SetBool("Run", false);

        if (horizontalMove < 0 && !isFacingRight)
        {
            Flip();
        }

        if (horizontalMove > 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Check if the player is grounded or within the coyote time window
            if (IsGrounded() || coyoteTimeRemaining > 0f)
            {
                Jump();
            }
            else
            {
                // Buffer the jump input for later use
                isJumpBuffered = true;
                jumpBufferTimeRemaining = jumpBufferTime;
            }
        }
    }

    private void Jump()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        isJumping = true;
        coyoteTimeRemaining = coyoteTime;
    }

    private bool IsGrounded()
    {
        // Define o ponto de origem do raycast logo abaixo do jogador
        Vector2 origin = new Vector2(transform.position.x, transform.position.y - 0.1f);

        // Define o tamanho e direção do raycast
        Vector2 direction = Vector2.down;
        float distance = 0.2f;

        // Executa o raycast para verificar colisões com layers específicas (defina suas layers apropriadas)
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, groundLayer);

        // Verifica se o raycast atingiu algum objeto e retorna true se estiver no chão
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}

