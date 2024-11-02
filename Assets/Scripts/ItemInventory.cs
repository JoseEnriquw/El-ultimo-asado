using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ItemInventory : MonoBehaviour
{
    public int Id;
    public string Type;
    public string Description;
    public Sprite Icon;
    [HideInInspector] public bool PickedUp;
    [HideInInspector] public bool equipped;
    [HideInInspector] public GameObject usableManager;
    public GameObject usable;
    public bool playerusable;

    private void Start()
    {
        usableManager = GameObject.FindWithTag("Player");

    }
    private void Update()
    {
        if (equipped && Input.GetKey(KeyCode.E))
        {
            equipped = false;
            gameObject.SetActive(false);
        }
    }

    public void ItemUsage()
    {

        if (usable == null && playerusable)
        {
            int allUsables = usableManager.transform.childCount;
            for (int i = 0; i < allUsables; i++)
            {
                GameObject child = usableManager.transform.GetChild(i).gameObject;

                if (child.GetComponent<ItemInventory>() != null && child.GetComponent<ItemInventory>().Id == Id)
                {
                    usable = child;
                    break;
                }
            }
        }

        if (Type == "usable" && usable != null)
        {
            usable.SetActive(true);
            usable.GetComponent<ItemInventory>().equipped = true;
        }
        else
        {
            Debug.LogWarning("No se pudo encontrar el objeto usable para este ItemInventory.");
        }
    }
}