using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    [SerializeField] private GameObject inventory;
    private int allSlots;
    private int enabledSlots;
    [SerializeField] private GameObject[] slot;
    [SerializeField] private GameObject SlotHandler;

    void Start()
    {
        allSlots= SlotHandler.transform.childCount;
        slot = new GameObject[allSlots];    
        for(int i = 0; i < allSlots; i++)
        {
            slot[i]=SlotHandler.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null) 
                slot[i].GetComponent<Slot>().empty = true;   
        } 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.I)) {
            inventoryEnabled = !inventoryEnabled;
        }
        if(inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ItemInventory"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                GameObject Itempickedup = other.gameObject;

                ItemInventory item= Itempickedup.GetComponent<ItemInventory>();

                AddItem(Itempickedup, item);
            }
        }
    }

    private void AddItem(GameObject itemobjet, ItemInventory iteminventory)
    {
        for(int i = 0;i < allSlots; i++) {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemobjet.GetComponent<ItemInventory>().PickedUp = true;

                slot[i].GetComponent<Slot>().item = itemobjet;
                slot[i].GetComponent<Slot>().Id=iteminventory.Id;
                slot[i].GetComponent<Slot>().Type = iteminventory.Type;
                slot[i].GetComponent<Slot>().Description= iteminventory.Description;
                slot[i].GetComponent<Slot>().Icon = iteminventory.Icon;

                itemobjet.transform.parent = slot[i].transform;
                itemobjet.SetActive(false);

                slot[i].GetComponent<Slot>().empty=false;

            }
        }

    }
}
