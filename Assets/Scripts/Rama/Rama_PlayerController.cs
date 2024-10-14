using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rama_PlayerController : MonoBehaviour
{
    private CharacterController _player;

    [SerializeField] private float _moveSpeed, _gravity, _fallVelocity, _JumpForce;

    private Vector3 _axis, _movePlayer;

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
