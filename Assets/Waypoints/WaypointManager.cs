using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    // la lista de wayPoints asignados desde el editor
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();

    public bool isMoving;
    public int wayPointIndex;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    
    // para aplicar movimiento en bucle
    [SerializeField] bool isLoop;

    // para aplicar movimiento aleatorio entre los waypoints
    [SerializeField] bool isRandom;
    
    // solo para probar la animacion de muerte
    [SerializeField] bool isDead;

    // almacenaremos posicion antes de manipular transform
    private Vector3 previousPosition;

    // el animator para administrar la maquina de estados
    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        // inicia siempre con el primer waypoint
        wayPointIndex = 0;
        //isMoving = true;
        animator = GetComponent<Animator>();
        previousPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        
        // al comienzo esta vivo
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        Animate();

        if(!isDead)
        {
            WaypointMovement();
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
        float movingDifference = Vector3.Distance(previousPosition, transform.position);
        //Debug.Log(movingDifference);

        // chequeamos si se movio mas que una cierta distancia
        isMoving = movingDifference >= 0.0005f;
        //Debug.Log(isMoving.ToString());

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
        } else
        {
            animator.SetBool("isDead", true);
        }
        
    }

    private void WaypointMovement()
    {
        previousPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
        // ingresa siempre que tengamos mas de 1 waypoint al comienzo
        if (wayPointIndex < wayPoints.Count)
        {
            // mueve el personaje hacia el waypoint
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointIndex].position, Time.deltaTime * moveSpeed);

            // obtiene vector direccion (hacia el waypoint)
            Vector3 direction = wayPoints[wayPointIndex].position - transform.position;

            // calcula el el angulo de rotacion y aplica rotacion con LERP
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // calcula la distancia hacia siguiente waypoint
            float distance = Vector3.Distance(transform.position, wayPoints[wayPointIndex].position);

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
}
