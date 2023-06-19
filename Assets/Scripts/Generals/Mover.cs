using UnityEngine;

public class Mover : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private GameType gameType;

    // Start is called before the first frame update
    void Start()
    {
        switch(gameType)
        {
            case GameType.InfinityJump:
                rigidBody.velocity = (Vector2.down * GCInfinityJump.Instance.platformSpeed);
                break;

            case GameType.FoodDrop:
                rigidBody.velocity = (Vector2.down * GCFoodDrop.Instance.foodSpeed);
                break;
        }

    } 
}
