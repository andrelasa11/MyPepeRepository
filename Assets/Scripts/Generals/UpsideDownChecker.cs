using System.Collections;
using UnityEngine;

public class UpsideDownChecker : MonoBehaviour
{
    [SerializeField] private float checkingTime;
    [SerializeField] private GameObject player;

    //private
    private float playerGravity;
    private SpriteRenderer[] spritesToTint;
    private bool shouldntStop = false;

    private void Start()
    {
        playerGravity = player.GetComponent<Rigidbody2D>().gravityScale;
        spritesToTint = player.GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Limits"))
        {
            StartCoroutine(nameof(RespawnPlayer));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Limits") && shouldntStop == false)
        {
            StopCoroutine(nameof(RespawnPlayer));
        }
    }

    private IEnumerator RespawnPlayer()
    {        
        yield return new WaitForSeconds(checkingTime);

        shouldntStop = true;

        foreach (SpriteRenderer spriteRenderer in spritesToTint)
        {
            spriteRenderer.enabled = false;
        }

        player.transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z), new Quaternion(0, 0, 0, 0));
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        for (float i = 0; i < 3; i += 0.1f)
        {
            foreach (SpriteRenderer spriteRenderer in spritesToTint)
            {
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }

            yield return new WaitForSeconds(0.1f);
        }
                
        foreach (SpriteRenderer spriteRenderer in spritesToTint)
        {
            spriteRenderer.enabled = true;
        }

        player.GetComponent<Rigidbody2D>().gravityScale = playerGravity;

        shouldntStop = false;

        StopCoroutine(nameof(RespawnPlayer));
    }

}
