using System.Collections.Generic;
using UnityEngine;

public class CharacterWaypoint : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new();
    private bool isMoving;
    private int wayPointIndex;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] bool isLoop;
    [SerializeField] bool isRandom;

    private Vector3 previousPosition;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        // inicia siempre con el primer waypoint
        wayPointIndex = 0;
        animator = GetComponent<Animator>();
        previousPosition = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // obtenemos el bool que decide la transicion a walking
        bool isWalking = animator.GetBool("isWalking");

        // almacenamos en un float si hubo desplazamiento del character
        float movingDifference = Vector3.Distance(previousPosition, transform.position);

        // chequeamos si se movio mas que una cierta distancia
        isMoving = movingDifference >= 0.0005f;

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

