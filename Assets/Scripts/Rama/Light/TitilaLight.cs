using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitilaLight : MonoBehaviour
{

    [SerializeField] private bool titila = false; //indica si la luz está actualmente parpadeando
    [SerializeField] private float timeDelay; //tiempo de espera entre los ciclos de encendido y apagado de la luz.
    [SerializeField] private Light _light;

    private void Awake()
    {
        _light = this.gameObject.GetComponent<Light>();
    }


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
        _light.enabled = false; //Luz se apaga
        timeDelay = Random.Range(0.01f, 0.2f); // determina un timeDelay aleatorio entre 0.01 y 0.2 segundos.
        yield return new WaitForSeconds(timeDelay);
        _light.enabled = true; //Luz se enciende
        timeDelay = Random.Range(0.01f, 0.2f); // se calcula otro timeDelay aleatorio y se espera nuevamente.
        yield return new WaitForSeconds(timeDelay);
        titila = false; //titila se apaga
    }
}
