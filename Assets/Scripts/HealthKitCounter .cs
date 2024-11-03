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

    private void Start()
    {       
        ActualizarContadorText();
    }

    public void IncrementarContador()
    {
        if (contadorBotiquin < maxBotiquines)
        {
            contadorBotiquin++;
            ActualizarContadorText();
        }
    }

    private void ActualizarContadorText()
    {
        contadorText.text= $"Completa el botiquin {contadorBotiquin}/{maxBotiquines}";    
    }
}
