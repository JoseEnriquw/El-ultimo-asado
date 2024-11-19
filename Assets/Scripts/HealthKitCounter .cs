using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthKitCounter : MonoBehaviour
{
    public TextMeshProUGUI contadorText;
    private int contadorBotiquin = 0;
    private const int maxBotiquines = 5; 
    public bool Completed = false;
    

    private void Start()
    {
        //ActualizarContadorText();
        
    }

    public void IncrementarContador(bool firstInteraction=false)
    {
        if (firstInteraction)
        {
            ActualizarContadorText();
        }
        else
        if ((contadorBotiquin < maxBotiquines) && (!firstInteraction))
        {
            contadorBotiquin++;
            ActualizarContadorText();
            if (contadorBotiquin == 5)
            {

                Completed = true;
                
                ActualizarContadorText();

            }
        }
    }

    private void ActualizarContadorText()
    {
        contadorText.text= Completed ? "Volver con Judy":$"Completa el botiquin {contadorBotiquin}/{maxBotiquines}";
        // Al cambiar de escena se pierde la referencia de a cual TextMeshProUGUI estamos enviando mensaje
        // Esto es porque hay varios incluso en una misma escena.
        //SOLUCIONADO!!! enviar contadorText.text a UIManager.
        var text = contadorText.text;
        UIManager.GetUIManager().SetTarea(text);
    }
}
