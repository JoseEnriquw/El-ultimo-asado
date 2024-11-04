using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private bool guiOn = false;
    [SerializeField] private string text;
    [SerializeField] private TextMeshProUGUI contadorText; // Aseg�rate de que esto est� asignado en el Inspector

    private void Awake()
    {
        ResetText();
    }

    private void Start()
    {
        
        if (contadorText == null)
        {
            var gameObject=GameObject.Find("TareaNombretxt");
            contadorText= gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    private void ResetText()
    {
        text = "";
        if (contadorText != null)
            contadorText.text = text;
    }
    private void OnTriggerEnter(Collider other)
    {
        guiOn = true;
       // UpdateTextBasedOnInteraction(true); // Actualiza el texto al entrar
    }

    private void OnTriggerExit(Collider other)
    {
        guiOn = false;
       // UpdateTextBasedOnInteraction(false); // Actualiza el texto al salir
    }

    public void UpdateTextBasedOnInteraction(bool interactionSuccessful, string _text, bool destroy=false)
    {
        // Cambia el texto basado en la interacci�n
        text = interactionSuccessful ? _text : ""; // Si no hay interacci�n, se puede dejar vac�o
        contadorText.text = text; // Actualiza el texto en la UI

        if (interactionSuccessful && destroy)
            Destroy(gameObject, 0.10f); // Destruye el objeto despu�s de un tiempo si es necesario
    }    
   
}
