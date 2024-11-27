using Assets.Scripts.GameManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    private bool inventoryEnabled;
    [SerializeField] private GameObject inventory;
    public GameObject Linterna;
    private int allSlots;
    private int enabledSlots;
    [SerializeField] private GameObject[] slot;
    [SerializeField] private GameObject SlotHandler;
    public bool hasLinterna = false;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

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

    private void OnEnable()
    {
        playerInput.actions["Inventory"].performed += DoSetActive;
    }

    private void OnDisable()
    {
        playerInput.actions["Inventory"].performed -= DoSetActive;
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
            if (!_slot.empty && _slot.Id == itemInventory.Id)
            {
                _slot.item = null;
                _slot.Id = 0;
                _slot.Type = "";
                _slot.Description = "";
                _slot.Icon = null;
                _slot.empty = true;

                _slot.Removeicon();
                break;
            }
        }
    }


}