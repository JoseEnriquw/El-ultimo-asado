using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float pushPower = 2.0f; //fuerza de empuje

    private float targetMass; //masa del objeto contra el que chocamos (pesado o liviano)

    private void OnControllerColliderHit(ControllerColliderHit hit) //actua cuando colisionamos con un objeto
    {
        Rigidbody body = hit.collider.attachedRigidbody; //almacenamos el rigibody contra el que colisionaremos

        if (body == null || body.isKinematic) //si no colisiona con nada o si es kinematico 
        {
            return;
        }

        if(hit.moveDirection.y < -0.3) //si noempujamos hacia abajo
        {
            return;
        }

        targetMass = body.mass;

        Vector3 pushDir = new Vector3(hit.moveDirection.x,0,hit.moveDirection.z); //creamos dirección
        body.velocity = pushDir * pushPower / targetMass;
    }
}
