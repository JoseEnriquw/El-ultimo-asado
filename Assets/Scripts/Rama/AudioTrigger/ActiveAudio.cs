using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //Asegurarnos de que el objeto tenga un componente AudioSource 
public class ActivarSonidoTrigger : MonoBehaviour
{
    public LayerMask layerToDetect; // Capa a detectar desde el Inspector
    private AudioSource audioSource; // Referencia al componente AudioSource

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtiene el componente AudioSource
    }

    void OnTriggerEnter(Collider other) //Si entro en el trigger
    {
        // Comprueba si el objeto que entra en el trigger es el "Player" o está en el layer especificado
        if (other.CompareTag("Player") || ((1 << other.gameObject.layer) & layerToDetect) != 0)
        {
            // Reproduce el sonido si el audio no está ya sonando
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}

