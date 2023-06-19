using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool isDraging;
    private Camera mainCamera;


    public static Vector2 screenMousePosition;
    public static Vector2 worldMousePosition;

    #region "Awake/Start/Update"

    private void Awake() => mainCamera = Camera.main;

    #endregion

    #region "PlayerInput"
    public void OnLeftMouseClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ClickAction();
        }
        else if (context.canceled)
        {
            isDraging = false;
        }
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 actualMousePos = context.ReadValue<Vector2>();

            screenMousePosition = actualMousePos;
            worldMousePosition = mainCamera.ScreenToWorldPoint(actualMousePos);
        }
    }
    #endregion

    private void ClickAction()
    {
        Ray ray = mainCamera.ScreenPointToRay(screenMousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.TryGetComponent(out IDraggable draggable))
        {
            StartCoroutine(DragUpdate(draggable));
        }
    }

    private IEnumerator DragUpdate(IDraggable draggable)
    {
        isDraging = true;
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        Debug.Log("Drag Start");
        draggable.OnDragStart();
        while (isDraging)
        {
            Debug.Log("Drag Update");
            draggable.OnDragUpdate();
            yield return waitForFixedUpdate;
        }
        Debug.Log("Drag End");
        draggable.OnDragEnd();
    }
}
