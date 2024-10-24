using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    public GameObject inventory;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    public GameObject SlotHandler;

    void Start()
    {
        allSlots= SlotHandler.transform.childCount;
        slot = new GameObject[allSlots];    
        for(int i = 0; i < allSlots; i++)
        {
            slot[i]=SlotHandler.transform.GetChild(i).gameObject;
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
}
