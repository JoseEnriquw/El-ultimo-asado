using Assets.Scripts.Character;
using Assets.Scripts.GameManager;
using System;
using UnityEngine;

public class PickObject : MonoBehaviour
{
    [SerializeField] private GameObject hanPoint;
    private GameObject pickedObject = null;
    Inventory _inventory;
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform pjtransform;
    private LayerMask maskCajones;
    [SerializeField] private GameObject _UI;
    bool ultimoreconocido = false;
    private HealthKitCounter healthKitCounter;
    private bool Busquedabotiquin = false;
    bool Completed = false;
    private int ContadorMedikalkit = 0;
    PlayerSounds _playsound;
    JoshAnimatorControllerState _josh;
    public bool ChangueScene = false;
    GameManager _gamenager;
    private int maxBotiquines =5;

    private void Start()
    {
        _inventory = FindObjectOfType<Inventory>();
        maskCajones = LayerMask.GetMask("Cajones");
        healthKitCounter = FindObjectOfType<HealthKitCounter>();
        _playsound = FindObjectOfType<PlayerSounds>();
        _josh = FindObjectOfType<JoshAnimatorControllerState>();
        _gamenager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        pjtransform = gameObject.GetComponentInParent<Transform>();
        Completed = healthKitCounter.Completed;
        if (pickedObject != null)
        {
            if (Input.GetKey(KeyCode.R) && pickedObject.CompareTag("ObjetoPickeable"))
            {
                pickedObject.GetComponent<Rigidbody>().useGravity = true;
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedObject.transform.SetParent(null);
                pickedObject = null;
            }
            if (Input.GetKeyDown(KeyCode.E) && pickedObject.CompareTag("ItemInventory"))
            {
                UnequipItem();
            }

            CheckRaycastHit();
            //if (Completed && pickedObject.CompareTag("MedicalKit")) ApplyMedicalKit();
        }

    }
    #region ACCIONES CON PCKEDOBJECT
    private void CheckRaycastHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, pjtransform.forward, out hit, checkDistance, layerMask))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            Text textComponent = hit.collider.GetComponent<Text>();
            if (interactable != null)
            {
                ItemInventory itemInventory = pickedObject.GetComponent<ItemInventory>();
                if (itemInventory != null && itemInventory.Id == interactable.interactionID)
                {
                    Debug.Log("Interacción válida con " + interactable.gameObject.name);
                    interactable.Interact();
                    var text = "Bien echo!!!";
                    textComponent.UpdateTextBasedOnInteraction(true, text);
                    _playsound.PlayDropObject();
                    DestroyPickedObject();
                }
                else
                {
                    Debug.Log("El objeto equipado no puede interactuar con " + interactable.gameObject.name);
                    textComponent.UpdateTextBasedOnInteraction(false, "");
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
                HandleAsado(otherObject);
                break;
            case "Asado":
                HandleAsado(otherObject);
                break;
            case "EndScene":
                HandleEndScene(otherObject);
                break;
            default:
                break;
        }
    }

    private void HandleEndScene(GameObject otherObject)
    {
        if (ChangueScene)
            _gamenager.NextScene();
    }

    private void HandleAsado(GameObject otherObject)
    {
        
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
        Text textComponent = otherObject.GetComponent<Text>();
        InteractableObject interactable = otherObject.GetComponent<InteractableObject>();

        if (!Busquedabotiquin && interactable != null && interactable.interactionID == 11 && !Completed)
        {
            Busquedabotiquin = true;
            healthKitCounter.IncrementarContador(true);
        }
        if (Busquedabotiquin && interactable != null && interactable.interactionID == 10 && Completed)
        {
            if (ContadorMedikalkit < 5)
            {
                _josh.ForceResumeMovement();
                var text = $"Aplicar Primeros Auxilios {ContadorMedikalkit}/{maxBotiquines}";
                textComponent.UpdateTextBasedOnInteraction(true, text, false);
                ApplyMedicalKit(otherObject);
            }
            else
            {
                                
                textComponent.UpdateTextBasedOnInteraction(true, "Busca a Josh", true);
                ChangueScene = true;
            }

        }
        else if (Busquedabotiquin && !Completed)
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
    private void ApplyMedicalKit(GameObject otherObject)
    {
        if (pickedObject != null)
        {

            InteractableObject interactable = otherObject.GetComponent<InteractableObject>();
            Text textComponent = otherObject.GetComponent<Text>();
            if (interactable != null)
            {
                ItemInventory itemInventory = pickedObject.GetComponent<ItemInventory>();
                if (itemInventory != null && itemInventory.Id == interactable.interactionID)
                {
                    Debug.Log("Interacción válida con " + interactable.gameObject.name);
                    interactable.Interact();
                    var text = "Bien echo!!!";
                    textComponent.UpdateTextBasedOnInteraction(true, text);
                    _playsound.PlayDropObject();
                    DestroyPickedObject();
                    ContadorMedikalkit++;
                }
                else
                {
                    Debug.Log("El objeto equipado no puede interactuar con " + interactable.gameObject.name);
                    textComponent.UpdateTextBasedOnInteraction(false, "");
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
        }
    }
    #endregion

    #region INTERACCION
    //private void SelectedObject(Transform trasnform)
    //{
    //    //transform.GetComponent<MeshRenderer>().material.color = Color.green;
    //    ultimoreconocido = trasnform.gameObject;
    //}
    //private void Deselect()
    //{
    //    if (ultimoreconocido)
    //    {
    //        //ultimoreconocido.GetComponent<Renderer>().material.color = Color.white;
    //        ultimoreconocido = null;
    //    }
    //}

    private void OnGUI()
    {
        if (ultimoreconocido)
        {
            _UI.SetActive(true);
        }
        else
        {
            _UI.SetActive(false);
        }
    }
    #endregion
}