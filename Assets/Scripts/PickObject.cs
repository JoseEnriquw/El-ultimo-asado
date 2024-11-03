using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class PickObject : MonoBehaviour
{
    [SerializeField] private GameObject hanPoint;
    private GameObject pickedObject = null;
    Inventory _inventory;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform pjtransform;
    private LayerMask  maskCajones;
    [SerializeField] private GameObject _UI;
    bool ultimoreconocido=false;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        maskCajones = LayerMask.GetMask("Cajones");
    }

    void Update()
    {
        pjtransform = gameObject.GetComponentInParent<Transform>();
        if (pickedObject != null)
        {
            if (Input.GetKey(KeyCode.R) && pickedObject.CompareTag("ObjetoPickeable"))
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
    #region RAYCAST
    private void CheckRaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, pjtransform.forward, out hit, checkDistance, layerMask))
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
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.forward * checkDistance);
    }
    #endregion
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("ObjetoPickeable") && (maskCajones != (maskCajones | (1 << other.gameObject.layer))))
        {
            ultimoreconocido = true;
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                other.GetComponent<Rigidbody>().useGravity = false;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.position = hanPoint.transform.position;
                other.gameObject.transform.SetParent(hanPoint.gameObject.transform);
                pickedObject = other.gameObject;
            }
        }
        else if (other.gameObject.CompareTag("ItemInventory"))
        {
            ultimoreconocido = true;
            if (Input.GetKey(KeyCode.E))
            {
                GameObject Itempickedup = other.gameObject;

                ItemInventory item = Itempickedup.GetComponent<ItemInventory>();

                _inventory.AddItem(Itempickedup, item);
            }
        }
        else if (other.gameObject.CompareTag("ObjetoPickeable") && (maskCajones == (maskCajones | (1 << other.gameObject.layer))))
        {
            ultimoreconocido = true;
            other.GetComponent<OpenandClose>().AbreCierra();
            if (Input.GetKey(KeyCode.E))
            {
                
            }


        }
        else
        {
            ultimoreconocido = false;
        }
    }
    #region OBJETO 
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
    #endregion

    #region INTERACCION
    //private void SelectedObject(Transform trasnform)
    //{
    //    //transform.GetComponent<MeshRenderer>().material.color = Color.green;
    //    ultimoreconocido = trasnform.gameObject;
    //}
    //private void Deselect()
    //{
    //    if (ultimoreconocido)
    //    {
    //        //ultimoreconocido.GetComponent<Renderer>().material.color = Color.white;
    //        ultimoreconocido = null;
    //    }
    //}

    private void OnGUI()
    {
        if(ultimoreconocido) {
            _UI.SetActive(true);
        }
        else
        {
            _UI.SetActive(false);
        }
    }
    #endregion
}