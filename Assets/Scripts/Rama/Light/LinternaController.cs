using Assets.Scripts.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinternaController : MonoBehaviour
{
    [SerializeField] private Light linterna; // Referencia a la luz de la linterna
    private bool isLinternaOn = false; // Estado de la linterna
    private PlayerCharacter _playerCharacter;
    //private PlayerInput playerInput;
    //private void Awake()
    //{
    //    playerInput = GetComponent<PlayerInput>();
    //}
    private void Awake()
    {
        _playerCharacter= FindObjectOfType<PlayerCharacter>();
    }

    void Start()
    {
        // Asegúrate de que la linterna esté apagada al inicio
        if (linterna != null)
        {
            linterna.enabled = false;
        }
    }

    void Update()
    {
        // Comprueba si se presiona la tecla "F"
        if (Input.GetKeyDown(KeyCode.F))
        {
            DoToggleLinterna(); // Llama a la función para encender/apagar la linterna
        }
        linterna.transform.rotation = _playerCharacter.getRotationToLantern();

    }

    //private void OnEnable()
    //{
    //    playerInput.actions["Linterna"].performed += DoToggleLinterna;
    //}

    //private void OnDisable()
    //{
    //    playerInput.actions["Linterna"].performed -= DoToggleLinterna;
    //}
    public void DoToggleLinterna(/*InputAction.CallbackContext callbackContext*/)
    {
        // Cambia el estado de la linterna
        isLinternaOn = !isLinternaOn;
        linterna.enabled = isLinternaOn; // Enciende o apaga la luz
        
    }
}