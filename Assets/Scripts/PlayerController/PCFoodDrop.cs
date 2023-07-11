using UnityEngine;

public class PCFoodDrop : PlayerController
{   
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

    public void DoEat()
    {
        AudioManager.Instance.PlaySound("Frog");
        animator.SetTrigger("Eat");
    }
}
