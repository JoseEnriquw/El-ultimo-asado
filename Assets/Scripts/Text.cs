using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private bool guiOn = false;
    [SerializeField] private string text;
    [SerializeField] private TextMeshProUGUI contadorText; // Asegúrate de que esto esté asignado en el Inspector

    private void OnTriggerEnter(Collider other)
    {
        guiOn = true;
        UpdateTextBasedOnInteraction(true); // Actualiza el texto al entrar
    }

    private void OnTriggerExit(Collider other)
    {
        guiOn = false;
        UpdateTextBasedOnInteraction(false); // Actualiza el texto al salir
    }

    public void UpdateTextBasedOnInteraction(bool interactionSuccessful)
    {
        // Cambia el texto basado en la interacción
        text = interactionSuccessful ? "BIEN ECHO ME VOY" : ""; // Si no hay interacción, se puede dejar vacío
        contadorText.text = text; // Actualiza el texto en la UI

        if (interactionSuccessful)
            Destroy(gameObject, 0.10f); // Destruye el objeto después de un tiempo si es necesario
    }
    //[SerializeField] private bool GuiOn=false;
    //[SerializeField] private string text ;
    //[SerializeField] private Rect BoxSize = new Rect(0, 0, 200, 100);
    //[SerializeField] private GUISkin customSkin;
    //public TextMeshProUGUI contadorText;

    //public void TriggerEnter()
    //{
    //    GuiOn = true;
    //}
    //public void TriggerExit()
    //{
    //    GuiOn = false;
    //}
    //private void OnTriggerEnter(Collider other)
    //{       
    //    GuiOn = true;        

    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    GuiOn = false;
    //}
    //public void UpdateTextBasedOnInteraction(bool interactionSuccessful)
    //{

    //    text = interactionSuccessful ? "BIEN ECHO ME VOY" : text;
    //    GuiOn = !interactionSuccessful;

    //    if(interactionSuccessful)
    //        Destroy(gameObject, 0.10f);
    //}
    //public void OnGUI()
    //{

    //    if (customSkin != null)
    //    {
    //        GUI.skin = customSkin;
    //    }

    //    if (GuiOn == true)
    //    {

    //        GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));

    //        GUI.Label(BoxSize, text);

    //        GUI.EndGroup();

    //    }

    //}
}
