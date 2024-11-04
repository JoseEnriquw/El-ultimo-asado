using Assets.Scripts.GameManager;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float pushPower = 2.0f;

    private void OnControllerColliderHit(ControllerColliderHit hit) //actua cuando colisionamos con un objeto
    {
        Rigidbody body = hit.collider.attachedRigidbody; // almacenamos el Rigidbody contra el que colisionaremos

        // Si no colisiona con nada o si es kinemático, retorna
        if (body == null || body.isKinematic)
        {
            return;
        }

        // Si el objeto no se está moviendo hacia abajo
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        if (hit.gameObject.name == "kids_room_rack" && GameManager.GetGameManager().GetSceneNumber()==4)
        {
            Text textComponent = body.GetComponent<Text>();
            var text = "Empujar con 'E'";
            textComponent.UpdateTextBasedOnInteraction(true, text, false);
            if (Input.GetKey(KeyCode.E))
            {
                Vector3 pushDir = new(hit.moveDirection.x, 0, hit.moveDirection.z);
                body.AddForce(pushDir * pushPower, ForceMode.Impulse);
            }
        }
    }
}
