using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public int interactionID;  

    public void Interact()
    {
        Debug.Log("Interacci�n con objeto: " + gameObject.name);        
    }
}
