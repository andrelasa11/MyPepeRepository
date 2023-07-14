using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 4f;
    [SerializeField] private float minX, maxX, minY, maxY;

    [Header("Audio")]
    public AudioSource audioSource;

    //private
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float moveSpeed;
    private bool isFacingRight = true;

    private void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        SetNewRandomTarget();
        audioSource.Play();
    }

    private void Update()
    {
        if (isMoving)
        {
            if (Vector3.SqrMagnitude(transform.position - targetPosition) < 0.1f * 0.1f)
            {
                isMoving = false;
                SetNewRandomTarget();
            }
            else
            {
                MoveTowardsTarget();
            }
        }
    }

    private void SetNewRandomTarget()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);

        UpdateFacingDirection();

        isMoving = true;
    }

    private void UpdateFacingDirection()
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = targetRotation;

        if (direction.x > 0 && !isFacingRight || direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void MoveTowardsTarget() => transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}