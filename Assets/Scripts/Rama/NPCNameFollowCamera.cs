using UnityEngine;

public class NPCNameFollowCamera : MonoBehaviour
{
    [Tooltip("Referencia a la c�mara del personaje principal.")]
    public Camera playerCamera;

    private void Start()
    {
        // Si no se asigna una c�mara en el inspector, intenta buscar la c�mara principal
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        // Si a�n no encuentra la c�mara, muestra una advertencia
        if (playerCamera == null)
        {
            Debug.LogWarning("No se encontr� una c�mara principal. Asigna una c�mara al script.");
        }
    }

    private void Update()
    {
        if (playerCamera != null)
        {
            // Hacer que el objeto (el texto) siempre mire hacia la c�mara
            transform.LookAt(playerCamera.transform);

            // Opcional: Invertir la rotaci�n en el eje Y para que el texto no se vea al rev�s
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0);
        }
    }
}
