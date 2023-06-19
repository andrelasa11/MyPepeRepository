using UnityEngine;

public class CharacterGrabber : MonoBehaviour, IDraggable
{
    [Header("Configuration")]
    [SerializeField] private float virtualDragSpeed;

    [Header("Dependencies")]
    public Transform target;
    [SerializeField] private GrabbableCharacter grabbableCharacter;

    //private
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private float initialDistance;

    private void Awake() => mainCamera = Camera.main;

    public void OnDragStart()
    {
        if(grabbableCharacter != null)
        {
            target.position = InputManager.worldMousePosition;

            initialDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            Vector2 anchor = (Vector2)grabbableCharacter.transform.position - InputManager.worldMousePosition;
            grabbableCharacter.Grabble(anchor);
            target.localPosition = transform.position;
        }
        else
        {
            Debug.Log("No Grabbable");
        }
    }

    public void OnDragUpdate()
    {
        if(grabbableCharacter != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(InputManager.screenMousePosition);
            target.position = Vector3.SmoothDamp(target.position, ray.GetPoint(initialDistance), ref velocity, virtualDragSpeed);
        }
        else
        {
            Debug.Log("No Grabbable");
        }

    }

    public void OnDragEnd()
    {
        if(grabbableCharacter != null)
        {
            grabbableCharacter.Ungrab();
        }
        else
        {
            Debug.Log("No Grabbable");
        }
    }
}
