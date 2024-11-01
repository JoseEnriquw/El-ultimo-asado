using UnityEngine;

namespace Assets.Scripts.GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager gameManager;
        private int scene;

        private void Awake()
        {
            if (gameManager != null)
            {
                Destroy(gameObject);
                return;
            }
            
            gameManager = this;
        }
    }
}
