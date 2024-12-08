using Assets.Scripts.Character;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager gameManager;
        public Action<EnemyStatesEnum> OnChangeEnemyState;
        public Action<GameObject> OnPickUpObject;
        public Action OnPlayEnemyScream;
        public Action<bool> OnChangePlayerInput;
        private bool ExecutedCoroutine;

        public static GameManager GetGameManager() => gameManager; 

        private void Awake()
        {
            if (gameManager != null)
            {
                Destroy(gameObject);
                return;
            }
            
            gameManager = this;
            DontDestroyOnLoad(this);
        }

        public void NextScene()
        {
            var sceneNumber = SceneManager.GetActiveScene().buildIndex;
            var nextScene = sceneNumber + 1;
            if (sceneNumber != 4)
            {
                if (ExecutedCoroutine) return; // Evita llamadas repetidas
                ExecutedCoroutine = true;
                UIManager.GetUIManager().ChangeLoadingBackGround(sceneNumber);

                // Suscribirse al evento una sola vez
                SceneManager.sceneLoaded += (scene, loadMode) => { UIManager.GetUIManager().HideLoadingPanel(); };

                // Inicia la carga de la escena con un delay
                StartCoroutine(EjecutarConDelay(5f, () =>
                {
                    SceneManager.LoadScene(nextScene);
                }));
            }
            else
            {
                UIManager.GetUIManager().HideTaskPanel();
                SceneManager.LoadScene(nextScene);
            }
        }

        public int GetSceneNumber()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public void ChangeEnemyState(EnemyStatesEnum newState)
        {
            OnChangeEnemyState?.Invoke(newState);
        }  

        public void PickUpObject(GameObject gameObject)
        {
            OnPickUpObject?.Invoke(gameObject);
        }    

        public void PlayEnemyScream()
        {
            OnPlayEnemyScream?.Invoke();
        }

        public void SetEnablePlayerInput(bool enable)
        {
            OnChangePlayerInput?.Invoke(enable);
            if (enable) Cursor.lockState = CursorLockMode.Locked;
            else Cursor.lockState = CursorLockMode.None;
        }

        IEnumerator EjecutarConDelay(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);

            // Código que se ejecuta después del delay
            Debug.Log($"Han pasado {seconds} segundos");
            action?.Invoke();
            ExecutedCoroutine=false;
        }
    }
}
