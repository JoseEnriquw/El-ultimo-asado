using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;
    public void PlayGame()
    {
        loadingPanel.SetActive(true);
        StartCoroutine(EjecutarConDelay(3f,() => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); }));
    }

    IEnumerator EjecutarConDelay(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);

        // Código que se ejecuta después del delay
        Debug.Log($"Han pasado {seconds} segundos");
        action?.Invoke();
    }

    public void QuitGame()
    {
        #if UNITY_STANDALONE
        Debug.Log("Quit");
        Application.Quit();
        #endif
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void InstructionMenu()
    {
        SceneManager.LoadScene(6);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
