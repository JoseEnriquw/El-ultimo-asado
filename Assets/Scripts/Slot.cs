using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour/*, IPointerClickHandler*/
{
    public GameObject item;
    public int Id;
    public string Type;
    public string Description;
    public Sprite Icon;
    public bool empty;
    public Transform slotIconGameObject;
    private PickObject pickObject;
    private Inventory inventory;

    private void Awake()
    {
        slotIconGameObject = transform.GetChild(0);
        pickObject = FindObjectOfType<PickObject>();
        inventory = FindObjectOfType<Inventory>();
    }
    public void UpdateSlot()
    {
         slotIconGameObject.GetComponent<Image>().sprite = Icon;
       


    }

    public void Removeicon()
    {
         slotIconGameObject.GetComponent<Image>().sprite = null;
       
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.GetComponent<ItemInventory>().ItemUsage();
        }
    }

    public void OnPointerClick(/*PointerEventData eventData*/)
    {
        /*if (eventData.button == PointerEventData.InputButton.Left)
        {
        }*/
            pickObject.EquipItem(item);
            inventory.CloseInventory();
    }


}