using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public int interactionID;  

    public void Interact()
    {
        Debug.Log("Interacción con objeto: " + gameObject.name);        
    }
}
