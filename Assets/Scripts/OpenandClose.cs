using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenandClose : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 posAbierto= new Vector3(1.169f, 1.140778f, 0);
    [SerializeField] private float cuandoAbre;
    [SerializeField] public float difCajonAbierto;

    bool abierto = false;
    bool abriendo = false;
    bool cerrando = false;
    Vector3 posCerrado = new Vector3(0.496f, 1.140778f, 0);

    private void Start()
    {
        //posCerrado = transform.position;
       // posAbierto = new Vector3(transform.localPosition.x - cuandoAbre, transform.localPosition.y, transform.localPosition.z);

    }
    public void AbreCierra()
    {
        // Cambia el estado de apertura/cierre según el estado actual
        if (!abierto)
        {
            abriendo = true;
            cerrando = false;
        }
        else
        {
            cerrando = true;
            abriendo = false;
        }
    }

    private void Update()
    {
        // Lógica para abrir el cajón
        if (abriendo)
        {
            MoverCajon(posAbierto, ref abriendo, true);
        }
        // Lógica para cerrar el cajón
        else if (cerrando)
        {
            MoverCajon(posCerrado, ref cerrando, false);
        }
    }

    // Función que maneja el movimiento del cajón y el cambio de estado
    //private void MoverCajon(Vector3 destino, ref bool estadoMovimiento, bool nuevoEstadoAbierto)
    //{
    //    transform.localPosition = Vector3.MoveTowards(transform.localPosition, destino, Time.deltaTime * velocidad);

    //    // Comprueba si ha llegado al destino (independientemente de si el destino es positivo o negativo)
    //    if (Vector3.Distance(transform.localPosition, destino) < 0.0001f)
    //    {
    //        abierto = nuevoEstadoAbierto;  // Actualiza el estado de "abierto" según el objetivo
    //        estadoMovimiento = false;      // Detiene el movimiento actual
    //    }
    //}

    private void MoverCajon(Vector3 destino, ref bool estadoMovimiento, bool nuevoEstadoAbierto)
    {
        // Mueve el cajón hacia la posición destino
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destino, Time.deltaTime * velocidad);

        // Verifica si el cajón ha alcanzado exactamente el destino
        if (Vector3.Distance(transform.localPosition, destino) < 0.001f) // Cambiado a Vector3.Distance para mayor precisión
        {
            transform.localPosition = destino; // Asegúrate de que el cajón esté exactamente en la posición de destino
            abierto = nuevoEstadoAbierto;       // Actualiza el estado "abierto" o "cerrado"
            estadoMovimiento = false;           // Detiene el movimiento actual (abriendo o cerrando)
        }
    }
    //private void Update()
    //{
    //    //LO UTILIZO PARA SABER CUANTO SE TIENE Q ABRIR EL CAJON
    //    difCajonAbierto=posCerrado.x-transform.localPosition.x;


    //}

    //public void AbreCierra()
    //{
    //    if (!abierto) abriendo = true;
    //    else cerrando = true;

    //    if (abriendo)
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, posCerrado, Time.deltaTime * velocidad);
    //        if (Vector3.Distance(transform.localPosition, posCerrado) < 0.00001f)
    //        {
    //            abierto = true;
    //            abriendo = false;
    //        }
    //    }
    //    if (cerrando)
    //    {
    //        transform.localPosition = Vector3.MoveTowards(transform.localPosition, posAbierto, Time.deltaTime * velocidad);
    //        if (Vector3.Distance(transform.localPosition, posAbierto) < 0.00001f)
    //        {
    //            abierto = false;
    //            cerrando = false;
    //        }
    //    }
    //}
}
