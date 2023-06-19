using UnityEngine;

public class PCInfinityJump : PlayerController
{
    [Header("Exclusive Configuration")]
    [SerializeField] private float jumpForce;


    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);

        if (rigidBody.velocity.y < 0.5)
        {
            animator.SetBool("IsFalling", true);
        }
        
        else
        {
            animator.SetBool("IsFalling", false);
        }

        if (horizontalMove > 0 && !isFacingRight)
        {
            Flip();
        }

        if (horizontalMove < 0 && isFacingRight)
        {
            Flip();
        }

    }

    public void DoJump()
    {
        if(rigidBody.velocity.y < -1.5f)
        {                       
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);

            AudioManager.Instance.PlayJump();
        }        
    }

    public void OnDeath()
    {
        GCInfinityJump.Instance.SetPlayerLives(-1);
        AudioManager.Instance.PlayDeath();
    }
}
