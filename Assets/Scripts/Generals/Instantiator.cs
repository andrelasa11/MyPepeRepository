using UnityEngine;

public class Instantiator : MonoBehaviour
{
    [Header("Conditions")]
    public bool instantiateOnStart = false;
    public bool objIsAChild = false;

    [Header("Dependencies")]
    public GameObject prefab;

    private void Start()
    {
        if(instantiateOnStart)
        {
            DoInstantiate();
        }
    }

    public void DoInstantiate()
    {
        if(objIsAChild)
        {
            Instantiate(prefab, transform, false);
        }
        else
        {
            Instantiate(prefab, transform.position, transform.rotation);
        }
    }
}
