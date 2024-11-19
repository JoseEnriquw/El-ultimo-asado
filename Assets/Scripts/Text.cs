using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private bool guiOn = false;
    [SerializeField] private string text;
    [SerializeField] private TextMeshProUGUI contadorText; // Asegúrate de que esto esté asignado en el Inspector

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
        // Cambia el texto basado en la interacción
        text = interactionSuccessful ? _text : ""; // Si no hay interacción, se puede dejar vacío
        contadorText.text = text; // Actualiza el texto en la UI

        if (interactionSuccessful && destroy)
            Destroy(gameObject, 0.10f); // Destruye el objeto después de un tiempo si es necesario
    }    
   
}
