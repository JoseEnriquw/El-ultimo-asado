using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitilaLight : MonoBehaviour
{

    public bool titila = false; //indica si la luz está actualmente parpadeando
    public float timeDelay; //tiempo de espera entre los ciclos de encendido y apagado de la luz.


    // Update is called once per frame
    void Update()
    {
        if (titila == false) //si titila esta falso
        {
            StartCoroutine(LuzQueTitila()); //activa titila
        }
    }
    IEnumerator LuzQueTitila()
    {
        titila = true; //titila se enciende
        this.gameObject.GetComponent<Light>().enabled = false; //Luz se apaga
        timeDelay = Random.Range(0.01f, 0.2f); // determina un timeDelay aleatorio entre 0.01 y 0.2 segundos.
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true; //Luz se enciende
        timeDelay = Random.Range(0.01f, 0.2f); // se calcula otro timeDelay aleatorio y se espera nuevamente.
        yield return new WaitForSeconds(timeDelay);
        titila = false; //titila se apaga
    }
}
