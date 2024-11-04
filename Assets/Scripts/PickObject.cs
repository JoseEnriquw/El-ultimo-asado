using Assets.Scripts.Character;
using Assets.Scripts.GameManager;
using System;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    [SerializeField] private GameObject hanPoint;
    private GameObject pickedObject = null;
    public bool IsPickedObject =false;
    Inventory _inventory;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform pjtransform;
    private LayerMask maskCajones;
    [SerializeField] private GameObject _UI;
    bool ultimoreconocido = false;
    private HealthKitCounter healthKitCounter;
    private CountFuse countFuse;
    private bool Busquedabotiquin = false;
    private bool BusquedaFusible = false;
    bool MedicalKitCompleted = false;
    bool ElectricPanelCompleted = false;
    private int ContadorMedikalkit = 0;
    private int ContadorFuse = 0;
    PlayerSounds _playsound;
    JoshAnimatorControllerState _josh;
    public bool ChangueScene = false;
    GameManager _gamenager;
    private int maxBotiquines =5;
    private int maxFuse = 2;
    Pariilla _parrilla;
    public string hasTaginhand = "";
    private TriggerJodyAnimation triggerJodyAnimation;
     

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        maskCajones = LayerMask.GetMask("Cajones");

        healthKitCounter = FindObjectOfType<HealthKitCounter>();
        _playsound = FindObjectOfType<PlayerSounds>();
        _josh = FindObjectOfType<JoshAnimatorControllerState>();
        //_gamenager = FindObjectOfType<GameManager>();
        countFuse = FindObjectOfType<CountFuse>();
        _parrilla= FindObjectOfType<Pariilla>();
        triggerJodyAnimation= FindObjectOfType<TriggerJodyAnimation>();
    }

    void Update()
    {
        pjtransform = gameObject.GetComponentInParent<Transform>();

        MedicalKitCompleted = healthKitCounter != null ? healthKitCounter.Completed : true;
        ElectricPanelCompleted = countFuse.Completed;
        if (pickedObject != null)
        {
            GameObject otherObject = pickedObject.gameObject;             
            hasTaginhand = otherObject.tag;
            if (Input.GetKey(KeyCode.R) && pickedObject.CompareTag("ObjetoPickeable"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.transform.SetParent(null);
                pickedObject = null;
                IsPickedObject = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && pickedObject.CompareTag("ItemInventory"))
            {
                UnequipItem();
            }           

            CheckRaycastHit();
            //if (MedicalKitCompleted && pickedObject.CompareTag("MedicalKit")) ApplyMedicalKit();
        }
        else {
            hasTaginhand = "";
        }
        
    }
    #region ACCIONES CON PCKEDOBJECT
    private void CheckRaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, pjtransform.forward, out hit, checkDistance, layerMask))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                ItemInventory itemInventory = pickedObject.GetComponent<ItemInventory>();
                if (itemInventory != null && itemInventory.Id == interactable.interactionID)
                {
                    Debug.Log("Interacción válida con " + interactable.gameObject.name);
                    interactable.Interact();
                    var text = "Bien echo!!!";
                    UIManager.GetUIManager().SetTarea(text);
                    _playsound.PlayDropObject();
                    DestroyPickedObject();
                }
                else
                {
                    Debug.Log("El objeto equipado no puede interactuar con " + interactable.gameObject.name);
                    UIManager.GetUIManager().SetTarea("");
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
    #region MANEJO DE TRIGGERS
    private void OnTriggerStay(Collider other)
    {
        ultimoreconocido = false;

        GameObject otherObject = other.gameObject;
        string tag = otherObject.tag;
       //ebug.Log(tag);
        switch (tag)
        {
            case "ObjetoPickeable":
                HandleObjetoPickeable(otherObject);
                break;
            case "ItemInventory":
                HandleItemInventory(otherObject);
                break;
            case "MedicalKit":
                HandleMedicalKit(otherObject);
                break;
            case "Cuadros":
                HandleCuadros(otherObject);
                break;
            case "Linterna":
                HandleLinterna(otherObject);
                break;
            case "Parrilla":
                HandleAsado(otherObject);
                break;
            case "PanelElectrico":
                HandleElectricPanel(otherObject);
                break;
            case "EndScene":
                HandleEndScene(otherObject);
                break;
            default:
                break;
        }
    }

    private void HandleElectricPanel(GameObject otherObject)
    {
        InteractableObject interactable = otherObject.GetComponent<InteractableObject>();

        if (!BusquedaFusible && interactable != null && interactable.interactionID == 20 && !ElectricPanelCompleted && _inventory.hasLinterna)
        {
            BusquedaFusible = true;
            countFuse.IncrementarContador(true);
        }
        if (BusquedaFusible && interactable != null && interactable.interactionID == 21 && ElectricPanelCompleted && _inventory.hasLinterna)
        {
            if (ContadorFuse < 2)
            {               
                var text = $"Colocar Fusibles {ContadorFuse}/{maxFuse}";
                UIManager.GetUIManager().SetTarea(text);
                ApplyInventory(otherObject);
            }
            else
            {
                UIManager.GetUIManager().SetTarea("Ve a la cocina");
                Destroy(otherObject, 0.10f);
                triggerJodyAnimation.Activate();
                ChangueScene = true;
            }

        }
        else if (BusquedaFusible && !ElectricPanelCompleted && _inventory.hasLinterna)
        {
            ultimoreconocido = true;
            if (Input.GetKey(KeyCode.E))
            {
                _playsound.PlayPickObject();
                GameObject Itempickedup = otherObject.gameObject;
                ItemInventory item = otherObject.GetComponent<ItemInventory>();
                _inventory.AddItem(otherObject, item);
                countFuse.IncrementarContador();

            }
        }
    }

    private void HandleEndScene(GameObject otherObject)
    {
        if (ChangueScene)
            GameManager.GetGameManager().NextScene();
           // _gamenager.NextScene();
    }

    private void HandleAsado(GameObject otherObject)
    {
        if (pickedObject != null)
        {
            if (Input.GetKey(KeyCode.R)  && !_parrilla.Completed)
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.transform.SetParent(null);
                pickedObject = null;
                IsPickedObject = false;
                Destroy(pickedObject);
                _parrilla.IncrementarContador();
            }
        }
    }
    private void HandleLinterna(GameObject otherObject)
    {
        if (Input.GetKey(KeyCode.E) )
        {
            Text textComponent = otherObject.GetComponent<Text>();
            _inventory.Linterna.SetActive(true);
            Destroy(otherObject);
            _inventory.hasLinterna = true;
            var text = "Busca el Panel de Luz";
            textComponent.UpdateTextBasedOnInteraction(true, text, false);
        }
    }

    private void HandleObjetoPickeable(GameObject otherObject)
    {
        if (maskCajones != (maskCajones | (1 << otherObject.layer)))
        {
            ultimoreconocido = true;
            if (Input.GetKey(KeyCode.E) && pickedObject == null)
            {
                _playsound.PlayPickObject();
                Rigidbody rb = otherObject.GetComponent<Rigidbody>();
                rb.useGravity = false;
                rb.isKinematic = true;
                otherObject.transform.position = hanPoint.transform.position;
                otherObject.transform.SetParent(hanPoint.transform);
                pickedObject = otherObject;
                IsPickedObject = true;
                GameManager.GetGameManager().PickUpObject(pickedObject);
            }
        }
        else
        {
            ultimoreconocido = true;
            otherObject.GetComponent<OpenandClose>().AbreCierra();
        }
    }

    private void HandleItemInventory(GameObject otherObject)
    {
        ultimoreconocido = true;
        if (Input.GetKey(KeyCode.E))
        {
            _playsound.PlayPickObject();
            GameObject Itempickedup = otherObject.gameObject;
            ItemInventory item = otherObject.GetComponent<ItemInventory>();
            _inventory.AddItem(otherObject, item);
        }
    }

    private void HandleMedicalKit(GameObject otherObject)
    {
        InteractableObject interactable = otherObject.GetComponent<InteractableObject>();

        if (!Busquedabotiquin && interactable != null && interactable.interactionID == 11 && !MedicalKitCompleted)
        {
            Busquedabotiquin = true;
            healthKitCounter.IncrementarContador(true);
        }
        if (Busquedabotiquin && interactable != null && interactable.interactionID == 10 && MedicalKitCompleted)
        {
            if (ContadorMedikalkit < 5)
            {
                _josh.ForceResumeMovement();
                var text = $"Aplicar Primeros Auxilios {ContadorMedikalkit}/{maxBotiquines}";
                UIManager.GetUIManager().SetTarea(text);
                ApplyInventory(otherObject);
            }
            else
            {

                UIManager.GetUIManager().SetTarea("Busca a Josh");
                Destroy(otherObject, 0.10f);
                ChangueScene = true;
            }

        }
        else if (Busquedabotiquin && !MedicalKitCompleted)
        {
            ultimoreconocido = true;
            if (Input.GetKey(KeyCode.E))
            {
                _playsound.PlayPickObject();
                GameObject Itempickedup = otherObject.gameObject;
                ItemInventory item = otherObject.GetComponent<ItemInventory>();
                _inventory.AddItem(otherObject, item);
                healthKitCounter.IncrementarContador();

            }
        }
    }
    private void ApplyInventory(GameObject otherObject)
    {
        if (pickedObject != null)
        {
            InteractableObject interactable = otherObject.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                ItemInventory itemInventory = pickedObject.GetComponent<ItemInventory>();
                if (itemInventory != null && itemInventory.Id == interactable.interactionID)
                {
                    Debug.Log("Interacción válida con " + interactable.gameObject.name);
                    interactable.Interact();
                    var text = "Bien echo!!!";
                    UIManager.GetUIManager().SetTarea(text);
                    _playsound.PlayDropObject();
                    DestroyPickedObject();
                    ContadorMedikalkit++;
                    ContadorFuse++; //ver de arreglar despues por escena
                }
                else
                {
                    Debug.Log("El objeto equipado no puede interactuar con " + interactable.gameObject.name);
                    UIManager.GetUIManager().SetTarea("");
                }
            }
        }
    }
    private void HandleCuadros(GameObject otherObject)
    {
        Rigidbody cuadroRb = otherObject.GetComponent<Rigidbody>();
        if (cuadroRb != null)
        {
            //REVISAR
            cuadroRb.isKinematic = false; // Deshabilitar isKinematic para que se aplique la física
            cuadroRb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Aplicar fuerza hacia abajo
            Collider cuadroCollider = otherObject.GetComponent<Collider>();
            if (cuadroCollider != null)
            {
                cuadroCollider.enabled = false; // Desactivar el colisionador para que no bloquee el paso
            }
        }
    }

    #endregion
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
        IsPickedObject = false;

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
            IsPickedObject = false;
        }
    }
    #endregion
}