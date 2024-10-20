using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    public GameObject hanPoint;
    private GameObject pickedObject = null;
    
    void Update()
    {
        if(pickedObject != null)
        {
            if(Input.GetKey(KeyCode.R))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.gameObject.transform.SetParent(null);
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
    }
}
