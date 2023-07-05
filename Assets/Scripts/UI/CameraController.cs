using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float minX = -1.5f;
    [SerializeField] private float maxX = 1.5f;

    private Vector2 dragStartPosition;
    private Vector3 cameraStartPosition;
    private bool isDragging = false;

    private void Update()
    {
        if (isDragging)
        {
            Vector2 dragDelta = Mouse.current.position.ReadValue() - dragStartPosition;
            float moveAmount = dragDelta.x * moveSpeed * Time.deltaTime;

            Vector3 newPosition = cameraStartPosition + new Vector3(moveAmount, 0f, 0f);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            transform.position = newPosition;
        }
    }

    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dragStartPosition = Mouse.current.position.ReadValue();
            cameraStartPosition = transform.position;
            isDragging = true;
        }
        else if (context.canceled)
        {
            isDragging = false;
        }
    }
}