using System.Collections.Generic;
using UnityEngine;

public class PetFoodController : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterGrabber characterGrabber;
    [SerializeField] private FixedJoint2D fixedJoint2D;
    
    //private
    private GameObject mouseJoint;
    private int spriteIndex;


    // Start is called before the first frame update
    void Start()
    {
        spriteIndex = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[spriteIndex];

        mouseJoint = GameObject.Find("MouseJoint");
        fixedJoint2D.connectedBody = mouseJoint.GetComponent<Rigidbody2D>();
        characterGrabber.target = mouseJoint.transform;
    }
}
