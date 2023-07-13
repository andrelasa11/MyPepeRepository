using UnityEngine;

public class FlyMovement : MonoBehaviour
{
    public float minMoveSpeed = 2f; // Velocidade mínima de movimento das moscas
    public float maxMoveSpeed = 4f; // Velocidade máxima de movimento das moscas

    public float minX, maxX, minY, maxY; // Limites do mapa

    private Vector3 targetPosition; // Posição de destino atual
    private bool isMoving = false; // Flag para controlar o movimento

    private bool isFacingRight = true; // Flag para indicar a direção da mosca
    private float moveSpeed;

    public AudioSource audioSource;

    private void Start()
    {
        // Definir a velocidade inicial aleatória
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);

        // Iniciar o movimento
        SetNewRandomTarget();

        audioSource.Play();
    }

    private void Update()
    {
        // Verificar se a mosca atingiu o destino
        if (isMoving && Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
            SetNewRandomTarget();
        }

        // Se estiver se movendo, atualizar a posição
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void SetNewRandomTarget()
    {
        // Gerar uma nova posição aleatória dentro dos limites do mapa
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);

        // Calcular a nova direção
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Verificar a direção da mosca e ajustar a escala (flip) se necessário
        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }

        // Iniciar o movimento
        isMoving = true;
    }

    private void Flip()
    {
        // Inverter a escala da mosca para virar para o outro lado
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        // Alternar a direção da mosca
        isFacingRight = !isFacingRight;
    }
}
