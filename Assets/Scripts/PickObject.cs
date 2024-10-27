using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    [SerializeField] private GameObject hanPoint;
    private GameObject pickedObject = null;
    Inventory _inventory;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if(pickedObject != null)
        {
            if(Input.GetKey(KeyCode.R))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
               // pickedObject.gameObject.transform.SetParent(null);
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
            if (Input.GetKeyDown(KeyCode.E))  // Desequipar con "E"
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
        }
       
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
        if (pickedObject != null)  // Libera el objeto anterior si hay uno
        {
            pickedObject.GetComponent<Rigidbody>().useGravity = true;
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject.transform.SetParent(null);
        }

        pickedObject = item;  // Asigna el nuevo objeto seleccionado
        if (pickedObject != null)
        {
            pickedObject.transform.position = hanPoint.transform.position;
            pickedObject.transform.SetParent(hanPoint.transform);
            pickedObject.GetComponent<Rigidbody>().useGravity = false;
            pickedObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
