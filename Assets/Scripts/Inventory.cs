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
        allSlots = SlotHandler.transform.childCount;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = SlotHandler.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null)
                slot[i].GetComponent<Slot>().empty = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
            UpdateCursorState();
        }
        if (inventoryEnabled)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    private void UpdateCursorState()
    {
        if (inventoryEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void CloseInventory()
    {
        inventoryEnabled = false;
        inventory.SetActive(false);
        UpdateCursorState();
    }
    //ENTRA EN CONFLICTO CON EL DE PCKEDOBJECT
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("ItemInventory"))
    //    {
    //        if (Input.GetKey(KeyCode.E))
    //        {
    //            GameObject Itempickedup = other.gameObject;

    //            ItemInventory item= Itempickedup.GetComponent<ItemInventory>();

    //            AddItem(Itempickedup, item);
    //        }
    //    }
    //}

    public void AddItem(GameObject itemobjet, ItemInventory iteminventory)
    {
        if (iteminventory.equipped) return;
        for (int i = 0; i < allSlots; i++)
        {
            if (slot[i].GetComponent<Slot>().empty)
            {
                itemobjet.GetComponent<ItemInventory>().PickedUp = true;
                slot[i].GetComponent<Slot>().item = itemobjet;
                slot[i].GetComponent<Slot>().Id = iteminventory.Id;
                slot[i].GetComponent<Slot>().Type = iteminventory.Type;
                slot[i].GetComponent<Slot>().Description = iteminventory.Description;
                slot[i].GetComponent<Slot>().Icon = iteminventory.Icon;

                itemobjet.transform.parent = slot[i].transform;
                itemobjet.SetActive(false);
                slot[i].GetComponent<Slot>().UpdateSlot();

                slot[i].GetComponent<Slot>().empty = false;




                return;
            }

        }

    }
    public void RemoveItemFromInventory(ItemInventory itemInventory)
    {
        for (int i = 0; i < allSlots; i++)
        {
            //Slot slot = slot[i].GetComponent<Slot>();
            if (!slot[i].GetComponent<Slot>().empty && slot[i].GetComponent<Slot>().Id == itemInventory.Id)
            {
                slot[i].GetComponent<Slot>().item = null;
                slot[i].GetComponent<Slot>().Id = 0;
                slot[i].GetComponent<Slot>().Type = "";
                slot[i].GetComponent<Slot>().Description = "";
                slot[i].GetComponent<Slot>().Icon = null;
                slot[i].GetComponent<Slot>().empty = true;

                slot[i].GetComponent<Slot>().Removeicon();
                break;
            }
        }
    }


}