using UnityEngine;

public class GroundController : MonoBehaviour
{
    //hidden
    private Vector3 generationPosition;
    private bool isGenerating = true;

    private void Start()
    {
        generationPosition = new Vector3(this.transform.position.x + 87.9f, this.transform.position.y, this.transform.position.z);
    }

    public void GenerateGround()
    {
        if(isGenerating)
        {
            GroundSpawnerController.Instance.GenerateGround(generationPosition);
            isGenerating = false;
        }        
    }    
}
