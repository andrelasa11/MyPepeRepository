using UnityEngine;
using UnityEngine.U2D.IK;

public class GrabbableCharacter : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private FixedJoint2D springJoint;
    [SerializeField] private IKManager2D iKManager2D;
    [SerializeField] private Animator animator;

    //private
    private Rigidbody2D rigidBody;
    private Vector3 initialPosition;
    private Vector3 initialRotation;

    private void Awake() => rigidBody = GetComponent<Rigidbody2D>();

    private void Start()
    {
        initialRotation = transform.eulerAngles;
        initialPosition = transform.position;
    }

    public void Grabble(Vector2 anchor)
    {
        rigidBody.isKinematic = false;
        springJoint.anchor = anchor;

        if (springJoint != null)
        {
            springJoint.enabled = true;
        }

        if(iKManager2D != null)
        {
            iKManager2D.enabled = false;
        }
        
        if(animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
            animator.enabled = false;
        }
        
    }

    public void Ungrab()
    {
        springJoint.enabled = false;

        if(iKManager2D != null)
        {
            iKManager2D.enabled = true;
        }
        
        if(animator != null)
        {
            animator.enabled = true;
        }        

        transform.position = initialPosition;
        transform.eulerAngles = initialRotation;

        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = 0;
        rigidBody.isKinematic = true;
    }
}
