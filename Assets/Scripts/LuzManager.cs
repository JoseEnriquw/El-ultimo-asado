using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Luces;
    [SerializeField] private bool Luzcortada=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarLuces()
    {
        foreach (var l in _Luces) { 
        l.gameObject.SetActive(true);
        }
        Luzcortada=false ;
    }
    public void DesactivarLuces()
    {
        foreach (var l in _Luces)
        {
            l.gameObject.SetActive(false);
        }
        Luzcortada = true;
    }

    public bool getEstadoLuz()
    {
        return this.Luzcortada;
    }
}
