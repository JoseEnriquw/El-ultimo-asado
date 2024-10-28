using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rama_PlayerController : MonoBehaviour
{
    private CharacterController _player;
    [SerializeField] private float _moveSpeed, _gravity, _fallVelocity, _JumpForce;
    private Vector3 _axis, _movePlayer;

    //RAYCAST
    [SerializeField] private Transform raycastOrigin; //Raycast
    [SerializeField] private float checkDistance; //chequeo de distancia
    [SerializeField] private LayerMask CuadroLayer; //capa que vamos a chequear

    private void Awake()
    {
        _player = GetComponent<CharacterController>();
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X"),0);
        _axis = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        //si la velocidad de movimiento es >1
        if (_axis.magnitude > 1) _axis = transform.TransformDirection(_axis).normalized;
        else _axis = transform.TransformDirection(_axis);

        //le paso movimiento al movePlayer
        _movePlayer.x= _axis.x;
        _movePlayer.z = _axis.z;
        setGravity();

        _player.Move(_movePlayer * _moveSpeed * Time.deltaTime);


        //RAYCAST hacia adelante
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, checkDistance, CuadroLayer))
        {
            // Si el raycast detecta un objeto en la capa CuadroLayer
            Rigidbody cuadroRb = hit.collider.GetComponent<Rigidbody>();
            if (cuadroRb != null)
            {
                cuadroRb.isKinematic = false; // Deshabilitar isKinematic para que se aplique la física
                cuadroRb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // Aplicar fuerza hacia abajo
            }
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.forward * checkDistance);
    }

    private void setGravity()
    {
        if (_player.isGrounded) //si el pj esta tocando el piso en el ultimo movimiento
        {
            _fallVelocity = -_gravity * Time.deltaTime; //velocidad de caida le aplico gravedad
            if (Input.GetKey(KeyCode.Space)) 
            {
                _fallVelocity = _JumpForce; //le damos fuerza de salto
            }
            
        }
        else //si no esta tocando el piso
        {
            _fallVelocity -= _gravity * Time.deltaTime; //velocidad de caida le aplico gravedad
        }
        _movePlayer.y = _fallVelocity; //agrego gravedad al eje Y
    }
    
}
