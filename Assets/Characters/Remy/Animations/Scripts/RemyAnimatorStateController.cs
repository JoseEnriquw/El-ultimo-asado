using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class RemyAnimatorStateController : MonoBehaviour
{
    // la lista de wayPoints asignados desde el editor
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();

    public bool isMoving;
    public bool isWaiting;
    public int wayPointIndex;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    // para aplicar movimiento en bucle
    [SerializeField] private bool isLoop;

    // para aplicar movimiento aleatorio entre los waypoints
    [SerializeField] private bool isRandom;

    // solo para probar la animacion de muerte
    [SerializeField] private bool isDead;

    // almacenaremos posicion antes de manipular transform
    private Vector3 previousPosition;
    private float movingDifference;

    // el animator para administrar la maquina de estados
    Animator animator;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        // inicia siempre con el primer waypoint
        wayPointIndex = 0;
        //isMoving = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        previousPosition = new Vector3(rb.transform.position.x, 0.0f, rb.transform.position.z);

        // al comienzo esta vivo
        isDead = false;
        isWaiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWaiting)
        {
            Animate();
        }
        
        if (!isDead)
        {
            if (!isWaiting)
            {
                WaypointMovement();
            }
        }

    }

    private void Animate()
    {
        // obtenemos el bool que decide la transicion a walking
        bool isWalking = animator.GetBool("isWalking");
        //if (!isMoving)
        //{
        //    return;
        //}

        // almacenamos en un float si hubo desplazamiento del character
        movingDifference = Vector3.Distance(previousPosition, new Vector3(rb.transform.position.x, 0.0f, rb.transform.position.z));
        //Debug.Log(movingDifference);

        // chequeamos si se movio mas que una cierta distancia
        isMoving = movingDifference >= 0.0005f;
        //Debug.Log(isMoving);

        if (!isDead)
        {
            animator.SetFloat("speedMultiplier", moveSpeed);
            // animacion desactivada y se esta moviendo?
            if (!isWalking && isMoving)
            {
                // activa animacion
                animator.SetBool("isWalking", true);
            }
            // animacion activada  y ya no se esta desplazando el character
            if (isWalking && !isMoving)
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            animator.SetBool("isDead", true);
        }

    }

    private void WaypointMovement()
    {
        previousPosition = new Vector3(rb.transform.position.x, 0.0f, rb.transform.position.z);
        // ingresa siempre que tengamos mas de 1 waypoint al comienzo
        if (wayPointIndex < wayPoints.Count)
        {
            // mueve el personaje hacia el waypoint
            rb.transform.position = Vector3.MoveTowards(rb.transform.position, wayPoints[wayPointIndex].position, Time.deltaTime * moveSpeed);

            // obtiene vector direccion (hacia el waypoint)
            Vector3 direction = wayPoints[wayPointIndex].position - rb.transform.position;

            // calcula el el angulo de rotacion y aplica rotacion con LERP
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.transform.rotation = Quaternion.Lerp(rb.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // calcula la distancia hacia siguiente waypoint
            float distance = Vector3.Distance(rb.transform.position, wayPoints[wayPointIndex].position);

            // llego al siguiente waypoint?
            if (distance <= 0.05f)
            {
                // random activo?
                if (isRandom)
                {
                    // asigna waypoint aleatorio
                    wayPointIndex = Random.Range(0, wayPoints.Count);
                }
                else
                {
                    // incrementa el contador de waypoint
                    wayPointIndex++;

                    // modo bucle y el contador llego al final del waypoint list
                    if (isLoop && wayPointIndex >= wayPoints.Count)
                    {
                        // comenzar desde el primer elemento del array
                        wayPointIndex = 0;
                    }
                }

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaitTime"))
        {
            movingDifference = 0.0f;
            isWaiting= true;
            animator.SetBool("isWalking", false);
            wayPointIndex++;
            Invoke("IsWaiting", 5.0f);
            Debug.Log("should wait here...");
        }
    }

    public void IsWaiting()
    {
        isWaiting = false;
    }
}
