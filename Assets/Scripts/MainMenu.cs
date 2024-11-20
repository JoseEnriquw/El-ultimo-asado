using Assets.Scripts.GameManager;
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
        StartCoroutine(EjecutarConDelay(5f,() => { GameManager.GetGameManager().NextScene(); }));
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

    public void BacktoMenu()
    {
        SceneManager.LoadScene(0);
    }
}
