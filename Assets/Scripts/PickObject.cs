using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    [SerializeField] private GameObject hanPoint;
    private GameObject pickedObject = null;
    Inventory _inventory;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask layerMask;
    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {     

        if (pickedObject != null)
        {
            if(Input.GetKey(KeyCode.R) && pickedObject.CompareTag("ObjetoPickeable"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;              
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
            if (Input.GetKeyDown(KeyCode.E) && pickedObject.CompareTag("ItemInventory"))
            {
                UnequipItem();
            }
            
                CheckRaycastHit();
        }
       
    }

    private void CheckRaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, checkDistance, layerMask))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            Text textComponent = hit.collider.GetComponent<Text>();
            if (interactable != null)
            {
                ItemInventory itemInventory = pickedObject.GetComponent<ItemInventory>();
                if (itemInventory != null && itemInventory.Id == interactable.interactionID)
                {
                    Debug.Log("Interacción válida con " + interactable.gameObject.name);
                    interactable.Interact();
                    textComponent.UpdateTextBasedOnInteraction(true);
                    DestroyPickedObject();
                }
                else
                {
                    Debug.Log("El objeto equipado no puede interactuar con " + interactable.gameObject.name);
                    textComponent.UpdateTextBasedOnInteraction(false);
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.forward * checkDistance);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ObjetoPickeable"))
        {
            if(Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;    
                other.transform.position=hanPoint.transform.position;
                other.gameObject.transform.SetParent(hanPoint.gameObject.transform);
                pickedObject=other.gameObject;
            }
        }
        else if(other.gameObject.CompareTag("ItemInventory"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                GameObject Itempickedup = other.gameObject;

                ItemInventory item = Itempickedup.GetComponent<ItemInventory>();

                _inventory.AddItem(Itempickedup, item);
            }
        }
        
   }   

    public void SetPickedObject(GameObject item)
    {
        UnequipItem();
        EquipItem(item);
    }
    public void EquipItem(GameObject item)
    {
        if (item == null) return;

        var itemInventory = item.GetComponent<ItemInventory>();
        if (itemInventory != null)
        {
            itemInventory.equipped = true;
        }

        pickedObject = item;
        pickedObject.transform.position = hanPoint.transform.position;
        pickedObject.transform.SetParent(hanPoint.transform);
        pickedObject.GetComponent<Rigidbody>().useGravity = false;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        pickedObject.SetActive(true);
    }

    public void UnequipItem()
    {
        if (pickedObject == null) return;
        var itemInventory = pickedObject.GetComponent<ItemInventory>();
        if (itemInventory != null)
        {
            itemInventory.equipped = false;
        }

        pickedObject.SetActive(false);            
        pickedObject.transform.SetParent(null);   
        pickedObject = null;

        //pickedObject.GetComponent<Rigidbody>().useGravity = true;
        //pickedObject.GetComponent<Rigidbody>().isKinematic = false;
        //pickedObject.transform.SetParent(null);
        //pickedObject = null;
    }

    private void DestroyPickedObject()
    {
        if (pickedObject != null)
        {            
            pickedObject.transform.SetParent(null);
            
            Rigidbody rb = pickedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            
            Inventory inventory = FindObjectOfType<Inventory>();
            if (inventory != null)
            {
                ItemInventory itemInventory = pickedObject.GetComponent<ItemInventory>();
                inventory.RemoveItemFromInventory(itemInventory);  
            }

           
            Destroy(pickedObject);
            pickedObject.SetActive(false);
            pickedObject.transform.SetParent(null);            
            pickedObject = null; 
        }
    }


}
