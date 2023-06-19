using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime = 1f;

    public void DoLoadScene(string scene) => StartCoroutine(LoadLevel(scene));

    IEnumerator LoadLevel(string scene)
    {
        if(transition != null)
        {
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            Time.timeScale = 1.0f;
        }
        

        

        SceneManager.LoadScene(scene);
    }
}
