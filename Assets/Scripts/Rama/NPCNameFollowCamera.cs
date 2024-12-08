using UnityEngine;

public class NPCNameFollowCamera : MonoBehaviour
{
    [Tooltip("Referencia a la cámara del personaje principal.")]
    public Camera playerCamera;

    private void Start()
    {
        // Si no se asigna una cámara en el inspector, intenta buscar la cámara principal
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        // Si aún no encuentra la cámara, muestra una advertencia
        if (playerCamera == null)
        {
            Debug.LogWarning("No se encontró una cámara principal. Asigna una cámara al script.");
        }
    }

    private void Update()
    {
        if (playerCamera != null)
        {
            // Hacer que el objeto (el texto) siempre mire hacia la cámara
            transform.LookAt(playerCamera.transform);

            // Opcional: Invertir la rotación en el eje Y para que el texto no se vea al revés
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
        }
    }
}
