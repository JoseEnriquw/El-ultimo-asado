using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinternaController : MonoBehaviour
{
    public Light linterna; // Referencia a la luz de la linterna
    private bool isLinternaOn = false; // Estado de la linterna

    void Start()
    {
        // Aseg�rate de que la linterna est� apagada al inicio
        if (linterna != null)
        {
            linterna.enabled = false;
        }
    }

    void Update()
    {
        // Comprueba si se presiona la tecla "F"
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLinterna(); // Llama a la funci�n para encender/apagar la linterna
        }
    }

    public void ToggleLinterna()
    {
        // Cambia el estado de la linterna
        isLinternaOn = !isLinternaOn;
        linterna.enabled = isLinternaOn; // Enciende o apaga la luz
    }
}