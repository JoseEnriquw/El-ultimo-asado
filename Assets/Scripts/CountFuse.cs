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
            if (contadorFuse == 5)
            {
                Completed = true;

                ActualizarContadorText();
            }
        }
    }

    private void ActualizarContadorText()
    {
        contadorText.text = Completed ? "Buscar a Judy" : $"Busca los fusibles dentro de la casa {contadorFuse}/{maxFuse}";
    }
}
