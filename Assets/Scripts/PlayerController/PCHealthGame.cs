using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCHealthGame : PlayerController
{
    #region Jump Variables
    [Header("Jump Settings")]
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float jumpDuration = 0.8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float landingDelay = 0.5f;

    private bool isJumping = false;
    private int jumpsRemaining;
    #endregion

    #region Shooting Variables
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;

    private float nextFireTime;
    #endregion


    #region Unity Callbacks

    private void Start()
    {
        InitializeJumpSettings();
        InitializeShootingSettings();
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(horizontalMove * speed, rigidBody.velocity.y);

        if (horizontalMove != 0)
        {
            animator.SetBool("Run", true);
        }
        else animator.SetBool("Run", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsGround", true);
            jumpsRemaining = maxJumps;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsGround", false);
        }
    }
    #endregion

    #region Jumping
    private void InitializeJumpSettings()
    {
        jumpsRemaining = maxJumps;

        // Configuração da curva de animação
        jumpCurve = new AnimationCurve(
            new Keyframe(0f, 0f),      // Tempo inicial, força inicial (0)
            new Keyframe(0.2f, 0.5f),    // Tempo intermediário, força máxima (1)
            new Keyframe(0.5f, 1f),    // Tempo intermediário, força máxima (1)
            new Keyframe(1f, 0f)       // Tempo final, força final (0)
        );
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && (CanJump()))
        {
            StartCoroutine(JumpRoutine());
            jumpsRemaining--;
            AudioManager.Instance.PlaySound("Frog");
            AudioManager.Instance.PlaySound("Jump");
        }
    }

    private IEnumerator JumpRoutine()
    {
        animator.SetFloat("Height", rigidBody.velocity.y);
        isJumping = true;
        float elapsedTime = 0f;
        float initialVelocity = rigidBody.velocity.y;

        while (elapsedTime < jumpDuration)
        {
            float normalizedTime = elapsedTime / jumpDuration;
            float jumpForceMultiplier = jumpCurve.Evaluate(normalizedTime);

            float targetVelocity = jumpForce * jumpForceMultiplier;
            float velocityDiff = targetVelocity - initialVelocity;

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, initialVelocity + velocityDiff);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f); // Stop vertical velocity at the peak of the jump
        isJumping = false;

        yield return new WaitForSeconds(landingDelay);
    }

    private bool CanJump()
    {
        return jumpsRemaining > 0 && !isJumping;
    }
    #endregion

    #region Shooting
    private void InitializeShootingSettings()
    {
        nextFireTime = 0f;
    }

    public void OnShooting(InputAction.CallbackContext context)
    {
        if (context.started && (Time.time >= nextFireTime))
        {
            animator.SetTrigger("Shoot");
            AudioManager.Instance.PlaySound("LaserShoot");
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
    }
    #endregion
}