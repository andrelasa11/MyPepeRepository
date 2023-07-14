using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Image fadeImage; // Referência para o componente Image no objeto "Fader"
    public float fadeSpeed = 1.5f; // Velocidade da transição

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void DoLoadScene(string sceneName)
    {
        if (fadeImage != null)
        {
            Debug.Log("Loading...");
            Time.timeScale = 1.0f;
            StartCoroutine(FadeOut(sceneName));
        }
        else
        {
            Debug.Log("Loading");
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadWithoutFade(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true); // Ativa o objeto "Fader"

        // Define a cor inicial da imagem para opaco
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);

        // Fade-in gradual
        while (fadeImage.color.a > 0)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - (Time.deltaTime * fadeSpeed));
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // Desativa o objeto "Fader"
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true); // Ativa o objeto "Fader"

        // Define a cor inicial da imagem para transparente
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);

        // Fade-out gradual
        while (fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + (Time.deltaTime * fadeSpeed));
            yield return null;
        }

        Time.timeScale = 1.0f;
        // Carrega a cena especificada
        SceneManager.LoadScene(sceneName);
    }
}
