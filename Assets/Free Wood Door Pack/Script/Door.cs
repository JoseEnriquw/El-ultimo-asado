using UnityEngine;

public class PlayerDoorController : MonoBehaviour
{
    public bool open = false;
    public float smooth = 1.0f;
    public float doorOpenAngle = -90.0f;
    public float doorCloseAngle = 0.0f;
    public AudioSource audioSource;
    public AudioClip openDoorSound;
    public AudioClip closeDoorSound;

    private bool playerInRange = false; // Para verificar si el jugador está cerca

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verifica si el jugador toca en la puerta y presiona "E"
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }

        // Movimiento suave de la puerta
        float targetAngle = open ? doorOpenAngle : doorCloseAngle;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }

    void ToggleDoor()
    {
        // Cambia el estado de la puerta
        open = !open;

        // Selecciona el sonido adecuado
        audioSource.clip = open ? openDoorSound : closeDoorSound;
        audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        // Activa el rango si el jugador está cerca
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Desactiva el rango cuando el jugador se aleja
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}