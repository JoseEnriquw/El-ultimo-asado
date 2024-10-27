using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrianAnimatorControllerState : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool("isWalking"); //true si esta caminando

        // ATENCION: Si el personaje no es jugable habrá que modificar las condiciones de Input!!!

        bool forwardPressed = Input.GetKey("w"); //true si se esta presionando W
        //no camina y se esta presionando w
        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }
        //esta caminando y ya no se esta presionando w
        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
        }
    }
}
