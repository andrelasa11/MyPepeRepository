using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingSceneController : MonoBehaviour
{
    public Slider slider;
    [SerializeField] private string petScene;

    void Start()
    {
        // Inicialize o valor mínimo e máximo do slider
        slider.minValue = 0f;
        slider.maxValue = 1f;

        // Inicie a rotina de carregamento em tempo real
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(petScene); // Substitua "NomeDaCena" pelo nome da sua cena de jogo

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f); // Obtenha o progresso do carregamento (0.0 a 1.0)

            // Atualize o valor do slider com base no progresso do carregamento
            slider.value = progress;

            yield return null;
        }
    }
}
