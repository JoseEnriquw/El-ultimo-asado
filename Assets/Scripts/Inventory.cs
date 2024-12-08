using Assets.Scripts.GameManager;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    [SerializeField] private GameObject inventory;
    public GameObject Linterna;
    private int allSlots;
    Slot _randomSlot;
    private int enabledSlots;
    [SerializeField] private GameObject[] slot;
    [SerializeField] private GameObject SlotHandler;
    public bool hasLinterna = false;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        allSlots = SlotHandler.transform.childCount;
        slot = new GameObject[allSlots];
        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = SlotHandler.transform.GetChild(i).gameObject;
            if (slot[i].GetComponent<Slot>().item == null)
                slot[i].GetComponent<Slot>().empty = true;
        }
        inventory.SetActive(true);
        _randomSlot = FindAnyObjectByType<Slot>();
    }

    void Start()
    {
        inventory.SetActive(false);

    }   

    private void OnEnable()
    {
        playerInput.actions["Inventory"].performed += DoSetActive;
        playerInput.actions["SlotInteraction"].performed += HandleSlotInteraction;
    }

    private void OnDisable()
    {
        playerInput.actions["Inventory"].performed -= DoSetActive;
        playerInput.actions["SlotInteraction"].performed -= HandleSlotInteraction;
    }


    private void DoSetActive(InputAction.CallbackContext callbackContext)
    {
        
        inventoryEnabled = !inventoryEnabled;
        inventory.SetActive(inventoryEnabled);
       
        UpdateCursorState();
    }
  

    private void UpdateCursorState()
    {
        GameManager.GetGameManager().SetEnablePlayerInput(!inventoryEnabled);
        
    }
    public void CloseInventory()
    {
        inventoryEnabled = false;
        inventory.SetActive(false);
        UpdateCursorState();
    }
   

    public void AddItem(GameObject itemobjet, ItemInventory iteminventory)
    {
        if (iteminventory.equipped) return;
        for (int i = 0; i < allSlots; i++)
        {
            var _slot = slot[i].GetComponent<Slot>();
            if (_slot.empty)
            {
                itemobjet.GetComponent<ItemInventory>().PickedUp = true;
                _slot.item = itemobjet;
                _slot.Id = iteminventory.Id;
                _slot.Type = iteminventory.Type;
                _slot.Description = iteminventory.Description;
                _slot.Icon = iteminventory.Icon;

                itemobjet.transform.parent = slot[i].transform;
                itemobjet.SetActive(false);
                 _slot.UpdateSlot();
               // _slot.GetComponent<Image>().sprite = iteminventory.Icon;
                _slot.empty = false;




                return;
            }

        }

    }
    public void RemoveItemFromInventory(ItemInventory itemInventory)
    {
        for (int i = 0; i < allSlots; i++)
        {
            var _slot = slot[i].GetComponent<Slot>();
            if (!_slot.empty && _slot.Id == itemInventory.Id && _slot.Description== itemInventory.Description)
            {
                _slot.item = null;
                _slot.Id = 0;
                _slot.Type = "";
                _slot.Description = "";
                _slot.Icon = null;
                _slot.empty = true;
                _slot.GetComponent<Image>().sprite = null;
                 _slot.Removeicon();
                break;
            }
        }
    }
    private void HandleSlotInteraction(InputAction.CallbackContext ctx)
    {
        try
        {
            Debug.Log("Clic detectado en SlotInteraction");
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue() // Posición del cursor
            };

            // Lista para almacenar los resultados del Raycast
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            // Procesar los resultados del Raycast
            foreach (RaycastResult result in results)
            {
                Debug.Log($"Clic en: {result.gameObject.name}");

                // Comprobar si el objeto clicado es un Slot
                Slot randomSlot = result.gameObject.GetComponent<Slot>();
                if (randomSlot != null)
                {
                    Debug.Log($"Clic detectado en Slot: {randomSlot.name}, Item: {randomSlot.item?.name}");
                    if(randomSlot.item != null)
                        randomSlot.OnPointerClick(); // Ejecutar lógica del Slot
                    break; // Salir después de procesar un clic válido
                }
            }

            if (results.Count == 0)
            {
                Debug.LogWarning("No se detectó ningún objeto bajo el cursor.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al ejecutar SlotInteraction: {ex.Message}\n{ex.StackTrace}");
        }

    }
        

}