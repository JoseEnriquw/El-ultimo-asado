using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    [SerializeField] private bool GuiOn=false;
    [SerializeField] private string text ;
    [SerializeField] private Rect BoxSize = new Rect(0, 0, 200, 100);
    [SerializeField] private GUISkin customSkin;

    public void TriggerEnter()
    {
        GuiOn = true;
    }
    public void TriggerExit()
    {
        GuiOn = false;
    }
    private void OnTriggerEnter(Collider other)
    {       
        GuiOn = true;        
        
    }
    private void OnTriggerExit(Collider other)
    {
        GuiOn = false;
    }
    public void UpdateTextBasedOnInteraction(bool interactionSuccessful)
    {
        
        text = interactionSuccessful ? "BIEN ECHO ME VOY" : text;
        GuiOn = !interactionSuccessful;

        if(interactionSuccessful)
            Destroy(gameObject, 0.10f);
    }
    public void OnGUI()
    {

        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn == true)
        {
          
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
            
            GUI.Label(BoxSize, text);

            GUI.EndGroup();

        }

    }
}
