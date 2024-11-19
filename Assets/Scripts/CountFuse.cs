using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountFuse : MonoBehaviour
{
    public TextMeshProUGUI contadorText;
    private int contadorFuse = 0;
    private const int maxFuse = 2;
    public bool Completed = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void IncrementarContador(bool firstInteraction = false)
    {
        if (firstInteraction)
        {
            ActualizarContadorText();
        }
        else
        if ((contadorFuse < maxFuse) && (!firstInteraction))
        {
            contadorFuse++;
            ActualizarContadorText();
            if (contadorFuse == 2)
            {
                Completed = true;

                ActualizarContadorText();
            }
        }
    }

    private void ActualizarContadorText()
    {
        contadorText.text = Completed ? "Volver al Panel Electrico" : $"Busca los fusibles dentro de la casa {contadorFuse}/{maxFuse}";
        // Al cambiar de escena se pierde la referencia de a cual TextMeshProUGUI estamos enviando mensaje
        // Esto es porque hay varios incluso en una misma escena.
        //SOLUCIONADO!!! enviar contadorText.text a UIManager.
        var text = contadorText.text;
        UIManager.GetUIManager().SetTarea(text);
    }
}
