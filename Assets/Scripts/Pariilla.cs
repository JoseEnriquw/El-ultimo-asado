using Assets.Scripts.GameManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pariilla : MonoBehaviour
{
    public TextMeshProUGUI contadorText;
    private int contadorParrilla = 0;
    private const int maxParrilla = 5;
    public bool Completed = false;
    [SerializeField] private GameObject Fire;
    LuzManager luzManager;
    // Start is called before the first frame update
    private void Start()
    {
        Fire.SetActive(false);
        luzManager = FindObjectOfType<LuzManager>();
    }
    public void IncrementarContador(bool firstInteraction = false)
    {
        if (firstInteraction)
        {
            ActualizarContadorText();
        }
        else
        if ((contadorParrilla < maxParrilla) && (!firstInteraction))
        {
            contadorParrilla++;
            ActualizarContadorText();
            if (contadorParrilla == 5)
            {
                Completed = true;
                ActualizarContadorText();
                PrenderFuego();

            }
        }
    }
    private void ActualizarContadorText()
    {
        contadorText.text = Completed ? "Habla con Josh" : $"Arma la Parrilla  {contadorParrilla}/{maxParrilla}";
    }

    public void PrenderFuego()
    {
        Fire.SetActive(true);       
        luzManager.DesactivarLuces();
        

    }

    public void CambioEscena()
    {
        GameManager.GetGameManager().NextScene();
    }

}
